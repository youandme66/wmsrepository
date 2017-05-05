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
    public partial class IssueWork : System.Web.UI.Page
    {
        InvoiceDC invoiceDC = new InvoiceDC();
        string user;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "领料单作业";
            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }
            else
                user = Session["LoginName"].ToString();

            if (!IsPostBack)
            {
                DataSet ds_Invoice_no = invoiceDC.getIssueInvoice_no(0);
                //DataSet ds_datecode = invoiceDC.getFrame();

                if (ds_Invoice_no != null)
                {
                    DropDownList_invoice_no.DataSource = ds_Invoice_no.Tables[0].DefaultView;
                    DropDownList_invoice_no.DataValueField = "invoice_no";
                    DropDownList_invoice_no.DataTextField = "invoice_no";
                    DropDownList_invoice_no.DataBind();
                }

                //if (ds_datecode != null)
                //{
                //    DropDownList_datecode.DataSource = ds_datecode.Tables[0].DefaultView;
                //    DropDownList_datecode.DataValueField = "datecode";
                //    DropDownList_datecode.DataTextField = "datecode";
                //    DropDownList_datecode.DataBind();
                //}

                DropDownList_invoice_no.Items.Insert(0, "--选择领料单号--");

                //DropDownList_datecode.Items.Insert(0, "--选择datacode--");
            }
        }

        //清除查询结果
        protected void cleanMassage(object sender, EventArgs e)
        {

            IssueHeaderReater.DataSource = null;
            IssueHeaderReater.DataBind();

            IssueLineReater.DataSource = null;
            IssueLineReater.DataBind();
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

                if (Invoice_no == "--选择领料单号--")
                    Invoice_no = "";

                IssueHeaderReater.DataSource = invoiceDC.getIssueHeaderBySome(Invoice_no, "","");
                IssueLineReater.DataSource = invoiceDC.getIssueLineBySome(Invoice_no, "", "", start_time, end_time);

                if (IssueHeaderReater.DataSource == null)
                {
                    PageUtil.showToast(this, "无相关领料单头信息");

                }
                IssueHeaderReater.DataBind();

                if (IssueLineReater.DataSource == null)
                {
                    PageUtil.showToast(this, "无相关领料单身信息");
                }
                IssueLineReater.DataBind();
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
                string Flag = flag_debit.Value;
                int Issue_line_id_debit = int.Parse(issue_line_id_debit.Value);
                string Issued_sub = issued_sub.Value;
               
                //string Invoice_no = invoice_no.Value;
                string Item_name = item_name.Value;
                int Issued_qty = int.Parse(issued_qty.Value);

                //检验数据完整
                if (datecode.Value == "" || issued_qty_debit.Value=="")
                {
                    PageUtil.showToast(this, "请将数据填写完整");
                    return;
                }

                //用户选择输入数据
                string Datecode_debit = datecode.Value;
                int Issued_qty_debit;
                string Frame = frame.Value;
                if (frame.Value == "")
                {
                    PageUtil.showToast(this, "请输入料架后再操作");
                    return;
                }
                //用户非法输入
                try
                {
                    Issued_qty_debit = int.Parse(issued_qty_debit.Value);
                }
                catch (Exception e2)
                {
                    PageUtil.showToast(this, "领料量请不要输入非数字");
                    return;
                }


                if (Flag == "Y")
                {
                    PageUtil.showToast(this, "该条领料数据已扣账！请重新选择");
                    return;
                }

                if (Issued_qty_debit < 0)
                {
                    PageUtil.showToast(this, "领料量应大于0");
                    return;
                }
                //检验实际领料量是否等于申请领料量
                if (Issued_qty_debit != Issued_qty)
                {
                    PageUtil.showToast(this, "领料数量应等于申请领料量");
                    return;
                }

                int status = 1;   //默认为工单领料
                if (wo_no.Value == "none")  //非工单领料
                    status = 0;

                if (invoiceDC.getSubinventoryByFrame(Frame).Tables[0].Rows[0]["subinventory_name"].ToString() != Issued_sub)
                {
                    PageUtil.showToast(this, "请重新输入料架，该料架不属于该库别下");
                    return;
                }

                //扣账
                int flag = invoiceDC.IssueDebitAction(DateTime.Now, user, Issue_line_id_debit, Issued_qty_debit, Issued_sub, Frame, Item_name, Datecode_debit,status);
                
                //扣账执行成功时
                if (flag == 1)
                {
                    PageUtil.showToast(this, "扣账成功！");

                    IssueHeaderReater.DataSource = null;
                    IssueHeaderReater.DataBind();

                    IssueLineReater.DataSource = null;
                    IssueLineReater.DataBind();

                }
                //扣账失败时，输出详细错误原因
                else
                {
                    if (flag == 3)
                    {
                        PageUtil.showAlert(this, "扣账失败，错误产生可能原因：\\n 1、没有对应数据，请检查料号+库别是否在库存总表有对应数据 \\n 2、库存量不够领料，请检查onhand_quantiy ");
                        return;
                    }
                    if (flag == 4)
                    {
                        PageUtil.showAlert(this, "扣账失败，错误产生可能原因：\\n 1、没有对应数据，请检查料号+料架+datecode是否在库存明细表中有对应数据（一般来说就是datecode输错了） \\n 2、剩余量不够领料，请检查left_qty ");
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
