using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;
using WMS_v1._0.Util;

namespace WMS_v1._0.Web
{
    public partial class materialRequisitionPage : System.Web.UI.Page
    {
        IssueHeaderDC issueHeaderDC;

        IssuelineDC issueLineDC;

        ModelMtl_issue_header modelMtl_issue_header = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "领料单";
            if (!IsPostBack)//当第一次进入该页面时
            {
                issue_type.Items.Insert(0, "选择类型");
                Session["issue_type"] = "none";
            }
            //this.issue_type.SelectedIndexChanged += new System.EventHandler(this.ddlCategoryid_SelectedIndexChanged);
        }

        ////当领料类型（issue_type下拉框）发生变化时，将值传给打印界面
        //protected void ddlCategoryid_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session["issue_type"] = issue_type.SelectedValue.ToString();
        //}

        //打印按钮中的确定操作----将文本框中的值传给打印界面
        protected void transPrint(object sender, EventArgs e)
        {
            //判断搜索框中的领料单号是否存在
            if (string.IsNullOrEmpty(select_text_print.Value) == true)
            {
                Session["select_text_print"] = null;
                PageUtil.showToast(this, "请输入领料单号，再执行此打印操作");
            }
            else
            {
                Session["select_text_print"] = select_text_print.Value;
                //打开打印界面
                Response.Write("<script>window.open('printMaterialRequisition.aspx', 'newwindow')</script>");
            }
        }

        //打印按钮中的取消操作------清除输入框数据
        protected void CleanInsertMessage(object sender, EventArgs e)
        {
            select_text_print.Value = String.Empty;
        }


        

      


        //清除填写信息,供扣账后使用
        protected void Auto_clear()
        {
            invoice_no.Value = string.Empty;
            wo_no.Value = string.Empty;
            issued_sub_key.Value = string.Empty;
            issue_type.SelectedValue = string.Empty;
            issue_man.Value = string.Empty;
            item_name.Value = string.Empty;
            required_qty.Value = string.Empty;
            simulated_qty.Value = string.Empty;
            issued_qty.Value = string.Empty;
            frame_key.Value = string.Empty;
            customer_key.Value = string.Empty;
        }


        //插入领料单--只是生成领料单号
        protected void insert(object sender, EventArgs e)
        {
            string strInvoice_no = invoice_no.Value;

            if (!string.IsNullOrWhiteSpace(strInvoice_no))
            {

                PageUtil.showToast(this, "当前领料单信息未提交！");

                return;

            }


            issueHeaderDC = new IssueHeaderDC();

            int end;
            string end_text;
            string month = string.Empty;
            string year = string.Empty;

            modelMtl_issue_header = issueHeaderDC.getTheNewestIssueHeaderByCreatetime();

            //判断数据库是否有数据,没有数据则初始化一个流水号
            if (modelMtl_issue_header == null)
            {

                end_text = "00001";

                month = DateTime.Now.Month.ToString();

                year = DateTime.Now.Year.ToString();


                //为月份补0
                for (int i = month.Length; i < 2; i++)
                {

                    month = "0" + month;

                }

                //截取年份后两位
                year = year.Substring(2, 2);
            }
            else
            {
                end = modelMtl_issue_header.Issue_header_id;


                //判断id是否为99998,是则取下一个流水号为99999,否则取余+1作为下一个流水号
                end = end % 99998 == 0 ? 99998 + 1 : end % 99999 + 1;

                end_text = end.ToString();


                //为流水号补0
                for (int i = end_text.Length; i < 5; i++)
                {

                    end_text = "0" + end_text;

                }

                month = DateTime.Now.Month.ToString();

                year = DateTime.Now.Year.ToString();

                year = year.Substring(2, 2);

                for (int i = month.Length; i < 2; i++)
                {

                    month = "0" + month;

                }
            }

            invoice_no.Value = "Q" + year + month + end_text;

        }


