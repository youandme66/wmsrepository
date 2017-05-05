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

namespace WMS_v1._0.Web
{
    public partial class rackSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "料架设定";
            if (!IsPostBack)
            {
                SubinventoryDC subdc = new SubinventoryDC();
                List<string> list = subdc.getAllSubinventory();
                DropDownList1.DataSource = list;
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "ALL");
                RegionDC dc = new RegionDC();
                DataSet ds = dc.getEnableRegion();
                if (ds != null)
                {
                    DropDownList2.DataSource = ds.Tables[0].DefaultView;
                    DropDownList2.DataValueField = "region_name";
                    DropDownList2.DataBind();
                    Region_key.DataSource = ds.Tables[0].DefaultView;
                    Region_key.DataValueField = "region_name";
                    Region_key.DataBind();
                    Region_key2.DataSource = ds.Tables[0].DefaultView;
                    Region_key2.DataValueField = "region_name";
                    Region_key2.DataBind();
                }
                DropDownList2.Items.Insert(0, "ALL");
                Region_key.Items.Insert(0, "--------select--------");
            }
        }

        //清空搜索框数据
        protected void Clear(object sender, EventArgs e)
        {
            Frame_name1.Value = "";
            Enabled1.Value = "";
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            Region_key.SelectedIndex = 0;
            Region_key2.SelectedIndex = 0;
            Create_by1.Value = "";
            Update_by1.Value = "";
            Enabled1.Value = "";
        }

        //添加料架
        protected void Insert(object sender, EventArgs e)
        {
            List<ModelFrame> list = new List<ModelFrame>();
            FrameDC frameDc = new FrameDC();

            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "请登录用户！");
                ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>setTimeout(function(){window.location.href='Login.aspx'},1000)</script>");
                return;
            }
            string create_by = Session["LoginName"].ToString();
            string frame_name = Frame_name.Value;
            if (string.IsNullOrWhiteSpace(frame_name))
            {
                PageUtil.showToast(this, "料架名不允许为空！");
                return;
            }
            if (frame_name.Length > 100)
            {
                PageUtil.showToast(this, "料架名输入长度超出范围！");
                return;
            }
            DataSet ds = frameDc.searchRegionByFourParameters("", frame_name, "", "", "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                PageUtil.showToast(this, "该料架名已存在！");
                return;
            }
            string enabled = Enabled.Value;
            string region_key = Region_key.SelectedValue;
            if (region_key == "--------select--------")
            {
                PageUtil.showToast(this, "请选择区域！");
                return;
            }

            string description = Description.Value;
            if (description.Length > 50)
            {
                PageUtil.showToast(this, "描述输入长度超出范围！");
                Description.Value = "";
                return;
            }

            if (frameDc.insertFrame(frame_name, enabled, description, create_by, region_key))
            {
                Select(sender, e);
                PageUtil.showToast(this, "成功插入料架信息！");
                Frame_name.Value = null;
                Enabled.Value = null;
                Region_key.SelectedIndex = 0;
                Description.Value = null;

            }
            else PageUtil.showToast(this, "添加料架信息失败！");

        }

        //查询
        protected void Select(object sender, EventArgs e)
        {
            string frame_name = Frame_name1.Value.Trim();
            string enabled = Enabled1.Value.Trim();
            if (enabled.Equals("ALL"))
            {
                enabled = "";
            }
            string region_name = DropDownList2.SelectedValue;
            if (region_name == "ALL")
            {
                region_name = "";
            }
            string create_by = Create_by1.Value;
            string update_by = Update_by1.Value;
            FrameDC frameDc = new FrameDC();
            try
            {
                DataSet ds = frameDc.searchRegionByFourParameters(create_by, frame_name, update_by, enabled, region_name);

                Line_Repeater.DataSource = ds;
                Line_Repeater.DataBind();
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "无相关数据！");
                return;
            }
        }


        //更新料架信息
        protected void Update(object sender, EventArgs e)
        {
            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "请登录用户！");
                ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>setTimeout(function(){window.location.href='Login.aspx'},1000)</script>");
                return;
            }
            List<ModelFrame> list = new List<ModelFrame>();
            FrameDC frameDc = new FrameDC();

            string frame_key = Frame_key2.Value;
            string frame_name = Frame_name2.Value;
            if (string.IsNullOrWhiteSpace(frame_name))
            {
                PageUtil.showToast(this, "料架名不允许为空！");
                return;
            }
            if (frame_name.Length > 100)
            {
                PageUtil.showToast(this, "料架名输入长度超出范围！");
                return;
            }
            string enabled = Enabled2.Value.Trim();
            string region_name = Region_key2.SelectedValue;
            string update_by = Session["LoginName"].ToString();

            string description = Description2.Value;
            if (description.Length > 50)
            {
                PageUtil.showToast(this, "描述输入长度超出范围！");
                return;
            }
            DataSet ds = frameDc.searchRegionByFourParameters("", frame_name, "", "", "");
            if (ds != null && ds.Tables[0].Rows.Count > 0 && !ds.Tables[0].Rows[0]["frame_key"].ToString().Equals(frame_key))
            {
                PageUtil.showToast(this, "更新失败，料架已存在！");
                return;
            }

            if (frameDc.updateSubinventory(frame_key, frame_name, update_by, enabled, description, region_name))
            {
                Select(sender, e);
                PageUtil.showToast(this, "成功更新料架信息！");
            }
            else
                PageUtil.showToast(this, "更新料架信息失败！");
        }

        //删除料架信息
        protected void Delete(object sender, EventArgs e)
        {
            string id = lab.Value;
            FrameDC frameDc = new FrameDC();
            if (frameDc.deleteFrameById(id))
            {
                Select(sender, e);
                PageUtil.showToast(this, "删除料架信息成功！");
            }
            else
                PageUtil.showToast(this, "删除料架信息失败！");

        }

        //联动
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            RegionDC dc = new RegionDC();
            string sub_name = DropDownList1.SelectedValue;
            DataSet ds = null;
            if (sub_name.Equals("ALL"))
            {
                ds = dc.getEnableRegion();
            }
            else
                ds = dc.getRegion_nameBySub_name(DropDownList1.SelectedValue);
            if (ds != null)
            {
                DropDownList2.DataSource = ds.Tables[0].DefaultView;
                DropDownList2.DataValueField = "region_name";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, "ALL");
            }
            else
                PageUtil.showToast(this, "数据异常！");
        }
    }
}