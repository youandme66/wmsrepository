using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;
using WMS_v1._0.Util;

namespace WMS_v1._0.PDA1
{
    public partial class BomSettingPDA : System.Web.UI.Page
    {
        BomDC bomDC = new BomDC();
        List<ModelBom> modelBom_List = new List<ModelBom>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "Bom设定";
        }

        //查询按钮------查询Bom表中的相关信息
        protected void QueryMeassage_Click(object sender, EventArgs e)
        {
            String big_item_name = item_name1.Value;
            String Item_name_Query = small_item_name3.Value;
            string version_id = version.Value;
            string Operation_seq_num_Query = Request.Form["operation_seq_num_Query"];
            Wip_operationDC wip_operationdc = new Wip_operationDC();
            try
            {
                bomRepeater.DataSource = bomDC.getBomBySome(big_item_name, Item_name_Query, Operation_seq_num_Query, version_id);

                if (bomRepeater.DataSource == null)
                    PageUtil.showToast(this, "数据库中没有对应数据，请添加数据后再查询");
                else
                    bomRepeater.DataBind();
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this.Page, "查询失败！");
            }

        }

        //删除按钮------删除Bom表中的相关信息
        protected void DeletMeassage_Click(object sender, EventArgs e)
        {
            int Requirement_line_Delet = int.Parse(requirement_line_Delet.Value);
            bool flagBomDC = bomDC.deleteBom(Requirement_line_Delet);
            if (flagBomDC)
            {
                PageUtil.showToast(this.Page, "删除成功!");

                bomRepeater.DataSource = bomDC.getBom();
                bomRepeater.DataBind();
            }
            else
            {
                PageUtil.showToast(this.Page, "删除失败！");
            }
        }

        //更新按钮中的确定操作------更新Bom表中具体数据的部分字段
        protected void UpdateMeassage_Click(object sender, EventArgs e)
        {
            //string wo_no,string item_name,int operation_seq,int required_qty ,DateTime create_time)
            int Requirement_line_id_Update = int.Parse(requirement_line_id_Update.Value);
            string big_item_name3 = item_name3.Value;
            String Item_name_Update = small_item_name2.Value;
            string Operation_seq_num_Query = Request.Form["operation_seq_num_Update"];
            string version = version3.Value;
            string update_by = "";
            string required_qty = required_qty_Update.Value;
            try
            {
                update_by = Session["LoginName"].ToString();
            }
            catch (Exception e2)
            {
                PageUtil.showToast(this.Page, "请登录之后再操作");
                return;
            }
            PnDC pndc = new PnDC();
            List<ModelPn> mods = new List<ModelPn>();
            mods = pndc.getPnByITEM_NAME(big_item_name3);
            if (mods == null)
            {
                PageUtil.showToast(this.Page, "你输入的大料号不存在，请检查后输入");
                return;
            }
            mods = pndc.getPnByITEM_NAME(Item_name_Update);
            if (mods == null)
            {
                PageUtil.showToast(this.Page, "你输入的小料号不存在，请检查后输入");
                return;
            }
            if (string.IsNullOrWhiteSpace(version) == true)
            {
                PageUtil.showToast(this.Page, "版本号不能为空");
                return;
            }
            int req_qty = 0;
            DateTime UPDATE_TIME = new DateTime();
            UPDATE_TIME = DateTime.Now;
            Wip_operationDC wip_operationdc = new Wip_operationDC();
            try
            {
                req_qty = int.Parse(required_qty);
            }
            catch (Exception e1)
            {
                PageUtil.showToast(this.Page, "请输入正确的数量");
            }
            try
            {
                if (bomDC.getBomByThree(big_item_name3, Item_name_Update, version, req_qty) != null)
                {
                    string temp_ToastString = "该条数据在数据库中已存在，更新失败！";
                    PageUtil.showToast(this, temp_ToastString);
                    return;
                }
                bool flagBomDC = bomDC.updateBom(Requirement_line_id_Update, Item_name_Update, Operation_seq_num_Query, req_qty, version, update_by, UPDATE_TIME);

                if (flagBomDC)
                {
                    PageUtil.showToast(this, "更新成功!");
                    //modelBom_List = bomDC.getBomBySome(Wo_no_Update, Item_name_Update, Operation_seq_num_Update, Required_qty_Update);
                    bomRepeater.DataSource = bomDC.getBomBySome(big_item_name3, Item_name_Update, Operation_seq_num_Query, version);
                    bomRepeater.DataBind();
                }
                else
                {
                    PageUtil.showToast(this, "更新失败!请检查输入是否超出范围");
                }
            }
            catch (Exception ex)
            {
                string temp_ToastString = "请按格式输入，且不要超出范围";
                PageUtil.showToast(this, temp_ToastString);
            }
        }

        //插入按钮中的确定操作------往Bom表中插入一条数据
        protected void InsertMeassage_Click(object sender, EventArgs e)
        {
            String big_item_name2 = item_name2.Value;
            String Item_name_Insert = small_item_name1.Value;
            string Operation_seq_num_Insert1 = Request.Form["operation_seq_num_Insert"];
            string version = version2.Value;
            string req_qty = required_qty_Insert.Value;
            string create_by = "";
            int Required_qty_Insert;
            try
            {
                create_by = Session["LoginName"].ToString();
            }
            catch (Exception e3)
            {
                PageUtil.showToast(this.Page, "请登录之后再操作");
                return;
            }

            PnDC pndc = new PnDC();
            List<ModelPn> mods = new List<ModelPn>();
            mods = pndc.getPnByITEM_NAME(big_item_name2);
            if (mods == null)
            {
                PageUtil.showToast(this.Page, "你输入的大料号不存在，请检查后输入");
                return;
            }
            mods = pndc.getPnByITEM_NAME(Item_name_Insert);
            if (mods == null)
            {
                PageUtil.showToast(this.Page, "你输入的小料号不存在，请检查后输入");
                return;
            }
            Wip_operationDC wip_operationdc = new Wip_operationDC();

            if (string.IsNullOrWhiteSpace(big_item_name2) == true)
            {
                //插入数据时工单编号不可未空，弹出警示框提示用户输入
                string temp_AlertString = "插入数据时大料号不可未空！请重新输入";
                PageUtil.showToast(this, temp_AlertString);
            }
            else if (string.IsNullOrWhiteSpace(version) == true)
            {
                PageUtil.showToast(this.Page, "版本号不能为空");
                return;
            }
            else
            {
                try   //判断输入字符是否符合规则
                {
                    if (string.IsNullOrWhiteSpace(required_qty_Insert.Value) == true)
                    {
                        PageUtil.showToast(this.Page, "请输入子料号数量");
                        return;
                    }
                    else
                    {
                        Required_qty_Insert = int.Parse(required_qty_Insert.Value);
                    }
                    if (bomDC.getBomByThree(big_item_name2, Item_name_Insert, version) != null)
                    {
                        string temp_ToastString = "该条数据在数据库中已存在，插入失败！";
                        PageUtil.showToast(this, temp_ToastString);
                        return;
                    }
                    //使用BomDC中的方法进行插入操作
                    bool flagBomDC = bomDC.insertBom(big_item_name2, Item_name_Insert, Operation_seq_num_Insert1, version, Required_qty_Insert, create_by, DateTime.Now);
                    if (flagBomDC == true)
                    {
                        string temp_ToastString = "该单号在数据库中存储成功！";
                        PageUtil.showToast(this, temp_ToastString);
                        //modelBom_List = bomDC.getBomBySome(Requirement_line_Insert, Item_name_Insert, Operation_seq_num_Insert, Required_qty_Insert);
                        bomRepeater.DataSource = bomDC.getBomBySome(big_item_name2, Item_name_Insert, Operation_seq_num_Insert1, version);
                        bomRepeater.DataBind();
                    }
                    else
                    {
                        string temp_ToastString = "该单号在数据库中存储失败！请检查输入是否超出范围";
                        PageUtil.showToast(this, temp_ToastString);
                    }
                }
                catch (Exception ex)
                {
                    string temp_ToastString = "请按格式输入，且不要超出范围";
                    PageUtil.showToast(this, temp_ToastString);
                }
            }
        }

        //清除按钮操作------清除输入框中数据
        protected void CleanAllMeassage_Click(object sender, EventArgs e)
        {
            bomRepeater.DataBind();
            PageUtil.showToast(this, "成功清除查询结果");
        }

        //插入按钮中的取消操作------清除输入框数据
        protected void CleanInsertMessage(object sender, EventArgs e)
        {
            required_qty_Insert.Value = String.Empty;
        }

        //更新按钮中的取消操作------清除输入框数据
        protected void CleanUpdateMessage(object sender, EventArgs e)
        {
            required_qty_Update.Value = String.Empty;
        }
    }
}