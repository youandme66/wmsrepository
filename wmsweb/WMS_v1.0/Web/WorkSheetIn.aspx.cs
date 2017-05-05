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
using WMS_v1._0.Model;

namespace WMS_v1._0.Web
{
    public partial class WorkSheetIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "工单入库";
            if (!IsPostBack)
            { 
            BindDrop();
            }
        }

        //打印按钮中的确定操作----将文本框中的值传给打印界面
        protected void transPrint(object sender, EventArgs e)
        {
            //判断搜索框中的工单号是否存在
            if (string.IsNullOrEmpty(select_text_print.Value) == true || string.IsNullOrEmpty(select_text_print2.Value) == true)
            {
                Session["select_text_print"] = null;
                Session["select_text_print2"] = null;
                PageUtil.showToast(this, "请输入需要入库的工单号和入庫數量，再执行此打印操作");
            }
            else
            {
                Session["select_text_print"] = select_text_print.Value;
                Session["select_text_print2"] = select_text_print2.Value;
                //打开打印界面
                Response.Write("<script>window.open('printStorageList.aspx', 'newwindow')</script>");
            }
        }

        //打印按钮中的取消操作------清除输入框数据
        protected void CleanInsertMessage(object sender, EventArgs e)
        {
            select_text_print.Value = String.Empty;
        }



        //工单入库
        protected void workSheetIn(object sender, EventArgs e)
        {
            string WO_no = Request.Form["wo_no"];
            string Number = number.Value;
            string subin = DropDownList1.Items[DropDownList1.SelectedIndex].Text;
            string frame_key = DropDownList2.Items[DropDownList2.SelectedIndex].Value;
            string create_user;
            string item_name=Request.Form["item_name"];
            if (Session["LoginName"] == null)
            {
                PageUtil.showToast(this, "请登录！");
                return;
            }
            else
                create_user = Session["LoginName"].ToString();

            //判断输入是否为空
            if (String.IsNullOrWhiteSpace(WO_no) || String.IsNullOrWhiteSpace(Number)||String.IsNullOrWhiteSpace(subin)||String.IsNullOrWhiteSpace(frame_key))
            {
                PageUtil.showToast(this, "入库数据不能为空！");
                return;
            }
            if (subin == "--选择库别--")
            {
                PageUtil.showToast(this.Page, "请选择库别");
                return;
            }
            if (frame_key == "--选择料架--")
            {
                PageUtil.showToast(this.Page, "请选择料架");
                return;
            }
            int num;
            int Frame_key;
            try
            {
                num = int.Parse(Number);
                Frame_key = int.Parse(frame_key);
            }
            catch (Exception e3)
            {
                PageUtil.showToast(this.Page, "请输入数字");
                return;
            }
            //判断输入是否为数字
            if (!IsNumber(Number))
            {
                PageUtil.showToast(this, "数量只能为数字！");
                return;
            }
            WoDC dc1 = new WoDC();
            int leftNum = dc1.getLeftNum(WO_no);
            if(leftNum==-1){
                PageUtil.showToast(this.Page,"您输入的值有误，请检查后输入");
                return;
            }
            else if (num > leftNum)
            {
                PageUtil.showToast(this.Page, "数量超出，请核对后输入");
                return;
            }
            TimeSpan ts = DateTime.Now - Convert.ToDateTime(DateTime.Now.ToString("yyyy") + "-01-01");
            int day = int.Parse(ts.TotalDays.ToString("F0"));
            int oneDay = (day % 7) > 0 ? 1 : 0;//如果余数大于0 ，说明已经过了半周
            string datecode=DateTime.Now.ToString("yyyy")+((day / 7) + oneDay).ToString("F0");
            SubinventoryDC dc2 = new SubinventoryDC();
            List<ModelSubinventory> ds2 = dc2.getSubinventoryBySubinventory_name(subin);
            if (ds2 == null)
            {
                PageUtil.showToast(this.Page, "请输入存在的库别");
                return;
            }
            //PnDC dc2 = new PnDC();
            //int item_id = dc2.getItem_idByItem_name(item_name);
            //if (item_id == -1)
            //{
            //    PageUtil.showToast(this.Page, "料号有误");
            //    return;
            //}
            WorkSheetInDC dc = new WorkSheetInDC();
            DataSet ds = dc.workSheetIn(WO_no, num, create_user,item_name,subin,Frame_key,datecode);

            if (ds == null)
            {
                PageUtil.showToast(this, "入库失败，请检查输入数据");
                GridView1.DataSource = ds;
                GridView1.DataBind();

                //GridView2.DataSource = ds;
                //GridView2.DataBind();
                return;
            }
            try
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();

                //GridView2.DataSource = ds;
                //GridView2.DataBind();
                PageUtil.showToast(this, "入库成功！");
            }
            catch
            {
                PageUtil.showToast(this, "入库失败！");
            }
            
        }

        //判断是否为数字
        protected bool IsNumber(string str)
        {
            if (Regex.IsMatch(str, @"^[0-9]*$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //通过工单模糊搜索
        protected void search(object sender, EventArgs e)
        {
            string WO_no = Request.Form["wo_no"];

            WorkSheetInDC dc = new WorkSheetInDC();
            DataSet ds = dc.getItemOnhandQty(WO_no);

            if (ds != null)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();

                //GridView2.DataSource = ds;
                //GridView2.DataBind();
                PageUtil.showToast(this, "查询成功！");
            }
        }
        //清除文本框的值
        protected void cleanup(object sender, EventArgs e) 
        {
            Request.Form["number"] = "";
        }
        private void BindDrop()
        {
            SubinventoryDC subDC = new SubinventoryDC();
            DataSet ds1 = subDC.getAllUsedSubinventory_name();
            DropDownList1.DataTextField = "subinventory_name";
            DropDownList1.DataValueField = "subinventory_key";
            DropDownList1.DataSource = ds1.Tables[0].DefaultView;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--选择库别--"));
            DropDownList2.Items.Insert(0, new ListItem("--选择料架--"));
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            PnDC dc=new PnDC();
            DataSet ds2 = dc.getFrameNameBySubinventory(DropDownList1.Items[DropDownList1.SelectedIndex].Text);
            if (ds2 != null)
            {
                DropDownList2.DataSource = ds2.Tables[0].DefaultView;
                DropDownList2.DataValueField = "frame_key";
                DropDownList2.DataTextField = "frame_name";
                DropDownList2.DataBind();
            }
            else
                PageUtil.showToast(this.Page, "料架载入出错！");
            DropDownList2.Items.Insert(0, "--选择料架--");
        }
    }
}