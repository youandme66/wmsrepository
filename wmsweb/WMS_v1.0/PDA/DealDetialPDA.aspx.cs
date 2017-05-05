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

namespace WMS_v1._0.PDA1
{
    public partial class DealDetialPDA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "交易明细查询";
        }

        /// <summary>
        /// Select查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Select(object sender, EventArgs e)
        {
            try
            {
                string RADIO = "";
                if (radio1.Checked)
                {
                    RADIO = "";
                }
                else if (radio2.Checked)
                {
                    RADIO = radio2.Value;
                }
                else if (radio3.Checked)
                {
                    RADIO = radio3.Value;
                }
                else if (radio4.Checked)
                {
                    RADIO = radio4.Value;
                }
                else if (radio5.Checked)
                {
                    RADIO = radio5.Value;
                }
                else if (radio8.Checked)
                {
                    RADIO = radio8.Value;
                }


                string FIRST_TIME = inpend.Value;
                if (FIRST_TIME == "")
                {
                    FIRST_TIME = "2016-10-22 00:00:00";
                }

                start_time = Convert.ToDateTime(FIRST_TIME);
                string LAST_TIME = inpstart.Value;
                end_time = Convert.ToDateTime(LAST_TIME);
                string ITEM_NAME = item_name.Value;//料号
                string INVOICE_NO = invoice_no.Value;//单据号
                string PO_NO = po_no.Value;//PO单号
                StorageDC storgeDC = new StorageDC();
                DataSet ds = storgeDC.getTransactionBySome(RADIO, start_time, end_time, ITEM_NAME, INVOICE_NO, PO_NO);
                if (ds != null)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    PageUtil.showToast(this, "数据库中没有对应数据");
                }
            }
            catch (Exception e2)
            {
                PageUtil.showToast(this, "查询交易明细失败！");
            }
        }

        //导出excel表格
        protected void export(object sender, EventArgs e)
        {
            outputexcel(GridView1);
        }
        public void outputexcel(System.Web.UI.WebControls.GridView gv)
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
            GridView1.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            Response.Write("<meta http-equiv=Content-Type content=\"text/html;charset=GB2312\">");
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.GridView1.RenderControl(oHtmlTextWriter);
            //这里是有分页的重新绑定可以把所有都导出
            this.GridView1.AllowPaging = false;
            this.GridView1.DataBind();
            Response.Output.Write(oStringWriter.ToString());
            Response.Flush();
            Response.End();

        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex = e.NewPageIndex;
        //    string MEMBER = member.Value;

        //    InventoryDC liaohao = new InventoryDC();
        //    GridView1.DataSource = liaohao.getTransactionByUser(MEMBER);
        //    GridView1.DataBind();
        //}    


        public DateTime end_time { get; set; }

        public DateTime start_time { get; set; }
    }
}