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

namespace WMS_v1._0.DataCenter //作者：周雅雯 最后一次修改时间：2016/10/12
{
    public class SupplierDC//供应商相关资料维护表（wms_supplier）对应的DateCenter
    {
        // 获得数据库中所有厂商代码 
        public List<string> getAllVendor_key()
        {
            string sql = "select vendor_key from wms_customers  ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["vendor_key"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }


        //查询出整张表中的信息
        public List<ModelSupplier> getSupplier()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT * FROM wms_customers";

            SqlParameter[] parameters = null;

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelSupplier> modelSupplier_list = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                modelSupplier_list = new List<ModelSupplier>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelSupplier_list.Add(toModel(datarow));
                }
            }
            return modelSupplier_list;
        }

        //2016.10.23 wxs
        //通过厂商名Vendor_name，查询供应商相关资料维护表信息
        public DataSet getSupplierByNameAndKey(string Vendor_name, string Vendor_key)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_customers where Vendor_name = @Vendor_name union all select * from wms_customers where Vendor_key = @Vendor_key";

            SqlParameter[] parameters = {
                new SqlParameter("Vendor_name", Vendor_name),
                new SqlParameter("Vendor_key", Vendor_key)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);
            return ds;
        }

        public DataSet getSupplierByNameAndKeyAndId(string vendor_id, string Vendor_name, string Vendor_key)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_customers where Vendor_name = @Vendor_name and vendor_id!=@vendor_id " +
                "union all " +
                "select * from wms_customers where Vendor_key = @Vendor_key and vendor_id!=@vendor_id";

            SqlParameter[] parameters = {
                new SqlParameter("vendor_id", vendor_id),
                new SqlParameter("Vendor_name", Vendor_name),
                new SqlParameter("Vendor_key", Vendor_key)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);
            return ds;
        }

        //通过厂商名Vendor_name，查询供应商相关资料维护表信息
        public List<ModelSupplier> getSupplierByVendor_name(string Vendor_name)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_customers where Vendor_name = @Vendor_name";

            SqlParameter[] parameters = {
                new SqlParameter("Vendor_name", Vendor_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelSupplier> modelSupplier_list = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                modelSupplier_list = new List<ModelSupplier>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelSupplier_list.Add(toModel(datarow));
                }
            }
            return modelSupplier_list;
        }

        /**作者：周雅雯 时间：2016/8/16
        * 供应商设定页面需要的查询方法
        **/
        public DataSet getSupplierBySome(string vendor_name, string vendor_key)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当vendor_name有值时
            if (string.IsNullOrWhiteSpace(vendor_name) == false)
            {
                sqlTail += "AND vendor_name LIKE '%'+@vendor_name+'%' ";
            }
            //当vendor_key有值时
            if (string.IsNullOrWhiteSpace(vendor_key) == false)
            {
                sqlTail += "AND vendor_key LIKE '%'+@vendor_key+'%' ";
            }

            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT c.vendor_id,c.vendor_name,c.vendor_key,create_time,c.update_time," +
                    "(select u.user_name from wms_users u where u.user_id=c.create_by) as create_by," +
                    "(select u.user_name from wms_users u where u.user_id=c.update_by) as update_by FROM wms_customers c";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_customers  WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("vendor_name", vendor_name),
                    new SqlParameter("vendor_key", vendor_key),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            return ds;
        }


        //向供应商相关资料维护表中插入数据
        public Boolean insertSupplier(string vendor_name, int create_by, string vendor_key, DateTime create_time)
        {

            string sql = "insert into wms_customers "
                       + "(vendor_name,create_by,vendor_key,create_time)values "
                       + "(@vendor_name,@create_by,@vendor_key,@create_time) ";

            SqlParameter[] parameters = {
                new SqlParameter("vendor_name",vendor_name),
                new SqlParameter("create_by",create_by),
                new SqlParameter("vendor_key",vendor_key),
                new SqlParameter("create_time",create_time)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //更新供应商相关资料维护表中的部分数据
        public Boolean updateSupplier(string vendor_id, string vendor_name, int update_by, string vendor_key, DateTime update_time)
        {
            string sql = "update wms_customers "
                        + "set vendor_name = @vendor_name,update_by = @update_by,vendor_key = @vendor_key,update_time=@update_time "
                        + "where vendor_id = @vendor_id";

            SqlParameter[] parameters = {
                new SqlParameter("vendor_id",vendor_id),
                new SqlParameter("vendor_name",vendor_name),
                new SqlParameter("update_by",update_by),
                new SqlParameter("vendor_key",vendor_key),
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

        //删除供应商相关资料维护表中的单条数据
        public Boolean deleteSupplier(int vendor_id)
        {
            string sql = "delete from wms_customers "
                        + "where vendor_id = @vendor_id";

            SqlParameter[] parameters = {
                new SqlParameter("vendor_id",vendor_id)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //删除供应商相关资料维护表中的单条数据
        public Boolean deleteSupplier(string vendor_name)
        {
            string sql = "delete from wms_customers "
                        + "where vendor_name = @vendor_name";

            SqlParameter[] parameters = {
                new SqlParameter("vendor_name",vendor_name)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        // 传入DataRow,将其转换为ModelSupplier
        private ModelSupplier toModel(DataRow dr)
        {
            ModelSupplier model = new ModelSupplier();

            //通过循环为ModelSupplier赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelSupplier).GetProperties())
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