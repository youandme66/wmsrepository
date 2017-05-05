using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;


namespace WMS_v1._0.Web
{
    public partial class DCSerach : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "库存查询";
        }
        /**
         * 
         * 限定料号长度*
         * 
         ***/
        public void It_Leng(string str, string name)
        {
            if (str.Length > 40)
            {
                PageUtil.showToast(this, "" + name + "输入长度过长！");
                return;
            }
        }

        /**
        * 
        * 限定库别和DateCode长度*
        * 
        ***/
        public void Sub_Leng(string str, string name)
        {
            if (str.Length > 10)
            {
                PageUtil.showToast(this, "" + name + "输入长度过长！");
                return;
            }
        }

        /**
         * 
         *查询库存信息* 
         * 
         ***/
        protected void Button1_Click(object sender, EventArgs e)
        {
            //获取前台数据
            string ITEM_NAME = item_name.Value;
            It_Leng(ITEM_NAME, "料号");
            string SUBINVENTORY_NAME = subinventory_name.Value;
            Sub_Leng (SUBINVENTORY_NAME,"库别");
            string DATECODE = datecode.Value;
            Sub_Leng (DATECODE,"DateCode");
            
            //查询库存数据
            InventoryDC inventoryDC = new InventoryDC();
            GridView1.DataSource = inventoryDC.getInverntory(ITEM_NAME, SUBINVENTORY_NAME, DATECODE);
            GridView1.DataBind();
        }

        /**
         * 
         * 将GridView数据导出到EXEC*
         * 
         ***/
        protected void button2_Click(object sender, EventArgs e)
        {
            toExecl(GridView1);
        }
        protected void toExecl(System.Web.UI.WebControls.GridView gv)
        {
            //清除分页
            GridView1.AllowPaging = false;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            //设定输出的字符集
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=DC查询信息.xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //设置导出文件格式
            Response.ContentType = "application/ms-excel";
            //关闭ViewState
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.GridView1.RenderControl(oHtmlTextWriter);
            //把HTML写回浏览器
            Response.Output.Write(oStringWriter.ToString());
            Response.Flush();
            Response.End();
            //回复分页
            GridView1.AllowPaging = true;
            GridView1.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        } 
    }
}