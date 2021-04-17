using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TorqueCalibrator.pojo.user;
using TorqueCalibrator.untils;
using System.Windows.Forms;
using System.Drawing;
using TorqueCalibrator.service.record;
using TorqueCalibrator.service.record.recordImpl;
using TorqueCalibrator.variables;

namespace TorqueCalibrator.pojo.torqueGun
{
    /// <summary>
    /// 扳手类
    /// </summary>
    public abstract class BaseTorque
    {
        public SerialPortHelper serialPort;
        public S7Help s7Help;
        public Record currentRecord;
        public RecordDetail currentRecordDetail;
        private RecordService recordService = new RecordServiceImpl();
        public string name;//扳手名称
        public Main wnd { get; set; }//窗体传入
        public string Id { get; set; }//对应数据库里的ID
        public string SeriesNum { get; set; }//扳手序列号
        public Product ProductObject { get; set; }//扳手所属的类型
        public Record PrecheckRecord { get; set; }//预检record
        public Record PostcheckRecord { get; set; }//回校record
        public Technology PreTech { get; set; }//当前扳手校验工艺
        public Technology PostTech { get; set; }//当前扳手回校工艺
        public Technology CurrentTech { get; set; }//当前使用的工艺
        public int CheckMode { get; set; }//检查是校验还是回校，校验为0，回校为1

        public float choseSensorEdge = float.Parse(ConfigurationManager.AppSettings["ChoseSensor"].ToString());
        public float speed = float.Parse(ConfigurationManager.AppSettings["kataSpeed"].ToString());
        public float toZeroSpeed = float.Parse(ConfigurationManager.AppSettings["ToZeroSpeed"].ToString());
        public float changeSpeed = float.Parse(ConfigurationManager.AppSettings["ChangeSpeed"].ToString());
        /// <summary>
        /// 执行测试任务
        /// </summary>
        public abstract void doTest();

