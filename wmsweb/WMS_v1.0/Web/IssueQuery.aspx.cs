using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;
using WMS_v1._0.Util;

namespace WMS_v1._0.Web
{
    public partial class IssueQuery : System.Web.UI.Page
    {
       InvoiceDC invoiceDC = new InvoiceDC();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "领料单查询";

            if (!IsPostBack)
            {
                //DataSet ds_Invoice_no = invoiceDC.getIssueInvoice_no(1);
                //DataSet ds_item_name = invoiceDC.getItem_name(0);

                //if (ds_Invoice_no != null)
                //{
                //    DropDownList_issue_no.DataSource = ds_Invoice_no.Tables[0].DefaultView;
                //    DropDownList_issue_no.DataValueField = "invoice_no";
                //    DropDownList_issue_no.DataTextField = "invoice_no";
                //    DropDownList_issue_no.DataBind();
                //}
                //if (ds_item_name != null)
                //{
                //    DropDownList_item_name.DataSource = ds_item_name.Tables[0].DefaultView;
                //    DropDownList_item_name.DataValueField = "item_name";
                //    DropDownList_item_name.DataTextField = "item_name";
                //    DropDownList_item_name.DataBind();
                //}

                //DropDownList_issue_no.Items.Insert(0, "--选择领料单号--");
                //DropDownList_item_name.Items.Insert(0, "--选择料号--");
            }
        }

        protected void Select(object sender, EventArgs e)
        {
            try
            {
                //string Invoice_no = DropDownList_issue_no.SelectedValue.ToString();
                //string Item_name = DropDownList_item_name.SelectedValue.ToString();
                string Invoice_no = invoice_no.Value;
                string Item_name = item_name.Value;
                string Issue_type = Request.Form["issue_type"];

                //if (Invoice_no == "--选择领料单号--")
                //    Invoice_no = "";
                //if (Item_name == "--选择料号--")
                //    Item_name = "";

                string FIRST_TIME = inpend.Value;
                if (FIRST_TIME == "")
                {
                    FIRST_TIME = "2016-07-01 00:00:00";
                }

                start_time = Convert.ToDateTime(FIRST_TIME);
                string LAST_TIME = inpstart.Value;
                end_time = Convert.ToDateTime(LAST_TIME);

                GridView_header.DataSource = invoiceDC.getIssueHeaderBySome(Invoice_no, Issue_type, Item_name);
                GridView_header.DataBind();

                GridView_line.DataSource = invoiceDC.getIssueLineBySome(Invoice_no, Item_name, Issue_type, start_time, end_time);
                GridView_line.DataBind();

                if (GridView_header.DataSource == null)
                {
                    PageUtil.showToast(this, "无相关领料单头信息");

                }
                if (GridView_line.DataSource == null)
                {
                    PageUtil.showToast(this, "无相关领料单身信息");
                }
                    
            }
            catch (Exception e1)
            {
                PageUtil.showToast(this, "查询失败");
            }
        }

        //导出excel表格
        protected void export(object sender, EventArgs e)
        {
            outputexcel(GridView_header);
        }
        public void outputexcel(System.Web.UI.WebControls.GridView gv)
        {


            Response.Clear();
            //不缓存
            Response.Buffer = false;
            Response.Charset = "GB2312";
            //这里的FileName.xls可以用变量动态替换
            Response.AppendHeader("Content-Disposition", "attachment;filename=退料单.xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //设置输出文件类型为excel文件
            Response.ContentType = "application/ms-excel";
            //以文本形式显示数据
            GridView_header.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            Response.Write("<meta http-equiv=Content-Type content=\"text/html;charset=GB2312\">");
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.GridView_header.RenderControl(oHtmlTextWriter);
            //这里是有分页的重新绑定可以把所有都导出
            this.GridView_header.AllowPaging = false;
            this.GridView_header.DataBind();

            this.GridView_line.RenderControl(oHtmlTextWriter);
            //这里是有分页的重新绑定可以把所有都导出
            this.GridView_line.AllowPaging = false;
            this.GridView_line.DataBind();

            Response.Output.Write(oStringWriter.ToString());
            Response.Flush();
            Response.End();

        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        public DateTime end_time { get; set; }

        public DateTime start_time { get; set; }

        //清除查询结果
        protected void cleanMassage(object sender, EventArgs e)
        {

            GridView_header.DataSource = null;
            GridView_header.DataBind();

            GridView_line.DataSource = null;
            GridView_line.DataBind();

        }

        //打印按钮中的确定操作----将文本框中的值传给打印界面
        protected void transPrint(object sender, EventArgs e)
        {
            //判断搜索框中的领料单号是否存在
            if (string.IsNullOrEmpty(select_text_print.Value) == true)
            {
                Session["select_text_print"] = null;
                PageUtil.showToast(this, "请输入领料单号，再执行此打印操作");
            }
            else
            {
                Session["select_text_print"] = select_text_print.Value;
                //打开打印界面
                Response.Write("<script>window.open('printMaterialRequisition.aspx', 'newwindow')</script>");
            }
        }

        //打印按钮中的取消操作------清除输入框数据
        protected void CleanInsertMessage(object sender, EventArgs e)
        {
            select_text_print.Value = String.Empty;
        }
    }
}