using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.variables;
using TorqueCalibrator.wnd;
using System.Windows.Forms;

namespace TorqueCalibrator.untils.SerialPortUntils
{
    public class IDCardSerialPort : SerialPortHelper
    {
        private Login LoginFrom;

        public IDCardSerialPort(Login wnd)
            : base(wnd)
        {
            LoginFrom = wnd;
            //private Login LoginFrom;
            this.wnd = wnd;
            port.PortName = ConfigurationManager.AppSettings["IDCardSerialPortName"];
            port.BaudRate = Int32.Parse(ConfigurationManager.AppSettings["IDCardSerialBaudRate"].ToString());
            port.StopBits = System.IO.Ports.StopBits.Two;
        }

       public override void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
       {
            //存储接收的字符串
            if (port != null)
            {
                //读取接收到的字节长度
                int n = port.BytesToRead;
                //定义字节存储器数组
                byte[] byteReceive = new byte[n];
                //接收的字节存入字节存储器数组
                port.Read(byteReceive, 0, n);
                //把接收的的字节数组转成字符串
                strReceiveTemp = strReceiveTemp + Encoding.UTF8.GetString(byteReceive);
                int m = strReceiveTemp.IndexOf("\r");
                if (strReceiveTemp.IndexOf("\r") > 0)
                {
                    strReceive = strReceiveTemp.Substring(1, m);
                    strReceiveTemp = strReceiveTemp.Substring(m, strReceiveTemp.Length - m);
                    strReceiveTemp = strReceiveTemp.Replace("\r", "");
                    strReceiveTemp = strReceiveTemp.Replace("\n", "");
                    Vars.Result_IDCard = strReceive.Trim();
                    //改变自动打卡登录框
                    ChangeIdCardTBText();
                }
            }
       }

       private void ChangeIdCardTBText()
       {
            if(LoginFrom.InvokeRequired)
            {
                // 如果是后台调用显示UI，那么就使用委托来切换到前台显示
                LoginFrom.Invoke(new Action(ChangeIdCardTBText));
                return;
            }
            this.LoginFrom.cardNumTbx.Text = Vars.Result_IDCard;
       }
    }
}
