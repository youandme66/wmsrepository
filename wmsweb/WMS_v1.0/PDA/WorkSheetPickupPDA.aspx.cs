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
    public partial class WorkSheetPickupPDA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Local"] = "工单备料";
        }

        //打印按钮中的确定操作----将文本框中的值传给打印界面
        protected void transPrint(object sender, EventArgs e)
        {
            //判断搜索框中的备料单号是否存在
            if (string.IsNullOrEmpty(select_text_print.Value) == true)
            {
                Session["select_text_print"] = null;
                PageUtil.showToast(this, "请输入备料单号，再执行此打印操作");
            }
            else
            {
                Session["select_text_print"] = select_text_print.Value;
                //打开打印界面
                Response.Write("<script>window.open('printSinglePreparation.aspx', 'newwindow')</script>");
            }
        }

        //打印按钮中的取消操作------清除输入框数据
        protected void CleanInsertMessage(object sender, EventArgs e)
        {
            select_text_print.Value = String.Empty;
        }

        /**
        * 
        * 限定料号和数量长度*
        * 
        ***/
        public void Nu_Leng(string str, string name)
        {
            if (str.Length > 20)
            {
                PageUtil.showToast(this, "" + name + "长度超过范围！");
                return;
            }
        }

        /**
       * 
       * 限定DateCode长度*
       * 
       ***/
        public void Da_Leng(string str, string name)
        {
            if (str.Length > 15)
            {
                PageUtil.showToast(this, "" + name + "过长超过范围！");
                return;
            }
        }

        /**
         * 
         * 查询备料信息*
         *
         **/
        protected void Select(object sender, EventArgs e)
        {
            //从前台获取数据
            string Simulate_id = simulate_id.Value;
            //判断备料单号是否为空
            if (Simulate_id == "")
            {
                PageUtil.showToast(this, "备料单号不能为空！");
                return;
            }
            //查询备料信息
            Pickup_mtlDC pickup = new Pickup_mtlDC();
            DataSet ds = new DataSet();
            ds = pickup.searchBySimulate(Simulate_id);
            if (ds == null)
            {
                PageUtil.showToast(this, "表中无符合条件的数据！");
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                Label1.Text = this.GridView1.Rows[0].Cells[2].Text.ToString();
            }
        }

        /**
        * 
        * 备料*
        *
        **/
        protected void Pickup(object sender, EventArgs e)
        {
            //从前台获取数据
            string INDC = indc.Value;
            Pickup_mtlDC pickup = new Pickup_mtlDC();
            //判断条码信息是否为空和输入格式是否正确
            if (INDC == "")
            {
                PageUtil.showToast(this, "条码信息不能为空！");
                return;
            }
            else if (!Regex.IsMatch(INDC, @"[\w]+.[\w]+#\d+#\d+"))
            {
                PageUtil.showToast(this, "格式输入不正确！");
                return;
            }
            //提取条码信息中的数据
            string[] s = INDC.Split(new char[] { '#' });
            string Item_id = s[0];
            Nu_Leng(Item_id, "料号");
            string Number = s[1];
            Nu_Leng(Number, "数量");
            //判断是否是数字字符串，是则将其转换成整型
            if (!Regex.IsMatch(Number, @"\d+"))
            {
                PageUtil.showToast(this, "数量应为整型！");
            }
            int number = int.Parse(Number);
            string Datecode = s[2];
            //int item_id = 0;
            Da_Leng(Datecode, "DateCode");
            //try
            //{
            //    item_id = int.Parse(Item_id);
            //}
            //catch (Exception e2)
            //{
            //    PageUtil.showToast(this.Page, "请输入Item_ID#数量#DataCode");
            //    return;
            //}
            //先查询后才能备料
            if (Label1.Text == "工单号")
            {
                PageUtil.showToast(this, "请先查询！");
                return;
            }
            //从表格中获取数据    
            //string Simulate_id = this.GridView1.Rows[0].Cells[1].Text.ToString();
            //string ITEM_ID = this.GridView1.Rows[0].Cells[4].Text.ToString();
            //string SIMULATED_QTY = this.GridView1.Rows[0].Cells[6].Text.ToString();
            //string PICKUP_QTY = this.GridView1.Rows[0].Cells[8].Text.ToString();
            //将字符串转换成整型
            //int simulated_qty=int.Parse (SIMULATED_QTY);
            //int pickup_qty = int.Parse(PICKUP_QTY);

            //int sum = number + pickup_qty;
            //string Num = sum.ToString();
            //判断输入的料号与表格中的料号是否相等
            int simulate = 0;
            if (simulate_id.Value.ToString() != "")
            {
                simulate = int.Parse(simulate_id.Value.ToString());
            }
            else
            {
                PageUtil.showToast(this.Page, "请先查询备料单号");
                return;
            }
            if (number < 0)
            {
                PageUtil.showToast(this, "备料的数量应该大于0");
                return;
            }
            else
            {
                lock (this)
                {
                    int[] num = pickup.search_pickup_num(simulate, Item_id, Datecode);
                    if (num == null)
                    {
                        PageUtil.showToast(this.Page, "没有符合的数据，请再次输入");
                        return;
                    }
                    if (number > num[0])
                    {
                        PageUtil.showToast(this, "数量超出！");
                        return;
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        ds = pickup.preparation(simulate, Item_id, number, Datecode, num[1]);
                        if (ds == null)
                        {
                            PageUtil.showToast(this.Page, "备料失败");
                        }
                        else
                        {
                            GridView1.DataSource = ds;
                            GridView1.DataBind();
                            PageUtil.showToast(this, "备料成功");
                        }
                    }
                }
            }
        }
    }

}