using BarcodeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TorqueCalibrator.dao;
using TorqueCalibrator.dao.product;
using TorqueCalibrator.dao.product.productImpl;
using TorqueCalibrator.dao.tech;
using TorqueCalibrator.dao.tech.techImpl;
using TorqueCalibrator.dao.user.userImpl;
using TorqueCalibrator.pojo;
using TorqueCalibrator.pojo.torqueGun;
using TorqueCalibrator.pojo.user;
using TorqueCalibrator.service;
using TorqueCalibrator.service.product;
using TorqueCalibrator.service.product.productImpl;
using TorqueCalibrator.service.record;
using TorqueCalibrator.service.record.recordImpl;
using TorqueCalibrator.service.tech;
using TorqueCalibrator.service.tech.techImpl;
using TorqueCalibrator.untils;
using TorqueCalibrator.untils.SerialPortUntils;
using TorqueCalibrator.variables;
using TorqueCalibrator.wnd;
using ZebraPrinterTest;

namespace TorqueCalibrator
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        #region 成员变量

        private S7Help s7help;
        Thread thRead;
        SerialPortHelper serialPort=Vars.serialPort;//校验仪
        SerialPortHelper serialScanPort=Vars.serialPortScan;//扫码枪
        Login wnd;
        TechnologyService technologyService = new TechnologyServiceImpl();
        TechnologyDetailService technologyDetailService = new TechnologyDetailServiceImpl();
        ProductService productService = new ProductServiceImpl();
        //BaseTorque torque;
        BorrowToolWnd borrowForm = null;
        //试验动作线程
        Thread th_temp=null;

        //试验参数读取线程
        Thread motorReadThread  = null;
        #endregion

        #region 构造函数
        public Main(Login wnd)
        {
            InitializeComponent();
            serialPort = new InstrumentSerialPort(this);//仪器串口
            //创建借还扳手窗口，并隐藏
            borrowForm = new BorrowToolWnd();
            borrowForm.Hide();
            serialScanPort = new ScanSerialPort(this, borrowForm);//扫码枪串口
            this.wnd = wnd;
            Vars.CurrentSensor = 2;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //打开串口
            if (serialPort.open() && serialScanPort.open())
            {
                barHeaderItem1.Caption = "串口开启成功！";
            }
            else
            {
                barHeaderItem1.Caption = "串口开启失败！";
            }
            //界面初始化
            this.Text = "欢迎使用扭矩校验系统，当前登录: " + P_Con_User.CurrentUser.User_name;
            this.barStaticItem1.Caption = DateTime.Now.Date.ToString("yyyy年MM月dd日");
            techDgv.Visible = false;
            
            //PLC连接
            string strMsg;
            s7help = new S7Help(this);
            s7help.SiemensS7NetConnect(HslCommunication.Profinet.Siemens.SiemensPLCS.S1200, out strMsg);
            barHeaderItem1.Caption += "; " + strMsg;

            //开启读取读取电机参数线程
            motorReadThread  = new Thread(ReadMotorValue);
            motorReadThread.Start();
        }
        #endregion
        
        #region PLC实时参数读取

        //读取电机参数
        private void ReadMotorValue()
        {
            Thread.Sleep(1000);
            RightMotorPosition.EditValue =  s7help.ReadPLCLeftMotorPositon();
            LeftMotorPosition.EditValue =  s7help.ReadPLCRightMotorPositon();
        }

        #endregion

        #region 扭矩枪扫码或手动输入
        private void seriesNumTbx_TextChanged(object sender, EventArgs e)
        {
            if(seriesNumTbx.Text == ""||seriesNumTbx.Text == null)
            {
                return;
            }
            //更新DataGrid数据
            Product product = productService.selectOneBySeriesNum(seriesNumTbx.Text);
            if (product == null)
            {
                hintBad("不存在该型号的测试大纲，请联系管理员进行添加");
                //清空试验参数表格
                techDgv.Rows.Clear();
                return;
            }
            product.TechnologyDetailList = technologyDetailService.selectList(product.Id);

            foreach (DataGridViewRow row in techDgv.Rows)
            {
                row.Cells[0].Value = true;
            } 
            refreshDgv(product.TechnologyDetailList, product);
            techDgv.Visible = true;

            #region 注释
            //    if (seriesNumTbx.Text == "")
            //    {
            //        MessageBox.Show("请扫描或输入工具编号");
            //        return;
            //        //throw new Exception("请扫描或输入工具编号");
            //    }
            //    Product product = productService.selectOneBySeriesNum(seriesNumTbx.Text);
            //    if (product == null)
            //    {
            //        MessageBox.Show("不存在该型号的测试大纲，请联系管理员进行添加");
            //        return;
            //        //throw new Exception("不存在该型号的测试大纲，请联系管理员进行添加");
            //    }
            //    switch (product.Kind)
            //    {
            //        case 1:
            //            torque = new DigitalTorque(serialPort, s7help, this);
            //            break;
            //        case 2:
            //            torque = new ElectricTorque(serialPort, s7help, this);
            //            break;
            //        case 3:
            //            torque = new ClickTorque(serialPort, s7help, this);
            //            break;
            //        default:
            //            throw new Exception("试验方式不存在，代码为" + product.Kind);
            //    }
            //    torque.Id = product.Id;
            //    torque.SeriesNum = product.NumIndex;
            //    torque.PreTech = new Technology("", product.Kind, "", "");
            //    torque.PostTech = new Technology("", product.Kind, "", "");
            //    torque.PreTech.TechnologyDetailList = technologyDetailService.selectPreList(torque.Id);
            //    torque.PostTech.TechnologyDetailList = technologyDetailService.selectPostList(torque.Id);
            //    if (torque.PreTech.TechnologyDetailList == null || torque.PostTech.TechnologyDetailList == null)
            //    {
            //        throw new Exception("该型号未查询到试验大纲，请联系管理员进行添加");
            //    }
            //    //根据选择来决定是否是回校，回校只在此处以及record写入数据库时判断，其他时候不判断
            //    if (postCheckRbx11.Checked)
            //    {
            //        torque.CurrentTech = torque.PostTech;
            //        torque.CheckMode = 1;
            //    }
            //    else
            //    {
            //        torque.CurrentTech = torque.PreTech;
            //        torque.CheckMode = 0;
            //    }
            //    addGoodHintMessage("大纲导入成功");
            //    addGoodHintMessage("开始测试.....");
            #endregion

        }
        #endregion

        #region 开始试验
        
        #region 旧按钮
        private void startBtn_Click(object sender, EventArgs e)
        {
            try
            {
                addCommonHintMessage("-----------------------");
                addCommonHintMessage("开始");

                if (seriesNumTbx.Text == "")
                {
                    MessageBox.Show("请扫描或输入工具编号");
                    return;
                    throw new Exception("请扫描或输入工具编号");
                }

                //清空试验记录表格
                //RecordDetailDgv.Rows.Clear();

                Product product = productService.selectOneBySeriesNum(seriesNumTbx.Text);
                if (product == null)
                {
                    MessageBox.Show("请扫描或输入工具编号");
                    return;
                    throw new Exception("不存在该型号的测试大纲，请联系管理员进行添加");
                }
                //判断PLC状态
                if(!s7help.ReadPLCStatus())
                {
                    MessageBox.Show("请等待试验台初始化完成后开始试验！");
                    return;
                }

                //确认检测模式
                Vars.ControlMode = ManualChoseCB1.Checked ? true : false;

                //试验完成后置位试验结束,为了试验初始化
                s7help.WriteTestEnd(true);
                Thread.Sleep(100);

                BaseTorque torque;
                //选择校验模式，base_tool中CHECK_MODE
                switch (product.Kind)
                {
                    case 1: //数显
                        torque = new DigitalTorque(serialPort, s7help, this);
                        //新增
                        RecordDetailDgv.Columns[5].Visible = true;
                        break;
                    case 2: //电动
                        torque = new ElectricTorque(serialPort, s7help, this);
                        RecordDetailDgv.Columns[5].Visible = false;
                        break;
                    case 3: //咔嚓
                        torque = new ClickTorque(serialPort, s7help, this);
                        RecordDetailDgv.Columns[5].Visible = false;
                        break;
                    default:
                        throw new Exception("试验方式不存在，代码为" + product.Kind);
                }
                torque.Id = product.Id;//base_tool_precheck中Tool_ID=>06ec1d1514dc499a9200b120587e499c,1000
                torque.SeriesNum = product.NumIndex;//序列号
                torque.PreTech = new Technology("", product.Kind, "", "");
                torque.PostTech = new Technology("", product.Kind, "", "");
                
                //根据techDgv中选中的工艺base_tool_precheck的ID
                List<TechnologyDetail> listPreCheckList = new List<TechnologyDetail>();
                List<TechnologyDetail> listPostCheckList = new List<TechnologyDetail>();
                //foreach (DataGridViewRow row in techDgv.Rows)
                //{
                //    if (techDgv.Rows[0].Cells[0].Value.ToString() == "true")
                //    {
                //        listPreCheckList.Add(technologyDetailService.SelectPreCheckList(row.Cells["Id"].ToString())[0]);
                //        listPostCheckList.Add(technologyDetailService.SelectPostCheckList(row.Cells["Id"].ToString())[0]);
                //    }

                //}
                //这个数目有问题-------需要改动
                for (int i= 0;i < techDgv.Rows.Count-1;i++)
                {
                    if((techDgv.Rows[i].Cells["Checkbox"].Value.ToString() == "True"|| techDgv.Rows[i].Cells["Checkbox"].Value.ToString() == "1") 
                        &&(!(techDgv.Rows[i].Cells["id"].Value.ToString())[0].Equals("")))
                    {
                        listPreCheckList.Add(technologyDetailService.SelectPreCheckList(techDgv.Rows[i].Cells["id"].Value.ToString())[0]);
                        listPostCheckList.Add(technologyDetailService.SelectPostCheckList(techDgv.Rows[i].Cells["id"].Value.ToString())[0]);
                    }
                }

                //torque.PreTech.TechnologyDetailList = technologyDetailService.selectPreList(torque.Id);
                //torque.PostTech.TechnologyDetailList = technologyDetailService.selectPostList(torque.Id);

                torque.PreTech.TechnologyDetailList = listPreCheckList; 
                torque.PostTech.TechnologyDetailList = listPostCheckList;

                if (torque.PreTech.TechnologyDetailList == null || torque.PostTech.TechnologyDetailList == null)
                {
                    throw new Exception("该型号未查询到试验大纲，请联系管理员进行添加");
                }
                //根据选择来决定是否是回校，回校只在此处以及record写入数据库时判断，其他时候不判断
                if (postCheckRbx1.Checked)
                {
                    torque.CurrentTech = torque.PostTech;
                    torque.CheckMode = 1;
                }
                else
                {
                    torque.CurrentTech = torque.PreTech;
                    torque.CheckMode = 0;
                }
                addGoodHintMessage("大纲导入成功");
                addGoodHintMessage("开始测试.....");
                //线程
                th_temp = new Thread(torque.doTest);
                th_temp.Start();
            }
            catch (Exception ex)
            {
                addErrorHintMessage("测试异常终止,原因:" + ex.Message);
                addCommonHintMessage("-----------------------");
                return;
            }

        }
        #endregion

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            try
            {
                addCommonHintMessage("-----------------------");
                addCommonHintMessage("试验开始");
                
                //关闭按钮使能
                barButtonItem1.Enabled = false;

                if (seriesNumTbx.Text == "")
                {
                    addCommonHintMessage("未扫描或输入工具编号，试验结束！");
                    addCommonHintMessage("-----------------------");
                    //打开试验使能
                    barButtonItem1.Enabled = true;
                    MessageBox.Show("请扫描或输入工具编号");
                    return;
                }

                //清空试验记录表格
                //RecordDetailDgv.Rows.Clear();

                Product product = productService.selectOneBySeriesNum(seriesNumTbx.Text);
                if (product == null)
                {
                    addCommonHintMessage("不存在该型号的测试大纲，请联系管理员进行添加，试验结束！");
                    addCommonHintMessage("-----------------------");
                    MessageBox.Show("不存在该型号的测试大纲，请联系管理员进行添加");
                    return;
                }
                //判断PLC状态
                if(!s7help.ReadPLCStatus())
                {
                    addCommonHintMessage("试验台未初始化完毕，试验结束！");
                    addCommonHintMessage("-----------------------");
                    MessageBox.Show("请等待试验台初始化完成后开始试验！");
                    return;
                }

                //确认检测模式
                Vars.ControlMode = ManualChoseCB1.Checked ? true : false;

                //试验完成后置位试验结束,为了试验初始化
                //s7help.WriteTestEnd(true);
                //Thread.Sleep(100);

                BaseTorque torque;
                //选择校验模式，base_tool中CHECK_MODE
                switch (product.Kind)
                {
                    case 1: //数显
                        torque = new DigitalTorque(serialPort, s7help, this);
                        //新增
                        RecordDetailDgv.Columns[5].Visible = true;
                        break;
                    case 2: //电动
                        torque = new ElectricTorque(serialPort, s7help, this);
                        RecordDetailDgv.Columns[5].Visible = false;
                        break;
                    case 3: //咔嚓
                        torque = new ClickTorque(serialPort, s7help, this);
                        RecordDetailDgv.Columns[5].Visible = false;
                        break;
                    default:
                        throw new Exception("试验方式不存在，代码为" + product.Kind);
                }
                torque.Id = product.Id;//base_tool_precheck中Tool_ID=>06ec1d1514dc499a9200b120587e499c,1000
                torque.SeriesNum = product.NumIndex;//序列号
                torque.PreTech = new Technology("", product.Kind, "", "");
                torque.PostTech = new Technology("", product.Kind, "", "");
                
                //根据techDgv中选中的工艺base_tool_precheck的ID
                List<TechnologyDetail> listPreCheckList = new List<TechnologyDetail>();
                List<TechnologyDetail> listPostCheckList = new List<TechnologyDetail>();
                
                //这个数目有问题-------需要改动
                for (int i= 0;i < techDgv.Rows.Count-1;i++)
                {
                    if( (techDgv.Rows[i].Cells["Checkbox"].EditedFormattedValue.ToString() == "1"||techDgv.Rows[i].Cells["Checkbox"].EditedFormattedValue.ToString() == "True")
                        &&(!(techDgv.Rows[i].Cells["id"].Value.ToString())[0].Equals("")))
                    {
                        listPreCheckList.Add(technologyDetailService.SelectPreCheckList(techDgv.Rows[i].Cells["id"].Value.ToString())[0]);
                        listPostCheckList.Add(technologyDetailService.SelectPostCheckList(techDgv.Rows[i].Cells["id"].Value.ToString())[0]);
                    }
                }

                torque.PreTech.TechnologyDetailList = listPreCheckList; 
                torque.PostTech.TechnologyDetailList = listPostCheckList;

                if (torque.PreTech.TechnologyDetailList == null || torque.PostTech.TechnologyDetailList == null)
                {
                    throw new Exception("该型号未查询到试验大纲，请联系管理员进行添加");
                }
                //根据选择来决定是否是回校，回校只在此处以及record写入数据库时判断，其他时候不判断
                if (postCheckRbx1.Checked)
                {
                    torque.CurrentTech = torque.PostTech;
                    torque.CheckMode = 1;
                }
                else
                {
                    torque.CurrentTech = torque.PreTech;
                    torque.CheckMode = 0;
                }
                addGoodHintMessage("大纲导入成功");
                //线程
                th_temp = new Thread(torque.doTest);
                th_temp.Start();
            }
            catch (Exception ex)
            {
                addErrorHintMessage("测试异常终止,原因:" + ex.Message);
                addCommonHintMessage("-----------------------");
                return;
            }
         }

        #endregion

        #region 测试按钮
        //试验停止按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //试验完成后置位试验结束,为了试验停止
            s7help.WriteTestEnd(true);

            //停止试验线程
            th_temp.Abort();
            while (th_temp.ThreadState != ThreadState.Aborted)
            {
                Thread.Sleep(100);
            }
            #region
            //for (int i = 0; i < 20; i++)
            //{
            //    serialPort.send(textBox2.Text);
            //    Thread.Sleep(100);
            //}
            //string sql = "";
            //sql = "INSERT INTO `torque_calibrator`.`t_user` (`id`, `name`, `pwd`) VALUES ('" + Guid.NewGuid().ToString("N") + "', '张五', '123456')";
            //MysqlTool.beginTransaction();
            //MysqlTool.addTransaction(sql);
            ////sql = "INSERT INTO `torque_calibrator`.`t_user` (`id`, `name`, `pwd`) VALUES ('" + Guid.NewGuid().ToString("N") + "', '张五', '123456')";
            //MysqlTool.addTransaction(sql);
            //MysqlTool.executeTransaction();
            //RecordService recordService = new RecordServiceImpl();
            //Record r1 = new Record();
            //r1.Mode = 0;
            //r1.Id = Guid.NewGuid().ToString("N");
            //r1.Operator = "kwkw";
            //r1.ProName = "卡塔扳手";
            //r1.Result = 1;
            //r1.SeriesNum = "00903";
            //r1.RecordDetailList = new List<RecordDetail>();
            //RecordDetail rd1 = new RecordDetail();
            //rd1.Difference = 0.3f;
            //rd1.Lower = 65;
            //rd1.Upper = 85;
            //rd1.Standard = 70;
            //rd1.TestValue = 60.4f;
            //rd1.TorqueValue = 71;
            //rd1.Result = 1;
            //rd1.RecordId = r1.Id;
            //rd1.Percent = 0.99f;
            //r1.RecordDetailList.Add(rd1);
            //RecordDetail rd2 = new RecordDetail();
            //rd2.Difference = 0.3f;
            //rd2.Lower = 65;
            //rd2.Upper = 85;
            //rd2.Standard = 70;
            //rd2.TestValue = 60.4f;
            //rd2.TorqueValue = 71;
            //rd2.Result = 1;
            //rd2.Percent = 0.99f;
            //RecordDetail rd = rd2;
            ////r1.RecordDetailList.Add(rd2);
            ////recordService.addOne(r1);
            //RecordDetailDgv.Rows.Add(1);
            //RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["percent"].Value = rd.Percent;
            //RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["result1"].Value = rd.Result == 0 ? "合格" : "不合格";
            //RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["Lower"].Value = rd.Lower;
            //RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["upper"].Value = rd.Upper;
            //RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["testValue"].Value = rd.TestValue;
            //RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["standard"].Value = rd.Standard;
            //RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["difference"].Value = rd.Difference;
            #endregion
        }
        //下一步按钮
        private void button2_Click(object sender, EventArgs e)
        {

            //textBox1.Text = "";
            //btnPrint_Click(sender, e);
            //serialPort.send(textBox2.Text);
        }

        #endregion

        #region BarCode及HintLab
        public static Image CreateBarCode(string content)
        {
            using (var barcode = new Barcode()
            {
                //true显示content，false反之
                IncludeLabel = true,

                //content的位置
                Alignment = AlignmentPositions.CENTER,

                //条形码的宽高
                Width = 150,
                Height = 50,

                //类型
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,

                //颜色
                BackColor = Color.White,
                ForeColor = Color.Black,
            })
            {
                return barcode.Encode(TYPE.CODE128B, content);
            }
        }
        private void hintBad(string str)
        {
            hintLab.ForeColor = Color.Red;
            hintLab.Text = str;
        }
        private void hintGod(string str)
        {
            hintLab.ForeColor = Color.Green;
            hintLab.Text = str;
        }
        #endregion

        #region 打印报表
        /// <summary>
        /// 打印的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            // printDocument1 为 打印控件
            //设置打印用的纸张 当设置为Custom的时候，可以自定义纸张的大小，还可以选择A4,A5等常用纸型
            this.printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custom", 60, 40);

            this.printDocument1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            //将写好的格式给打印预览控件以便预览
            printPreviewDialog1.Document = printDocument1;
            //显示打印预览
            DialogResult result = printPreviewDialog1.ShowDialog();
            //printDocument1.PrinterSettings.PrinterName = "Foxit Reader PDF Printer";
            //printDocument1.Print();
        }

        /// <summary>
        /// 打印的格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            /*如果需要改变自己 可以在new Font(new FontFamily("黑体"),11）中的“黑体”改成自己要的字体就行了，黑体 后面的数字代表字体的大小
             System.Drawing.Brushes.Blue , 170, 10 中的 System.Drawing.Brushes.Blue 为颜色，后面的为输出的位置 */
            //e.Graphics.DrawString("新乡市三月软件公司入库单", new Font(new FontFamily("黑体"), 11), System.Drawing.Brushes.Black, 170, 10);
            //e.Graphics.DrawString("供货商:河南科技学院", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Blue, 10, 12);
            ////信息的名称
            //e.Graphics.DrawLine(Pens.Black, 8, 30, 480, 30);
            //e.Graphics.DrawString("入库单编号", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 9, 35);
            //e.Graphics.DrawString("商品名称", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 160, 35);
            //e.Graphics.DrawString("数量", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 260, 35);
            //e.Graphics.DrawString("单价", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 330, 35);
            //e.Graphics.DrawString("总金额", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 400, 35);
            //e.Graphics.DrawLine(Pens.Black, 8, 50, 480, 50);
            ////产品信息
            //e.Graphics.DrawString("R2011-01-2016:06:35", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 9, 55);
            //e.Graphics.DrawString("联想A460", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 160, 55);
            //e.Graphics.DrawString("100", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 260, 55);
            //e.Graphics.DrawString("200.00", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 330, 55);
            //e.Graphics.DrawString("20000.00", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 400, 55);


            //e.Graphics.DrawLine(Pens.Black, 8, 200, 480, 200);
            //e.Graphics.DrawString("地址：新乡市河南科技学院信息工程学院", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 9, 210);
            //e.Graphics.DrawString("经办人:任忌", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 220, 210);
            //e.Graphics.DrawString("服务热线:15083128577", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 320, 210);
            //e.Graphics.DrawString("入库时间:" + DateTime.Now.ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 9, 230);
            e.Graphics.DrawImage(CreateBarCode("123344444"), new Rectangle(20, 20, 10, 20));
        }
        #endregion

        #region 打印试验标签
        //试验完成标签打印
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ZebraPrinterHelper.ZebraPrinter("","");

            //提示是否打印

            //读取该试验工具编码

            //查询合格信息

            //试验时间

            //组合标签String

            //打印标签

        }
        #endregion

        #region 按钮触发函数
        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认注销？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            wnd.Show();
            P_Con_User.CurrentUser = null;
            this.Close();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            wnd.Close();
        }

        private void 工艺管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new TechnologyWnd().ShowDialog();
        }

        private void 试验记录查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new RecordWnd().ShowDialog();
        }

        private void borrow_Click(object sender, EventArgs e)
        {
            //Vars.BorrowToolWnd = true;
            //borrowForm.ShowDialog();
        }
        //选择扭矩标准按钮
        private void chooseTechBtn_Click(object sender, EventArgs e)
        {
            if (seriesNumTbx.Text == "")
            {
                hintBad("请扫描或输入工具编号");
            }
            //Product product = productService.selectOneBySeriesNum(seriesNumTbx.Text);
            //if (product == null)
            //{
            //    hintBad("不存在该型号的测试大纲，请联系管理员进行添加");
            //}
            ////product.Tech = technologyService.selectOne(product.TechId);
            ////if (product.Tech == null)
            ////{
            ////    hintBad("该型号未查询到试验大纲，请联系管理员进行添加");
            ////}
            ////product.Tech.TechnologyDetailList = technologyDetailService.selectList(product.Id);
            //product.TechnologyDetailList = technologyDetailService.selectList(product.Id);


            //if (techDgv.Visible)
            //{
            //    techDgv.Visible = false;
            //}
            //else
            //{
            //    techDgv.Visible = true;
            //    foreach (DataGridViewRow row in techDgv.Rows)
            //    {                
            //        row.Cells[0].Value = true;
            //    }
            //    refreshDgv(product.TechnologyDetailList, product);
            //}

            if (techDgv.Visible)
            {
                techDgv.Visible = false;
            }
            else
            {
                techDgv.Visible = true;
            }

        }
        #endregion

        #region 控件消息显示
        private void addErrorHintMessage(string msg)
        {
            hintRtbx.SelectionColor = Color.Red;
            hintRtbx.AppendText(msg + System.Environment.NewLine);
            hintRtbx.ScrollToCaret();
        }
        private void addGoodHintMessage(string msg)
        {
            hintRtbx.SelectionColor = Color.Green;
            hintRtbx.AppendText(msg + System.Environment.NewLine);
            hintRtbx.ScrollToCaret();
        }
        private void addCommonHintMessage(string msg)
        {
            hintRtbx.SelectionColor = Color.Black;
            hintRtbx.AppendText(msg + System.Environment.NewLine);
            hintRtbx.ScrollToCaret();
        }
        public void refreshTextbox(TextBox tbx, string str)
        {
            tbx.Text = str;
        }
        #endregion

        #region 刷新选择工艺表格
        private void refreshDgv(List<TechnologyDetail> techDetailList, Product product)
        {
            if (techDetailList == null)
            {
                techDgv.Rows.Clear();
                return;
            }
            techDgv.Rows.Clear();
            techDgv.Rows.Add(techDetailList.Count);
            for (int i = 0; i < techDetailList.Count; i++)
            {
                //判断是否为回校参数
                if (this.postCheckRbx1.Checked)
                { 
                    techDgv.Rows[i].Cells["id"].Value = techDetailList[i].Id;
                    techDgv.Rows[i].Cells["Tool_ID"].Value = techDetailList[i].ToolID;
                    techDgv.Rows[i].Cells["ToolName"].Value = product.Name;
                    techDgv.Rows[i].Cells["num1"].Value = techDetailList[i].PostNum;
                    techDgv.Rows[i].Cells["index1"].Value = techDetailList[i].PostNum;
                    techDgv.Rows[i].Cells["standard1"].Value = techDetailList[i].Standard;
                    techDgv.Rows[i].Cells["upper1"].Value = techDetailList[i].PostNum;
                    techDgv.Rows[i].Cells["lower1"].Value = techDetailList[i].PostLower;
                    techDgv.Rows[i].Cells[0].Value = true;
                }
                else
                {
                    techDgv.Rows[i].Cells["id"].Value = techDetailList[i].Id;
                    techDgv.Rows[i].Cells["Tool_ID"].Value = techDetailList[i].ToolID;
                    techDgv.Rows[i].Cells["ToolName"].Value = product.Name;
                    techDgv.Rows[i].Cells["num1"].Value = techDetailList[i].PreNum;
                    techDgv.Rows[i].Cells["index1"].Value = techDetailList[i].PreNum;
                    techDgv.Rows[i].Cells["standard1"].Value = techDetailList[i].Standard;
                    techDgv.Rows[i].Cells["upper1"].Value = techDetailList[i].PreUpper;
                    techDgv.Rows[i].Cells["lower1"].Value = techDetailList[i].PreLower;
                    techDgv.Rows[i].Cells[0].Value = true;
                }

            }
        }
        private void refreshDgv(List<TechnologyDetail> techDetailList)
        {
            if (techDetailList == null)
            {
                techDgv.Rows.Clear();
                return;
            }
            techDgv.Rows.Clear();
            techDgv.Rows.Add(techDetailList.Count);
            for (int i = 0; i < techDetailList.Count; i++)
            {
                //判断是否为回校参数
                if (this.postCheckRbx1.Checked)
                { 
                    techDgv.Rows[i].Cells["id"].Value = techDetailList[i].ToolID;
                    techDgv.Rows[i].Cells["num1"].Value = techDetailList[i].PostNum;
                    techDgv.Rows[i].Cells["index1"].Value = techDetailList[i].PostNum;
                    techDgv.Rows[i].Cells["standard1"].Value = techDetailList[i].Standard;
                    techDgv.Rows[i].Cells["upper1"].Value = techDetailList[i].PostNum;
                    techDgv.Rows[i].Cells["lower1"].Value = techDetailList[i].PostLower;
                }
                else
                {
                    techDgv.Rows[i].Cells["id"].Value = techDetailList[i].ToolID;
                    techDgv.Rows[i].Cells["num1"].Value = techDetailList[i].PreNum;
                    techDgv.Rows[i].Cells["index1"].Value = techDetailList[i].PreNum;
                    techDgv.Rows[i].Cells["standard1"].Value = techDetailList[i].Standard;
                    techDgv.Rows[i].Cells["upper1"].Value = techDetailList[i].PreUpper;
                    techDgv.Rows[i].Cells["lower1"].Value = techDetailList[i].PreLower;
                }

                
            }
        }

        #endregion

        #region 新界面按钮
        
        //退出按钮
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("确认退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            //试验完成后置位试验结束,为了试验停止
            s7help.WriteTestEnd(true);
            wnd.Close();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Vars.BorrowToolWnd = true;
            borrowForm.ShowDialog();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new RecordWnd().ShowDialog();
        }
        //注销
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("确认注销？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            //试验完成后置位试验结束,为了试验停止
            s7help.WriteTestEnd(true);
            wnd.Show();
            P_Con_User.CurrentUser = null;
            this.Close();
        }
        //扭矩标准选择
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (seriesNumTbx.Text == "")
            {
                hintBad("请扫描或输入工具编号");
            }

            if (techDgv.Visible)
            {
                techDgv.Visible = false;
            }
            else
            {
                techDgv.Visible = true;
            }

        }
        //停止试验按钮
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //关闭开启的试验线程

            //打开按钮使能
            barButtonItem1.Enabled = true;
            //试验完成后置位试验结束,为了试验停止
            s7help.WriteTestEnd(true);
            //停止试验线程
            th_temp.Abort();
            while (th_temp.ThreadState != ThreadState.Aborted)
            {
                Thread.Sleep(100);
            }
        }



        #endregion

        
    }
}
