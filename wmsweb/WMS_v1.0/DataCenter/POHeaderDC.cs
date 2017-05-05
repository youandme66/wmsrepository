using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;

namespace WMS_v1._0.DataCenter
{
    //By zrk
    public class POHeaderDC
    {

        //查询出整张表中的信息
        public List<ModelPO_Header> getPO_Header()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT * FROM wms_po_header";

            SqlParameter[] parameters = null;

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelPO_Header> modelPO_Header_list = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                modelPO_Header_list = new List<ModelPO_Header>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelPO_Header_list.Add(toModel(datarow));
                }
            }
            return modelPO_Header_list;
        }


        /// <summary>
        /// 通过PO_NO,获取PO单头对象
        /// </summary>
        /// <param name="po_no"></param>
        /// <returns></returns>
        public List<ModelPO_Header> getPOHeaderByPO_NO(string po_no, string line)
        {
            string sqlline = "select a.PO_NO,a.PO_HEADER_ID,a.VENDOR_ID,a.CREATE_BY,a.CREATE_TIME,a.UPDATE_BY,a.UPDATE_TIME from wms_po_header a  inner join wms_po_line b on(a.PO_HEADER_ID = b.Po_header_id AND a.OR PO_NO = @po_no) where b.Line_num = @line";
            string sqlpo_no = "select * from wms_po_header where PO_NO = @po_no";
            List<ModelPO_Header> polist = null;

            SqlParameter[] parameters1 = {
                new SqlParameter("po_no", po_no)
            };

            SqlParameter[] parameters2 = {
                new SqlParameter("po_no", po_no),
                new SqlParameter("line", line)
            };

            DB.connect();
            DataSet ds = null;
            if (String.IsNullOrWhiteSpace(line))
            {
                ds = DB.select(sqlpo_no, parameters1);
            }
            else {
                ds = DB.select(sqlline, parameters2);
            }
            

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    polist.Add(toModel(dr));
                }
                return polist;
            }
            else 
            {
                return null;
            }
        }

        /**"如果一个PO单号数量已经收料完成了，就不用再显示出来了"
         *   PO暂收中下拉框中 只返回还需要收料的PO单号
         **/
        public DataSet getAllValidPo_no()
            {
            string sql = "SELECT po_no FROM wms_po_header c WHERE (SELECT SUM(rcv_qty) FROM wms_receive_mtl a where a.po_no=c.po_no) < (SELECT request_qty FROM wms_po_line p WHERE p.po_line_id in (SELECT po_line_id FROM wms_receive_mtl WHERE po_no = c.po_no)  and po_header_id = (SELECT po_header_id FROM wms_po_header WHERE po_no=c.po_no)) ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }


        /**作者：周雅雯 时间：2016/9/3
         * 返回所有的PO单号
         **/
        public DataSet getAllPo_no()
        {
            string sql = "select po_no from wms_po_header  ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        /**作者：周雅雯 时间：2016/8/13
         * Po/Poline页面需要的查询方法
         **/
        public DataSet getPO_HeaderBySome(string po_no, string vendor_key)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当po_no有值时
            if (string.IsNullOrWhiteSpace(po_no) == false )
            {
                sqlTail += "AND po_no LIKE '%'+@po_no+'%' ";
            }
            //当vendor_key有值时
            if (string.IsNullOrWhiteSpace(vendor_key) == false)
            {
                sqlTail += "AND vendor_key LIKE '%'+@vendor_key+'%' ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_po_header ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_po_header  WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("po_no", po_no),
                    new SqlParameter("vendor_key", vendor_key)
                };

            DataSet ds = DB.select(sqlAll, parameters);

            return ds;
        }




        /**作者周雅雯，时间：2016/11/14
         * 通过PO_NO返回POHeader所有数据和在手总量onhand_quantiy
         * **/
        public DataSet getPOHeaderByPo_no(string Po_no)
        {
            //通过SQL语句，获取DateSet
            string sql = "select wms_po_header.*,wms_customers.vendor_name from wms_po_header "+
                         "left join wms_customers on wms_customers.vendor_key = wms_po_header.vendor_key "+
                         "where wms_po_header.po_no = @po_no";

            SqlParameter[] parameters = {
                new SqlParameter("Po_no", Po_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            return ds;
        }

        /**作者周雅雯，时间：2016/8/12
         * PO/POline设定页面
         * 向po单头表中插入部分数据
         **/
        public Boolean insertPoHeader(string po_no, string vendor_key, int create_by, DateTime create_time)
        {

            string sql = "insert into wms_po_header "
                       + "(po_no,vendor_key,create_by,create_time)values "
                       + "(@po_no,@vendor_key,@create_by,@create_time) ";

            SqlParameter[] parameters = {
                new SqlParameter("po_no",po_no),
                new SqlParameter("vendor_key",vendor_key),
                new SqlParameter("create_by",create_by),
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

        /**作者周雅雯，时间：2016/8/14
         * PO/POline设定页面
         * 删除po单头表中单条数据
         **/
        public Boolean deletePoHeader(string po_no)
        {
            string sql = //删除PO单头
                        "delete from wms_po_header where po_no = @po_no";
                        

            SqlParameter[] parameters = {
                new SqlParameter("po_no",po_no),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        /**作者周雅雯，时间：2016/8/6
         * PO/POline设定页面
         * 删除po单头和对应的PO单身
         **/
        public Boolean deletePo(string po_no)
        {
            string sql = "BEGIN TRAN Tran_deletePo    --开始事务"
                       + "DECLARE @tran_error int;"
                       + "SET @tran_error = 0;"

                       + "BEGIN TRY "
                       + "--删除PO单头"
                       + "delete from wms_po_header "
                       + "where po_no = @po_no; "
                       + "SET @tran_error = @tran_error + @@ERROR;"
                       + "--删除PO单身"
                       + "delete from wms_po_line "
                       + "where po_header_id = (select po_header_id from wms_po_header where po_no=@po_no)"
                       + "END TRY"

                       + "BEGIN CATCH"
                       + "PRINT '出现异常，错误编号：' + convert(varchar,error_number()) + ',错误消息：' + error_message()"
                       + "SET @tran_error = @tran_error + 1"
                       + "END CATCH"

                       + "IF(@tran_error > 0)"
                       + "BEGIN"
                       + "--执行出错，回滚事务"
                       + "ROLLBACK TRAN;"
                       + "PRINT '删除PO/POline失败，取消交易!';"
                       + "END"
                       + "ELSE"
                       + "BEGIN"
                       + "--没有异常，提交事务"
                       + " COMMIT TRAN;"
                       + "PRINT '删除PO/POline成功!';"
                       + "END";

            SqlParameter[] parameters = {
                new SqlParameter("po_no",po_no),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }
       

        /**作者周雅雯，时间：2016/8/13
         * PO/POline设定页面
         * 更新po单头表中的部分数据
         **/
        public Boolean updatePoHeader(string po_no,string vendor_key, int update_by ,DateTime update_time)
        {
            string sql = "update wms_po_header "
                        + "set po_no = @po_no,vendor_key = @vendor_key,update_by = @update_by,update_time=@update_time "
                        + "where po_no = @po_no";

            SqlParameter[] parameters = {
                new SqlParameter("po_no",po_no),
                new SqlParameter("vendor_key",vendor_key),
                new SqlParameter("update_by",update_by),
                new SqlParameter("update_time",update_time),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //通过PO单号Po_no，查询PO单号维护表信息
        public List<ModelPO_Header> getPO_Header(string Po_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_po_header where Po_no = @Po_no";

            SqlParameter[] parameters = {
                new SqlParameter("Po_no", Po_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelPO_Header> modelPO_Header_list = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                modelPO_Header_list = new List<ModelPO_Header>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelPO_Header_list.Add(toModel(datarow));
                }
            }
            return modelPO_Header_list;
        }

        /// <summary>
        /// 传入DataRow,将其转换为PO单头对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private ModelPO_Header toModel(DataRow dr)
        {
            ModelPO_Header model = new ModelPO_Header();

            //通过循环为ModelPO_Header赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelPO_Header).GetProperties())
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