using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WMS_v1._0.Model;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.Web
{
    public partial class workOrderSetting : System.Web.UI.Page
    {
        List<ModelWO> list = new List<ModelWO>();
        WoDC wodc = new WoDC();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "工单设定";
            if (!IsPostBack)
            {
                BomDC bomdc = new BomDC();
                DataSet ds = bomdc.getBom_item();
                if (ds != null)
                {
                    DropDownList1.DataSource = ds;
                    DropDownList1.DataTextField = "bom_item_name";
                    DropDownList1.DataBind();
                    DropDownList3.DataSource = ds;
                    DropDownList3.DataTextField = "bom_item_name";
                    DropDownList3.DataBind();
                }
                else
                    PageUtil.showToast(this.Page, "没有任何bom可供产生工单");
                DropDownList1.Items.Insert(0, "选择大料号");
                DropDownList3.Items.Insert(0, "选择大料号");
                DropDownList2.Items.Insert(0, "选择版本号");
                DropDownList4.Items.Insert(0, "选择版本号");
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Insert(object sender, EventArgs e)
        {
            string part_no = DropDownList1.Items[DropDownList1.SelectedIndex].Value;
            string wo_no = Wo_no.Value;
            string status = Status.Value;
            string version = DropDownList2.Items[DropDownList2.SelectedIndex].Value;
            int  target_qty;
            //if (part_no.Length > 20)
            //{
            //    PageUtil.showToast(this, "输入的料号长度不得超过20个字符！");
            //    return;
            //}
            if (wo_no.Length > 50)
            {
                PageUtil.showToast(this, "输入的工单号长度不得超过50个字符！");
                return;
            }
            //if (Status.Value == "")
            //{
            //    status = 0;
            //}
            //else
            //{
            //    status = Convert.ToInt32(Status.Value);
            //}
            if (Target_qty.Value == "")
            {
                target_qty = 0;
            }
            else
            {
                  try
                     {
                         target_qty = Convert.ToInt32(Target_qty.Value);
                      }
                  catch
                 {
                     PageUtil.showToast(this, "工单对应数量输入格式错误！");
                    return;
                  }
             }
            if (part_no == "选择大料号")
            {
                PageUtil.showToast(this.Page, "请选择大料号");
                return;
            }
            if (version == "请选择版本号")
            {
                PageUtil.showToast(this.Page, "请选择版本号");
                return;
            }
            if (version==string.Empty || part_no == string.Empty || Status.Value == string.Empty || Target_qty.Value == string.Empty || wo_no == string.Empty)
            {
                PageUtil.showToast(this, "新增工单的输入数据不能为空！");
                return;
            }
            if (wodc.getWOByWo_no(wo_no) != null)
            {
                PageUtil.showToast(this, "该工单号已存在！");
                return;
            }
            bool flag = new bool();
            flag = wodc.insertWo(wo_no, status, target_qty, part_no,version, DateTime.Now);
            if (flag == true)
            {
                PageUtil.showToast(this, "添加数据成功！");
                Select(sender, e);
            }
            else
            {
                string add_work = "添加数据失败！";
                PageUtil.showToast(this, add_work);
            }
            //Part_no.Value = String.Empty;
            Wo_no.Value = String.Empty;
            Status.Value = String.Empty;
            Target_qty.Value = String.Empty;
        }

       /// <summary>
       /// 查找数据
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void Select(object sender, EventArgs e)
        {
            string part_no = Request.Form["Part_No_1"];
            string wo_no = Request.Form["Wo_No_1"];
            string status = Status_1.Value;
            string target_qty = Target_Qty_1.Value;
            list = wodc.getWoBySome(wo_no, status, target_qty, part_no);
            if (list == null)
            {
                if (part_no == string.Empty && status == string.Empty && target_qty== string.Empty && wo_no == string.Empty)
               {
                   PageUtil.showToast(this, "工单表中无任何数据！");
                }
                else
                    PageUtil.showToast(this, "工单表中无符合条件的数据！");
            }
            else
            {
                workOrder.DataSource = list;
                workOrder.DataBind();
            }
           // Part_No_1.Value = String.Empty;
            //Wo_No_1.Value = String.Empty;
            Status_1.Value = String.Empty;
            Target_Qty_1.Value = String.Empty;
        }

       /// <summary>
       /// 更新数据
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void update(object sender, EventArgs e)
        {
            string part_no = DropDownList3.Items[DropDownList3.SelectedIndex].Value;
            string version = DropDownList4.Items[DropDownList4.SelectedIndex].Value;
            string wo_no = Wo_no_update.Value;
            string status = Status_update.Value;
            int target_qty;
            //if (part_no.Length > 20)
            //{
            //    PageUtil.showToast(this, "输入的料号长度不得超过20个字符！");
            //    return;
            //}
            //if (Status_update.Value == "")
            //{
            //    status = 0;
            //}
            //else
            //{
            //    status = Convert.ToInt32(Status_update.Value);
            //}
            if (Target_qty_update.Value == "")
            {
                target_qty = 0;
            }
            else
            {
                try
                {
                    target_qty = Convert.ToInt32(Target_qty_update.Value);
                }
                catch
                {
                    PageUtil.showToast(this, "工单对应数量输入格式错误！");
                    return;
                }
            }
            if (part_no == "选择大料号")
            {
                PageUtil.showToast(this.Page, "请选择大料号");
                return;
            }
            if (version == "请选择版本号")
            {
                PageUtil.showToast(this.Page, "请选择版本号");
                return;
            }
            if (part_no == string.Empty|| version==string.Empty || Status_update.Value == string.Empty || Target_qty_update.Value == string.Empty || wo_no == string.Empty)
            {
                PageUtil.showToast(this, "更新数据不能为空！");
                return;
            }
            bool flag = new bool();
            flag = wodc.updateWo(wo_no, status, target_qty, part_no,version, DateTime.Now);
            if (flag == true)
            {
                PageUtil.showToast(this, "更新数据成功！");
            }
            else
            {
                PageUtil.showToast(this, "更新数据失败！");
            }
            list = wodc.getWo();
            workOrder.DataSource = list;
            workOrder.DataBind();
            //Part_no_update.Value = String.Empty;
            Status_update.Value = String.Empty;
            Target_qty_update.Value = String.Empty;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void delete(object sender, EventArgs e)
        {
            bool flag = new bool();
            flag = wodc.deleteWo(Wo_no_Delet.Value);
            if (flag == true)
            {
                PageUtil.showToast(this, "删除数据成功！");
            }
            else
            {
                PageUtil.showToast(this, "删除数据失败！");
            }
            list = wodc.getWo();
            workOrder.DataSource = list;
            workOrder.DataBind();
        }

        /// <summary>
        /// 清除查询框中的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CleanAllMeassage(object sender, EventArgs e)
        {
            //Part_No_1.Value = String.Empty;
           // Wo_No_1.Value = String.Empty;
            Status_1.Value = String.Empty;
            Target_Qty_1.Value = String.Empty;
        }

        /// <summary>
        /// 清除插入框中的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CleanInsertMessage(object sender, EventArgs e)
        {
            //Part_no.Value = String.Empty;
            Wo_no.Value = String.Empty;
            Status.Value = String.Empty;
            Target_qty.Value = String.Empty;
        }

        /// <summary>
        /// 清除更新框中的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CleanUpdateMessage(object sender, EventArgs e)
        {
            //Part_no_update.Value = String.Empty;
            Status_update.Value = String.Empty;
            Target_qty_update.Value = String.Empty;
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            BomDC bomdc = new BomDC();
            DataSet ds2 = bomdc.getBomVersion(DropDownList1.Items[DropDownList1.SelectedIndex].Text);
            int count = ds2.Tables[0].Rows.Count;
            if (ds2 != null)
            {
                DropDownList2.DataSource = ds2.Tables[0].DefaultView;
                DropDownList2.DataTextField = "bom_version";
                DropDownList2.DataBind();
            }
            else
                PageUtil.showAlert(this, "区域载入出错！");
            DropDownList2.Items.Insert(0, "选择版本号");
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList4.Items.Clear();
            BomDC bomdc = new BomDC();
            DataSet ds2 = bomdc.getBomVersion(DropDownList3.Items[DropDownList3.SelectedIndex].Text);
            int count = ds2.Tables[0].Rows.Count;
            if (ds2 != null)
            {
                DropDownList4.DataSource = ds2.Tables[0].DefaultView;
                DropDownList4.DataTextField = "bom_version";
                DropDownList4.DataBind();
            }
            else
                PageUtil.showAlert(this, "版本号载入出错！");
            DropDownList4.Items.Insert(0, "选择版本号");
        }
    }
}