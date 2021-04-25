using HslCommunication;
using HslCommunication.Profinet.Siemens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TorqueCalibrator.variables;

namespace TorqueCalibrator.untils
{
    public class S7Help
    {
        private SiemensS7Net siemensTcpNet;// 西门子的网络访问器 
        private Main wnd;
        private RichTextBox rtbx;

        /// <summary>
        /// 构造函数
        /// </summary>
        public S7Help(Main wnd)
        {
            this.wnd = wnd;
            rtbx = wnd.hintRtbx;//;(RichTextBox)wnd.Controls["hintRtbx"];
        }
        /// <summary>
        /// 改变richtextbox内容
        /// </summary>
        /// <param name="rtbx"></param>
        /// <param name="str"></param>
        public void changeWndRichTextBoxText(RichTextBox rtbx, string str)
        {
            rtbx.SelectionColor = Color.Black;
            rtbx.AppendText(str + System.Environment.NewLine);
            rtbx.ScrollToCaret();
        }
        public void export(string str)
        {
            if(rtbx.InvokeRequired)
            {
                rtbx.Invoke(new Action<string>(export),str);
                return;
            }
            changeWndRichTextBoxText(rtbx, str);
        }


        //连接PLC  SiemensPLCS.S1200
        public void SiemensS7NetConnect(SiemensPLCS siemens, out string strMsg)
        {
            strMsg = "";
            siemensTcpNet = new SiemensS7Net(siemens);                                            // 实例化西门子的对象
            siemensTcpNet.IpAddress = ConfigurationManager.AppSettings["S7IpAdress"].ToString();    // 设置IP地址
            siemensTcpNet.ConnectTimeOut = 1000;                                                    // 超时时间为1秒
            try
            {
                OperateResult connect = siemensTcpNet.ConnectServer();                             //切换为长连接
                if (connect.IsSuccess)
                {
                    //成功添加处理，日志？
                    Log.loginfo.Info("PLC连接成功！");
                    strMsg = "PLC连接成功！";
                }
                else
                {
                    strMsg = HslCommunication.StringResources.Language.ConnectedFailed + connect.Message;
                    Log.logerror.Error("PLC连接失败！", new Exception("失败"));
                }
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
            }
        }

        //试验准备
        public void WriteTestReadyStart(bool b)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["TestReadyStartAddress"].ToString(), b);
            if (Vars.TestModeLog == "1")
                export("试验准备置位！");
        }

        //试验结束
        public bool ReadTestReadyFinish()
        {
            return siemensTcpNet.ReadBool(ConfigurationManager.AppSettings["TestReadyFinishAddress"].ToString()).Content;
        }

        //试验开始
        public void WriteTestStart(bool b)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["TestStartAddress"].ToString(), b);
            if (Vars.TestModeLog == "1")
                export("试验开始置位！");
        }

        ////试验结束
        //public bool ReadTestEnd()
        //{
        //    return siemensTcpNet.ReadBool(ConfigurationManager.AppSettings["TestEndAddress"].ToString()).Content;
        //}

        //试验模式
        public void WriteTestMode(int i)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["TestModeAddress"].ToString(), i);
            if (Vars.TestModeLog == "1")
                export("已设置测试模式：" + i);
        }

        //试验采集器选择
        public void WriteCollectorSelect(int i)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["CollectorSelectAddress"].ToString(), i);
            if (Vars.TestModeLog == "1")
                export("已选择传感器：" + i);

        }

        ////电机选择
        //public void WriteMotorSelect(int i)
        //{
        //    siemensTcpNet.Write( ConfigurationManager.AppSettings["MotorSelectAddress"].ToString(), i );
        //}

        //电机速度模式开始
        public void WriteMotorStartV(bool b)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["MotorStartVAddress"].ToString(), b);
            if (Vars.TestModeLog == "1")
                export("速度模式使能");
        }

        //电机速度模式下的速度
        public void WriteMotorStartVSpeed(float f)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["MotorStartVSpeedAddress"].ToString(), f);
            if (Vars.TestModeLog == "1")
                export("电机速度设置为："+f);
        }

        //电机相对位移模式开始
        public void WriteMotorStartP(bool b)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["MotorStartPAddress"].ToString(), b);
        }

        //电机相对位移模式速度
        public void WriteMotorStartPSpeed(float f)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["MotorStartPSpeedAddress"].ToString(), f);
        }

        //电机相对位移模式位置
        public void WriteMotorStartPPosition(float f)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["MotorStartPPositonAddress"].ToString(), f);
        }

        //电机相对位移模式下的停止
        public bool ReadMotorStartPFinish(float f)
        {
            return siemensTcpNet.ReadBool(ConfigurationManager.AppSettings["MotorStartPFinishAddress"].ToString()).Content;
        }

        //电机停止
        public void WriteMotorStop(bool b)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["MotorStopAddress"].ToString(), b);
            if (Vars.TestModeLog == "1")
                export("电机暂停置位");
        }

        //电机停止完成
        public bool ReadMotorStopFinish()
        {
            bool MotorStopFinish = siemensTcpNet.ReadBool(ConfigurationManager.AppSettings["MotorStopFinishAddress"].ToString()).Content;
            return MotorStopFinish;
        }

        //电机回零
        public void WriteMotorToZero(bool b)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["MotorToZeroAddress"].ToString(), b);
            if (Vars.TestModeLog == "1")
                export("电机回零使能置位！");
        }

        //电机回零速度
        public void WriteMotorToZeroSpeed(float f)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["MotorToZeroSpeedAddress"].ToString(), f);
            if (Vars.TestModeLog == "1")
                export("电机回零速度设置：" + f);
        }

        //电机回零完成
        public bool ReadMotorToZeroFinish()
        {
            bool MotorToZeroFinish = siemensTcpNet.ReadBool(ConfigurationManager.AppSettings["MotorToZeroFinishAddress"].ToString()).Content;
            return MotorToZeroFinish;
        }


        //小车上电初始化状态
        public bool ReadPowerOnInitStatus()
        {
            bool MotorToZeroFinish = siemensTcpNet.ReadBool(ConfigurationManager.AppSettings["PowerOnInitStatusAddress"].ToString()).Content;
            return MotorToZeroFinish;
        }

        //试验结束置位
        public void WriteTestEnd(bool b)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["TestEndAddress"].ToString(), b);
            if (Vars.TestModeLog == "1")
                export("试验结束，PLC已重置！");

        }
        //传输每步的试验结果
        public void WriteTestProcessResult(int b)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["TestProcessResultAddress"].ToString(), b);

        }

        //速度改变后需要使能
        public void WriteMotorVChange(bool b)
        {
            siemensTcpNet.Write(ConfigurationManager.AppSettings["MotorVChangeAddress"].ToString(), b);

        }
        public bool ReadPLCStatus()
        {

            return siemensTcpNet.ReadBool(ConfigurationManager.AppSettings["PowerOnInitStatus"].ToString()).Content;
        }

        //读取左边电机位置
        public float ReadPLCLeftMotorPositon()
        {
            return siemensTcpNet.ReadFloat(ConfigurationManager.AppSettings["LeftMotorPositon"].ToString()).Content;
        }
        //读取右边电机位置
        public float ReadPLCRightMotorPositon()
        {
            return siemensTcpNet.ReadFloat(ConfigurationManager.AppSettings["RightMotorPositon"].ToString()).Content;
        }
    }
}
