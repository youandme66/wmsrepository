using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;

namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/9/28
{
    public class Transaction_operationDC//交易表（wms_transaction_operation）对应的DateCenter
    {
        /**作者：周雅雯 时间：2016/9/28
         * 交易查询页面需要的查询方法
         **/
        public List<ModelTransaction_operation> getTransactionBySome(string transaction_type, DateTime start_time, DateTime end_time, string item_name, string invoice_no, string po_no)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当transaction_type有值时
            if (string.IsNullOrWhiteSpace(transaction_type) == false)
            {
                sqlTail += "AND transaction_type LIKE '%'+@transaction_type+'%' ";
            }
            //对start_time和end_time做处理，默认他们两个都有值
            sqlTail += "AND  transaction_time BETWEEN @start_time AND @end_time ";


            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name LIKE '%'+@item_name+'%' ";
            }
            //当invoice_no有值时
            if (string.IsNullOrWhiteSpace(invoice_no) == false)
            {
                sqlTail += "AND invoice_no LIKE '%'+@invoice_no+'%' ";
            }
            //当po_no有值时
            if (string.IsNullOrWhiteSpace(po_no) == false)
            {
                sqlTail += "AND po_no LIKE '%'+@po_no+'%' ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_transaction_operation ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_transaction_operation WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("transaction_type", transaction_type),
                    new SqlParameter("start_time", start_time),
                    new SqlParameter("end_time", end_time),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("invoice_no", invoice_no),
                    new SqlParameter("po_no", po_no),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            List<ModelTransaction_operation> ModelTransaction_operationlist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelTransaction_operationlist = new List<ModelTransaction_operation>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelTransaction_operationlist.Add(toModel(DateSetRows));
                }
                return ModelTransaction_operationlist;
            }
            else
            {
                return ModelTransaction_operationlist;
            }
        }

        /**作者：周雅雯 时间：2016/9/28
         * 交易查询页面需要的查询方法
         **/
        public DataSet getTransactionBySome(string transaction_type, string item_name, string invoice_no)
        {
            String sql = "select * from wms_transaction_operation where " +
                "transaction_type = @transaction_type and " +
                "item_name = @item_name AND " +
                "invoice_no = @invoice_no ";

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("transaction_type", transaction_type),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("invoice_no", invoice_no)
                };

            DataSet ds = DB.select(sql, parameters);
            return ds;
        }

        /** 插入数据到Transaction_operation   
         **/
        public Boolean insertTransaction_operation(int transaction_qty, string transaction_type,
            DateTime transaction_time)
        {
            string sql = "insert into wms_transaction_operation(TRANSACTION_QTY,TRANSACTION_TYPE,TRANSACTION_TIME) "
                         + "values(@transaction_qty,@transaction_type,@transaction_time)";

            SqlParameter[] parameters = {
                new SqlParameter("transaction_qty", transaction_qty),
                new SqlParameter("transaction_type", transaction_type),
                new SqlParameter("transaction_time", transaction_time)
            };

            DB.connect();

            int flag = DB.insert(sql, parameters);

            if (flag == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //通过transaction_qty，获取ModelTransaction_operation对象的列表集合
        public List<ModelTransaction_operation> getTransactionByTransaction_qty(string transaction_type, int transaction_qty)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_transaction_operation where transaction_qty = @transaction_qty and transaction_type=@transaction_type ";

            SqlParameter[] parameters = {
                new SqlParameter("transaction_qty", transaction_qty),
                new SqlParameter("transaction_type", transaction_type),
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelTransaction_operation> ModelTransaction_operationlist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelTransaction_operationlist = new List<ModelTransaction_operation>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelTransaction_operationlist.Add(toModel(DateSetRows));
                }
                return ModelTransaction_operationlist;
            }
            else
            {
                return ModelTransaction_operationlist;
            }
        }





        // 传入DataRow,将其转换为ModelTransaction_operation
        private ModelTransaction_operation toModel(DataRow dr)
        {
            ModelTransaction_operation model = new ModelTransaction_operation();

            //通过循环为ModelTransaction_operation赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelTransaction_operation).GetProperties())
            {
                //如果数据库的字段为空，跳过其赋值
                if (dr[propertyInfo.Name].ToString() == "")
                {
                    continue;
                }
                //赋值
                model.GetType().GetProperty(propertyInfo.Name).SetValue(model, dr[propertyInfo.Name], null);
            }
            return model;
        }
    }
}