using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.pojo;

namespace TorqueCalibrator.service.product
{
    interface ProductService
    {
        Product selectOneBySeriesNum(string seriesNum);
    }
}
