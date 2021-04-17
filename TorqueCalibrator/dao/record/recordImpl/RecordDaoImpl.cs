
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
    public class RecordDaoImpl : RecordDao
    {
        public List<Record> selectListByAll(string id, string seriesNum, string operatorName, DateTime startTime, DateTime endTime)
        {
            string query_string = "SELECT * from `t_record` where isDel=0";
            if (id != "")
            {
                query_string = query_string + " and id='" + id + "'";
            }
            if (operatorName != "")
            {
                query_string = query_string + " and operator='" + operatorName + "'";
            }
            if (seriesNum != "")
            {
                query_string = query_string + " and seriesNum='" + seriesNum + "'";
            }
            if (startTime.Year != 1)
            {
                query_string = query_string + " and createTime>='" + startTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            if (endTime.Year != 1)
            {
                query_string = query_string + " and createTime<='" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            DataTable dt = MysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter()});
            List<Record> ls = MysqlTool.toList<Record>(dt);
            return ls.Count > 0 ? ls : null;
        }

        public bool addOne(Record one)
        {
            string query_string = "INSERT INTO `t_record` (`id`, `seriesNum`, `proName`, `mode`, `result`, `operator`) VALUES (@id, @seriesNum, @proName, @mode, @result, @operator)";
            return MysqlTool.addData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@id", one.Id),
                    new MySqlParameter("@seriesNum", one.SeriesNum),
                    new MySqlParameter("@proName", one.ProName),
                    new MySqlParameter("@mode", one.Mode),
                    new MySqlParameter("@result", one.Result),
                    new MySqlParameter("@operator", one.Operator)});
        }

        public List<string> selectUserNameList(DateTime startTime, DateTime endTime)
        {
            string query_string = "SELECT distinct operator from `t_record` where isDel=0";
            if (startTime.Year != 1)
            {
                query_string = query_string + " and createTime>='" + startTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            if (endTime.Year != 1)
            {
                query_string = query_string + " and createTime<='" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            DataTable dt = MysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter()});
            List<string> ls = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ls.Add(dt.Rows[i]["operator"].ToString());
            }
            return ls.Count > 0 ? ls : null;
        }
    }
}
