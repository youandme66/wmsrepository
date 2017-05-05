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
    public partial class printMaterialRequisition : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //判断领料单号是否存在，将根据领料单号进行一下所有操作
            if (Session["select_text_print"] == null)
                PageUtil.showAlert(this, "请输入领料单号，再执行此打印操作");
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
            InvoiceDC invoiceDC = new InvoiceDC();
            try
            {
                //绑定其他数据
                DataSet dataset_header = new DataSet();
                dataset_header = invoiceDC.getIssueHeaderBySome(select_text.Value, "", "");
                flex_value.Value = dataset_header.Tables[0].Rows[0]["flex_value"].ToString();
                description.Value = dataset_header.Tables[0].Rows[0]["description"].ToString();
                remark.Value = dataset_header.Tables[0].Rows[0]["remark"].ToString();
                issue_type.Value = dataset_header.Tables[0].Rows[0]["issue_type"].ToString();
                status.Value = dataset_header.Tables[0].Rows[0]["status"].ToString();


                //绑定Repeater中数据
                DataSet dataset_line = new DataSet();
                dataset_line = invoiceDC.getIssueLineBySome(select_text.Value, "", "", Convert.ToDateTime("2016-07-01 00:00:00"), DateTime.Now);

                printMaterialRequisitionRepeater.DataSource = dataset_line;
                printMaterialRequisitionRepeater.DataBind();
            }
            catch (Exception e)
            {
                PageUtil.showToast(this, "数据库中没有对应数据，请重新输入领料单号");
            }
        }
    }
}