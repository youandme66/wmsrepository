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
    public class SubinventoryDC     //库别表（subinventory）对应的DateCenter
    {
        //通过Subinventory_name库别名称，获取ModelSubinventory对象的列表集合
        public List<ModelSubinventory> getSubinventoryBySubinventory_name(string Subinventory_name)
        {
            //通过SQL语句，获取DateSet
            string sql = "select * from wms_subinventory where Subinventory_name = @Subinventory_name";

            SqlParameter[] parameters = {
                new SqlParameter("Subinventory_name", Subinventory_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelSubinventory> ModelSubinventorylist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelSubinventorylist = new List<ModelSubinventory>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelSubinventorylist.Add(toModel(DateSetRows));
                }
                return ModelSubinventorylist;
            }
            else
            {
                return ModelSubinventorylist;
            }

        }

        //通过模糊查询Subinventory_name库别名称，获取ModelSubinventory对象的列表集合
        public List<ModelSubinventory> getSubinventoryByLikeSubinventory_name(string Subinventory_name)
        {
            //通过SQL语句，获取DateSet
            string sql = "select * from wms_subinventory where Subinventory_name LIKE @Subinventory_name";

            SqlParameter[] parameters = {
                new SqlParameter("Subinventory_name", Subinventory_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelSubinventory> ModelSubinventorylist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelSubinventorylist = new List<ModelSubinventory>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelSubinventorylist.Add(toModel(DateSetRows));
                }
                return ModelSubinventorylist;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 获得数据库中所有库别名
        /// </summary>
        /// <returns></returns>
        public List<string> getAllSubinventory()
        {


            string sql = "select * from wms_subinventory where enabled = 'Y'or enabled='1' ";


            DB.connect();
            DataSet ds = DB.select(sql,null);

            List<string> modellist = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["subinventory_name"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据库中所有库别名 getAllSubinventory.ashx用
        /// </summary>
        /// <returns></returns>
        public DataSet getAllSubinventory2()
        {


            string sql = "select [subinventory_name],subinventory_key from wms_subinventory ";


            DB.connect();
            DataSet ds = DB.select(sql, null);

            if (ds != null)
                return ds;
            else
                return null;
        }

        //查询wms_subinventory中所有的subinventory_name
        public DataSet getAllUsedSubinventory_name()
        {
            string sql = "select [subinventory_name],subinventory_key from wms_subinventory where enabled = 'Y' or enabled = '1' ";

            DB.connect();
            DataSet ds = DB.select(sql, null);
            
            if (ds != null)
                return ds;
            else
                return null;
            
        }


       
        /** 通过subinventory_name获得subinventory_key
         * 调拨单页面需要方法
         **/
        public int getSubinventory_key(string subinventory_name)
        {
            string sql = "select Subinventory_key from wms_subinventory where subinventory_name=@subinventory_name";

            DB.connect();

            SqlParameter[] parameters = {
                new SqlParameter("subinventory_name", subinventory_name)
            };

            DataSet ds = DB.select(sql, parameters);

            int subinventory_key = -1;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   subinventory_key=int.Parse(dr["subinventory_key"].ToString());
                }
                return subinventory_key;
            }
            else
            {
                return -1;
            }
        }

        /** 通过subinventory_key获得Subinventory_name
         **/
        public string getSubinventory_name(int subinventory_key)
        {
            string sql = "select Subinventory_name from wms_subinventory where subinventory_key=@subinventory_key";

            DB.connect();

            SqlParameter[] parameters = {
                new SqlParameter("subinventory_key", subinventory_key)
            };

            DataSet ds = DB.select(sql, parameters);

            string Subinventory_name="" ;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Subinventory_name = dr["Subinventory_name"].ToString();
                }
                return Subinventory_name;
            }
            else
            {
                return Subinventory_name;
            }
        }

        /// <summary>
        /// 获得所有库别数据
        /// </summary>
        /// <returns></returns>
        public List<ModelSubinventory> getSubinventoryList()
        {
            string sql = "SELECT * FROM wms_subinventory";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<ModelSubinventory> modellist = new List<ModelSubinventory>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(toModel(dr));
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 新增一条库别数据
        /// </summary>
        /// <param name="subinventory_name"></param>
        /// <param name="create_by"></param>
        /// <param name="enabled"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public bool insertSubinventory(string subinventory_name, int create_by, string enabled, string description, DateTime create_time)
        {
            string sql = "insert into wms_subinventory(subinventory_name, create_by, enabled, description,create_time) values(@subinventory_name, @create_by, @enabled, @description,@create_time) ";

            SqlParameter[] parameters = { 
                new SqlParameter("subinventory_name", subinventory_name),
                new SqlParameter("create_by", create_by),
                new SqlParameter("enabled", enabled),
                new SqlParameter("create_time", create_time),
                new SqlParameter("description", description)
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

        /// <summary>
        /// 更新一条库别表的数据
        /// </summary>
        /// <param name="subinventory_key"></param>
        /// <param name="subinventory_name"></param>
        /// <param name="update_by"></param>
        /// <param name="enabled"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public bool updateSubinventory(int subinventory_key, string subinventory_name, int update_by, string enabled, string description)
        {
            string sql = "update wms_subinventory set subinventory_name = @subinventory_name , update_by = @update_by , enabled = @enabled , description = @description, update_time = GETDATE() where subinventory_key = @subinventory_key ";

            SqlParameter[] parameters = { 
                new SqlParameter("subinventory_name", subinventory_name),
                new SqlParameter("subinventory_key", subinventory_key),
                new SqlParameter("update_by", update_by),
                new SqlParameter("enabled", enabled),
                new SqlParameter("description", description)
            };

            DB.connect();

            List<ModelSubinventory> modellist = new List<ModelSubinventory>();
            int flag = DB.update(sql, parameters);

            if (flag == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条库别表数据
        /// </summary>
        /// <param name="subinventory_key"></param>
        /// <returns></returns>
        public bool deleteSubinventoryById(int subinventory_key)
        {
            string sql = "delete from wms_subinventory where subinventory_key = @subinventory_key ";

            SqlParameter[] parameters = { 
                new SqlParameter("subinventory_key", subinventory_key)                            
            };

            DB.connect();
            int flag = DB.delete(sql, parameters);

            if (flag == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 按条件查询库别
        /// </summary>
        /// <param name="subinventory_name"></param>
        /// <param name="enabled"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public DataSet getSubinventoryBySome(string subinventory_name, string enabled, string description)
        {
            string sql = "select *,CREATE_name=(select user_name from wms_users where user_id=wms_subinventory.create_by),UPDATE_name=(select user_name from wms_users where user_id=wms_subinventory.update_by) from wms_subinventory where 1=1 ";

            SqlParameter[] parameters = { 
                new SqlParameter("subinventory_name", subinventory_name),         
                new SqlParameter("enabled", enabled),
                new SqlParameter("description", description),
            };

            DB.connect();

            DataSet ds = new DataSet();
            List<ModelSubinventory> modellist = new List<ModelSubinventory>();

            if (!string.IsNullOrWhiteSpace(subinventory_name))
            {
                sql += "AND subinventory_name  = @subinventory_name  ";
            }
            if (!string.IsNullOrWhiteSpace(enabled))
            {
                sql += "AND enabled = @enabled  ";
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                sql += "AND description like '%' + @description + '%' ";
            }
          
   
            ds = DB.select(sql, parameters);

            if (ds != null&&ds.Tables[0].Rows.Count >0)
            {
                return ds;
            }
            else
            {
                return null;
            }

        }

        // 传入DataRow,将其转换为ModelSubinventory
        private ModelSubinventory toModel(DataRow dr)
        {
            ModelSubinventory model = new ModelSubinventory();

            //通过循环为ModelSubinventory赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelSubinventory).GetProperties())
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