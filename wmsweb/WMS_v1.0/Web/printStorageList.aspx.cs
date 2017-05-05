using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;
using WMS_v1._0.Util;

namespace WMS_v1._0.Web
{
    public partial class printStorageList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断入庫工單號是否存在，将根据入庫工單號进行一下所有操作
            if (Session["select_text_print"] == null || Session["select_text_print2"] == null)
                PageUtil.showAlert(this, "请输入需要入庫的工單號，再执行此打印操作");
            else
            {
                select_text.Value = Session["select_text_print"].ToString();
                select_text2.Value = Session["select_text_print2"].ToString();
                DataView();
                PageUtil.printPage(this);
            }
        }

        //显示打印页面中的所有数据
        protected void DataView()
        {
            Transaction_operationDC transaction_operationdc = new Transaction_operationDC();
            List<ModelTransaction_operation> modelTransaction_operation = new List<ModelTransaction_operation>();
            WorkSheetInDC worksheetindc = new WorkSheetInDC();

            string transaction_type="workSheetIn";
            modelTransaction_operation = transaction_operationdc.getTransactionByTransaction_qty(transaction_type,int.Parse(select_text2.Value));

            transaction_time.Value = modelTransaction_operation[0].Transaction_time.ToLongDateString();
            transaction_create_user.Value = modelTransaction_operation[0].Create_user;

            try
            {
                //绑定Repeater中数据
                DataSet dataset = new DataSet();
                dataset = worksheetindc.getSomeByWo_no(select_text.Value);

                printStorageListRepeater.DataSource = dataset;
                printStorageListRepeater.DataBind();
            }
            catch (Exception e)
            {
                PageUtil.showToast(this, "数据库中没有对应数据，请重新输入需要入庫的工單號");
            }
        }
    }
}