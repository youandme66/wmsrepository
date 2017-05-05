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
    public partial class ExchangeWork : System.Web.UI.Page
    {
        InvoiceDC invoiceDC = new InvoiceDC();
        string user;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "调拨单作业";
            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }
            else
                user = Session["LoginName"].ToString();

            if (!IsPostBack)
            {
                DataSet ds_Invoice_no = invoiceDC.getExchangeInvoice_no();
              
                if (ds_Invoice_no != null)
                {
                    DropDownList_invoice_no.DataSource = ds_Invoice_no.Tables[0].DefaultView;
                    DropDownList_invoice_no.DataValueField = "invoice_no";
                    DropDownList_invoice_no.DataTextField = "invoice_no";
                    DropDownList_invoice_no.DataBind();
                }           
                DropDownList_invoice_no.Items.Insert(0, "--选择调拨单号--");
               
            }
        }

        //清除查询结果
        protected void cleanMassage(object sender, EventArgs e)
        {

            ExchangeHeaderReater.DataSource = null;
            ExchangeHeaderReater.DataBind();

            ExchangeLineReater.DataSource = null;
            ExchangeLineReater.DataBind();
        }

        //查询单头单身
        protected void search(object sender, EventArgs e)
        {
            try
            {
                string Invoice_no = DropDownList_invoice_no.SelectedValue.ToString();

                string first_time = "2016-07-01 00:00:00";
                DateTime start_time = Convert.ToDateTime(first_time);
                DateTime end_time = DateTime.Now;

                if (Invoice_no == "--选择调拨单号--")
                    Invoice_no = "";

                ExchangeHeaderReater.DataSource = invoiceDC.getExchangeHeaderBySome(Invoice_no,"");
                ExchangeLineReater.DataSource = invoiceDC.getExchangeLineBySome(Invoice_no, "", start_time, end_time);

                if (ExchangeHeaderReater.DataSource == null)
                {
                    PageUtil.showToast(this, "无相关调拨单头信息");

                }
                ExchangeHeaderReater.DataBind();

                if (ExchangeLineReater.DataSource == null)
                {
                    PageUtil.showToast(this, "无相关调拨单身信息");
                }
                ExchangeLineReater.DataBind();
            }
            catch (Exception e1)
            {
                PageUtil.showToast(this, "查询失败");
            }
        }

        //确定扣账操作
        protected void Debit_action(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(update_man.Value) == false)
                {
                    PageUtil.showToast(this, "该条调拨数据已扣账！请重新选择");
                    return;
                }


                //JS通过查询结果，绑定数据

                int Exchange_line_id_debit = int.Parse(exchange_line_id_debit.Value);
                string In_subinventory = in_subinventory.Value;
                string Out_subinventory = out_subinventory.Value;
                string Item_name = item_name.Value;
                int Exchanged_qty = int.Parse(exchanged_qty.Value);             
                //string Out_frame_key = out_frame_key.Value;
                //string In_frame_key = in_frame_key.Value;

              

                //用户手动输入数据
                string Datecode_debit = datecode.Value;
                int Exchanged_qty_debit = int.Parse(exchanged_qty_debit.Value);
                string Out_frame_key = out_frame_key.Value;
                string In_frame_key = in_frame_key.Value;



                if (Exchanged_qty_debit < 0)
                {
                    PageUtil.showToast(this, "调拨量应大于0");
                    return;
                }
                //检验实际退回量是否大于申请退料量
                if (Exchanged_qty_debit != Exchanged_qty)
                {
                    PageUtil.showToast(this, "调拨数量应等于申请调拨量");
                    return;
                }

                if (invoiceDC.getSubinventoryByFrame(Out_frame_key).Tables[0].Rows[0]["subinventory_name"].ToString() != Out_subinventory)
                {
                    PageUtil.showToast(this, "请重新输入调出料架，该料架不属于该调出库别下");
                    return;
                }
                if (invoiceDC.getSubinventoryByFrame(In_frame_key).Tables[0].Rows[0]["subinventory_name"].ToString() != In_subinventory)
                {
                    PageUtil.showToast(this, "请重新输入调入料架，该料架不属于该调入库别下");
                    return;
                }


                //扣账
                int flag = invoiceDC.ExchangeDebitAction(DateTime.Now, user, Exchange_line_id_debit, Exchanged_qty_debit, In_subinventory, Out_subinventory, Item_name, Datecode_debit, Out_frame_key, In_frame_key);
                //扣账成功时
                if (flag == 1)
                {
                    PageUtil.showToast(this, "扣账成功！");

                    ExchangeHeaderReater.DataSource = null;
                    ExchangeHeaderReater.DataBind();

                    ExchangeLineReater.DataSource = null;
                    ExchangeLineReater.DataBind();

                }
                else
                {
                    if (flag == 5)
                    {
                        PageUtil.showAlert(this, "扣账失败，错误产生可能原因：\\n 1、没有对应数据，请检查料号+库别是否在库存总表有对应数据 \\n 2、库存量不够调拨，请检查onhand_quantiy ");
                        return;
                    }
                    if (flag == 6)
                    {
                        PageUtil.showAlert(this, "扣账失败，错误产生可能原因：\\n 1、没有对应数据，请检查料号+料架+datecode是否在库存明细表中有对应数据（一般来说就是datecode输错了）\\n 2、剩余量不够调拨，请检查left_qty ");
                        return;
                    }
                }
            }
            catch (Exception e2)
            {
                PageUtil.showToast(this, "扣账失败！");
            }
        }
    }
}
