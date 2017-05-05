using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;
using WMS_v1._0.Util;

namespace WMS_v1._0.PDA
{
    public partial class ShipSettingPDA : System.Web.UI.Page
    {
        ShipDC ship_dc = new ShipDC();

        Ship_linesDC ship_lines_dc = new Ship_linesDC();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "出货设定";
            string ship_man = "";
            try
            {
                ship_man = Session["LoginName"].ToString();
            }
            catch (Exception ex)
            {
                Response.Redirect("LoginPDA.aspx", false);
                return;
            }
        }

        /// <summary>
        /// 生成下一个出货单号
        /// </summary>
        /// <returns></returns>
        protected string nextShip_no()
        {
            ShipDC ship_dc = new ShipDC();
            string lastship_no = "";
            string head = "P";
            string end = "0001";
            DateTime date = DateTime.Now;
            //获取当前的时间
            string time = date.ToString("yyyyMMdd");
            //获取最后一次的ship_no
            ModelShip modelship = ship_dc.getLastShip_no();
            int last_id = ship_dc.getLast_id();//数据库中最后一次存储的ID
            int ship_key;
            if (modelship == null)
                return null;
            else
            {
                ship_key = modelship.Ship_key;
                lastship_no = modelship.Ship_no;
            }
            if (lastship_no == null)
            {
                end = ((last_id + 1) % 10000).ToString("D4");// "0001";
            }
            else
            {
                int s = lastship_no.Length;
                string last_date = lastship_no.Substring(1, 8);//上一条数据的日期
                if (string.Compare(last_date, time, StringComparison.Ordinal) < 0)
                    end = "0001";
                else
                {
                    string last_no = lastship_no.Substring(s - 4, 4);
                    //获取后后四位，转换为数字
                    int last = int.Parse(last_no);
                    //格式化整型，D:十进制，4：字符串长度
                    end = (last_id - ship_key + last + 1).ToString("D4");
                    if (end.Equals("10000"))
                        end = "0001";
                }
            }
            return head + time + end;
        }

        protected bool checkpicked_qty(int ship_key, int num1)
        {
            ModelShip model = ship_dc.getshipbykey(ship_key);
            int temp = model.Request_qty - model.Picked_qty;
            if (temp >= num1)
                return true;
            return false;
        }

        /// <summary>
        /// 更新显示单头信息
        /// </summary>
        /// <param name="ds"></param>
        protected void updateTop_GridView(DataSet ds)
        {
            if (ds == null)
            {
                ds = ship_dc.searchShipByCustomer_name("");
            }
            Top_GridView.DataSource = ds;
            Top_GridView.DataBind();
        }

