using DevExpress.XtraBars;
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
            Action<BarStaticItem, string> changeWndHintItem = new Action<BarStaticItem, string>(changeWndHintItemText);
            Action<DataGridView, RecordDetail, bool> changeWndDgv = new Action<DataGridView, RecordDetail, bool>(changeWndDgvText);

            //清空RecordDetailDgv界面
            wnd.RecordDetailDgv.Invoke(changeWndDgv, wnd.RecordDetailDgv, null, true);
            
            //初始化记录
            this.initRecord(this.CheckMode);
            Vars.Result = "";

            //按照工艺中规定的进行校验
            for (int i = 0; i < this.CurrentTech.TechnologyDetailList.Count; i++)
            {

                //发送设置校验仪传感器选择位
                this.choseSensor(this.CurrentTech.TechnologyDetailList[i].Standard);

                //将该发送的模式，设置下去，基类中实现
                this.setMode(serialPort);

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
                    if (!Vars.ControlMode) s7Help.WriteMotorStartVSpeed(this.speed);

                    //100ms小延时
                    Thread.Sleep(100);

                    //试验速度模式使能
                    if (!Vars.ControlMode)
                    {
                        s7Help.WriteMotorStartV(true);
                        wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "第" + (j + 1).ToString() + "次试验，电机开始运动！");
                        wnd.HintIteam.Caption = "第" + (j + 1).ToString() + "次试验，电机开始运动！";
                    }

                    //手动模式提示
                    if (Vars.ControlMode)
                    {
                        wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "请进行"+ "第" + (j + 1).ToString() + "次试验！");
                        wnd.HintIteam.Caption = "请进行" + "第" + (j + 1).ToString() + "次试验！";
                    }

                    //数据采集，完成后停止
                    while (true)
                    {
                        if (Vars.Result.Contains(@"N.m"))
                        {
                            currentRecordDetail.TestValue = float.Parse(Vars.Result.Substring(0, Vars.Result.Length - 4));
                            currentRecordDetail.judgeResult();
                            s7Help.WriteTestProcessResult(currentRecordDetail.Result == 1 ? 1 : 2);
                            break;
                        }

                        Thread.Sleep(200);
                    }

                    //电机停止位置位
                    if (!Vars.ControlMode) s7Help.WriteMotorStop(true);

                    Vars.Result = "";
                    currentRecordDetail.ManualWriteValue = 0;
                    
                    hybirdLock.Enter();
                    wnd.RecordDetailDgv.Invoke(changeWndDgv, wnd.RecordDetailDgv, currentRecordDetail, false);
                    currentRecord.RecordDetailList.Add(currentRecordDetail);
                    //手动模式提示
                    if (Vars.ControlMode) wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "第"+ (j+1).ToString()+"次试验，数据采集完成！");
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
                    if (!Vars.ControlMode)
                    {
                        wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "第" + (j + 1).ToString() + "次试验，电机停止运动！");
                        wnd.HintIteam.Caption = "第" + (j + 1).ToString() + "次试验，电机停止运动！";
                    }
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
                    if (!Vars.ControlMode)
                    {
                        wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "第" + (j + 1).ToString() + "次试验，电机已回零！");
                        wnd.HintIteam.Caption = "第" + (j + 1).ToString() + "次试验，电机已回零！";
                    } 
                    wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "----------");
                }
                //试验完成后置位试验结束
                s7Help.WriteTestEnd(true);
            }
            //保存数据
            saveRecord();

            //打开开始试验按钮使能
            OpenStartButtonEnable();

            wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "本次试验已完成！");
            wnd.HintIteam.Caption = "本次试验已完成！";
            wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "----------------------------------------------");
            
            //本次试验完成
            MessageBox.Show("本次试验完成！", "试验提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            wnd.HintIteam.Caption = "等待试验！";

        }
        //打开开始试验按钮使能
        private void OpenStartButtonEnable()
        {
            if(wnd.InvokeRequired)
            {
                wnd.Invoke(new Action(OpenStartButtonEnable));
                return;
            }
            wnd.barButtonItem1.Enabled = true;
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
