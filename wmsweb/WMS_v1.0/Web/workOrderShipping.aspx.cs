using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.Web
{
    public partial class workOrderShipping : System.Web.UI.Page
    {
        ShipDC shipdc = new ShipDC();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "工单出货";
        }

        protected void searchCustomer(object sender, EventArgs e)
        {
            string Ship_no = ship_no.Value;
            if (String.IsNullOrWhiteSpace(Ship_no))
            {
                PageUtil.showToast(this, "请输入出货单号！");
                return;
            }
            ds = shipdc.getCustomerNameAndIdByAShipNo(Ship_no);
            if (ds == null)
            {
                PageUtil.showToast(this, "该出货单号不存在！");
                return;
            }
            try
            {
                customer_id.Value = ds.Tables[0].Rows[0]["customer_key"].ToString();
                customer_name.Value = ds.Tables[0].Rows[0]["customer_name"].ToString();
                updateWo_noDate(ds.Tables[0].Rows[0]["item_name"].ToString());
                string strstatus = ds.Tables[0].Rows[0]["status"].ToString();
                if (strstatus == "1")
                    status.Value = "已完成";
                else
                    status.Value = "未完成";

            }
            catch
            {
                PageUtil.showToast(this, "该出货单号不存在！");
                return;
            }
            updateData(Ship_no);
        }

        protected void searchPartNo(object sender, EventArgs e)
        {
            searchPartNoAndPickedQty();
        }

        public void searchPartNoAndPickedQty()
        {
            string Ship_no = ship_no.Value;
            string Wo_no = wo_no.Value;
            string part_no1, part_no2;
            if (String.IsNullOrWhiteSpace(Ship_no) || String.IsNullOrWhiteSpace(Wo_no))
            {
                PageUtil.showToast(this, "请输入工单编号！");
                return;
            }
            part_no1 = shipdc.getPartNoByWoNo(Wo_no);
            part_no2 = shipdc.getPartNoByShip_no(Ship_no);
            if (part_no1 == null || part_no2 == null || !part_no1.Equals(part_no2))
            {
                wo_no.Attributes.Add("style", "border: #ff0000  1px   solid;");
                PageUtil.showToast(this, "工单输入错误！");
                return;
            }
            wo_no.Attributes.Add("style", "");
            part_no.Value = part_no1;
            ds = shipdc.getResQtyAndPicQtyByShip_no(Ship_no);
            if (ds.Tables[0].Rows[0]["picked_qty"].ToString() == null)
            {
                ship_qty.Value = "0";
            }
            else
                ship_qty.Value = ds.Tables[0].Rows[0]["picked_qty"].ToString();
            if (ds.Tables[0].Rows[0]["request_qty"].ToString() == null)
            {
                request_qty.Value = "0";
            }
            else
                request_qty.Value = ds.Tables[0].Rows[0]["request_qty"].ToString();
        }


        protected void workOrderShip(object sender, EventArgs e)
        {
            string Ship_no = ship_no.Value;
            string Wo_no = wo_no.Value;
            string Part_no = part_no.Value;
            string Customer_id = customer_id.Value;
            string Customer_name = customer_name.Value;
            int Picked_qty, Ship_qty, Request_qty;
            bool status_temp = false;
            string ship_man;
            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "请登录！");
                return;
            }
            else
                ship_man = Session["LoginName"].ToString();
            if (String.IsNullOrWhiteSpace(Ship_no) || String.IsNullOrWhiteSpace(Wo_no) || String.IsNullOrWhiteSpace(Part_no) || String.IsNullOrWhiteSpace(ship_qty.Value))
            {
                PageUtil.showToast(this, "输入数据不能为空！");
                return;
            }
            try
            {
                Picked_qty = Convert.ToInt32(picked_qty.Value);
            }
            catch
            {
                PageUtil.showToast(this, "出货量输入格式错误！");
                return;
            }
            if ("已完成".Equals(status.Value))
            {
                PageUtil.showToast(this, "该出货单出货已结束！");
                return;
            }
            Request_qty = Convert.ToInt32(request_qty.Value);
            Ship_qty = Convert.ToInt32(ship_qty.Value);
            if (Picked_qty + Ship_qty > Request_qty)
            {
                PageUtil.showToast(this, "数量超出客户所需数量！");
                return;
            }
            if (Picked_qty + Ship_qty == Request_qty)
            {
                status_temp = true;
            }
            List<int> list = shipdc.getTargetQtyByWoNo(Wo_no);
            if (Picked_qty + list[2] > list[1])
            {
                PageUtil.showToast(this, "出货量超出工单入库量！");
                return;
            }
            if (Picked_qty + list[2] > list[0])
            {
                PageUtil.showToast(this, "出货量超出工单开工量！");
                return;
            }
            bool flag = shipdc.workShipping(Ship_no, Wo_no, Part_no, Picked_qty, status_temp, ship_man, DateTime.Now);

            if (flag == true)
            {
                updateData(Ship_no);
                updateWo_noDate(Part_no);
                PageUtil.showToast(this, "出货成功！");
                searchPartNoAndPickedQty();
            }
            else
            {
                PageUtil.showToast(this, "出货失败！");
            }
        }

        protected void cleanMassage(object sender, EventArgs e)
        {
            status.Value = "";
            ship_no.Value = String.Empty;
            wo_no.Value = String.Empty;
            part_no.Value = String.Empty;
            customer_id.Value = String.Empty;
            customer_name.Value = String.Empty;
            picked_qty.Value = String.Empty;
            ship_qty.Value = String.Empty;
            request_qty.Value = String.Empty;
            workOrderShip_Repeater.DataSource = null;
            workOrderShip_Repeater.DataBind();
        }

        public void updateData(string Ship_no)
        {
            Label1.InnerText = "已出货工单：";
            DataSet ds2 = shipdc.getShipMassage(Ship_no);
            workOrderShip_Repeater.DataSource = ds2;
            workOrderShip_Repeater.DataBind();
        }

        public void updateWo_noDate(string part_no)
        {
            Label2.InnerText = "可用工单：";
            WoDC wodc = new WoDC();
            DataSet ds2=wodc.getWo_noBypart_no(part_no);
            enable_wo_no.DataSource = ds2;
            enable_wo_no.DataBind();
        }

    }
}