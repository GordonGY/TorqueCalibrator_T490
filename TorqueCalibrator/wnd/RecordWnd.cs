using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TorqueCalibrator.service.record;
using TorqueCalibrator.service.record.recordImpl;
using TorqueCalibrator.pojo;
using TorqueCalibrator.service.product;
using TorqueCalibrator.service.product.productImpl;
using System.Configuration;
using TorqueCalibrator.untils;
using System.IO;
using System.Runtime.InteropServices;

namespace TorqueCalibrator.wnd
{
    public partial class RecordWnd : Form
    {
        //打印报表
        private Microsoft.Office.Interop.Word._Application oWord = new Microsoft.Office.Interop.Word.Application();
        //private Microsoft.Office.Interop.Word._Application oWord = (Microsoft.Office.Interop.Word.Application)Marshal.GetActiveObject("Word.Application");
        private Microsoft.Office.Interop.Word._Document oDoc;
        object oReadOnly = false;
        object oMissing = System.Reflection.Missing.Value;

        private RecordService recordService = new RecordServiceImpl();
        private RecordDetailService recordDetailService = new RecordDetailServiceImpl();
        private ProductService productService = new ProductServiceImpl();
        private List<Record> recordList;
        private List<RecordDetail> recordDetailList;

        //最终筛选结果
        private List<Record> befterSeriesNumShowList = new List<Record>();
        private List<Record> afterSeriesNumShowList = new List<Record>();
        private List<RecordDetail> beforeRecordDetailList;
        private List<RecordDetail> afterRecordDetailList;
        public RecordWnd()
        {
            InitializeComponent();
        }

        private void RecordWnd_Load(object sender, EventArgs e)
        {
            startDtp.Value = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0, 0)).Date;
            endDtp.Value = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

            //List<Product> list = ;
            //productCbx.DataSource = list;
            //productCbx.DisplayMember = "Name";
            userCbx.DataSource = recordService.selectUserNameList(new DateTime(), new DateTime());
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            if (startDtp.Value > endDtp.Value)
            {
                MessageBox.Show("日期选择错误->开始日期大于结束日期");
            }

            //查询初始结束时间
            DateTime sTime = Convert.ToDateTime(startDtp.Value);
            DateTime eTime = Convert.ToDateTime(endDtp.Value);

            //查询初始结束时间段内所有试验记录
            recordList = recordService.selectListByAll("", seriesNumTbx.Text, userCbx.Text, startDtp.Value, endDtp.Value);

            //筛选出所有扭矩枪型号（去重）
            List<string> seriesNumList= new List<string>();
            foreach (var reRecord in recordList)
            {
                if (reRecord.SeriesNum != null || reRecord.SeriesNum == "")
                {
                    seriesNumList.Add(reRecord.SeriesNum);
                }

            }
            HashSet<string> seriesNumHashSet = new HashSet<string>(seriesNumList);
            
            //中间变量
            DateTime beforeTempTime = sTime;
            int beforeMaxIndex = -1;
            DateTime afterTempTime = sTime;
            int afterMaxIndex = -1;

            //前后校筛选结果
            List<Record> beforeTotalseriesNumList = new List<Record>();
            List<Record> afterTotalseriesNumList = new List<Record>();
            
            //最终筛选结果（已作为全局变量）
            //List<Record> befterSeriesNumShowList = new List<Record>();
            //List<Record> afterSeriesNumShowList = new List<Record>();
            
            //清掉上次筛选
            befterSeriesNumShowList.Clear();
            afterSeriesNumShowList.Clear();

            //筛选所有型号扳手每一天最近一次包含前后校的试验结果
            foreach (var seriesNum in seriesNumHashSet)
            {
                beforeTotalseriesNumList.Clear();
                afterTotalseriesNumList.Clear();
                //找到所有相同的seriesNum
                foreach (var record in recordList)
                {
                    if(record.SeriesNum == seriesNum && record.Mode == 0)
                    {
                        beforeTotalseriesNumList.Add(record);
                    }
                    else if (record.SeriesNum == seriesNum && record.Mode == 1)
                    {
                        afterTotalseriesNumList.Add(record);
                    }
                }

                //找到时间最近的一条
                for (DateTime dateTime = sTime; dateTime.Date < eTime.Date; dateTime = dateTime.AddDays(1))
                {
                    beforeMaxIndex = -1;
                    afterMaxIndex = -1;
                    //查出前校
                    for (int i = 0; i < beforeTotalseriesNumList.Count; i++)
                    {
                        if (dateTime.AddDays(1) > beforeTotalseriesNumList[i].CreateTime && beforeTotalseriesNumList[i].CreateTime >= dateTime)
                        {
                            if (beforeTempTime < beforeTotalseriesNumList[i].CreateTime)
                            {
                                beforeTempTime = beforeTotalseriesNumList[i].CreateTime;
                                beforeMaxIndex = i;
                            }
                        }

                    }
                    //查出后校
                    for (int i = 0; i < afterTotalseriesNumList.Count; i++)
                    {
                        if (dateTime.AddDays(1) > afterTotalseriesNumList[i].CreateTime && afterTotalseriesNumList[i].CreateTime >= dateTime)
                        {
                            if (afterTempTime < afterTotalseriesNumList[i].CreateTime)
                            {
                                afterTempTime = afterTotalseriesNumList[i].CreateTime;
                                afterMaxIndex = i;
                            }
                        }

                    }
                    if (beforeMaxIndex != -1 && afterMaxIndex != -1)
                    {
                        //添加一天中最新一条试验记录
                        befterSeriesNumShowList.Add(beforeTotalseriesNumList[beforeMaxIndex]);
                        afterSeriesNumShowList.Add(afterTotalseriesNumList[afterMaxIndex]);
                    }
                    
                }

            }

