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
    public partial class IssueApplyPDA : System.Web.UI.Page
    {
        InvoiceDC invoiceDC = new InvoiceDC();
        string user;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "领料单申请";
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
                DataSet ds_return_wo_no = invoiceDC.getIssueWo_no();

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
                    DropDownList_issue_wo_no.DataSource = ds_return_wo_no.Tables[0].DefaultView;
                    DropDownList_issue_wo_no.DataValueField = "wo_no";
                    DropDownList_issue_wo_no.DataTextField = "wo_no";
                    DropDownList_issue_wo_no.DataBind();
                }

                DropDownList_Flax_value.Items.Insert(0, "--选择部门代码--");
                DropDownList_description.Items.Insert(0, "--选择部门名称--");
                DropDownList_item_name.Items.Insert(0, "--选择小料号--");
                DropDownList_operation_seq_num.Items.Insert(0, "--选择制程--");
                //DropDownList_frame_key.Items.Insert(0, "--选择料架--");
                DropDownList_issue_wo_no.Items.Insert(0, "--选择工单--");
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
                PageUtil.showToast(this, "部门代码与部门名称二级联动失败，请检查部门表是否有关联数据");
                return;
            }
        }

        //领料工单号改变时
        protected void DropDownList_issue_wo_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            //领料工单号与料号的的二级联动
            try
            {
                DropDownList_item_name.Items.Clear();

                string text = DropDownList_issue_wo_no.SelectedValue.ToString();
                DataSet ds_item_name = invoiceDC.getItem_nameByIssue_wo_no(text);
                if (ds_item_name != null)
                {
                    DropDownList_item_name.DataSource = ds_item_name.Tables[0].DefaultView;
                    DropDownList_item_name.DataValueField = "item_name";
                    DropDownList_item_name.DataTextField = "item_name";
                    DropDownList_item_name.DataBind();
                }

                DropDownList_item_name.Items.Insert(0, "--选择小料号--");
                DropDownList_item_name.SelectedIndex = 1;
            }
            catch (Exception e2)
            {
                PageUtil.showToast(this, "领料工单号与小料号二级联动失败，请检查工单物料需求表");
                return;
            }

            //根据工单号，和料号。拿需求量和发料量
            try
            {
                if (invoice_no.Value == "")
                {
                    PageUtil.showToast(this, "请输入领料单号再操作");
                    return;
                }

                //检测是否为工单领料，如果是，则需求量，发料量从工单物料需求表中带出
                DataSet ds = invoiceDC.getIssueHeaderBySome(invoice_no.Value, "", "");
                if (ds == null)
                {
                    PageUtil.showToast(this, "该领料单号不存在，请重新输入（确保已经申请成功）");
                    return;
                }

                if (ds.Tables[0].Rows[0]["issue_type"].ToString() == "工单领料")
                {

                    DataSet ds2 = invoiceDC.getRequirement(DropDownList_issue_wo_no.SelectedValue.ToString(), DropDownList_item_name.SelectedValue.ToString());

                    required_qty.Value = ds2.Tables[0].Rows[0]["required_qty"].ToString();
                    simulated_qty.Value = ds2.Tables[0].Rows[0]["issued_qty"].ToString();
                }
                else
                {
                    required_qty.Value = "0";
                    simulated_qty.Value = "0";
                }
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "获取需求量，发料量失败，请检查工单物料需求表");
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
            //    PageUtil.showToast(this, "小料号与料架二级联动失败，请检查库存明细表");
            //    return;
            //}

            ////料架与库别的二级联动
            //try
            //{
            //    DropDownList_issue_sub_key.Items.Clear();

            //    string text = DropDownList_frame_key.SelectedValue.ToString();
            //    DataSet ds_issue_sub_key = invoiceDC.getSubinventoryByFrame(text);
            //    if (ds_issue_sub_key != null)
            //    {
            //        DropDownList_issue_sub_key.DataSource = ds_issue_sub_key.Tables[0].DefaultView;
            //        DropDownList_issue_sub_key.DataValueField = "subinventory_name";
            //        DropDownList_issue_sub_key.DataTextField = "subinventory_name";
            //        DropDownList_issue_sub_key.DataBind();
            //    }

            //    DropDownList_issue_sub_key.Items.Insert(0, "--选择库别--");
            //    DropDownList_issue_sub_key.SelectedIndex = 1;
            //}
            //catch (Exception e2)
            //{
            //    PageUtil.showToast(this, "料架与库别二级联动失败，请检查是否有关联数据");
            //    return;
            //}


            //料号与库别的二级联动，去库存总表中找有此料号的库别
            try
            {
                DropDownList_issue_sub_key.Items.Clear();

                string text = DropDownList_item_name.SelectedValue.ToString();
                DataSet ds_issue_sub_key = invoiceDC.getSubinventoryByItem_name(text);
                if (ds_issue_sub_key != null)
                {
                    DropDownList_issue_sub_key.DataSource = ds_issue_sub_key.Tables[0].DefaultView;
                    DropDownList_issue_sub_key.DataValueField = "subinventory";
                    DropDownList_issue_sub_key.DataTextField = "subinventory";
                    DropDownList_issue_sub_key.DataBind();

                    DropDownList_issue_sub_key.Items.Insert(0, "--选择库别--");
                    DropDownList_issue_sub_key.SelectedIndex = 1;
                }
                else
                {
                    PageUtil.showToast(this, "料号与库别二级联动失败，请检查库存总表");
                    return;
                }
            }
            catch (Exception e2)
            {
                PageUtil.showToast(this, "料号与库别二级联动失败，请检查库存总表");
                return;
            }

        }


        //料号改变时，触发事件
        protected void DropDownList_item_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据料号和工单号确定需求量，发料量
            try
            {
                if (invoice_no.Value == "")
                {
                    PageUtil.showToast(this, "请输入领料单号再操作");
                    return;
                }

                DataSet ds = invoiceDC.getIssueHeaderBySome(invoice_no.Value, "", "");
                if (ds == null)
                {
                    PageUtil.showToast(this, "该领料单号不存在，请重新输入（确保已经申请成功）");
                    return;
                }

                //检测是否为工单领料，如果是，则需求量，发料量从工单物料需求表中带出
                if (ds.Tables[0].Rows[0]["issue_type"].ToString() == "工单领料")
                {

                    DataSet ds2 = invoiceDC.getRequirement(DropDownList_issue_wo_no.SelectedValue.ToString(), DropDownList_item_name.SelectedValue.ToString());

                    required_qty.Value = ds2.Tables[0].Rows[0]["required_qty"].ToString();
                    simulated_qty.Value = ds2.Tables[0].Rows[0]["issued_qty"].ToString();
                }
                else
                {
                    required_qty.Value = "0";
                    simulated_qty.Value = "0";
                }
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "获取需求量，发料量失败，请检查工单物料需求表");
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
            //    PageUtil.showToast(this, "小料号与料架二级联动失败，请检查库存明细表");
            //    return;
            //}

            ////料架与库别的二级联动
            //try
            //{
            //    DropDownList_issue_sub_key.Items.Clear();

            //    string text = DropDownList_frame_key.SelectedValue.ToString();
            //    DataSet ds_issue_sub_key = invoiceDC.getSubinventoryByFrame(text);
            //    if (ds_issue_sub_key != null)
            //    {
            //        DropDownList_issue_sub_key.DataSource = ds_issue_sub_key.Tables[0].DefaultView;
            //        DropDownList_issue_sub_key.DataValueField = "subinventory_name";
            //        DropDownList_issue_sub_key.DataTextField = "subinventory_name";
            //        DropDownList_issue_sub_key.DataBind();
            //    }

            //    DropDownList_issue_sub_key.Items.Insert(0, "--选择库别--");
            //    DropDownList_issue_sub_key.SelectedIndex = 1;
            //}
            //catch (Exception e2)
            //{
            //    PageUtil.showToast(this, "料架与库别二级联动失败，请检查是否有关联数据");
            //    return;
            //}

            //料号与库别的二级联动，去库存总表中找有此料号的库别
            try
            {
                DropDownList_issue_sub_key.Items.Clear();

                string text = DropDownList_item_name.SelectedValue.ToString();
                DataSet ds_issue_sub_key = invoiceDC.getSubinventoryByItem_name(text);
                if (ds_issue_sub_key != null)
                {
                    DropDownList_issue_sub_key.DataSource = ds_issue_sub_key.Tables[0].DefaultView;
                    DropDownList_issue_sub_key.DataValueField = "subinventory";
                    DropDownList_issue_sub_key.DataTextField = "subinventory";
                    DropDownList_issue_sub_key.DataBind();


                    DropDownList_issue_sub_key.Items.Insert(0, "--选择库别--");
                    DropDownList_issue_sub_key.SelectedIndex = 1;
                }
                else
                {
                    PageUtil.showToast(this, "料号与库别二级联动失败，请检查库存总表是否有相关数据");
                    return;
                }
            }
            catch (Exception e2)
            {
                PageUtil.showToast(this, "料号与库别二级联动失败，请检查库存总表是否有相关数据");
                return;
            }
        }


        ////选择料架时触发
        //protected void DropDownList_frame_key_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DropDownList_issue_sub_key.Items.Clear();

        //        string text = DropDownList_frame_key.SelectedValue.ToString();
        //        DataSet ds_issue_sub_key = invoiceDC.getSubinventoryByFrame(text);
        //        if (ds_issue_sub_key != null)
        //        {
        //            DropDownList_issue_sub_key.DataSource = ds_issue_sub_key.Tables[0].DefaultView;
        //            DropDownList_issue_sub_key.DataValueField = "subinventory_name";
        //            DropDownList_issue_sub_key.DataTextField = "subinventory_name";
        //            DropDownList_issue_sub_key.DataBind();
        //        }

        //        DropDownList_issue_sub_key.Items.Insert(0, "--选择库别--");
        //        DropDownList_issue_sub_key.SelectedIndex = 1;
        //    }
        //    catch (Exception e2)
        //    {
        //        PageUtil.showToast(this, "料架与库别二级联动失败，请检查是否有关联数据");
        //        return;
        //    }
        //}


        //生成一个领料单单据号
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

            ds = invoiceDC.getTheNewestIssueHeaderByCreatetime();

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
                //截取上一个单号的后5位,即从第5位开始的5位（0开始增）
                string sInvoice_no = ds.Tables[0].Rows[0]["invoice_no"].ToString();
                string result = sInvoice_no.Substring(5, 5);
                end = int.Parse(result);

                string last_month = sInvoice_no.Substring(3, 2);
                string last_year = sInvoice_no.Substring(1, 2);


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

                //为单数月补0
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

            invoice_no.Value = "Q" + year + month + end_text;
        }



        //导入缺料明细
        protected void import_short_detail(object sender, EventArgs e)
        {
         
            //导入缺料明细前，先把框框里的数据清空
            GridView1.DataSource = null;
            GridView1.DataBind();

            DataSet ds;
            if (string.IsNullOrWhiteSpace(invoice_no.Value))
            {
                PageUtil.showToast(this, "请填写单据号");
                return;
            }
            string Invoice_no = invoice_no.Value;

            if (invoiceDC.getIssueHeaderBySome(Invoice_no, "", "") == null)
            {
                PageUtil.showToast(this, "请确定单据号存在");
                return;
            }
            ds = invoiceDC.getIssueHeaderBySome(Invoice_no, "", "");

            //工单领料时才需要导入缺料明细
            if (ds.Tables[0].Rows[0]["issue_type"].ToString() == "工单领料")
            {

                string Issue_wo_no = DropDownList_issue_wo_no.SelectedValue.ToString();


                if (DropDownList_issue_wo_no.SelectedValue.ToString() == "--选择工单--")
                {
                    PageUtil.showToast(this, "请选择工单后再做操作");
                    return;
                }


                //避免重复点击导入缺料明细按钮（每一个领料单只能导入一次缺料明细）
                DataSet ds2;
                ds2 = invoiceDC.getIssueLineBySome(Invoice_no, "");
                if (ds2 != null)
                {
                    PageUtil.showToast(this, "该领料单已导入过缺料明细，请勿重复导入缺料明细！");
                    return;
                }


                //从数据库中获取该单据对应的line_num值
                int Line_num = invoiceDC.getIssueLine_numByInvoice_no(Invoice_no);
                //Line_num生成失败
                if (Line_num == -1)
                {
                    PageUtil.showToast(this, "添加单身失败");
                    return;
                }

                DataTable table = new DataTable();

                table = GetGridViewData(table);

                //当框框中已经有一条数据及以上时，需要对line_num做处理
                if (table.Rows.Count >= 1)
                {
                    Line_num = Line_num + table.Rows.Count;
                }

                //获取缺料明细
                DataSet ds3 = invoiceDC.getShort_detail(Issue_wo_no);

                //遍历查询出来的每行
                foreach (DataRow Dr in ds3.Tables[0].Rows)
                {

                    DataRow sourseRow = table.NewRow();
                    sourseRow["wo_no"] = Issue_wo_no;
                    sourseRow["line_num"] = Line_num;
                    sourseRow["item_name"] = Dr["item_name"];
                    sourseRow["peration_seq_num"] = "请修改制程";
                    sourseRow["issued_qty"] = Dr["issued_qty"];
                    sourseRow["simulated_qty"] = Dr["simulated_qty"];
                    sourseRow["required_qty"] = Dr["required_qty"];
                    //sourseRow["frame_key"] = Frame_key;
                    sourseRow["issued_sub"] = "请修改库别";
                    sourseRow["create_man"] = user;
                    sourseRow["create_time"] = DateTime.Now;

                    table.Rows.Add(sourseRow);
                    Line_num++;  //line_num递增
                }

                Session["TaskTable"] = table;

                GridView1.DataSource = table;

                GridView1.DataBind();

                //提交单个单身数据成功时，清除用户已填写的单身数据，方便用户填写下一个单身
                DropDownList_issue_wo_no.SelectedValue = "--选择工单--";
                DropDownList_item_name.SelectedValue = "--选择小料号--";
                DropDownList_operation_seq_num.SelectedValue = "--选择制程--";
                DropDownList_issue_sub_key.SelectedValue = "--选择库别--";
                //DropDownList_frame_key.SelectedValue = "--选择料架--";

                issued_qty.Value = "";
                simulated_qty.Value = "";
                required_qty.Value = "";

                PageUtil.showToast(this, "导入缺料明细成功，请修改库别与制程后点击更新按钮！");
                return;
            }
        }



        //提交领料单单头
        protected void Issue_Header_Commit(object sender, EventArgs e)
        {
            try
            {
                //注：主表不插工单号
                string Invoice_no = invoice_no.Value;
                string Flex_value = DropDownList_Flax_value.SelectedValue.ToString();
                string Description = DropDownList_description.SelectedValue.ToString();
                string Issue_type = Request.Form["issue_type"];
                string Remark = remark.Value;

                if (Flex_value == "--选择部门代码--" || Description == "--选择部门名称--" || Issue_type == "")
                {
                    PageUtil.showToast(this, "请将信息填写完全（可不填领料原因）");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Invoice_no))
                {
                    PageUtil.showToast(this, "请生成单据号或填写单据号再操作");
                    return;
                }
                if (invoiceDC.getIssueHeaderBySome(Invoice_no, "", "") != null)
                {
                    PageUtil.showToast(this, "单据号已存在，请重新生成");
                    return;
                }
                //对单据号的判断，防止乱输入单据号 @是将不能转义的字符转义
                Regex r = new Regex(@"^[Q][1-9]\d{8}$");
                Match m = r.Match(Invoice_no.Trim());
                if (m.Success == false)
                {
                    PageUtil.showToast(this, "请按规则输入单据号");
                    return;
                }


                bool flag = invoiceDC.insertIssue_header(Invoice_no, Issue_type, Flex_value, Description, Remark);

                if (flag == true)
                {
                    PageUtil.showToast(this, "生成领料单单头成功");

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

        //添加领料单单身到框框内（并未插入到数据库）
        protected void Issue_Line_Commit(object sender, EventArgs e)
        {
            try
            {
                DataSet ds;
                if (string.IsNullOrWhiteSpace(invoice_no.Value))
                {
                    PageUtil.showToast(this, "请填写单据号");
                    return;
                }
                string Invoice_no = invoice_no.Value;

                if (invoiceDC.getIssueHeaderBySome(Invoice_no, "", "") == null)
                {
                    PageUtil.showToast(this, "请确定单据号存在");
                    return;
                }
                ds = invoiceDC.getIssueHeaderBySome(Invoice_no, "", "");

                //工单领料时
                if (ds.Tables[0].Rows[0]["issue_type"].ToString() == "工单领料")
                {

                    string Issue_wo_no = DropDownList_issue_wo_no.SelectedValue.ToString();
                    string Item_name = DropDownList_item_name.SelectedValue.ToString();

                    if (DropDownList_issue_wo_no.SelectedValue.ToString() == "--选择工单--" || DropDownList_item_name.SelectedValue.ToString() == "--选择料号--")
                    {
                        PageUtil.showToast(this, "请选择工单，料号后再做操作");
                        return;
                    }

                    if (DropDownList_operation_seq_num.SelectedValue.ToString() == "--选择制程--" || DropDownList_issue_sub_key.SelectedValue.ToString() == "--选择库别--")
                    {
                        PageUtil.showToast(this, "请选择制程，库别后再做操作");
                        return;
                    }

                    //若该单身已对应工单（且不是当前添加的工单）则不允许添加
                    DataSet ds2, ds3;
                    ds2 = invoiceDC.getIssueLineBySome(Invoice_no, "");
                    ds3 = invoiceDC.getIssueLineBySome(Invoice_no, Issue_wo_no);
                    if (ds2 != null && ds3==null)
                    {
                        PageUtil.showToast(this, "同一个领料单号只对应一个工单，请检查工单号！");
                        return;
                    }


                    int Operation_seq_num = int.Parse(DropDownList_operation_seq_num.SelectedValue.ToString());
                    string Issue_sub_key = DropDownList_issue_sub_key.SelectedValue.ToString();
                    //string Frame_key = DropDownList_frame_key.SelectedValue.ToString();

                    if (required_qty.Value == "" || simulated_qty.Value == "")
                    {
                        required_qty.Value = "0";
                        simulated_qty.Value = "0";
                    }
                    int Issued_qty;
                    int Simulated_qty;
                    int Required_qty;

                    try
                    {
                        Issued_qty = int.Parse(issued_qty.Value);
                        Simulated_qty = int.Parse(simulated_qty.Value);
                        Required_qty = int.Parse(required_qty.Value);
                    }
                    catch (Exception e2)
                    {
                        PageUtil.showToast(this, "请规范填写领料量后再做操作");
                        return;
                    }
                    if (Required_qty != 0 && Simulated_qty != 0)
                    {
                        if (Issued_qty > (Required_qty - Simulated_qty) || Issued_qty <= 0)
                        {
                            PageUtil.showToast(this, "请规范填写领料量，领料量大于0且小于 需求量-发料量");
                            return;
                        }
                    }

                    //从数据库中获取该单据对应的line_num值
                    int Line_num = invoiceDC.getIssueLine_numByInvoice_no(Invoice_no);
                    //Line_num生成失败
                    if (Line_num == -1)
                    {
                        PageUtil.showToast(this, "添加单身失败");
                        return;
                    }

                    DataTable table = new DataTable();

                    table = GetGridViewData(table);

                    //当框框中已经有一条数据及以上时，需要对line_num做处理
                    if (table.Rows.Count >= 1)
                    {
                        Line_num = Line_num + table.Rows.Count;
                    }

                    //检验数据库中相同工单+料号的情况下，领料量有没有超过临界值
                    int db_issued_qty = invoiceDC.getIssued_qty(Issue_wo_no, Item_name);
                    if (Issued_qty + db_issued_qty > (Required_qty - Simulated_qty))
                    {
                        PageUtil.showToast(this, "该料号领料量超过需求量-发料量了，不能再申请领料，请去查看相关信息再申请");
                        return;
                    }


                    //当框框中已经有多条数据时，需要检验此时添加的新单身是否与在框框中的单身信息一样(工单号+料号)，需要对料号数量进行管控
                    int qty = 0;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (Issue_wo_no == table.Rows[i].ItemArray[3].ToString() && Item_name == table.Rows[i].ItemArray[1].ToString())
                        {
                            //累加已经领的料
                            qty += int.Parse(table.Rows[i].ItemArray[6].ToString());

                            //判断该工单下的该料号领料量是否超过需求量-发料量
                            if (Issued_qty + qty > (Required_qty - Simulated_qty))
                            {
                                PageUtil.showToast(this, "该料号领料量超过需求量-发料量了，不能再申请领料");
                                return;
                            }
                        }
                    }


                    DataRow sourseRow = table.NewRow();
                    sourseRow["wo_no"] = Issue_wo_no;
                    sourseRow["line_num"] = Line_num;
                    sourseRow["item_name"] = Item_name;
                    sourseRow["peration_seq_num"] = Operation_seq_num;
                    sourseRow["issued_qty"] = Issued_qty;
                    sourseRow["simulated_qty"] = Simulated_qty;
                    sourseRow["required_qty"] = Required_qty;
                    //sourseRow["frame_key"] = Frame_key;
                    sourseRow["issued_sub"] = Issue_sub_key;
                    sourseRow["create_man"] = user;
                    sourseRow["create_time"] = DateTime.Now;

                    table.Rows.Add(sourseRow);

                    GridView1.DataSource = table;

                    GridView1.DataBind();

                    //提交单个单身数据成功时，清除用户已填写的单身数据，方便用户填写下一个单身
                    DropDownList_issue_wo_no.SelectedValue = "--选择工单--";
                    DropDownList_item_name.SelectedValue = "--选择小料号--";
                    DropDownList_operation_seq_num.SelectedValue = "--选择制程--";
                    DropDownList_issue_sub_key.SelectedValue = "--选择库别--";
                    //DropDownList_frame_key.SelectedValue = "--选择料架--";

                    issued_qty.Value = "";
                    simulated_qty.Value = "";
                    required_qty.Value = "";
                }


                //非工单领料时
                if (ds.Tables[0].Rows[0]["issue_type"].ToString() == "非工单领料")
                {

                    string Item_name2 = item_name2.Value;
                    string Subinventory2 = subinventory2.Value;
                    //string Frame2 = frame2.Value;
                    int Route_id;
                    int Issued_qty2;

                    if (String.IsNullOrWhiteSpace(Item_name2) == true || String.IsNullOrWhiteSpace(Subinventory2) == true)
                    {
                        PageUtil.showToast(this, "请将非工单领料部分数据填写完整");
                        return;
                    }

                    //if (invoiceDC.getMaterial_io(Item_name2, Frame2) == null)
                    //{
                    //    PageUtil.showToast(this, "请重新输入料号或料架，库存明细表中没有对应数据");
                    //    return;
                    //}

                    //if (invoiceDC.getSubinventoryByFrame(Frame2) == null)
                    //{
                    //    PageUtil.showToast(this, "请重新输入库别，该料架不属于该库别下");
                    //    return;
                    //}

                    if (invoiceDC.getItems_onhand_qty_detail(Subinventory2, Item_name2) == null)
                    {
                        PageUtil.showToast(this, "请重新输入库别或料号，库存总表中没有对应数据");
                        return;
                    }


                    try
                    {
                        Route_id = int.Parse(invoiceDC.getRoute(operation_seq_num2.Value).Tables[0].Rows[0]["route_id"].ToString());
                    }
                    catch (Exception e2)
                    {
                        PageUtil.showToast(this, "请检查制程代号是否填写正确");
                        return;
                    }

                    try
                    {
                        Issued_qty2 = int.Parse(issued_qty2.Value);
                    }
                    catch (Exception e2)
                    {
                        PageUtil.showToast(this, "请规范填写领料量");
                        return;
                    }
                    if (Issued_qty2 <= 0)
                    {
                        PageUtil.showToast(this, "领料量应大于0");
                        return;
                    }

                    //从数据库中获取该单据对应的line_num值
                    int Line_num = invoiceDC.getIssueLine_numByInvoice_no(Invoice_no);
                    //Line_num生成失败
                    if (Line_num == -1)
                    {
                        PageUtil.showToast(this, "添加单身失败");
                        return;
                    }

                    DataTable table = new DataTable();

                    table = GetGridViewData(table);

                    //当框框中已经有一条数据及以上时，需要对line_num做处理
                    if (table.Rows.Count >= 1)
                    {
                        Line_num = Line_num + table.Rows.Count;
                    }

                    DataRow sourseRow = table.NewRow();
                    sourseRow["wo_no"] = "none";
                    sourseRow["line_num"] = Line_num;
                    sourseRow["item_name"] = Item_name2;
                    sourseRow["peration_seq_num"] = Route_id;
                    sourseRow["issued_qty"] = Issued_qty2;
                    sourseRow["simulated_qty"] = 0;
                    sourseRow["required_qty"] = 0;
                    //sourseRow["frame_key"] = Frame2;
                    sourseRow["issued_sub"] = Subinventory2;
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
                    issued_qty2.Value = "";
                }
            }
            catch (Exception e2)
            {
                PageUtil.showToast(this, "添加单身失败");
            }
        }



        //提交所有的领料单单身到数据库
        protected void Commit_All_Issue_Line(object sender, EventArgs e)
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
                bool flag = invoiceDC.insertIssue_line(table, Invoice_no);
                if (flag == true)
                {
                    PageUtil.showToast(this, "成功提交所有单身！");

                    //清空框框，避免用户重复提交
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    //提交单个单身数据成功时，清除用户已填写的单身数据，方便用户填写下一个单身
                    DropDownList_issue_wo_no.SelectedValue = "--选择工单--";
                    DropDownList_item_name.SelectedValue = "--选择小料号--";
                    DropDownList_operation_seq_num.SelectedValue = "--选择制程--";
                    DropDownList_issue_sub_key.SelectedValue = "--选择库别--";
                    //DropDownList_frame_key.SelectedValue = "--选择料架--";

                    issued_qty.Value = "";
                    simulated_qty.Value = "";
                    required_qty.Value = "";

                    //提交单个单身数据成功时，清除用户已填写的单身数据，方便用户填写下一个单身

                    item_name2.Value = "";
                    subinventory2.Value = "";
                    //frame2.Value = "";
                    operation_seq_num2.Value = "";
                    issued_qty2.Value = "";
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
            table.Columns.Add(new DataColumn("item_name"));
            table.Columns.Add(new DataColumn("peration_seq_num"));
            table.Columns.Add(new DataColumn("wo_no"));
            table.Columns.Add(new DataColumn("required_qty"));
            table.Columns.Add(new DataColumn("simulated_qty"));
            table.Columns.Add(new DataColumn("issued_qty"));
            table.Columns.Add(new DataColumn("issued_sub"));
            //table.Columns.Add(new DataColumn("frame_key"));
            table.Columns.Add(new DataColumn("create_man"));
            table.Columns.Add(new DataColumn("create_time"));

            foreach (GridViewRow row in GridView1.Rows)
            {
                DataRow sourseRow = table.NewRow();
                sourseRow["line_num"] = row.Cells[0].Text;
                sourseRow["item_name"] = row.Cells[1].Text;
                sourseRow["peration_seq_num"] = ((TextBox)(row.FindControl("peration_seq_num"))).Text;
                sourseRow["wo_no"] = row.Cells[3].Text;
                sourseRow["required_qty"] = row.Cells[4].Text;
                sourseRow["simulated_qty"] = row.Cells[5].Text;
                sourseRow["issued_qty"] = ((TextBox)(row.FindControl("issued_qty"))).Text;
                sourseRow["issued_sub"] = ((TextBox)(row.FindControl("issued_sub"))).Text;
                //sourseRow["frame_key"] = row.Cells[8].Text;
                sourseRow["create_man"] = row.Cells[8].Text;
                sourseRow["create_time"] = row.Cells[9].Text;
                table.Rows.Add(sourseRow);
            }

            return table;
        }

        ////更新表格行
        //protected void Update_row(object sender, EventArgs e)
        //{
        //    string Subnventory_update = subinventory_update.Value;

        //    //制程暂时特定为一个值，全部走通再写制程
        //    int route_id = 1;

        //    GridViewUpdateEventArgs gve;
        //    int index = gve.RowIndex;



        //}

        ////清空更新输入信息
        //protected void CleanUpdateMessage(object sender, EventArgs e)
        //{


        //}


        //更新表格行（最新）
        protected void TaskGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //将导入的缺料明细存入table中
                DataTable dt = (DataTable)Session["TaskTable"];

                //获取该行数据
                GridViewRow row = GridView1.Rows[e.RowIndex];

                //获取更改后的申请领料量
                dt.Rows[row.DataItemIndex]["issued_qty"] = ((TextBox)(row.FindControl("issued_qty"))).Text;

                //将用户输入的制程转换成制程ID      
                DataSet ds2 = invoiceDC.getRoute(((TextBox)(row.FindControl("peration_seq_num"))).Text);
                if (ds2 == null)
                    PageUtil.showToast(this, "该制程没有对应的制程id，请重新输入！");
                else
                    dt.Rows[row.DataItemIndex]["peration_seq_num"] = ds2.Tables[0].Rows[0]["route_id"].ToString();

                //((TextBox)(row.FindControl("issued_sub"))).Text:取得Gridview中第e.RowIndex索引行中索引为issued_sub的控件，将其强转为TEXTBOX以获得其中的值

                //看该库别是否存在，并获取库别在手量
                DataSet ds1 = invoiceDC.getItems_onhand_qty_detail(((TextBox)(row.FindControl("issued_sub"))).Text, dt.Rows[e.RowIndex]["item_name"].ToString());
                //该库别有足够量时，再显示
                if (ds1 == null)
                    PageUtil.showToast(this, "该料号对应的库别不存在，请重新输入！");

                if (int.Parse(ds1.Tables[0].Rows[0]["onhand_quantiy"].ToString()) >= int.Parse(dt.Rows[e.RowIndex]["issued_qty"].ToString()))
                    dt.Rows[row.DataItemIndex]["issued_sub"] = ((TextBox)(row.FindControl("issued_sub"))).Text;
                else
                    PageUtil.showToast(this, "该库别没有足够量，请重新输入！");


                //重置编辑行
                GridView1.EditIndex = -1;

                //重新显示编辑后的Gridview
                GridView1.DataSource = Session["TaskTable"];
                GridView1.DataBind();
            }
            catch (Exception e3)
            {
                PageUtil.showToast(this, "请检查该更新行的制程和库别是否均输入完毕");
            }
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