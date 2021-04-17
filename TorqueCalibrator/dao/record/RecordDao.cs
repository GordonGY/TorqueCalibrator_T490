using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo;

namespace TorqueCalibrator.dao.record
{
    interface RecordDao
    {
        /// <summary>
        /// 根据不同条件查询试验记录
        /// </summary>
        /// <param name="id">唯一ID</param>
        /// <param name="seriesNum">序列号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        List<Record> selectListByAll(string id, string seriesNum, string operatorName, DateTime startTime, DateTime endTime);
        bool addOne(Record record);
        List<string> selectUserNameList(DateTime startTime, DateTime endTime);
    }
}
