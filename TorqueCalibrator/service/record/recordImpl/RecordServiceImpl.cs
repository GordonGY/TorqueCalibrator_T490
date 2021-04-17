using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.dao.record;
using TorqueCalibrator.dao.record.recordImpl;
using TorqueCalibrator.pojo;

namespace TorqueCalibrator.service.record.recordImpl
{
    public class RecordServiceImpl : RecordService
    {
        private RecordDao recordDao = new RecordDaoImpl();
        private RecordDetailDao recordDetailDao = new RecordDetailDaoImpl();
        public bool addOne(Record one)
        {
            return (recordDao.addOne(one) && recordDetailDao.addList(one.RecordDetailList));
        }
        public List<Record> selectListByAll(string id, string seriesNum, string operatorName, DateTime startTime, DateTime endTime)
        {
            return recordDao.selectListByAll(id, seriesNum,operatorName, startTime, endTime);
        }


        public List<string> selectUserNameList(DateTime startTime, DateTime endTime)
        {
            return recordDao.selectUserNameList(startTime, endTime);
        }
    }
}
