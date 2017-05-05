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
    public partial class PnMaterialsIssuePDA : System.Web.UI.Page
    {
        PnDC pn = new PnDC();
        List<ModelPn> Modelpn_list;

        protected void Page_Load(object sender, EventArgs e)
        {

            Session["Local"] = "料号设定";
        }



        //判断料号长度是否超出范围
        protected bool StrLength_name(string str)
        {
            if (str.Length > 40)
            {
                string temp = "料号长度不能超过40！";
                PageUtil.showToast(this, temp);
                return false;
            }
            else
                return true;


        }


        //判断料号描述长度是否超出范围
        protected bool StrLength_desc(string str)
        {
            if (str.Length > 200)
            {
                string temp = "料号描述长度不能超过200！";
                PageUtil.showToast(this, temp);
                return false;
            }
            else
                return true;

        }


        protected bool StrLength_uom(string str)
        {
            if (str.Length > 6)
            {
                string temp = "单位长度不能超过6！";
                PageUtil.showToast(this, temp);
                return false;
            }
            else
                return true;

        }



        //插入操作对应的按钮
        protected void Insert_Click_pn(object sender, EventArgs e)
        {
            PnDC pn = new PnDC();
            List<ModelPn> Modelpn_list;
            string item_name = ItemName_insert_pn.Value.Trim();
            string item_desc = ItemDesc_insert_pn.Value;
            string uom = Uom_insert_pn.Value;
            DateTime pn_inserttime = new DateTime();
            pn_inserttime = DateTime.Now;
            if (!StrLength_name(item_name))   //调用StrLength_name（）判断料号长度是否超出范围
                return;
            if (!StrLength_desc(item_desc))   //调用StrLength_desc（）判断料号描述长度是否超出范围
                return;
            if (!StrLength_uom(uom))     //调用StrLength_uom（）判断料号描述长度是否超出范围
                return;
            Boolean flag;
            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "登录Id为空");
                return;
            }
            string id = Session["LoginName"].ToString();
            if (String.IsNullOrEmpty(item_name))     //料号不能为空
            {
                string temp = "料号不能为空！";
                PageUtil.showToast(this, temp);
            }
            else
            {
                Modelpn_list = pn.getPnByITEM_NAME(item_name);     //查询后判断是否已存在该料号
                if (Modelpn_list == null)
                {
                    flag = pn.insertPn(item_name, item_desc, uom, pn_inserttime, id);//调用DataCenter中PnDC.cs里面的insertPn()方法
                    if (flag == true)
                    {
                        string temp = "数据插入成功！";
                        PageUtil.showToast(this, temp);
                        Modelpn_list = pn.getPnBySome(item_name, item_desc, uom);
                        Pn_Repeater.DataSource = Modelpn_list;
                        Pn_Repeater.DataBind();

                    }
                    else
                    {
                        string temp = "数据插入失败！";
                        PageUtil.showToast(this, temp);
                    }
                }
                else
                {
                    string temp = "料号已存在,不能重复！";
                    PageUtil.showToast(this, temp);
                    return;
                }
            }
            ItemName_insert_pn.Value = String.Empty;  //清除插入部分的输入框内容
            ItemDesc_insert_pn.Value = String.Empty;
            Uom_insert_pn.Value = String.Empty;
        }


        //删除按钮对应操作
        protected void Delect_Click_Pn(object sender, EventArgs e)
        {

            Boolean flag;
            string item_name = ItemName_delete_pn.Value;
            flag = pn.deletePn(item_name);             //调用DataCenter中PnDC.cs里面的deletePn()方法
            if (flag == true)
            {
                string temp = "该条数据删除成功，其他数据请查询！";
                PageUtil.showToast(this, temp);
                Pn_Repeater.DataSource = Modelpn_list;
                Pn_Repeater.DataBind();

            }
            else
            {
                string temp = "数据删除失败！";
                PageUtil.showToast(this, temp);
            }

        }


        //更新按钮对应的操作
        protected void Update_Click_Pn(object sender, EventArgs e)
        {
            PnDC pn = new PnDC();
            List<ModelPn> ModelPn_list;
            string item_name = ItemName_update_pn.Value.Trim();
            string item_desc = ItemDesc_update_pn.Value;
            string uom = Uom_update_pn.Value;
            DateTime pn_updatetime = new DateTime();
            pn_updatetime = DateTime.Now;
            if (!StrLength_desc(item_desc))   //调用StrLength_desc（）判断料号描述长度是否超出范围
                return;
            if (!StrLength_uom(uom))     //调用StrLength_uom（）判断料号描述长度是否超出范围
                return;
            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "登录Id为空");
                return;
            }
            string id = Session["LoginName"].ToString();
            if (String.IsNullOrEmpty(item_name))
            {
                string temp = "料号不能为空！";
                PageUtil.showToast(this, temp);
            }

            Boolean flag;
            flag = pn.updatePn(item_name, item_desc, uom, pn_updatetime, id);  //调用DataCenter中PnDC.cs里面的updatePn()方法
            if (flag == true)
            {
                string temp = "数据更新成功！";
                PageUtil.showToast(this, temp);
                ModelPn_list = pn.getPnBySome(item_name, item_desc, uom);
                Pn_Repeater.DataSource = ModelPn_list;
                Pn_Repeater.DataBind();

            }
            else
            {
                string temp = "数据更新失败！";
                PageUtil.showToast(this, temp);
            }

        }



        //查询按钮对应的操作
        protected void Select_Click_Pn(object sender, EventArgs e)
        {
            PnDC pn = new PnDC();
            string item_name = ItemName_pn.Value.Trim();
            string item_desc = ItemDesc_pn.Value;
            string uom = UOM_pn.Value;
            List<ModelPn> ModelPn_list;
            ModelPn_list = pn.getPnBySome(item_name, item_desc, uom); //调用DataCenter中PnDC.cs里面的getPnBySome()方法
            if (ModelPn_list == null)
            {
                string temp = "数据库没有该数据，查询失败！";
                PageUtil.showToast(this, temp);
            }
            else
            {
                Pn_Repeater.DataSource = ModelPn_list;
                Pn_Repeater.DataBind();
                string temp = "查询成功！";
                PageUtil.showToast(this, temp);
            }
        }


        //Clear按钮对应操作
        protected void Clean_input_Click(object sender, EventArgs e)
        {
            ItemName_pn.Value = String.Empty;        //清除查询部分的输入框内容
            ItemDesc_pn.Value = String.Empty;
            UOM_pn.Value = String.Empty;
        }


        //添加框内部取消按钮对应操作
        protected void Cancle_insert_pn(object sender, EventArgs e)
        {
            ItemName_insert_pn.Value = String.Empty;  //清除插入部分的输入框内容
            ItemDesc_insert_pn.Value = String.Empty;
            Uom_insert_pn.Value = String.Empty;
        }

        //更新框内部取消按钮对应操作
        protected void Cancle_update_pn(object sender, EventArgs e)
        {
            ItemName_insert_pn.Value = String.Empty;
            ItemDesc_insert_pn.Value = String.Empty;
            Uom_insert_pn.Value = String.Empty;
        }

    }
}