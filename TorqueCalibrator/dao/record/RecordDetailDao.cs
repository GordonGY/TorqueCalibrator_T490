using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo;

namespace TorqueCalibrator.dao.record
{
    interface RecordDetailDao
    {
        List<RecordDetail> selectListByRecordId(string recordId);
        bool addList(List<RecordDetail> list);
    }
}
