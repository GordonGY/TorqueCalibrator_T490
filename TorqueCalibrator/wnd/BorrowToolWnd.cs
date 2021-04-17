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
using TorqueCalibrator.pojo.user;
using TorqueCalibrator.service.borrow;
using TorqueCalibrator.service.borrow.borrowImpl;
using TorqueCalibrator.service.product;
using TorqueCalibrator.service.product.productImpl;
using TorqueCalibrator.untils;
using TorqueCalibrator.untils.SerialPortUntils;
using TorqueCalibrator.variables;

namespace TorqueCalibrator.wnd
{
    public partial class BorrowToolWnd : Form
    {
        private List<BorrowRecord> recordList = new List<BorrowRecord>();
        private BorrowRecordService recordService = new BorrowRecordServiceImpl();
        ProductService productService = new ProductServiceImpl();
        //SerialPortHelper serialScanPort=Vars.serialPortScan;//扫码枪
        
        public BorrowToolWnd()
        {
            InitializeComponent();
            //serialScanPort = new ScanSerialPort(null, this);//扫码枪串口
        }

        private void BorrowToolWnd_Load(object sender, EventArgs e)
        {
            //if (serialScanPort.open())
            //{
            //    //MessageBox("not open");
            //}
            //else
            //{
                
            //}
            startDtp.Value = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0, 0)).Date;
            endDtp.Value = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            userCbx.DataSource = recordService.selectUserNameList(new DateTime(), new DateTime());
            userCbx.SelectedIndex = -1;
            this.Width = 420;
            this.Height = 80;
        }
        //借用按钮，为未检测库中是否有该设备
        private void borrow_Click(object sender, EventArgs e)
        {
            if (seriesNumTbx.Text == "")
            {
                MessageBox.Show("请扫描或输入工具编号");
                return;
            }
            List<BorrowRecord> list = recordService.selectListByAll("", seriesNumTbx.Text, "", new DateTime(), new DateTime(), 0);
            if (list != null)
            {
                if (list.Count > 0)
                {
                    if (list[0].Status == 0)
                    {
                        if (MessageBox.Show("工具已经借出，如需借出将触发自动还工具动作，仍然借出请确认", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //封装上次的借出动作
                            recordService.updateStatus(list[0].Id, list[0].Status);

                        }
                        else { return; }//不借返回
                    }
                }
            }

            //判断该工具库中是否存在
            Product product = productService.selectOneBySeriesNum(seriesNumTbx.Text);
            if(product==null)
            {
                MessageBox.Show("库中无该工具！", "警告", MessageBoxButtons.OK);
                seriesNumTbx.Text = "";
                return;
            }

            //借出动作,添加一条记录
            recordService.addOne(new BorrowRecord(Guid.NewGuid().ToString("N"),
                seriesNumTbx.Text,
                P_Con_User.CurrentUser.User_name,
                0));
            borrowLab.Text = "工具" + seriesNumTbx.Text + "借出成功";
        }
        private void return_Click(object sender, EventArgs e)
        {
            if (seriesNumTbx.Text == "")
            {
                MessageBox.Show("请扫描或输入工具编号");
                return;
            }
            List<BorrowRecord> list = recordService.selectListByAll("", seriesNumTbx.Text, "", new DateTime(), new DateTime(), 0);
            if (list != null)
            {
                if (list.Count > 0)
                {
                    //归还
                    recordService.updateStatus(list[0].Id, 1);
                }
            }
            else//没借出过
            {
                MessageBox.Show("此工具未被借出，无法归还");
                return;
            }
            borrowLab.Text = "工具" + seriesNumTbx.Text + "归还成功";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (extendBtn.Text == "展开")
            {
                this.Width = 800;
                this.Height = 600;
                extendBtn.Text = "收起";
            }
            else
            {
                this.Width = 420;
                this.Height = 80;
                extendBtn.Text = "展开";
            }
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            if (startDtp.Value > endDtp.Value)
            {
                MessageBox.Show("日期选择错误->开始日期大于结束日期");
                return;
            }
            RecordDgv.Rows.Clear();
            recordList = recordService.selectListByAll("", seriesNumTbx.Text, userCbx.Text, startDtp.Value, endDtp.Value, statusChbx.Checked == true ? 0 : -1);
            if (recordList != null)
            {
                RecordDgv.Rows.Add(recordList.Count);
                for (int i = 0; i < recordList.Count; i++)
                {
                    RecordDgv.Rows[i].Cells["id"].Value = recordList[i].Id;
                    RecordDgv.Rows[i].Cells["seriesNum"].Value = recordList[i].SeriesNum;
                    RecordDgv.Rows[i].Cells["operator1"].Value = recordList[i].Operator;
                    RecordDgv.Rows[i].Cells["status"].Value = recordList[i].Status == 0 ? "借出" : "在库";
                    RecordDgv.Rows[i].Cells["createTime"].Value = recordList[i].CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    RecordDgv.Rows[i].Cells["updateTime"].Value = recordList[i].UpdateTime == recordList[i].CreateTime ? "" : recordList[i].UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }

        private void RecordDgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BorrowToolWnd_FormClosing(object sender, FormClosingEventArgs e)
        {
            Vars.BorrowToolWnd =  false;
        }
    }
}
