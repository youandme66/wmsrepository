using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.PDA
{
    public partial class RegisterPDA : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Username1 = user_name1.Value.Trim();
            string department1 = department.Value.Trim();
            string password1 = password.Value.Trim();
            UsersDC userDC = new UsersDC();
            string prompt = userDC.register(Username1, department1, password1);//传注册信息
            if (prompt == "注册成功！")
                Response.Write("<script>alert('" + prompt + "');window.location.href ='LoginPDA.aspx';</script>"); //输出注册结果并返回登录页面
            else
            {
                Response.Write("<script>alert('" + prompt + "');</script>");
                user_name1.Value = String.Empty;
                department.Value = String.Empty;
                password.Value = String.Empty;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPDA.aspx");
        }
    }
}