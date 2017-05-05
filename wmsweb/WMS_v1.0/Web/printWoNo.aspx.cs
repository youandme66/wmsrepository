using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.Util;
using System.Data;
using WMS_v1._0.DataCenter;


namespace WMS_v1._0.Web
{
    public partial class printWoNo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断退料單號是否存在，将根据退料單號进行一下所有操作
            if (Session["select_text_print_wo_no"] == null)
                PageUtil.showAlert(this, "请输入模拟單號，再执行此打印操作");
            else
            {
                select_text.Value = Session["select_text_print_wo_no"].ToString();
                DataView();
                PageUtil.printPage(this);
            }
        }
        //显示打印页面中的所有数据
        protected void DataView()
        {
            WoDC dc = new WoDC();
            try
            {
                //绑定Repeater中数据
                DataSet dataset_line = new DataSet();
                dataset_line = dc.getWOBySimulate_id(select_text.Value);

                printSinglePreparationRepeater.DataSource = dataset_line;
                printSinglePreparationRepeater.DataBind();
            }
            catch (Exception e)
            {
                PageUtil.showToast(this, "数据库中没有对应数据，请重新输入领料单号");
            }
        }

    }
}