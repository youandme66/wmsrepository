using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.Web
{
    public partial class PoSuspense : System.Web.UI.Page
    {
        PoDC poDC = new PoDC();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "PO暂收";
            if (!IsPostBack)
            {
                DataSet po_noList = poDC.getAllValidPo_no();
                if (po_noList != null)
                {
                    po_no.DataSource = po_noList.Tables[0].DefaultView;
                    po_no.DataValueField = "po_no";
                    po_no.DataBind();
                    po_no.Items.Insert(0, "选择PO单号");
                    //line_num.Items.Insert(0, "选择Line序号");
                }
                else
                {
                    PageUtil.showAlert(this, "无数据载入下拉框！所以单号已收料完成");
                }
            }

            this.po_no.SelectedIndexChanged += new System.EventHandler(this.ddlCategoryid_SelectedIndexChanged);
        }


        protected void ddlCategoryid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //绑定机构信息
            DataSet dtOrg = new DataSet();
            dtOrg = poDC.getValidLine_numByPo_no(this.po_no.SelectedValue.ToString());
            line_num.DataSource = dtOrg;
            line_num.DataTextField = "line_num";
            line_num.DataBind();
            line_num.Items.Insert(0, "选择Line序号");
        }

        protected void Clear()
        {
            po_no.SelectedIndex = 0;
            line_num.SelectedIndex = 0;
            item_id_auto.Value = string.Empty;
            item_name_auto.Value = string.Empty;
            rcv_qty.Value = string.Empty;
            request_qty.Value = string.Empty;
            datecode.Value = string.Empty;
            vendor_name.Value = string.Empty;
            serch_Receive_mtl.DataSource = null;
            serch_Receive_mtl.DataBind();
            Receipt_no_Query.Value = string.Empty;
            Po_no_Query.Value = string.Empty;
            Item_name_Query.Value = string.Empty;

            //更新PO单号下拉框数据，收料完成则无需显示
            DataSet po_noList = poDC.getAllValidPo_no();
            if (po_noList != null)
            {
                po_no.DataSource = po_noList.Tables[0].DefaultView;
                po_no.DataValueField = "po_no";
                po_no.DataBind();
            }
            else
            {
                po_no.DataSource = null;
                po_no.DataBind();
            }
        }

        //删除所有按钮------清除填写的暂收单信息
        protected void ClearALLMessage_Click(object sender, EventArgs e)
        {
            this.Clear();
            PageUtil.showToast(this, "成功清除输入框中数据");
        }


        //commit按钮------显示ＰＯ单头和单身信息，使得页面中灰色部分自动填写信息
        protected void ChangeNav_Click(object sender, EventArgs e)
        {
            this.PoGetmessage_Click();
            //使用JS语句更换页面效果
            this.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#foot_zbSecond').show();", true);
        }


        //为了显示ＰＯ单头和单身信息，使得页面中灰色部分自动填写信息
        protected void PoGetmessage_Click()
        {

            string PO_NO = po_no.SelectedValue.ToString();
            int Line_num_Write = -1;
            DataSet dataset;
            DataSet modelPO_line_List = new DataSet();
            if (string.IsNullOrWhiteSpace(PO_NO) && string.IsNullOrWhiteSpace(line_num.SelectedValue.ToString()))
            {
                //PO单号及料号不可未空，弹出警示框提示用户输入
                string temp_AlertString = "信息不可为空！请重新输入";
                PageUtil.showToast(this, temp_AlertString);
                return;
            }

            try
            {
                Line_num_Write = int.Parse(line_num.SelectedValue.ToString());
            }
            catch
            {
                PageUtil.showToast(this, "请重新选择！");
                po_no.SelectedIndex = 0;
                //line_num_Write.SelectedIndex = 0;
                return;
            }
            dataset = poDC.getPOHeaderByPo_no(PO_NO);
            modelPO_line_List = poDC.getPOLineByLine_num(PO_NO,Line_num_Write);
            if (modelPO_line_List == null || dataset == null)
            {
                //PO单号及料号不可未空，弹出警示框提示用户输入
                PageUtil.showToast(this, "未找到有效对应的PO单头或单身数据，请检查输入的PO单号和料号！");
                return;
            }
            //给定数据源，并进行数据绑定。
            serch_POHeader.DataSource = dataset;
            serch_POHeader.DataBind();
            serch_POLine.DataSource = modelPO_line_List;
            serch_POLine.DataBind();
            if (dataset == null && modelPO_line_List == null)
            {
                string temp_ToastString = "对应单号在数据库中无相应数据！";
                PageUtil.showToast(this, temp_ToastString);
            }
            //自动填写灰色区域的信息
            this.AutoSetMessage(dataset, modelPO_line_List);

        }


        //自动动填写灰色区域的信息
        protected void AutoSetMessage(DataSet dataset, DataSet modelPO_line_List)
        {

            //使用try,catch捕捉int字段值为空的情况
            try
            {
                request_qty.Value = modelPO_line_List.Tables[0].Rows[0].ItemArray[4].ToString();
            }
            catch (Exception e) { request_qty.Value = ""; }
            try
            {
                po_line_id.Value = modelPO_line_List.Tables[0].Rows[0].ItemArray[0].ToString();
                item_id_auto.Value = modelPO_line_List.Tables[0].Rows[0].ItemArray[3].ToString();
            }
            catch (Exception e) { po_line_id.Value = ""; item_id_auto.Value = ""; }
            try
            {
                po_header_id.Value = dataset.Tables[0].Rows[0].ItemArray[0].ToString();
            }
            catch (Exception e) { po_header_id.Value = ""; }
            try
             {
                 vendor_code.Value = dataset.Tables[0].Rows[0].ItemArray[2].ToString();
                CustomersDC customersDC = new CustomersDC();
                DataSet set = customersDC.getCustomerByVendor_key(dataset.Tables[0].Rows[0].ItemArray[2].ToString());
                vendor_name.Value = set.Tables[0].Rows[0].ItemArray[0].ToString();
            }
            catch (Exception e) { po_header_id.Value = ""; }
            try
            {
                item_name_auto.Value = poDC.getItem_nameByItem_id(int.Parse(item_id_auto.Value));
            }
            catch (Exception e) { item_name_auto.Value = ""; }
        }


        //保存全部按钮------获取页面填写的数据存储到暂收表中
        protected void PoCommitmessage_Click(object sender, EventArgs e)    
        {
            if (Session["LoginId"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }
            string create_user = Session["LoginName"].ToString();

            string ITEM_NAME = item_name_auto.Value;
            string PO_NO = po_no.SelectedValue.ToString();
            string DateCode = datecode.Value;
            string VENDOR_CODE = vendor_code.Value;
            DateTime UPDATE_TIME = new DateTime();
            UPDATE_TIME = DateTime.Now;
            int RCV_QTY = 0,ITEM_ID = 0,PO_HEADER_ID = 0,PO_LINE_ID = 0;
            if (DateCode.Equals(""))
            {
                PageUtil.showToast(this, "请填写DC(生产周期)！");
                return;
            }
            try
            {
                PO_HEADER_ID = int.Parse(po_header_id.Value);
                PO_LINE_ID = int.Parse(po_line_id.Value);
                RCV_QTY = int.Parse(rcv_qty.Value);
                ITEM_ID = int.Parse(item_id_auto.Value);
            }
            catch 
            {
                PageUtil.showToast(this, "输入有问题，请检查输入是否规范！");
                return;
            }
            if (int.Parse(rcv_qty.Value) <= 0)
            {
                string temp_ToastString = "收料量应大于0,请重新填写收料量";
                PageUtil.showToast(this, temp_ToastString);
                return;
            }
            //逻辑判断，收料量不可大于需求量
            if (int.Parse(request_qty.Value) < int.Parse(rcv_qty.Value))
            {
                string temp_ToastString = "收料量不可大于需求量,请重新填写收料量";
                PageUtil.showToast(this, temp_ToastString);
                return;
            }
            
            //同一单号多次暂收量判断：总收料量不可大于需求量
            //po_header_id.Value;
            int allRcv_Qty = poDC.getRcv_qtyByPo_header_idAndline_id(int.Parse(po_header_id.Value), int.Parse(po_line_id.Value));
           
            if (allRcv_Qty + int.Parse(rcv_qty.Value) > int.Parse(request_qty.Value))
            {
                string temp_ToastString = "此单号之前有过暂收记录，已收料" + allRcv_Qty.ToString() + ",总收料量不可大于需求量！";
                PageUtil.showToast(this, temp_ToastString);
                return;
            }

            //调用receive_mtlDC类里的方法进行存储
            Boolean flagReceive_mtlDC = poDC.poSuspense(ITEM_NAME, ITEM_ID, DateCode, RCV_QTY, PO_NO, VENDOR_CODE,PO_HEADER_ID,PO_LINE_ID, UPDATE_TIME, create_user);
            //使用JS语句使查询出的表格隐藏
            if (flagReceive_mtlDC == true)
            {
                Clear();
                //刷新下拉框可选PO单号
                DataSet po_noList = poDC.getAllValidPo_no();
                po_no.DataSource = po_noList.Tables[0].DefaultView;
                po_no.DataValueField = "po_no";
                po_no.DataBind();
                po_no.Items.Insert(0, "选择PO单号");

                string temp_ToastString = "该单号在数据库中存储成功！";
                PageUtil.showToast(this, temp_ToastString);
            }
            else {
                string temp_ToastString = "该单号在数据库中存储失败！请检查字符长度是否超出范围！";
                PageUtil.showToast(this, temp_ToastString);
 
            }
            this.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#foot_zbFirst').show();", true);
            //this.Clear();
            serch_POHeader.DataSource = null;
            serch_POHeader.DataBind();
        }


        //查询按钮------由暂收单号、料号、PO条件查询暂收表中数据
        protected void SerchReceive_mtl_Click(object sender, EventArgs e) 
        {
            DataSet modelReceive_mtl_list = new DataSet();
            string item_name = Item_name_Query.Value;
            modelReceive_mtl_list = poDC.getReceive_mtlBySome(Receipt_no_Query.Value, item_name, Po_no_Query.Value);
            if (modelReceive_mtl_list == null)
            {
                PageUtil.showToast(this, "未在数据库中找到对应数据");
            }
            serch_Receive_mtl.DataSource = modelReceive_mtl_list;
            serch_Receive_mtl.DataBind();
        }

        /*
        protected void Data_bind()
        {
            List<ModelReceive_mtl> modelReceive_mtl_list = new List<ModelReceive_mtl>();
            modelReceive_mtl_list.Add(new ModelReceive_mtl() { Lot_number = 0});                   
            serch_Receive_mtl.DataSource = modelReceive_mtl_list;
            serch_Receive_mtl.DataBind();        
        //protected void GridView_Page4(object sender, GridViewPageEventArgs e)
        //{
        //    GridView2.PageIndex = e.NewPageIndex;
        //    GridView2.DataBind();
        }*/

        protected void serch_Receive_mtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            serch_Receive_mtl.PageIndex = e.NewPageIndex;
            DataSet modelReceive_mtl_list = new DataSet();
            string item_name = Item_name_Query.Value;
            modelReceive_mtl_list = poDC.getReceive_mtlBySome(Receipt_no_Query.Value, item_name, Po_no_Query.Value);
            serch_Receive_mtl.DataSource = modelReceive_mtl_list;
            serch_Receive_mtl.DataBind();
        }
    }
}