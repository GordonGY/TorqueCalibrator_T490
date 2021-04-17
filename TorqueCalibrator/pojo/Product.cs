using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TorqueCalibrator.pojo
{
    /// <summary>
    /// 被试件
    /// </summary>
    public class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }//被试件名称
        public int Kind { get; set; }//被试件类型
        public string NumIndex { get; set; }//被试件条码
        
        //分几层----高颜
        public List<TechnologyDetail> TechnologyDetailList { get; set; }//工艺详情列表
        
        public Technology Tech { get; set; }//试验大纲
        public string TechId { get; set; }//试验大纲ID
        public Product() { }
        public Product(string id, string name, string numIndex)
        {
            this.Id = id;
            this.Name = name;
            this.NumIndex = numIndex;
        }

        private static int numIndex = Int32.Parse(ConfigurationManager.AppSettings["numIndex"].ToString());

        /// <summary>
        /// 获取序列号代表的工具类型，用于匹配工艺(technology)
        /// </summary>
        /// <param name="seriesNum">工具编号</param>
        /// <returns>类型</returns>
        public static string getProId(string seriesNum)
        {
            return seriesNum.Substring(0, numIndex);
        }
    }
}
