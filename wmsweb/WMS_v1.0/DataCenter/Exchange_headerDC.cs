using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;


namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/9/11
{
    public class Exchange_headerDC//调拨单主表（Exchange_header）对应的DataCenter
    {
        //通过invoice_no获取调拨单主键值
        public int getExchange_header_idByinvoice_no(string invoice_no)
        {
            //通过SQL语句，获取DateSet
            string sql = "select Exchange_header_id from wms_exchange_header where invoice_no = @invoice_no";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no", invoice_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            int exchange_header_id = int.Parse(ds.Tables[0].Rows[0]["exchange_header_id"].ToString());

            return exchange_header_id;

        }

        //向调拨单主表中插入数据
        public Boolean insertExchange_header(string invoice_no, int out_subinventory_key, int in_subinventory_key, DateTime create_time, DateTime update_time, string exchange_wo_no)
        {

            string sql = "insert into wms_exchange_header "
                       + "(invoice_no,out_subinventory_key,in_subinventory_key,create_time,update_time,exchange_wo_no)values "
                       + "(@invoice_no,@out_subinventory_key,@in_subinventory_key,@create_time,@update_time,@exchange_wo_no) ";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no",invoice_no),
                new SqlParameter("out_subinventory_key",out_subinventory_key),
                new SqlParameter("in_subinventory_key",in_subinventory_key),
                new SqlParameter("create_time",create_time),
                new SqlParameter("update_time",update_time),
                new SqlParameter("exchange_wo_no",exchange_wo_no)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //通过自增长的createtime，获取最新一条退料主表信息
        public DataSet getTheNewestExchange_header_idByCreatetime()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT TOP 1.*  FROM wms_exchange_header ORDER BY Exchange_header_id DESC";

            SqlParameter[] parameters = null;


            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
       






        //向调拨单主表中插入数据
        public Boolean insertExchange_header(string invoice_no)
        {

            string sql = "insert into wms_exchange_header "
                       + "(invoice_no)values "
                       + "(@invoice_no) ";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no",invoice_no)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //调拨单页面对应commit操作，将传进的参数分别插入到调拨单主表和从表中去
        public Boolean CommitAction(DataTable dataTable)
        {
            //将dataTable中的每行数据插入数据库中
            foreach (DataRow datarow in dataTable.Rows)
            {
                //将每行数据插入数据库中
                Exchange_headerDC exchange_headerDC = new Exchange_headerDC();
                Exchange_lineDC exchange_lineDC = new Exchange_lineDC();

                //将数据插入调拨单主表
                Boolean flag_header = exchange_headerDC.insertExchange_header(datarow.ItemArray[5].ToString(), int.Parse(datarow.ItemArray[3].ToString()), int.Parse(datarow.ItemArray[2].ToString()), DateTime.Now, DateTime.Now, datarow.ItemArray[4].ToString());

                //通过invoice_No获取调拨单主表ID，这样将主表ID插入从表，使主从表有关联
                int Exchange_header_id=exchange_headerDC.getExchange_header_idByinvoice_no(datarow.ItemArray[5].ToString());
                //将数据插入调拨单从表
                Boolean flag_line = exchange_lineDC.insertExchange_line(Exchange_header_id, datarow.ItemArray[0].ToString(), int.Parse(datarow.ItemArray[1].ToString()), DateTime.Now, datarow.ItemArray[4].ToString(), datarow.ItemArray[6].ToString());
                
                //若插入失败时，则返回false
                if (flag_header == false || flag_line == false)
                    return false;
            }
            return true;
        }


        //调拨单操作页面对应commit操作，将传进的参数分别插入到调拨单主表和从表中去
        public Boolean CommitAction(string invoice_no, string item_name, int required_qty)
        {
            Exchange_headerDC exchange_headerDC = new Exchange_headerDC();
            Exchange_lineDC exchange_lineDC = new Exchange_lineDC();

            //将数据插入调拨单主表
            Boolean flag_header = exchange_headerDC.insertExchange_header(invoice_no);

            //通过invoice_No获取调拨单主表ID，这样将主表ID插入从表，使主从表有关联
            int Exchange_header_id = exchange_headerDC.getExchange_header_idByinvoice_no(invoice_no);
            //将数据插入调拨单从表
            Boolean flag_line = exchange_lineDC.insertExchange_line(Exchange_header_id, item_name, required_qty);

            //若插入失败时，则返回false
            if (flag_header == false || flag_line == false)
                return false;
            else
                return true;
        }


        //通过调拨单号INVOICE_NO，查询调拨单主表信息
        public List<ModelExchange_header> getExchange_headerByINVOICE_NO(string invoice_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_exchange_header where INVOICE_NO = @invoice_no";

            SqlParameter[] parameters = {
                new SqlParameter("INVOICE_NO", invoice_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelExchange_header> ModelExchange_header_list = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                ModelExchange_header_list = new List<ModelExchange_header>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    ModelExchange_header_list.Add(toModel(datarow));
                }
            }
            return ModelExchange_header_list;
        }



        /**作者：周雅雯 修改时间：2016/9/11
         * 调拨单页面对应的Finish操作，Finish操作包括Commit提交和Debit扣账
         * Commit提交：
         * 将数据插入到调拨单主表和调拨单从表
         * Debit扣账：
         * 扣除庫存总表的庫存，扣除庫存明細表中对应仓库的庫存量，增加調入倉的庫存量、可用量，将信息插入到交易表中
         */
        public Boolean FinishAction(string invoice_no, int out_subinventory, int in_subinventory, int in_locator_id, int out_locator_id, string exchange_wo_no, string item_name, int operation_seq_num, int required_qty, int exchanged_qty, string remark)

        {
             Exchange_headerDC exchange_headerDC = new Exchange_headerDC();
            List<ModelExchange_header> list=new List<ModelExchange_header>();
            DateTime transaction_time = DateTime.Now;
            string sql=null;
            list = exchange_headerDC.getExchange_headerByINVOICE_NO(invoice_no);
            if(list==null){
            sql =
                //Commit提交
                //+ "--将数据插入到调拨单主表"
                         "insert into wms_exchange_header "
                        + "(invoice_no,out_subinventory_key,in_subinventory_key,out_locator_id,in_locator_id,status,exchange_wo_no)values "
                        + "(@invoice_no,@out_subinventory,@in_subinventory,@out_locator_id,@in_locator_id,'N',@exchange_wo_no); "

            //+ "--将数据插入到调拨单从表"
            +"insert into wms_exchange_line "
            + "(exchange_header_id,item_name,required_qty,operation_seq_num,exchange_wo_no,remark,exchanged_qty)values "
            + "((select exchange_header_id from wms_exchange_header where invoice_no=@invoice_no ),@item_name,@required_qty,@operation_seq_num,@exchange_wo_no,@remark,@exchanged_qty); "

            //debit扣账
            //+ "--减少庫存总表的庫存(调出仓）"
            +"update wms_items_onhand_qty_detail set onhand_quantiy=onhand_quantiy - @required_qty  where subinventory=@out_subinventory and item_id = (select item_id from wms_pn where item_name=@item_name);"

            //+ "--减少庫存明細表中对应仓库的庫存量(调出仓）"
            + "update wms_material_io set onhand_qty=onhand_qty - @required_qty where subinventory=(select subinventory_name from wms_subinventory where subinventory_key=@out_subinventory) and item_id = (select item_id from wms_pn where item_name=@item_name) and frame_key=@out_locator_id;"

            //+ "--增加調入倉的庫存量、可用量（库存总表）"
            + "update wms_items_onhand_qty_detail set onhand_quantiy=onhand_quantiy - @required_qty  where subinventory=@in_subinventory and item_id = (select item_id from wms_pn where item_name=@item_name);"

            //+ "--增加調入倉的庫存量、可用量（库存明细表）"
            + "update wms_material_io set onhand_qty=onhand_qty - @required_qty where subinventory=(select subinventory_name from wms_subinventory where subinventory_key=@in_subinventory) and item_id = (select item_id from wms_pn where item_name=@item_name) and frame_key=@in_locator_id;"

            //+ "--将数据插入到交易表"
            + "insert into wms_transaction_operation (transaction_qty,transaction_type,transaction_time) values (@required_qty,'exchange ',@transaction_time)"; 
        }
        else
       {
      sql =

            //+ "--将数据插入到调拨单从表"
             "insert into wms_exchange_line "
            + "(exchange_header_id,item_name,required_qty,operation_seq_num,exchange_wo_no,remark,exchanged_qty)values "
            + "((select exchange_header_id from wms_exchange_header where invoice_no=@invoice_no ),@item_name,@required_qty,@operation_seq_num,@exchange_wo_no,@remark,@exchanged_qty); "

            //debit扣账
            //+ "--减少庫存总表的庫存(调出仓）"
            +"update wms_items_onhand_qty_detail set onhand_quantiy=onhand_quantiy - @required_qty  where subinventory=@out_subinventory and item_id = (select item_id from wms_pn where item_name=@item_name);"

            //+ "--减少庫存明細表中对应仓库的庫存量(调出仓）"
            + "update wms_material_io set onhand_qty=onhand_qty - @required_qty where subinventory=(select subinventory_name from wms_subinventory where subinventory_key=@out_subinventory) and item_id = (select item_id from wms_pn where item_name=@item_name) and frame_key=@out_locator_id;"

            //+ "--增加調入倉的庫存量、可用量（库存总表）"
            + "update wms_items_onhand_qty_detail set onhand_quantiy=onhand_quantiy - @required_qty  where subinventory=@in_subinventory and item_id = (select item_id from wms_pn where item_name=@item_name);"

            //+ "--增加調入倉的庫存量、可用量（库存明细表）"
            + "update wms_material_io set onhand_qty=onhand_qty - @required_qty where subinventory=(select subinventory_name from wms_subinventory where subinventory_key=@in_subinventory) and item_id = (select item_id from wms_pn where item_name=@item_name) and frame_key=@in_locator_id;"

            //+ "--将数据插入到交易表"
            + "insert into wms_transaction_operation (transaction_qty,transaction_type,transaction_time) values (@required_qty,'exchange ',@transaction_time)";
            
    }

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no", invoice_no),
                new SqlParameter("out_subinventory", out_subinventory),
                new SqlParameter("in_subinventory", in_subinventory),
                
                new SqlParameter("in_locator_id", in_locator_id),
                new SqlParameter("out_locator_id", out_locator_id),
                new SqlParameter("exchange_wo_no", exchange_wo_no),
                new SqlParameter("item_name", item_name),
                new SqlParameter("operation_seq_num", operation_seq_num),
                new SqlParameter("required_qty", required_qty),
                new SqlParameter("exchanged_qty", exchanged_qty),
                new SqlParameter("remark", remark),
                new SqlParameter("transaction_time", transaction_time)
                };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.tran(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //调拨单页面对应Finish操作,对应调拨单动态添加时使用的方法
        public Boolean FinishAction(DataTable dataTable)
        {
            //对dataTable中的每行数据进行操作
            foreach (DataRow datarow in dataTable.Rows)
            {
                Exchange_headerDC exchange_headerDC = new Exchange_headerDC();
                Boolean flag = false;

                //调拨单页面对应的Finish操作，Finish操作包括Commit提交和Debit扣账
                flag = exchange_headerDC.FinishAction(datarow.ItemArray[0].ToString(), int.Parse(datarow.ItemArray[1].ToString()), int.Parse(datarow.ItemArray[2].ToString()), int.Parse(datarow.ItemArray[3].ToString()), int.Parse(datarow.ItemArray[4].ToString()), datarow.ItemArray[5].ToString(), datarow.ItemArray[6].ToString(), int.Parse(datarow.ItemArray[7].ToString()), int.Parse(datarow.ItemArray[8].ToString()), int.Parse(datarow.ItemArray[9].ToString()), datarow.ItemArray[10].ToString());
                //操作失败时，则返回false
                if (flag == false )
                    return false;
            }
            return true;
        }



        // 传入DataRow,将其转换为ModelExchange_header
        private ModelExchange_header toModel(DataRow dr)
        {
            ModelExchange_header model = new ModelExchange_header();

            //通过循环为ModelExchange_header赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelExchange_header).GetProperties())
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