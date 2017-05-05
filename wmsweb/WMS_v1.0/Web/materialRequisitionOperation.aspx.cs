using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.Model;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;
namespace WMS_v1._0.Web
{
    public partial class materialRequisitionOperation : System.Web.UI.Page
    {
        List<ModelMtl_issue_line> list = new List<ModelMtl_issue_line>();
        List<ModelItems_onhand_qty_detail> list2 = new List<ModelItems_onhand_qty_detail>();
        List<ModelMaterial_io> list3 = new List<ModelMaterial_io>();
        IssueHeaderDC isuline = new IssueHeaderDC();
        IssuelineDC issuli = new IssuelineDC();
        Items_onhand_qty_detailDC items_detail = new Items_onhand_qty_detailDC();
        Material_ioDC material = new Material_ioDC();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "领料单作业";
        }

        protected void cleanMassage(object sender, EventArgs e)
        {
            issue.DataSource = null;
            issue.DataBind();
            issue2.DataSource = null;
            issue2.DataBind();
        }

        protected void search_issue(object sender, EventArgs e)
        {
            string invoice_no = Request.Form["issue_no"];
            issue_no_search.Value = invoice_no;
            issue.DataSource = isuline.getIssueByinvoice_no(invoice_no);
            issue.DataBind();
            //隐藏的repeater用来导出表格
            issue2.DataSource = isuline.getIssueByinvoice_no(invoice_no);
            issue2.DataBind();
        }

        protected void update_issued_qty(object sender, EventArgs e)
        {
            string item_name = item_name_update.Value;
            int issued_qty;
            string invoice_no = issue_no_search.Value;
            if (item_name == string.Empty)
            {
                PageUtil.showToast(this, "料号输入不能为空！");
                return;
            }
            try
            {
                issued_qty = Convert.ToInt32(issued_qty_update.Value);
            }
            catch
            {
                PageUtil.showToast(this, "领料量输入格式错误！");
                return;
            }
            int issue_line_id = Convert.ToInt32(issue_line_id_update.Value);
            int required_qty2 = Convert.ToInt32(REQUIRED_QTY2.Value);
            if (required_qty2 < issued_qty)
            {
                PageUtil.showToast(this, "领料量不得大于需求量！");
                return;
            }
            if (issued_qty <= 0)
            {
                PageUtil.showToast(this, "领料量大于0！");
                return;
            }
            string status = isuline.getIssueStatus(issue_line_id);
            if (status == "Y")
            {
                PageUtil.showToast(this, "该条领料数据已扣账！无法更新");
                return;
            }
            bool flag = issuli.update_issue(issue_line_id, item_name, issued_qty);
            issue.DataSource = isuline.getIssueByinvoice_no(invoice_no);
            issue.DataBind();
            issue2.DataSource = isuline.getIssueByinvoice_no(invoice_no);
            issue2.DataBind();
            if (flag == true)
            {
                PageUtil.showToast(this, "更新成功！");
                return;
            }
            else
            {
                PageUtil.showToast(this, "更新失败！");
                return;
            }
            
        }

        protected void delete_issued_qty(object sender, EventArgs e)
        {
            string invoice_no = issue_no_search.Value;
            int issue_line_id = Convert.ToInt32(issue_line_id_Delet.Value);
            bool flag = issuli.delete_issue_line(issue_line_id);
            issue.DataSource = isuline.getIssueByinvoice_no(invoice_no);
            issue.DataBind();
            issue2.DataSource = isuline.getIssueByinvoice_no(invoice_no);
            issue2.DataBind();
            if (flag == true)
            {
                PageUtil.showToast(this, "删除成功！");
                return;
            }
            else
            {
                PageUtil.showToast(this, "删除失败！");
                return;
            }
           
        }

        protected void Debit_action(object sender, EventArgs e)
        {
            string item_name = item_name_debit.Value;
            int issued_qty = Convert.ToInt32(issued_qty_debit.Value);
            string frame_name = frame_key_debit.Value;
            string issued_sub_key = issued_sub_key_debit.Value;
            int issue_line_id = Convert.ToInt32(issue_line_id_debit.Value);
            string status = isuline.getIssueStatus(issue_line_id);
            int required_qty2 = Convert.ToInt32(REQUIRED_QTY2.Value);
            string invoice_no = issue_no_search.Value;
            if (status == "Y")
            {
                PageUtil.showToast(this, "该条领料数据已扣账！请重新选择");
                return;
            }
            if (issued_qty <= 0)
            {
                PageUtil.showToast(this, "领料量大于0！");
                return;
            }
            if (required_qty2 < issued_qty)
            {
                PageUtil.showToast(this, "领料量不得大于需求量！");
                return;
            }
            list3 = material.getItems_onhand_qty_detailByITEM_NAMEandSubinventoryandFrame_key(item_name, issued_sub_key, frame_name);
            try
            {
                int qty =  Convert.ToInt32(list3[0].Onhand_qty);
                if (qty <= 0)
                {
                    PageUtil.showToast(this, "该料号没有料！");
                    return;
                }
            }
            catch
            {
                PageUtil.showToast(this, "该料号没有料！");
                return;
            }
            bool flag = isuline.DebitAction(item_name, issued_qty, frame_name, issued_sub_key, DateTime.Now, issue_line_id);
            issue.DataSource = isuline.getIssueByinvoice_no(invoice_no);
            issue.DataBind();
            issue2.DataSource = isuline.getIssueByinvoice_no(invoice_no);
            issue2.DataBind();
            if (flag == true)
            {
                PageUtil.showToast(this, "扣账成功！");
                return;
            }
            else
            {
                PageUtil.showToast(this, "扣账失败！");
                return;
            }
            
        }

        //导出excel表格
        protected void export(object sender, EventArgs e)
        {
            outputexcel(issue2);
        }
        public void outputexcel(System.Web.UI.WebControls.Repeater rt)
        {
            Response.Clear();
            //不缓存
            Response.Buffer = false;
            Response.Charset = "GB2312";
            //这里的FileName.xls可以用变量动态替换
            Response.AppendHeader("Content-Disposition", "attachment;filename=交易查询.xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //设置输出文件类型为excel文件
            Response.ContentType = "application/ms-excel";
            //以文本形式显示数据

            Response.Write("<meta http-equiv=Content-Type content=\"text/html;charset=GB2312\">");
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.issue2.RenderControl(oHtmlTextWriter);
            //这里是有分页的重新绑定可以把所有都导出
            Response.Output.Write(oStringWriter.ToString());
            Response.Flush();
            Response.End();

        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

    }
}