using System;
using System.Collections.Generic;
using System.Data;
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
        StorageDC storageDC = new StorageDC();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "库存查询";

            if (!IsPostBack)
            {
                StorageSelect();

                List<String> item_name_List = new List<String>();
                List<String> subinventory_List = new List<String>();
                //List<String> frame_name_List = new List<String>();
                item_name_List = storageDC.getAllItem_name();
                subinventory_List = storageDC.getAllSubinventory_name();
                //frame_name_List = storageDC.getAllFrame_name();
                if (item_name_List != null && subinventory_List != null)
                {
                    Item.DataSource = item_name_List;
                    Item.DataBind();
                    Item.Items.Insert(0, "选择料名");
                    Subinventory.DataSource = subinventory_List;
                    Subinventory.DataBind();
                    Subinventory.Items.Insert(0, "选择库别");
                    //Frame_name.DataSource = frame_name_List;
                    //Frame_name.DataBind();
                    //Frame_name.Items.Insert(0, "选择料架");

                }
                else
                {
                    PageUtil.showAlert(this, "无数据载入下拉框！");
                }

            }
        }


        /**
         * 
         *查询库存信息* 
         * 
         ***/
        protected void QueryStorage_Click(object sender, EventArgs e)
        {
            string item_name = Item.SelectedValue;
            string subinventory_name = Subinventory.SelectedValue;
            //判断两个下拉框是否未选择
            if (item_name.Equals("选择料名"))
            {
                item_name = "";
            }
            if (subinventory_name.Equals("选择库别"))
            {
                subinventory_name = "";
            }
            DataSet dataset = storageDC.getStorageBySome(item_name, subinventory_name);
            Storage.DataSource = dataset;
            Storage.DataBind();

        }





        //库存明细表的默认显示（由此可通过总表点击查询按钮出现对应的单身数据）
        public void StorageSelect()
        {
            DataSet ds = new DataSet();
            ds = storageDC.getStorage_DetailAndItem_nameAndFrame_nameAndSubinventory_name();
            if (ds == null)
            {
                PageUtil.showAlert(this, "库存明细表无对应数据！");
            }
            Storage_Detail.DataSource = ds;
            Storage_Detail.DataBind();
        }




        /**
        * 
        * 绑定查询的数据*
        * 
        ***/
        //public void bind()
        //{
        //    string ITEM_NAME = item_name.Value;           
        //    It_Leng(ITEM_NAME, "料号");
        //    string SUBINVENTORY_NAME = subinventory_name.Value;
        //    Sub_Leng(SUBINVENTORY_NAME, "库别");
        //    string DATECODE = datecode.Value;
        //    Sub_Leng(DATECODE, "DateCode");

        //    //查询库存数据
        //    InventoryDC inventoryDC = new InventoryDC();
        //    GridView1.DataSource = inventoryDC.getInverntory(ITEM_NAME, SUBINVENTORY_NAME, DATECODE);
        //    GridView1.DataBind();

        //}

        /**
         * 
         * 将GridView数据导出到EXEC*
         * 
         ***/
        //protected void button2_Click(object sender, EventArgs e)
        //{
        //    toExecl(GridView1);
        //}
        //protected void toExecl(System.Web.UI.WebControls.GridView gv)
        //{

        //    //清除分页
        //    GridView1.AllowPaging = false;
        //    GridView1.AllowSorting = false;
        //    bind (); 

        //    Response.Clear();
        //    Response.Buffer = true;
        //    //设定输出的字符集
        //    Response.Charset = "GB2312";
        //    Response.AppendHeader("Content-Disposition", "attachment;filename=DC查询信息.xls");
        //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        //    //设置导出文件格式
        //    Response.ContentType = "application/ms-excel";
        //    //关闭ViewState
        //    //this.EnableViewState = false;
        //    System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        //    GridView1.RenderControl(oHtmlTextWriter);
        //    //把HTML写回浏览器
        //    Response.Output.Write(oStringWriter.ToString());
        //    Response.Flush();
        //    Response.End();
        //    //恢复分页
        //    //GridView1.AllowPaging = true;
        //    //GridView1.AllowSorting = true;

        //    //GridView1.DataBind();
        //}
        //public override void VerifyRenderingInServerForm(Control control)
        //{

        //}
        /**
         * 
         * 分页
         * 
         ***/
        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex = e.NewPageIndex;

        //    string ITEM_NAME = item_name.Value;
        //    It_Leng(ITEM_NAME, "料号");
        //    string SUBINVENTORY_NAME = subinventory_name.Value;
        //    Sub_Leng(SUBINVENTORY_NAME, "库别");
        //    string DATECODE = datecode.Value;
        //    Sub_Leng(DATECODE, "DateCode");

        //    //查询库存数据
        //    InventoryDC inventoryDC = new InventoryDC();
        //    DataSet ds = new DataSet();
        //    ds = inventoryDC.getInverntory(ITEM_NAME, SUBINVENTORY_NAME, DATECODE);
        //    if (ds == null)
        //    {
        //        if (ITEM_NAME == string.Empty && SUBINVENTORY_NAME == string.Empty && DATECODE == string.Empty)
        //        {
        //            PageUtil.showToast(this, "库存细明表中无任何数据！");
        //        }
        //        else
        //            PageUtil.showAlert(this, "库存细明表中无符合条件的数据！");
        //    }
        //    else
        //    {
        //        GridView1.DataSource = ds;
        //        GridView1.DataBind();
        //    }

        //}
        

    }
}