using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.Model;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.PDA
{
    public partial class materialRequisitionOperationPDA : System.Web.UI.Page
    {
        List<ModelMtl_issue_line> list=new List<ModelMtl_issue_line>();
        IssueHeaderDC isuline = new IssueHeaderDC();
        IssuelineDC issuli=new IssuelineDC();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "领料单作业";
        }

        protected void search_issue(object sender, EventArgs e)
        {
            string invoice_no = Request.Form["issue_no"]; 
            if (invoice_no == string.Empty)
            {
                PageUtil.showToast(this, "领料单号输入不能为空！");
                return;
            }
            issue.DataSource = isuline.getIssueByinvoice_no(invoice_no);
            issue.DataBind();
        }

        protected void update_issued_qty(object sender, EventArgs e)
        {
            string item_name=item_name_update.Value;
            int issued_qty;
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
            bool flag = issuli.update_issue_line(item_name, issued_qty);
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
            int issue_line_id =Convert.ToInt32(issue_line_id_Delet.Value);
            bool flag = issuli.delete_issue_line(issue_line_id);
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
            string frame_key = frame_key_debit.Value;
            string issued_sub_key = issued_sub_key_debit.Value;
            bool flag =true;// isuline.DebitAction(item_name, issued_qty, frame_key, issued_sub_key, DateTime.Now);
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
            outputexcel(issue);
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
            this.issue.RenderControl(oHtmlTextWriter);
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