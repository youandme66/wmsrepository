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
    public class Return_headerDC//退料总表（Return_header）对应的DateCenter
    {
        //通过INVOICE_NO退料单号，获取ModelReturn_header对象的列表集合
        public List<ModelReturn_header> getReturn_headerByINVOICE_NO(string invoice_no)
        {

            //通过SQL语句，获取DateSet    
            string sql = "select * from wms_return_header where INVOICE_NO = @invoice_no";

            SqlParameter[] parameters = {
                new SqlParameter("INVOICE_NO", invoice_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelReturn_header> ModelReturn_headerlist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelReturn_headerlist = new List<ModelReturn_header>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelReturn_headerlist.Add(toModel(DateSetRows));
                }
                return ModelReturn_headerlist;
            }
            else
            {
                return ModelReturn_headerlist;
            }
        }

        //通过模糊查询INVOICE_NO退料单号，获取ModelReturn_header对象的列表集合
        public DataSet getReturn_headerByLikeINVOICE_NO(string invoice_no)
        {

            //通过SQL语句，获取DateSet 
            string sql;

            if(string.IsNullOrWhiteSpace(invoice_no)!= true)
                sql = "select *,return_sub_name=(select subinventory_name from wms_subinventory where subinventory_key=wms_return_header.return_sub_key),return_wo_no_name=(select wo_no from wms_wo where wo_key=wms_return_header.return_wo_no) from wms_return_header where INVOICE_NO LIKE '%'+@invoice_no+'%'";
            else
                sql = "select *,return_sub_name=(select subinventory_name from wms_subinventory where subinventory_key=wms_return_header.return_sub_key),return_wo_no_name=(select wo_no from wms_wo where wo_key=wms_return_header.return_wo_no) from wms_return_header ";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no", invoice_no)
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

        //通过自增长的createtime，获取最新一条退料主表信息
        public ModelReturn_header getTheNewestReturn_headerByCreatetime()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT TOP 1.*  FROM wms_return_header ORDER BY return_header_id DESC";

            SqlParameter[] parameters = null;


            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return toModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        //向退料主表中插入部分数据
        public Boolean insertReturn_header(string INVOICE_NO)
        {

            string sql = "insert into wms_return_header"
                       + "(INVOICE_NO)values"
                       + "(@INVOICE_NO) ";

            SqlParameter[] parameters = {
                new SqlParameter("INVOICE_NO",INVOICE_NO),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //向退料主表中插入部分数据
        public Boolean insertReturn_header(string INVOICE_NO, string return_type, int return_sub_key, int status, int return_wo_no, string return_man, string remark)
        {

            string sql = "insert into wms_return_header"
                       + "(INVOICE_NO,return_type,return_sub_key,status,return_wo_no,return_man,remark)values"
                       + "(@INVOICE_NO,@return_type,@return_sub_key,@status,@return_wo_no,@return_man,@remark) ";

            SqlParameter[] parameters = {
                new SqlParameter("INVOICE_NO",INVOICE_NO),
                new SqlParameter("return_type",return_type),
                new SqlParameter("return_sub_key",return_sub_key),
                new SqlParameter("status",status),
                new SqlParameter("return_wo_no",return_wo_no),
                new SqlParameter("return_man",return_man),
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


        //向退料主表中插入部分数据
        public Boolean insertReturn_header(string INVOICE_NO, int return_sub_key, string return_wo_no)
        {

            string sql = "insert into wms_return_header"
                       + "(INVOICE_NO,return_sub_key,return_wo_no)values"
                       + "(@INVOICE_NO,@return_sub_key,@return_wo_no) ";

            SqlParameter[] parameters = {
                new SqlParameter("INVOICE_NO",INVOICE_NO),
                new SqlParameter("return_sub_key",return_sub_key),
                new SqlParameter("return_wo_no",return_wo_no)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }



        //更新退料主表中部分数据
        public Boolean updateReturn_header(string invoice_no, string return_type, int return_sub_key, int status, int return_wo_no, string return_man, string remark)
        {

            string sql = "update  wms_return_header set return_type=@return_type,return_sub_key=@return_sub_key,status=@status,return_wo_no=@return_wo_no,return_man=@return_man,remark=@remark where invoice_no=@invoice_no";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no",invoice_no),
                new SqlParameter("return_type",return_type),
                new SqlParameter("return_sub_key",return_sub_key),
                new SqlParameter("status",status),
                new SqlParameter("return_wo_no",return_wo_no),
                new SqlParameter("return_man",return_man),
                new SqlParameter("remark",remark),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //删除退料主表中部分数据
        public Boolean deleteReturn_header(string INVOICE_NO)
        {

            string sql = "delete from  wms_return_header"
                       + " where INVOICE_NO=@INVOICE_NO";

            SqlParameter[] parameters = {
                new SqlParameter("INVOICE_NO",INVOICE_NO),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        public List<string> getAllreturn()
        {
            string sql = "select * from wms_return_header";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["return_header_id"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        public List<string> getAllreturns()
        {
            string sql = "select * from wms_return_header";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["invoice_no"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        public string getAllreturnss(string invoice_no)
        {
            string sql = "select * from wms_return_header";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            List<string> modellists = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string b = "0";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["invoice_no"].ToString());
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellists.Add(dr["return_header_id"].ToString());
                }
                for (int i = 0; i < modellist.Count; i++)
                {
                    if (invoice_no == modellist[i])
                    {
                        b = modellists[i];
                        break;
                    }
                }
                return b;
            }
            else
            {
                return null;
            }
        }

        // 传入DataRow,将其转换为ModelReturn_header
        private ModelReturn_header toModel(DataRow dr)
        {
            ModelReturn_header model = new ModelReturn_header();

            //通过循环为ModelReturn_header赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelReturn_header).GetProperties())
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