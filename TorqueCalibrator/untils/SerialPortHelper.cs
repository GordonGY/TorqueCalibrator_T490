using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using TorqueCalibrator.variables;

namespace TorqueCalibrator.untils
{
    /// <summary>
    /// 串口通讯类
    /// </summary>
    public abstract class SerialPortHelper
    {
        public SerialPort port = new SerialPort();//串口对象
        public string strReceive = string.Empty;
        public string strReceiveTemp = string.Empty;

        public Form wnd;//窗体传入

        public SerialPortHelper(Form wnd)
        {
        }
        public bool open()
        {
            try
            {
                this.port.Open();
                Receieve();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //接收数据
        private void Receieve()
        {
            //接收到数据就会触发port_DataReceived方法
            port.DataReceived += port_DataReceived;
        }
        public abstract void port_DataReceived(object sender, SerialDataReceivedEventArgs e);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="strRead"></param>
        /// <returns></returns>
        public bool send(string strRead)
        {
            strRead = strRead + "\r\n";
            port.WriteLine(strRead);
            return true;
        }


        /// <summary>
        /// 关闭COM口
        /// </summary>
        public void Close()
        {
            if (port != null && port.IsOpen)
            {
                port.Close();
                port.Dispose();
            }

        }
    }
}
