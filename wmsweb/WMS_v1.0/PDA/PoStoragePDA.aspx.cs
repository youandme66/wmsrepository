using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.PDA
{
    public partial class PoStoragePDA : System.Web.UI.Page
    {
        PoDC poDC = new PoDC();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "PO入库";
            if (!IsPostBack)
            {
                List<string> frameNameList = new List<string>();
                frameNameList = poDC.getAllFrame_name();
                if (frameNameList != null)
                {
                    frame_name.DataSource = frameNameList;
                    frame_name.DataBind();
                    frame_name.Items.Insert(0, "选择料架");
                }
                else
                {
                    PageUtil.showAlert(this, "无数据载入下拉框！");
                }
            }
            this.frame_name.SelectedIndexChanged += new System.EventHandler(this.frameName_SelectedIndexChanged);
        }

        protected void frameName_SelectedIndexChanged(object sender, EventArgs e)
        {
            subinventory_name.Value = poDC.getSubinventory_nameByFrame_name(frame_name.SelectedValue);
        }



        //入库操作
        protected void PoStorage_Click(object sender, EventArgs e)
        {
            //string issued_sub_key = Request["issued_sub_key"];
            //string frame_key = Request["frame_key"];
            int rec_qty = 0, accepted_qty = 0;
            int deliver_qty = 0, subinventory_key, frame_key;
            bool table_flag, table_detail_flag;
            string status;

            string receipt_no = Rec_Num.Value;
            string item_name = ITEM_name.Value;
            string datecd = datecode.Value;
            string user_name = Session["LoginName"].ToString();

            try
            {
                frame_key = poDC.getFrame_keyByFrame_name(frame_name.SelectedValue);
                subinventory_key = poDC.getSubinventory_keyBySubinventory_name(subinventory_name.Value);

                rec_qty = int.Parse(Rec_qty.Value);
                accepted_qty = Convert.ToInt32(Accepted_qty.Value);
                deliver_qty = Convert.ToInt32(Deliver_qty.Value);
            }
            catch
            {
                PageUtil.showToast(this, "输入格式错误！");
                return;
            }
            if (rec_qty <= 0)
            {
                string temp_ToastString = "入库量应大于0,请重新填写入库量！";
                PageUtil.showToast(this, temp_ToastString);
                return;
            }
            //入库量数值判断
            if (accepted_qty < rec_qty + deliver_qty)
            {
                PageUtil.showToast(this, "已入库" + deliver_qty + "入库量不得大于允收数量！");
                return;
            }

            //判断是否修改暂收表中的该条数据的状态
            if (accepted_qty == rec_qty + deliver_qty)
            {
                status = "Y";
            }
            else
            {
                status = "N";
            }

            //判断库存总表是否已存在该料号和库别 
            DataSet table = poDC.getItems_onhand_qty_detailByITEM_NAMEandSubinventory(item_name, subinventory_name.Value);
            //判断库存从表中是否存在该料号、库别和料架
            DataSet table_detail = poDC.getItems_onhand_qty_detailByITEM_NAMEandSubinventoryandFrame_key(item_name, datecd, frame_key);
            if (table == null)
            {
                table_flag = false;
            }
            else
            {
                table_flag = true;
            }
            if (table_detail == null)
            {
                table_detail_flag = false;
            }
            else
            {
                table_detail_flag = true;
            }

            //根据以上的boolean值判断（为了来确定库存总表、明细表中数据是插入还是更新）， 来完成入库操作
            if (poDC.inStorage(receipt_no, item_name, rec_qty, datecd, frame_name.SelectedValue, subinventory_name.Value, DateTime.Now, status, table_flag, table_detail_flag, user_name) == true)
            {
                PageUtil.showToast(this, "入库成功！");
            }
            else
            {
                PageUtil.showToast(this, "入库失败！");
            }

            //查询该条入库数据
            DataSet dataSet = poDC.getReceive_mtlByNotInstorage(string.Empty, string.Empty, string.Empty);
            PoStorage_Repeater.DataSource = dataSet;
            PoStorage_Repeater.DataBind();

            frame_name.SelectedIndex = 0;
            subinventory_name.Value = string.Empty;
            Rec_Num.Value = string.Empty;
            ITEM_name.Value = string.Empty;
            datecode.Value = string.Empty;
            Rec_qty.Value = string.Empty;
        }



        // 查询操作
        protected void search_Click(object sender, EventArgs e)
        {
            string item_name = ITEM_name2.Value;
            string po_num = PO_num2.Value;
            string receipt_no = Rec_Num2.Value;
            DataSet list = poDC.getReceive_mtlByNotInstorage(receipt_no, item_name, po_num);
            if (list == null)
            {
                if (item_name == String.Empty && po_num == String.Empty && receipt_no == String.Empty)
                {
                    PageUtil.showToast(this, "暂收表中无任何数据！");
                }
                PageUtil.showToast(this, "未找到匹配信息！");
                return;
            }
            PoStorage_Repeater.DataSource = list;
            PoStorage_Repeater.DataBind();
            PO_num2.Value = string.Empty;
            Rec_Num2.Value = string.Empty;
        }


        //清除输入框数据
        protected void CleanAllMeassage_Click(object sender, EventArgs e)
        {
            ITEM_name2.Value = String.Empty;
            Rec_Num2.Value = String.Empty;
            PO_num2.Value = String.Empty;
            ITEM_name.Value = String.Empty;
            Rec_Num.Value = String.Empty;
            datecode.Value = String.Empty;
            Rec_qty.Value = String.Empty;

        }


        //protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    PoStorage_Repeater.PageIndex = e.NewPageIndex;

        //    string item_name = ITEM_name2.Value;
        //    string po_num = PO_num2.Value;
        //    string receipt_no = Rec_Num2.Value;
        //    list = REC_MT.getReceive_mtlBySome(receipt_no, item_name, po_num);

        //    PoStorage_Repeater.DataSource = list;
        //    PoStorage_Repeater.DataBind();

        //}



        //打印按钮中的确定操作----将文本框中的值传给打印界面
        protected void transPrint(object sender, EventArgs e)
        {
            ////判断搜索框中的领料单号是否存在
            //if (string.IsNullOrEmpty(select_text_print.Value) == true)
            //{
            //    Session["select_text_print"] = null;
            //    PageUtil.showToast(this, "请输入领料单号，再执行此打印操作");
            //}
            //else
            //{
            //    Session["select_text_print"] = select_text_print.Value;
            //    //打开打印界面
            //    Response.Write("<script>window.open('printIssue.aspx', 'newwindow')</script>");
            //}
        }

        //打印按钮中的取消操作------清除输入框数据
        protected void CleanInsertMessage(object sender, EventArgs e)
        {
            //select_text_print.Value = String.Empty;
        }

    }
}