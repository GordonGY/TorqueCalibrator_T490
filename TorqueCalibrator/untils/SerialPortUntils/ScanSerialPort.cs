using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TorqueCalibrator.variables;
using TorqueCalibrator.wnd;

namespace TorqueCalibrator.untils.SerialPortUntils
{
    public class ScanSerialPort : SerialPortHelper
    {
        private Main MainForm;
        private BorrowToolWnd borrowToolWnd;
        public ScanSerialPort(Main wnd, BorrowToolWnd borrowToolWnd)
            : base(wnd)
        {
            //界面传输
            this.MainForm= wnd;
            this.borrowToolWnd = borrowToolWnd;

            this.wnd = wnd;
            port.PortName = ConfigurationManager.AppSettings["ScanSerialPortName"];
            port.BaudRate = Int32.Parse(ConfigurationManager.AppSettings["ScanSerialBaudRate"].ToString());
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
                    strReceive = strReceiveTemp.Substring(0, m);
                    strReceiveTemp = strReceiveTemp.Substring(m, strReceiveTemp.Length - m);
                    strReceiveTemp = strReceiveTemp.Replace("\r", "");
                    //strReceiveTemp = strReceiveTemp.Replace("\n", "");
                    Vars.Result_PortScan = strReceive;
                    //改变自动打卡登录框
                    ChangeScanPortText();
                }
            }
            
        }
        private void ChangeScanPortText()
        {
            if(Vars.BorrowToolWnd == false)
            {
                if(MainForm.InvokeRequired)
                {
                    // 如果是后台调用显示UI，那么就使用委托来切换到前台显示
                    MainForm.Invoke(new Action(ChangeScanPortText));
                    return;
                }
                this.MainForm.seriesNumTbx.Text = Vars.Result_PortScan;
            }
            else
            {
                if(borrowToolWnd.InvokeRequired)
                {
                    // 如果是后台调用显示UI，那么就使用委托来切换到前台显示
                    borrowToolWnd.Invoke(new Action(ChangeScanPortText));
                    return;
                }
                this.borrowToolWnd.seriesNumTbx.Text = Vars.Result_PortScan;
            }
            
        }
    }
}
