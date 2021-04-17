using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorqueCalibrator.dao
{
    public abstract class DaoBase
    {
        public string DatabaseName =new SqlConnectionStringBuilder( ConfigurationManager.ConnectionStrings["MysqlConnection"].ToString()).InitialCatalog;
    }
}
