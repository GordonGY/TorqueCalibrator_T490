using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo.user;

namespace TorqueCalibrator.dao.user
{
    interface UserDao
    {
        List<P_Con_User> selectList(string id, string name, string pwd, string userNumber);
        string selectOrganizationName(string org_id);

    }
}
