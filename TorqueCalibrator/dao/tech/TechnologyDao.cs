using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo;

namespace TorqueCalibrator.dao.tech
{
    interface TechnologyDao
    {
        int deleteOne(Technology tech);
        Technology selectOne(string id);
        Technology selectOneByProductId(string id);
        bool updateOne(Technology tech);
        bool addOne(Technology tech);
    }
}
