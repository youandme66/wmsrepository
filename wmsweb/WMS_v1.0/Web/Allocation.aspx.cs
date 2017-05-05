using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;
using WMS_v1._0.Util;

namespace WMS_v1._0.Web
{
    public partial class Allocation : System.Web.UI.Page
    {
        Exchange_headerDC exchanged_headerDC = new Exchange_headerDC();
        Exchange_lineDC exchanged_lineDC = new Exchange_lineDC();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "调拨单作业";
        }
        protected void Insert(object sender, EventArgs e)
        {
            //将前台传的数据进行转换
            string INVOICE_NO = invoice_no.Value;
            string ITEM_NAME = item_name.Value;
            string EXCHANGED = required_qty.Value;

            //调拨单号是否为空
            if (INVOICE_NO == string.Empty)
            {
                PageUtil.showAlert(this, "调拨单号不能为空！");
                return;
            }

            //判断调拨单号，数量是否为空，不为空时，是否是数字
            if (!( (Regex.IsMatch(EXCHANGED, @"\d+") || EXCHANGED.Length == 0)))
            {
                PageUtil.showAlert(this, "调拨单号，数量中只能输入数字！");
                return;
            }
            int EXCHANGED_QTY = -1;
            if (EXCHANGED.Length != 0)
            {
                EXCHANGED_QTY = int.Parse(EXCHANGED);
            }

            //插入数据
            Exchange_headerDC exchanged_headerDC = new Exchange_headerDC();
            exchanged_headerDC.CommitAction(INVOICE_NO, ITEM_NAME, EXCHANGED_QTY);
            //插入成功提示
            PageUtil.showAlert(this, "插入成功！");
        }
        protected void Select(object sender, EventArgs e)
        {
            //将前台传的数据进行转换
            string INVOICE_NO = invoice_no1.Value;
            //判断查询输入的调拨单号是否为空
            if (String.IsNullOrEmpty(INVOICE_NO))
            {
                Response.Write("<script>alert('调拨单号不能为空')</script>");
            }
            else
            {

                    //查询出主表信息
                    List<ModelExchange_header> list = new List<ModelExchange_header>();
                    list = exchanged_headerDC.getExchange_headerByINVOICE_NO(INVOICE_NO);
                    //数据绑定
                    GridView1.DataSource = list;
                    GridView1.DataBind();

                    //查询出从表信息
                    List<ModelExchange_line> list2 = new List<ModelExchange_line>();
                    list2 = exchanged_lineDC.getExchange_lineByInvoice_no(INVOICE_NO);
                    //数据绑定
                    GridView2.DataSource = list2;
                    GridView2.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //将前台传的数据进行转换
            string INVOICE_NO = invoice_no1.Value;

            GridView1.PageIndex = e.NewPageIndex;
            //查询出主表信息
            List<ModelExchange_header> list = new List<ModelExchange_header>();
            list = exchanged_headerDC.getExchange_headerByINVOICE_NO(INVOICE_NO);
            //数据绑定
            GridView1.DataSource = list;
            GridView1.DataBind();
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {


            GridView2.PageIndex = e.NewPageIndex;
            //将前台传的数据进行转换
            string INVOICE_NO = invoice_no1.Value;

            //查询出从表信息
            List<ModelExchange_line> list2 = new List<ModelExchange_line>();
            list2 = exchanged_lineDC.getExchange_lineByInvoice_no(INVOICE_NO);
            //数据绑定
            GridView2.DataSource = list2;
            GridView2.DataBind();
        }

        /**
        * 
        * 清除查询区域输入的数据*
        * 
        * **/
        protected void Clear(object sender, EventArgs e)
        {
            invoice_no.Value = String.Empty;
            item_name.Value = String.Empty;
            required_qty.Value = String.Empty;

        }
        /*
         * 退出到主页面
        */
        protected void Back(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

    }
}