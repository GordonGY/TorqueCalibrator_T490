using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo;

namespace TorqueCalibrator.service
{
    interface TechnologyService
    {
        Technology selectOne(string id);
        Technology selectOneByProductId(string productId);
    }
}
