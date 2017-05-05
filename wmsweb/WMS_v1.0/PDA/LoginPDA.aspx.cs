using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WMS_v1._0.DataCenter;

namespace WMS_v1._0.PDA
{
    public partial class LoginPDA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //退出系统时（即跳转到登录页面时）将session清空
            Session["LoginId"] = null;
            Session["LoginName"] = null;

        }
        protected void login_Click(object sender, EventArgs e)
        {
            string user_name = username.Value.Trim();
            string user_password = password.Value.Trim();
            UsersDC udc = new UsersDC();
            if (udc.login(user_name, user_password))
            {
                Model.ModelUsers user = udc.searchUsersByName(user_name);
                //如果登陆成功，设置session =>登陆者id和登录名
                Session["LoginId"] = user.User_id;
                Session["LoginName"] = user_name;
                Session["Local"] = "welcome";
                //Response.Write("<script>alert('');</script>");
                Response.Redirect("WelcomePDA.aspx");
            }
            else
            {
                //弹出提示，帐号或密码错误
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('帐号或密码错误，请重新输入！');</script>");
            }
        }
        }
}