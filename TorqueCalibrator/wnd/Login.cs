using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TorqueCalibrator.pojo;
using TorqueCalibrator.pojo.torqueGun;
using TorqueCalibrator.pojo.user;
using TorqueCalibrator.service.record.recordImpl;
using TorqueCalibrator.service.user;
using TorqueCalibrator.service.user.userImpl;
using TorqueCalibrator.untils;
using TorqueCalibrator.untils.SerialPortUntils;
using TorqueCalibrator.variables;

namespace TorqueCalibrator.wnd
{
    public partial class Login : Form
    {
        string strMsg = "";
        private UserService userService = new UserServiceImpl();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //string strMsg;
            //s7help.SiemensS7NetConnect(HslCommunication.Profinet.Siemens.SiemensPLCS.S1200, out strMsg);
            Vars.serialPortIDCard = new IDCardSerialPort(this);
            if (!Vars.serialPortIDCard.open())
            {
                 label1.Text = "串口打开失败！";
            }
        }

        private void cardRdbt_CheckedChanged(object sender, EventArgs e)
        {
            usernameLab.Visible = false;
            passwordLab.Visible = false;
            usernameTbx.Visible = false;
            passwordTbx.Visible = false;
            cardLab.Visible = true;
            cardNumTbx.Visible = true;
            cardNumTbx.Focus();
        }

        private void userRdbt_CheckedChanged(object sender, EventArgs e)
        {
            usernameLab.Visible = true;
            passwordLab.Visible = true;
            usernameTbx.Visible = true;
            passwordTbx.Visible = true;
            cardLab.Visible = false;
            cardNumTbx.Visible = false;
            usernameTbx.Focus();
        }

        private void cardNumTbx_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length < 5)
            {
                return;
            }
            //此处验证登录信息
            if (!P_Con_User.LoginVerifyJustNumber(((TextBox)sender).Text, out strMsg))
            {
                MessageBox.Show("用户不存在");
                return;
            }
            //清空文本框内容
            ((TextBox)sender).Text = "";
            showMain(P_Con_User.CurrentUser);
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (userRdbt.Checked)
            {
                bool ret = P_Con_User.LoginVerify(usernameTbx.Text, passwordTbx.Text, out strMsg);
                if (ret)
                {
                    showMain(P_Con_User.CurrentUser);
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！");
                }

            }
        }

        private void showMain(P_Con_User user)
        {
            if (user != null)
            {
                new Main(this).Show();
                this.Hide();
            }
        }

    }
}
