
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using TorqueCalibrator.pojo.user;
using TorqueCalibrator.untils;

namespace TorqueCalibrator.dao.user.userImpl
{

    public class UserDaoImpl : UserDao
    {

        public List<P_Con_User> selectList(string id, string name, string pwd, string userNumber)
        {
            string query_string = "SELECT * from `p_con_user` where 1=1";
            if (id != "")
            {
                query_string = query_string + " and id='" + id + "'";
            }
            if (name != "")
            {
                query_string = query_string + " and USER_CODE='" + name + "'";
            }
            if (pwd != "")
            {
                query_string = query_string + " and USER_PASSWORD='" + pwd + "'";
            }
            if (userNumber != "")
            {
                query_string = query_string + " and USER_NUMBER='" + userNumber + "'";
            }
            DataTable dt = MESMysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter()});
            List<P_Con_User> ls = MESMysqlTool.toList<P_Con_User>(dt);
            return ls.Count > 0 ? ls : null;
        }


        public string selectOrganizationName(string org_id)
        {
            string query_string = "SELECT ORG_NAME from `p_con_organization` where ORG_ID='" + org_id + "'";
            DataTable dt = MESMysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter()});
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["ORG_NAME"].ToString();
            }
            else
            {
                return "";
            }

        }
    }
}
