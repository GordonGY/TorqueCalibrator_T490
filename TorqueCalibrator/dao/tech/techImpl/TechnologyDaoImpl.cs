
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using TorqueCalibrator.pojo;
using TorqueCalibrator.untils;

namespace TorqueCalibrator.dao.tech.techImpl
{
    public class TechnologyDaoImpl : TechnologyDao
    {
        public int deleteOne(Technology tech)
        {
            throw new NotImplementedException();
        }

        public Technology selectOne(string id)
        {
            string query_string = "SELECT `id`, `mode`, `version`,`proId` FROM t_technology WHERE id=@id and isDel=0";
            DataTable dt = MysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@id", id)});
            Technology tech = null;
            if (dt.Rows.Count > 0)
            {
                tech = new Technology(dt.Rows[0]["id"].ToString(),
                    Int32.Parse(dt.Rows[0]["mode"].ToString()),
                    dt.Rows[0]["version"].ToString(),
                    dt.Rows[0]["proId"].ToString());
            }
            return tech;
        }

        public Technology selectOneByProductId(string productId)
        {
            string query_string = "SELECT * FROM t_technology WHERE id = ( SELECT techId FROM `t_product` WHERE id = @id and isDel=0) and isDel=0";
            DataTable dt = MysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@id", productId)});
            Technology tech = null;
            if (dt.Rows.Count > 0)
            {
                tech = new Technology(dt.Rows[0]["id"].ToString(),
                    Int32.Parse(dt.Rows[0]["mode"].ToString()),
                    dt.Rows[0]["version"].ToString(),
                    dt.Rows[0]["proId"].ToString());
            }
            return tech;
        }

        public bool updateOne(Technology one)
        {
            string query_string = "UPDATE `t_technology` SET `mode`=@mode WHERE `id`=@id";
            return MysqlTool.updateData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@id", one.Id),
                    new MySqlParameter("@mode", one.Mode)});
        }


        public bool addOne(Technology one)
        {
            string query_string = "INSERT INTO `t_technology` (`id`, `mode`, `version`, `proId`) VALUES (@id, @mode, @version, @proId)";
            return MysqlTool.addData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@id", one.Id),
                    new MySqlParameter("@mode", one.Mode),
                    new MySqlParameter("@version", one.Version),
                    new MySqlParameter("@proId", one.ProId)});
        }
    }
}
