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

namespace WMS_v1._0.PDA
{
    public partial class worksheetIssuePDA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "工单发料";
        }
        //分页
        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex = e.NewPageIndex;
        //    string SIMULATE_ID = simulate_id.Value;
        //    if (!(Regex.IsMatch(SIMULATE_ID, "^[0-9]*$") || SIMULATE_ID == ""))
        //    {
        //        PageUtil.showAlert(this, "请输入数字！");
        //        return;
        //    }
        //    Pickup_mtlDC faliao = new Pickup_mtlDC();
        //    GridView1.DataSource = faliao.searchBySimulate(SIMULATE_ID);
        //    GridView1.DataBind();
        //}

        //备料单号查询
        protected void Select(object sender, EventArgs e)
        {


            string SIMULATE_ID = simulate_id.Value;
            if (!(Regex.IsMatch(SIMULATE_ID, "^[0-9]*$") || SIMULATE_ID == ""))
            {
                PageUtil.showAlert(this, "请输入数字！");
                return;
            }
            Pickup_mtlDC faliao = new Pickup_mtlDC();
            Work_Repeater.DataSource = faliao.searchPickupBySimulate(SIMULATE_ID);
            Work_Repeater.DataBind();

        }
        //发料
        protected void Store_issue(object sender, EventArgs e)
        {
            Pickup_mtlDC DC = new Pickup_mtlDC();
            string SIMULATE = simulate.Value;
            string ITEM_ID = item_id1.Value;
            //string PICKUP_QTY = pickup_qty.Value;
            string ISSUED_QTY = issued_qty.Value;
            int IQ = int.Parse(ISSUED_QTY);
            int simulate_id = int.Parse(SIMULATE);
            int item_id = int.Parse(ITEM_ID);
            //int PQ=int.Parse(PICKUP_QTY);
            lock (this)
            {
                int num = DC.search_issue_num(simulate_id, item_id);
                if (num == -1)
                {
                    PageUtil.showToast(this.Page, "没有符合的数据");
                }
                else if (IQ > num)
                {
                    PageUtil.showToast(this, "发料失败,发料数量多于剩余的备料数量!");
                }
                else
                {
                    Pickup_mtlDC faliao1 = new Pickup_mtlDC();
                    DataSet ds = new DataSet();
                    ds = faliao1.Issue(simulate_id, IQ, item_id);
                    if (ds == null)
                    {
                        Work_Repeater.DataSource = ds;
                        Work_Repeater.DataBind();
                        PageUtil.showToast(this.Page, "发料失败");
                    }
                    else
                    {
                        Work_Repeater.DataSource = ds;
                        Work_Repeater.DataBind();
                        issued_qty.Value = null;
                        PageUtil.showToast(this, "发料成功");
                    }
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Pickup_mtlDC DC = new Pickup_mtlDC();
            string simulate_line_id = simulate_id.Value;
            DataSet ds = DC.is_issued(simulate_line_id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                int j = DC.issued_qty(simulate_line_id);
                if (j == 1)
                {
                    Work_Repeater.DataSource = DC.searchPickupBySimulate(simulate_line_id);
                    Work_Repeater.DataBind();
                    PageUtil.showToast(this.Page, "发料成功");
                }
                else
                {
                    PageUtil.showToast(this.Page, "发料失败");
                }
            }
            else
            {
                PageUtil.showToast(this.Page, "没有可以发的料");
            }

        }




        //protected void Store_issue(object sender, EventArgs e)
        //{
        //    int i;
        //    int count = Convert.ToInt32(this.GridView1.Rows.Count);
        //    string Celltext;
        //    for (i = 0; i < count; i++)
        //    {
        //        Celltext = GridView1.Rows[i].Cells[0].Text;
        //        if (Celltext == simulate.Value)
        //        {
        //            try
        //            {

        //                string SIMULATE = simulate.Value;
        //                Pickup_mtlDC faliao1 = new Pickup_mtlDC();
        //                DataSet ds = faliao1.Issue(SIMULATE);
        //                GridView1.DataSource = ds;
        //                GridView1.DataBind();
        //                //PageUtil.showToast(this.Page, "发料成功");
        //            }
        //            catch
        //            {
        //                PageUtil.showToast(this.Page, "发料失败");
        //            }
        //            PageUtil.showToast(this.Page, "发料成功");
        //            break;
        //        }

        //    }
        //    if (i == count)
        //    {
        //        PageUtil.showAlert(this.Page, "发料失败！！\\n请输入正确的备料单号");
        //    }

        //}
    }
}