        //提交领料单主表信息 ,并写入数据库.
        protected void Issue_Header_Commit(object sender, EventArgs e)
        {
            issueHeaderDC = new IssueHeaderDC();

            string INVOICE_NO = invoice_no.Value;                                   //领料单号


            /*避免重复提交*/
            try
            {
                ModelMtl_issue_header header = header = issueHeaderDC.getTheNewestIssueHeaderByCreatetime();

                if (header.Invoice_no == INVOICE_NO)
                {

                    PageUtil.showToast(this, "请勿重复提交！");

                    return;

                }
            }
            catch (Exception)
            {
                Boolean flag = false;
            }



            string WO_NO = Request.Form["wo_no_c"];                                 //工单号
            string ISSUED_SUB_KEY = Request["issued_sub_key_c"];                    //库别
            string OPERATION_SEQ_NUM = Request.Form["operation_seq_num_c"];         //制程
            string ISSUE_TYPE = issue_type.SelectedValue;                                   //领料类别
            string ISSUE_MAN = issue_man.Value;                                     //领料人
            string CUSTOMER_KEY = customer_key.Value;                               //客户代码

            if (ISSUE_MAN.Length > 10) {

                PageUtil.showToast(this,"领料人长度超出范围！");

                return;
            }

            if (CUSTOMER_KEY.Length > 10) {

                PageUtil.showToast(this,"客户代码长度超出范围！");

                return;
            }

            DateTime CREATE_TIME = DateTime.Now;

            DateTime UPDATE_TIME = DateTime.Now;

            if (string.IsNullOrWhiteSpace(ISSUE_MAN) || string.IsNullOrWhiteSpace(CUSTOMER_KEY))
            {
                string error = "请填写完整信息!";

                PageUtil.showToast(this, error);

                return;
            }



            try
            {
                issueHeaderDC.insertIssueHeader(INVOICE_NO, WO_NO, ISSUED_SUB_KEY, CREATE_TIME, UPDATE_TIME, OPERATION_SEQ_NUM, ISSUE_TYPE, ISSUE_MAN, CUSTOMER_KEY);

                PageUtil.showToast(this, "插入信息成功！");
            }
            catch (Exception)
            {

                PageUtil.showToast(this, "插入信息失败！");

                return;
            }

            if (ISSUE_TYPE == "工单领料") {

                string target_qty = issueHeaderDC.getTarget(WO_NO);

                required_qty.Value = target_qty;

            }


        }


        //提交信息（从表）到下方表格
        protected void Issue_Line_Commit(object sender, EventArgs e)
        {

            string ITEM_NAME = item_name.Value;

            if (ITEM_NAME.Length > 30) {

                PageUtil.showToast(this,"料号长度超出范围！");

                return;

            }

            string strREQUIRED_QTY = required_qty.Value;
            string strSIMULATED_QTY = simulated_qty.Value;
            string strISSUED_QTY = issued_qty.Value;

            if (string.IsNullOrWhiteSpace(ITEM_NAME) || string.IsNullOrWhiteSpace(strREQUIRED_QTY) || string.IsNullOrWhiteSpace(strSIMULATED_QTY) || string.IsNullOrWhiteSpace(strISSUED_QTY)) {

                PageUtil.showToast(this,"请填写完整信息！");

                return;
            }


            Int64 REQUIRED_QTY64 = 0;
            Int64 SIMULATED_QTY64 = 0;
            Int64 ISSUED_QTY64 = 0;

            try
            {
                REQUIRED_QTY64 = Int64.Parse(strREQUIRED_QTY);
            }
            catch (Exception)
            {

                PageUtil.showToast(this,"需求量格式错误！");

                return;
            }

            if (REQUIRED_QTY64 > Math.Pow(2, 31) - 1) {

                PageUtil.showToast(this,"需求量长度超出范围！");

                return;

            }

            try
            {
                SIMULATED_QTY64 = Int64.Parse(strSIMULATED_QTY);
            }
            catch (Exception)
            {

                PageUtil.showToast(this, "模拟量格式错误！");

                return;
            }

            if (SIMULATED_QTY64 > Math.Pow(2, 31) - 1)
            {

                PageUtil.showToast(this, "模拟量长度超出范围！");

                return;

            }


            try
            {
                ISSUED_QTY64 = Int64.Parse(strISSUED_QTY);
            }
            catch (Exception)
            {

                PageUtil.showToast(this, "领料量格式错误！");

                return;
            }

            if (ISSUED_QTY64 > Math.Pow(2, 31) - 1)
            {

                PageUtil.showToast(this, "领料量长度超出范围！");

                return;

            }
            


            string FRAME_KEY = Request.Form["frame_key_c"];

            DataTable table = new DataTable();

            table = GetGridViewData(table);

            DataRow sourseRow = table.NewRow();
            sourseRow["ITEM_NAME"] = ITEM_NAME;
            sourseRow["REQUIRED_QTY"] = strREQUIRED_QTY;
            sourseRow["SIMULATED_QTY"] = strSIMULATED_QTY;
            sourseRow["ISSUED_QTY"] = strISSUED_QTY;
            sourseRow["FRAME_KEY"] = FRAME_KEY;

            table.Rows.Add(sourseRow);

            GridView1.DataSource = table;

            GridView1.DataBind();
        }

