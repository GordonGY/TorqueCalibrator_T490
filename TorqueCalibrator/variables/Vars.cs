using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo;
using TorqueCalibrator.pojo.user;
using TorqueCalibrator.untils;

namespace TorqueCalibrator.variables
{
    public static class Vars
    {
        //public static P_Con_User CurrentUser;//记录当前登录的用户
        public static string Result = "";//扭矩校验仪返回的数据
        public static string MnualDigitalResult = "";
        //控制模式
        public static bool ControlMode;//0自动，1手动
        public static int CurrentSensor;//当前传感器
        public static int LastSensor;//当前传感器
        public static bool BorrowToolWnd;//界面是否打开
        public static string Result_IDCard;
        public static string Result_PortScan;
        public static SerialPortHelper serialPort;
        public static SerialPortHelper serialPortIDCard;
        public static SerialPortHelper serialPortScan;
    }
}