            //显示可打印的查询结果
            RecordDgv.Rows.Clear();
            RecordDetailDgv.Rows.Clear();
            if (befterSeriesNumShowList != null && afterSeriesNumShowList != null && !AllTRCheckBox.Checked && afterSeriesNumShowList.Count != 0 && befterSeriesNumShowList.Count != 0)
            {
                RecordDetailDgv.Visible = false;
                RecordDgv.Rows.Add(befterSeriesNumShowList.Count);

                //隐去试验公共不同的字段4,5,7,8
                RecordDgv.Columns[4].Visible = false; 
                RecordDgv.Columns[5].Visible = false; 
                RecordDgv.Columns[7].Visible = false; 
                //RecordDgv.Columns[8].Visible = false; 

                for (int i = 0; i < befterSeriesNumShowList.Count; i++)
                {
                    RecordDgv.Rows[i].Cells["id"].Value = recordList[i].Id;
                    RecordDgv.Rows[i].Cells["seriesNum"].Value = recordList[i].SeriesNum;
                    RecordDgv.Rows[i].Cells["proName"].Value = recordList[i].ProName;
                    //RecordDgv.Rows[i].Cells["checkMode"].Value = recordList[i].Mode == 1 ? "回校" : "校验";
                    RecordDgv.Rows[i].Cells["operator1"].Value = recordList[i].Operator;
                    //RecordDgv.Rows[i].Cells["result"].Value = recordList[i].Result == 1 ? "合格" : "不合格";
                    //RecordDgv.Rows[i].Cells["createTime"].Value = recordList[i].CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    RecordDgv.Rows[i].Cells["print"].Value = "打印";
                }
            }
            //显示所有试验数据
            if (recordList != null && AllTRCheckBox.Checked)
            {
                RecordDetailDgv.Visible = true;
                RecordDgv.Rows.Add(recordList.Count);                
                RecordDgv.Columns[4].Visible = true; 
                RecordDgv.Columns[5].Visible = true; 
                RecordDgv.Columns[7].Visible = true; 
                RecordDgv.Columns[8].Visible = true; 
                for (int i = 0; i < recordList.Count; i++)
                {
                    RecordDgv.Rows[i].Cells["id"].Value = recordList[i].Id;
                    RecordDgv.Rows[i].Cells["seriesNum"].Value = recordList[i].SeriesNum;
                    RecordDgv.Rows[i].Cells["proName"].Value = recordList[i].ProName;
                    RecordDgv.Rows[i].Cells["checkMode"].Value = recordList[i].Mode == 1 ? "回校" : "校验";
                    RecordDgv.Rows[i].Cells["operator1"].Value = recordList[i].Operator;
                    RecordDgv.Rows[i].Cells["result"].Value = recordList[i].Result == 1 ? "合格" : "不合格";
                    RecordDgv.Rows[i].Cells["createTime"].Value = recordList[i].CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    RecordDgv.Rows[i].Cells["print"].Value = "打印";
                }
            }
        }

        private void RecordDgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(AllTRCheckBox.Checked)
                {
                    if (e.ColumnIndex == 0) { return; }
                    recordDetailList = recordDetailService.selectListByRecordId(RecordDgv.Rows[RecordDgv.CurrentCell.RowIndex].Cells["id"].Value.ToString());
                    recordList[e.RowIndex].RecordDetailList = recordDetailList;
                    RecordDetailDgv.Rows.Clear();
                    if (recordDetailList == null)
                    {
                        return;
                    }
                    RecordDetailDgv.Rows.Add(recordDetailList.Count);
                    for (int i = 0; i < recordDetailList.Count; i++)
                    {
                        RecordDetailDgv.Rows[i].Cells["id2"].Value = recordDetailList[i].Id;
                        RecordDetailDgv.Rows[i].Cells["index"].Value = recordDetailList[i].Index;
                        RecordDetailDgv.Rows[i].Cells["testValue"].Value = recordDetailList[i].TestValue;
                        RecordDetailDgv.Rows[i].Cells["lower"].Value = recordDetailList[i].Lower;
                        RecordDetailDgv.Rows[i].Cells["result1"].Value = recordDetailList[i].Result == 1 ? "合格" : "不合格";
                        RecordDetailDgv.Rows[i].Cells["upper"].Value = recordDetailList[i].Upper;
                        RecordDetailDgv.Rows[i].Cells["standard"].Value = recordDetailList[i].Standard;
                        RecordDetailDgv.Rows[i].Cells["percent"].Value = recordDetailList[i].Percent + "%";
                        RecordDetailDgv.Rows[i].Cells["difference"].Value = recordDetailList[i].Difference;
                    }
                }
                else
                {
                    beforeRecordDetailList = recordDetailService.selectListByRecordId(befterSeriesNumShowList[e.RowIndex].Id);
                    afterRecordDetailList = recordDetailService.selectListByRecordId(afterSeriesNumShowList[e.RowIndex].Id);
                    befterSeriesNumShowList[e.RowIndex].RecordDetailList = beforeRecordDetailList;
                    afterSeriesNumShowList[e.RowIndex].RecordDetailList = afterRecordDetailList;
                    if (e.ColumnIndex == RecordDgv.Columns["print"].Index && !AllTRCheckBox.Checked)
                    {
                        if (MessageBox.Show("confirm print?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //executeToExcel(recordList[e.RowIndex]);
                            executeToWord(befterSeriesNumShowList[e.RowIndex],afterSeriesNumShowList[e.RowIndex]);
                        }
                    }
                }

                

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

        }
        private void executeToWord(Record beforeRecord, Record afterRecord)
        {

            oWord.Visible = true;
            object oFileName = "";
            try
            { 
                File.Copy(@"D:\扭力扳手校验记录单(专检）QM1_TCD00000003344_B_20180420_083651_0.doc", Application.StartupPath + @"\test.doc",true);// + DateTime.Now.ToString() + ".doc", true);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            oFileName = Application.StartupPath + @"\test.doc";
            oDoc = oWord.Documents.Open(ref oFileName, ref oMissing, ref oReadOnly, ref oMissing, ref oMissing,
                       ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            oDoc = oWord.ActiveDocument;
            int count = oDoc.Bookmarks.Count;
            for (int i = count; i > 0; i--)
            {   
                //Conclusion: "不合格non-conformity" "合格conformity"  
                // ToolCode,Date,NominalValue,MaxValue,MinValue,TestData1,TestData2,TestData3,Conclusion,CheckName,QSName,
                // AfterMaxValue,AfterMinValue,AfterTorqueValue,AfterDate,AfterCheckName,AfterQSName,AfterConclusion
                
                if (oDoc.Bookmarks[i].Name.Equals("ToolCode")) oDoc.Bookmarks[i].Range.Text = beforeRecord.SeriesNum;
                if (oDoc.Bookmarks[i].Name.Equals("Date")) oDoc.Bookmarks[i].Range.Text = beforeRecord.CreateTime.ToString(); 
                if (oDoc.Bookmarks[i].Name.Equals("NominalValue")) oDoc.Bookmarks[i].Range.Text = beforeRecord.RecordDetailList[0].Standard.ToString();
                if (oDoc.Bookmarks[i].Name.Equals("MaxValue")) oDoc.Bookmarks[i].Range.Text = ((beforeRecord.RecordDetailList[0].Upper+1)*beforeRecord.RecordDetailList[0].Standard).ToString(); 
                if (oDoc.Bookmarks[i].Name.Equals("MinValue")) oDoc.Bookmarks[i].Range.Text = ((beforeRecord.RecordDetailList[0].Lower+1)*beforeRecord.RecordDetailList[0].Standard).ToString(); 
                if(beforeRecord.RecordDetailList.Count == 1)
                {
                    if (oDoc.Bookmarks[i].Name.Equals("TestData1")) oDoc.Bookmarks[i].Range.Text = beforeRecord.RecordDetailList[0].TestValue.ToString();
                }else if(beforeRecord.RecordDetailList.Count == 2)
                {
                    if (oDoc.Bookmarks[i].Name.Equals("TestData1")) oDoc.Bookmarks[i].Range.Text = beforeRecord.RecordDetailList[0].TestValue.ToString();
                    if (oDoc.Bookmarks[i].Name.Equals("TestData2")) oDoc.Bookmarks[i].Range.Text = beforeRecord.RecordDetailList[1].TestValue.ToString();
                }else if(beforeRecord.RecordDetailList.Count == 3)
                {
                    if (oDoc.Bookmarks[i].Name.Equals("TestData1")) oDoc.Bookmarks[i].Range.Text = beforeRecord.RecordDetailList[0].TestValue.ToString();
                    if (oDoc.Bookmarks[i].Name.Equals("TestData2")) oDoc.Bookmarks[i].Range.Text = beforeRecord.RecordDetailList[1].TestValue.ToString();
                    if (oDoc.Bookmarks[i].Name.Equals("TestData3")) oDoc.Bookmarks[i].Range.Text = beforeRecord.RecordDetailList[2].TestValue.ToString();
                }            
                if (oDoc.Bookmarks[i].Name.Equals("Conclusion")) oDoc.Bookmarks[i].Range.Text = beforeRecord.Result == 1 ? "不合格non-conformity":"合格conformity";
                if (oDoc.Bookmarks[i].Name.Equals("CheckName")) oDoc.Bookmarks[i].Range.Text = beforeRecord.Operator.ToString();
                //if (oDoc.Bookmarks[i].Name.Equals("QSName")) oDoc.Bookmarks[i].Range.Text = record.ProName;

                if (oDoc.Bookmarks[i].Name.Equals("AfterMaxValue")) oDoc.Bookmarks[i].Range.Text = ((afterRecord.RecordDetailList[0].Upper+1)*afterRecord.RecordDetailList[0].Standard).ToString(); 
                if (oDoc.Bookmarks[i].Name.Equals("AfterMinValue")) oDoc.Bookmarks[i].Range.Text = ((afterRecord.RecordDetailList[0].Lower+1)*afterRecord.RecordDetailList[0].Standard).ToString(); 
                if (oDoc.Bookmarks[i].Name.Equals("AfterTorqueValue")) oDoc.Bookmarks[i].Range.Text = afterRecord.RecordDetailList[0].TestValue.ToString();
                if (oDoc.Bookmarks[i].Name.Equals("AfterDate")) oDoc.Bookmarks[i].Range.Text = afterRecord.CreateTime.ToString();
                if (oDoc.Bookmarks[i].Name.Equals("AfterCheckName")) oDoc.Bookmarks[i].Range.Text = afterRecord.Operator;
                //if (oDoc.Bookmarks[i].Name.Equals("AfterQSName")) oDoc.Bookmarks[i].Range.Text = afterRecord.ProName;
                if (oDoc.Bookmarks[i].Name.Equals("AfterConclusion")) oDoc.Bookmarks[i].Range.Text = afterRecord.Result == 0 ? "不合格non-conformity":"合格conformity";
            }
            oDoc.PrintOut();
            oDoc.Save();
            oDoc.Close();
            GC.Collect();
        }
        private void executeToExcel(Record record)
        {
            string path = Application.StartupPath + ConfigurationManager.AppSettings["excelPath"].ToString();
            File.Copy(path, Application.StartupPath + @"\test.xlsx", true);
            ExcelEdit excelOperator = new ExcelEdit();
            excelOperator.Open(Application.StartupPath + @"\test.xlsx");
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = excelOperator.GetSheet("Sheet1");
            sheet1.Cells[4, 5].Value = record.Operator;
            sheet1.Cells[4, 8].Value = record.CreateTime.ToString("yyyy-MM-dd");
            sheet1.Cells[6, 2].Value = record.SeriesNum;
            sheet1.Cells[6, 5].Value = record.ProName;
            sheet1.Cells[6, 8].Value = record.Result == 1 ? "合格" : "不合格";
            for (int i = 0; i < record.RecordDetailList.Count; i++)
            {
                sheet1.Cells[i + 11, 2].Value = record.RecordDetailList[i].Index;
                sheet1.Cells[i + 11, 3].Value = record.RecordDetailList[i].TestValue + " N·m";
                sheet1.Cells[i + 11, 4].Value = record.RecordDetailList[i].Standard + " N·m";
                sheet1.Cells[i + 11, 5].Value = record.RecordDetailList[i].Difference;
                sheet1.Cells[i + 11, 6].Value = record.RecordDetailList[i].Percent + "%";
                sheet1.Cells[i + 11, 7].Value = record.Result == 1 ? "合格" : "不合格";
                sheet1.Cells[i + 11, 8].Value = record.RecordDetailList[i].Upper + " N·m";
                sheet1.Cells[i + 11, 9].Value = record.RecordDetailList[i].Lower + " N·m";
            }
            if (ConfigurationManager.AppSettings["printMode"].ToString().Equals("preview"))
            {
                sheet1.PrintPreview();
            }
            else if (ConfigurationManager.AppSettings["printMode"].ToString().Equals("print"))
            {
                sheet1.PrintOutEx();
            }

            excelOperator.Save();
            excelOperator.Close();
            GC.Collect();
        }

        private void RecordDgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void productCbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void printBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
