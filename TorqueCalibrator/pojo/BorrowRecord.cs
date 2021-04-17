using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorqueCalibrator.pojo
{
    /// <summary>
    /// 借用记录
    /// </summary>
    public class BorrowRecord
    {
        public string Id { get; set; }
        public string SeriesNum { get; set; }
        public string Operator { get; set; }//操作者姓名
        public int Status { get; set; }//标识归还状态,0代表被借出，1代表已归还
        public DateTime CreateTime { get; set; }//借出时间
        public DateTime UpdateTime { get; set; }//归还时间

        public BorrowRecord() { }
        public BorrowRecord(string id, string seriesNum, string person, int status)
        {
            this.Id = id;
            this.SeriesNum = seriesNum;
            this.Operator = person;
            this.Status = status;
        }
    }
}
