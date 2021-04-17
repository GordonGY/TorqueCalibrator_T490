using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorqueCalibrator.pojo
{
    /// <summary>
    /// 工艺对应的类
    /// </summary>
    public class Technology
    {
        public string Id { get; set; }
        public List<TechnologyDetail> TechnologyDetailList { get; set; }//工艺详情列表
        public int Mode { get; set; }//用于设定调整模式,并与PLC的变量对应
        public string Version { get; set; }//版本
        public string ProId { get; set; }//对应的产品类型的uuid
        public DateTime CreateTime { get; set; }//创建时间
        public User CreateUser { get; set; }//创建者
        public DateTime UpdateTime { get; set; }//上次更新时间
        public User UpdateUser { get; set; }//上次修改者
        public Technology() { }
        public Technology(string id,
            int mode,
            string version,
            string proId)
        {
            this.Id = id;
            this.Mode = mode;
            this.Version = version;
            this.ProId = proId;
        }
    }
}
