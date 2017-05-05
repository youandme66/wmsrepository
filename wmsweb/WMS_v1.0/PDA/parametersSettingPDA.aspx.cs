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
    public partial class parametersSettingPDA : System.Web.UI.Page
    {
        ParametersDC Parameters = new ParametersDC();
        List<ModelParameters> list = new List<ModelParameters>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "参数设定";
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void parameters_Insert(object sender, EventArgs e)
        {
            int lookup_type;
            string lookup_code = Lookup_code_insert.Value;
            string meaning = Meaning_insert.Value;
            string description = Description_insert.Value;
            string enabled = enabled_insert.Value;
            int create_by;
            if (Session["LoginId"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }
            create_by = int.Parse(Session["LoginId"].ToString());
            if (lookup_code.Length > 30)
            {
                PageUtil.showToast(this, "输入的对应栏位长度不得超过30个字符！");
                return;
            }
            if (meaning.Length > 80)
            {
                PageUtil.showToast(this, "输入的数据字段长度不得超过80个字符！");
                return;
            }
            if (description.Length > 255)
            {
                PageUtil.showToast(this, "输入的数据描述长度不得超过255个字符！");
                return;
            }
            if (Lookup_type_insert.Value == "")
            {
                lookup_type = 0;
            }
            else
            {
                try
                {
                    lookup_type = Convert.ToInt32(Lookup_type_insert.Value);
                }
                catch
                {
                    PageUtil.showToast(this, "数据表名输入格式错误！");
                    return;
                }
            }
            if (Lookup_type_insert.Value == string.Empty || lookup_code == string.Empty || meaning == string.Empty || description == string.Empty || enabled == string.Empty)
            {
                PageUtil.showToast(this, "添加数据不能为空！");
                return;
            }
            if (Parameters.getParametersByLookup_type(lookup_type) != null)                   //判断该数据表名是否已存在
            {
                PageUtil.showToast(this, "该数据表名已存在！");
                return;
            }

            bool flag = new bool();
            flag = Parameters.insertParameters(lookup_type, lookup_code, meaning, description, enabled, create_by, DateTime.Now);
            if (flag == true)
            {
                PageUtil.showToast(this, "添加数据成功！");
            }
            else
            {
                PageUtil.showToast(this, "添加数据失败！");
            }
            Lookup_type_insert.Value = String.Empty;
            Lookup_code_insert.Value = String.Empty;
            Meaning_insert.Value = String.Empty;
            Description_insert.Value = String.Empty;
            enabled_insert.Value = String.Empty;
        }


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void parameters_Update(object sender, EventArgs e)
        {
            int lookup_type;
            string lookup_code = Lookup_code_update.Value;
            string meaning = Meaning_update.Value;
            string description = Description_update.Value;
            string enabled = enabled_update.Value;
            int update_by;
            if (Session["LoginId"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }
            update_by = int.Parse(Session["LoginId"].ToString());

            //if(update_by==null)
            //{
            //    PageUtil.showToast(this, "未获取到你的登陆状态，请重新登录！");
            //    Response.Redirect("Login.aspx");
            //}
            if (lookup_code.Length > 30)
            {
                PageUtil.showToast(this, "输入的对应栏位长度不得超过30个字符！");
                return;
            }
            if (meaning.Length > 80)
            {
                PageUtil.showToast(this, "输入的数据字段长度不得超过80个字符！");
                return;
            }
            if (description.Length > 255)
            {
                PageUtil.showToast(this, "输入的数据描述长度不得超过255个字符！");
                return;
            }
            if (Lookup_type_update.Value == "")
            {
                lookup_type = 0;
            }
            else
            {
                try
                {
                    lookup_type = Convert.ToInt32(Lookup_type_update.Value);
                }
                catch
                {
                    PageUtil.showToast(this, "数据表名输入格式错误！");
                    return;
                }
            }

            if (Lookup_type_update.Value == string.Empty || lookup_code == string.Empty || meaning == string.Empty || description == string.Empty || enabled == string.Empty)
            {
                PageUtil.showToast(this, "更新数据不能为空！");
                return;
            }
            bool flag = new bool();
            flag = Parameters.updateParameters(lookup_type, lookup_code, meaning, description, enabled, update_by, DateTime.Now);
            if (flag == true)
            {
                PageUtil.showToast(this, "更新数据成功！");
            }
            else
            {
                PageUtil.showToast(this, "更新数据失败！");
            }
            list = Parameters.getParameters();
            parameters_repeater.DataSource = list;
            parameters_repeater.DataBind();
            Lookup_type_update.Value = String.Empty;
            Lookup_code_update.Value = String.Empty;
            Meaning_update.Value = String.Empty;
            Description_update.Value = String.Empty;
            enabled_update.Value = String.Empty;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void parameters_Delete(object sender, EventArgs e)
        {
            int lookup_type = Convert.ToInt32(Lookup_type_Delet.Value);
            bool flag = new bool();
            flag = Parameters.deleteParameters(lookup_type);
            if (flag == true)
            {
                PageUtil.showToast(this, "删除数据成功！");
            }
            else
            {
                PageUtil.showToast(this, "删除数据失败！");
            }
            list = Parameters.getParameters();
            parameters_repeater.DataSource = list;
            parameters_repeater.DataBind();
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void parameters_Select(object sender, EventArgs e)
        {
            string lookup_type = Lookup_type.Value;
            string lookup_code = Lookup_code.Value;
            string meaning = Meaning.Value;
            string description = Description.Value;
            string enabled = Enabled.Value;
            list = Parameters.getPnBySome(lookup_type, lookup_code, meaning, description, enabled);
            if (list == null)
            {
                if (lookup_type == string.Empty && lookup_code == string.Empty && meaning == string.Empty && description == string.Empty && enabled == string.Empty)
                {
                    PageUtil.showToast(this, "参数表中无任何数据！");
                }
                else
                    PageUtil.showToast(this, "参数表中无符合条件的数据！");
            }
            else
            {
                parameters_repeater.DataSource = list;
                parameters_repeater.DataBind();
            }
        }

        /// <summary>
        /// 清除添加框中的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void parameters_insert_clear(object sender, EventArgs e)
        {
            Lookup_type_insert.Value = String.Empty;
            Lookup_code_insert.Value = String.Empty;
            Meaning_insert.Value = String.Empty;
            Description_insert.Value = String.Empty;
            enabled_insert.Value = String.Empty;
        }


        /// <summary>
        /// 清除更新框中的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void parameters_update_clear(object sender, EventArgs e)
        {
            Lookup_type_update.Value = String.Empty;
            Lookup_code_update.Value = String.Empty;
            Meaning_update.Value = String.Empty;
            Description_update.Value = String.Empty;
            enabled_update.Value = String.Empty;
        }

        /// <summary>
        /// 清除查询框中的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void parameters_select_clear(object sender, EventArgs e)
        {
            Lookup_type.Value = String.Empty;
            Lookup_code.Value = String.Empty;
            Meaning.Value = String.Empty;
            Description.Value = String.Empty;
            Enabled.Value = String.Empty;
        }

    }
}