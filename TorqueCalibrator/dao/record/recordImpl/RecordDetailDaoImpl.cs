
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using TorqueCalibrator.pojo;
using TorqueCalibrator.untils;

namespace TorqueCalibrator.dao.record.recordImpl
{
    public class RecordDetailDaoImpl : RecordDetailDao
    {
        public List<RecordDetail> selectListByRecordId(string recordId)
        {
            string query_string = "SELECT * from `t_record_detail` where recordId=@recordId and isDel=0 order by `index`";
            DataTable dt = MysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@recordId", recordId)});
            List<RecordDetail> ls = MysqlTool.toList<RecordDetail>(dt);
            return ls.Count > 0 ? ls : null;
        }
        public bool addList(List<RecordDetail> list)
        {
            string query_string = "insert into `t_record_detail` (`id`, `recordId`, `index`, `testValue`, `torqueValue`, `difference`, `percent`, `result`, `standard`, `upper`, `lower`,`manualwritevalue`) VALUES ";
            string query_string1 = "";
            for (int i = 0; i < list.Count; i++)
            {

                string uuid = Guid.NewGuid().ToString("N");
                query_string1 = query_string + "('" + uuid
                                        + "','" + list[i].RecordId
                                        + "','" + list[i].Index.ToString()
                                        + "','" + list[i].TestValue.ToString()
                                        + "','" + list[i].TorqueValue.ToString()
                                        + "','" + list[i].Difference.ToString()
                                        + "','" + list[i].Percent.ToString()
                                        + "','" + list[i].Result.ToString()
                                        + "','" + list[i].Standard.ToString()
                                        + "','" + list[i].Upper.ToString()
                                        + "','" + list[i].Lower.ToString()
                                        + "','" + list[i].ManualWriteValue.ToString() + "')"
                                        + ";";
                MysqlTool.addData(query_string1);
            }
            //query_string = query_string.Remove(query_string.Length - 1);

            return true;//;MysqlTool.addData(query_string);
        }

    }
}
