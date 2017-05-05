using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;
using WMS_v1._0.Util;

namespace WMS_v1._0.PDA
{
    public partial class AuthoritySettingPDA : System.Web.UI.Page
    {
        string enabled;
        WatchdogDC authority = new WatchdogDC();
        List<ModelWatchdog> Model_Authority = new List<ModelWatchdog>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "权限设定";
        }

        //插入按钮对应操作
        protected void Insert_Authority(object sender, EventArgs e)
        {
            string user_name = Request.Form["insert_user_id_Authority"];
            string insert_program_id = Request.Form["insert_program_id_Authority"];
            string insert_select_id_Authority1 = insert_select_id_Authority.Value;
            string create_by, update_by = "none";
            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }
            else
            {
                create_by = Session["LoginName"].ToString();

            }
            DateTime insert_time_Authority = new DateTime();
            insert_time_Authority = DateTime.Now;
            Boolean flag;
            try
            {
                if (String.IsNullOrEmpty(insert_program_id) && String.IsNullOrEmpty(user_name))
                {
                    PageUtil.showToast(this, "界面名称且用户名不能为空！");
                    return;
                }
                else
                {
                    Model_Authority = authority.getWatchdogBySome(user_name, insert_program_id, "");
                    if (Model_Authority == null)                 //判断是否已存在该用户名
                    {
                        flag = authority.insertWatchdog(user_name, insert_program_id, insert_select_id_Authority1, create_by, update_by, insert_time_Authority);
                        if (flag == true)
                        {

                            string temp = "数据插入成功！";
                            PageUtil.showToast(this, temp);
                            Model_Authority = authority.getWatchdogBySome(user_name, insert_program_id, insert_select_id_Authority1);
                            AuthoritySetting_Repeater.DataSource = Model_Authority;
                            AuthoritySetting_Repeater.DataBind();
                        }
                        else
                        {
                            string temp = "数据插入失败！";
                            PageUtil.showToast(this, temp);
                        }
                    }
                    else
                    {
                        PageUtil.showToast(this, "该数据已存在！");
                    }
                }
            }
            catch (Exception)
            {
                PageUtil.showToast(this, "请输入数字类型的界面名称和用户名！");
            }


        }


        //查询按钮对应操作
        protected void Select_Authority(object sender, EventArgs e)
        {
            string user_id = Request.Form["user_id_Authority"];
            string program_id = Request.Form["program_id_Authority"];
            enabled = select_id_Authority.Value;
            WatchdogDC authority = new WatchdogDC();
            Model_Authority = authority.getWatchdogBySome(user_id, program_id, enabled); //调用DataCenter中的WatchdogDC里面的getWatchdogBySome()方法
            if (Model_Authority != null)
            {
                string temp = "数据查询成功！";
                PageUtil.showToast(this, temp);
                AuthoritySetting_Repeater.DataSource = Model_Authority;
                AuthoritySetting_Repeater.DataBind();
            }
            else
            {
                string temp = "数据库没有该数据，查询失败！";
                PageUtil.showToast(this, temp);
            }
            select_id_Authority.Value = String.Empty;
        }



        //更新按钮对应操作
        protected void Update_Authority(object sender, EventArgs e)
        {
            int upadate_program_id1;
            try
            {
                upadate_program_id1 = Convert.ToInt32(update_program_id.Value);
            }
            catch
            {
                PageUtil.showToast(this, "ID数据转换错误！");
                return;

            }
            string user_name = update_user_id_authority.Value;
            string update_program_id_Authority1 = update_program_id_Authority.Value;
            string enabled1 = update_select_id_Authority.Value;
            string update_by;
            DateTime update_time_Authority = new DateTime();
            update_time_Authority = DateTime.Now;
            Boolean flag;

            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
                //Response.Write("<script>alert(‘未获取到你的登陆状态，请退出系统重新登录’);window.location.href ='Login.aspx';</script>"); 
            }
            else
            {
                update_by = Session["LoginName"].ToString();
            }


            Model_Authority = authority.getWatchdogBySome(user_name, update_program_id_Authority1, enabled1);
            if (Model_Authority == null)
            {

                flag = authority.updateWatchdog(upadate_program_id1, enabled1, update_by, update_time_Authority);//调用DataCenter中的WatchdogDC里面的getWatchdogBySome()方法
                if (flag == true)
                {
                    string temp = "数据更新成功！";
                    PageUtil.showToast(this, temp);
                    Model_Authority = authority.getWatchdogBySome(user_name, update_program_id_Authority1, enabled1);
                    AuthoritySetting_Repeater.DataSource = Model_Authority;
                    AuthoritySetting_Repeater.DataBind();
                }
                else
                {
                    string temp = "数据更新失败！";
                    PageUtil.showToast(this, temp);
                }
            }
            else
            {
                PageUtil.showToast(this, "该条数据已存在");
            }


        }




        //删除按钮对应操作
        protected void Delete_Authority(object sender, EventArgs e)
        {

            int delete_id_authority1;
            try
            {
                delete_id_authority1 = Convert.ToInt32(delete_user_id_authority.Value);
            }
            catch
            {
                PageUtil.showToast(this, "ID数据转换错误！");
                return;
            };


            Boolean flag;
            try
            {
                flag = authority.deleteWatchdog(delete_id_authority1);//调用DataCenter中的WatchdogDC里面的deleteWatchdog()方法
                if (flag == true)
                {
                    string temp = "该条数据删除成功，其他数据请查询！";
                    PageUtil.showToast(this, temp);
                    AuthoritySetting_Repeater.DataSource = Model_Authority;
                    AuthoritySetting_Repeater.DataBind();
                }
                else
                {
                    string temp = "数据删除失败！";
                    PageUtil.showToast(this, temp);
                }
            }
            catch (Exception)
            {
                PageUtil.showToast(this, "界面名称不是数字类型！");
            }



        }
        //清除按钮操作------清除输入框中数据
        protected void CleanAllMeassage_Click(object sender, EventArgs e)
        {
            AuthoritySetting_Repeater.DataBind();
            PageUtil.showToast(this, "成功清除查询结果");
        }
    }
}