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
    public partial class ReturnWork : System.Web.UI.Page
    {
        InvoiceDC invoiceDC=new InvoiceDC();
        string user;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "退料单作业";
            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }
            else
                user = Session["LoginName"].ToString();

            if (!IsPostBack)
            {
                DataSet ds_Invoice_no = invoiceDC.getInvoice_no(0);
                //DataSet ds_frame = invoiceDC.getFrame();
                //DataSet ds_datecode = invoiceDC.getFrame();

                if (ds_Invoice_no != null)
                {
                    DropDownList_invoice_no.DataSource = ds_Invoice_no.Tables[0].DefaultView;
                    DropDownList_invoice_no.DataValueField = "invoice_no";
                    DropDownList_invoice_no.DataTextField = "invoice_no";
                    DropDownList_invoice_no.DataBind();
                }
                //if (ds_frame != null)
                //{
                //    DropDownList_frame.DataSource = ds_frame.Tables[0].DefaultView;
                //    DropDownList_frame.DataValueField = "frame_key";
                //    DropDownList_frame.DataTextField = "frame_name";
                //    DropDownList_frame.DataBind();
                //}
                //if (ds_datecode != null)
                //{
                //    DropDownList_datecode.DataSource = ds_datecode.Tables[0].DefaultView;
                //    DropDownList_datecode.DataValueField = "datecode";
                //    DropDownList_datecode.DataTextField = "datecode";
                //    DropDownList_datecode.DataBind();
                //}

                DropDownList_invoice_no.Items.Insert(0, "--选择退料单号--");
                //DropDownList_frame.Items.Insert(0, "--选择料架--");
                //DropDownList_datecode.Items.Insert(0, "--选择datacode--");
            }
        }

        //清除查询结果
        protected void cleanMassage(object sender, EventArgs e)
        {

            ReturnHeaderReater.DataSource = null;
            ReturnHeaderReater.DataBind();

            ReturnLineReater.DataSource = null;
            ReturnLineReater.DataBind();
        }

        //查询单头单身
        protected void search(object sender, EventArgs e)
        {
            try
            {
                string Invoice_no = DropDownList_invoice_no.SelectedValue.ToString();

                string first_time="2016-07-01 00:00:00";
                DateTime start_time=Convert.ToDateTime(first_time);
                DateTime end_time = DateTime.Now;

                if (Invoice_no == "--选择退料单号--")
                    Invoice_no = "";

                ReturnHeaderReater.DataSource = invoiceDC.getReturnHeaderBySome(Invoice_no, "","");
                ReturnLineReater.DataSource = invoiceDC.getReturnLineBySome(Invoice_no, "", "", start_time, end_time);

                if (ReturnHeaderReater.DataSource == null)
                {
                    PageUtil.showToast(this, "无相关退料单头信息");

                }
                ReturnHeaderReater.DataBind();

                if (ReturnLineReater.DataSource == null)
                {
                    PageUtil.showToast(this, "无相关退料单身信息");
                }
                ReturnLineReater.DataBind();
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
                //JS通过查询结果，绑定数据
                int Flag=int.Parse(flag_debit.Value);
                int Return_line_id_debit = int.Parse(return_line_id_debit.Value);
                string Return_sub_name = return_sub_name.Value;
                //string Invoice_no = invoice_no.Value;
                string Item_name=item_name.Value;
                int Return_qty = int.Parse(return_qty.Value);
               
                //string Frame_key = frame_key.Value;

                //检验数据完整
                if (datecode.Value == "")
                {
                    PageUtil.showToast(this, "请将数据填写完整");
                    return;
                }

                //用户选择输入数据
                string Datecode_debit = datecode.Value;
                int Return_qty_debit = int.Parse(return_qty_debit.Value);
                //int Frame_key = int.Parse(DropDownList_frame.SelectedValue.ToString());
                string Frame_key = frame_key.Value;

                if (frame_key.Value == "")
                {
                    PageUtil.showToast(this, "请输入料架再操作");
                    return;
                }

                if (Flag == 1)
                {
                    PageUtil.showToast(this, "该条退料数据已扣账！请重新选择");
                    return;
                }
               
                if (Return_qty_debit < 0)
                {
                    PageUtil.showToast(this, "退料量应大于0");
                    return;
                }
                //检验实际退回量是否等于申请退料量
                if (Return_qty_debit != Return_qty)
                {
                    PageUtil.showToast(this, "退料数量应等于申请退料量");
                    return;
                }

                int status = 1;   //默认为工单退料
                if (return_wo_no.Value == "none")  //非工单退料
                    status = 0;

                if (invoiceDC.getSubinventoryByFrame(Frame_key).Tables[0].Rows[0]["subinventory_name"].ToString() != Return_sub_name)
                {
                    PageUtil.showToast(this, "请重新输入料架，该料架不属于该库别下");
                    return;
                }

                //扣账
                int flag = invoiceDC.DebitAction(DateTime.Now, user, Return_line_id_debit, Return_qty_debit, Return_sub_name, Frame_key, Item_name, Datecode_debit, status);
                //扣账成功
                if (flag == 1)
                {
                    PageUtil.showToast(this, "扣账成功！");

                    ReturnHeaderReater.DataSource = null;
                    ReturnHeaderReater.DataBind();

                    ReturnLineReater.DataSource = null;
                    ReturnLineReater.DataBind();
                    
                }
                //扣账失败时，输出详细错误原因
                else
                {
                    if (flag == 2)
                    {
                        PageUtil.showAlert(this, "扣账失败，错误产生可能原因：\\n 1、没有对应数据，请检查料号+库别是否在库存总表有对应数据 ");
                        return;
                    }
                    if (flag == 3)
                    {
                        PageUtil.showAlert(this, "扣账失败，错误产生可能原因：\\n 1、没有对应数据，请检查料号+料架+datecode是否在库存明细表中有对应数据（一般来说就是datecode输错了）");
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