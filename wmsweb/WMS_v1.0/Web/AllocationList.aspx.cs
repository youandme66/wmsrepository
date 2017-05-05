using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;
using WMS_v1._0.Model;

namespace WMS_v1._0.Web
{
    public partial class AllocationList : System.Web.UI.Page
    {
       
        PnDC pnDc = new PnDC();
        Material_ioDC material_ioDC = new Material_ioDC();
        Wip_operationDC wip_operationDC = new Wip_operationDC();
        Exchange_headerDC exchange_headerDC = new Exchange_headerDC();
        DataTable table = new DataTable();
        SubinventoryDC subDC = new SubinventoryDC();
        FrameDC frameDC = new FrameDC();
        Items_onhand_qty_detailDC itemondetail = new Items_onhand_qty_detailDC();
        List<ModelItems_onhand_qty_detail> list = new List<ModelItems_onhand_qty_detail>();
        static bool flag_com;
        int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "调拨单";
            if (!IsPostBack)
            {
                List<string> list = new List<string>();
                list = pnDc.getAllItem_name();
                DataSet operation_Route_List = wip_operationDC.getAllRoute(); 
                DataSet subinventory_List = subDC.getAllUsedSubinventory_name();
                DataSet frame_List = frameDC.getAllFrame_name();
                if (subinventory_List != null || frame_List != null)
                {
                    item_name_List.DataSource = list;
                    item_name_List.DataBind();
                    operation_seq_num.DataSource = operation_Route_List.Tables[0].DefaultView;
                    operation_seq_num.DataValueField = "route";
                    operation_seq_num.DataBind();
                    out_subinventory_name.DataSource = subinventory_List.Tables[0].DefaultView;
                    out_subinventory_name.DataValueField = "subinventory_name";
                    out_subinventory_name.DataBind();
                    in_subinventory_name.DataSource = subinventory_List.Tables[0].DefaultView;
                    in_subinventory_name.DataValueField = "subinventory_name";
                    in_subinventory_name.DataBind();
                    out_locator_name.DataSource = frame_List;
                    out_locator_name.DataValueField = "frame_name";
                    out_locator_name.DataBind();
                    in_locator_name.DataSource = frame_List;
                    in_locator_name.DataValueField = "frame_name";
                    in_locator_name.DataBind();
                }
                else
                {
                    PageUtil.showAlert(this, "数据载入出错！");
                }
                out_subinventory_name.Items.Insert(0, "选择库别");
                in_subinventory_name.Items.Insert(0, "选择库别");
                out_locator_name.Items.Insert(0, "选择料架");
                in_locator_name.Items.Insert(0, "选择料架");
                operation_seq_num.Items.Insert(0, "选择制程");
                item_name_List.Items.Insert(0, "选择料号");
            }
        }

        //打印按钮中的确定操作----将文本框中的值传给打印界面
        protected void transPrint(object sender, EventArgs e)
        {
            //判断搜索框中的调拨单号是否存在
            if (string.IsNullOrEmpty(select_text_print.Value) == true)
            {
                Session["select_text_print"] = null;
                PageUtil.showToast(this, "请输入调拨单号，再执行此打印操作");
            }
            else
            {
                Session["select_text_print"] = select_text_print.Value;
                //打开打印界面
                Response.Write("<script>window.open('printTransFerringOrder.aspx', 'newwindow')</script>");
            }
        }

        //打印按钮中的取消操作------清除输入框数据
        protected void CleanInsertMessage(object sender, EventArgs e)
        {
            select_text_print.Value = String.Empty;
        }

        //Clean按钮------清楚所有数据
        protected void CleanAllMessage_Click(object sender, EventArgs e)
        {
            invoice_no.Value = String.Empty;
            out_subinventory_name.SelectedIndex = 0;
            in_subinventory_name.SelectedIndex = 0;
            out_locator_name.SelectedIndex = 0;
            in_locator_name.SelectedIndex = 0;
            operation_seq_num.SelectedIndex = 0; ;

            required_qty.Value = String.Empty;
            exchange_qty.Value = String.Empty;
            remark.Value = String.Empty;

            exchang_header_gridview.DataSource = null;
            exchang_header_gridview.DataBind();
            item_name_List.DataSource = null;
            item_name_List.DataBind();
            exchang_header_gridview.DataBind();
            exchange_message_gridview.DataSource = null;
            exchange_message_gridview.DataBind();
            PageUtil.showToast(this, "成功清除输入框中数据");
        }

