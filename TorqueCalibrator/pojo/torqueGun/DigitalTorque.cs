using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TorqueCalibrator.untils;
using TorqueCalibrator.variables;
using TorqueCalibrator.wnd;

namespace TorqueCalibrator.pojo.torqueGun
{
    /// <summary>
    /// 数显扳手
    /// </summary>
    public class DigitalTorque : BaseTorque
    {
        public Dictionary<string, string> PsetMap { get; set; }//存储pset与值的对应关系,如“1”->50Nm
        public string IP { get; set; }//枪的IP，有的时候用得到
        private float SpeedFast = float.Parse(ConfigurationManager.AppSettings["shuxianSpeedFast"].ToString()), SpeedSlow = float.Parse(ConfigurationManager.AppSettings["shuxianSpeedSlow"].ToString());//数显扳手
        //新增
        public Form manualWriteValueForm = null;
        public float ManualWriteValue=0.0F;
        public override void doTest()
        {
            Action<TextBox, string> changeWndText = new Action<TextBox, string>(changeWndTextBoxText);
            Action<RichTextBox, string> changeWndRich = new Action<RichTextBox, string>(changeWndRichTextBoxText);
            Action<BarStaticItem, string> changeWndHintItem = new Action<BarStaticItem, string>(changeWndHintItemText);
            //新增
            Action<DataGridView, RecordDetail, float, bool> changeWndDgv1 = new Action<DataGridView, RecordDetail, float, bool>(changeWndDgvText1);
            Action<DataGridView, RecordDetail,  bool> changeWndDgv = new Action<DataGridView, RecordDetail,  bool>(changeWndDgvText);

            //清空DataGridView
            DataGridViewClear();

            //准备
            this.initRecord(this.CheckMode);
            Vars.Result = "";

            //按照工艺中规定的进行校验
            for (int i = 0; i < this.CurrentTech.TechnologyDetailList.Count; i++)
            {
                #if TRUE
                
                //发送设置校验仪传感器选择位
                this.choseSensor(this.CurrentTech.TechnologyDetailList[i].Standard);

                //将该发送的模式，设置下去，基类中实现11
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
                #endif

                //开始循环
                for (int j = 0; j < this.CurrentTech.TechnologyDetailList[i].Num; j++)
                {

                    this.initRecordDetail(this.CurrentTech.TechnologyDetailList[i]);

                    //试验速度模式速度下发
                    if (!Vars.ControlMode) s7Help.WriteMotorStartVSpeed(this.SpeedFast);

                    //100ms小延时
                    Thread.Sleep(100);

                    #region 数显扳手自动模式

                    if (!Vars.ControlMode)//自动模式
                    {
                        //采集数据使能，使能后将开始不断采数
                        Thread th = new Thread(sendData);
                        th.Start();
                    
                    
                        //试验速度模式使能
                        s7Help.WriteMotorStartV(true);
                        wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "第" + (j + 1).ToString() + "次试验，电机以速度 "+this.SpeedFast+" 开始运动！");
                        wnd.HintIteam.Caption = "第" + (j + 1).ToString() + "次试验，电机以速度 " + this.SpeedFast + " 开始运动！";

                        Vars.Result = "";

                        //数据采集，到达后停止
                        //第一段
                        while (true && !Vars.ControlMode)
                        {
                            if (Vars.Result.Contains(@"N.m"))
                            {
                                float value = float.Parse(Vars.Result.Substring(0, Vars.Result.Length - 4));
                                if (value > currentRecordDetail.Standard - changeSpeed)
                                {
                                    break;
                                }
                                Vars.Result = "";
                            }
                        }
                        //wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "到达第一设定值-减速");
                        
                        //第二段
                        //试验速度模式速度下发
                        s7Help.WriteMotorStartVSpeed(this.SpeedSlow);
                        Thread.Sleep(10);
                        s7Help.WriteMotorVChange(true);

                        wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "第" + (j + 1).ToString() + "次试验，电机以速度 " + this.SpeedSlow + " 开始运动！");
                        wnd.HintIteam.Caption = "第" + (j + 1).ToString() + "次试验，电机以速度 " + this.SpeedSlow + " 开始运动！";

                        while (true)
                        {
                            if (Vars.Result.Contains(@"N.m"))
                            {
                                float value = float.Parse(Vars.Result.Substring(0, Vars.Result.Length - 4));
                                if (value > currentRecordDetail.Standard - 0.4)
                                {
                                    break;
                                }
                                Vars.Result = "";
                            }
                        }
                        //wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "到达第二设定值-减速");
                        
                        //停止发数
                        th.Abort();

                        //电机停止位置位
                        s7Help.WriteMotorStop(true);

                        //等待电机停止 
                        while (true)
                        {
                            if (s7Help.ReadMotorStopFinish())
                            {
                                wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "第" + (j + 1).ToString() + "次试验，电机暂停运动！");
                                wnd.HintIteam.Caption = "第" + (j + 1).ToString() + "次试验，电机暂停运动！";
                                break;
                            }
                            Thread.Sleep(100);
                        }

                        //静止后再次采集数据
                        Vars.Result = "";
                        List<float> ls = new List<float>();
                        while (ls.Count < 1)
                        {
                            while (Vars.Result == "")
                            {
                                serialPort.send("2");
                                Thread.Sleep(500);
                            }
                            ls.Add(float.Parse(Vars.Result.Substring(0, Vars.Result.Length - 4)));
                            Vars.Result = "";
                        }
                        currentRecordDetail.TestValue = ls.Average();

                        wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "第" + (j + 1).ToString() + "次试验，数据采集完成！");
                        wnd.HintIteam.Caption = "第" + (j + 1).ToString() + "次试验，数据采集完成！";

