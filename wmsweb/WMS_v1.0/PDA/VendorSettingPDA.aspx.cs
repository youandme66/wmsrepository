using System;
using System.Data;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.PDA
{
    public partial class VendorSettingPDA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "供应商设定";
        }
        protected void clear(object sender, EventArgs e)
        {
            vendor_name_id.Value = string.Empty;
            vendor_key_id.Value = string.Empty;
            Line_Repeater.DataSource = new DataTable();
            Line_Repeater.DataBind();
            PageUtil.showToast(this, "清除成功！");
        }
        //插入数据
        protected void notarizeInsert(object sender, EventArgs e)
        {
            int loginid = getLoginUserID();
            if (loginid == -1) return;
            string vendor_name = vendor_name_insert.Value.Trim();
            string vendor_key = vendor_key_insert.Value.Trim();
            if (!checkData(vendor_name, vendor_key)) return;
            SupplierDC supplierDC = new SupplierDC();
            DataSet search_ds = supplierDC.getSupplierByNameAndKey(vendor_name, vendor_key);
            if (search_ds != null && search_ds.Tables[0].Rows.Count > 0)
            {
                PageUtil.showToast(this, "供应商名称或者供应商代码已存在！");
                return;
            }
            else if (supplierDC.insertSupplier(vendor_name, loginid, vendor_key, DateTime.Now))
            {
                PageUtil.showToast(this, "数据插入成功！");
                vendor_name_insert.Value = "";
                vendor_key_insert.Value = "";
                Line_Repeater.DataSource = supplierDC.getSupplierBySome("", "");
                Line_Repeater.DataBind();
            }
            else PageUtil.showToast(this, "数据插入失败！");

        }
        //取消
        protected void cancelInsert(object sender, EventArgs e)
        {
            vendor_key_insert.Value = string.Empty;
            vendor_name_insert.Value = string.Empty;
        }
        //更新数据
        protected void notarizeUpdate(object sender, EventArgs e)
        {
            int loginid = getLoginUserID();
            if (loginid == -1) return;
            string vendor_id = vendor_id_update.Value.Trim();
            string vendor_name = vendor_name_update.Value.Trim();
            string vendor_key = vendor_key_update.Value.Trim();
            if (!checkData(vendor_name, vendor_key)) return;
            SupplierDC supplierDC = new SupplierDC();
            DataSet search_ds = supplierDC.getSupplierByNameAndKeyAndId(vendor_id, vendor_name, vendor_key);
            //如过大于2，则数据库中已存在该name和key，否则 就可以更新
            if (search_ds != null && search_ds.Tables[0].Rows.Count > 0)
            {
                PageUtil.showToast(this, "供应商名称或者供应商代码已存在！");
                return;
            }
            if (supplierDC.updateSupplier(vendor_id, vendor_name, loginid, vendor_key, DateTime.Now))
            {
                PageUtil.showToast(this, "数据更新成功！");
                string vendor_name1 = vendor_name_id.Value.Trim();
                string vendor_key1 = vendor_key_id.Value.Trim();
                Line_Repeater.DataSource = supplierDC.getSupplierBySome(vendor_name1, vendor_key1);
                Line_Repeater.DataBind();
            }
            else PageUtil.showToast(this, "数据更新失败！");
        }
        //删除数据
        protected void notarizeDelete(object sender, EventArgs e)
        {
            int loginid = getLoginUserID();
            if (loginid == -1) return;
            int vendor_id = -1;
            try
            {
                vendor_id = int.Parse(vendor_id_delete.Value.ToString());
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "数据异常，删除失败！");
                return;
            }
            SupplierDC supplierDC = new SupplierDC();
            if (supplierDC.deleteSupplier(vendor_id))
            {
                PageUtil.showToast(this, "数据删除成功！");
                string vendor_name1 = vendor_name_id.Value.Trim();
                string vendor_key1 = vendor_key_id.Value.Trim();
                Line_Repeater.DataSource = supplierDC.getSupplierBySome(vendor_name1, vendor_key1);
                Line_Repeater.DataBind();
            }
            else PageUtil.showToast(this, "数据删除失败！");
        }
        //查询
        protected void selectSome(object sender, EventArgs e)
        {
            string vendor_name = vendor_name_id.Value.Trim();
            string vendor_key = vendor_key_id.Value.Trim();

            SupplierDC supplierDC = new SupplierDC();
            DataSet ds = supplierDC.getSupplierBySome(vendor_name, vendor_key);
            if (ds == null)
            {
                PageUtil.showToast(this, "未查询到数据！");
                return;
            }
            Line_Repeater.DataSource = ds;
            Line_Repeater.DataBind();
        }
        //获取登录的ID
        private int getLoginUserID()
        {
            int user_id = -1;
            try
            {
                user_id = int.Parse(Session["LoginId"].ToString());
            }
            catch (Exception ex1)
            {
                PageUtil.showToast(this.Page, "获取登录用户名失败，请刷新页面或重新登录！");
                return -1;
            }
            return user_id;
        }
        private bool checkData(string vendor_name, string vendor_key)
        {

            if (string.IsNullOrEmpty(vendor_key) || string.IsNullOrEmpty(vendor_name))
            {
                PageUtil.showToast(this.Page, "请输入完整的数据!");
                return false;
            }
            if (vendor_name.Length > 240 || vendor_key.Length > 15)
            {
                PageUtil.showToast(this.Page, "供应商名或供应商代码长度过长");
                return false;
            }
            return true;
        }
    }
}