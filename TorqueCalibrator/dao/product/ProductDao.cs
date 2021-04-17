using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo;

namespace TorqueCalibrator.dao.product
{
    /// <summary>
    /// 产品类型对应的数据库dao接口
    /// </summary>
    interface ProductDao
    {
        List<Product> selectList(string numIndex);
    }
}
