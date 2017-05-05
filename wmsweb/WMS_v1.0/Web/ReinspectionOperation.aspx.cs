using System;
using System.Data;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;

namespace WMS_v1._0.Web
{
    public partial class ReinspectionOperation : System.Web.UI.Page
    {
        ReinspectHeaderDC reinspect = new ReinspectHeaderDC();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "复验作业";
        }

        //判断料号长度是否超出范围
        protected bool StrItem_name(string str)
        {
            if (str.Length > 30)
            {
                string temp = "料号长度不能超过30！";
                PageUtil.showToast(this, temp);
                return false;
            }
            else
                return true;
        }

        //判断描述长度是否超出范围
        protected bool StrRemark(string str)
        {
            if (str.Length > 30)
            {
                string temp = "描述长度不能超过30！";
                PageUtil.showToast(this, temp);
                return false;
            }
            else
                return true;
        }

        //判断datecode长度是否超出范围
        protected bool StrDatecode(string str)
        {
            if (str.Length > 30)
            {
                string temp = "datecode长度不能超过30！";
                PageUtil.showToast(this, temp);
                return false;
            }
            else
                return true;
        }

        //提交按钮
        protected void Commite_reinspect(object sender, EventArgs e)
        {
            string item_name = select_name_reinspect.Value.Trim();
            string datecode = DateCode_reinspect.Value.Trim();
            string frame_name = Request.Form["frame_name_reinspect"]; //frame_name_reinspect.Value;
            string status = reinspect_select.Value;
            string remark = remark_reinspect.Value.Trim();
            //判断输入框输入数据是否超出范围
            StrItem_name(item_name);
            StrRemark(remark);
            StrDatecode(datecode);

            bool flag;
            string temp_flag;
            string check_user = "";
            try
            {
                check_user = Session["LoginName"].ToString();
            }
            catch (Exception ex)
            {
                PageUtil.showToast(this, "请重新登录！");
                return;
            }

            Reinspect_parameterDC reinspectdc = new Reinspect_parameterDC();
            if (!reinspectdc.checkItem_name(item_name))
            {
                PageUtil.showToast(this, "该料号未设置复验周期，无法复验！");
                return;
            }


            DateTime check_time = new DateTime();
            check_time = DateTime.Now;
            int item_id, frame_key;
            DataSet list;
            PnDC pn = new PnDC();
            PoDC DC = new PoDC();
            try
            {
                item_id = DC.getItem_idByItem_name(item_name);
                frame_key = DC.getFrame_keyByFrame_name(frame_name);
            }
            catch
            {
                PageUtil.showToast(this, "输入格式错误！");
                return;
            }

            //判断库存明细表中是否存在该料号、库别和料架
            DataSet table = DC.getItems_onhand_qty_detailByITEM_NAMEandSubinventoryandFrame_key(item_name, datecode, frame_key);
            if (table != null && table.Tables[0].Rows.Count > 0)
            {
                temp_flag = reinspect.getLineNumByPoHeaderId(item_id, datecode, frame_key)[0].ToString();
                //如果复验状态是PASS,则需要复验              
                if ((temp_flag.Equals("PENDING")))
                {
                    int unique_id = reinspect.getSome(item_name, datecode, frame_name, status);
                    //判断是否存在
                    if (unique_id == -1)
                    {
                        //不存在，新增一条数据
                        flag = reinspect.insertReinspectHeader(item_name, datecode, frame_name, status, remark, check_user, check_time);
                        PageUtil.showToast(this, "该料号未复验过，复验成功！");
                    }
                    else
                    {
                        //已存在，更新数据
                        flag = reinspect.updateReinspectHeader(unique_id, status, remark, check_user, check_time);
                        PageUtil.showToast(this, "该料号已复验过，更新成功！");
                    }
                    if (flag)
                    {
                        Material_ioDC iodc = new Material_ioDC();
                        if (iodc.updateStatus(item_name, datecode, frame_name, status, check_time))
                        {
                            pending(sender, e);
                            PageUtil.showToast(this, "库存信息更新成功！");
                        }
                        else
                        {
                            PageUtil.showToast(this, "库存信息更新失败！");
                        }
                    }
                    else
                        PageUtil.showToast(this, "操作失败！");
                }
                else
                    PageUtil.showToast(this, "该料不需要复验！");
            }
            else
            {
                PageUtil.showToast(this, "没有该条数据！");
                return;
            }
            CleanAllMeassage_Click(sender, e);
        }

        //清除按钮操作------清除Repeater中数据
        protected void CleanAllMeassage_Click(object sender, EventArgs e)
        {

            select_name_reinspect.Value = String.Empty;
            DateCode_reinspect.Value = String.Empty;
            //frame_name_reinspect.Value = String.Empty;
            reinspect_select.Value = String.Empty;
            remark_reinspect.Value = String.Empty;
        }

        //查询
        protected void timeCaculator(object sender, EventArgs e)
        {
            //当前的时间转化为datecode格式
            int curdate = DateTime.Now.Year * 100 + (int)Math.Ceiling(DateTime.Now.Day / 7.0);
            //reinspect.updateReinspectHeader(curdate);//更新状态，一周内过期的标记为PENDING
            DataSet ds = reinspect.getTime(curdate); //查询所有状态为PENDING的料
            if (ds.Tables[0].Rows.Count > 0)
            {
                Reinspect_Repeater.DataSource = ds;
                Reinspect_Repeater.DataBind();
            }
            else
            {
                CleanAllMeassage_Click(sender, e);
                PageUtil.showToast(this, "未查询到数据！");
            }
        }

        //已复验
        protected void pass(object sender, EventArgs e)
        {
            Label1.InnerText = "";
            Reinspect_Repeater2.DataBind();
            DataSet ds = reinspect.getallpass();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                table_type.InnerText = "已复验";
                Reinspect_Repeater.DataSource = ds;
                Reinspect_Repeater.DataBind();
            }
            else
            {
                table_type.InnerText = "";
                Reinspect_Repeater.DataBind();
                CleanAllMeassage_Click(sender, e);
                PageUtil.showToast(this, "无已复验数据！");
            }
        }

        //待复验
        protected void pending(object sender, EventArgs e)
        {
            table_type.InnerText = "";
            Reinspect_Repeater.DataBind();
            int curdate = DateTime.Now.Year * 100 + (int)Math.Ceiling(DateTime.Now.Day / 7.0);
            DataSet ds = reinspect.getallpending(curdate);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Label1.InnerText = "待复验";
                Reinspect_Repeater2.DataSource = ds;
                Reinspect_Repeater2.DataBind();
            }
            else
            {
                Label1.InnerText = "";
                Reinspect_Repeater2.DataBind();
                CleanAllMeassage_Click(sender, e);
                PageUtil.showToast(this, "无待复验数据！");
            }
        }
    }
}