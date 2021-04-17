using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorqueCalibrator.pojo
{
    /// <summary>
    /// 记录详情
    /// </summary>
    public class RecordDetail
    {
        public string Id { get; set; }
        public int Index { get; set; }//排序,自动排
        public string RecordId { get; set; }//记录ID关联用
        public float TestValue { get; set; }//扭矩校验仪检测值
        //新增
        public float ManualWriteValue { get; set; }//扭矩校验仪检测值
        public float TorqueValue { get; set; }//扭矩枪返回的值
        public float Difference { get; set; }//误差绝对值
        public float Percent { get; set; }//误差百分比
        public int Result { get; set; }//试验结果，1 or 0，1代表合格
        public float Standard { get; set; }
        public float Upper { get; set; }
        public float Lower { get; set; }
        public string Unit { get; set; }//单位

        //根据测试值生成结果
        public void judgeResult()
        {
            if ((this.TestValue >= (this.Lower + 1) * this.Standard) && (this.TestValue <= (this.Upper + 1) * this.Standard))
            {
                this.Result = 1;
            }
            else
            {
                this.Result = 0;
            }
            this.Difference = System.Math.Abs(this.TestValue - this.Standard);
            this.Percent = this.Difference / this.Standard * 100;
        }
        public void judgeResult(float ManualWriteValue)
        {
            if(ManualWriteValue > this.TestValue)
            {

            }
            if ((ManualWriteValue >= (this.Lower + 1) * this.TestValue) && (ManualWriteValue <= (this.Upper + 1) * this.TestValue))
            {
                this.Result = 1;
            }
            else
            {
                this.Result = 0;
            }
            this.Difference = Math.Abs(this.TestValue - ManualWriteValue);
            this.Percent = this.Difference / TestValue * 100;
        }
    }
}
