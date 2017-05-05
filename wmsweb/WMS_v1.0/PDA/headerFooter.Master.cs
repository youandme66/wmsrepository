using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.PDA//作者：周雅雯 时间：2016/8/13
{
    public partial class headerFooter1 : System.Web.UI.MasterPage//母版页对应后台代码
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //设定处于什么界面
            LabelLocal.Text = "Copyright 2016-       您正处于" + Session["Local"].ToString() + "页面";
            ////设定当前时间
            //LabelLoginTime.Text = DateTime.Now.ToString();
            ////获取当前用户用户名
            //UsersDC userDC = new UsersDC();
            //try
            //{
            //    int id = int.Parse(Session["LoginID"].ToString());
            //    //将获取到的用户ID,用户名显示在母版页中
            //    LabelUserID.Text = id.ToString();
            //    LabelUser.Text = userDC.searchUserNameByID(id);
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("<script>alert('获取当前用户失败！请检查是否已登录')</script>");
            //}

        }
    }
}