        //生成调拨单号
        protected void invoice_no_make_Click(object sender, EventArgs e)
        {

            //invoice_no.Value = Number.code(exchange_headerDC.getTheNewestExchange_header_idByCreatetime().Exchange_header_id, 3);
            string strInvoice_no = invoice_no.Value;

            if (!string.IsNullOrWhiteSpace(strInvoice_no))
            {
                PageUtil.showToast(this, "当前领料单信息未提交！");
                return;
            }
            int end;
            string end_text;
            string month = string.Empty;
            string year = string.Empty;
            string day = string.Empty;

            DataSet exchange_header = exchange_headerDC.getTheNewestExchange_header_idByCreatetime();

            //判断数据库是否有数据,没有数据则初始化一个流水号
            if (exchange_header == null)
            {

                end_text = "00001";

                month = DateTime.Now.Month.ToString();

                year = DateTime.Now.Year.ToString();


                //为月份补0
                for (int i = month.Length; i < 2; i++)
                {

                    month = "0" + month;

                }

                //截取年份后两位
                year = year.Substring(2, 2);
            }
            else
            {
                end = int.Parse(exchange_header.Tables[0].Rows[0].ItemArray[4].ToString());


                //判断id是否为998,是则取下一个流水号为99999,否则取余+1作为下一个流水号
                end = end % 998 == 0 ? 998 + 1 : end % 999 + 1;

                end_text = end.ToString();


                //为流水号补0
                for (int i = end_text.Length; i < 3; i++)
                {

                    end_text = "0" + end_text;

                }
                day = DateTime.Now.Day.ToString();

                month = DateTime.Now.Month.ToString();

                year = DateTime.Now.Year.ToString();

                year = year.Substring(2, 2);

                for (int i = month.Length; i < 2; i++)
                {

                    month = "0" + month;

                }
            }

            invoice_no.Value = "E" + year + month + day + end_text;

        }


