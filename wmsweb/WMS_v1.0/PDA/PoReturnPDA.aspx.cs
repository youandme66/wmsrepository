using System;
using System.Data;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.PDA
{
    /// <summary>
    /// 作者：伍雪松
    /// 界面：PO退回
    /// 功能：查询可进行PO退回操作的数据，进行PO退回操作
    /// </summary>
    public partial class PoReturnPDA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "PO退回";
            if (Session["LoginId"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }

            if (!IsPostBack)
            {
                SubinventoryDC subDC = new SubinventoryDC();
                DataSet ds1 = subDC.getAllUsedSubinventory_name();
                if (ds1 != null)
                {
                    DropDownList1.DataSource = ds1.Tables[0].DefaultView;
                    DropDownList1.DataValueField = "subinventory_name";
                    DropDownList1.DataBind();
                }
                //else
                //    PageUtil.showAlert(this, "库别载入出错！");
                DropDownList1.Items.Insert(0, "--选择库别--");
                DropDownList2.Items.Insert(0, "--选择区域--");
            }
        }

        //选择
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            receipt_no_id.Value = GridView1.Rows[row.RowIndex].Cells[2].Text;
        }

        //查询
        protected void select_click(object sender, EventArgs e)
        {
            string po_no = id_po_NO.Value.Trim();
            string item_name = id_item_Name.Value.Trim();
            string receipt_no = id_receipt_NO.Value.Trim();

            Receive_mtlDC receive_mtlDC = new Receive_mtlDC();//po退回的DataCenter对象
            DataSet ds = receive_mtlDC.getSomeFieldsByReceipt_noAndPO_noAndItem_name(receipt_no, po_no, item_name);

            if (ds == null)
            {
                PageUtil.showToast(this, "未查询到数据！");
                id_receipt_NO.Value = "";
                id_item_Name.Value = "";
                id_po_NO.Value = "";
                return;
            }
            else
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        dt.Rows[i]["rcv_qty"] = int.Parse(dt.Rows[i]["rcv_qty"].ToString()) - int.Parse(dt.Rows[i]["return_qty"].ToString()) - int.Parse(dt.Rows[i]["deliver_qty"].ToString());
                    }
                    catch (Exception ex)
                    {
                        //PageUtil.showToast(this, "数据异常");
                        Console.Write(ex.Source);
                    }
                }
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }

        //退回操作
        protected void return_click(object sender, EventArgs e)
        {
            string return_user = null, po_no = null, item_name = null, receipt_no = null, vendor_key = null;
            int rcv_qty = 0, return_qty = 0, deliver_qty = 0;
            int line_num = 0, required_qty = 0, can_return = 0;
            try
            {
                //操作人员姓名
                return_user = Session["LoginName"].ToString();
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "请登录后在操作！");
                return;
            }
            receipt_no = receipt_no_id.Value.Trim();
            if (string.IsNullOrEmpty(receipt_no))
            {
                PageUtil.showToast(this, "请输入暂收单号");
                return;
            }
            string return_sub = DropDownList1.SelectedValue.ToString();
            string return_region = DropDownList2.SelectedValue.ToString();

            if (return_sub.Equals("--选择库别--") || return_region.Equals("--选择区域--"))
            {
                PageUtil.showToast(this, "请选择库别和区域！");
                return;
            }

            Receive_mtlDC receive_mtlDC = new Receive_mtlDC();//po退回的DataCenter对象
            DataSet ds = receive_mtlDC.getSomeFieldsByReceipt_no(receipt_no);
            if (ds != null)
            {
                po_no = ds.Tables[0].Rows[0]["po_no"].ToString();
                item_name = ds.Tables[0].Rows[0]["item_name"].ToString();
                vendor_key = ds.Tables[0].Rows[0]["VENDOR_CODE"].ToString();
                try
                {
                    line_num = int.Parse(ds.Tables[0].Rows[0]["line_num"].ToString());
                }
                catch (Exception ex)
                {
                    PageUtil.showToast(this, "Line Num数据异常！");
                    Console.Write(ex.Message);
                    line_num = 0;
                }
                try
                {
                    //暂收量
                    rcv_qty = int.Parse(ds.Tables[0].Rows[0]["rcv_qty"].ToString());
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    PageUtil.showToast(this, "暂收量数据异常！");
                    rcv_qty = 0;
                }
                try
                {
                    //退回量
                    return_qty = int.Parse(ds.Tables[0].Rows[0]["return_qty"].ToString());
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    PageUtil.showToast(this, "退回量数据异常！");
                    return_qty = 0;
                }
                try
                {
                    //入库量
                    deliver_qty = int.Parse(ds.Tables[0].Rows[0]["deliver_qty"].ToString());
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    PageUtil.showToast(this, "入库量数据异常！");
                    deliver_qty = 0;
                }
                can_return = rcv_qty - return_qty - deliver_qty;
            }
            else
            {
                PageUtil.showToast(this, "未找到该暂收单号，请检查暂收单号是否正确！");
                return;
            }
            try
            {
                required_qty = int.Parse(return_num_id.Value.Trim());
                if (required_qty <= 0)
                {
                    PageUtil.showToast(this, "退回量应大于0");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                PageUtil.showToast(this, "请输入正确的数量");
                return_num_id.Value = "";
                return;
            }
            if (required_qty > can_return)
            {
                PageUtil.showToast(this, "退料量不得大于可退量!");
                return_num_id.Value = "";
                return;
            }
            //else if (can_return == required_qty)//退料量等于可退量
            //{
            //    //删除暂收表中的数据，修改po退回总表，po退回明细表，交易表
            //    if (receive_mtlDC.poReturn_second(receipt_no, vendor_key, line_num, po_no, item_name, required_qty, return_sub, return_region, return_user, DateTime.Now, DateTime.Now))
            //    {
            //            PageUtil.showToast(this, "PO退回成功！");
            //            select_click(sender, e);
            //    }
            //    else
            //    {
            //        PageUtil.showToast(this, "PO退回失败，请重新操作！");
            //    }
            //}
            //退料量小于可退量
            else
            {
                //修改暂收表中的数据，，po退回总表，po退回明细表，交易表
                if (receive_mtlDC.poReturn_first(receipt_no, vendor_key, line_num, po_no, item_name, required_qty, return_sub, return_region, return_user, DateTime.Now, DateTime.Now))
                {
                    PageUtil.showToast(this, "PO退回成功！");
                    select_click(sender, e);
                }
                else
                {
                    PageUtil.showToast(this, "PO退回失败，请重新操作！");
                }
            }
        }

        //获取GridView内容
        //private DataTable GetGridViewData(DataTable table)
        //{
        //    table.Columns.Add(new DataColumn("Po_no"));
        //    table.Columns.Add(new DataColumn("Line_num"));
        //    table.Columns.Add(new DataColumn("Item_name"));
        //    table.Columns.Add(new DataColumn("RECEIPT_NO"));
        //    table.Columns.Add(new DataColumn("Rcv_qty"));
        //    table.Columns.Add(new DataColumn("RETURN_qty"));
        //    table.Columns.Add(new DataColumn("VENDOR_CODE"));
        //    foreach (GridViewRow row in GridView1.Rows)
        //    {
        //        DataRow sourseRow = table.NewRow();
        //        sourseRow["Po_no"] = row.Cells[0].Text;
        //        sourseRow["Item_name"] = row.Cells[1].Text;
        //        sourseRow["RECEIPT_NO"] = row.Cells[2].Text;
        //        sourseRow["Line_num"] = row.Cells[3].Text;
        //        sourseRow["Rcv_qty"] = row.Cells[4].Text;
        //        sourseRow["RETURN_qty"] = row.Cells[5].Text;
        //        sourseRow["VENDOR_CODE"] = row.Cells[6].Text;
        //        table.Rows.Add(sourseRow);
        //    }
        //    return table;
        //}
        //联动

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            RegionDC regionDC = new RegionDC();
            string text = DropDownList1.SelectedValue.ToString();
            DataSet ds2 = regionDC.getRegion_nameBySub_name(text);
            if (ds2 != null)
            {
                DropDownList2.DataSource = ds2.Tables[0].DefaultView;
                DropDownList2.DataValueField = "region_key";
                DropDownList2.DataTextField = "region_name";
                DropDownList2.DataBind();
            }
            //else
            //    PageUtil.showAlert(this, "区域载入出错！");
            DropDownList2.Items.Insert(0, "--选择区域--");
        }
        //分页
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string po_no = id_po_NO.Value.Trim();
            string item_name = id_item_Name.Value.Trim();
            string receipt_no = id_receipt_NO.Value.Trim();

            GridView1.PageIndex = e.NewPageIndex;

            Receive_mtlDC receive_mtlDC = new Receive_mtlDC();//po退回的DataCenter对象
            DataSet ds = receive_mtlDC.getSomeFieldsByReceipt_noAndPO_noAndItem_name(receipt_no, po_no, item_name);

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

    }
}