using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;

namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/8/29
{
    public class CustomersDC //客户信息表(wms_customers2）对应的DateCenter
    {


        /**作者：周雅雯 时间：2016/8/29
        * 客户设定需要的插入方法
        **/
        public Boolean insertCustomers(string customer_name, string create_by, string code)
        {

            string sql = "insert into wms_customers2 "
                       + "(customer_name,create_by,customer_code)values "
                       + "(@customer_name,@create_by,@code) ";

            SqlParameter[] parameters = {
                new SqlParameter("customer_name",customer_name),
                new SqlParameter("create_by",create_by),
                new SqlParameter("code",code)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /**作者：周雅雯 时间：2016/8/13
         * 客户设定页面需要的删除方法
         **/
        public Boolean deleteCustomers(string customer_code)
        {
            string sql = "delete from wms_customers2 "
                        + "where customer_code = @customer_code";

            SqlParameter[] parameters = {
                new SqlParameter("customer_code",customer_code),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /**作者：周雅雯 时间：2016/8/29
        * 用户设定页面需要的更新方法
        **/
        public Boolean updateCustomers(string customer_name, string update_by, DateTime update_time, string customer_key, int key)
        {
            string sql = "update wms_customers2 "
                        + "set customer_code=@customer_key,customer_name = @customer_name,update_by=@update_by,update_time=@update_time "
                        + "where customer_key = @key";

            SqlParameter[] parameters = {
                new SqlParameter("customer_name",customer_name),
                new SqlParameter("update_by",update_by),
                new SqlParameter("update_time",update_time),
                new SqlParameter("customer_key",customer_key),
                new SqlParameter("key",key)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /**作者：周雅雯 时间：2016/8/29
         * 客户设定页面需要的查询方法
         **/
        public DataSet getCustomersBySome(string customer_name, string code)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";



            //当customer_name有值时
            if (string.IsNullOrWhiteSpace(customer_name) == false)
            {
                sqlTail += "AND customer_name LIKE '%'+@customer_name+'%' ";
            }
            //当code有值时
            if (string.IsNullOrWhiteSpace(code) == false)
            {
                sqlTail += "AND code LIKE '%'+@code+'%' ";
            }

            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_customers2 ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_customers2  WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                new SqlParameter("customer_name",customer_name),
                new SqlParameter("code",code)
            };

            DataSet ds = DB.select(sqlAll, parameters);

            return ds;
        }
        public DataSet getCustomerCount(string customer_key, string customer_name)
        {
            string sql = "select * from wms_customers2 where customer_code=@customer_key union all select * from wms_customers2 where customer_name=@customer_name";
            DB.connect();
            SqlParameter[] parameters ={
                                           new SqlParameter("customer_key",customer_key),
                                           new SqlParameter("customer_name",customer_name)
                                       };
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }

        public int getCustomeridByname(string customer_name)
        {
            string sql = "select customer_key from wms_customers2 where customer_name=@customer_name;";
            DB.connect();
            SqlParameter[] parameters ={
                new SqlParameter("customer_name",customer_name),
                                      };
            DataSet ds = DB.select(sql, parameters);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int id = -1;
                try
                {
                    id = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                    return id;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
            return -1;
        }
        public DataSet getCustomer(string customer_name)
        {
            string sql = "select * from wms_customers2 where customer_name=@customer_name;";
            DB.connect();
            SqlParameter[] parameters ={
                new SqlParameter("customer_name",customer_name),
                                      };
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }
        public DataSet getCustomer2(string customer_code)
        {
            string sql = "select * from wms_customers2 where customer_code=@customer_code";
            DB.connect();
            SqlParameter[] parameters ={
                      new SqlParameter("customer_code",customer_code),
                                      };
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }


        public DataSet getCustomerByVendor_key(string vendor_key)
        {
            string sql = "select vendor_name from wms_customers where vendor_key=@vendor_key;";
            DB.connect();
            SqlParameter[] parameters ={
                new SqlParameter("vendor_key",vendor_key),
                                      };
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }

        private ModelCustomers toModel(DataRow dr)
        {
            ModelCustomers model = new ModelCustomers();

            //通过循环为ModelCustomers赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelCustomers).GetProperties())
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