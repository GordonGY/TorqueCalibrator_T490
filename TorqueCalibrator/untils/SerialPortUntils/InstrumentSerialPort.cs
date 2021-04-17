using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TorqueCalibrator.variables;

namespace TorqueCalibrator.untils.SerialPortUntils
{

    public class InstrumentSerialPort : SerialPortHelper
    {
        public InstrumentSerialPort(Main wnd)
            : base(wnd)
        {
            this.wnd = wnd;
            port.PortName = ConfigurationManager.AppSettings["InstrumentSerialPortName"];
            port.BaudRate = Int32.Parse(ConfigurationManager.AppSettings["InstrumentSerialBaudRate"].ToString());
            port.StopBits = System.IO.Ports.StopBits.Two;
        }

        public override void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Action<TextBox, string> changeWndText = new Action<TextBox, string>(((Main)wnd).refreshTextbox);
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
                    strReceive = strReceiveTemp.Substring(0, m);
                    strReceiveTemp = strReceiveTemp.Substring(m, strReceiveTemp.Length - m);
                    strReceiveTemp = strReceiveTemp.Replace("\r", "");
                    strReceiveTemp = strReceiveTemp.Replace("\n", "");
                    
                    Vars.Result = strReceive;

                    //wnd.textBox3.Invoke(changeWndText, wnd.textBox3, strReceive);
                }
            }
        }
    }
}
