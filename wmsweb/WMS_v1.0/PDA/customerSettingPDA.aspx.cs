using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;
using System.Data;
using WMS_v1._0.Util;

namespace WMS_v1._0.PDA1
{
    public partial class customerSettingPDA : System.Web.UI.Page
    {
        UsersDC user = new UsersDC();
        DataSet users_dataset = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "用户设定";
        }
        protected void clear(object sender, EventArgs e)
        {
            user_name1.Value = string.Empty;
            Enabled1.SelectedIndex = 0;
            Textarea1.Value = string.Empty;
        }
        protected void notarizeAdd(object sender, EventArgs e)
        {
            string user_name = Frame_name.Value.Trim();
            string enabled = Enabled.Items[Enabled.SelectedIndex].Value.Trim();
            string department = Request.Form["Department"];
            string description = Description.Value.Trim();
            if (description.Length >= 20)
            {
                PageUtil.showToast(this.Page, "描述太长！");
            }
            else if (user_name.Length >= 20)
            {
                PageUtil.showToast(this.Page, "用户名太长！");
            }
            else if (string.IsNullOrEmpty(user_name))
            {
                PageUtil.showToast(this.Page, "请输入用户名！");
            }
            else
            {
                string create_by = String.Empty;
                try
                {
                    create_by = Session["LoginName"].ToString();
                }
                catch (Exception ex1)
                {
                    PageUtil.showToast(this.Page, "获取登录用户ID失败，请刷新页面或重新登录！");
                    return;
                }
                DataSet ds = user.getUser(user_name);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    bool a = user.insertUsers(user_name, description, enabled, create_by, department);
                    if (a == true)
                    {
                        try
                        {
                            string user_name2 = user_name1.Value;
                            string enabled1 = Enabled1.Items[Enabled1.SelectedIndex].Value;
                            string department1 = Request.Form["Select1"];
                            string description1 = Textarea1.Value;
                            users_dataset = user.getUsersBySome(create_by, user_name2, description1, enabled1, department1);
                            Line_Repeater.DataSource = users_dataset;
                            Line_Repeater.DataBind();
                        }
                        catch (Exception e1)
                        {
                            PageUtil.showToast(this.Page, "查询数据出错，添加数据成功！");
                        }
                        Frame_name.Value = string.Empty;
                        Enabled.SelectedIndex = 0;
                        Description.Value = string.Empty;
                        PageUtil.showToast(this.Page, "添加数据成功！");
                    }
                    else
                    {
                        PageUtil.showToast(this.Page, "添加数据失败！");
                    }
                }
                else
                {
                    PageUtil.showToast(this.Page, "用户已存在,添加失败！");
                }

            }
        }
        //protected void cancelAdd(object sender, EventArgs e)
        //{
        //    Frame_name.Value = string.Empty;
        //    Enabled.SelectedIndex = 0;
        //    Department.SelectedIndex = 0;
        //    Description.Value = string.Empty;
        //}
        protected void notarizeUpdate(object sender, EventArgs e)
        {
            int user_id = Int32.Parse(Label2.Value);
            string user_name = Frame_name2.Value;
            string enabled = Enabled2.Value;
            string department = Request.Form["Department2"];
            string description = Description2.Value;
            DateTime updateTime = DateTime.Now;
            if (user_name.Length >= 20)
            {
                PageUtil.showToast(this.Page, "用户名长度过长！");
            }
            else if (description.Length >= 20)
            {
                PageUtil.showToast(this.Page, "描述长度过长！");
            }
            else if (user_name.Length == 0)
            {
                PageUtil.showToast(this.Page, "请输入用户名");
            }
            else
            {
                string create_by = String.Empty;
                try
                {
                    create_by = Session["LoginName"].ToString();
                }
                catch (Exception ex1)
                {
                    PageUtil.showToast(this.Page, "获取登录用户ID失败，请刷新页面或重新登录！");
                    return;
                }
                try
                {
                    bool a = user.updateUsers(user_id, user_name, description, enabled, create_by, department, updateTime);
                    if (a == true)
                    {
                        try
                        {
                            string user_name2 = user_name1.Value;
                            string enabled1 = Enabled1.Items[Enabled1.SelectedIndex].Value;
                            string department1 = Request.Form["Select1"];
                            string description1 = Textarea1.Value;
                            users_dataset = user.getUsersBySome(create_by, user_name2, description1, enabled1, department1);
                            Line_Repeater.DataSource = users_dataset;
                            Line_Repeater.DataBind();
                        }
                        catch (Exception e1)
                        {
                            PageUtil.showToast(this.Page, "查询数据失败，更新数据成功！");
                        }
                        PageUtil.showToast(this.Page, "更新数据成功！");
                    }
                    else
                    {
                        PageUtil.showToast(this.Page, "用户名已存在！");
                    }
                }
                catch (Exception ex)
                {
                    PageUtil.showToast(this.Page, "更新失败！");
                }
            }
        }
        //protected void cancelUpdate(object sender, EventArgs e)
        //{
        //    Enabled2.SelectedIndex = 0;
        //    Department2.SelectedIndex = 0;
        //    Description2.Value = string.Empty;
        //}
        protected void notarizeDelete(object sender, EventArgs e)
        {
            int user_id = Int32.Parse(lab.Value);
            bool a = user.deleteUsers(user_id);
            string create_by = String.Empty;
            try
            {
                create_by = Session["LoginName"].ToString();
            }
            catch (Exception ex1)
            {
                PageUtil.showToast(this.Page, "获取登录用户ID失败，请刷新页面或重新登录！");
                return;
            }
            if (a == true)
            {
                try
                {
                    string user_name2 = user_name1.Value;
                    string enabled1 = Enabled1.Items[Enabled1.SelectedIndex].Value;
                    string department1 = Request.Form["Select1"];
                    string description1 = Textarea1.Value;
                    users_dataset = user.getUsersBySome(create_by, user_name2, description1, enabled1, department1);
                    Line_Repeater.DataSource = users_dataset;
                    Line_Repeater.DataBind();
                }
                catch (Exception e1)
                {
                    PageUtil.showToast(this.Page, "查询数据成功，删除数据失败！");
                }
                PageUtil.showToast(this.Page, "删除数据成功！");
            }
            else
            {
                PageUtil.showToast(this.Page, "删除数据成功！");
            }
        }
        //protected void cancelDelete(object sender, EventArgs e)
        //{
        //    lab.Value = string.Empty;
        //}
        protected void selectAll(object sender, EventArgs e)
        {
            try
            {
                string user_name = user_name1.Value;
                string enabled = Enabled1.Items[Enabled1.SelectedIndex].Value;
                string create_by = String.Empty;
                try
                {
                    create_by = Session["LoginName"].ToString();
                }
                catch (Exception ex1)
                {
                    PageUtil.showToast(this.Page, "获取登录用户ID失败，请刷新页面或重新登录！");
                    return;
                }
                string department = Request.Form["Select1"];
                string description = Textarea1.Value;
                users_dataset = user.getUsersBySome(create_by, user_name, description, enabled, department);
                Line_Repeater.DataSource = users_dataset;
                Line_Repeater.DataBind();
            }
            catch (Exception e1)
            {
                PageUtil.showToast(this.Page, "查询数据失败！");
            }

        }
    }
}