using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorqueCalibrator.pojo
{
    public class User
    {
        public string Id { get; set; }//唯一id,uuid
        public string Name { get; set; }//人员姓名
        public string Pwd { get; set; }//MD5加密
        public string CardNum { get; set; }//机器上刷的卡的卡号
        public string StaffNum { get; set; }//员工编号
        public string StaffName { get; set; }//员工姓名
        public string Remark { get; set; }//备注
        public User() { }
        public User(string name,string pwd,string cardNum,string staffNum,string staffName,string remark)
        {
            this.Name = name;
            this.Pwd = pwd;
            this.CardNum = cardNum;
            this.StaffName = staffName;
            this.StaffNum = staffNum;
            this.Remark = remark;
        }
    }
}
