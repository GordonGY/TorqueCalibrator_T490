using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo;

namespace TorqueCalibrator.service.record
{
    interface RecordService
    {
        bool addOne(Record one);
        List<Record> selectListByAll(string id, string seriesNum,string operatorName, DateTime startTime, DateTime endTime);
        List<string> selectUserNameList(DateTime startTime, DateTime endTime);
    }
}