        /// <summary>
        /// 设置测量模式
        /// </summary>
        public void setMode(SerialPortHelper serialPort)
        {
            int temp_mode = 0;//用于存储各种扳手实际对应仪器的变量

            //设置PLC模式，一一对应，电子扳手：仪器的1->PLC的1；咔嗒扳手：仪器的3->PLC的1；电动扳手：仪器的2->PLC的2
            switch (CurrentTech.Mode)
            {
                case 1:
                    s7Help.WriteTestMode(Vars.ControlMode ? 2 : 1);
                    temp_mode = 2;
                    break;
                case 2:
                    //s7Help.WriteTestMode(Vars.ControlMode ? 3 : 2);
                    s7Help.WriteTestMode(2);//电动
                    temp_mode = 6;
                    break;
                case 3:
                    s7Help.WriteTestMode(Vars.ControlMode ? 2 : 1);
                    //s7Help.WriteTestMode(1);//咔嗒
                    temp_mode = 3;
                    break;
            }
            //设置校验仪模式
            for (int i = 0; i < 20; i++)
            {
                serialPort.send(temp_mode.ToString());
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 设置传感器
        /// </summary>
        public void choseSensor(float standard)
        {
            //大于配置文件中的设置值时选择大扭矩，否则选择小扭矩
            int temp = standard > choseSensorEdge ? 2 : 1;
            s7Help.WriteCollectorSelect(temp);
        }

        public void initRecord(int checkMode)
        {
            currentRecord = new Record();
            currentRecord.Id = Guid.NewGuid().ToString("N");
            currentRecord.Mode = CurrentTech.Mode;//模式记录
            currentRecord.Operator = P_Con_User.CurrentUser.User_name;//操作者
            currentRecord.ProName = name;//扳手类型，名称
            currentRecord.SeriesNum = this.SeriesNum;//扳手序列号
            currentRecord.Result = 0;//校验结果
            currentRecord.Mode = checkMode;
        }

        public void initRecordDetail(TechnologyDetail techDetail)
        {
            currentRecordDetail = new RecordDetail();
            currentRecordDetail.RecordId = currentRecord.Id;
            currentRecordDetail.Standard = techDetail.Standard;
            currentRecordDetail.Lower = techDetail.Lower;
            currentRecordDetail.Upper = techDetail.Upper;
            //新增
            currentRecordDetail.ManualWriteValue = 0;
        }

        /// <summary>
        /// 变更MAIN窗口中数据
        /// </summary>
        /// <param name="tbx"></param>
        /// <param name="str"></param>
        public void changeWndTextBoxText(TextBox tbx, string str)
        {
            tbx.Text = str;
        }

        /// <summary>
        /// 改变richtextbox内容
        /// </summary>
        /// <param name="rtbx"></param>
        /// <param name="str"></param>
        public void changeWndRichTextBoxText(RichTextBox rtbx, string str)
        {
            rtbx.SelectionColor = Color.Black;
            rtbx.AppendText(str + System.Environment.NewLine);
            rtbx.ScrollToCaret();
        }
        /// <summary>
        /// 更新MAIN中的datagridview
        /// </summary>
        /// <param name="RecordDetailDgv"></param>
        /// <param name="rd"></param>
        /// <param name="del">是否为清空列表</param>
        public void changeWndDgvText(DataGridView RecordDetailDgv, RecordDetail rd, bool del)
        {
            if (del)
            {
                RecordDetailDgv.Rows.Clear();
                return;
            }
            RecordDetailDgv.Rows.Add(1);
            //新增
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["ManualWriteValue"].Value = "-";
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["percent"].Value = rd.Percent.ToString("0.00");
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["result1"].Value = rd.Result == 1 ? "合格" : "不合格";
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["Lower"].Value = rd.Lower;
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["upper"].Value = rd.Upper;
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["testValue"].Value = rd.TestValue.ToString("0.00");
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["standard"].Value = rd.Standard.ToString("0.00");
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["difference"].Value = rd.Difference.ToString("0.00");
        }
        //新增
        public void changeWndDgvText1(DataGridView RecordDetailDgv, RecordDetail rd, float manualWriteValue, bool del)
        {
            if (del)
            {
                RecordDetailDgv.Rows.Clear();
                return;
            }
            RecordDetailDgv.Rows.Add(1);

            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["percent"].Value = rd.Percent.ToString("0.00");
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["result1"].Value = rd.Result == 1 ? "合格" : "不合格";
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["Lower"].Value = rd.Lower;
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["upper"].Value = rd.Upper;
            //手动输入的数值
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["ManualWriteValue"].Value = manualWriteValue;
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["testValue"].Value = rd.TestValue.ToString("0.00");
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["standard"].Value = rd.Standard.ToString("0.00");
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["difference"].Value = rd.Difference.ToString("0.00");
        }

        public void changeWndDgvText2(DataGridView RecordDetailDgv, RecordDetail rd, float manualWriteValue, bool del)
        {
            if (del)
            {
                RecordDetailDgv.Rows.Clear();
                return;
            }
            RecordDetailDgv.Rows.Add(1);

            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["percent"].Value = rd.Percent.ToString("0.00");
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["result1"].Value = rd.Result == 1 ? "合格" : "不合格";
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["Lower"].Value = rd.Lower;
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["upper"].Value = rd.Upper;
            //手动输入的数值
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["ManualWriteValue"].Value = manualWriteValue;
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["testValue"].Value = rd.TestValue.ToString("0.00");
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["standard"].Value = rd.Standard.ToString("0.00");
            RecordDetailDgv.Rows[RecordDetailDgv.Rows.Count - 2].Cells["difference"].Value = rd.Difference.ToString("0.00");
        }
        public void saveRecord()
        {
            currentRecord.Result = 1;
            //判断是否全部成功
            for (int i = 0; i < currentRecord.RecordDetailList.Count; i++)
            {
                if (currentRecord.RecordDetailList[i].Result == 0)
                {
                    currentRecord.Result = 0;
                    break;
                }
            }
            recordService.addOne(currentRecord);
        }
    }
}
