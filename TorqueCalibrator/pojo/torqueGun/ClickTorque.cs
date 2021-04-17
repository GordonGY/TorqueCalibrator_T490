using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TorqueCalibrator.pojo.user;
using TorqueCalibrator.untils;
using TorqueCalibrator.variables;

namespace TorqueCalibrator.pojo.torqueGun
{
    /// <summary>
    /// 咔嗒扳手
    /// </summary>
    public class ClickTorque : BaseTorque
    {

        public override void doTest()
        {
            Action<TextBox, string> changeWndText = new Action<TextBox, string>(changeWndTextBoxText);
            Action<RichTextBox, string> changeWndRich = new Action<RichTextBox, string>(changeWndRichTextBoxText);
            Action<DataGridView, RecordDetail, bool> changeWndDgv = new Action<DataGridView, RecordDetail, bool>(changeWndDgvText);
            //清空RecordDetailDgv界面
            //DataGridViewClear();
            
            //清空RecordDetailDgv界面
            wnd.RecordDetailDgv.Invoke(changeWndDgv, wnd.RecordDetailDgv, null, true);
            //初始化记录
            this.initRecord(this.CheckMode);
            Vars.Result = "";

            //按照工艺中规定的进行校验
            for (int i = 0; i < this.CurrentTech.TechnologyDetailList.Count; i++)
            {
                //将该发送的模式，设置下去，基类中实现
                this.setMode(serialPort);

                //发送设置校验仪传感器选择位
                this.choseSensor(this.CurrentTech.TechnologyDetailList[i].Standard);

                //试验置位
                s7Help.WriteTestReadyStart(true);

                //等等试验准备完成
                while (true)
                {
                    if (s7Help.ReadTestReadyFinish())
                    {
                        break;
                    }
                    Thread.Sleep(100);
                }

                //试验开始置位
                s7Help.WriteTestStart(true);

                //开始循环
                for (int j = 0; j < this.CurrentTech.TechnologyDetailList[i].Num; j++)
                {
                    this.initRecordDetail(this.CurrentTech.TechnologyDetailList[i]);

                    //试验速度模式速度下发
                    s7Help.WriteMotorStartVSpeed(this.speed);

                    //100ms小延时
                    Thread.Sleep(100);


                    //试验速度模式使能
                    if (!Vars.ControlMode) s7Help.WriteMotorStartV(true);
                    
                    //数据采集，完成后停止
                    while (true)
                    {
                        if (Vars.Result.Contains(@"N.m"))
                        {
                            //wnd.Controls["textBox4"].Invoke(changeWndText, (TextBox)wnd.Controls["textBox4"], Vars.Result);
                            currentRecordDetail.TestValue = float.Parse(Vars.Result.Substring(0, Vars.Result.Length - 4));
                            currentRecordDetail.judgeResult();
                            if (currentRecordDetail.Result == 1)
                            {
                                //wnd.Controls["textBox5"].Invoke(changeWndText, (TextBox)wnd.Controls["textBox5"], "合格");
                                s7Help.WriteTestProcessResult(1);
                            }
                            else
                            {
                                //wnd.Controls["textBox5"].Invoke(changeWndText, (TextBox)wnd.Controls["textBox5"], "不合格");
                                s7Help.WriteTestProcessResult(2);
                            }
                            break;
                        }

                        Thread.Sleep(200);
                    }

                    //电机停止位置位
                    if (!Vars.ControlMode) s7Help.WriteMotorStop(true);

                    Vars.Result = "";
                    currentRecordDetail.ManualWriteValue = 0;

                    hybirdLock.Enter();
                    //wnd.Controls["RecordDetailDgv"].Invoke(changeWndDgv, (DataGridView)wnd.Controls["RecordDetailDgv"], currentRecordDetail, false);
                    wnd.RecordDetailDgv.Invoke(changeWndDgv, wnd.RecordDetailDgv, currentRecordDetail, false);
                    currentRecord.RecordDetailList.Add(currentRecordDetail);
                    wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "采集完成++++++");
                    
                    hybirdLock.Leave();

                    Thread.Sleep(200);

                    //等待电机停止 
                    while (true && !Vars.ControlMode)
                    {
                        if (s7Help.ReadMotorStopFinish())
                        {
                            break;
                        }
                        Thread.Sleep(100);
                    }
                    wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "已停止");
                    
                    //电机回零速度设置
                    if (!Vars.ControlMode) s7Help.WriteMotorToZeroSpeed(toZeroSpeed);
                    Thread.Sleep(100);
                    //电机回零
                    if (!Vars.ControlMode) s7Help.WriteMotorToZero(true);
                    //等待回零完成
                    while (true && !Vars.ControlMode)
                    {
                        if (s7Help.ReadMotorToZeroFinish())
                        {
                            break;
                        }
                        Thread.Sleep(100);
                    }
                    wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "已回零");
                }
                //试验完成后置位试验结束
                s7Help.WriteTestEnd(true);
            }
            //保存数据
            saveRecord();
            //本地试验完成
            MessageBox.Show("本次试验完成！");
        }
        public void DataGridViewClear()
        {
            if(wnd.InvokeRequired)
            {
                wnd.Invoke(new Action(DataGridViewClear));
                return;
            }
            wnd.RecordDetailDgv.Rows.Clear();
        }
        private HslCommunication.Core.SimpleHybirdLock hybirdLock;
        public ClickTorque(SerialPortHelper serialPort, S7Help s7Help, Main wnd)
        {
            this.serialPort = serialPort;
            this.wnd = wnd;
            name = "咔嗒扳手";
            this.s7Help = s7Help;
            hybirdLock = new HslCommunication.Core.SimpleHybirdLock();
        }
    }
}
