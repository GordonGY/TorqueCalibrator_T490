using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorqueCalibrator.pojo
{
    /// <summary>
    /// 工艺步骤
    /// </summary>
    public class TechnologyDetail
    {
        public string Id { get; set; }
        public string ToolID{ get; set; }
        public int PreNum { get; set; }//校验时打的个数
        public int PostNum { get; set; }//回校时打的个数
        public int Num { get; set; }//执行时打的个数，会被动态赋值，可能选择为prenum或postnum
        public int Index { get; set; }//步骤排序
        public float Standard { get; set; }//扭矩标准值
        public float PreUpper { get; set; }//预检标准上限值
        public float PostUpper { get; set; }//回校标准上限值
        public float Upper { get; set; }//执行时上限，会被动态赋值

        public float PreLower { get; set; }//预检标准下限值
        public float PostLower { get; set; }//回校标准下限值
        public float Lower { get; set; }//执行时下限，会被动态赋值
        public string Unit { get; set; }//单位        
        public DateTime CreateTime { get; set; }//创建时间
        public User CreateUser { get; set; }//创建者
        public DateTime UpdateTime { get; set; }//上次更新时间
        public User UpdateUser { get; set; }//上次修改者
    }
}
