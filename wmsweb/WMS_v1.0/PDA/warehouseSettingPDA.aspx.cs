using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;
using WMS_v1._0.Util;

namespace WMS_v1._0.PDA
{
    public partial class warehouseSettingPDA : System.Web.UI.Page
    {

        SubinventoryDC subinventoryDC = new SubinventoryDC();

        int user_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "库别设定";

            if (Session["LoginId"] == null)  //检测登录状态
            {
                PageUtil.showToast(this, "请登录后再做操作");
            }
            else
                user_id = int.Parse(Session["LoginId"].ToString());
        }

        //清除按钮操作------清除Repeater中数据
        protected void CleanAllMeassage_Click(object sender, EventArgs e)
        {
            subinventoryRepeater.DataBind();
            PageUtil.showToast(this, "成功清除查询结果");
        }

        //查询按钮------查询库别表中的相关信息
        protected void QueryMeassage_Click(object sender, EventArgs e)
        {
            String Subinventory_name_Query = Request.Form["subinventory_name_Query"];
            String Enabled_Query = Request.Form["enabled_Query"];
            string Description_Query = description_Query.Value;

            try
            {
                subinventoryRepeater.DataSource = subinventoryDC.getSubinventoryBySome(Subinventory_name_Query, Enabled_Query, Description_Query);

                if (subinventoryRepeater.DataSource == null)
                    PageUtil.showToast(this, "数据库中没有对应数据，请添加数据后再查询");
                else
                    subinventoryRepeater.DataBind();
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "查询失败！");
            }
        }

        //删除按钮
        protected void DeletMeassage_Click(object sender, EventArgs e)
        {
            int Subinventory_key_Delete = int.Parse(subinventory_key_Delete.Value);
            bool flag = subinventoryDC.deleteSubinventoryById(Subinventory_key_Delete);
            if (flag == true)
            {
                PageUtil.showToast(this, "删除成功!");

                subinventoryRepeater.DataSource = subinventoryDC.getSubinventoryBySome("", "", "");
                subinventoryRepeater.DataBind();
            }
            else
            {
                PageUtil.showToast(this, "删除失败！");
            }
        }

        //更新按钮中的确定操作
        protected void UpdateMeassage_Click(object sender, EventArgs e)
        {
            int Subinventory_key_Update = int.Parse(subinventory_key_Update.Value);
            String Subinventory_name_Update = subinventory_name_Update.Value;
            String Enabled_Update = Request.Form["enabled_Update"];
            string Description_Update = description_Update.Value;

            try
            {

                if (subinventoryDC.getSubinventoryBySome(Subinventory_name_Update, Enabled_Update, Description_Update) != null)
                {
                    string temp_ToastString = "该条数据在数据库中已存在，更新失败！";
                    PageUtil.showToast(this, temp_ToastString);
                    return;
                }
                bool flag = subinventoryDC.updateSubinventory(Subinventory_key_Update, Subinventory_name_Update, user_id, Enabled_Update, Description_Update);

                if (flag == true)
                {
                    PageUtil.showToast(this, "更新成功!");

                    subinventoryRepeater.DataSource = subinventoryDC.getSubinventoryBySome(Subinventory_name_Update, Enabled_Update, Description_Update);
                    subinventoryRepeater.DataBind();
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

        //插入按钮中的确定操作
        protected void InsertMeassage_Click(object sender, EventArgs e)
        {
            String Subinventory_name_Insert = subinventory_name_Insert.Value;
            String Enabled_Insert = Request.Form["enabled_Insert"];
            string Description_Insert = description_Insert.Value;
            try
            {
                if (subinventoryDC.getSubinventoryBySome(Subinventory_name_Insert, "", "") != null)
                {
                    string temp_ToastString = "该条数据在数据库中已存在，插入失败！";
                    PageUtil.showToast(this, temp_ToastString);
                    return;
                }
                //插入操作
                bool flag;
                flag = subinventoryDC.insertSubinventory(Subinventory_name_Insert, user_id, Enabled_Insert, Description_Insert, DateTime.Now);
                if (flag == true)
                {
                    string temp_ToastString = "该单号在数据库中存储成功！";
                    PageUtil.showToast(this, temp_ToastString);
                    subinventoryRepeater.DataSource = subinventoryDC.getSubinventoryBySome(Subinventory_name_Insert, Enabled_Insert, Description_Insert);
                    subinventoryRepeater.DataBind();

                    //清空插入成功后的插入框
                    subinventory_name_Insert.Value = "";
                    description_Insert.Value = "";
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
}