        /// <summary>
        /// 新增一条出货单单头信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Top_Insert(object sender, EventArgs e)
        {
            string strInput_customer = Request.Form["insert_customer_name_c"];

            strInput_customer = ship_dc.getCustomerIdByName(strInput_customer);

            string ship_no = top_insert_ship_no.Value.Trim();
            if (string.IsNullOrWhiteSpace(ship_no))
            {
                PageUtil.showToast(this, "出货单号不能为空!");
                return;
            }
            if (!ship_dc.queryShip_no(ship_no) && !ship_no.Equals(nextShip_no()))
            {
                PageUtil.showToast(this, "请输入正确的出货单号!");
                return;
            }

            string item_name = Request.Form["insert_item_name_c"];
            PnDC pnDc = new PnDC();
            int item_id = pnDc.getItem_idByItem_name(item_name);
            if (item_id == -1)
            {
                PageUtil.showToast(this, "该料号不存在，请输入正确的料号!");
                return;
            }
            int request_qty = 0;
            int picked_qty = 0;
            try
            {
                request_qty = Int32.Parse(top_insert_request_qty.Value.Trim());
                if (request_qty < 0)
                {
                    PageUtil.showToast(this, "请输入正确的需求量!");
                    return;
                }
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "请输入正确的需求量!");
                return;
            }
            try
            {
                picked_qty = Int32.Parse(top_insert_picked_qty.Value.Trim());
                if (picked_qty < 0)
                {
                    PageUtil.showToast(this, "请输入正确的需求量!");
                    return;
                }
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "请输入正确的出货量!");
                return;
            }
            int customer_id = 0;
            try
            {
                customer_id = Int32.Parse(strInput_customer);
            }
            catch (Exception exception)
            {
                PageUtil.showToast(this, "客户信息异常!");
                return;
            }
            DataSet ds = ship_dc.insertAndReturnShip(ship_no, customer_id, item_id, request_qty, picked_qty);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                top_insert_ship_no.Value = "";
                top_insert_request_qty.Value = "";
                PageUtil.showToast(this, "添加成功!");

            }
            else
            {
                PageUtil.showToast(this, "添加出货单单头信息失败!");
            }

            updateTop_GridView(ds);
        }

        /// <summary>
        /// 删除出货单单头一条信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Top_Delete(object sender, EventArgs e)
        {

            string strDelete_id = delete_id.Value;

            int numDelete_id = int.Parse(strDelete_id);

            try
            {
                DataSet ds = ship_dc.deleteShipById(numDelete_id);
                updateTop_GridView(ds);
                PageUtil.showToast(this, "删除成功!");
            }
            catch (Exception)
            {

                PageUtil.showToast(this, "删除失败!");
            }

        }

        /// <summary>
        /// 编辑出货单单头信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Top_Edit(object sender, EventArgs e)
        {

            string strShip_key = edit_ship_key.Value;

            string strEdit_customer = edit_customer.Value;

            string strEdit_part_no = edit_part_no.Value;

            string strEdit_request_qty = Top_request_qty.Value;

            string strEdit_picked_qty = Top_picked_qty.Value;

            string ship_no = edit_ship_no.Value;

            int ship_key = 0;
            int request_qty = 0;
            int picked_qty = 0;
            int customer_id = 0;
            int part_no = 0;
            try
            {
                ship_key = int.Parse(strShip_key);
                CustomersDC customerDc = new CustomersDC();
                customer_id = customerDc.getCustomeridByname(strEdit_customer);
                if (customer_id == -1)
                {
                    PageUtil.showToast(this, "该客户不存在，请检查数据!");
                    return;
                }
                request_qty = int.Parse(strEdit_request_qty);
                if (request_qty < 0)
                {
                    PageUtil.showToast(this, "数据格式错误，请输入数字！");
                    return;
                }
                picked_qty = int.Parse(strEdit_picked_qty);
                if (picked_qty < 0)
                {
                    PageUtil.showToast(this, "数据格式错误，请输入数字！");
                    return;
                }
                else if (picked_qty > 0)
                {
                    PageUtil.showToast(this, "该出货单已进行过出货操作，暂不支持修改！");
                    return;
                }
                PnDC pndc = new PnDC();
                part_no = pndc.getItem_idByItem_name(strEdit_part_no);
                if (part_no == -1)
                {
                    PageUtil.showToast(this, "该料号不存在!");
                    return;
                }
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "数据异常!");
                return;
            }
            Transaction_operationDC trandc = new Transaction_operationDC();
            DataSet tds = trandc.getTransactionBySome("shipping", strEdit_part_no, ship_no);
            if (tds == null)
            {
                PageUtil.showToast(this, "数据异常!");
                return;
            }
            else if (tds.Tables[0].Rows.Count > 0)
            {
                PageUtil.showToast(this, "该出货单已进行过出货操作，暂不支持修改！");
                return;
            }
            try
            {
                DataSet ds = ship_dc.updateShip(ship_key, customer_id, part_no, request_qty, picked_qty);
                if (ds != null)
                {
                    updateTop_GridView(ds);
                    PageUtil.showToast(this, "修改成功!");
                }
                else
                {
                    PageUtil.showToast(this, "修改失败!");
                }
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "修改失败!");
            }

        }

        /// <summary>
        /// 按照客户ID查找出货单单头信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Top_Select(object sender, EventArgs e)
        {

            string customer_name = Request.Form["customer_name_c"];

            if (customer_name == "ALL")
            {
                customer_name = "";
            }

            DataSet ds = ship_dc.searchShipByCustomer_name(customer_name);

            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                PageUtil.showToast(this, "无相关信息!");
                return;
            }
            try
            {
                updateTop_GridView(ds);
            }
            catch (Exception ex2)
            {
                PageUtil.showToast(this, "数据库无信息，请添加数据后再进行操作");
            }

        }

        //换页
        protected void Top_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Top_GridView.PageIndex = e.NewPageIndex;
            updateTop_GridView(null);
        }

    }
}