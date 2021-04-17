using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo;
using TorqueCalibrator.dao.borrow;
using TorqueCalibrator.dao.borrow.borrowImpl;

namespace TorqueCalibrator.service.borrow.borrowImpl
{
    public class BorrowRecordServiceImpl : BorrowRecordService
    {
        private BorrowRecordDao borrowRecordDao = new BorrowRecordDaoImpl();
        public List<BorrowRecord> selectListByAll(string id, string seriesNum, string operatorName, DateTime startTime, DateTime endTime, int status = -1)
        {
            return borrowRecordDao.selectListByAll(id, seriesNum, operatorName, startTime, endTime, status);
        }

        public bool updateStatus(string id, int status)
        {
            return borrowRecordDao.updateStatus(id, status);
        }


        public List<string> selectUserNameList(DateTime startTime, DateTime endTime)
        {
            return borrowRecordDao.selectUserNameList(startTime, endTime);
        }


        public bool addOne(BorrowRecord one)
        {
            return borrowRecordDao.addOne(one);
        }
    }
}
