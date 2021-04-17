using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.dao.product;
using TorqueCalibrator.dao.product.productImpl;
using TorqueCalibrator.pojo;

namespace TorqueCalibrator.service.product.productImpl
{
    public class ProductServiceImpl : ProductService
    {
        private ProductDao productDao = new ProductDaoImpl();


        public Product selectOneBySeriesNum(string seriesNum)
        {
            List<Product> productList = productDao.selectList(seriesNum);
            return productList == null ? null : productList[0];
        }
    }
}
