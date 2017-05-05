using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;
using System.Collections;
using System.Configuration;


namespace WMS_v1._0.DataCenter
{
    //单据作业的DC
    public class InvoiceDC
    {



        /* 退料单申请
         * 获取所有的制程
         */
        public DataSet getRoute()
        {
            string sql = "select route,route_id from wms_wip_operations ";

            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }
        /* 领料单申请
         * 获取制程id
         */
        public DataSet getRoute(string route)
        {
            string sql = "select route_id from wms_wip_operations where route=@route";

            SqlParameter[] parameters = {
                 new SqlParameter("route",route)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 领料单申请
         * 导入缺料明细 
         * 料号有要求（只带出已发料的工单对应的料号）
         * 分两种情况
         * 1、模拟表中有模拟到的，料号就要满足 需求量-发料量-备料量>0  （模拟表中的需求量，发料量，备料量）
         * 2、在模拟表中没有模拟到的料号，料号是可以在这边需求量-发料量>0（工单物料需求表中的需求量，发料量）
         *
         * 然后从工单需求表中带出满足要求的料号相关信息。领料量为（需求量-发料量），需求量得>0
         */
        public DataSet getShort_detail(string wo_no)
        {
             //检测该工单是否被模拟过
            string sql1 = "select wo_no from wms_simulate_operation where wo_no=@wo_no ";

            SqlParameter[] parameters1 = {
                 new SqlParameter("wo_no",wo_no)
            };
            DB.connect();
            DataSet ds1 = DB.select(sql1, parameters1);
          
            //第一种情况：模拟表中模拟到的工单
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                string sql2 = "select item_name,wo_no,required_qty,simulated_qty=wms_requirement_operation.issued_qty,issued_qty=(required_qty-wms_requirement_operation.issued_qty)   from wms_requirement_operation where wo_no=@wo_no and required_qty-wms_requirement_operation.issued_qty>0 and item_name in(select  item_name=(select item_name from wms_pn where item_id=b.item_id) from wms_simulate_operation as b where wo_no=@wo_no and requirement_qty-(select  sum(pickup_qty) from wms_simulate_operation where wo_no=@wo_no and item_id=b.item_id  group by item_id)-(select  sum(issued_qty) from wms_simulate_operation where wo_no=@wo_no and item_id=b.item_id group by item_id)>0 group by item_id )";

                SqlParameter[] parameters = {
                 new SqlParameter("wo_no",wo_no)
            };
                DB.connect();
                DataSet ds2 = DB.select(sql2, parameters);

                if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                    return ds2;
                else
                    return null;
            }

            //第二种情况：在模拟表中没有模拟到的料号
            else
            {
                string sql3 = "select item_name,wo_no,required_qty,simulated_qty=wms_requirement_operation.issued_qty,issued_qty=(required_qty-wms_requirement_operation.issued_qty)   from wms_requirement_operation where wo_no=@wo_no and required_qty-wms_requirement_operation.issued_qty>0 ";

                SqlParameter[] parameters3 = {
                 new SqlParameter("wo_no",wo_no)
            };
                DB.connect();
                DataSet ds3 = DB.select(sql3, parameters3);

                if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
                    return ds3;
                else
                    return null;
            }

        }


        /* 领料单申请
         * 获取库存总表
         */
        public DataSet getItems_onhand_qty_detail(string subinventory, string item_name)
        {
            string sql = "select onhand_quantiy from wms_items_onhand_qty_detail where subinventory=@subinventory and item_name=@item_name";

            SqlParameter[] parameters = {
                 new SqlParameter("subinventory",subinventory),
                 new SqlParameter("item_name",item_name)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 领料单申请
         * 获取库存总表
         */
        public DataSet getItems_onhand_qty_detail(int subinventory_key, string item_name)
        {
            string sql = "select onhand_quantiy from wms_items_onhand_qty_detail where subinventory=(select subinventory_name from wms_subinventory where subinventory_key=@subinventory_key) and item_name=@item_name";

            SqlParameter[] parameters = {
                 new SqlParameter("subinventory_key",subinventory_key),
                 new SqlParameter("item_name",item_name)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }
        /* 领料单申请
         * 获取库存明细表
         */
        public DataSet getMaterial_io(string item_name, string frame_name)
        {
            string sql = "select left_qty from wms_material_io where item_id=(select item_id from wms_pn where item_name=@item_name) and frame_key=(select frame_key from wms_frame where frame_name=@frame_name)";

            SqlParameter[] parameters = {
                 new SqlParameter("frame_name",frame_name),
                 new SqlParameter("item_name",item_name)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 退料单申请
         * 获取所有库别（可用的！）
         */
        public DataSet getSubinventory_name()
        {
            string sql = "select subinventory_name,subinventory_key from wms_subinventory where enabled='Y' or enabled='1' ";

            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /* 调拨单申请
         * 获取库存总表中的库别
         */
        public DataSet getSubinventory()
        {
            string sql = "select subinventory,subinventory_key=(select subinventory_key from wms_subinventory where subinventory_name=wms_items_onhand_qty_detail.subinventory) from wms_items_onhand_qty_detail ";

            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }


        /* 领料单申请
         * 从工单需求表中带出所有工单
         */
        public DataSet getIssueWo_no()
        {
            string sql = "select distinct wo_no from wms_requirement_operation  ";

            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 退料单申请
         * 获取所有工单
         */
        public DataSet getWo_no()
        {
            string sql = "select wo_no,wo_key from wms_wo  ";

            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 领料单申请
         * 获取领料单单身中的领料量
         */
        public int getIssued_qty(string wo_no, string item_name)
        {
            string sql = "select issued_qty from wms_issue_line where item_name=@item_name and wo_no=@wo_no  ";

            int qty = 0;

            SqlParameter[] parameters = {
                new SqlParameter("wo_no", wo_no),
                new SqlParameter("item_name", item_name),
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    qty += int.Parse(dr["issued_qty"].ToString());
                }
                return qty;
            }
            else
                return qty;
        }

        /* 退料单申请
         * 获取退料单单身中的退料量
         */
        public int getReturn_qty(string return_wo_no, string item_name)
        {
            string sql = "select return_qty from wms_return_line where item_name=@item_name and return_wo_no=@return_wo_no  ";

            int qty = 0;

            SqlParameter[] parameters = {
                new SqlParameter("return_wo_no", return_wo_no),
                new SqlParameter("item_name", item_name),
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    qty += int.Parse(dr["return_qty"].ToString());
                }
                return qty;
            }
            else
                return qty;
        }



        /* 退料单申请
         * 获取所有的部门编号(可用的)
         */
        public DataSet getFlex_value()
        {
            string sql = "select flex_value from wms_account_flex where enabled='Y' or enabled='1' ";

            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 退料单申请
         * 获取库别key
         */
        public DataSet getSubinventory_Key(string subinventory_name)
        {
            string sql = "select subinventory_key from wms_subinventory where subinventory_name=@subinventory_name ";

            SqlParameter[] parameters = {
                 new SqlParameter("subinventory_name",subinventory_name)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }
        /* 退料单申请
         * 部门编号与部门名称二级联动(可用的)
         */
        public DataSet getDescriptionByFlex_value(string flex_value)
        {
            string sql = "select description from wms_account_flex where flex_value=@flex_value and ( enabled='Y' or enabled='1') ";

            SqlParameter[] parameters = {
                 new SqlParameter("flex_value",flex_value)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 领料单申请
         * 领料工单号与料号的二级联动（这个地方只带出已发料的工单对应的料号）
         * 分两种情况
         * 1、模拟表中有模拟到的，就要满足 需求量-发料量-备料量>0  就可以领料（模拟表中的需求量，发料量，备料量）
         * 2、在模拟表中没有模拟到的料号，是可以在这边需求量-发料量>0就可以（工单物料需求表中的需求量，发料量）
         */
        public DataSet getItem_nameByIssue_wo_no(string issue_wo_no)
        {
            //检测该工单是否被模拟过
            string sql1 = "select wo_no from wms_simulate_operation where wo_no=@issue_wo_no ";

            SqlParameter[] parameters1 = {
                 new SqlParameter("issue_wo_no",issue_wo_no)
            };
            DB.connect();
            DataSet ds1 = DB.select(sql1, parameters1);
          
            //第一种情况：模拟表中模拟到的工单
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                //将一个工单中的料号，备料量和发料量查询出来。因为一个料号可以模拟多次，所以这个地方要用group by item_id 还要取备料量和发料量的总结
                string sql2 = "select  item_id,item_name=(select item_name from wms_pn where item_id=b.item_id) from wms_simulate_operation as b where wo_no=@issue_wo_no and requirement_qty-(select  sum(pickup_qty) from wms_simulate_operation where wo_no=@issue_wo_no and item_id=b.item_id  group by item_id)-(select  sum(issued_qty) from wms_simulate_operation where wo_no=@issue_wo_no and item_id=b.item_id group by item_id)>0 group by item_id ";

                SqlParameter[] parameters2 = {
                 new SqlParameter("issue_wo_no",issue_wo_no)
            };
                DB.connect();
                DataSet ds2 = DB.select(sql2, parameters2);

                if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                    return ds2;
                else
                    return null;
            }
            //第二种情况：在模拟表中没有模拟到的料号
            else
            {
                string sql3 = "select item_name from wms_requirement_operation where wo_no=@issue_wo_no and required_qty-issued_qty>0  ";

                SqlParameter[] parameters3 = {
                 new SqlParameter("issue_wo_no",issue_wo_no)
            };
                DB.connect();
                DataSet ds3 = DB.select(sql3, parameters3);

                if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
                    return ds3;
                else
                    return null;
            }

        }

        /* 退料单申请
         * 退料单号与料号的二级联动
         * 从工单物料需求表中带出小料号
         */
        public DataSet getItem_nameByReturn_wo_no(string return_wo_no)
        {
            string sql = "select item_name from wms_requirement_operation where wo_no=@return_wo_no  ";

            SqlParameter[] parameters = {
                 new SqlParameter("return_wo_no",return_wo_no)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }






        /* 退料单申请
         * 生成退料单号
         */
        public DataSet getTheNewestReturnHeaderByCreatetime()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT TOP 1.*  FROM wms_return_header ORDER BY create_time DESC";

            SqlParameter[] parameters = null;


            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /* 退料单申请
         * 向退料主表中插入部分数据 
         * 初始总表退料状态为0（退料中），单身全部扣账完毕，才会改变主表的状态
         */
        public Boolean insertReturn_header(string invoice_no, string return_type, string flex_value, string description, string remark)
        {
            string sql = "insert into wms_return_header "
                       + "(invoice_no,return_type,flex_value,description,remark,status)values "
                       + "(@invoice_no,@return_type,@flex_value,@description,@remark,0)";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no",invoice_no),
                new SqlParameter("return_type",return_type),
                new SqlParameter("flex_value",flex_value),
                new SqlParameter("description",description),             
                new SqlParameter("remark",remark),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        /* 退料单申请
         * 以退料单主表的退料单号生成从表的Line_num
         */
        public int getLine_numByInvoice_no(string invoice_no)
        {
            string sql = "select line_num from wms_return_line where return_header_id = (select return_header_id from wms_return_header where invoice_no=@invoice_no)  ";

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("invoice_no", invoice_no)
                };
            DataSet ds = DB.select(sql, parameters);

            //当数据库中该退料主表下面有从表时，Line_num为上一个值+1
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                sql = "select TOP 1 line_num from wms_return_line where return_header_id = (select return_header_id from wms_return_header where invoice_no=@invoice_no) order by line_num DESC ";

                SqlParameter[] parameters2 = {
                    new SqlParameter("invoice_no", invoice_no)
                };
                DataSet ds2 = DB.select(sql, parameters);

                if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                    return (int.Parse(ds2.Tables[0].Rows[0]["line_num"].ToString()) + 1);
                else
                    return -1;
            }
            //当数据库中该退料主表下面没有从表时，即这条为第一个从表信息，Line_num为1
            else
            {
                return 1;
            }
        }


        /* 退料单申请
         * 根据条件获取退料单身
         */
        public DataSet getReturnLineBySome(string invoice_no, string return_wo_no, int operation_seq_num, string item_name, int return_qty, int return_sub_key)
        {
            string sql = "select * from wms_return_line where return_header_id=(select return_header_id from wms_return_header where invoice_no=@invoice_no) and return_wo_no=@return_wo_no and operation_seq_num=@operation_seq_num and item_name=@item_name and return_qty=@return_qty and return_sub_key=@return_sub_key ";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no",invoice_no),
                new SqlParameter("return_wo_no",return_wo_no),
                new SqlParameter("operation_seq_num",operation_seq_num),
                new SqlParameter("item_name",item_name),
                new SqlParameter("return_qty",return_qty),
                new SqlParameter("return_sub_key",return_sub_key),
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }


        /* 退料单申请(对应提交按钮)
         * 向退料明细表中插入单条数据
         * 初始从表退料状态为0（退料中），扣账完毕，才会改变从表的状态
         */
        public Boolean insertReturn_line(string invoice_no, int line_num, string return_wo_no, int operation_seq_num, string item_name, int return_qty, int return_sub_key, string create_man, DateTime create_time)
        {
            string sql;

            sql = "insert into wms_return_line"
                   + "(return_header_id,line_num,return_wo_no,operation_seq_num,item_name,return_qty,return_sub_key,create_man,create_time,flag)values"
                   + "((select return_header_id from wms_return_header where invoice_no=@invoice_no),@line_num,@return_wo_no,@operation_seq_num,@item_name,@return_qty,@return_sub_key,@create_man,@create_time,0) ";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no",invoice_no),
                new SqlParameter("line_num",line_num),
                new SqlParameter("return_wo_no",return_wo_no),
                new SqlParameter("operation_seq_num",operation_seq_num),
                new SqlParameter("item_name",item_name),
                new SqlParameter("return_qty",return_qty),
                new SqlParameter("return_sub_key",return_sub_key),              
                new SqlParameter("create_man",create_man),
                new SqlParameter("create_time",create_time),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /* 退料单申请（对应commit按钮）
         * 向退料明细表中插入多条数据
         */
        public Boolean insertReturn_line(DataTable dataTable, string invoice_no)
        {
            //将dataTable中的每行数据插入数据库中
            foreach (DataRow datarow in dataTable.Rows)
            {
                //将每行数据插入数据库中
                InvoiceDC invoiceDC = new InvoiceDC();

                bool flag = invoiceDC.insertReturn_line(invoice_no, int.Parse(datarow["line_num"].ToString()), datarow["return_wo_no"].ToString(), int.Parse(datarow["operation_seq_num"].ToString()), datarow["item_name"].ToString(), int.Parse(datarow["return_qty"].ToString()), int.Parse(datarow["return_sub_key"].ToString()), datarow["create_man"].ToString(), Convert.ToDateTime(datarow["create_time"].ToString()));
                //若插入失败时，则返回false
                if (flag == false)
                    return false;
            }
            return true;
        }

        /* 领料单查询0,退料单查询1，调拨单查询2
         * 获取单身中里对应的料号
         */
        public DataSet getItem_name(int status)
        {
            string sql;

            if (status == 0)
                sql = "select item_name from wms_issue_line ";
            else if (status == 1)
                sql = "select item_name from wms_return_line ";
            else
                sql = "select item_name from wms_exchange_line ";

            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 退料单查询
         * 获取所有的退料单据号
         */
        public DataSet getInvoice_no(int status)
        {
            //status为1时，获取所有的退料单据号。status为0时，获取没扣账的单据号
            string sql;

            if (status == 1)
                sql = "select invoice_no from wms_return_header ";
            else
                sql = "select invoice_no from wms_return_header where status=0 ";

            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }



        /* 退料单查询
         * 返回退料单头  对单头的时间不做考虑（因为详细信息都存在单身里，没必要把单头也带出来）
         */
        public DataSet getReturnHeaderBySome(string invoice_no, string return_type, string item_name)
        {
            //完整查询内容
            string sqlAll = "";
            //查询条件
            string sqlTail = "";

            //当invoice_no有值时
            if (string.IsNullOrWhiteSpace(invoice_no) == false)
            {
                sqlTail += "AND invoice_no=@invoice_no ";
            }
            //当return_type有值时
            if (string.IsNullOrWhiteSpace(return_type) == false)
            {
                sqlTail += "AND return_type =@return_type ";
            }
            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND  return_header_id=(select return_header_id from wms_return_line where item_name =@item_name) ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "select * from wms_return_header  ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "select * from wms_return_header WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {    
                    new SqlParameter("invoice_no", invoice_no),
                    new SqlParameter("return_type", return_type),
                    new SqlParameter("item_name", item_name),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }


        /* 退料单查询
         * 返回退料单身
         */
        public DataSet getReturnLineBySome(string invoice_no, string item_name, string return_type, DateTime start_time, DateTime end_time)
        {
            //完整查询内容
            string sqlAll = "";
            //查询条件
            string sqlTail = "";

            //对start_time和end_time做处理，默认他们两个都有值
            sqlTail += "AND  create_time BETWEEN @start_time AND @end_time ";

            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name =@item_name ";
            }
            //当invoice_no有值时
            if (string.IsNullOrWhiteSpace(invoice_no) == false)
            {
                sqlTail += "AND return_header_id=(select return_header_id from wms_return_header where invoice_no=@invoice_no) ";
            }
            //当return_type有值时
            if (string.IsNullOrWhiteSpace(return_type) == false)
            {
                sqlTail += "AND return_header_id=(select return_header_id from wms_return_header where return_type=@return_type) ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "select *,operation_seq_num_name=(select route from wms_wip_operations where route_id=operation_seq_num),return_sub_name=(select subinventory_name from wms_subinventory where subinventory_key=return_sub_key) from wms_return_line   ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "select *,operation_seq_num_name=(select route from wms_wip_operations where route_id=operation_seq_num),return_sub_name=(select subinventory_name from wms_subinventory where subinventory_key=return_sub_key) from wms_return_line WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("start_time", start_time),
                    new SqlParameter("end_time", end_time),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("invoice_no", invoice_no),
                    new SqlParameter("return_type", return_type),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }


        ///* 退料单作业
        // * 获取库别对应的料架   在扣账页面本来想更精确的调出料架，但是如何在js中触发ajax？
        // */
        //public DataSet getFrameBySub(string subinventory)
        //{
        //    //string sql = "select frame_name,frame_key from wms_frame where enabled='1' or enabled='Y' ";
        //    string sql = "select * from WMS_frame where region_key=(select region_key from WMS_region where subinventory=(select subinventory_key from wms_subinventory where subinventory_name=@subinventory))";

        //    SqlParameter[] parameters = {
        //        new SqlParameter("subinventory", subinventory),
        //        };

        //    DB.connect();
        //    DataSet ds = DB.select(sql, parameters);

        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        return ds;
        //    else
        //        return null;

        //}

        /* 领料单作业
         * 从库存明细表中获取该料号存在的所有料架
         */
        public DataSet getFrameByItem_name(string item_name)
        {
            string sql = "select frame_name=(select frame_name from wms_frame where frame_key=wms_material_io.frame_key),frame_key from wms_material_io where item_id=(select item_id from wms_pn where item_name=@item_name) group by frame_key ";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),              
                };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /* 退料单作业
         * 存不存在这样的料号
         */
        public DataSet getItem_name(string item_name)
        {
            string sql = "select item_name from wms_pn where item_name=@item_name";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),              
                };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /* 退料单作业
         * 存不存在这样的料架
         */
        public DataSet getFrame_name(string frame_name)
        {
            string sql = "select frame_name from wms_frame where frame_name=@frame_name";

            SqlParameter[] parameters = {
                new SqlParameter("frame_name", frame_name),              
                };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }


        /* 退料单作业
         * Debit（扣账）操作
         */
        public int DebitAction(DateTime update_time, string update_man, int return_line_id, int return_qty, string subinventory, string frame_key, string item_name, string datecode, int status)
        {
            string sql;

            //检测存不存在这样的库存明细，存在是更新，不存在是插入
            InvoiceDC invoiceDC = new InvoiceDC();

            if (invoiceDC.getMaterial_ioBySome(item_name, frame_key, datecode) == null)
                sql =//+ "-增加庫存总表的庫存" 2
                    "insert into wms_items_onhand_qty_detail (item_name,item_id,onhand_quantiy,subinventory) values (@item_name,(select item_id from wms_pn where item_name=@item_name),@return_qty,@subinventory); "
                    //+ "--增加庫存明細表中对应仓库的庫存量 3
                    + "insert into wms_material_io (item_id,frame_key,datecode,onhand_qty,simulated_qty,create_time,return_flag) values ((select item_id from wms_pn where item_name=@item_name),(select frame_key from wms_frame where frame_name=@frame_key),@datecode,@return_qty,0,getdate(),'Y'); ";
            else
                sql = //+ "--增加庫存总表的庫存" 2
            "UPDATE wms_items_onhand_qty_detail SET onhand_quantiy=onhand_quantiy + @return_qty,update_time=@update_time  WHERE subinventory=@subinventory AND item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name); "
                    //+ "--增加庫存明細表中对应仓库的庫存量" 3
            + "UPDATE wms_material_io SET onhand_qty=onhand_qty + @return_qty,update_time=@update_time,return_flag='Y' WHERE frame_key=(select frame_key from wms_frame where frame_name=@frame_key) AND item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name) AND datecode=@datecode;  ";

            sql +=
                //更新退料明细表 4
               "UPDATE wms_return_line SET flag=1,update_time=@update_time,update_man=@update_man,frame_key=@frame_key  WHERE return_line_id=@return_line_id; "
                //+ "--将数据插入到交易表" 5
           + "INSERT INTO wms_transaction_operation (invoice_no,item_name,transaction_qty,transaction_type,transaction_time,create_user) VALUES ((select invoice_no from wms_return_header where return_header_id=(select return_header_id from wms_return_line where return_line_id=@return_line_id)),@item_name,@return_qty,'Return_Debit',@update_time,@update_man) ";


            //只有工单退料时才需要更新工单物料需求表（因为非工单退料时没工单号）
            if (status == 1)
                sql +=
                    //更新工单物料需求表 6
            "; UPDATE wms_requirement_operation set return_invoice_qty=return_invoice_qty+@return_qty where wo_no=(select wo_no from wms_return_line where return_line_id=@return_line_id) and item_name=@item_name ";


            SqlParameter[] parameters = {
                new SqlParameter("update_time", update_time),
                new SqlParameter("update_man", update_man),
                new SqlParameter("return_line_id", return_line_id),
                new SqlParameter("return_qty", return_qty),
                new SqlParameter("subinventory", subinventory),
                new SqlParameter("frame_key", frame_key),
                new SqlParameter("item_name", item_name),
                new SqlParameter("datecode", datecode),
                    
                };

            DB.connect();

            //返回受影响行数InfluenceNum
            int result = InvoiceDC.tran(sql, parameters);

            return result;  //返回1的时候是事务执行成功
        }


        /* 退料单申请
         * 生成领料单号
         */
        public DataSet getTheNewestIssueHeaderByCreatetime()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT TOP 1.*  FROM wms_mtl_issue_header ORDER BY create_time DESC";

            SqlParameter[] parameters = null;


            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /* 领料单申请
         * 料架与库别的二级联动
         */
        public DataSet getSubinventoryByFrame(string frame)
        {
            string sql = "select subinventory_key,subinventory_name from wms_subinventory where subinventory_key=(select subinventory from wms_region where region_key=(select region_key from wms_frame where frame_name=@frame)) ";

            SqlParameter[] parameters = {
                 new SqlParameter("frame",frame)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 领料单申请
         * 料号与库别的二级联动，从库存总表中带出
         */
        public DataSet getSubinventoryByItem_name(string item_name)
        {
            string sql = "select DISTINCT subinventory,subinventory_key=(select subinventory_key from wms_subinventory where subinventory_name=wms_items_onhand_qty_detail.subinventory) from wms_items_onhand_qty_detail where item_name=@item_name ";

            SqlParameter[] parameters = {
                 new SqlParameter("item_name",item_name)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 领料单申请
         * 获取需求量，发料量   从工单物料需求表中带出
         */
        public DataSet getRequirement(string wo_no, string item_name)
        {
            string sql = "select required_qty,issued_qty,issue_invoice_qty from wms_requirement_operation where wo_no=@wo_no and item_name=@item_name ";

            SqlParameter[] parameters ={
                 new SqlParameter("wo_no",wo_no),
                 new SqlParameter("item_name",item_name),
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 领料单申请
         * 获取模拟量，从模拟表中带出
         */
        public DataSet getSimulate(string wo_no)
        {
            string sql = "select * from wms_simulate_operation where wo_no=@wo_no  ";

            SqlParameter[] parameters ={
                 new SqlParameter("wo_no",wo_no),
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 领料单申请
         * 向领料主表中插入部分数据 
         * 初始总表领料状态为0（领料中），单身全部扣账完毕，才会改变主表的状态
         */
        public Boolean insertIssue_header(string invoice_no, string issue_type, string flex_value, string description, string remark)
        {
            string sql = "insert into wms_mtl_issue_header "
                       + "(invoice_no,issue_type,flex_value,description,remark,status)values "
                       + "(@invoice_no,@issue_type,@flex_value,@description,@remark,'N ')";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no",invoice_no),
                new SqlParameter("issue_type",issue_type),
                new SqlParameter("flex_value",flex_value),
                new SqlParameter("description",description),             
                new SqlParameter("remark",remark),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /* 领料单申请
         * 以领料单主表的领料单号生成从表的Line_num
         */
        public int getIssueLine_numByInvoice_no(string invoice_no)
        {
            string sql = "select line_num from wms_issue_line where issue_header_id = (select issue_header_id from wms_mtl_issue_header where invoice_no=@invoice_no)  ";

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("invoice_no", invoice_no)
                };
            DataSet ds = DB.select(sql, parameters);

            //当数据库中该领料主表下面有从表时，Line_num为上一个值+1
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                sql = "select TOP 1 line_num from wms_issue_line where issue_header_id = (select issue_header_id from wms_mtl_issue_header where invoice_no=@invoice_no) order by line_num DESC ";

                SqlParameter[] parameters2 = {
                    new SqlParameter("invoice_no", invoice_no)
                };
                DataSet ds2 = DB.select(sql, parameters);

                if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                    return (int.Parse(ds2.Tables[0].Rows[0]["line_num"].ToString()) + 1);
                else
                    return -1;
            }
            //当数据库中该领料主表下面没有从表时，即这条为第一个从表信息，Line_num为1
            else
            {
                return 1;
            }
        }

        /* 领料单申请
         * 根据条件获取退料单身
         */
        public DataSet getIssueLineBySome(string invoice_no, string wo_no, int line_num, string item_name, int peration_seq_num, int issued_qty, int simulated_qty, int required_qty, string frame_key, string issued_sub)
        {
            string sql = "select * from wms_issue_line where issue_header_id=(select issue_header_id from wms_mtl_issue_header where invoice_no=@invoice_no) and wo_no=@wo_no and line_num=@line_num and item_name=@item_name and peration_seq_num=@peration_seq_num and issued_qty=@issued_qty and simulated_qty=@simulated_qty and required_qty=@required_qty and frame_key=@frame_key and issued_sub=@issued_sub ";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no",invoice_no),
                new SqlParameter("wo_no",wo_no),
                new SqlParameter("line_num",line_num),
                new SqlParameter("item_name",item_name),
                new SqlParameter("peration_seq_num",peration_seq_num),
                new SqlParameter("issued_qty",issued_qty),
                new SqlParameter("simulated_qty",simulated_qty),
                new SqlParameter("required_qty",required_qty),
                new SqlParameter("frame_key",frame_key),
                new SqlParameter("issued_sub",issued_sub),
               
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /* 领料单申请
         * 根据条件获取退料单身
         */
        public DataSet getIssueLineBySome(string invoice_no, string wo_no)
        {
            string sql;
            
            if(wo_no=="")
                sql = "select issue_line_id from wms_issue_line where issue_header_id=(select issue_header_id from wms_mtl_issue_header where invoice_no=@invoice_no) ";
            else
                sql = "select issue_line_id from wms_issue_line where issue_header_id=(select issue_header_id from wms_mtl_issue_header where invoice_no=@invoice_no) and wo_no=@wo_no ";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no",invoice_no),
                new SqlParameter("wo_no",wo_no),            
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }


        /* 领料单申请(对应提交按钮)
         * 向领料明细表中插入单条数据
         * 初始从表领料状态为0（领料中），扣账完毕，才会改变从表的状态
         */
        public Boolean insertIssue_line(string invoice_no, int line_num, string wo_no, int peration_seq_num, string item_name, int required_qty, int simulated_qty, int issued_qty, string issued_sub, string create_man, DateTime create_time)
        {
            string sql;

            sql = "insert into wms_issue_line"
                   + "(issue_header_id,line_num,wo_no,peration_seq_num,item_name,required_qty,simulated_qty,issued_qty,issued_sub,create_man,create_time,status)values"
                   + "((select issue_header_id from wms_mtl_issue_header where invoice_no=@invoice_no),@line_num,@wo_no,@peration_seq_num,@item_name,@required_qty,@simulated_qty,@issued_qty,@issued_sub,@create_man,@create_time,'N') ";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no",invoice_no),
                new SqlParameter("line_num",line_num),
                new SqlParameter("wo_no",wo_no),
                new SqlParameter("peration_seq_num",peration_seq_num),
                new SqlParameter("item_name",item_name),
                new SqlParameter("required_qty",required_qty),
                new SqlParameter("simulated_qty",simulated_qty),
                new SqlParameter("issued_qty",issued_qty),
                new SqlParameter("issued_sub",issued_sub),              
                new SqlParameter("create_man",create_man),
                new SqlParameter("create_time",create_time),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        /* 领料单申请（对应commit按钮）
         * 向领料明细表中插入多条数据
         */
        public Boolean insertIssue_line(DataTable dataTable, string invoice_no)
        {
            //将dataTable中的每行数据插入数据库中
            foreach (DataRow datarow in dataTable.Rows)
            {
                //将每行数据插入数据库中
                InvoiceDC invoiceDC = new InvoiceDC();

                bool flag = invoiceDC.insertIssue_line(invoice_no, int.Parse(datarow["line_num"].ToString()), datarow["wo_no"].ToString(), int.Parse(datarow["peration_seq_num"].ToString()), datarow["item_name"].ToString(), int.Parse(datarow["required_qty"].ToString()), int.Parse(datarow["simulated_qty"].ToString()), int.Parse(datarow["issued_qty"].ToString()), datarow["issued_sub"].ToString(), datarow["create_man"].ToString(), DateTime.Now);
                //若插入失败时，则返回false
                if (flag == false)
                    return false;
            }
            return true;
        }

        /* 领料单查询
         * 获取领料单据号
         */
        public DataSet getIssueInvoice_no(int status)
        {
            //status为1时，获取所有的领料单据号。status为0时，获取没扣账的单据号
            string sql;

            if (status == 1)
                sql = "select invoice_no from wms_mtl_issue_header ";
            else
                sql = "select invoice_no from wms_mtl_issue_header where status='N' ";

            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 领料单查询
         * 返回领料单头  对单头的时间不做考虑（因为详细信息都存在单身里，没必要把单头也带出来）        
         */
        public DataSet getIssueHeaderBySome(string invoice_no, string issue_type, string item_name)
        {
            //完整查询内容
            string sqlAll = "";
            //查询条件
            string sqlTail = "";

            //当invoice_no有值时
            if (string.IsNullOrWhiteSpace(invoice_no) == false)
            {
                sqlTail += "AND invoice_no=@invoice_no ";
            }
            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND issue_header_id=(select issue_header_id from wms_issue_line where item_name=@item_name) ";
            }
            //当issue_type有值时
            if (string.IsNullOrWhiteSpace(issue_type) == false)
            {
                sqlTail += "AND issue_type =@issue_type ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "select * from wms_mtl_issue_header  ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "select * from wms_mtl_issue_header WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {    
                    new SqlParameter("invoice_no", invoice_no),
                    new SqlParameter("issue_type", issue_type),
                    new SqlParameter("item_name", item_name),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /* 领料单作业
         * 返回领料料单身
         */
        public DataSet getIssueLineBySome(string invoice_no, string item_name, string issue_type, DateTime start_time, DateTime end_time)
        {
            //完整查询内容
            string sqlAll = "";
            //查询条件
            string sqlTail = "";

            //对start_time和end_time做处理，默认他们两个都有值
            sqlTail += "AND  create_time BETWEEN @start_time AND @end_time ";

            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name =@item_name ";
            }
            //当invoice_no有值时
            if (string.IsNullOrWhiteSpace(invoice_no) == false)
            {
                sqlTail += "AND issue_header_id=(select issue_header_id from wms_mtl_issue_header where invoice_no=@invoice_no) ";
            }
            //当issue_type有值时
            if (string.IsNullOrWhiteSpace(issue_type) == false)
            {
                sqlTail += "AND issue_header_id=(select issue_header_id from wms_mtl_issue_header where issue_type=@issue_type) ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "select *,operation_seq_num_name=(select route from wms_wip_operations where route_id=peration_seq_num) from wms_issue_line   ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "select *,operation_seq_num_name=(select route from wms_wip_operations where route_id=peration_seq_num) from wms_issue_line WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("start_time", start_time),
                    new SqlParameter("end_time", end_time),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("invoice_no", invoice_no),
                    new SqlParameter("issue_type", issue_type),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /* 领料单作业
         * Debit（扣账）操作
         */
        public int IssueDebitAction(DateTime update_time, string update_man, int issue_line_id, int issued_qty, string issued_sub, string frame, string item_name, string datecode, int status)
        {
            string sql =
                //更新领料单明细表 2
                "UPDATE wms_issue_line SET status='Y',update_time=@update_time,update_man=@update_man,frame_key=@frame  WHERE issue_line_id=@issue_line_id; "

                //+ "--减少庫存总表的庫存" 3
            + "UPDATE wms_items_onhand_qty_detail SET onhand_quantiy=onhand_quantiy - @issued_qty,update_time=@update_time  WHERE subinventory=@issued_sub AND item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name)  ; "

            //+ "--减少庫存明細表中对应仓库的庫存量" 4
            + "UPDATE wms_material_io SET onhand_qty=onhand_qty - @issued_qty,update_time=@update_time WHERE frame_key=(select frame_key from wms_frame where frame_name=@frame) AND item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name) AND datecode=@datecode and left_qty- @issued_qty>=0 ;  "

            //+ "--将数据插入到交易表" 5
            + "INSERT INTO wms_transaction_operation (invoice_no,item_name,transaction_qty,transaction_type,transaction_time,create_user) VALUES ((select invoice_no from wms_mtl_issue_header where issue_header_id=(select issue_header_id from wms_issue_line where issue_line_id=@issue_line_id)),@item_name,@issued_qty,'Issue_Debit',@update_time,@update_man) ";


            //只有工单领料时才需要更新工单物料需求表（因为非工单领料时没工单号）
            if (status == 1)
                sql +=
                    //更新工单物料需求表 6
            "; UPDATE wms_requirement_operation set issue_invoice_qty=issue_invoice_qty+@issued_qty where wo_no=(select wo_no from wms_issue_line where issue_line_id=@issue_line_id) and item_name=@item_name ";


            SqlParameter[] parameters = {
                new SqlParameter("update_time", update_time),
                new SqlParameter("update_man", update_man),
                new SqlParameter("issue_line_id", issue_line_id),
                new SqlParameter("issued_qty", issued_qty),
                new SqlParameter("issued_sub", issued_sub),
                new SqlParameter("frame", frame),
                new SqlParameter("item_name", item_name),
                new SqlParameter("datecode", datecode),                 
                };

            DB.connect();

            //返回结果
            int result = InvoiceDC.tran(sql, parameters);

            //事务执行成功时，返回1。
            return result;
        }


        /* 调拨单作业
         * 从库存明细表中获取所有料架
         */
        public DataSet getFrameByMaterial_io()
        {
            string sql = "select frame_name=(select frame_name from wms_frame where frame_key=wms_material_io.frame_key),frame_key from wms_material_io group by frame_key ";

            SqlParameter[] parameters = null;


            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 调拨单作业
         * 从料架表中获取可用的料架
         */
        public DataSet getFrame()
        {
            string sql = "select frame_name,frame_key from wms_frame where enabled='Y' or enabled='1' ";

            SqlParameter[] parameters = null;


            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 调拨单申请
         * 调出库别与料号二级联动
         */
        public DataSet getItem_nameBySubinventory(int subinventory_key)
        {
            string sql = "select item_name,item_id from wms_items_onhand_qty_detail where subinventory=(select subinventory_name from wms_subinventory where subinventory_key=@subinventory_key)";

            SqlParameter[] parameters = {
                 new SqlParameter("subinventory_key",subinventory_key)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }


        /* 调拨单申请
         * 调出料架与料号二级联动  从库存明细表中获取料号
         */
        public DataSet getItem_nameByFrame(string frame_name)
        {
            string sql = "select item_name=(select item_name from wms_pn where item_id=wms_material_io.item_id),item_id from wms_material_io where frame_key=(select frame_key from wms_frame where frame_name=@frame_name)";

            SqlParameter[] parameters = {
                 new SqlParameter("frame_name",frame_name)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 调拨单申请
         * 生成调拨单号
         */
        public DataSet getTheNewestExchangeHeaderByCreatetime()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT TOP 1.*  FROM wms_exchange_header ORDER BY create_time DESC";

            SqlParameter[] parameters = null;


            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }


        /* 调拨单申请
         * 向领料主表中插入部分数据 
         */
        public Boolean insertExchange_header(string invoice_no, string flex_value, string description)
        {
            string sql = "insert into wms_exchange_header "
                       + "(invoice_no,flex_value,description)values "
                       + "(@invoice_no,@flex_value,@description)";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no",invoice_no),
                new SqlParameter("flex_value",flex_value),
                new SqlParameter("description",description),             
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /* 调拨单申请
         * 料架与料号确定库存明细表中的调拨上限
         */
        public DataSet getOnhandQty(string frame_name, string item_name)
        {
            string sql = "select onhand_qty from wms_material_io where frame_key=(select frame_key from wms_frame where frame_name=@frame_name) and item_id=(select item_id from wms_pn where item_name=@item_name) ";

            SqlParameter[] parameters = {
                 new SqlParameter("frame_name",frame_name),
                 new SqlParameter("item_name",item_name),
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 调拨单申请
         * 获取所有料号
         */
        public DataSet getPn()
        {
            string sql = "select item_name,item_id  from wms_pn ";

            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /* 调拨单申请(对应提交按钮)
         * 向调拨明细表中插入单条数据
         */
        public Boolean insertExchange_line(string invoice_no, int operation_seq_num, string item_name, int required_qty, int exchanged_qty, int out_subinventory, int in_subinventory, string create_man, DateTime create_time)
        {
            string sql;

            sql = "insert into wms_exchange_line"
                   + "(exchange_header_id,operation_seq_num,item_name,item_id,required_qty,exchanged_qty,out_subinventory,in_subinventory,create_man,create_time)values"
                   + "((select exchange_header_id from wms_exchange_header where invoice_no=@invoice_no),@operation_seq_num,@item_name,(select item_id from wms_pn where item_name=@item_name),@required_qty,@exchanged_qty,@out_subinventory,@in_subinventory,@create_man,@create_time) ";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no",invoice_no),
                new SqlParameter("operation_seq_num",operation_seq_num),
                new SqlParameter("item_name",item_name),
                new SqlParameter("required_qty",required_qty),
                new SqlParameter("exchanged_qty",exchanged_qty),
                new SqlParameter("out_subinventory",out_subinventory),
                new SqlParameter("in_subinventory",in_subinventory),   
                new SqlParameter("create_man",create_man),
                new SqlParameter("create_time",create_time),
                //new SqlParameter("out_frame_key",out_frame_key),
                //new SqlParameter("in_frame_key",in_frame_key),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /* 调拨单申请(对应commit按钮)
         * 向调拨明细表中插入多条数据
         */
        public Boolean insertExchange_line(DataTable dataTable, string invoice_no)
        {
            //将dataTable中的每行数据插入数据库中
            foreach (DataRow datarow in dataTable.Rows)
            {
                //将每行数据插入数据库中
                InvoiceDC invoiceDC = new InvoiceDC();

                bool flag = invoiceDC.insertExchange_line(invoice_no, int.Parse(datarow["operation_seq_num"].ToString()), datarow["item_name"].ToString(), int.Parse(datarow["required_qty"].ToString()), int.Parse(datarow["exchanged_qty"].ToString()), int.Parse(datarow["out_subinventory"].ToString()), int.Parse(datarow["in_subinventory"].ToString()), datarow["create_man"].ToString(), DateTime.Now);
                //若插入失败时，则返回false
                if (flag == false)
                    return false;
            }
            return true;
        }


        /* 调拨单查询
         * 获取所有的调拨单据号
         */
        public DataSet getExchangeInvoice_no()
        {
            string sql = "select invoice_no from wms_exchange_header ";

            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }

        /* 调拨单查询
         * 返回调拨单头  对单头的时间不做考虑（因为详细信息都存在单身里，没必要把单头也带出来）
         */
        public DataSet getExchangeHeaderBySome(string invoice_no, string item_name)
        {
            //完整查询内容
            string sqlAll = "";
            //查询条件
            string sqlTail = "";

            //当invoice_no有值时
            if (string.IsNullOrWhiteSpace(invoice_no) == false)
            {
                sqlTail += "AND invoice_no=@invoice_no ";
            }
            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND exchange_header_id=(select exchange_header_id from wms_exchange_line where item_name=@item_name) ";
            }


            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "select * from wms_exchange_header  ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "select * from wms_exchange_header WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {    
                    new SqlParameter("invoice_no", invoice_no),   
                    new SqlParameter("item_name", item_name),   
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /* 调拨单查询
         * 返回调拨单身
         */
        public DataSet getExchangeLineBySome(string invoice_no, string item_name, DateTime start_time, DateTime end_time)
        {
            //完整查询内容
            string sqlAll = "";
            //查询条件
            string sqlTail = "";

            //对start_time和end_time做处理，默认他们两个都有值
            sqlTail += "AND  create_time BETWEEN @start_time AND @end_time ";

            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name =@item_name ";
            }
            //当invoice_no有值时
            if (string.IsNullOrWhiteSpace(invoice_no) == false)
            {
                sqlTail += "AND exchange_header_id=(select exchange_header_id from wms_exchange_header where invoice_no=@invoice_no) ";
            }

            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "select *,operation_seq_num_name=(select route from wms_wip_operations where route_id=operation_seq_num),out_subinventory_name=(select subinventory_name from wms_subinventory where subinventory_key=out_subinventory),in_subinventory_name=(select subinventory_name from wms_subinventory where subinventory_key=in_subinventory) from wms_exchange_line   ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "select *,operation_seq_num_name=(select route from wms_wip_operations where route_id=operation_seq_num),out_subinventory_name=(select subinventory_name from wms_subinventory where subinventory_key=out_subinventory),in_subinventory_name=(select subinventory_name from wms_subinventory where subinventory_key=in_subinventory) from wms_exchange_line WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("start_time", start_time),
                    new SqlParameter("end_time", end_time),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("invoice_no", invoice_no),  
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /* 调拨单作业
         * 检查存不存在这样的库存明细
         */
        public DataSet getMaterial_ioBySome(string item_name, string frame_name, string datecode)
        {
            string sql = "select unique_id from wms_material_io where item_id=(select item_id from wms_pn where item_name=@item_name) and frame_key=(select frame_key from wms_frame where frame_name=@frame_name ) and datecode=@datecode ";

            SqlParameter[] parameters = {
                 new SqlParameter("item_name",item_name),
                 new SqlParameter("frame_name",frame_name),
                 new SqlParameter("datecode",datecode),
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;

        }


        /* 调拨单作业
         * Debit（扣账）操作
        */
        public int ExchangeDebitAction(DateTime update_time, string update_man, int exchange_line_id, int exchanged_qty, string in_subinventory, string out_subinventory, string item_name, string datecode, string out_frame_key, string in_frame_key)
        {
            string sql;

            //检测存不存在这样的库存明细（针对调入料架，调入仓库）存在是更新，不存在是插入
            InvoiceDC invoiceDC = new InvoiceDC();

            if (invoiceDC.getMaterial_ioBySome(item_name, in_frame_key, datecode) == null)
                sql =//+ "--增加庫存明細表中对应仓库的庫存量（调入料架）" 2
                    "insert into wms_material_io (item_id,frame_key,datecode,onhand_qty,simulated_qty,create_time) values ((select item_id from wms_pn where item_name=@item_name),(select frame_key from wms_frame where frame_name=@in_frame_key),@datecode,@exchanged_qty,0,getdate()); "
                    //+ "--增加庫存总表的庫存（调入库别） 3
                    + "insert into wms_items_onhand_qty_detail (item_name,item_id,onhand_quantiy,subinventory) values (@item_name,(select item_id from wms_pn where item_name=@item_name),@exchanged_qty,@in_subinventory); ";
            else
                sql = //+ "--增加庫存明細表中对应仓库的庫存量（调入料架）" 2
                    "UPDATE wms_material_io SET onhand_qty=onhand_qty + @exchanged_qty,update_time=@update_time WHERE frame_key=(select frame_key from wms_frame where frame_name=@in_frame_key) AND item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name) AND datecode=@datecode;  "
                    //+ "--增加庫存总表的庫存（调入库别）" 3
                    + "UPDATE wms_items_onhand_qty_detail SET onhand_quantiy=onhand_quantiy + @exchanged_qty,update_time=@update_time  WHERE subinventory=@in_subinventory AND item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name); ";

            sql +=
                //更新调拨单明细表 4
                "UPDATE wms_exchange_line SET update_time=@update_time,update_man=@update_man,in_frame_key=@in_frame_key,out_frame_key=@out_frame_key  WHERE exchange_line_id=@exchange_line_id; "

                //+ "--减少庫存总表的庫存（调出库别）" 5
            + "UPDATE wms_items_onhand_qty_detail SET onhand_quantiy=onhand_quantiy - @exchanged_qty,update_time=@update_time  WHERE subinventory=@out_subinventory AND item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name); "

            //+ "--减少庫存明細表中对应仓库的庫存量（调出料架）" 6
            + "UPDATE wms_material_io SET onhand_qty=onhand_qty - @exchanged_qty,update_time=@update_time WHERE frame_key=(select frame_key from wms_frame where frame_name=@out_frame_key) AND item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name) AND datecode=@datecode and left_qty- @exchanged_qty>=0 ;  "

            //+ "--将数据插入到交易表" 7
            + "INSERT INTO wms_transaction_operation (invoice_no,item_name,transaction_qty,transaction_type,transaction_time,create_user) VALUES ((select invoice_no from wms_exchange_header where exchange_header_id=(select exchange_header_id from wms_exchange_line where exchange_line_id=@exchange_line_id)),@item_name,@exchanged_qty,'Exchange_Debit',@update_time,@update_man)";



            SqlParameter[] parameters = {
                new SqlParameter("update_time", update_time),
                new SqlParameter("update_man", update_man),
                new SqlParameter("exchange_line_id", exchange_line_id),
                new SqlParameter("exchanged_qty", exchanged_qty),
                new SqlParameter("in_subinventory", in_subinventory),
                new SqlParameter("out_subinventory", out_subinventory),
                new SqlParameter("out_frame_key", out_frame_key),
                new SqlParameter("in_frame_key", in_frame_key),
                new SqlParameter("item_name", item_name),
                new SqlParameter("datecode", datecode),
                    
                };

            DB.connect();

            //返回结果
            int result = InvoiceDC.tran(sql, parameters);

            return result;  //返回1则是事务执行成功
        }

        //自己重写的数据库事务
        public static int tran(string str, SqlParameter[] cmdParms)
        {

            SqlConnection conn;
            string connectStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conn = new SqlConnection(connectStr);

            conn.Open();
            SqlCommand sqlcomman = conn.CreateCommand();
            SqlTransaction transaction;
            transaction = conn.BeginTransaction("Tran");
            sqlcomman.Connection = conn;
            sqlcomman.Transaction = transaction;
            string[] spstr = str.Split(';');
            int i;
            try
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    sqlcomman.Parameters.Add(parm);
                }
                for (i = 0; i < spstr.Length; i++)
                {
                    sqlcomman.CommandText = spstr[i];
                    try
                    {
                        //sqlcomman.ExecuteScalar(); （这个是返回结果集中第一行的第一列或空引用）

                        int count = sqlcomman.ExecuteNonQuery();  //这个是返回结果集
                        if (count <= 0)  //执行失败的时候，返回错误语句+2，避免返回1的情况。从2开始
                            return i + 2;

                    }
                    catch (Exception ex3)
                    {
                        return 0;
                    }
                }

                transaction.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    return 0;
                }
                catch (Exception ex2)
                {
                    return 0;
                }
            }
            finally
            {
                conn.Close();
                sqlcomman.Parameters.Clear();
            }
        }
    }
}