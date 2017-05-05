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
    public class Return_lineDC//退料明细表（Return_line）对应的DateCenter
    {
        //通过退料主表的INVOICE_NO退料单号，获取ModelReturn_line对象的列表集合
        public List<ModelReturn_line> getReturn_lineByinvoice_no(string invoice_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_return_line where return_header_id =(select return_header_id from wms_return_header where invoice_no=@invoice_no )";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no", invoice_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelReturn_line> ModelReturn_linelist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelReturn_linelist = new List<ModelReturn_line>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelReturn_linelist.Add(toModel(DateSetRows));
                }
                return ModelReturn_linelist;
            }
            else
            {
                return ModelReturn_linelist;
            }
        }

        //通过退料主表中的退料单号invoice_no，查询出退料单打印所需的数据
        public DataSet getSomeByinvoice_no(string invoice_no)
        {
            //通过SQL语句，获取DateSet
            string sql = "select  wms_return_line.operation_seq_num,wms_return_line.item_name,subinventory_name =(select subinventory_name from wms_subinventory where subinventory_key=(select return_sub_key from wms_return_line where return_header_id=(select return_header_id from wms_return_header where invoice_no=@invoice_no ))),locator,uom,datecode=(select datecode from wms_material_io where item_id=(select item_id from wms_pn where item_name=(select item_name from wms_return_line where return_header_id=(select return_header_id from wms_return_header where invoice_no=@invoice_no )))),return_qty,remark=(select remark from wms_return_header where invoice_no=@invoice_no) from wms_return_line where return_header_id=(select return_header_id from wms_return_header where invoice_no=@invoice_no) ";

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


        //通过RETURN_WO_NO退料工单号，获取ModelReturn_line对象的列表集合
        public List<ModelReturn_line> getReturn_lineByReturn_line_id(int return_line_id)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_return_line where return_line_id = @return_line_id";

            SqlParameter[] parameters = {
                new SqlParameter("return_line_id", return_line_id)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelReturn_line> ModelReturn_linelist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelReturn_linelist = new List<ModelReturn_line>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelReturn_linelist.Add(toModel(DateSetRows));
                }
                return ModelReturn_linelist;
            }
            else
            {
                return ModelReturn_linelist;
            }
        }
        //通过RETURN_WO_NO退料工单号，获取ModelReturn_line对象的列表集合
        public List<ModelReturn_line> getReturn_lineByRETURN_HEADER_ID(string return_header_id)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_return_line where RETURN_HEADER_ID LIKE @return_header_id";

            SqlParameter[] parameters = {
                new SqlParameter("RETURN_HEADER_ID", return_header_id)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelReturn_line> ModelReturn_linelist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelReturn_linelist = new List<ModelReturn_line>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelReturn_linelist.Add(toModel(DateSetRows));
                }
                return ModelReturn_linelist;
            }
            else
            {
                return ModelReturn_linelist;
            }
        }
        //通过ITEM_NAME料号，获取ModelReturn_line对象的列表集合
        public List<ModelReturn_line> getReturn_lineByITEM_NAME(string ITEM_NAME)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_return_line where ITEM_NAME = @ITEM_NAME";

            SqlParameter[] parameters = {
                new SqlParameter("ITEM_NAME", ITEM_NAME)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelReturn_line> ModelReturn_linelist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelReturn_linelist = new List<ModelReturn_line>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelReturn_linelist.Add(toModel(DateSetRows));
                }
                return ModelReturn_linelist;
            }
            else
            {
                return ModelReturn_linelist;
            }
        }

        /**作者周雅雯，时间：2016/11/19
        * 根据return_wo_no 返回该工单下对应的Item_name (从工单表找对应的料号)
        * */
        public string getItem_nameByReturn_wo_no(string return_wo_no)
        {
            string sql = "select part_no from wms_wo where wo_no=@return_wo_no  ";

            SqlParameter[] parameters = {
                new SqlParameter("return_wo_no", return_wo_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)   //查询操作成功
            {
                string part_no = ds.Tables[0].Rows[0]["part_no"].ToString();
                return part_no;
            }
            else
                return null;
        }

        //通过模糊查询RETURN_WO_NO退料工单号，获取ModelReturn_line对象的列表集合
        public DataSet getReturn_lineByLikeRETURN_WO_NO(string return_wo_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select *,OPERATION_SEQ_NUM_name=(select route from wms_wip_operations where route_id=wms_return_line.OPERATION_SEQ_NUM),RETURN_SUB_name=(select subinventory_name from wms_subinventory where subinventory_key=wms_return_line.return_sub_key) from wms_return_line where RETURN_WO_NO LIKE '%'+@return_wo_no+'%'";

            SqlParameter[] parameters = {
                new SqlParameter("RETURN_WO_NO", return_wo_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count>0)   //如果存在一行及以上数据
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        //通过模糊查询ITEM_NAME料号，获取ModelReturn_line对象的列表集合
        public List<ModelReturn_line> getReturn_lineByLikeITEM_NAME(string ITEM_NAME)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_return_line where ITEM_NAME LIKE @ITEM_NAME";

            SqlParameter[] parameters = {
                new SqlParameter("ITEM_NAME", ITEM_NAME)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelReturn_line> ModelReturn_linelist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelReturn_linelist = new List<ModelReturn_line>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelReturn_linelist.Add(toModel(DateSetRows));
                }
                return ModelReturn_linelist;
            }
            else
            {
                return ModelReturn_linelist;
            }
        }


        /**作者：周雅雯 时间：2016/9/17
         * single query需要的查询方法
         * 通过return_sub_key库别，return_wo_no退料工单号，operation_seq_num制程，uom单位，locator区域,获取ModelReturn_line对象的列表集合
         * 注意：此处因为要将数据库字段int型进行模糊查询，比如operation_seq_num，故传参时传string而不是int，后面进行数据库上的动态转换
         **/
        public DataSet getReturn_lineBySome(string return_sub_key, string return_wo_no, string operation_seq_num, string uom, string locator)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当return_sub_key有值时
            if (string.IsNullOrWhiteSpace(return_sub_key) == false)
            {
                sqlTail += "AND CONVERT(NVARCHAR(10),return_sub_key) LIKE '%'+@return_sub_key+'%'  ";

            }
            //当return_wo_no有值时
            if (string.IsNullOrWhiteSpace(return_wo_no) == false)
            {
                sqlTail += "AND return_wo_no LIKE '%'+@return_wo_no+'%' ";
            }
            //当operation_seq_num有值时
            if (string.IsNullOrWhiteSpace(operation_seq_num) == false)
            {
                sqlTail += "AND CONVERT(NVARCHAR(10),operation_seq_num) LIKE '%'+@operation_seq_num+'%'  ";
            }
            //当uom有值时
            if (string.IsNullOrWhiteSpace(uom) == false)
            {
                sqlTail += "AND uom LIKE '%'+@uom+'%' ";
            }
            //当locator有值时
            if (string.IsNullOrWhiteSpace(locator) == false)
            {
                sqlTail += "AND locator LIKE '%'+@locator+'%' ";
            }

            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT *,invoice_no=(select invoice_no from wms_return_header where return_header_id=wms_return_line.return_header_id),OPERATION_SEQ_NUM_name=(select route from wms_wip_operations where route_id=wms_return_line.operation_seq_num),RETURN_SUB_name=(select subinventory_name from wms_subinventory where subinventory_key=wms_return_line.return_sub_key) FROM wms_return_line ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT *,invoice_no=(select invoice_no from wms_return_header where return_header_id=wms_return_line.return_header_id),OPERATION_SEQ_NUM_name=(select route from wms_wip_operations where route_id=wms_return_line.operation_seq_num),RETURN_SUB_name=(select subinventory_name from wms_subinventory where subinventory_key=wms_return_line.return_sub_key) FROM wms_return_line WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("return_sub_key", return_sub_key),
                    new SqlParameter("return_wo_no", return_wo_no),
                    new SqlParameter("operation_seq_num", operation_seq_num),
                    new SqlParameter("uom", uom),
                    new SqlParameter("locator", locator),
                };

            DataSet ds = DB.select(sqlAll, parameters);

           

            if (ds!=null)   //如果存在一行及以上数据
            {
             
                return ds;
            }
            else
            {
                return null;
            }
        }




        //向退料明细表中插入部分数据 
        public Boolean insertReturn_line(int RETURN_QTY)
        {
            string sql = "insert into wms_return_line"
                       + "(RETURN_QTY)values"
                       + "(@RETURN_QTY) ";

            SqlParameter[] parameters = {
                new SqlParameter("RETURN_QTY",RETURN_QTY),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        /**作者：周雅雯 时间：2016/11/19
         * 返回所有的Line_num
         **/
        public DataSet getLine_numByReturn_header_id(int return_header_id)
        {
            string sql = "select line_num from wms_return_line where return_header_id = @return_header_id  ";

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("return_header_id", return_header_id)
                };
            DataSet ds = DB.select(sql, parameters);


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        //向退料明细表中插入部分数据 
        public Boolean insertReturn_line(string return_wo_no, int operation_seq_num, string item_name, int return_qty, int return_sub_key, int return_header_id, string locator)
        {
            Return_lineDC return_lineDC = new Return_lineDC();
            string sql;

            //当数据库中该退料主表下面没有从表时，即这条为第一个从表信息，Line_num为1
            if(return_lineDC.getLine_numByReturn_header_id(return_header_id)==null)  
                sql = "insert into wms_return_line"
                       + "(line_num,return_wo_no,operation_seq_num,item_name,return_qty,return_sub_key,return_header_id,locator)values"
                       + "(1,@return_wo_no,@operation_seq_num,@item_name,@return_qty,@return_sub_key,@return_header_id,@locator) ";
            else
                sql = "insert into wms_return_line"
                       + "(line_num,return_wo_no,operation_seq_num,item_name,return_qty,return_sub_key,return_header_id,locator)values"
                       + "((select TOP 1 line_num from wms_return_line where return_header_id = @return_header_id order by line_num DESC)+1,@return_wo_no,@operation_seq_num,@item_name,@return_qty,@return_sub_key,@return_header_id,@locator) ";

            SqlParameter[] parameters = {
                new SqlParameter("return_wo_no",return_wo_no),
                new SqlParameter("operation_seq_num",operation_seq_num),
                new SqlParameter("item_name",item_name),
                new SqlParameter("return_qty",return_qty),
                new SqlParameter("return_sub_key",return_sub_key),
                new SqlParameter("return_header_id",return_header_id),
                new SqlParameter("locator",locator)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //向退料明细表中插入部分数据 
        //public Boolean insertReturn_line(DataTable dataTable, string invoice_no)
        //{
        //    //通过invoice_no获取退料主表的对象
        //    Return_headerDC return_headerDC = new Return_headerDC();
        //    List<ModelReturn_header> list = new List<ModelReturn_header>();
        //    list = return_headerDC.getReturn_headerByINVOICE_NO(invoice_no);

        //    //将dataTable中的每行数据插入数据库中
        //    foreach (DataRow datarow in dataTable.Rows)
        //    {
        //        //将每行数据插入数据库中
        //        Return_lineDC return_LineDC = new Return_lineDC();

        //        bool flag = return_LineDC.insertReturn_line(datarow.ItemArray[0].ToString(), int.Parse(datarow.ItemArray[1].ToString()), datarow.ItemArray[2].ToString(), int.Parse(datarow.ItemArray[3].ToString()), int.Parse(datarow.ItemArray[4].ToString()),list[0].Return_header_id);
        //        //若插入失败时，则返回false
        //        if (flag == false)
        //            return false;
        //    }
        //    return true;
        //}

        //向退料明细表中插入部分数据 
        public Boolean insertReturn_line(string return_wo_no, int operation_seq_num, int return_sub_key)
        {
            string sql = "insert into wms_return_line"
                       + "(return_wo_no,operation_seq_num,return_sub_key)values"
                       + "(@return_wo_no,@operation_seq_num,@return_sub_key) ";

            SqlParameter[] parameters = {
                new SqlParameter("return_wo_no",return_wo_no),
                new SqlParameter("operation_seq_num",operation_seq_num),
                new SqlParameter("return_sub_key",return_sub_key)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        //更新退料明细表中部分数据 
        public Boolean updateReturn_line(string return_wo_no, int return_line_id, int operation_seq_num, string item_name, int return_qty, int return_sub_key, string locator)
        {
            string sql = "update wms_return_line "
                       + "set  return_qty=@return_qty,operation_seq_num=@operation_seq_num,item_name=@item_name,return_sub_key=@return_sub_key,locator=@locator,return_wo_no=@return_wo_no"
                       + " where return_line_id=@return_line_id";

            SqlParameter[] parameters = {
                new SqlParameter("return_wo_no",return_wo_no),
                new SqlParameter("operation_seq_num",operation_seq_num),
                new SqlParameter("item_name",item_name),
                new SqlParameter("return_sub_key",return_sub_key),
                new SqlParameter("return_qty",return_qty),
                new SqlParameter("return_line_id",return_line_id),
                new SqlParameter("locator",locator)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //删除退料明细表中部分数据 
        public Boolean deleteReturn_line(int return_line_id)
        {
            string sql = "delete from wms_return_line"
                       + " where return_line_id=@return_line_id";

            SqlParameter[] parameters = {
                new SqlParameter("return_line_id",return_line_id),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }
        //退料操作
        public int updateNum(string subvintory, string region, string item_name, int num, int return_line_id)
        {
            int a = 0;
            string sql = "update wms_items_onhand_qty_detail " 
                +
"set onhand_quantiy=onhand_quantiy+@num" 
+
" where subinventory=(select subinventory_key from wms_subinventory where subinventory_name=@subinventory);" +
"update wms_material_io" 
+
" set onhand_qty=onhand_qty+@num,left_qty=left_qty+@num,return_flag='Y'" 
+
" where subinventory=@subinventory and frame_key=(" 
+
"select top 1 frame_key from WMS_frame " 
+
"where subinventory_key=(select subinventory_key from wms_subinventory where subinventory_name=@subinventory) " +
"and region_key=(select region_key from WMS_region where region_name=@region)) and item_id=(select item_id from wms_pn where item_name=@item_name);" 
+
"update wms_return_line set flag=1 where return_line_id=@return_line_id";
            SqlParameter[] parameters = {            
                new SqlParameter("num",num),
                new SqlParameter("item_name",item_name),
                new SqlParameter("region",region),
                new SqlParameter("subinventory",subvintory),
                new SqlParameter("return_line_id",return_line_id),
            };
            DB.connect();
            a = DB.tran(sql, parameters);
            return a;
        }


        // 传入DataRow,将其转换为ModelReturn_line
        private ModelReturn_line toModel(DataRow dr)
        {
            ModelReturn_line model = new ModelReturn_line();

            //通过循环为ModelReturn_line赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelReturn_line).GetProperties())
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