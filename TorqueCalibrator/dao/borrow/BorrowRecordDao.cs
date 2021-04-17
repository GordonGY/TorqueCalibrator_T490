using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo;

namespace TorqueCalibrator.dao.borrow
{
    interface BorrowRecordDao
    {
        List<BorrowRecord> selectListByAll(string id, string seriesNum, string operatorName, DateTime startTime, DateTime endTime, int status);

        /// <summary>
        /// 根据器具ID更新归还记录的状态
        /// </summary>
        /// <param name="id">器具ID</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        bool updateStatus(string id, int status);

        List<string> selectUserNameList(DateTime startTime, DateTime endTime);

        bool addOne(BorrowRecord one);
    }
}
