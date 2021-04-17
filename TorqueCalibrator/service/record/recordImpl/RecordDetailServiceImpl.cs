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
    public class RecordDetailServiceImpl : RecordDetailService
    {
        private RecordDetailDao recordDetailDao = new RecordDetailDaoImpl();
        public List<RecordDetail> selectListByRecordId(string recordId)
        {
            return recordDetailDao.selectListByRecordId(recordId);
        }
    }
}
