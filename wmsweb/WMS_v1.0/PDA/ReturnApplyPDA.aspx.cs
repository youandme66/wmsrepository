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

namespace WMS_v1._0.PDA
{
    public partial class ReturnApplyPDA : System.Web.UI.Page
    {
        InvoiceDC invoiceDC = new InvoiceDC();
        string user;
        int Limit_return;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "退料单申请";
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
                DataSet ds_return_wo_no = invoiceDC.getWo_no();
                //DataSet ds_frame_key = invoiceDC.getFrame();
                DataSet ds_return_sub_key = invoiceDC.getSubinventory_name();

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

                if (ds_return_wo_no != null)
                {
                    DropDownList_return_wo_no.DataSource = ds_return_wo_no.Tables[0].DefaultView;
                    DropDownList_return_wo_no.DataValueField = "wo_no";
                    DropDownList_return_wo_no.DataTextField = "wo_no";
                    DropDownList_return_wo_no.DataBind();
                }
                //if (ds_frame_key != null)
                //{
                //    DropDownList_frame_key.DataSource = ds_frame_key.Tables[0].DefaultView;
                //    DropDownList_frame_key.DataValueField = "frame_name";
                //    DropDownList_frame_key.DataTextField = "frame_name";
                //    DropDownList_frame_key.DataBind();
                //}
                if (ds_return_sub_key != null)
                {
                    DropDownList_return_sub_key.DataSource = ds_return_sub_key.Tables[0].DefaultView;
                    DropDownList_return_sub_key.DataValueField = "subinventory_key";
                    DropDownList_return_sub_key.DataTextField = "subinventory_name";
                    DropDownList_return_sub_key.DataBind();
                }

