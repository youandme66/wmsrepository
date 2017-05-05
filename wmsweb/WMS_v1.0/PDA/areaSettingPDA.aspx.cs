using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;
using WMS_v1._0.Util;

namespace WMS_v1._0.PDA
{
    public partial class areaSettingPDA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "区域设定";
        }

        /**
        * 
        * 判断是否为空，不为空时，是否为数字
        *
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
         * 限定区域名长度*
         * 
         ***/
        public void Re_Leng(string str, string name)
        {
            if (str.Length > 100)
            {
                PageUtil.showToast(this, "" + name + "输入长度过长！");
                return;
            }
        }

        /**
        * 
        * 限定描述长度*
        * 
        ***/
        public void Des_Leng(string str, string name)
        {
            if (str.Length > 50)
            {
                PageUtil.showToast(this, "" + name + "输入长度过长！");
                return;
            }
        }

        /**
         * 
         * 查询区域表信息*
         * 
         ***/
        protected void Select(object sender, EventArgs e)
        {
            //获取前台数据
            string REGION_NAME = region_name.Value;
            Re_Leng(REGION_NAME, "区域名");
            string SUBINVENTORY_KEY = Request.Form["subinventory_name"];
            if (SUBINVENTORY_KEY == "ALL")
            {
                SUBINVENTORY_KEY = "";
            }
            string CREATE_BY = create_by.Value;
            string ENABLED = enabled.Value;
            if (ENABLED == "ALL")
            {
                ENABLED = "";
            }

            //查询区域表数据
            RegionDC regionDC = new RegionDC();
            DataSet ds = new DataSet();
            ds = regionDC.searchRegionByFourParameters(REGION_NAME, SUBINVENTORY_KEY, CREATE_BY, ENABLED);
            if (ds == null)
            {
                if (REGION_NAME == string.Empty && SUBINVENTORY_KEY == string.Empty && CREATE_BY == string.Empty && ENABLED == string.Empty)
                {
                    PageUtil.showToast(this, "区域表中无任何数据！");
                }
                else
                {
                    PageUtil.showToast(this, "区域表中无符合条件的数据！");
                }
            }
            else
            {
                Line_Repeater.DataSource = ds;
                Line_Repeater.DataBind();
            }
        }

        /**
         * 
         * 新增区域表信息*
         *
         ***/
        protected void Insert(object sender, EventArgs e)
        {
            //获取输入的数据
            string REGION_NAME1 = region_name1.Value;
            Re_Leng(REGION_NAME1, "区域名");
            string SUBINVENTORY_KEY1 = Request.Form["subinventory_name1"];
            string ENABLED1 = enabled1.Value;
            string DESCRIPTION1 = description1.Value;
            Des_Leng(DESCRIPTION1, "描述");
            string CREATE_BY1 = Session["LoginName"].ToString();

            //选择库别
            if (SUBINVENTORY_KEY1 == "-----请选择库别-----")
            {
                PageUtil.showToast(this, "请选择库别！");
                return;
            }

            //判断区域名是否全为空
            if (REGION_NAME1 == string.Empty)
            {
                PageUtil.showToast(this, "区域名不能为空！");
                return;
            }

            //判断创建者是否为空
            if (Session["LoginId"] == null)
            {
                PageUtil.showToast(this, "创建者为空！");
                return;
            }




            //将数据插入数据表
            RegionDC regionDC1 = new RegionDC();
            DataSet ds = new DataSet();
            ds = regionDC1.searchRegionByFourParameters(REGION_NAME1, "", "", "");
            if (ds != null)
            {
                PageUtil.showToast(this, "该区域已存在！");
            }
            else
            {
                try
                {
                    ds = regionDC1.insertRegion(REGION_NAME1, SUBINVENTORY_KEY1, CREATE_BY1, ENABLED1, DESCRIPTION1);
                    Line_Repeater.DataSource = ds;
                    Line_Repeater.DataBind();
                }
                catch
                {
                    PageUtil.showAlert(this, "新增区域表信息失败！");
                }
            }
            region_name1.Value = String.Empty;
            description1.Value = String.Empty;
        }

        /**
         * 
         * 修改区域表信息*
         * 
         ***/
        protected void Update(object sender, EventArgs e)
        {
            //获取输入的数据
            string REGION_KEY = Request.Form["region_key2"];
            string REGION_NAME2 = Request.Form["region_name2"];
            Re_Leng(REGION_NAME2, "区域名");
            string SUBINVENTORY_KEY2 = Request.Form["subinventory_name2"];
            string ENABLED2 = Request.Form["enabled2"];
            string DESCRIPTION2 = Request.Form["description2"];
            Des_Leng(DESCRIPTION2, "描述");
            string UPDATE_BY2 = Session["LoginName"].ToString();

            //判断更改者是否为空
            if (Session["LoginId"] == null)
            {
                PageUtil.showToast(this, "更改者为空！");
                return;
            }

            //修改数据
            RegionDC regionDC2 = new RegionDC();
            DataSet ds = new DataSet();
            try
            {
                ds = regionDC2.updateRegion(REGION_KEY, REGION_NAME2, SUBINVENTORY_KEY2, UPDATE_BY2, ENABLED2, DESCRIPTION2);
                Line_Repeater.DataSource = ds;
                Line_Repeater.DataBind();
                PageUtil.showToast(this, "修改区域表信息成功！");
            }
            catch
            {
                PageUtil.showToast(this, "修改区域表信息失败！");
            }

        }

        /**
         * 
         * 删除区域表信息*
         * 
         * **/
        protected void Delete(object sender, EventArgs e)
        {

            //取主键（区域）的值
            string REGION_KEY1 = Request.Form["region_key3"];
            //删除区域数据
            RegionDC regionDC3 = new RegionDC();
            DataSet ds = new DataSet();
            try
            {
                ds = regionDC3.deleteRegionById(REGION_KEY1);
                Line_Repeater.DataSource = ds;
                Line_Repeater.DataBind();
                PageUtil.showToast(this, "删除区域表信息成功！");
            }
            catch
            {
                PageUtil.showAlert(this, "删除区域表信息失败！");
            }
        }

        /**
         * 
         * 清除查询区域输入的数据*
         * 
         * **/
        protected void Clear(object sender, EventArgs e)
        {
            region_name.Value = String.Empty;
            create_by.Value = String.Empty;

        }
    }
}