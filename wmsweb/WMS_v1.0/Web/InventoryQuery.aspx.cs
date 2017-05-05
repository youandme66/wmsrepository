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
    public partial class InventoryQuery : System.Web.UI.Page
    {
        InventoryDC inventoryDC = new InventoryDC();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "库存查询";
        }


        protected void Select(object sender, EventArgs e)
        {
            try
            {
                string Subinventory_name = subinventory_name.Value;
                string Item_name = item_name.Value;

                GridView_header.DataSource = inventoryDC.getITEMS_ONHAND_QTY_DETAIL(Item_name, Subinventory_name);
                GridView_line.DataSource = inventoryDC.getMaterial_io(Item_name, Subinventory_name);

                if (GridView_header.DataSource == null)
                {
                    PageUtil.showToast(this, "无相关库存总表信息");

                }
                GridView_header.DataBind();

                if (GridView_line.DataSource == null)
                {
                    PageUtil.showToast(this, "无相关库存明细表信息");
                }
                GridView_line.DataBind();
            }
            catch (Exception e1)
            {
                PageUtil.showToast(this, "查询失败");
            }
        }

        //清除查询结果
        protected void cleanMassage(object sender, EventArgs e)
        {

            GridView_header.DataSource = null;
            GridView_header.DataBind();

            GridView_line.DataSource = null;
            GridView_line.DataBind();

        }
    }
}