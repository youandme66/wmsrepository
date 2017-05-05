using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;
using WMS_v1._0.Util;

namespace WMS_v1._0.Web
{
    public partial class printSinglePreparation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断備料單號是否存在，将根据備料單號进行一下所有操作
            if (Session["select_text_print"] == null)
                PageUtil.showAlert(this, "请输入備料單號，再执行此打印操作");
            else
            {
                select_text.Value = Session["select_text_print"].ToString();
                DataView();
                PageUtil.printPage(this);
            }
        }

        //显示打印页面中的所有数据
        protected void DataView()
        {
            Pickup_mtlDC pickup_mtlDC = new Pickup_mtlDC();
     
            try
            {
                //绑定Repeater中数据
                DataSet dataset = new DataSet();
                dataset = pickup_mtlDC.getSomeBySimulate_id(int.Parse(select_text.Value));

                printSinglePreparationRepeater.DataSource = dataset;
                printSinglePreparationRepeater.DataBind();
            }
            catch (Exception e)
            {
                PageUtil.showToast(this, "数据库中没有对应数据，请重新输入備料單号");
            }
        }
    }
}