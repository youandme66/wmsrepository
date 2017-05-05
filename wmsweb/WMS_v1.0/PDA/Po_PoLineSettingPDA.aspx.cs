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
    public partial class Po_PoLineSettingPDA : System.Web.UI.Page
    {
        PoDC poDC = new PoDC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                poSelect();
                List<String> vendor_key_List = new List<String>();
                vendor_key_List = poDC.getAllVendor_key();
                if (vendor_key_List != null)
                {
                    vendor_keys.DataSource = vendor_key_List;
                    vendor_keys.DataBind();
                    vendor_keys.Items.Insert(0, "选择厂商代码");
                }
                else
                {

                    PageUtil.showAlert(this, "无数据载入下拉框！");
                }
            }

            Session["Local"] = "PO/POLine设定";

        }


        //PO单头的查询按钮------查询PO单头中的信息
        protected void QueryPO_Click(object sender, EventArgs e)
        {
            string Po_no = po_no.Value;
            try
            {
                //需要try，无数据时会报错
                string Vendor_key = vendor_keys.SelectedItem.Text;
                if (Vendor_key.Equals("选择厂商代码"))
                    Vendor_key = "";
                DataSet modelPO_Header_List = poDC.getPO_HeaderBySome(Po_no, Vendor_key);
                //用 modelPO_Header_List == null 判断不出来DataSet为空
                if (modelPO_Header_List == null)
                {
                    PageUtil.showToast(this, "查询失败，可能数据库中没数据");
                    po_no.Value = string.Empty;
                    vendor_keys.SelectedValue = string.Empty;

                }
                PO_Repeater.DataSource = modelPO_Header_List;
                PO_Repeater.DataBind();
            }
            catch (Exception e1)
            {
                PageUtil.showToast(this, "查询失败，数据库中没数据");
            }
        }


        //PO单头的插入按钮------往ＰＯ单头表中插入一条数据
        protected void InsertPO_Click(object sender, EventArgs e)
        {
            if (Session["LoginId"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }
            int create_user = int.Parse(Session["LoginId"].ToString());
            string Po_no_Insert = po_no_Insert.Value;

            string Vendor_key_Insert = Request.Form["vendor_key_Insert"];
            DateTime UPDATE_TIME = new DateTime();
            UPDATE_TIME = DateTime.Now;
            if (string.IsNullOrWhiteSpace(Po_no_Insert))
            {
                PageUtil.showToast(this, "请输入完整信息");
                return;
            }
            try
            {
                if (poDC.getPOHeaderByPo_no(Po_no_Insert).Tables[0].Rows.Count >= 1)
                {
                    //PO单号重复
                    PageUtil.showToast(this, "该PO单头已存在！");
                    return;
                }
                //调用PO单头方法存储数据
                bool flag_POHeaderDC = poDC.insertPoHeader(Po_no_Insert, Vendor_key_Insert, create_user, UPDATE_TIME);
                if (flag_POHeaderDC)
                {
                    PageUtil.showToast(this, "插入成功!");
                    po_no_Insert.Value = String.Empty;
                    DataSet modelPO_Header_List = new DataSet();
                    modelPO_Header_List = poDC.getPOHeaderByPo_no(Po_no_Insert);
                    PO_Repeater.DataSource = modelPO_Header_List;
                    PO_Repeater.DataBind();


                }
                else
                {
                    PageUtil.showToast(this, "插入失败!请检查字符长度是否超出范围！");
                    po_no_Insert.Value = String.Empty;
                }
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "请按格式输入！");
                po_no_Insert.Value = String.Empty;
            }
        }


        //PO单头的更新按钮------更新ＰＯ单头表中具体数据的部分字段
        protected void UpdatePO_Click(object sender, EventArgs e)
        {
            DataSet modelPO_Header_List = new DataSet();
            int create_user = int.Parse(Session["LoginId"].ToString());
            string Po_no_Update = po_no_Update.Value;

            string Vendor_key_Update = Request.Form["vendor_key_Update"];
            DateTime UPDATE_TIME = new DateTime();
            UPDATE_TIME = DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
            //若单号已经处于暂收即不允许再进行修改
            if (poDC.getReceive_mtlByPo_no(Po_no_Update))
            {
                PageUtil.showToast(this, Po_no_Update + "单号已经开始暂收了，不可再修改!");
                return;
            }
            try
            {
                //调用PO单头方法修改数据
                bool flag_POHeaderDC = poDC.updatePoHeader(Po_no_Update, Vendor_key_Update, create_user, UPDATE_TIME);
                if (flag_POHeaderDC)
                {
                    PageUtil.showToast(this, "修改成功!");
                    //进行模糊查询
                    modelPO_Header_List = poDC.getPO_HeaderBySome(Po_no_Update, Vendor_key_Update);
                    PO_Repeater.DataSource = modelPO_Header_List;
                    PO_Repeater.DataBind();
                }
                else
                {
                    PageUtil.showToast(this, "修改失败!请检查字符长度是否超出范围！");
                }
            }
            catch
            {
                PageUtil.showToast(this, "请按格式输入！");
            }
        }


        //PO单头的删除按钮------删除ＰＯ单头表中具体数据，对应的PO单身信息也会一并被删除
        protected void DeletPO_Click(object sender, EventArgs e)
        {
            string Po_no_delet = po_no_delet.Value;

            //若单号已经处于暂收即不允许再进行删除
            if (poDC.getReceive_mtlByPo_no(Po_no_delet))
            {
                PageUtil.showToast(this, Po_no_delet + "单号已经开始暂收了，不可删除!");
                return;
            }

            //调用PO单头方法删除数据
            bool flag_POHeaderDC = poDC.deletePoHeader(Po_no_delet);
            if (flag_POHeaderDC)
            {
                PageUtil.showToast(this, "删除成功!");
            }
            else
            {
                PageUtil.showToast(this, "删除失败!");
            }
            DataSet PO_Header_List = new DataSet();
            //查询所有数据
            PO_Header_List = poDC.getPO_Header();
            PO_Repeater.DataSource = PO_Header_List;
            PO_Repeater.DataBind();
        }




        //PO单身的插入按钮------往ＰＯ单身表中插入一条数据
        protected void InsertPO_Line_Click(object sender, EventArgs e)
        {
            List<String> line_num__list = new List<string>();
            string Cancel_flag_insertPoLine = cancel_flag_insertPoLine.Value;
            int item_id = 0, Request_qty_insertPoLine = 0;
            string PO_no = PO_NO1.Value;
            try
            {
                string item = Request.Form["Item"];//现在传进来的是item_name

                //判断，po单号下的单身中料号不可重复添加，应该直接修改已有单身！
                DataSet list = poDC.getPOLineByPo_noAndItem_name(PO_no, item);
                if (list != null)
                {
                    PageUtil.showToast(this, "单身中已有相同料号，请在原有单身上进行修改!");
                    return;
                }
                //将item_name转换成item_id
                PnDC pnDC = new PnDC();
                item_id = poDC.getItem_idByItem_name(item);

                //Item_id_insertPoLine = int.Parse(item);
                Request_qty_insertPoLine = int.Parse(request_qty_insertPoLine.Value);

                //调用PO单身的方法插入数据
                bool flag_poLineDC = poDC.insertPO_line(PO_no, item_id, Request_qty_insertPoLine, Cancel_flag_insertPoLine);
                if (flag_poLineDC)
                {
                    DataSet ds2 = new DataSet();
                    ds2 = poDC.getPOLineByPo_no(PO_no);
                    po_Line_Repeater.DataSource = ds2;
                    po_Line_Repeater.DataBind();

                    poSelect();
                    this.CleanPO_Line_Insert();
                    PageUtil.showToast(this, "插入成功!");

                }
                else
                {
                    PageUtil.showToast(this, "插入失败!请检查输入唯一性");
                    this.CleanPO_Line_Insert();
                }
            }
            catch
            {
                PageUtil.showToast(this, "请按格式输入！");
                this.CleanPO_Line_Insert();
            }

        }




        //PO单身的查询按钮------查询PO单身中的信息
        public void poSelect()
        {
            DataSet ds = new DataSet();
            ds = poDC.getPOLineandPo_no();
            po_Line_Repeater.DataSource = ds;
            po_Line_Repeater.DataBind();
        }


        //private ModelPO_line toModel(DataRow dr)
        //{
        //    ModelPO_line model = new ModelPO_line();

        //    foreach (PropertyInfo propertyInfo in typeof(ModelPO_line).GetProperties())
        //    {
        //        //如果数据库的字段为空，跳过其赋值
        //        if (dr[propertyInfo.Name].ToString() == "")
        //        {
        //            continue;
        //        }
        //        //赋值
        //        model.GetType().GetProperty(propertyInfo.Name).SetValue(model, dr[propertyInfo.Name], null);
        //    }

        //    return model;
        //}


        //protected void QueryPO_Line_Click(object sender, EventArgs e)
        //{

        //    int Po_header_id_Query;
        //    try
        //    {
        //        Po_header_id_Query = int.Parse(po_header_id_Query.Value);

        //    }
        //    catch
        //    {
        //        PageUtil.showToast(this, "未在数据库中查出对应数据，请检查是否输入有问题");
        //        return;
        //    }
        //    //查询
        //    modelPO_line_List = poLineDC.getPOLineByPo_header_id(Po_header_id_Query);
        //    if (modelPO_line_List == null)
        //    {
        //        PageUtil.showToast(this, "未在数据库中查出对应数据，请检查是否输入有问题");
        //    }
        //    po_Line_Repeater.DataSource = modelPO_line_List;
        //    po_Line_Repeater.DataBind();
        //}



        //PO单身的修改按钮------修改PO单身中的信息
        protected void UpdatePO_Line_Click(object sender, EventArgs e)
        {

            string Cancel_flag_Update_poLine = cancel_flag_Update_poLine.Value;
            int item_id1 = 0, Request_qty_Update_poLine = 0;
            try
            {
                string PO_no2 = PO_NO2.Value;

                string item1 = Request.Form["Item1"];//现在传进来的是item_name
                //将item_name转换成item_id
                item_id1 = poDC.getItem_idByItem_name(item1);

                Request_qty_Update_poLine = int.Parse(request_qty_Update_poLine.Value);
                //若单号已经处于暂收即不允许再进行修改
                if (poDC.getReceive_mtlByPo_no(PO_no2))
                {
                    PageUtil.showToast(this, PO_no2 + "单号已经开始暂收了，不可再修改!");
                    return;
                }

                //调用PO单身的方法修改数据
                bool flag_poLineDC = poDC.updatePO_line(PO_no2, item_id1, Request_qty_Update_poLine, Cancel_flag_Update_poLine, int.Parse(line_num_Update_poLine.Value));
                if (flag_poLineDC)
                {
                    PageUtil.showToast(this, "修改成功!");
                    poSelect();
                }
                else
                {
                    PageUtil.showToast(this, "修改失败!请检查字符长度是否超出范围！");
                    this.CleanPO_Line_Update();
                }
            }
            catch
            {
                PageUtil.showToast(this, "请按格式输入！");
                this.CleanPO_Line_Update();
            }

        }


        //PO单身的删除按钮------删除PO单身中的信息
        protected void DeletPO_Line_Click(object sender, EventArgs e)
        {

            //若单号已经处于暂收即不允许再进行删除
            if (poDC.getReceive_mtlByPo_no(po_no_Delet_poLine.Value))
            {
                PageUtil.showToast(this, po_no_Delet_poLine.Value + "单号已经开始暂收了，不可删除!");
                return;
            }
            //调用PO单身方法删除数据
            bool flag_poLineDC = poDC.deletePO_lineByPo_line_Id(int.Parse(po_line_id_delet.Value));
            if (flag_poLineDC)
            {
                PageUtil.showToast(this, "删除成功!");
                DataSet ds1 = new DataSet();
                ds1 = poDC.getPOLineByPo_no(po_no_Delet_poLine.Value);
                po_Line_Repeater.DataSource = ds1;
                po_Line_Repeater.DataBind();
            }
            else
            {
                PageUtil.showToast(this, "删除失败!");
            }
        }


        //清除按钮操作------清除输入框中数据
        protected void CleanAllMeassage_Click(object sender, EventArgs e)
        {
            po_no.Value = String.Empty;
            vendor_keys.SelectedIndex = 0;
            PO_Repeater.DataSource = null;
            PO_Repeater.DataBind();
            po_Line_Repeater.DataSource = null;
            po_Line_Repeater.DataBind();
            PageUtil.showToast(this, "成功清除输入框数据");
        }


        //PO单头插入按钮中的取消操作------清除输入框数据
        protected void CleanPO_Insert_Click(object sender, EventArgs e)
        {
            po_no_Insert.Value = String.Empty;
        }


        ////PO单头更新按钮中的取消操作------清除输入框数据
        //protected void CleanPO_Update_Click(object sender, EventArgs e)
        //{
        //    vendor_key_Update.Value = String.Empty;
        //}


        //PO单身插入按钮中的取消操作------清除输入框数据
        protected void CleanPO_Line_Insert_Click(object sender, EventArgs e)
        {
            this.CleanPO_Line_Insert();
        }
        protected void CleanPO_Line_Insert()
        {
            //item.Value = String.Empty;
            request_qty_insertPoLine.Value = String.Empty;
        }


        //PO单身更新按钮中的取消操作------清除输入框数据
        protected void CleanPO_Line_Update_Click(object sender, EventArgs e)
        {
            this.CleanPO_Line_Update();
        }

        protected void CleanPO_Line_Update()
        {
            //item_id_Update_poLine.Value = String.Empty;
            request_qty_Update_poLine.Value = String.Empty;
        }



    }
}

