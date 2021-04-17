
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using TorqueCalibrator.pojo;
using TorqueCalibrator.untils;

namespace TorqueCalibrator.dao.product.productImpl
{
    public class ProductDaoImpl : ProductDao
    {
        public List<Product> selectList(string numIndex)
        {
            string query_string = "select ID id,TOOL_CODE numIndex,TOOL_NAME name,CHECK_MODE kind from `base_tool` where DEL_FLAG=0";
            if (numIndex != "")
            {
                query_string = query_string + " and TOOL_CODE='" + numIndex + "'";
            }
            DataTable dt = MESMysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter()});
            List<Product> ls = MESMysqlTool.toList<Product>(dt);
            return ls.Count > 0 ? ls : null;
        }
    }
}
