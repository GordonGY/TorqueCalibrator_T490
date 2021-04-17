using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TorqueCalibrator.dao.product.productImpl;
using TorqueCalibrator.pojo;
using TorqueCalibrator.service;
using TorqueCalibrator.service.product;
using TorqueCalibrator.service.product.productImpl;
using TorqueCalibrator.service.tech;
using TorqueCalibrator.service.tech.techImpl;
using TorqueCalibrator.untils;

namespace TorqueCalibrator.wnd
{
    public partial class TechnologyWnd : Form
    {
        private List<Product> productList;
        private Technology currentTech;//当前界面被选中的型号对应的工艺
        private Product currentProduct;//当前页面被选中的型号
        private bool changed = false;//代表界面上的内容有无更改
        private TechnologyService technologyService = new TechnologyServiceImpl();
        private ProductService productService = new ProductServiceImpl();
        private TechnologyDetailService technologyDetailService = new TechnologyDetailServiceImpl();
        public TechnologyWnd()
        {
            InitializeComponent();
        }

        private void TechnologyWnd_Load(object sender, EventArgs e)
        {
            productList = new ProductDaoImpl().selectList("", "", "", "", new DateTime(), new DateTime());
            productCbx.Items.Clear();
            foreach (Product p in productList)
            {
                productCbx.Items.Add(p.Name);
            }

        }

        private void productCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ((ComboBox)sender).SelectedIndex;
            if (index != -1)
            {
                currentProduct = productList[index];
                productList[index].Tech = new TechnologyServiceImpl().selectOneByProductId(productList[index].Id);
                if (productList[index].Tech == null)
                {
                    clearWnd();
                    return;
                }
                refreshDgv(productList[index].Tech.TechnologyDetailList);
                testModeCbx.SelectedIndex = productList[index].Tech.Mode - 1;
                currentTech = productList[index].Tech;
            }
            saveBtn.Enabled = false;
        }

        private void clearWnd()
        {
            refreshDgv(null);
            testModeCbx.SelectedIndex = -1;
            currentTech = null;
        }

        private void refreshDgv(List<TechnologyDetail> techDetailList)
        {
            if (techDetailList == null)
            {
                techDgv.Rows.Clear();
                return;
            }
            techDgv.Rows.Clear();
            techDgv.Rows.Add(techDetailList.Count);
            for (int i = 0; i < techDetailList.Count; i++)
            {
                techDgv.Rows[i].Cells["id"].Value = techDetailList[i].Id;
                techDgv.Rows[i].Cells["num"].Value = techDetailList[i].Num;
                techDgv.Rows[i].Cells["index"].Value = techDetailList[i].Index;
                techDgv.Rows[i].Cells["standard"].Value = techDetailList[i].Standard;
                techDgv.Rows[i].Cells["upper"].Value = techDetailList[i].Upper;
                techDgv.Rows[i].Cells["lower"].Value = techDetailList[i].Lower;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < techDgv.Rows.Count - 1; i++)
            {
                for (int j = 1; j < techDgv.Columns.Count; j++)
                {
                    if (techDgv.Rows[i].Cells[j].Value == null || techDgv.Rows[i].Cells[j].Value.ToString() == "")
                    {
                        MessageBox.Show("不能有空值");
                        return;
                    }
                }
            }
            if (MessageBox.Show("确认更改？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                /**
                 * 工艺更新逻辑
                 * 1、获取当前工艺,即currentTech中的version字段,字段+1,如由V00-V01
                 * 2、向technology表中插入新的一条tech数据,返回其id
                 * 3、更新product对应的techid为上述Id
                 * 4、将dgv中的数据插入到techdetail中,并绑定第2条中的id为techId
                 */
                //生成新的technology对象
                Technology techTemp = new Technology(Guid.NewGuid().ToString("N"),
                    testModeCbx.SelectedIndex + 1,
                    "V" + (currentTech == null ? "01" : (int.Parse(currentTech.Version.Substring(1, currentTech.Version.Length - 1)) + 1).ToString("00")),
                    currentProduct.Id);
                technologyService.addOne(techTemp);

                //更新product中的数据
                productService.updateTechId(currentProduct, techTemp.Id);

                //读取dgv中的数据,创建新的techDetail
                List<TechnologyDetail> technologyDetailList = new List<TechnologyDetail>();
                for (int i = 0; i < techDgv.Rows.Count - 1; i++)
                {
                    TechnologyDetail technologyDetail = new TechnologyDetail();
                    technologyDetail.Id = Guid.NewGuid().ToString("N");
                    technologyDetail.Index = int.Parse(techDgv.Rows[i].Cells["index"].Value.ToString());
                    technologyDetail.Num = int.Parse(techDgv.Rows[i].Cells["num"].Value.ToString());
                    technologyDetail.Standard = float.Parse(techDgv.Rows[i].Cells["standard"].Value.ToString());
                    technologyDetail.Upper = float.Parse(techDgv.Rows[i].Cells["upper"].Value.ToString());
                    technologyDetail.Lower = float.Parse(techDgv.Rows[i].Cells["lower"].Value.ToString());
                    technologyDetailList.Add(technologyDetail);
                }
                technologyDetailService.addList(technologyDetailList, techTemp.Id);
                saveBtn.Enabled = false;
            }
        }

        private void testModeCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            //由于设置了下拉框的属性,不可能选-1
            int index = ((ComboBox)sender).SelectedIndex;
            if (index == -1)
            {
                return;
            }
            if (currentTech != null)
            {
                currentTech.Mode = index + 1;//需要+1对应                
            }
            saveBtn.Enabled = true;
        }

        private void techDgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            saveBtn.Enabled = true;
        }
    }
}
