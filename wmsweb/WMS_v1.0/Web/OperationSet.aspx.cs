using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.Web
{
    public partial class OperationSet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "制程设定";
        }

        //清空搜索框信息
        protected void Clear(object sender, EventArgs e)
        {
            Route_id.Value = null;
            Create_by_id.Value = null;
            Update_by_id.Value = null;
        }
        //插入数据
        protected void Insert(object sender, EventArgs e)
        {
            string ROUTE_ID1 = Route_id1.Value;
            Route_lg(ROUTE_ID1, "制程代号");
            string DESCRIPTION_ID1 = Description_id1.Value;
            Desc_lg(DESCRIPTION_ID1, "描述");
            string CREATE_BY1 = Session["LoginName"].ToString();
            
            Wip_operationDC wip_operationDC = new Wip_operationDC();
            DataSet ds = new DataSet();
            ds = wip_operationDC.searchWip_operation(ROUTE_ID1,"", "");
            if (ds != null)
            {
                PageUtil.showAlert(this, "该制程代号已存在");
                Route_id1.Value = null;
                Description_id1.Value = null;
                return;
            }
            else
            {
                try
                {
                    ds = wip_operationDC.insertWip_operation(ROUTE_ID1, DESCRIPTION_ID1, CREATE_BY1);
                    Line_Repeater.DataSource = ds;
                    Line_Repeater.DataBind();
                    PageUtil.showToast(this, "插入数据成功");
                    Route_id1.Value = null;
                    Description_id1.Value = null;
                }
                catch
                {
                    PageUtil.showAlert(this, "插入数据失败！");

                }
            }
        }

        //取消时清除插入的数据信息
        protected void Inclear(object sender, EventArgs e)
        {
            Route_id1.Value = null;
            Description_id1.Value = null;
        }
           
        //查询制程表信息
        protected void Select(object sender, EventArgs e)
        {

            string ROUTE_ID = Route_id.Value;
            string CREATE_BY_ID = Create_by_id.Value;
            Null_Num(CREATE_BY_ID,"创建者");
            string UPDATE_BY_ID = Update_by_id.Value;
            Null_Num(UPDATE_BY_ID,"更新者");
            Wip_operationDC wip_operationDC1 = new Wip_operationDC();
            DataSet ds1 = new DataSet();

            ds1 = wip_operationDC1.searchWip_operation(ROUTE_ID, CREATE_BY_ID, UPDATE_BY_ID);
            if (ds1 == null)
            {
                if (ROUTE_ID == string.Empty && CREATE_BY_ID == string.Empty && UPDATE_BY_ID == string.Empty)
                {
                    PageUtil.showToast(this, "区域表中无任何数据！");
                }
                else
                    PageUtil.showToast(this, "区域表中无符合条件的数据！");
            }
            else
            {
                Line_Repeater.DataSource = ds1;
                Line_Repeater.DataBind();
            }
            

        }
        //更新制程表信息
        protected void Update(object sender, EventArgs e)
        {
            string ROUTE2 = Route2.Value;
            string ROUTE_ID2 = Route_id2.Value;
            Route_lg(ROUTE_ID2, "制程代号");
            string DESCRIPTION_ID2 = Description_id2.Value;
            Desc_lg(DESCRIPTION_ID2, "描述");
            string UPDATE_BY2 = Session["LoginName"].ToString();
            
            Wip_operationDC wip_operationDC2 = new Wip_operationDC();
            DataSet ds2 = new DataSet();
            ds2 = wip_operationDC2.searchWip_operation(ROUTE_ID2,"","");
            try
            {
                ds2 = wip_operationDC2.updateWip_operation(ROUTE2, ROUTE_ID2, DESCRIPTION_ID2, UPDATE_BY2);
                Line_Repeater.DataSource = ds2;
                Line_Repeater.DataBind();
                PageUtil.showToast(this, "更新成功");
            }
            catch
            {
                PageUtil.showAlert(this, "更新数据失败");
            }
        }
        //删除删除一条制程数据
        protected void Delete(object sender, EventArgs e)
        {
            string ROUTE_ID3 = Route_id3.Value;
            Wip_operationDC wip_operationDC3 = new Wip_operationDC();
            DataSet ds3 = new DataSet();
            try
            {
                ds3 = wip_operationDC3.deleteWip_operation(ROUTE_ID3);
                Line_Repeater.DataSource = ds3;
                Line_Repeater.DataBind();
                PageUtil.showToast(this, "删除成功");
            }
            catch
            {
                PageUtil.showAlert(this, "删除失败");
            }
        }

        /**
         * 判断是否为数字
         */
        public void Null_Num(string text, string name)
        {
            if (!(Regex.IsMatch(text, @"\d+") || text.Length == 0))
            {
                PageUtil.showToast(this, "请在" + name + "中输入数字！");
                return;
            }
        }
        /**
        * 
        * 限定制程代号长度*
        * 
        ***/
        public void Route_lg(string str, string name)
        {
            if (str.Length > 6)
            {
                PageUtil.showToast(this, "" + name + "输入长度过长！");
                return;
            }
        }

        /**
        * 
        * 限定库描述长度*
        * 
        ***/
        public void Desc_lg(string str, string name)
        {
            if (str.Length > 30)
            {
                PageUtil.showToast(this, "" + name + "输入长度过长！");
                return;
            }
        }
       
    }
}