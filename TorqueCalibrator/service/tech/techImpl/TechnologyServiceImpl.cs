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
    public class TechnologyServiceImpl : TechnologyService
    {
        private TechnologyDao technologyDao = new TechnologyDaoImpl();
        private TechnologyDetailDao technologyDetailDao = new TechnologyDetailDaoImpl();
        private TechnologyDetailService technologyDetailService = new TechnologyDetailServiceImpl();
        public Technology selectOne(string id)
        {
            Technology tech = technologyDao.selectOne(id);
            tech.TechnologyDetailList = technologyDetailDao.selectList(tech.Id);
            return tech;
        }


        public Technology selectOneByProductId(string productId)
        {
            Technology tech = technologyDao.selectOneByProductId(productId);
            if (tech == null)
            {
                return null;
            }
            tech.TechnologyDetailList = technologyDetailDao.selectList(tech.Id);
            return tech;
        }
    }
}
