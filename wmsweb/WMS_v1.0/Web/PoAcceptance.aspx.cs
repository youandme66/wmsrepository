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
    public partial class PoAcceptance : System.Web.UI.Page
    {
        PoDC poDC = new PoDC();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "PO允收";
        }


        //Clean按钮------删除输入框中数据
        protected void CleanAllMessage_Click(object sender, EventArgs e)
        {
            accepted_qty.Value = string.Empty;
            return_qty.Value = string.Empty;
            receiveMtl_gridview.DataSource = null;
            receiveMtl_gridview.DataBind();
            PageUtil.showToast(this, "成功清除输入框中数据");
            hiddent.Value = "请选择暂收单号";

        }


        //确定按钮------以暂收单号查询暂收表里的数据
        protected void QueryMessage_Click(object sender, EventArgs e)
        {
             string Receipt_no = Request.Form["receipt_no"];
            //调用暂收表DC里的查询方法
            if (Receipt_no == "" || Receipt_no==null)
            {
                string temp_AlertString = "暂收单号不可为空，请填写暂收单号！";
                PageUtil.showToast(this, temp_AlertString);
            }
            else
            {
                DataSet modelReceive_mtl_List = poDC.searchReceive_mtlByReceipt_no(Receipt_no);
                if (modelReceive_mtl_List == null)
                {
                    string temp_ToastString = "对应单号在数据库中无相应数据！";
                    PageUtil.showToast(this, temp_ToastString);
                }
                else
                {
                    receiveMtl_gridview.DataSource = modelReceive_mtl_List;
                    receiveMtl_gridview.DataBind();
                }
                
            }
            receive.Value = Receipt_no;
            //receipt_no.Value = Receipt_no;
            //Response.Write("<script type='text/javascript'>alert('XXX');</script>");
            hiddent.Value = Receipt_no;
        }


        protected void ModifyMessage_Click(object sender, EventArgs e)
        {
            int Accepted_qty = 0, Return_qty = 0;
            int RECEIPT_NO=0;
            String Receipt_no = receive.Value;
            if (string.IsNullOrWhiteSpace(Receipt_no))
            {
                PageUtil.showToast(this, "暂收单号不可为空！");
                return;
            }
            try
            {
                Accepted_qty = int.Parse(accepted_qty.Value);
                Return_qty = int.Parse(return_qty.Value);
            }
            catch
            {
                PageUtil.showToast(this, "填写格式不规范，请重新输入！");
                return;
            }
            if (Return_qty < 0 || Accepted_qty <0)
            {
                PageUtil.showToast(this, "数量必须要大于0！");
                return;
            }
            RECEIPT_NO = poDC.getRcv_qtyByReceipt_no(Receipt_no);
            if (RECEIPT_NO != (Return_qty + Accepted_qty))
            {
                PageUtil.showToast(this, "暂收量需要等于退回量与允收量的和！");
                return;
            }
            if (RECEIPT_NO == (Return_qty + Accepted_qty))
            {
                //调用暂收表DC里的修改数据方法
                bool flagReceive_mtlDC = poDC.updateAccepted_qtyAndReturn_qty(Receipt_no, Accepted_qty, Return_qty);
                if (flagReceive_mtlDC)
                {
                    //修改数据成功
                    string temp_AlertString = "修改暂收表中的数据成功！";
                    PageUtil.showToast(this, temp_AlertString);
                    DataSet modelReceive_mtl_List = poDC.searchReceive_mtlByReceipt_no(Receipt_no);
                    receiveMtl_gridview.DataSource = modelReceive_mtl_List;
                    receiveMtl_gridview.DataBind();
                    //操作完成，清空输入框中的数据
                    this.CleanAllMessage();
                }
                else
                {
                    string temp_AlertString = "修改暂收表中的数据失败！请检查字符长度是否超出范围！";
                    PageUtil.showToast(this, temp_AlertString);
                }
                Session["Local"] = "PO允收";
            }

        }


        //清楚函数，清空所有输入框中的值
        protected void CleanAllMessage()
        {
            accepted_qty.Value = String.Empty;
            return_qty.Value = String.Empty;
        }

        protected void receiveMtl_gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            String Receipt_no = Request.Form["receipt_no"]; 

            receiveMtl_gridview.PageIndex = e.NewPageIndex;

            DataSet modelReceive_mtl_List = poDC.searchReceive_mtlByReceipt_no(Receipt_no);

            receiveMtl_gridview.DataSource = modelReceive_mtl_List;
            receiveMtl_gridview.DataBind();
        }

    }
}