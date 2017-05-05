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
    public partial class CheckParameter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "复验参数设定";
        }

        /**
         * 
         * 限定Pn_head长度*
         * 
         ***/
        public void Pn_Leng(string str, string name)
        {
            if (str.Length > 30)
            {
                PageUtil.showToast(this, "" + name + "输入长度过长！");
                return;
            }
        }

        /**
        * 
        * 限定复验周期长度*
        * 
        ***/
        public void Re_Leng(string str, string name)
        {
            if (str.Length > 10)
            {
                PageUtil.showToast(this, "" + name + "输入长度过长！");
                return;
            }
        }



        /**
         * 
         * 查询复验参数表信息*
         * 
         ***/
        protected void Select(object sender, EventArgs e)
        {
            //获取前台数据
            string PN_HEAD = pn_head.Value;
            Pn_Leng(PN_HEAD, "Pn_head");
            string REINSPECT_WEEK = reinspect_week.Value;
            Re_Leng(REINSPECT_WEEK, "复验周期");
            //查询复验参数表数据
            Reinspect_parameterDC reinspect_parameterDC = new Reinspect_parameterDC();
            DataSet ds = new DataSet();
            ds = reinspect_parameterDC.searchReinspect_parameters(PN_HEAD, REINSPECT_WEEK);
            if (ds == null)
            {
                if (PN_HEAD == string.Empty && REINSPECT_WEEK == string.Empty)
                {
                    PageUtil.showToast(this, "复验参数表中无任何数据！");
                }
                else
                    PageUtil.showToast(this, "复验参数表中无符合条件的数据！");
            }
            else
            {
                Line_Repeater.DataSource = ds;
                Line_Repeater.DataBind();
            }
        }

        /**
         * 
         * 新增复验参数表信息*
         *
         ***/
        protected void Insert(object sender, EventArgs e)
        {
            //获取输入的数据
            string PN_HEAD1 = pn_head1.Value;
            Pn_Leng(PN_HEAD1, "Pn_head");
            string REINSPECT_WEEK1 = reinspect_week1.Value;
            Re_Leng(REINSPECT_WEEK1, "复验周期");
            string REINSPECT_QTY1 ="";
           

            //判断输入是否全为空
            if (PN_HEAD1 == string.Empty && REINSPECT_WEEK1 == string.Empty )
            {
                PageUtil.showToast(this, "新增复验参数表数据的输入不能全为空！");
                return;
            }

            //将数据插入数据表
            Reinspect_parameterDC reinspect_parameterDC = new Reinspect_parameterDC();
            DataSet ds = new DataSet();
            ds=reinspect_parameterDC .searchReinspect_parameters (PN_HEAD1,"");
            if (ds != null)
            {
                PageUtil.showToast(this, "该Pn_head已存在！");

            }
            else
            {
                try
                {
                    ds = reinspect_parameterDC.insertReinspect_parameters(PN_HEAD1, REINSPECT_WEEK1, REINSPECT_QTY1);
                    Line_Repeater.DataSource = ds;
                    Line_Repeater.DataBind();
                }
                catch
                {
                    PageUtil.showToast(this, "新增复验参数表信息失败！");
                }
            }
        }

        /**
         * 
         * 修改区域表信息*
         * 
         ***/
        protected void Update(object sender, EventArgs e)
        {
            //获取输入的数据
            string UNIQUE_ID2 = Request.Form["unique_id2"];
            string PN_HEAD2 = Request.Form["pn_head2"];
            Pn_Leng(PN_HEAD2, "Pn_head");
            string REINSPECT_WEEK2 = Request.Form["reinspect_week2"];
            Re_Leng(REINSPECT_WEEK2, "复验周期");
            string REINSPECT_QTY2 = "";
           

            //修改数据
            Reinspect_parameterDC reinspect_parameterDC = new Reinspect_parameterDC();
            DataSet ds = new DataSet();
            try
            {
                ds = reinspect_parameterDC.updateReinspect_parameters(UNIQUE_ID2, PN_HEAD2, REINSPECT_WEEK2, REINSPECT_QTY2);
                Line_Repeater.DataSource = ds;
                Line_Repeater.DataBind();
                PageUtil.showToast(this, "修改复验参数表信息成功！");
            }
            catch
            {
                PageUtil.showToast(this, "修改复验参数表信息失败！");
            }
        }

        /**
         * 
         * 删除区域表信息*
         * 
         * **/
        protected void Delete(object sender, EventArgs e)
        {

            //取主键（Unique_id）的值
            string UNIQUE_ID3 = Request.Form["unique_id3"];
            //删除复验参数数据
            Reinspect_parameterDC reinspect_parameterDC = new Reinspect_parameterDC();
            DataSet ds = new DataSet();
            try
            {
                ds = reinspect_parameterDC.deleteReinspect_parameters(UNIQUE_ID3);
                Line_Repeater.DataSource = ds;
                Line_Repeater.DataBind();
            }
            catch
            {
                PageUtil.showToast(this, "删除复验参数表信息失败！");
            }
        }

        /**
         * 
         * 清除查询区域输入的数据*
         * 
         * **/
        protected void Clear(object sender, EventArgs e)
        {
            pn_head.Value = String.Empty;
            reinspect_week.Value = String.Empty;

        }
    }
}