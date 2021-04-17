
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using TorqueCalibrator.pojo;
using TorqueCalibrator.untils;

namespace TorqueCalibrator.dao.borrow.borrowImpl
{
    public class BorrowRecordDaoImpl : BorrowRecordDao
    {

        public List<BorrowRecord> selectListByAll(string id, string seriesNum, string operatorName, DateTime startTime, DateTime endTime, int status)
        {
            string query_string = "SELECT * from `t_borrow` where isDel=0";
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
            if (status != -1)
            {
                query_string = query_string + " and status='" + status.ToString() + "'";
            }
            query_string = query_string + " order by createTime desc";
            DataTable dt = MysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter()});
            List<BorrowRecord> ls = MysqlTool.toList<BorrowRecord>(dt);
            return ls.Count > 0 ? ls : null;
        }

        public bool updateStatus(string id, int status)
        {
            string query_string = "UPDATE `t_borrow` SET `status`=@status WHERE `id`=@id";
            return MysqlTool.updateData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@id", id),
                    new MySqlParameter("@status", status)});
        }


        public List<string> selectUserNameList(DateTime startTime, DateTime endTime)
        {
            string query_string = "SELECT distinct operator from `t_borrow` where isDel=0";
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


        public bool addOne(BorrowRecord one)
        {
            string query_string = "INSERT INTO `t_borrow` (`id`, `seriesNum`, `operator`, `status`) VALUES (@id, @seriesNum, @operator, @status)";
            return MysqlTool.addData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@id", one.Id),
                    new MySqlParameter("@seriesNum", one.SeriesNum),
                    new MySqlParameter("@operator", one.Operator),
                    new MySqlParameter("@status",one.Status)});
        }
    }
}
