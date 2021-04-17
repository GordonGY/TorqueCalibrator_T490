using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo;

namespace TorqueCalibrator.dao.tech
{
    interface TechnologyDetailDao
    {
        List<TechnologyDetail> selectList(string toolId);

        List<TechnologyDetail> selectPreList(string toolId);
        List<TechnologyDetail> selectPostList(string toolId);

        List<TechnologyDetail> SelectPreCheckList(string toolId);

        List<TechnologyDetail> SelectPostCheckList(string toolId);
    }
}
