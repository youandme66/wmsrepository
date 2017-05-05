using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;
using WMS_v1._0.Model;
using System.Data;

namespace WMS_v1._0.PDA
{
    public partial class SingleQueryPDA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "退料单作业";
            if (!IsPostBack)
            {
                BindDrop();
                BindDrop1();
                Wip_operationDC regDC = new Wip_operationDC();
                DataSet ds2 = regDC.getRegion_nameBySubinventory_name();
                if (ds2 != null)
                {
                    seq_operation_num1.DataSource = ds2.Tables[0].DefaultView;
                    seq_operation_num1.DataValueField = "route";
                    seq_operation_num1.DataBind();
                    seq_operation_num2.DataSource = ds2.Tables[0].DefaultView;
                    seq_operation_num2.DataValueField = "route";
                    seq_operation_num2.DataBind();
                }
                else
                    PageUtil.showToast(this.Page, "数据载入出错！");
                seq_operation_num1.Items.Insert(0, "选择制程");
                //seq_operation_num2.Items.Insert(0, "选择制程");

                DropDownList2.Items.Insert(0, new ListItem("选择区域"));

                DropDownList4.Items.Insert(0, new ListItem("选择区域"));
            }
        }

        protected void select_singlequery(object sender, EventArgs e)
        {
            List<ModelReturn_line> Data;
            Return_lineDC DC = new Return_lineDC();
            string region;
            string return_wo_no = singlequery_return_wo_id.Value;
            string uom = singlequery_uom_id.Value;
            string seq_operation_num = seq_operation_num1.SelectedValue;
            if (seq_operation_num.Equals("选择制程"))
                seq_operation_num = "";
            string Subinventory = Subinventory1.SelectedValue;
            if (Subinventory.Equals("选择库别"))
                Subinventory = "";
            region = DropDownList2.SelectedValue;
            if (region.Equals("选择区域"))
                region = "";
            //Data = DC.getReturn_lineBySome(Subinventory, return_wo_no, seq_operation_num, uom, region);
            //if (Data != null)
            //{
            Repeater1.DataSource = DC.getReturn_lineBySome(Subinventory, return_wo_no, seq_operation_num, uom, region);
            if (Repeater1.DataSource == null)
            {
                PageUtil.showToast(this.Page, "没有该条数据，查询失败!");
            }
            else
            {
                Repeater1.DataBind();
                PageUtil.showToast(this.Page, "数据查询成功!");
            }
            //}
            //else
            PageUtil.showToast(this.Page, "没有该条数据，查询失败!");
            seq_operation_num1.SelectedValue = "选择制程";
            Subinventory1.SelectedValue = "选择库别";
            DropDownList2.SelectedValue = "选择区域";
        }

        protected void update2(object sender, EventArgs e)
        {
            Return_lineDC DC = new Return_lineDC();
            int return_line_id = int.Parse(return_line_id2.Value);
            string return_wo_no = return_wo_no2.Value;
            int seq_operation_num = 0;
            string item_name = item_name2.Value;
            int subinventory = 0;
            string locator = DropDownList4.Items[DropDownList4.SelectedIndex].Value;
            int quit_num = 0;
            if (String.IsNullOrEmpty(item_name2.Value))
            {
                PageUtil.showToast(this.Page, "请输入料号！");
            }
            else if (String.IsNullOrEmpty(quit_num2.Value))
            {
                PageUtil.showToast(this.Page, "请输入退料数量！");
            }
            else
            {
                try
                {
                    seq_operation_num = int.Parse(seq_operation_num2.Items[seq_operation_num2.SelectedIndex].Value);
                    subinventory = int.Parse(DropDownList3.Items[DropDownList3.SelectedIndex].Value);
                    quit_num = int.Parse(quit_num2.Value);
                }
                catch (Exception ex)
                {
                    PageUtil.showToast(this.Page, "数据转换出错，请检查数据格式！");
                    return;
                }
                bool a = DC.updateReturn_line(return_wo_no, return_line_id, seq_operation_num, item_name, quit_num, subinventory, locator);
                if (a == true)
                {

                    try
                    {

                        Repeater1.DataSource = DC.getReturn_lineByReturn_line_id(return_line_id);
                        if (Repeater1.DataSource == null)
                        {
                            PageUtil.showToast(this.Page, "没有该条数据，查询失败!");
                        }
                        else
                        {
                            Repeater1.DataBind();
                            PageUtil.showToast(this.Page, "数据查询成功!");
                        }

                    }
                    catch (Exception ex)
                    {
                        PageUtil.showToast(this.Page, "数据查询错误!");
                    }
                    PageUtil.showToast(this.Page, "更新单身成功！");
                }
                else
                {
                    PageUtil.showToast(this.Page, "更新单身失败！");
                }
            }
        }

        protected void delete_single_query(object sender, EventArgs e)
        {
            Return_lineDC DC = new Return_lineDC();
            //List<ModelReturn_line> Data;
            int return_line_id = int.Parse(lab3.Value);
            bool a = DC.deleteReturn_line(return_line_id);
            if (a == true)
            {
                PageUtil.showToast(this.Page, "删除单身成功！");
                //Data = DC.getReturn_lineBySome("", "", "", "", "");


                Repeater1.DataSource = DC.getReturn_lineBySome("", "", "", "", "");

                if (Repeater1.DataSource == null)
                {
                    PageUtil.showToast(this.Page, "没有该条数据，查询失败!");
                }
                else
                {
                    Repeater1.DataBind();
                    PageUtil.showToast(this.Page, "数据查询成功!");
                }
                Repeater1.DataBind();
            }
            else
            {
                PageUtil.showToast(this.Page, "删除单身失败！");
            }
        }

        protected void Opration_SingelQuery(object sender, EventArgs e)
        {
            Return_lineDC DC = new Return_lineDC();
            string subinventory = singlequery_subinventory.Value;
            string region = singlequery_region.Value;
            string item_name = singlequery_item_name.Value;
            string return_qty = singlequery_return_qty.Value;
            string return_line_id1 = return_line_id.Value;
            int a, return_qty1, return_line_id2;
            try
            {
                return_qty1 = int.Parse(return_qty);
                return_line_id2 = int.Parse(return_line_id1);
                a = DC.updateNum(subinventory, region, item_name, return_qty1, return_line_id2);
                if (a > 0)
                {
                    PageUtil.showToast(this.Page, "退料成功！");
                }
                else
                    PageUtil.showToast(this.Page, "数据失败！");
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this.Page, "数据转换出错！");

            }
        }

        private void BindDrop()          //查询时的数据绑定     
        {
            SubinventoryDC subDC = new SubinventoryDC();
            DataSet ds1 = subDC.getAllUsedSubinventory_name();
            Subinventory1.DataValueField = "subinventory_name";
            Subinventory1.DataSource = ds1.Tables[0].DefaultView;
            Subinventory1.DataBind();
            Subinventory1.Items.Insert(0, "选择库别");
        }

        protected void Subinventory1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegionDC DC = new RegionDC();
            int i = 1;
            int subinventory = 0;
            try
            {
                subinventory = int.Parse(Subinventory1.SelectedValue);
            }
            catch (Exception ex)
            {
                i = 0;
            }
            if (i == 1)
            {

                DataSet data = DC.getRegion_nameBySubinventory_key(subinventory);//库别，区域联动
                if (data != null)
                {
                    DropDownList2.DataValueField = "region_name";
                    DropDownList2.DataSource = data.Tables[0].DefaultView;
                    DropDownList2.DataBind();
                }
                else
                    PageUtil.showToast(this.Page, "数据载入出错！");
                DropDownList2.Items.Insert(0, "选择区域");
            }
        }

        private void BindDrop1()  //更新的数据绑定
        {
            SubinventoryDC subDC = new SubinventoryDC();
            DataSet ds1 = subDC.getAllUsedSubinventory_name();
            if (ds1 != null)
            {
                DropDownList3.DataValueField = "subinventory_name";
                DropDownList3.DataSource = ds1.Tables[0].DefaultView;
                DropDownList3.DataBind();
            }
            else
                PageUtil.showToast(this.Page, "数据载入出错！");
            DropDownList3.Items.Insert(0, "选择库别");
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegionDC DC = new RegionDC();
            int i = 1;
            int subinventory = 0;
            try
            {
                subinventory = int.Parse(DropDownList3.SelectedValue);
            }
            catch (Exception ex)
            {
                i = 0;
            }
            if (i == 1)
            {

                DataSet data = DC.getRegion_nameBySubinventory_key(subinventory);
                if (data != null)
                {
                    DropDownList4.DataValueField = "region_name";
                    DropDownList4.DataSource = data.Tables[0].DefaultView;
                    DropDownList4.DataBind();
                }
                else
                    PageUtil.showToast(this.Page, "数据载入出错");
                DropDownList4.Items.Insert(0, "请选择区域");
            }
        }
    }
}