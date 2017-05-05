using System;
using System.Collections.Generic;
using System.Data;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.Web
{
    public partial class ReinspectionWork : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "PO退回";
            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
                return;
            }
            if (!IsPostBack)
            {
                SubinventoryDC subDC = new SubinventoryDC();
                List<string> sub_list = subDC.getAllSubinventory();
                if (sub_list != null)
                {
                    subinventory_select.DataSource = sub_list;
                    subinventory_select.DataBind();

                }
                else PageUtil.showToast(this, "库别获取出错！");
                subinventory_select.Items.Insert(0, "--选择库别--");

                List<string> list = new List<string>();
                list.Add("PASS");
                list.Add("NO");
                reinspect_result_select.DataSource = list;
                reinspect_result_select.DataBind();
            }
        }

        /// <summary>
        /// 点击提交按钮，检验该料号是否需要复验
        /// 如需要，则保存需要复验的料号的复验信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void commit_button_Click(object sender, EventArgs e)
        {
            string user = "";
            try
            {
                user = Session["LoginName"].ToString();
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "获取登录信息失败，请重新登录！");
                return;
            }
            string item_name = item_name_input.Text.Trim();
            string datecode = datecode_input.Text.Trim();
            string subinventory = subinventory_select.SelectedValue.Trim();

            if (string.IsNullOrEmpty(item_name) || string.IsNullOrEmpty(datecode) || subinventory.Equals("--选择库别--"))
            {
                PageUtil.showToast(this, "请输入完整数据！");
                return;
            }
            if (checkStatus(item_name, datecode, subinventory))
            {
                string result = reinspect_result_select.SelectedValue.ToString();
                if (string.IsNullOrEmpty(result))
                {
                    PageUtil.showToast(this, "复验结果数据异常！");
                    return;
                }
                if (!hassaved(item_name, datecode, subinventory))
                {
                    string remark = remark_input.Value.Trim();
                    if (save_reinspect_result(item_name, datecode, subinventory, result,remark,user))
                    {
                        PageUtil.showToast(this, "保存成功！");
                    }
                    else PageUtil.showToast(this, "保存失败，请检查数据格式！");
                }
                else
                {
                    PageUtil.showToast(this, "保存失败，该料号复验信息已保存！");
                }
            }
        }

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void clear_button_Click(object sender, EventArgs e)
        {
            item_name_input.Text = "";
            datecode_input.Text = "";
            subinventory_select.SelectedIndex = 0;
            remark_input.Value = "";
            reinspect_result_select.SelectedIndex = 0;
        }

        /// <summary>
        /// 检查该料号是否需要复验
        /// </summary>
        /// <param name="item_name"></param>
        /// <param name="datecode"></param>
        /// <param name="subinventory"></param>
        /// <returns></returns>
        public bool checkStatus(string item_name, string datecode, string subinventory)
        {
            ReinspectHeaderDC dc = new ReinspectHeaderDC();
            DataSet ds = dc.get_All_By_itemname_datecode_subiventory(item_name, datecode, subinventory);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                PageUtil.showToast(this, "未查询到该数据，请检查输入！");
                return false;
            }
            else
            {
                string status = ds.Tables[0].Rows[0]["last_reinspect_status"].ToString();
                if (string.IsNullOrEmpty(status))
                {
                    PageUtil.showToast(this, "数据异常！");
                    return false;
                }
                else if (status.Equals("PENG"))
                {
                    return true;
                }
                else
                {
                    PageUtil.showToast(this, "该料号无需复验！");
                    return false;
                }
            }
        }

        /// <summary>
        /// 保存该料号的复验信息
        /// </summary>
        /// <param name="item_name"></param>
        /// <param name="datecode"></param>
        /// <param name="subinventory"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool save_reinspect_result(string item_name, string datecode, string subinventory, string result,string remark, string user)
        {

            ReinspectHeaderDC dc = new ReinspectHeaderDC();
            return dc.insertReinspectHeader(item_name, datecode, subinventory, result,remark, user, DateTime.Now);
        }

        /// <summary>
        /// 判断该料号复验结果是否已保存
        /// </summary>
        /// <param name="item_name"></param>
        /// <param name="datecode"></param>
        /// <param name="subinventory"></param>
        /// <returns></returns>
        public bool hassaved(string item_name, string datecode, string subinventory)
        {
            ReinspectHeaderDC dc = new ReinspectHeaderDC();
            DataSet ds = dc.select_By_itemname_datecode_sub(item_name, datecode, subinventory);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else return true;
        }

    }
}