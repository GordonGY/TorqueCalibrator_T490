using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo;
using TorqueCalibrator.pojo.user;

namespace TorqueCalibrator.service.user
{
    interface UserService
    {
        P_Con_User selectOneById(string id);
        P_Con_User selectOneByName(string name);
        P_Con_User selectOneByNameAndPwd(string name, string pwd);
        P_Con_User selectOneByNum(string number);
        string getOrganizationName(string org_id);

    }
}
