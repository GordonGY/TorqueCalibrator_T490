using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.dao.tech;
using TorqueCalibrator.dao.tech.techImpl;
using TorqueCalibrator.pojo;

namespace TorqueCalibrator.service.tech.techImpl
{
    public class TechnologyDetailServiceImpl : TechnologyDetailService
    {
        private TechnologyDetailDao technologyDetailDao = new TechnologyDetailDaoImpl();

        public List<TechnologyDetail> selectList(string toolId)
        {
            return technologyDetailDao.selectList(toolId);
        }


        public List<TechnologyDetail> selectPreList(string toolId)
        {
            return technologyDetailDao.selectPreList(toolId);
        }

        public List<TechnologyDetail> selectPostList(string toolId)
        {
            return technologyDetailDao.selectPostList(toolId);
        }

        public List<TechnologyDetail> SelectPreCheckList(string toolId)
        {
            return technologyDetailDao.SelectPreCheckList(toolId);
        }

         public List<TechnologyDetail> SelectPostCheckList(string toolId)
         {
            return technologyDetailDao.SelectPostCheckList(toolId);
         }

    }
}
