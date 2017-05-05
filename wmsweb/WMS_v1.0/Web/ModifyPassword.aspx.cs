using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.Web
{
    public partial class ModifyPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        protected void submit_Click(object sender, EventArgs e)
        {
            string user_name = username.Value;
            string user_password = password.Value;
            string new_password = newpassword2.Value;
            UsersDC udc = new UsersDC();
            if (udc.resetPassWord(user_name, user_password, new_password))
            {
                PageUtil.showAlert(this, "密码修改成功！");
            }
            else
            {
                PageUtil.showAlert(this, "帐号或密码错误！");
            }
            username.Value = string.Empty;
            password.Value = string.Empty;
            newpassword1.Value = string.Empty;
            newpassword2.Value = string.Empty;
        }
    }
}