                        #region 回零

                        //电机回零速度设置
                        s7Help.WriteMotorToZeroSpeed(toZeroSpeed);
                        Thread.Sleep(100);

                        //电机回零
                        s7Help.WriteMotorToZero(true);
                        wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "第" + (j + 1).ToString() + "次试验，电机正在回零！");
                        wnd.HintIteam.Caption = "第" + (j + 1).ToString() + "次试验，电机正在回零！";

                        //等待回零完成
                        while (true)
                        {
                            if (s7Help.ReadMotorToZeroFinish())
                            {
                                break;
                            }
                            Thread.Sleep(100);
                        }
                        wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "第" + (j + 1).ToString() + "次试验，电机回零完成！");
                        wnd.HintIteam.Caption = "第" + (j + 1).ToString() + "次试验，电机回零完成！";
                        #endregion
                    }
                    #endregion

                    #region 数显扳手手动模式
                    else //手动模式
                    {
                        //
                        wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "正手动摇动到达：" + this.CurrentTech.TechnologyDetailList[i].Standard.ToString() + "NM！");
                        wnd.HintIteam.Caption = "请手动摇动到达："+ this.CurrentTech.TechnologyDetailList[i].Standard.ToString() + "NM！";

                        while (true)
                        {
                            if (Vars.Result.Contains(@"N.m"))
                            {
                                float value = float.Parse(Vars.Result.Substring(0, Vars.Result.Length - 4));
                                break;
                            }
                        }
                        currentRecordDetail.TestValue = float.Parse(Vars.Result.Substring(0, Vars.Result.Length - 4));
                        Vars.Result = "";
                    }
                    #endregion

                    wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "正输入数显扳手显示的值！");
                    wnd.HintIteam.Caption = "请输入数显扳手显示的值！";

                    //新增, 语句位置有变更
                    manualWriteValueForm.ShowDialog();
                    this.ManualWriteValue = float.Parse(Vars.MnualDigitalResult);
                    currentRecordDetail.ManualWriteValue = this.ManualWriteValue;
                    
                    //判断实验结果
                    currentRecordDetail.judgeResult(this.ManualWriteValue);
                   
                    //新增
                    hybirdLock.Enter();
                    RecordDetailDgv(currentRecordDetail, this.ManualWriteValue, false);
                    hybirdLock.Leave();

                    //PLC亮灯 
                    if (currentRecordDetail.Result == 1)
                    {
                        s7Help.WriteTestProcessResult(1);
                    }
                    else
                    {
                        s7Help.WriteTestProcessResult(2);
                    }
                    currentRecord.RecordDetailList.Add(currentRecordDetail);

                    wnd.hintRtbx.Invoke(changeWndRich, wnd.hintRtbx, "第" + (j + 1).ToString() + "次试验数据已采集！");
                    wnd.HintIteam.Caption = "第" + (j + 1).ToString() + "次试验数据已采集！";

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

            //本地试验完成
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

        public void RecordDetailDgv(RecordDetail rd, float manualWriteValue, bool del)
        {
            if(wnd.InvokeRequired)
            {
                wnd.Invoke(new Action<RecordDetail, float, bool>(RecordDetailDgv),rd, manualWriteValue, del);
                return;
            }
            changeWndDgvText2(wnd.RecordDetailDgv, rd, manualWriteValue, del);
        }
        /// <summary>
        /// 发送采集数据的指令 
        /// </summary>
        private void sendData()
        {
            while (true)
            {
                serialPort.send("2");
                Thread.Sleep(500);
            }
        }
        private HslCommunication.Core.SimpleHybirdLock hybirdLock;
        public DigitalTorque(SerialPortHelper serialPort, S7Help s7Help, Main wnd)
        {
            this.name = "数显扳手";
            this.serialPort = serialPort;
            this.wnd = wnd;
            this.s7Help = s7Help;
            hybirdLock = new HslCommunication.Core.SimpleHybirdLock();
            manualWriteValueForm = new ManualWriteValueForm();
            manualWriteValueForm.Hide();
        }
    }
}