        //Add按钮------动态添加一条数据，尚未报存到数据库中
        protected void AddMeassage_Clisk1(object sender, EventArgs e)
        {
            if (Session["LoginId"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }
            string Exchange_wo_no = Session["LoginId"].ToString();

            string Invoice_no = invoice_no.Value;
            string Out_subinventory_name = out_subinventory_name.Items[out_subinventory_name.SelectedIndex].Text;
            string In_subinventory_name = in_subinventory_name.Items[in_subinventory_name.SelectedIndex].Text;
            string In_locator_name = in_locator_name.Items[in_locator_name.SelectedIndex].Text;
            string Out_locator_name = out_locator_name.Items[out_locator_name.SelectedIndex].Text;
            //string Operation_seq_num = operation_seq_num.Value;
          

            int Out_subinventory_key = subDC.getSubinventory_key(Out_subinventory_name);
            int In_subinventory_key = subDC.getSubinventory_key(In_subinventory_name);
            int In_locator_id = frameDC.getFrame_key(In_locator_name);
            int Out_locator_id = frameDC.getFrame_key(Out_locator_name);

            if (Invoice_no == "" || Out_subinventory_key == -1 || In_subinventory_key == -1 || Exchange_wo_no == "")
            {
                //查询时暂收单号不可未空，弹出警示框提示用户输入
                string temp_AlertString = "所有数据请需要填写完整！请检查是否填写完整";
                PageUtil.showToast(this, temp_AlertString);
            }
            else
            {
                table = GetGridViewData(table);
                DataRow  sourseRow = table.NewRow();
                sourseRow["invoice_no"] = Invoice_no;
                sourseRow["out_subinventory_key"] = Out_subinventory_key;
                sourseRow["in_subinventory_key"] = In_subinventory_key;
                sourseRow["in_locator_id"] = In_locator_id;
                sourseRow["out_locator_id"] = Out_locator_id;
                //sourseRow["datecode"] = Datecode;
                sourseRow["exchange_wo_no"] = Session["LoginId"].ToString();
                //sourseRow["operation_seq_num"] = Operation_seq_num;
                table.Rows.Add(sourseRow);
                exchang_header_gridview.DataSource = table;
                exchang_header_gridview.DataBind();
                exchange_message_gridview.DataSource = table;
                exchange_message_gridview.DataBind();
                flag_com = false;

       
            }
        }

        protected void AddMeassage_Clisk(object sender, EventArgs e)
        {
            if (Session["LoginId"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }
            string Exchange_wo_no = Session["LoginId"].ToString();
            string Invoice_no = invoice_no.Value;
            string Out_subinventory_name = out_subinventory_name.Items[out_subinventory_name.SelectedIndex].Text;
            string In_subinventory_name = in_subinventory_name.Items[in_subinventory_name.SelectedIndex].Text;
            string In_locator_name = in_locator_name.Items[in_locator_name.SelectedIndex].Text;
            string Out_locator_name = out_locator_name.Items[out_locator_name.SelectedIndex].Text;
            int Operation_seq_num;
            int Required_qty;
            string Exchange_qty = exchange_qty.Value;
            string Item_name = item_name_List.SelectedValue;
            string Remark = remark.Value;
            int Out_subinventory_key = subDC.getSubinventory_key(Out_subinventory_name);
            int In_subinventory_key = subDC.getSubinventory_key(In_subinventory_name);
            int In_locator_id = frameDC.getFrame_key(In_locator_name);
            int Out_locator_id = frameDC.getFrame_key(Out_locator_name);
            string Datecode = datecode.Value;

            if (Item_name == "")
            {
                PageUtil.showToast(this, "请选择料号！");
                return;
            }

            if (operation_seq_num.SelectedIndex == 0)
            {
                PageUtil.showToast(this, "请选择制程！");
                return;
            }
            Operation_seq_num = wip_operationDC.getRoute_idByRoute(operation_seq_num.SelectedValue);
            //list = itemondetail.getItems_onhand_qty_detailByITEM_NAME(Item_name);
            //try
            //{
            //    int qty = list[0].Onhand_quantiy;
            //    if (qty <= 0)
            //    {
            //        PageUtil.showToast(this, "该料号中没有料！");
            //        return;
            //    }
            //}
            //catch
            //{
            //    PageUtil.showToast(this, "该料号中没有料！");
            //}

            int item_id = pnDc.getItem_idByItem_name(Item_name);

            //根据填写条件确定调拨明细表中的需求量
            //int Onhand_qty;
            Required_qty = material_ioDC.getOnhand_QtyBySome(item_id, Out_locator_id, Out_subinventory_name, Datecode);


            if ( Exchange_qty == "" || Remark == "")
            {
                //查询时暂收单号不可未空，弹出警示框提示用户输入
                string temp_AlertString = "所有数据请需要填写完整！请检查是否填写完整";
                PageUtil.showToast(this, temp_AlertString);
            }
            else
            {

                try
                {
                    int.Parse(Exchange_qty);
                }
                catch
                {
                    PageUtil.showToast(this, "输入类型有问题，请检查输入规范！");
                    return;
                }
                if (Required_qty == -1)
                {
                    PageUtil.showToast(this, "未查出对应需求量,请重新检查料号、库别、Datecode、制程数据！");
                    return;
                }
                if (Required_qty < int.Parse(Exchange_qty))
                {
                    PageUtil.showToast(this, "需求量为" + Required_qty + ",调拨量不可大于需求量！");
                    return;
                }
                table = GetGridViewData(table);
                //防止用户未填写调拨单主要部分，直接填写下半部分
                try
                {
                    DataRow sourseRow = table.Rows[i - 1];
                    if (sourseRow["item_name"].Equals("&nbsp;"))
                    {
                        sourseRow.BeginEdit();
                        sourseRow["required_qty"] = Required_qty;
                        sourseRow["exchange_qty"] = Exchange_qty;
                        sourseRow["item_name"] = Item_name;
                        sourseRow["remark"] = Remark;
                        sourseRow["operation_seq_num"] = Operation_seq_num;
                        sourseRow.EndEdit();
                    }
                    else
                    {
                        DataRow sourseRow1 = table.NewRow();
                        sourseRow1["invoice_no"] = Invoice_no;
                        sourseRow1["out_subinventory_key"] = Out_subinventory_key;
                        sourseRow1["in_subinventory_key"] = In_subinventory_key;
                        sourseRow1["in_locator_id"] = In_locator_id;
                        sourseRow1["out_locator_id"] = Out_locator_id;
                        sourseRow1["exchange_wo_no"] = Session["LoginId"].ToString();  
                        sourseRow1["item_name"] = Item_name;
                        sourseRow1["operation_seq_num"] = Operation_seq_num;

                        sourseRow1["required_qty"] = Required_qty;
                        sourseRow1["exchange_qty"] = Exchange_qty;
         
                        sourseRow1["remark"] = Remark;

                        table.Rows.Add(sourseRow1);
                    }
                    flag_com = true;
                    exchange_message_gridview.DataSource = table;
                    exchange_message_gridview.DataBind();
                }
                catch (Exception e1)
                {
                    string temp_ToastString = "请先填写调拨单主要部分！";
                    PageUtil.showToast(this, temp_ToastString);
                }
            }
        }


        //Commit按钮------将动态添加的调拨单信息存储到数据库中，进行扣账操作，修改Exchange_headerDC表中数据
        protected void CommitMessage_Click(object sender, EventArgs e)
        {
            if (flag_com == false)
            {
                PageUtil.showToast(this, "请填写完整信息！");
                return;
            }
            table = GetGridViewData(table);
            if (table.Rows.Count == 0)
            {
                PageUtil.showToast(this, "未填写数据之前，无法进行存储步骤！");
                return;
            }
            exchange_message_gridview.DataSource = table;
            exchange_message_gridview.DataBind();
            bool flagCommit = exchange_headerDC.FinishAction(table);
            if (flagCommit == true)
            {
                string temp_ToastString = "存储成功！";
                PageUtil.showToast(this, temp_ToastString);
                i = 0;
            }
            else
            {
                string temp_ToastString = "存储失败！";
                PageUtil.showToast(this, temp_ToastString);
            }
        }


        //获取页面上表格中的所有行,并组装为tabel
        private DataTable GetGridViewData(DataTable table)
        {
            table.Columns.Add(new DataColumn("Invoice_no"));
            table.Columns.Add(new DataColumn("out_subinventory_key"));
            table.Columns.Add(new DataColumn("in_subinventory_key"));
            table.Columns.Add(new DataColumn("in_locator_id"));
            table.Columns.Add(new DataColumn("out_locator_id"));
            table.Columns.Add(new DataColumn("exchange_wo_no"));
            table.Columns.Add(new DataColumn("item_name"));
            table.Columns.Add(new DataColumn("operation_seq_num"));
            table.Columns.Add(new DataColumn("required_qty"));
            table.Columns.Add(new DataColumn("exchange_qty"));
            table.Columns.Add(new DataColumn("remark"));
            foreach (GridViewRow row in exchange_message_gridview.Rows)
            {
                DataRow sourseRow = table.NewRow();
                sourseRow["Invoice_no"] = row.Cells[0].Text;
                sourseRow["out_subinventory_key"] = row.Cells[1].Text;
                sourseRow["in_subinventory_key"] = row.Cells[2].Text;
                sourseRow["in_locator_id"] = row.Cells[3].Text;
                sourseRow["out_locator_id"] = row.Cells[4].Text;
                sourseRow["item_name"] = row.Cells[5].Text;
                sourseRow["operation_seq_num"] = row.Cells[6].Text;
                sourseRow["required_qty"] = row.Cells[7].Text;
                sourseRow["exchange_qty"] = row.Cells[8].Text;
                sourseRow["remark"] = row.Cells[9].Text;
                sourseRow["exchange_wo_no"] = Session["LoginId"].ToString();
                table.Rows.Add(sourseRow);
                i = i + 1;
            }
            return table;
        }

        protected void exchange_message_gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            exchange_message_gridview.PageIndex = e.NewPageIndex;
            exchange_message_gridview.DataSource = table;
            exchange_message_gridview.DataBind();
        }
    }
}