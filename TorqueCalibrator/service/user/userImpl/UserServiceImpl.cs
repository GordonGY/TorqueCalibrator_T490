using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.dao.user;
using TorqueCalibrator.dao.user.userImpl;
using TorqueCalibrator.pojo.user;

namespace TorqueCalibrator.service.user.userImpl
{
    public class UserServiceImpl : UserService
    {
        private UserDao userDao = new UserDaoImpl();

        public P_Con_User selectOneById(string id)
        {
            List<P_Con_User> list = userDao.selectList(id, "", "", "");
            return list != null ? list[0] : null;
        }

        public P_Con_User selectOneByName(string name)
        {
            List<P_Con_User> list = userDao.selectList("", name, "", "");
            return list != null ? list[0] : null;
        }

        public P_Con_User selectOneByNameAndPwd(string name, string pwd)
        {
            List<P_Con_User> list = userDao.selectList("", name, pwd, "");
            return list != null ? list[0] : null;
        }


        public P_Con_User selectOneByNum(string number)
        {
            List<P_Con_User> list = userDao.selectList("", "", "", number);
            return list != null ? list[0] : null;
        }


        public string getOrganizationName(string org_id)
        {
            return userDao.selectOrganizationName(org_id);
        }
    }
}