        //将所有从表信息插入数据库
        protected void Commit_All_Issue_Line(object sender, EventArgs e)
        {

            string INVOICE_NO = invoice_no.Value;

            DataTable table = new DataTable();

            issueLineDC = new IssuelineDC();

            table = GetGridViewData(table);

            if (table.Rows.Count == 0) {

                PageUtil.showToast(this,"请将信息提交到右方表格！");

                return;

            }

            try
            {
                issueLineDC.insertMtl_issue_line(table, INVOICE_NO);

                Auto_clear();

                PageUtil.showToast(this, "信息提交成功！");

            }
            catch (Exception)
            {

                PageUtil.showToast(this, "信息提交失败！");

                return;
            }

        }


        

        //获取页面上表格中的所有行,并组装为tabel
        private DataTable GetGridViewData(DataTable table)
        {
            table.Columns.Add(new DataColumn("ITEM_NAME"));
            table.Columns.Add(new DataColumn("REQUIRED_QTY"));
            table.Columns.Add(new DataColumn("SIMULATED_QTY"));
            table.Columns.Add(new DataColumn("ISSUED_QTY"));
            table.Columns.Add(new DataColumn("FRAME_KEY"));

            foreach (GridViewRow row in GridView1.Rows)
            {
                DataRow sourseRow = table.NewRow();
                sourseRow["ITEM_NAME"] = row.Cells[0].Text;
                sourseRow["REQUIRED_QTY"] = row.Cells[1].Text;
                sourseRow["SIMULATED_QTY"] = row.Cells[2].Text;
                sourseRow["ISSUED_QTY"] = row.Cells[3].Text;
                sourseRow["FRAME_KEY"] = row.Cells[4].Text;
                table.Rows.Add(sourseRow);
            }

            return table;
        }


        //模糊查询领料单号
        protected void select(object sender, EventArgs e)
        {

            issueHeaderDC = new IssueHeaderDC();

            string SELECT_TEXT = select_text.Value;

            List<ModelMtl_issue_header> modelMtl_issue_header = new List<ModelMtl_issue_header>();

            try
            {
                modelMtl_issue_header = issueHeaderDC.getIssueHeaderByLikeINVOICE_NO(SELECT_TEXT);

            }
            catch (Exception)
            {
                PageUtil.showToast(this, "查询失败！");

                return;
            }

            GridView2.DataSource = modelMtl_issue_header;

            GridView2.DataBind();

        }


        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;

            issueHeaderDC = new IssueHeaderDC();

            string SELECT_TEXT = select_text.Value;

            List<ModelMtl_issue_header> modelMtl_issue_header = new List<ModelMtl_issue_header>();

            modelMtl_issue_header = issueHeaderDC.getIssueHeaderByLikeINVOICE_NO(SELECT_TEXT);

            GridView2.DataSource = modelMtl_issue_header;

            GridView2.DataBind();
        }



        //删除表格行
        public void Delete_row(object sender,GridViewDeleteEventArgs e) {

            int index = e.RowIndex;

            DataTable table = new DataTable();

            table = GetGridViewData(table);

            int flag = 0;

            foreach (DataRow row in table.Rows) {

                if (index == flag) {

                    row.Delete();

                    break;

                }
                flag += 1;
            }

            GridView1.DataSource = table;

            GridView1.DataBind();
        }
    }
}