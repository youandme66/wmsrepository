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
    public partial class DpartmentSettingPDA : System.Web.UI.Page
    {
        DepartmentDC Department = new DepartmentDC();
        List<ModelDepartment> Department_list, list;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "部门设定";
        }


        //判断部门名称长度是否超出范围
        protected bool StrLength_name(string str)
        {
            if (str.Length > 20)
            {
                string temp = "部门名称长度不能超过20！";
                PageUtil.showToast(this, temp);
                return false;
            }
            else
                return true;
        }





        protected void Insert_Click_Department(object sender, EventArgs e)
        {
            string enabled = enabled_insert.Value;
            string flex_value = flex_value_insert.Value;
            string description = description_insert.Value.Trim();
            DateTime department_inserttime = new DateTime();
            department_inserttime = DateTime.Now;
            if (!StrLength_name(description_insert.Value))   //调用StrLength_name（）判断部门名称长度是否超出范围
                return;
            Boolean flag;
            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "登录名为空");
                return;
            }
            string login_name = Session["LoginName"].ToString();

            if (String.IsNullOrEmpty(description_insert.Value))     //部门名称不能为空
            {
                string temp = "部门名称不能为空！";
                PageUtil.showToast(this, temp);
            }
            else
            {
                Department_list = Department.getDepartmentBySome(flex_value, description);
                if (Department_list == null && list == null)
                {
                    flag = Department.insertDepartment(flex_value, description, enabled, department_inserttime, login_name);//调用DataCenter中DepartmentDC.cs里面的insertDepartment()方法
                    if (flag == true)
                    {
                        string temp = "数据插入成功！";
                        PageUtil.showToast(this, temp);
                        Department_list = Department.getDepartmentBySome(-1, flex_value, description, enabled);
                        Department_Repeater.DataSource = Department_list;
                        Department_Repeater.DataBind();
                    }
                    else
                    {
                        string temp = "数据插入失败！";
                        PageUtil.showToast(this, temp);
                    }
                }
                else
                {
                    string temp = "部门编号或者部门名称已存在,不能重复！";
                    PageUtil.showToast(this, temp);
                    return;
                }
            }


        }

        protected void Select_Click_Department(object sender, EventArgs e)
        {
            string flex_value = flex_value_select.Value;
            string description = description_select.Value;
            string enabled = enabled_select.Value;

            Department_list = Department.getDepartmentBySome(-1, flex_value, description, enabled); //调用DataCenter中PnDC.cs里面的getPnBySome()方法
            if (Department_list == null)
            {
                string temp = "数据库没有该数据，查询失败！";
                PageUtil.showToast(this, temp);
            }
            else
            {
                Department_Repeater.DataSource = Department_list;
                Department_Repeater.DataBind();
                string temp = "查询成功！";
                PageUtil.showToast(this, temp);
            }
            flex_value_select.Value = String.Empty;  //清除查询部分的输入框内容
            description_select.Value = String.Empty;

        }


        protected void Update_Click_Department(object sender, EventArgs e)
        {
            string department_id = department_id_update.Value.Trim();
            string flex_value = flex_value_update.Value;
            string description = description_update.Value;
            string enabled = enabled_update_id.Value;
            string description_old = description_update_old.Value;//当不改变部门名称时
            int department_id1;
            try
            {
                department_id1 = Convert.ToInt32(department_id);
            }
            catch
            {
                PageUtil.showToast(this, "部门编号转换错误！");
                return;
            }
            DateTime department_updatetime = new DateTime();
            department_updatetime = DateTime.Now;
            string login_name = Session["LoginName"].ToString();
            Boolean flag;
            Department_list = Department.getDepartmentBySome(description);       //查询后判断是否已存在该部门名称                      
            if (Department_list == null)
            {
                flag = Department.updateDepartment(department_id1, flex_value, description, enabled, department_updatetime, login_name);  //调用DataCenter中PnDC.cs里面的updatePn()方法
                if (flag == true)
                {
                    string temp = "数据更新成功！";
                    PageUtil.showToast(this, temp);
                    Department_list = Department.getDepartmentBySome(-1, flex_value, description, "");
                    Department_Repeater.DataSource = Department_list;
                    Department_Repeater.DataBind();
                }
                else
                {
                    string temp = "数据更新失败！";
                    PageUtil.showToast(this, temp);
                }
            }
            else if (description == description_old)
            {
                flag = Department.updateDepartment(description_old, enabled, department_updatetime, login_name); //调用DataCenter中PnDC.cs里面的updatePn()方法 updateDepartment(string description, string enabled, DateTime update_time, string update_user)
                if (flag == true)
                {
                    string temp = "数据更新成功！";
                    PageUtil.showToast(this, temp);
                    Department_list = Department.getDepartmentBySome(-1, flex_value, description, "");
                    Department_Repeater.DataSource = Department_list;
                    Department_Repeater.DataBind();
                }
                else
                {
                    string temp = "数据更新失败！";
                    PageUtil.showToast(this, temp);
                }
            }
            else
            {
                PageUtil.showToast(this, "该部门名称已存在");
            }

        }

        protected void Delect_Click_Department(object sender, EventArgs e)
        {
            Boolean flag;
            string department_id = department_id_delete.Value;
            int department_id1;
            try
            {
                department_id1 = Convert.ToInt32(department_id);
            }
            catch
            {
                PageUtil.showToast(this, "部门编号转换错误！");
                return;
            }
            flag = Department.deleteDepartment(department_id1);             //调用DataCenter中PnDC.cs里面的deletePn()方法
            if (flag == true)
            {
                string temp = "该条数据删除成功，其他数据请查询！";
                PageUtil.showToast(this, temp);
                Department_Repeater.DataSource = Department_list;
                Department_Repeater.DataBind();
            }
            else
            {
                string temp = "数据删除失败！";
                PageUtil.showToast(this, temp);
            }

        }


        protected void Clean_input_Click(object sender, EventArgs e)
        {
            flex_value_select.Value = String.Empty;  //清除查询部分的输入框内容
            description_select.Value = String.Empty;


            Department_Repeater.DataBind();
            PageUtil.showToast(this, "成功清除查询结果");
        }






    }
}