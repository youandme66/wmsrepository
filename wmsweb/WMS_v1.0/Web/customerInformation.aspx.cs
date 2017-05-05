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

namespace customerInformation
{
    public partial class customerInformation : System.Web.UI.Page
    {
        DataSet customers = new DataSet();
        CustomersDC customer = new CustomersDC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { }
            Session["Local"] = "客户设定";
        }
        protected void clear(object sender, EventArgs e)
        {
            user_name1.Value = string.Empty;
        }
        protected void notarizeInsert(object sender, EventArgs e)
        {
            string customer_name = user_name.Value;
            string code = customer_code1.Value;
            if (customer_name.Length >= 10)
            {
                PageUtil.showToast(this.Page, "客户名长度过长！");
            }
            else if (code.Length >= 10)
            {
                PageUtil.showToast(this.Page, "客户代码长度过长！");
            }
            else if (code.Length ==0)
            {
                PageUtil.showToast(this.Page, "客户代码为空");
            }
            else if (customer_name.Length == 0)
            {
                PageUtil.showToast(this.Page, "客户名称为空");
            }
            else
            {
                string user_id = String.Empty;
                try
                {
                    user_id = Session["LoginName"].ToString();
                }
                catch (Exception ex1)
                {
                    PageUtil.showToast(this.Page, "获取登录用户ID失败，请刷新页面或重新登录！");
                    return;
                }
                DataSet ds = customer.getCustomer(customer_name);
                DataSet ds2 = customer.getCustomer2(code);
                if (ds.Tables[0].Rows.Count == 0&&ds2.Tables[0].Rows.Count==0)
                {
                    bool a = customer.insertCustomers(customer_name, user_id, code);
                    if (a == true)
                    {
                        try
                        {
                            string customer_name1 = user_name1.Value;
                            string customer_code = code1.Value;
                            customers = customer.getCustomersBySome(customer_name1, customer_code);
                            Line_Repeater.DataSource = customers;
                            Line_Repeater.DataBind();
                            user_name.Value = string.Empty;
                        }
                        catch (Exception e1)
                        {
                            PageUtil.showToast(this.Page, "查询数据失败，添加数据成功！");
                        }
                        PageUtil.showToast(this.Page, "添加数据成功！");
                    }
                    else
                    {
                        PageUtil.showToast(this.Page, "添加数据失败！");
                    }
                }
                else
                {
                    PageUtil.showToast(this.Page, "客户名或客户代码已存在,无法添加！");
                }
            }
        }
        protected void cancelInsert(object sender, EventArgs e)
        {
            user_name.Value = string.Empty;
        }
        protected void notarizeUpdate(object sender, EventArgs e)
        {
            string user_id = String.Empty;
            try
            {
                user_id = Session["LoginName"].ToString();
            }
            catch (Exception ex1)
            {
                PageUtil.showToast(this.Page, "获取登录用户ID失败，请刷新页面或重新登录！");
                return;
            }
            string customer_key =Label2.Value;
            string customer_name = user_name2.Value;
            int key = int.Parse(key1.Value);
            if (customer_name.Length >= 10)
            {
                PageUtil.showToast(this.Page, "客户名长度过长！");
            }else if(customer_key.Length>=10){
                PageUtil.showToast(this.Page, "客户代码过长！");
            }
            else if (customer_key.Length == 0)
            {
                PageUtil.showToast(this.Page, "请输入客户名代码");
            }
            else if (customer_name.Length == 10)
            {
                PageUtil.showToast(this.Page, "请输入客户名称");
            }
            else
            {
                DataSet ds = customer.getCustomerCount(customer_key,customer_name);
                var count=ds.Tables[0].Rows.Count;
                if (ds.Tables[0].Rows.Count <2)
                {
                    DateTime update_time = DateTime.Now;
                    bool a = customer.updateCustomers(customer_name, user_id, update_time, customer_key,key);
                    if (a == true)
                    {
                        try
                        {
                            string customer_name1 = user_name1.Value;
                            string customer_code = code1.Value;
                            customers = customer.getCustomersBySome(customer_name1, customer_code);
                            Line_Repeater.DataSource = customers;
                            Line_Repeater.DataBind();
                            user_name.Value = string.Empty;
                        }
                        catch (Exception e1)
                        {
                            PageUtil.showToast(this.Page, "查询数据失败，更新数据成功！");
                        }
                        PageUtil.showToast(this.Page, "更新数据成功！");
                    }
                    else
                    {
                        PageUtil.showToast(this.Page, "更新数据失败！");
                    }
                }
                else
                {
                    PageUtil.showToast(this.Page, "客户名已存在，更新失败！");
                }
            }
        }
        protected void notarizeDelete(object sender, EventArgs e)
        {
            string customer_name = lab.Value;
            bool a = customer.deleteCustomers(customer_name);
            if (a == true)
            {
                try
                {
                    string customer_code = code1.Value;
                    string customer_name1 = user_name1.Value;
                    customers = customer.getCustomersBySome(customer_name1,customer_code);
                    Line_Repeater.DataSource = customers;
                    Line_Repeater.DataBind();
                    user_name.Value = string.Empty;
                }
                catch (Exception e1)
                {
                    PageUtil.showToast(this.Page, "查询数据失败，删除数据成功！");
                    return;
                }
                PageUtil.showToast(this.Page, "删除数据成功！");
            }
            else
            {
                PageUtil.showToast(this.Page, "删除数据失败！");
            }
        }
        protected void selectSomeBySome(object sender, EventArgs e)
        {
            try
            {
                string customer_code = code1.Value;
                string customer_name = user_name1.Value;
                customers = customer.getCustomersBySome(customer_name,customer_code);
                Line_Repeater.DataSource = customers;
                Line_Repeater.DataBind();
            }
            catch (Exception e1)
            {
                PageUtil.showToast(this.Page, "查询数据失败！");
            }

        }
    }
}