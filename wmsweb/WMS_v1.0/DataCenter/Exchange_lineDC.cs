using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;

namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/10/13
{
    public class Exchange_lineDC//调拨单从表（Exchange_line）对应的DataCenter
    {
        //向调拨单从表中插入数据
        public Boolean insertExchange_line(int exchange_header_id, string item_name, int required_qty, DateTime create_time, string exchange_wo_no, string remark)
        {

            string sql = "insert into wms_exchange_line "
                       + "(exchange_header_id,item_name,required_qty,create_time,exchange_wo_no,remark)values "
                       + "(@exchange_header_id,@item_name,@required_qty,@create_time,@exchange_wo_no,@remark) ";

            SqlParameter[] parameters = {
                new SqlParameter("exchange_header_id",exchange_header_id),
                new SqlParameter("item_name",item_name),
                new SqlParameter("required_qty",required_qty),
                new SqlParameter("create_time",create_time),
                new SqlParameter("exchange_wo_no",exchange_wo_no),
                new SqlParameter("remark",remark)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        //通过调拨单主表的调拨单号invoice_no，查询调拨单从表信息
        public List<ModelExchange_line> getExchange_lineByInvoice_no(string invoice_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_exchange_line where exchange_header_id in (select exchange_header_id from wms_exchange_header where invoice_no = @invoice_no)";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no", invoice_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelExchange_line> ModelExchange_line_list = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                ModelExchange_line_list = new List<ModelExchange_line>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    ModelExchange_line_list.Add(toModel(datarow));
                }
            }
            return ModelExchange_line_list;
        }

        //通过调拨主表中的调拨单号invoice_no，查询出调拨单打印所需的数据
        public DataSet getSomeByinvoice_no(string invoice_no)
        {
            //通过SQL语句，获取DateSet
            string sql = "select  wms_exchange_line.operation_seq_num,wms_exchange_line.item_name,datecode=(select datecode from wms_material_io where item_id=(select item_id from wms_pn where item_name=(select item_name from wms_exchange_line where exchange_header_id=(select exchange_header_id from wms_exchange_header where invoice_no=@invoice_no )))),out_locator_name=(select frame_name from wms_frame where frame_key=(select out_locator_id from wms_exchange_header where invoice_no=@invoice_no)),in_locator_name=(select frame_name from wms_frame where frame_key=(select in_locator_id from wms_exchange_header where invoice_no=@invoice_no)),required_qty,exchanged_qty from wms_exchange_line where exchange_header_id=(select exchange_header_id from wms_exchange_header where invoice_no=@invoice_no) ";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no", invoice_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)
                return ds;
            else
                return null;
        }

        //向调拨单从表中插入数据
        public Boolean insertExchange_line(int exchange_header_id, string item_name, int required_qty)
        {

            string sql = "insert into wms_exchange_line "
                       + "(exchange_header_id,item_name,required_qty)values "
                       + "(@exchange_header_id,@item_name,@required_qty) ";

            SqlParameter[] parameters = {
                new SqlParameter("exchange_header_id",exchange_header_id),
                new SqlParameter("item_name",item_name),
                new SqlParameter("required_qty",required_qty)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        // 传入DataRow,将其转换为ModelExchange_line
        private ModelExchange_line toModel(DataRow dr)
        {
            ModelExchange_line model = new ModelExchange_line();

            //通过循环为ModelExchange_line赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelExchange_line).GetProperties())
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