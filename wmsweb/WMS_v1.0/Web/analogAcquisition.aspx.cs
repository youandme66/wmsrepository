using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Util;


namespace WMS_v1._0.Web
{
    public partial class analogAcquisition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "工单领料模拟";
            if(!IsPostBack){
            init();
            }
            

            //if (Session["LoginId"] == null)
            //{
            //    PageUtil.showToast(this, "未获取到你的登陆状态，请退出系统重新登录！");
            //    return;
            //}
        }





        public void init()
        {
            GridView_Add.DataSource = null;
            GridView_Add.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
        }
        //打印按钮中的确定操作----将文本框中的值传给打印界面
        protected void transPrint(object sender, EventArgs e)
        {
            //判断搜索框中的领料单号是否存在
            if (string.IsNullOrEmpty(select_text_print.Value) == true)
            {
                Session["select_text_print_wo_no"] = null;
                PageUtil.showToast(this, "请输入退料单号，再执行此打印操作");
            }
            else
            {
                Session["select_text_print_wo_no"] = select_text_print.Value;
                //打开打印界面
                Response.Write("<script>window.open('printWoNo.aspx', 'newwindow')</script>");
            }
        }
        //打印按钮中的取消操作------清除输入框数据
        protected void CleanInsertMessage(object sender, EventArgs e)
        {
            select_text_print.Value = String.Empty;
        }

        /// <summary>
        /// 清空页面上的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void clear_button_Click(object sender, EventArgs e)
        {
            wo_no_1.Text = "";
            init();
        }
        //获取GridView_add内容
        private DataTable GetGridViewData()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("wo_no_ml"));
            foreach (GridViewRow row in GridView_Add.Rows)
            {
                DataRow sourseRow = table.NewRow();
                sourseRow["wo_no_ml"] = row.Cells[0].Text;
                table.Rows.Add(sourseRow);
            }
            return table;
        }
        //获取gridView1内容
        private DataTable getGridViewData1()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("requirement_line_id"));
            table.Columns.Add(new DataColumn("wo_no"));
            table.Columns.Add(new DataColumn("bom_version"));
            table.Columns.Add(new DataColumn("item_name"));
            table.Columns.Add(new DataColumn("operation_seq_num"));
            table.Columns.Add(new DataColumn("required_qty"));
            table.Columns.Add(new DataColumn("issued_qty"));
            table.Columns.Add(new DataColumn("issue_invoice_qty"));
            table.Columns.Add(new DataColumn("return_invoice_qty"));
            table.Columns.Add(new DataColumn("create_time"));
            table.Columns.Add(new DataColumn("update_time"));
            foreach (GridViewRow row in GridView1.Rows)
            {
                DataRow sourceRow = table.NewRow();
                sourceRow["requirement_line_id"] = row.Cells[0].Text.Trim();
                sourceRow["wo_no"] = row.Cells[1].Text.Trim();
                sourceRow["bom_version"] = row.Cells[2].Text.Trim();
                sourceRow["item_name"] = row.Cells[3].Text.Trim();
                sourceRow["operation_seq_num"] = row.Cells[4].Text.Trim();
                sourceRow["required_qty"] = row.Cells[5].Text.Trim();
                sourceRow["issued_qty"] = row.Cells[6].Text.Trim();
                sourceRow["issue_invoice_qty"] = row.Cells[7].Text.Trim();
                sourceRow["return_invoice_qty"] = row.Cells[8].Text.Trim();
                sourceRow["create_time"] = row.Cells[9].Text.Trim();
                sourceRow["update_time"] = row.Cells[10].Text.Trim();
                table.Rows.Add(sourceRow);  
            }
            return table;
        }
        //获取gridView2内容
        private DataTable getGridViewData2()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("simulate_line_id"));
            table.Columns.Add(new DataColumn("unique_id"));
            table.Columns.Add(new DataColumn("simulate_id"));
            table.Columns.Add(new DataColumn("wo_no"));
            table.Columns.Add(new DataColumn("wo_key"));
            table.Columns.Add(new DataColumn("item_id"));
            table.Columns.Add(new DataColumn("requirement_qty"));
            table.Columns.Add(new DataColumn("simulated_qty"));
            table.Columns.Add(new DataColumn("status"));
            table.Columns.Add(new DataColumn("pickup_qty"));
            table.Columns.Add(new DataColumn("issued_qty"));
            foreach (GridViewRow row in GridView2.Rows)
            {
                DataRow sourceRow = table.NewRow();
                sourceRow["simulate_line_id"] = row.Cells[0].Text.Trim();
                sourceRow["unique_id"] = row.Cells[1].Text.Trim();
                sourceRow["simulate_id"] = row.Cells[2].Text.Trim();
                sourceRow["wo_no"] = row.Cells[3].Text.Trim();
                sourceRow["wo_key"] = row.Cells[4].Text.Trim();
                sourceRow["item_id"] = row.Cells[5].Text.Trim();
                sourceRow["requirement_qty"] = row.Cells[6].Text.Trim();
                sourceRow["simulated_qty"] = row.Cells[7].Text.Trim();
                sourceRow["status"] = row.Cells[8].Text.Trim();
                sourceRow["pickup_qty"] = row.Cells[9].Text.Trim();
                sourceRow["issued_qty"] = row.Cells[10].Text.Trim();
                table.Rows.Add(sourceRow);
            }
            return table;
        }
        //添加gridView1内容
        private DataTable addGridView1(DataSet dt)
        {
            DataTable ds2 = getGridViewData1();
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
            DataRow sourceRow = ds2.NewRow();
            sourceRow["requirement_line_id"] = dt.Tables[0].Rows[i][0].ToString().Trim();
            sourceRow["wo_no"] = dt.Tables[0].Rows[i][1].ToString().Trim();
            sourceRow["bom_version"] = dt.Tables[0].Rows[i][2].ToString().Trim();
            sourceRow["item_name"] = dt.Tables[0].Rows[i][3].ToString().Trim();
            sourceRow["operation_seq_num"] = dt.Tables[0].Rows[i][4].ToString().Trim();
            sourceRow["required_qty"] = dt.Tables[0].Rows[i][5].ToString().Trim();
            sourceRow["issued_qty"] = dt.Tables[0].Rows[i][6].ToString().Trim();
            sourceRow["issue_invoice_qty"] = dt.Tables[0].Rows[i][7].ToString().Trim();
            sourceRow["return_invoice_qty"] = dt.Tables[0].Rows[i][8].ToString().Trim();
            sourceRow["create_time"] = dt.Tables[0].Rows[i][9].ToString().Trim();
            sourceRow["update_time"] = dt.Tables[0].Rows[i][10].ToString().Trim();
            ds2.Rows.Add(sourceRow);
            }
            return ds2;
        }
        //添加gridView1内容
        private DataTable addGridView2(DataSet dt)
        {
            DataTable ds2 = getGridViewData2();
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
            DataRow sourceRow = ds2.NewRow();
            sourceRow["simulate_line_id"] = dt.Tables[0].Rows[i][0].ToString().Trim();
            sourceRow["unique_id"] = dt.Tables[0].Rows[i][1].ToString().Trim();
            sourceRow["simulate_id"] = dt.Tables[0].Rows[i][2].ToString().Trim();
            sourceRow["wo_no"] = dt.Tables[0].Rows[i][3].ToString().Trim();
            sourceRow["wo_key"] = dt.Tables[0].Rows[i][4].ToString().Trim();
            sourceRow["item_id"] = dt.Tables[0].Rows[i][5].ToString().Trim();
            sourceRow["requirement_qty"] = dt.Tables[0].Rows[i][6].ToString().Trim();
            sourceRow["simulated_qty"] = dt.Tables[0].Rows[i][7].ToString().Trim();
            sourceRow["status"] = dt.Tables[0].Rows[i][8].ToString().Trim();
            sourceRow["pickup_qty"] = dt.Tables[0].Rows[i][9].ToString().Trim();
            sourceRow["issued_qty"] = dt.Tables[0].Rows[i][10].ToString().Trim();
            ds2.Rows.Add(sourceRow);
            }
            return ds2;
        }

        //添加新的wo_no
        private DataTable Add_Wo_No(DataTable table, string wo_no)
        {
            DataRow sourseRow = table.NewRow();
            sourseRow["wo_no_ml"] = wo_no;
            table.Rows.Add(sourseRow);
            return table;
        }
        //添加工单号，并追加各个gridview
        protected void add_wo_no_Click(object sender, EventArgs e)
        {
            DataTable table = GetGridViewData();
            string wo_no = wo_no_1.Text;
            if (string.IsNullOrEmpty(wo_no))
            {
                PageUtil.showToast(this, "请输入工单编号！");
                return;
            }
            AnalogAcquisitionDC analogAcquisitionDC = new AnalogAcquisitionDC();
            DataSet ds = analogAcquisitionDC.getSomeByWo_No(wo_no);
            DataSet ds3 = null;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (string.Compare(wo_no, table.Rows[i][0].ToString()) == 0)
                {
                    PageUtil.showToast(this, "工单号重复,请检查后输入");
                    return;
                }
            }
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                PageUtil.showToast(this, "该工单编号不存在！");
                return;
            }
            else
            {
                //ds3 = analogAcquisitionDC.get_simulite_by_wo_no(wo_no);
                //if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                //{
                //    DateTime d = DateTime.Now;
                //    int simulate_id=int.Parse(d.ToString("yyyyMMdd"));
                //    int a = analogAcquisitionDC.add_wo_simulate(wo_no);
                //    if (a == 0)
                //    {
                //        PageUtil.showToast(this.Page, "添加模拟单失败");
                //        return;
                //    }
                //    else
                //    {
                //        a = analogAcquisitionDC.update_wo_simulate(simulate_id, wo_no);
                //        if (a == 0)
                //        {
                //            PageUtil.showToast(this.Page, "系统崩溃");
                //            return;
                //        }
                //        else
                //        {
                //            ds3 = analogAcquisitionDC.get_simulite_by_wo_no(wo_no);
                //        }
                //    }
                //}
            }
            Add_Wo_No(table, wo_no);
            GridView_Add.DataSource = table;
            GridView1.DataSource = addGridView1(ds);
            //GridView2.DataSource = addGridView2(ds3);
            GridView_Add.DataBind();
            GridView1.DataBind();
            //GridView2.DataBind();
            wo_no_1.Text = "";
            panl1.Style["display"] = "block";
            panl2.Style["display"] = "none";
            panl3.Style["display"] = "none";
            string message = "更新完成";
            status.InnerText = message;

        }
        //获取最新需求
        protected void one_latest_demand(object sender, EventArgs e)
        {
            
            string message = "更新完成";
            AnalogAcquisitionDC analogAcquisitionDC = new AnalogAcquisitionDC();
            DataTable table = GetGridViewData();
            string[] wo_no_array=new string[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                wo_no_array[i] = table.Rows[i][0].ToString().Trim();
            }
            if (wo_no_array.Length == 0)
            {
                PageUtil.showToast(this.Page, "请输入工单号");
                return;
            }
            DataSet ds1 = analogAcquisitionDC.get_new_demand(wo_no_array);
            int panduan = analogAcquisitionDC.update_simulate(wo_no_array);
            //DataSet ds2 = analogAcquisitionDC.get_new_simulate(wo_no_array);
            if (panduan == 1)
            {
                panl1.Style["display"] = "block";
                panl2.Style["display"] = "none";
                panl3.Style["display"] = "none";
                GridView1.DataSource = ds1;
                //GridView2.DataSource = ds2;
                GridView1.DataBind();
                //GridView2.DataBind();
                status.InnerText = message;
            }
            else
            {
                PageUtil.showToast(this.Page, "最新需求获取失败，可能是有人删除了工单。");
            }
        }


        /// <summary>
        /// 开始模拟
        /// 按工單将仓库中的原料HOLD住
        /// 遵循先进先出的原则，还有有退过料的先出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void start_analog_Click(object sender, EventArgs e)
        {
            DataTable table = GetGridViewData();
            string[] wo_no_array = new string[table.Rows.Count];
            Simulate_operation dc = new Simulate_operation();
            int z = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int j=dc.getSimulateByWo(table.Rows[i][0].ToString());
                if (j == 1)
                {
                    PageUtil.showToast(this.Page, "请不要重复模拟部分工单，请刷新！");
                    return;
                }
                else
                {
                    wo_no_array[i] = table.Rows[i][0].ToString();
                }
            }
            AnalogAcquisitionDC analogAcquisitionDC = new AnalogAcquisitionDC();
           int fail= analogAcquisitionDC.start_simulate(wo_no_array);
           if (fail == 1)
           {
               PageUtil.showToast(this.Page, "模拟成功");
           }
           else
           {
               PageUtil.showToast(this.Page, "模拟失败");
           }
        }

        //根据输入框的工单号进行单向模拟查询
        protected void select_one_simulate(object sender, EventArgs e)
        {
            AnalogAcquisitionDC DC=new AnalogAcquisitionDC();
            string wo_no = wo_no_1.Text;
            if (string.IsNullOrEmpty(wo_no))
            {
                PageUtil.showToast(this, "请输入工单编号！");
                return;
            }
            DataSet ds = DC.get_simulite_by_wo_no(wo_no);
            panl1.Style["display"] = "none";
            panl2.Style["display"] = "block";
            panl3.Style["display"] = "none";
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void export_excel_Click(object sender, EventArgs e)
        {

        }
    }
}