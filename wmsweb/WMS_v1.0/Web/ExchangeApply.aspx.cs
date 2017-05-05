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
using System.Text.RegularExpressions;

namespace WMS_v1._0.Web
{
    public partial class ExchangeApply : System.Web.UI.Page
    {
        InvoiceDC invoiceDC = new InvoiceDC();
        string user;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "调拨单申请";
            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }
            else
                user = Session["LoginName"].ToString();


            if (!IsPostBack)
            {
                DataSet ds_Flax_value = invoiceDC.getFlex_value();
                DataSet ds_Route = invoiceDC.getRoute();
                //DataSet ds_out_frame_key = invoiceDC.getFrameByMaterial_io();
                //DataSet ds_in_frame_key = invoiceDC.getFrame();
                DataSet ds_out_subinventory = invoiceDC.getSubinventory();
                DataSet ds_in_subinventory = invoiceDC.getSubinventory_name();
                if (ds_Flax_value != null)
                {
                    DropDownList_Flax_value.DataSource = ds_Flax_value.Tables[0].DefaultView;
                    DropDownList_Flax_value.DataValueField = "flex_value";
                    DropDownList_Flax_value.DataTextField = "flex_value";
                    DropDownList_Flax_value.DataBind();
                }
                if (ds_Route != null)
                {
                    DropDownList_operation_seq_num.DataSource = ds_Route.Tables[0].DefaultView;
                    DropDownList_operation_seq_num.DataTextField = "route";
                    DropDownList_operation_seq_num.DataValueField = "route_id";
                    DropDownList_operation_seq_num.DataBind();
                }
                if (ds_out_subinventory != null)
                {
                    DropDownList_out_subinventory.DataSource = ds_out_subinventory.Tables[0].DefaultView;
                    DropDownList_out_subinventory.DataTextField = "subinventory";
                    DropDownList_out_subinventory.DataValueField = "subinventory_key";
                    DropDownList_out_subinventory.DataBind();
                }
                if (ds_in_subinventory != null)
                {
                    DropDownList_in_subinventory.DataSource = ds_in_subinventory.Tables[0].DefaultView;
                    DropDownList_in_subinventory.DataTextField = "subinventory_name";
                    DropDownList_in_subinventory.DataValueField = "subinventory_key";
                    DropDownList_in_subinventory.DataBind();
                }
                //if (ds_out_frame_key != null)
                //{
                //    DropDownList_out_frame_key.DataSource = ds_out_frame_key.Tables[0].DefaultView;
                //    DropDownList_out_frame_key.DataTextField = "frame_name";
                //    DropDownList_out_frame_key.DataValueField = "frame_name";
                //    DropDownList_out_frame_key.DataBind();
                //}
                //if (ds_in_frame_key != null)
                //{
                //    DropDownList_in_frame_key.DataSource = ds_in_frame_key.Tables[0].DefaultView;
                //    DropDownList_in_frame_key.DataTextField = "frame_name";
                //    DropDownList_in_frame_key.DataValueField = "frame_name";
                //    DropDownList_in_frame_key.DataBind();
                //}
                                        
                DropDownList_Flax_value.Items.Insert(0, "--选择部门代码--");
                DropDownList_description.Items.Insert(0, "--选择部门名称--");
                DropDownList_item_name.Items.Insert(0, "--选择料号--");
                DropDownList_operation_seq_num.Items.Insert(0, "--选择制程--");
                DropDownList_out_subinventory.Items.Insert(0, "--选择调出库别--");
                DropDownList_in_subinventory.Items.Insert(0, "--选择调入库别--");
                //DropDownList_out_frame_key.Items.Insert(0, "--选择调出料架--");
                //DropDownList_in_frame_key.Items.Insert(0, "--选择调入料架--");               
            }
        }

        //部门代码与部门名称的二级联动，选择部门代码时触发
        protected void DropDownList_Flax_value_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            try
            {
                DropDownList_description.Items.Clear();

                string text = DropDownList_Flax_value.SelectedValue.ToString();
                DataSet ds_description = invoiceDC.getDescriptionByFlex_value(text);
                if (ds_description != null)
                {
                    DropDownList_description.DataSource = ds_description.Tables[0].DefaultView;
                    DropDownList_description.DataValueField = "description";
                    DropDownList_description.DataTextField = "description";
                    DropDownList_description.DataBind();

                }

                DropDownList_description.Items.Insert(0, "--选择部门名称--");
                DropDownList_description.SelectedIndex = 1;
            }
            catch (Exception e2)
            {
                PageUtil.showToast(this, "部门代码与部门名称二级联动失败，请检查是否有关联数据");
                return;
            }
        }

        //调出库别发生改变时
        protected void DropDownList_out_subinventory_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (invoice_no.Value == "")
            {
                PageUtil.showToast(this, "请输入调拨单号再操作");
                return;
            }
            DataSet ds = invoiceDC.getExchangeHeaderBySome(invoice_no.Value, "");
            if (ds == null)
            {
                PageUtil.showToast(this, "请确保该调拨单号存在，再操作");
                return;
            }

            //调出库别与料号的二级联动
            try
            {
                DropDownList_item_name.Items.Clear();

                int text = int.Parse(DropDownList_out_subinventory.SelectedValue.ToString());
                DataSet ds_item_name = invoiceDC.getItem_nameBySubinventory(text);
                if (ds_item_name != null)
                {
                    DropDownList_item_name.DataSource = ds_item_name.Tables[0].DefaultView;
                    DropDownList_item_name.DataValueField = "item_name";
                    DropDownList_item_name.DataTextField = "item_name";
                    DropDownList_item_name.DataBind();
                }

                DropDownList_item_name.Items.Insert(0, "--选择料号--");
                DropDownList_item_name.SelectedIndex = 1;
            }
            catch (Exception e2)
            {
                PageUtil.showToast(this, "调出库别与料号二级联动失败，请检查库存总表是否有关联数据");
                return;
            }
        }

        ////调出料架发生改变时
        //protected void DropDownList_out_frame_key_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (invoice_no.Value == "")
        //    {
        //        PageUtil.showToast(this, "请输入调拨单号再操作");
        //        return;
        //    }
        //    DataSet ds = invoiceDC.getExchangeHeaderBySome(invoice_no.Value, "");
        //    if (ds == null)
        //    {
        //        PageUtil.showToast(this, "请确保该调拨单号存在，再操作");
        //        return;
        //    }

        //    //调出料架与调出库别的二级联动
        //    try
        //    {
        //        DropDownList_out_subinventory.Items.Clear();

        //        string text = DropDownList_out_frame_key.SelectedValue.ToString();
        //        DataSet ds_out_subinventory = invoiceDC.getSubinventoryByFrame(text);
        //        if (ds_out_subinventory != null)
        //        {
        //            DropDownList_out_subinventory.DataSource = ds_out_subinventory.Tables[0].DefaultView;
        //            DropDownList_out_subinventory.DataValueField = "subinventory_key";
        //            DropDownList_out_subinventory.DataTextField = "subinventory_name";
        //            DropDownList_out_subinventory.DataBind();
        //        }

        //        DropDownList_out_subinventory.Items.Insert(0, "--选择调出库别--");
        //        DropDownList_out_subinventory.SelectedIndex = 1;
        //    }
        //    catch (Exception e2)
        //    {
        //        PageUtil.showToast(this, "调出料架与调出库别二级联动失败，请检查是否有关联数据");
        //        return;
        //    }

        //    //调出料架与料号的二级联动
        //    try
        //    {
        //        DropDownList_item_name.Items.Clear();

        //        string text = DropDownList_out_frame_key.SelectedValue.ToString();
        //        DataSet ds_item_name = invoiceDC.getItem_nameByFrame(text);
        //        if (ds_item_name != null)
        //        {
        //            DropDownList_item_name.DataSource = ds_item_name.Tables[0].DefaultView;
        //            DropDownList_item_name.DataValueField = "item_name";
        //            DropDownList_item_name.DataTextField = "item_name";
        //            DropDownList_item_name.DataBind();
        //        }

        //        DropDownList_item_name.Items.Insert(0, "--选择料号--");
        //        DropDownList_item_name.SelectedIndex = 1;
        //    }
        //    catch (Exception e2)
        //    {
        //        PageUtil.showToast(this, "调出料架与料号二级联动失败，请检查库存明细表是否有关联数据");
        //        return;
        //    }


        //    ////调出料架与料号确定库存明细表中的调拨上限
        //    //try
        //    //{
        //    //    string frame_key = DropDownList_out_frame_key.SelectedValue.ToString();
        //    //    string item_name = DropDownList_item_name.SelectedValue.ToString();
        //    //    DataSet ds2 = invoiceDC.getOnhandQty(frame_key, item_name);

        //    //    exchange_limit.Value = ds2.Tables[0].Rows[0]["onhand_qty"].ToString();
        //    //}
        //    //catch (Exception e2)
        //    //{
        //    //    PageUtil.showToast(this, "获取调拨上限失败，请检查是否有关联数据");
        //    //    return;
        //    //}

        //    ////料号与调入料架的二级联动        
        //    //try
        //    //{
        //    //    DropDownList_in_frame_key.Items.Clear();

        //    //    string text = DropDownList_item_name.SelectedValue.ToString();
        //    //    DataSet ds_in_frame_key = invoiceDC.getFrameByItem_name(text);
        //    //    if (ds_in_frame_key != null)
        //    //    {
        //    //        DropDownList_in_frame_key.DataSource = ds_in_frame_key.Tables[0].DefaultView;
        //    //        DropDownList_in_frame_key.DataValueField = "frame_name";
        //    //        DropDownList_in_frame_key.DataTextField = "frame_name";
        //    //        DropDownList_in_frame_key.DataBind();
        //    //    }

        //    //    DropDownList_in_frame_key.Items.Insert(0, "--选择调入料架--");
        //    //    DropDownList_in_frame_key.SelectedIndex = 1;
        //    //}
        //    //catch (Exception e2)
        //    //{
        //    //    PageUtil.showToast(this, "料号与调入料架二级联动失败，请检查是否有关联数据");
        //    //    return;
        //    //}

        //    ////调入料架与调入库别的二级联动
        //    //try
        //    //{
        //    //    DropDownList_in_subinventory.Items.Clear();

        //    //    string text = DropDownList_in_frame_key.SelectedValue.ToString();
        //    //    DataSet ds_in_subinventory = invoiceDC.getSubinventoryByFrame(text);
        //    //    if (ds_in_subinventory != null)
        //    //    {
        //    //        DropDownList_in_subinventory.DataSource = ds_in_subinventory.Tables[0].DefaultView;
        //    //        DropDownList_in_subinventory.DataValueField = "subinventory_key";
        //    //        DropDownList_in_subinventory.DataTextField = "subinventory_name";
        //    //        DropDownList_in_subinventory.DataBind();
        //    //    }

        //    //    DropDownList_in_subinventory.Items.Insert(0, "--选择调入库别--");
        //    //    DropDownList_in_subinventory.SelectedIndex = 1;
        //    //}
        //    //catch (Exception e2)
        //    //{
        //    //    PageUtil.showToast(this, "调入料架与调入库别二级联动失败，请检查是否有关联数据");
        //    //    return;
        //    //}
        //}

        ////调出料号改变时
        //protected void DropDownList_item_name_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (invoice_no.Value == "")
        //    {
        //        PageUtil.showToast(this, "请输入调拨单号再操作");
        //        return;
        //    }
        //    DataSet ds = invoiceDC.getExchangeHeaderBySome(invoice_no.Value, "");
        //    if (ds == null)
        //    {
        //        PageUtil.showToast(this, "请确保该调拨单号存在，再操作");
        //        return;
        //    }

        //    ////调出料号改变时，改变调拨上限
        //    //try
        //    //{
        //    //    string frame_key = DropDownList_out_frame_key.SelectedValue.ToString();
        //    //    string item_name = DropDownList_item_name.SelectedValue.ToString();
        //    //    DataSet ds2 = invoiceDC.getOnhandQty(frame_key, item_name);

        //    //    exchange_limit.Value = ds2.Tables[0].Rows[0]["onhand_qty"].ToString();
        //    //}
        //    //catch (Exception e2)
        //    //{
        //    //    PageUtil.showToast(this, "获取调拨上限失败，请检查是否有关联数据");
        //    //    return;
        //    //}

        //    //料号与调入料架的二级联动        
        //    try
        //    {
        //        DropDownList_in_frame_key.Items.Clear();

        //        string text = DropDownList_item_name.SelectedValue.ToString();
        //        DataSet ds_in_frame_key = invoiceDC.getFrameByItem_name(text);
        //        if (ds_in_frame_key != null)
        //        {
        //            DropDownList_in_frame_key.DataSource = ds_in_frame_key.Tables[0].DefaultView;
        //            DropDownList_in_frame_key.DataValueField = "frame_name";
        //            DropDownList_in_frame_key.DataTextField = "frame_name";
        //            DropDownList_in_frame_key.DataBind();
        //        }

        //        DropDownList_in_frame_key.Items.Insert(0, "--选择调入料架--");
        //        DropDownList_in_frame_key.SelectedIndex = 1;
        //    }
        //    catch (Exception e2)
        //    {
        //        PageUtil.showToast(this, "料号与调入料架二级联动失败，请检查是否有关联数据");
        //        return;
        //    }

        //    //调入料架与调入库别的二级联动
        //    try
        //    {
        //        DropDownList_in_subinventory.Items.Clear();

        //        string text = DropDownList_in_frame_key.SelectedValue.ToString();
        //        DataSet ds_in_subinventory = invoiceDC.getSubinventoryByFrame(text);
        //        if (ds_in_subinventory != null)
        //        {
        //            DropDownList_in_subinventory.DataSource = ds_in_subinventory.Tables[0].DefaultView;
        //            DropDownList_in_subinventory.DataValueField = "subinventory_key";
        //            DropDownList_in_subinventory.DataTextField = "subinventory_name";
        //            DropDownList_in_subinventory.DataBind();
        //        }

        //        DropDownList_in_subinventory.Items.Insert(0, "--选择调入库别--");
        //        DropDownList_in_subinventory.SelectedIndex = 1;
        //    }
        //    catch (Exception e2)
        //    {
        //        PageUtil.showToast(this, "调入料架与调入库别二级联动失败，请检查是否有关联数据");
        //        return;
        //    }
        //}


        ////调入料架发生改变时
        //protected void DropDownList_in_frame_key_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (invoice_no.Value == "")
        //    {
        //        PageUtil.showToast(this, "请输入调拨单号再操作");
        //        return;
        //    }
        //    DataSet ds = invoiceDC.getExchangeHeaderBySome(invoice_no.Value, "");
        //    if (ds == null)
        //    {
        //        PageUtil.showToast(this, "请确保该调拨单号存在，再操作");
        //        return;
        //    }

        //    //调入料架与调入库别的二级联动
        //    try
        //    {
        //        DropDownList_in_subinventory.Items.Clear();

        //        string text = DropDownList_in_frame_key.SelectedValue.ToString();
        //        DataSet ds_in_subinventory = invoiceDC.getSubinventoryByFrame(text);
        //        if (ds_in_subinventory != null)
        //        {
        //            DropDownList_in_subinventory.DataSource = ds_in_subinventory.Tables[0].DefaultView;
        //            DropDownList_in_subinventory.DataValueField = "subinventory_key";
        //            DropDownList_in_subinventory.DataTextField = "subinventory_name";
        //            DropDownList_in_subinventory.DataBind();
        //        }

        //        DropDownList_in_subinventory.Items.Insert(0, "--选择调入库别--");
        //        DropDownList_in_subinventory.SelectedIndex = 1;
        //    }
        //    catch (Exception e2)
        //    {
        //        PageUtil.showToast(this, "调入料架与调入库别二级联动失败，请检查是否有关联数据");
        //        return;
        //    }
        //}

       
        //生成一个调拨单单据号
        protected void NewInvoice_no(object sender, EventArgs e)
        {
            string strInvoice_no = invoice_no.Value;

            if (!string.IsNullOrWhiteSpace(strInvoice_no))
            {

                PageUtil.showToast(this, "单据号已生成，请勿重复操作");

                return;

            }

            int end;
            string end_text;
            string month = string.Empty;
            string year = string.Empty;
            DataSet ds;

            ds = invoiceDC.getTheNewestExchangeHeaderByCreatetime();

            //判断数据库是否有数据,没有数据则初始化一个流水号
            if (ds == null)
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
                end = int.Parse(ds.Tables[0].Rows[0]["exchange_header_id"].ToString());

                //截取上一个单号的后5位,即从第5位开始的5位（0开始增）
                string sInvoice_no = ds.Tables[0].Rows[0]["invoice_no"].ToString();
                string result = sInvoice_no.Substring(5, 5);
                end = int.Parse(result);

                string last_month = sInvoice_no.Substring(3, 2);
                string last_year = sInvoice_no.Substring(1, 2);

                ////判断id是否为99998,是则取下一个流水号为99999,否则取余+1作为下一个流水号
                //end = end % 99998 == 0 ? 99998 + 1 : end % 99999 + 1;

                //当end为99999时，重置从1开始计数。否则，递增
                if (end == 99999)
                    end = 1;
                else
                    end = end + 1;

                end_text = end.ToString();


                //为流水号补0
                for (int i = end_text.Length; i < 5; i++)
                {

                    end_text = "0" + end_text;

                }

                month = DateTime.Now.Month.ToString();

                year = DateTime.Now.Year.ToString();

                year = year.Substring(2, 2);

                for (int i = month.Length; i < 2; i++)
                {

                    month = "0" + month;

                }

                //新月份或新的一年，从1重新计数
                if (month != last_month)
                    end_text = "00001";
                if (year != last_year)
                    end_text = "00001";
            }

            invoice_no.Value = "T" + year + month + end_text;
        }




        //提交调拨单单头
        protected void Exchange_Header_Commit(object sender, EventArgs e)
        {
            try
            {
                string Invoice_no = invoice_no.Value;
                string Flex_value = DropDownList_Flax_value.SelectedValue.ToString();
                string Description = DropDownList_description.SelectedValue.ToString();

                if (Flex_value == "--选择部门代码--" || Description == "--选择部门名称--")
                {
                    PageUtil.showToast(this, "请将信息填写完全");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Invoice_no))
                {
                    PageUtil.showToast(this, "请生成单据号或填写单据号再操作");
                    return;
                }
                if (invoiceDC.getExchangeHeaderBySome(Invoice_no,"") != null)
                {
                    PageUtil.showToast(this, "单据号已存在，请重新生成");
                    return;
                }

                //对单据号的判断，防止乱输入单据号 @是将不能转义的字符转义
                Regex r = new Regex(@"^[T][1-9]\d{8}$");
                Match m = r.Match(Invoice_no.Trim());
                if (m.Success == false)
                {
                    PageUtil.showToast(this, "请按规则输入单据号");
                    return;
                } 

                bool flag = invoiceDC.insertExchange_header(Invoice_no, Flex_value, Description);

                if (flag == true)
                {
                    PageUtil.showToast(this, "生成调拨单单头成功");
                    DropDownList_Flax_value.SelectedValue = "--选择部门代码--";
                    DropDownList_description.SelectedValue = "--选择部门名称--";
                }
                else
                    PageUtil.showToast(this, "插入失败");
            }
            catch (Exception e1)
            {
                PageUtil.showToast(this, "插入失败");
            }


        }

        //添加调拨单单身到框框内（并未插入到数据库）
        protected void Exchange_Line_Commit(object sender, EventArgs e)
        {
            try
            {
                string Invoice_no = invoice_no.Value;
                if (string.IsNullOrWhiteSpace(invoice_no.Value))
                {
                    PageUtil.showToast(this, "请填写单据号");
                    return;
                }
                if (invoiceDC.getExchangeHeaderBySome(Invoice_no,"") == null)
                {
                    PageUtil.showToast(this, "请确定单据号存在");
                    return;
                }
    
                string Item_name = DropDownList_item_name.SelectedValue.ToString();
                //检查信息 
                if ( DropDownList_item_name.SelectedValue.ToString() == "--选择料号--"||DropDownList_operation_seq_num.SelectedValue.ToString() == "--选择制程--")
                {
                    PageUtil.showToast(this, "请选择料号和制程");
                    return;
                }
                int Operation_seq_num = int.Parse(DropDownList_operation_seq_num.SelectedValue.ToString());

                if (DropDownList_out_subinventory.SelectedValue.ToString() == "--选择调出库别--" || DropDownList_in_subinventory.SelectedValue.ToString() == "--选择调入库别--")
                {
                    PageUtil.showToast(this, "请选择调出库别和调入库别");
                    return;
                }

                //if(DropDownList_out_frame_key.SelectedValue.ToString() == "--选择调出料架--" || DropDownList_in_frame_key.SelectedValue.ToString() == "--选择调入料架--")
                //{
                //    PageUtil.showToast(this, "请选择调出料架和调入料架");
                //    return;
                //}

                int Out_subinventory = int.Parse(DropDownList_out_subinventory.SelectedValue.ToString());
                int in_subinventory = int.Parse(DropDownList_in_subinventory.SelectedValue.ToString());

                if (exchanged_qty.Value == "" || required_qty.Value == "")
                {
                    PageUtil.showToast(this, "请填写数量完整");
                    return;
                }

                int Exchanged_qty = int.Parse(exchanged_qty.Value);    
                int Required_qty = int.Parse(required_qty.Value);
                
                if (Exchanged_qty <= 0 || Required_qty <= 0)
                {
                    PageUtil.showToast(this, "数量应大于0");
                    return;
                }
                if (Exchanged_qty > Required_qty)
                {
                    PageUtil.showToast(this, "调拨数量不能大于需求量");
                    return;
                }
               




                DataTable table = new DataTable();

                table = GetGridViewData(table);

                DataRow sourseRow = table.NewRow();
              
                sourseRow["item_name"] = Item_name;
                sourseRow["operation_seq_num"] = Operation_seq_num;
                sourseRow["out_subinventory"] = Out_subinventory;
                //sourseRow["out_frame_key"] = DropDownList_out_frame_key.SelectedValue.ToString();
                sourseRow["in_subinventory"] = in_subinventory;
                //sourseRow["in_frame_key"] = DropDownList_in_frame_key.SelectedValue.ToString();
                sourseRow["required_qty"] = Required_qty;
                sourseRow["exchanged_qty"] = Exchanged_qty;
                sourseRow["create_man"] = user;
                sourseRow["create_time"] = DateTime.Now;

                table.Rows.Add(sourseRow);

                GridView1.DataSource = table;

                GridView1.DataBind();

                //提交单个单身数据成功时，清除用户已填写的单身数据，方便用户填写下一个单身
             
                DropDownList_item_name.SelectedValue = "--选择料号--";
                DropDownList_operation_seq_num.SelectedValue = "--选择制程--";
                DropDownList_out_subinventory.SelectedValue= "--选择调出库别--";
                DropDownList_in_subinventory.SelectedValue = "--选择调入库别--";
                //DropDownList_out_frame_key.SelectedValue = "--选择调出料架--";
                //DropDownList_in_frame_key.SelectedValue = "--选择调入料架--";
            
                required_qty.Value = "";
                exchanged_qty.Value = "";
                
            }
            catch (Exception e2)
            {
                PageUtil.showToast(this, "添加单身失败");
            }

        }

        //提交所有的调拨单单身到数据库
        protected void Commit_All_Exchange_Line(object sender, EventArgs e)
        {
            string Invoice_no = invoice_no.Value;

            DataTable table = new DataTable();

            table = GetGridViewData(table);

            if (table.Rows.Count == 0)
            {
                PageUtil.showToast(this, "请将单身添加到右方表格,再操作");
                return;
            }

            try
            {
                bool flag = invoiceDC.insertExchange_line(table, Invoice_no);
                if (flag == true)
                {
                    PageUtil.showToast(this, "成功提交所有单身！");

                    //清空框框，避免用户重复提交
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    DropDownList_item_name.SelectedValue = "--选择料号--";
                    DropDownList_operation_seq_num.SelectedValue = "--选择制程--";
                    DropDownList_out_subinventory.SelectedValue = "--选择调出库别--";
                    DropDownList_in_subinventory.SelectedValue = "--选择调入库别--";
                    //DropDownList_out_frame_key.SelectedValue = "--选择调出料架--";
                    //DropDownList_in_frame_key.SelectedValue = "--选择调入料架--";

                    required_qty.Value = "";
                    exchanged_qty.Value = "";
                }
                else
                    PageUtil.showToast(this, "信息提交失败！");
            }
            catch (Exception)
            {
                PageUtil.showToast(this, "信息提交失败！");
                return;
            }
        }

        //获取页面上表格中的所有行,并组装为tabel
        protected DataTable GetGridViewData(DataTable table)
        {
            table.Columns.Add(new DataColumn("item_name"));
            table.Columns.Add(new DataColumn("operation_seq_num"));
            table.Columns.Add(new DataColumn("out_subinventory"));
            //table.Columns.Add(new DataColumn("out_frame_key"));
            table.Columns.Add(new DataColumn("in_subinventory"));
            //table.Columns.Add(new DataColumn("in_frame_key"));
            table.Columns.Add(new DataColumn("required_qty"));
            table.Columns.Add(new DataColumn("exchanged_qty"));
            table.Columns.Add(new DataColumn("create_man"));
            table.Columns.Add(new DataColumn("create_time"));

            foreach (GridViewRow row in GridView1.Rows)
            {
                DataRow sourseRow = table.NewRow();
              
                sourseRow["item_name"] = row.Cells[0].Text;
                sourseRow["operation_seq_num"] = row.Cells[1].Text;
                sourseRow["out_subinventory"] = row.Cells[2].Text;
                //sourseRow["out_frame_key"] = row.Cells[3].Text;
                sourseRow["in_subinventory"] = row.Cells[3].Text;
                //sourseRow["in_frame_key"] = row.Cells[5].Text;
                sourseRow["required_qty"] = row.Cells[4].Text;
                sourseRow["exchanged_qty"] = row.Cells[5].Text;
                sourseRow["create_man"] = row.Cells[6].Text;
                sourseRow["create_time"] = row.Cells[7].Text;
                table.Rows.Add(sourseRow);
            }

            return table;
        }

        //删除表格行
        protected void Delete_row(object sender, GridViewDeleteEventArgs e)
        {

            int index = e.RowIndex;

            DataTable table = new DataTable();

            table = GetGridViewData(table);

            int flag = 0;

            foreach (DataRow row in table.Rows)
            {

                if (index == flag)
                {

                    row.Delete();

                    break;

                }
                flag += 1;
            }

            GridView1.DataSource = table;

            GridView1.DataBind();
        }

    }

}