                DropDownList_Flax_value.Items.Insert(0, "--选择部门代码--");
                DropDownList_description.Items.Insert(0, "--选择部门名称--");
                DropDownList_item_name.Items.Insert(0, "--选择料号--");
                DropDownList_operation_seq_num.Items.Insert(0, "--选择制程--");
                DropDownList_return_sub_key.Items.Insert(0, "--选择库别--");
                DropDownList_return_wo_no.Items.Insert(0, "--选择工单--");
                //DropDownList_frame_key.Items.Insert(0, "--选择料架--");
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
                PageUtil.showToast(this, "部门代码与部门名称二级联动失败，请检查是否部门表有关联数据");
                return;
            }
        }

        //选择退料工单号时触发
        protected void DropDownList_return_wo_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            //退料工单号与料号的二级联动，从工单物料需求表中带出料号
            try
            {
                DropDownList_item_name.Items.Clear();

                string text = DropDownList_return_wo_no.SelectedValue.ToString();
                DataSet ds_item_name = invoiceDC.getItem_nameByReturn_wo_no(text);
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
                PageUtil.showToast(this, "退料工单号与料号二级联动失败，请检查工单物料需求表是否有关联数据");
                return;
            }

            //根据工单号和料号，从工单物料需求表中带出发料量+领料量
            try
            {
                if (invoice_no.Value == "")
                {
                    PageUtil.showToast(this, "请输入退料单号再做操作");
                    return;
                }

                DataSet ds = invoiceDC.getReturnHeaderBySome(invoice_no.Value, "", "");
                if (ds == null)
                {
                    PageUtil.showToast(this, "该退料单不存在，请重新输入（请确保申请成功）");
                    return;
                }

                //检测是否为工单退料，如果是，则退料上限为发料量+领料量从工单物料需求表中带出
                if (ds.Tables[0].Rows[0]["return_type"].ToString() == "工单退料")
                {

                    DataSet ds2 = invoiceDC.getRequirement(DropDownList_return_wo_no.SelectedValue.ToString(), DropDownList_item_name.SelectedValue.ToString());

                    Limit_return = int.Parse(ds2.Tables[0].Rows[0]["issue_invoice_qty"].ToString()) + int.Parse(ds2.Tables[0].Rows[0]["issued_qty"].ToString());

                    return_limit.Value = Limit_return.ToString();
                }
                else
                {
                    return_limit.Value = "none";
                }
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "获取领料量，发料量失败，请检查是否工单物料需求表有关联数据");
                return;
            }

            ////料号与料架的二级联动，去库存明细表中找有此料号的料架
            //try
            //{
            //    DropDownList_frame_key.Items.Clear();

            //    string text = DropDownList_item_name.SelectedValue.ToString();
            //    DataSet ds_frame_key = invoiceDC.getFrameByItem_name(text);
            //    if (ds_frame_key != null)
            //    {
            //        DropDownList_frame_key.DataSource = ds_frame_key.Tables[0].DefaultView;
            //        DropDownList_frame_key.DataValueField = "frame_name";
            //        DropDownList_frame_key.DataTextField = "frame_name";
            //        DropDownList_frame_key.DataBind();
            //    }

            //    DropDownList_frame_key.Items.Insert(0, "--选择料架--");
            //    DropDownList_frame_key.SelectedIndex = 1;
            //}
            //catch (Exception e2)
            //{
            //    PageUtil.showToast(this, "料号与料架二级联动失败，请检查库存明细表是否有关联数据");
            //    return;
            //}

            ////料架与库别的二级联动
            //try
            //{
            //    DropDownList_return_sub_key.Items.Clear();

            //    string text = DropDownList_frame_key.SelectedValue.ToString();
            //    DataSet ds_return_sub_key = invoiceDC.getSubinventoryByFrame(text);
            //    if (ds_return_sub_key != null)
            //    {
            //        DropDownList_return_sub_key.DataSource = ds_return_sub_key.Tables[0].DefaultView;
            //        DropDownList_return_sub_key.DataValueField = "subinventory_key";
            //        DropDownList_return_sub_key.DataTextField = "subinventory_name";
            //        DropDownList_return_sub_key.DataBind();
            //    }

            //    DropDownList_return_sub_key.Items.Insert(0, "--选择库别--");
            //    DropDownList_return_sub_key.SelectedIndex = 1;
            //}
            //catch (Exception e2)
            //{
            //    PageUtil.showToast(this, "料架与库别二级联动失败，请检查是否有关联数据");
            //    return;
            //}
        }

        //料号改变时触发事件
        protected void DropDownList_item_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据料号和工单号去工单物料需求表中带出发料量+领料量
            try
            {
                if (invoice_no.Value == "")
                {
                    PageUtil.showToast(this, "请输入退料单号再做操作");
                    return;
                }

                DataSet ds = invoiceDC.getReturnHeaderBySome(invoice_no.Value, "", "");
                if (ds == null)
                {
                    PageUtil.showToast(this, "该退料单不存在，请重新输入（请确保申请成功）");
                    return;
                }

                //检测是否为工单退料，如果是，则退料上限为发料量+领料量从工单物料需求表中带出
                if (ds.Tables[0].Rows[0]["return_type"].ToString() == "工单退料")
                {

                    DataSet ds2 = invoiceDC.getRequirement(DropDownList_return_wo_no.SelectedValue.ToString(), DropDownList_item_name.SelectedValue.ToString());
                    Limit_return = int.Parse(ds2.Tables[0].Rows[0]["issue_invoice_qty"].ToString()) + int.Parse(ds2.Tables[0].Rows[0]["issued_qty"].ToString());

                    return_limit.Value = Limit_return.ToString();
                }
                else
                {
                    return_limit.Value = "none";
                }
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "获取领料量，发料量失败，请检查工单物料需求表是否有关联数据");
                return;
            }

            ////料号与料架的二级联动，去库存明细表中找有此料号的料架（没有数量限制，因为是退料）
            //try
            //{
            //    DropDownList_frame_key.Items.Clear();

            //    string text = DropDownList_item_name.SelectedValue.ToString();
            //    DataSet ds_frame_key = invoiceDC.getFrameByItem_name(text);
            //    if (ds_frame_key != null)
            //    {
            //        DropDownList_frame_key.DataSource = ds_frame_key.Tables[0].DefaultView;
            //        DropDownList_frame_key.DataValueField = "frame_name";
            //        DropDownList_frame_key.DataTextField = "frame_name";
            //        DropDownList_frame_key.DataBind();
            //    }

            //    DropDownList_frame_key.Items.Insert(0, "--选择料架--");
            //    DropDownList_frame_key.SelectedIndex = 1;
            //}
            //catch (Exception e2)
            //{
            //    PageUtil.showToast(this, "料号与料架二级联动失败，请检查库存明细表是否有关联数据");
            //    return;
            //}

            ////料架与库别的二级联动
            //try
            //{
            //    DropDownList_return_sub_key.Items.Clear();

            //    string text = DropDownList_frame_key.SelectedValue.ToString();
            //    DataSet ds_return_sub_key = invoiceDC.getSubinventoryByFrame(text);
            //    if (ds_return_sub_key != null)
            //    {
            //        DropDownList_return_sub_key.DataSource = ds_return_sub_key.Tables[0].DefaultView;
            //        DropDownList_return_sub_key.DataValueField = "subinventory_key";
            //        DropDownList_return_sub_key.DataTextField = "subinventory_name";
            //        DropDownList_return_sub_key.DataBind();
            //    }

            //    DropDownList_return_sub_key.Items.Insert(0, "--选择库别--");
            //    DropDownList_return_sub_key.SelectedIndex = 1;
            //}
            //catch (Exception e2)
            //{
            //    PageUtil.showToast(this, "料架与库别二级联动失败，请检查是否有关联数据");
            //    return;
            //}
        }

        ////料架与库别的二级联动，选择料架时触发
        //protected void DropDownList_frame_key_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DropDownList_return_sub_key.Items.Clear();

        //        string text = DropDownList_frame_key.SelectedValue.ToString();
        //        DataSet ds_return_sub_key = invoiceDC.getSubinventoryByFrame(text);
        //        if (ds_return_sub_key != null)
        //        {
        //            DropDownList_return_sub_key.DataSource = ds_return_sub_key.Tables[0].DefaultView;
        //            DropDownList_return_sub_key.DataValueField = "subinventory_key";
        //            DropDownList_return_sub_key.DataTextField = "subinventory_name";
        //            DropDownList_return_sub_key.DataBind();
        //        }

        //        DropDownList_return_sub_key.Items.Insert(0, "--选择库别--");
        //        DropDownList_return_sub_key.SelectedIndex = 1;
        //    }
        //    catch (Exception e2)
        //    {
        //        PageUtil.showToast(this, "料架与库别二级联动失败，请检查是否有关联数据");
        //        return;
        //    }
        //}





        //生成一个退料单单据号
        protected void NewInvoice_no(object sender, EventArgs e)
        {
            string strInvoice_no = invoice_no.Value;

            if (!string.IsNullOrWhiteSpace(strInvoice_no))
            {

                PageUtil.showToast(this, "单据号已生成，请勿重复操作！");

                return;

            }

            int end;
            string end_text;
            string month = string.Empty;
            string year = string.Empty;
            DataSet ds;

            ds = invoiceDC.getTheNewestReturnHeaderByCreatetime();

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
                //end = int.Parse(ds.Tables[0].Rows[0]["return_header_id"].ToString());

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

            invoice_no.Value = "F" + year + month + end_text;

        }




        //提交退料单单头
        protected void Return_Header_Commit(object sender, EventArgs e)
        {
            try
            {
                //注：主表不插工单号
                string Invoice_no = invoice_no.Value;
                string Flex_value = DropDownList_Flax_value.SelectedValue.ToString();
                string Description = DropDownList_description.SelectedValue.ToString();
                string Return_type = Request.Form["return_type"];
                string Remark = remark.Value;

                if (Flex_value == "--选择部门代码--" || Description == "--选择部门名称--" || Return_type == "")
                {
                    PageUtil.showToast(this, "请将信息填写完全（可不填退料原因）");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Invoice_no))
                {
                    PageUtil.showToast(this, "请生成单据号或填写单据号再操作");
                    return;
                }
                if (invoiceDC.getReturnHeaderBySome(Invoice_no, "", "") != null)
                {
                    PageUtil.showToast(this, "单据号已存在，请重新生成");
                    return;
                }

                //对单据号的判断，防止乱输入单据号 @是将不能转义的字符转义
                Regex r = new Regex(@"^[F][1-9]\d{8}$");
                Match m = r.Match(Invoice_no.Trim());
                if (m.Success == false)
                {
                    PageUtil.showToast(this, "请按规则输入单据号");
                    return;
                }


                bool flag = invoiceDC.insertReturn_header(Invoice_no, Return_type, Flex_value, Description, Remark);

                if (flag == true)
                {
                    PageUtil.showToast(this, "生成退料单单头成功");
                    DropDownList_Flax_value.SelectedValue = "--选择部门代码--";
                    DropDownList_description.SelectedValue = "--选择部门名称--";
                    remark.Value = "";
                }
                else
                    PageUtil.showToast(this, "插入失败");
            }
            catch (Exception e1)
            {
                PageUtil.showToast(this, "插入失败");
            }
        }

        //添加退料单单身到框框内（并未插入到数据库）
        protected void Return_Line_Commit(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(invoice_no.Value))
                {
                    PageUtil.showToast(this, "请生成单据号或填写单据号再操作");
                    return;
                }
                string Invoice_no = invoice_no.Value;


                if (invoiceDC.getReturnHeaderBySome(Invoice_no, "", "") == null)
                {
                    PageUtil.showToast(this, "请确定单据号存在");
                    return;
                }
                DataSet ds = invoiceDC.getReturnHeaderBySome(invoice_no.Value, "", "");

                //工单退料时
                if (ds.Tables[0].Rows[0]["return_type"].ToString() == "工单退料")
                {

                    string Return_wo_no = DropDownList_return_wo_no.SelectedValue.ToString();
                    string Item_name = DropDownList_item_name.SelectedValue.ToString();

                    //检查信息 
                    if (DropDownList_return_wo_no.SelectedValue.ToString() == "--选择工单--" || DropDownList_item_name.SelectedValue.ToString() == "--选择料号--")
                    {
                        PageUtil.showToast(this, "请检查信息是否填写完整和无误");
                        return;
                    }

                    if (DropDownList_operation_seq_num.SelectedValue.ToString() == "--选择制程--" || DropDownList_return_sub_key.SelectedValue.ToString() == "--选择库别--")
                    {
                        PageUtil.showToast(this, "请将信息填写完全");
                        return;
                    }
                    int Operation_seq_num = int.Parse(DropDownList_operation_seq_num.SelectedValue.ToString());
                    int Return_sub_key = int.Parse(DropDownList_return_sub_key.SelectedValue.ToString());
                    //string Frame_key = DropDownList_frame_key.SelectedValue.ToString();

                    if (return_qty.Value == "")
                    {
                        PageUtil.showToast(this, "请填写退料量并保证大于0");
                        return;
                    }

                    int Return_qty = int.Parse(return_qty.Value);
                    if (Return_qty <= 0)
                    {
                        PageUtil.showToast(this, "请填写退料量并保证大于0");
                        return;
                    }

                    if (Return_qty > int.Parse(return_limit.Value))
                    {
                        PageUtil.showToast(this, "当前为工单退料，退料量不能大于退料上限");
                        return;
                    }


                    //从数据库中获取该单据对应的line_num值
                    int Line_num = invoiceDC.getLine_numByInvoice_no(Invoice_no);
                    //Line_num生成失败
                    if (Line_num == -1)
                    {
                        PageUtil.showToast(this, "添加单身失败");
                        return;
                    }

                    //退回量不能超过该工单领料的量未实现

                    DataTable table = new DataTable();

                    table = GetGridViewData(table);

                    //当框框中已经有一条数据及以上时，需要对line_num做处理
                    if (table.Rows.Count >= 1)
                    {
                        Line_num = Line_num + table.Rows.Count;
                    }


                    //检验数据库中相同工单+料号的情况下，退料量有没有超过临界值
                    int db_return_qty = invoiceDC.getReturn_qty(Return_wo_no, Item_name);
                    if (Return_qty + db_return_qty > int.Parse(return_limit.Value))
                    {
                        PageUtil.showToast(this, "该料号退料量超过退料上限（发料量+领料量）了，不能再申请退料，请去查看相关信息再申请");
                        return;
                    }


                    //当框框中已经有多条数据时，需要检验此时添加的新单身是否与在框框中的单身信息一样(工单号+料号)，需要对料号数量进行管控
                    int qty = 0;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (Return_wo_no == table.Rows[i].ItemArray[1].ToString() && Item_name == table.Rows[i].ItemArray[2].ToString())
                        {
                            //累加已经退的料
                            qty += int.Parse(table.Rows[i].ItemArray[4].ToString());

                            //判断该工单下的该料号退料量是否超过退料上限（发料量+领料量）
                            if (Return_qty + qty > int.Parse(return_limit.Value))
                            {
                                PageUtil.showToast(this, "该料号退料量超过退料上限（发料量+领料量）了，不能再申请领料");
                                return;
                            }
                        }
                    }



                    DataRow sourseRow = table.NewRow();
                    sourseRow["line_num"] = Line_num;
                    sourseRow["return_wo_no"] = Return_wo_no;
                    sourseRow["item_name"] = Item_name;
                    sourseRow["operation_seq_num"] = Operation_seq_num;
                    sourseRow["return_qty"] = Return_qty;
                    sourseRow["return_sub_key"] = Return_sub_key;
                    //sourseRow["frame_key"] = Frame_key;
                    sourseRow["create_man"] = user;
                    sourseRow["create_time"] = DateTime.Now;

                    table.Rows.Add(sourseRow);

                    GridView1.DataSource = table;

                    GridView1.DataBind();

                    //提交单个单身数据成功时，清除用户已填写的单身数据，方便用户填写下一个单身
                    DropDownList_return_wo_no.SelectedValue = "--选择工单--";
                    DropDownList_item_name.SelectedValue = "--选择料号--";
                    DropDownList_operation_seq_num.SelectedValue = "--选择制程--";
                    //DropDownList_frame_key.SelectedValue = "--选择料架--";
                    DropDownList_return_sub_key.SelectedValue = "--选择库别--";

                    return_qty.Value = "";
                    return_limit.Value = "";
                }


                //非工单退料时
                else
                {
                    string Item_name2 = item_name2.Value;
                    int Subinventory2;
                    try
                    {
                        Subinventory2 = int.Parse(invoiceDC.getSubinventory_Key(subinventory2.Value).Tables[0].Rows[0]["subinventory_key"].ToString());
                    }
                    catch (Exception e2)
                    {
                        PageUtil.showToast(this, "请检查库别是否填写正确，库别表中没有对应数据");
                        return;
                    }
                    //string Frame2 = frame2.Value;
                    int Route_id;
                    int Return_qty2;

                    if (String.IsNullOrWhiteSpace(Item_name2) == true)
                    {
                        PageUtil.showToast(this, "请将非工单退料部分数据填写完整");
                        return;
                    }

                    if (invoiceDC.getItem_name(Item_name2) == null)
                    {
                        PageUtil.showToast(this, "请重新输入料号，该料号不存在于料号表");
                        return;
                    }

                    //if (invoiceDC.getFrame_name(Frame2) == null)
                    //{
                    //    PageUtil.showToast(this, "请重新输入料架，该料架不存在于料架表");
                    //    return;
                    //}

                    //if (invoiceDC.getSubinventoryByFrame(Frame2) == null)
                    //{
                    //    PageUtil.showToast(this, "请重新输入库别，该料架不属于该库别下");
                    //    return;
                    //}

                    try
                    {
                        Route_id = int.Parse(invoiceDC.getRoute(operation_seq_num2.Value).Tables[0].Rows[0]["route_id"].ToString());
                    }
                    catch (Exception e2)
                    {
                        PageUtil.showToast(this, "请检查制程代号是否填写正确，制程表中没有对应数据");
                        return;
                    }

                    try
                    {
                        Return_qty2 = int.Parse(return_qty2.Value);
                    }
                    catch (Exception e2)
                    {
                        PageUtil.showToast(this, "请规范填写退料量");
                        return;
                    }

                    if (Return_qty2 <= 0)
                    {
                        PageUtil.showToast(this, "请填写退料量并保证大于0");
                        return;
                    }

                    //从数据库中获取该单据对应的line_num值
                    int Line_num = invoiceDC.getLine_numByInvoice_no(Invoice_no);
                    //Line_num生成失败
                    if (Line_num == -1)
                    {
                        PageUtil.showToast(this, "添加单身失败");
                        return;
                    }

                    //退回量不能超过该工单领料的量未实现

                    DataTable table = new DataTable();

                    table = GetGridViewData(table);

                    //当框框中已经有一条数据及以上时，需要对line_num做处理
                    if (table.Rows.Count >= 1)
                    {
                        Line_num = Line_num + table.Rows.Count;
                    }

                    DataRow sourseRow = table.NewRow();
                    sourseRow["line_num"] = Line_num;
                    sourseRow["return_wo_no"] = "none";
                    sourseRow["item_name"] = Item_name2;
                    sourseRow["operation_seq_num"] = Route_id;
                    sourseRow["return_qty"] = Return_qty2;
                    sourseRow["return_sub_key"] = Subinventory2;
                    //sourseRow["frame_key"] = Frame2;
                    sourseRow["create_man"] = user;
                    sourseRow["create_time"] = DateTime.Now;

                    table.Rows.Add(sourseRow);

                    GridView1.DataSource = table;

                    GridView1.DataBind();

                    //提交单个单身数据成功时，清除用户已填写的单身数据，方便用户填写下一个单身
                    item_name2.Value = "";
                    subinventory2.Value = "";
                    //frame2.Value = "";
                    operation_seq_num2.Value = "";
                    return_qty2.Value = "";

                }
            }
            catch (Exception e2)
            {
                PageUtil.showToast(this, "添加单身失败");
            }

        }

        //提交所有的退料单单身到数据库
        protected void Commit_All_Return_Line(object sender, EventArgs e)
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
                bool flag = invoiceDC.insertReturn_line(table, Invoice_no);
                if (flag == true)
                {
                    PageUtil.showToast(this, "成功提交所有单身！");

                    //清空框框，避免用户重复提交
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    //提交单个单身数据成功时，清除用户已填写的单身数据，方便用户填写下一个单身
                    DropDownList_return_wo_no.SelectedValue = "--选择工单--";
                    DropDownList_item_name.SelectedValue = "--选择料号--";
                    DropDownList_operation_seq_num.SelectedValue = "--选择制程--";
                    //DropDownList_frame_key.SelectedValue = "--选择料架--";
                    DropDownList_return_sub_key.SelectedValue = "--选择库别--";

                    return_qty.Value = "";
                    return_limit.Value = "";

                    //提交单个单身数据成功时，清除用户已填写的单身数据，方便用户填写下一个单身
                    item_name2.Value = "";
                    subinventory2.Value = "";
                    //frame2.Value = "";
                    operation_seq_num2.Value = "";
                    return_qty2.Value = "";
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
            table.Columns.Add(new DataColumn("line_num"));
            table.Columns.Add(new DataColumn("return_wo_no"));
            table.Columns.Add(new DataColumn("item_name"));
            table.Columns.Add(new DataColumn("operation_seq_num"));
            table.Columns.Add(new DataColumn("return_qty"));
            table.Columns.Add(new DataColumn("return_sub_key"));
            //table.Columns.Add(new DataColumn("frame_key"));
            table.Columns.Add(new DataColumn("create_man"));
            table.Columns.Add(new DataColumn("create_time"));

            foreach (GridViewRow row in GridView1.Rows)
            {
                DataRow sourseRow = table.NewRow();
                sourseRow["line_num"] = row.Cells[0].Text;
                sourseRow["return_wo_no"] = row.Cells[1].Text;
                sourseRow["item_name"] = row.Cells[2].Text;
                sourseRow["operation_seq_num"] = row.Cells[3].Text;
                sourseRow["return_qty"] = row.Cells[4].Text;
                sourseRow["return_sub_key"] = row.Cells[5].Text;
                //sourseRow["frame_key"] = row.Cells[6].Text;
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