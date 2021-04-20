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
    /// 电动扳手
    /// </summary>
    public class ElectricTorque : BaseTorque
    {
        public Dictionary<string, string> PsetMap { get; set; }//存储pset与值的对应关系,如“1”->50Nm
        public string IP { get; set; }//枪的IP，有的时候用得到

        public override void doTest()
        {
            Action<TextBox, string> changeWndText = new Action<TextBox, string>(changeWndTextBoxText);
            Action<RichTextBox, string> changeWndRich = new Action<RichTextBox, string>(changeWndRichTextBoxText);
            Action<DataGridView, RecordDetail, bool> changeWndDgv = new Action<DataGridView, RecordDetail, bool>(changeWndDgvText);
            //清空RecordDetailDgv界面
            wnd.RecordDetailDgv.Invoke(changeWndDgv, wnd.RecordDetailDgv, null, true);
            //创建新的record
            initRecord(this.CheckMode);
            Vars.Result = "";
            //按照工艺中规定的进行校验（换传感器情况未考虑）
            for (int i = 0; i < this.CurrentTech.TechnologyDetailList.Count; i++)
            {
                //将该发送的模式，设置下去，基类中实现
                this.setMode(serialPort);
                
                //发送设置校验仪传感器选择位
                this.choseSensor(this.CurrentTech.TechnologyDetailList[i].Standard);

                //试验置位
                s7Help.WriteTestReadyStart(true);

                //等试验准备完成，错误处理机制未写
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
                    //生成recordDetail
                    this.initRecordDetail(this.CurrentTech.TechnologyDetailList[i]);

                    //数据采集，完成后停止
                    while (true)
                    {
                        if (Vars.Result.Contains(@"N.m"))
                        {   
                            //返回的扭矩值小于等于0，不接收其结果
                            if (float.Parse(Vars.Result.Substring(0, Vars.Result.Length - 4)) <= 0)
                            {
                                Vars.Result = "";
                                continue;
                            }

                            if (Vars.Result.Length - 4 <0)
                            {
                                continue;
                            }
                            //不带N.m的扭矩值
                            currentRecordDetail.TestValue = float.Parse(Vars.Result.Substring(0, Vars.Result.Length - 4));
                            
                            //if((int)MessageBox.Show("请确认电动扭矩枪端是否合格！！！","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation)!=1){continue;}


                            //返回当前试验状态，合格，不合格
                            currentRecordDetail.judgeResult();
                            
                            //前端显示试验结论，并且给到PLC试验过程中的结果，用于亮灯
                            if (currentRecordDetail.Result == 1)
                            {
                                //wnd.Controls["textBox5"].Invoke(changeWndText, (TextBox)wnd.Controls["textBox5"], "合格");
                                s7Help.WriteTestProcessResult(1);
                            }
                            else
                            {
                                s7Help.WriteTestProcessResult(2);
                            }
                            
                            break;
                        }
                    }
                    Vars.Result = "";
                    currentRecordDetail.ManualWriteValue = 0;

                    //锁机制
                    hybirdLock.Enter();
                    wnd.RecordDetailDgv.Invoke(changeWndDgv, wnd.RecordDetailDgv, currentRecordDetail, false);
                    hybirdLock.Leave();

                    Thread.Sleep(100);

                    currentRecord.RecordDetailList.Add(currentRecordDetail);

                    wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "采集完成++++++");
                }
                //试验完成后置位试验结束
                s7Help.WriteTestEnd(true);
            }
            //保存数据
            saveRecord();
            //打开开始试验按钮使能
            OpenStartButtonEnable();
            //本地试验完成
            MessageBox.Show("本次试验完成！");
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
        private HslCommunication.Core.SimpleHybirdLock hybirdLock;
        public ElectricTorque(SerialPortHelper serialPort, S7Help s7Help, Main wnd)
        {
            this.serialPort = serialPort;
            name = "电动扳手";
            this.wnd = wnd;
            this.s7Help = s7Help;
            hybirdLock = new HslCommunication.Core.SimpleHybirdLock();
        }
    }
}
