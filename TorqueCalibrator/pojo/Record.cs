using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo.torqueGun;

namespace TorqueCalibrator.pojo
{
    /// <summary>
    /// 试验记录类
    /// </summary>
    public class Record
    {
        public string Id { get; set; }
        public string SeriesNum { get; set; }
        public BaseTorque Torque { get; set; }//关联的扳手对象
        public string ProName { get; set; }//型号
        public string Operator { get; set; }//操作者姓名
        public int Mode { get; set; }//1：回校；0：校验
        public int Result { get; set; }//试验结果
        public DateTime CreateTime { get; set; }//试验日期
        public List<RecordDetail> RecordDetailList { get; set; }
        public Record()
        {
            RecordDetailList = new List<RecordDetail>();
        }

    }
}
