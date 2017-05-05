﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;
using WMS_v1._0.Util;


namespace WMS_v1._0.PDA
{
    public partial class InteractionPDA : System.Web.UI.Page
    {
        ProgramsDC programsDC = new ProgramsDC();

        int user_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "界面设定";

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
            programsRepeater.DataBind();
            PageUtil.showToast(this, "成功清除查询结果");
        }

        //查询按钮
        protected void QueryMeassage_Click(object sender, EventArgs e)
        {
            String Program_name_Query = Request.Form["program_name_Query"];
            String Enabled_Query = Request.Form["enabled_Query"];
            string Description_Query = description_Query.Value;

            try
            {
                programsRepeater.DataSource = programsDC.searchProgram(Program_name_Query, Enabled_Query, Description_Query);

                if (programsRepeater.DataSource == null)
                    PageUtil.showToast(this, "数据库中没有对应数据，请添加数据后再查询");
                else
                    programsRepeater.DataBind();
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "查询失败！");
            }
        }

        //删除按钮
        protected void DeletMeassage_Click(object sender, EventArgs e)
        {
            int Program_id_Delete = int.Parse(program_id_Delete.Value);
            bool flag = programsDC.deleteProgram(Program_id_Delete);
            if (flag == true)
            {
                PageUtil.showToast(this, "删除成功!");

                programsRepeater.DataSource = programsDC.searchProgram("", "", "");
                programsRepeater.DataBind();
            }
            else
            {
                PageUtil.showToast(this, "删除失败！");
            }
        }

        //更新按钮中的确定操作
        protected void UpdateMeassage_Click(object sender, EventArgs e)
        {
            int Program_id_Update = int.Parse(program_id_Update.Value);
            String Program_name_Update = program_name_Update.Value;
            String Enabled_Update = Request.Form["enabled_Update"];
            string Description_Update = description_Update.Value;

            try
            {
                if (programsDC.searchProgram(Program_name_Update, Enabled_Update, Description_Update) != null)
                {
                    string temp_ToastString = "该条数据在数据库中已存在，更新失败！";
                    PageUtil.showToast(this, temp_ToastString);
                    return;
                }
                bool flag = programsDC.updateProgram(Program_id_Update, Program_name_Update, Description_Update, Enabled_Update, user_id);

                if (flag == true)
                {
                    PageUtil.showToast(this, "更新成功!");

                    programsRepeater.DataSource = programsDC.searchProgram(Program_name_Update, Enabled_Update, Description_Update);
                    programsRepeater.DataBind();
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
            String Program_name_Insert = program_name_Insert.Value;
            String Enabled_Insert = Request.Form["enabled_Insert"];
            string Description_Insert = description_Insert.Value;
            try
            {
                if (programsDC.searchProgram(Program_name_Insert, "", "") != null)
                {
                    string temp_ToastString = "该条数据在数据库中已存在，插入失败！";
                    PageUtil.showToast(this, temp_ToastString);
                    return;
                }
                //插入操作
                bool flag;
                flag = programsDC.insertProgram(Program_name_Insert, Description_Insert, Enabled_Insert, user_id);
                if (flag == true)
                {
                    string temp_ToastString = "该单号在数据库中存储成功！";
                    PageUtil.showToast(this, temp_ToastString);
                    programsRepeater.DataSource = programsDC.searchProgram(Program_name_Insert, Enabled_Insert, Description_Insert);
                    programsRepeater.DataBind();
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