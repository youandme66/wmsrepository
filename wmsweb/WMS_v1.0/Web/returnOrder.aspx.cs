using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;
using WMS_v1._0.Model;

namespace returnOrder
{
    public partial class returnOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "退料单";
            if (!IsPostBack)
            {
                BindDrop();
                SubinventoryDC subDC = new SubinventoryDC();
                DataSet ds1 = subDC.getAllUsedSubinventory_name();
                Wip_operationDC regDC = new Wip_operationDC();
                DataSet ds2 = regDC.getRegion_nameBySubinventory_name();
                if (ds1 != null)
                {
                    Subinventory4.DataSource = ds1.Tables[0].DefaultView;
                    Subinventory1.DataSource = ds1.Tables[0].DefaultView;
                    Subinventory4.DataValueField = "subinventory_key";
                    Subinventory4.DataTextField = "subinventory_name";
                    Subinventory1.DataValueField = "subinventory_key";
                    Subinventory1.DataTextField = "subinventory_name";
                    Subinventory4.DataBind();
                    Subinventory1.DataBind();
                }
                else
                    PageUtil.showToast(this.Page, "数据载入出错！");
                Subinventory1.Items.Insert(0, "选择库别");
                if (ds2 != null)
                {
                    seq_operation_num1.DataSource = ds2.Tables[0].DefaultView;

                    seq_operation_num1.DataValueField = "route_id";

                    seq_operation_num1.DataBind();

                }
                else
                    PageUtil.showToast(this.Page, "数据载入出错");
                seq_operation_num1.Items.Insert(0, "选择制程");
            }
        }

        //打印按钮中的确定操作----将文本框中的值传给打印界面
        protected void transPrint(object sender, EventArgs e)
        {
            //判断搜索框中的领料单号是否存在
            if (string.IsNullOrEmpty(select_text_print.Value) == true)
            {
                Session["select_text_print"] = null;
                PageUtil.showToast(this, "请输入退料单号，再执行此打印操作");
            }
            else
            {
                Session["select_text_print"] = select_text_print.Value;
                //打开打印界面
                Response.Write("<script>window.open('printMaterialReturning.aspx', 'newwindow')</script>");
            }
        }

        //打印按钮中的取消操作------清除输入框数据
        protected void CleanInsertMessage(object sender, EventArgs e)
        {
            select_text_print.Value = String.Empty;
        }

        //提交单头
        protected void insert1(object sender, EventArgs e)
        {
            Return_headerDC DC = new Return_headerDC();
            WoDC DC2 = new WoDC();
            string invoice_no = invoice_no2.Value;
            string quit_type = quit_type1.Items[quit_type1.SelectedIndex].Value;
            int enabled = 0;
            int subinventory = 0;
            string remark = remark1.InnerText;
            string return_man = Session["LoginName"].ToString();
            string wo_no = return_wo_no1.Value;

            if (String.IsNullOrEmpty(invoice_no))
            {
                PageUtil.showToast(this.Page, "请点击插入自动生成单据号！");
            }
            else if (String.IsNullOrEmpty(return_wo_no1.Value))
            {
                PageUtil.showToast(this.Page, "请输入工单号！");
            }

            List<ModelWO> wo;
            wo = DC2.getWOByWo_no(wo_no);
            if (wo == null)
            {
                PageUtil.showToast(this.Page, "没有这个工单号，请检查数据");
            }
            else
            {
                try
                {
                    enabled = int.Parse(Enabled1.Items[Enabled1.SelectedIndex].Value);
                    subinventory = int.Parse(Subinventory1.Items[Subinventory1.SelectedIndex].Value);
                }
                catch (Exception ex)
                {
                    PageUtil.showToast(this.Page, "数据转换出错，请检查数据格式！");
                }
                bool a = DC.insertReturn_header(invoice_no, quit_type, subinventory, enabled, wo[0].Wo_key, return_man, remark);
                if (a == true)
                {
                    PageUtil.showToast(this.Page, "添加退料单头成功！");

                    Line_Repeater.DataSource = DC.getReturn_headerByLikeINVOICE_NO(invoice_no);
                    if (Line_Repeater.DataSource == null)
                        PageUtil.showToast(this, "数据库中没有对应数据，请添加数据后再查询");
                    else
                        Line_Repeater.DataBind();
                }
                else
                {
                    PageUtil.showToast(this.Page, "添加退料单头失败！");
                }
            }
        }
        //获取料号
        protected void getItem_nameByReturn_wo_no(object sender, EventArgs e)
        {
            Return_lineDC DC = new Return_lineDC();
            string Reurn_wo_no=return_wo_no.Value;
            if (String.IsNullOrEmpty(Reurn_wo_no))
            {
                PageUtil.showToast(this.Page, "请输入工单号");
            }
            try
            {
                item_name_Insert.Value = DC.getItem_nameByReturn_wo_no(Reurn_wo_no);
            }
            catch
            {
                PageUtil.showToast(this.Page, "该工单号下，无料号对应");
            }
        }

        //提交单身
        protected void insert2(object sender, EventArgs e)
        {
            Return_lineDC DC = new Return_lineDC();
            WoDC woDC = new WoDC();
            int return_header_id = int.Parse(Text1.Value);
            string return_wo_no1 = return_wo_no.Value;
            string item_name = item_name_Insert.Value;
           
           
            int seq_operation_num = 0;
            int subinventory = 0;
            string region = DropDownList2.Items[DropDownList2.SelectedIndex].Text;
            int quit_number = 0;
           
            if (String.IsNullOrEmpty(quit_num.Value))
            {
                PageUtil.showToast(this.Page, "请输入退料量");
            }
            
            //Items_onhand_qty_detailDC DC2 = new Items_onhand_qty_detailDC();
            //List<ModelItems_onhand_qty_detail> item;
            //item = DC2.getItems_onhand_qty_detailByITEM_NAME(item_name);
            //if (item == null)
            //{
            //    PageUtil.showToast(this.Page, "您输入的料号不存在");
            //}
            //else
            //{
            try
            {
                seq_operation_num = int.Parse(seq_operation_num1.Items[seq_operation_num1.SelectedIndex].Value);
                subinventory = int.Parse(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
                quit_number = int.Parse(quit_num.Value);
                if (quit_number > woDC.getTarget_qtyByReturn_wo_no(return_wo_no1))
                {
                    PageUtil.showToast(this.Page, "退料量大于领料量，请重新输入！");
                }
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this.Page, "数据转换出错，请检查数据格式！");
                return;
            }
            bool a = DC.insertReturn_line(return_wo_no1, seq_operation_num, item_name, quit_number, subinventory, return_header_id, region);
            if (a == true)
            {

                //string return_wo_no2 = return_wo_no4.Value;
                try
                {
                    Repeater1.DataSource = DC.getReturn_lineByLikeRETURN_WO_NO(return_wo_no1);

                    if (Repeater1.DataSource == null)
                        PageUtil.showToast(this, "数据库中没有对应数据，请添加数据后再查询");
                    else
                        Repeater1.DataBind();
                }
                catch (Exception ex)
                {
                    PageUtil.showToast(this.Page, "数据查询错误!");
                }
                PageUtil.showToast(this.Page, "添加单身成功！");
            }
            else
            {
                PageUtil.showToast(this.Page, "添加单身失败！");
            }
            //}
        }
        protected void operation(object sender, EventArgs e)
        {

        }

        //更新单头
        protected void update1(object sender, EventArgs e)
        {
            Return_headerDC DC = new Return_headerDC();
            WoDC woDC=new WoDC();
            string invoice_no = quit_invoice_no2.Value;
            string quit_type = quit_type2.Items[quit_type2.SelectedIndex].Value;
            int subinventory = 0;
            int status = 0;
            int quit_wo_no = 0;
            string remark = remark2.Value;
            string return_man = Session["LoginName"].ToString();
            if (String.IsNullOrEmpty(quit_wo_no2.Value))
            {
                PageUtil.showToast(this.Page, "请输入工单号！");
            }
            else
            {
                try
                {
                    subinventory = int.Parse(Subinventory4.Items[Subinventory4.SelectedIndex].Value);
                    status = int.Parse(status2.Items[status2.SelectedIndex].Value);
                    quit_wo_no =woDC.getWo_keyByReturn_wo_no(quit_wo_no2.Value);
                }
                catch (Exception ex)
                {
                    PageUtil.showToast(this.Page, "数据转换出错，请检查数据格式！");
                    return;
                }
                bool a = DC.updateReturn_header(invoice_no, quit_type, subinventory, status, quit_wo_no, return_man, remark);
                if (a == true)
                {
                    try
                    {
                        Line_Repeater.DataSource = DC.getReturn_headerByLikeINVOICE_NO(invoice_no);
                        if (Line_Repeater.DataSource == null)
                            PageUtil.showToast(this, "数据库中没有对应数据，请添加数据后再查询");
                        Line_Repeater.DataBind();
                    }
                    catch (Exception ex)
                    {
                        PageUtil.showToast(this.Page, "数据查询失败！");
                    }
                    PageUtil.showToast(this.Page, "单据号为" + invoice_no + "更新成功！");
                }
                else
                {
                    PageUtil.showToast(this.Page, "单据号为" + invoice_no + "更新失败！");
                }
            }
        }

        //删除单头
        protected void delete1(object sender, EventArgs e)
        {
            Return_headerDC DC = new Return_headerDC();
            string invoice_no = lab1.Value;
            bool a = DC.deleteReturn_header(invoice_no);
            if (a == true)
            {

                string invoice_no1 = invoice_no4.Value;
                try
                {
                    Line_Repeater.DataSource = DC.getReturn_headerByLikeINVOICE_NO(invoice_no1);
                    if (Line_Repeater.DataSource == null)
                        PageUtil.showToast(this, "数据库中没有对应数据，请添加数据后再查询");
                    else
                        Line_Repeater.DataBind();
                }
                catch (Exception ex)
                {
                    PageUtil.showToast(this.Page, "数据查询失败！");
                }
                Return_lineDC DC1 = new Return_lineDC();
                string return_wo_no = return_wo_no4.Value;
                try
                {
                    Repeater1.DataSource = DC1.getReturn_lineByLikeRETURN_WO_NO(return_wo_no);
                    if (Repeater1.DataSource == null)
                        PageUtil.showToast(this, "数据库中没有对应数据，请添加数据后再查询");
                    else
                        Repeater1.DataBind();
                }
                catch (Exception ex)
                {
                    PageUtil.showToast(this.Page, "数据查询错误!");
                }
                PageUtil.showToast(this.Page, "单据号" + invoice_no + "删除成功！");
            }
            else
            {
                PageUtil.showToast(this.Page, "单据号" + invoice_no + "删除失败！");
            }
        }

        //查询单头
        protected void select1(object sender, EventArgs e)
        {
            Return_headerDC DC = new Return_headerDC();
            string invoice_no = invoice_no4.Value;

            try
            {
                Line_Repeater.DataSource = DC.getReturn_headerByLikeINVOICE_NO(invoice_no);
                if (Line_Repeater.DataSource == null)
                    PageUtil.showToast(this, "数据库中没有对应数据，请添加数据后再查询");
                else
                    Line_Repeater.DataBind();
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this.Page, "数据查询失败！");
            }

        }

        //查询单身
        protected void select2(object sender, EventArgs e)
        {
            Return_lineDC DC = new Return_lineDC();
            string return_wo_no = return_wo_no4.Value;
            try
            {
                Repeater1.DataSource = DC.getReturn_lineByLikeRETURN_WO_NO(return_wo_no);
                if (Repeater1.DataSource == null)
                    PageUtil.showToast(this, "数据库中没有对应数据，请添加数据后再查询");
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this.Page, "数据查询错误!");
            }

        }
        protected void clear(object sender, EventArgs e)
        {
            Line_Repeater.DataBind();
            Repeater1.DataBind();
            PageUtil.showToast(this, "成功清除查询结果");
        }
        //生成单据号规则
        protected void num_no(object sender, EventArgs e)
        {
            string num;
            Return_headerDC header = new Return_headerDC();
            ModelReturn_header invoice = header.getTheNewestReturn_headerByCreatetime();
            if (invoice == null)
            {
                num = Number.code(0, 2);
            }
            else
            {
                num = Number.code(invoice.Return_header_id, 2);
            }
            invoice_no2.Value = num;
        }


        private void BindDrop()
        {
            SubinventoryDC subDC = new SubinventoryDC();
            DataSet ds1 = subDC.getAllUsedSubinventory_name();
            DropDownList1.DataTextField = "subinventory_name";
            DropDownList1.DataValueField = "subinventory_key";
            DropDownList1.DataSource = ds1.Tables[0].DefaultView;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--选择库别--"));
            DropDownList2.Items.Insert(0, new ListItem("--选择区域--"));
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            RegionDC regionDC = new RegionDC();
            DataSet ds2 = regionDC.getRegion_nameBySub_name(DropDownList1.Items[DropDownList1.SelectedIndex].Text);
            int count = ds2.Tables[0].Rows.Count;
            if (ds2 != null)
            {
                DropDownList2.DataSource = ds2.Tables[0].DefaultView;
                DropDownList2.DataValueField = "region_key";
                DropDownList2.DataTextField = "region_name";
                DropDownList2.DataBind();
            }
            else
                PageUtil.showAlert(this, "区域载入出错！");
            DropDownList2.Items.Insert(0, "--选择区域--");
        }
    }
}