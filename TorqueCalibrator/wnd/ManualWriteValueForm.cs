using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using TorqueCalibrator.variables;
namespace TorqueCalibrator.wnd
{
    public partial class ManualWriteValueForm : Form
    {
        
        public ManualWriteValueForm()
        {
            InitializeComponent();
        }

        private bool IsFloat(string str)
        {
            string regextext = @"^\d+\.\d+$";
            Regex regex = new Regex(regextext, RegexOptions.None);
            return regex.IsMatch(str);
        }
        //判断是否为整数
        private bool IsInteger(string str)
        {
            try
            {
                int i = Convert.ToInt32(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //判断输入扭矩值是否合规
            if (IsFloat(textBox1.Text) || IsInteger(textBox1.Text))
            {
                //MessageBox.Show("请输入正确的扭矩值！");
            }
            else
            {
                MessageBox.Show("请输入正确的扭矩值！");
                return;
            }

            Vars.MnualDigitalResult = Regex.Replace(textBox1.Text, "^0*", "");
            textBox1.Text = "";
            this.Close();
        }

        private void ManualWriteValueForm_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void ManualWriteValueForm_KeyDown(object sender, KeyEventArgs e)
        {
           
            //// 判断输入扭矩值是否合规
            //if (IsFloat(textBox1.Text) || IsInteger(textBox1.Text))
            //{
            //    //MessageBox.Show("请输入正确的扭矩值！");
            //}
            //else
            //{
            //    MessageBox.Show("请输入正确的扭矩值！");
            //    return;
            //}

            //Vars.MnualDigitalResult = Regex.Replace(textBox1.Text, "^0*", "");
            //textBox1.Text = "";
            //this.Close();
        }

        private void ManualWriteValueForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                if (IsFloat(textBox1.Text) || IsInteger(textBox1.Text))
                {
                    //MessageBox.Show("请输入正确的扭矩值！");
                }
                else
                {
                    MessageBox.Show("请输入正确的扭矩值！");
                    return;
                }

                Vars.MnualDigitalResult = Regex.Replace(textBox1.Text, "^0*", "");
                textBox1.Text = "";
                this.Close();
            }
        }
    }
    

}
