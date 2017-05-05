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
    public class DepartmentDC
    {    
        public Boolean insertDepartment(string flex_value,string description, string enabled, DateTime create_time, string create_user)
        {

            string sql = "insert into wms_account_flex "
                       + "(flex_value,description,enabled,create_time,create_user)values "
                       + "(@flex_value,@description,@enabled,@create_time,@create_user) ";

            SqlParameter[] parameters = {
                new SqlParameter("flex_value",flex_value),
                new SqlParameter("description",description),
                new SqlParameter("enabled",enabled),
                new SqlParameter("create_time",create_time),
                new SqlParameter("create_user",create_user),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        public Boolean updateDepartment(int department_id, string flex_value, string description, string enabled, DateTime update_time, string update_user)
        {
            string sql = "update wms_account_flex "
                        + "set flex_value=@flex_value,description = @description,enabled = @enabled,update_time=@update_time,update_user=@update_user "
                        + "where department_id = @department_id";

            SqlParameter[] parameters = {
                new SqlParameter("department_id",department_id),
                new SqlParameter("flex_value",flex_value),
                new SqlParameter("description",description),
                new SqlParameter("enabled",enabled),
                new SqlParameter("update_time",update_time),
                new SqlParameter("update_user",update_user),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        //不更新部门名称只更新转态
        public Boolean updateDepartment(string description, string enabled, DateTime update_time, string update_user)
        {
            string sql = "update wms_account_flex "
                        + "set enabled = @enabled,update_time=@update_time,update_user=@update_user "
                        + "where description = @description";

            SqlParameter[] parameters = {                         
                new SqlParameter("description",description),
                new SqlParameter("enabled",enabled),
                new SqlParameter("update_time",update_time),
                new SqlParameter("update_user",update_user),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }
       
        public Boolean deleteDepartment(int department_id)
        {
            string sql =
                        "delete from wms_account_flex where department_id=@department_id";

            SqlParameter[] parameters = {
                new SqlParameter("department_id",department_id),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        public List<ModelDepartment> getDepartmentBySome(int department_id, string flex_value,string description, string enabled)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当department_id有值时
            if (department_id>0)
            {
                sqlTail += "AND department_id = @department_id ";
            }
            //当flex_value有值时
            if (string.IsNullOrWhiteSpace(flex_value) == false)
            {
                sqlTail += "AND flex_value = @flex_value ";
            }
            //当description有值时
            if (string.IsNullOrWhiteSpace(description) == false)
            {
                sqlTail += "AND description LIKE '%'+@description+'%' ";
            }
            //当enabled有值时
            if (string.IsNullOrWhiteSpace(enabled) == false)
            {
                sqlTail += "AND enabled LIKE '%'+@enabled+'%' ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_account_flex ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_account_flex WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("department_id", department_id),
                    new SqlParameter("flex_value", flex_value),
                    new SqlParameter("description", description),
                    new SqlParameter("enabled", enabled),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            List<ModelDepartment> ModelDepartmentlist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelDepartmentlist = new List<ModelDepartment>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelDepartmentlist.Add(toModel(DateSetRows));
                }
                return ModelDepartmentlist;
            }
            else
            {
                return ModelDepartmentlist;
            }
        }



        public List<ModelDepartment>  getDepartmentBySome( string flex_value,string description)
        {
            string sql = "select * from wms_account_flex where flex_value=@flex_value union all select * from wms_account_flex where description=@description";
            DB.connect();
            SqlParameter[] parameters ={
                                           new SqlParameter("flex_value",flex_value),
                                           new SqlParameter("description",description)
                                       };
            DataSet ds = DB.select(sql, parameters);

            List<ModelDepartment> ModelDepartmentlist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelDepartmentlist = new List<ModelDepartment>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelDepartmentlist.Add(toModel(DateSetRows));
                }
                return ModelDepartmentlist;
            }
            else
            {
                return ModelDepartmentlist;
            }
        }


        public List<ModelDepartment> getDepartmentBySome( string description)
        {
            string sql = "select * from wms_account_flex where description=@description";
            DB.connect();
            SqlParameter[] parameters ={
                                           
                                           new SqlParameter("description",description)
                                       };
            DataSet ds = DB.select(sql, parameters);

            List<ModelDepartment> ModelDepartmentlist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelDepartmentlist = new List<ModelDepartment>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelDepartmentlist.Add(toModel(DateSetRows));
                }
                return ModelDepartmentlist;
            }
            else
            {
                return ModelDepartmentlist;
            }
        }

        public List<string> getAllFlex_value()
        {

            string sql = "select flex_value from wms_account_flex";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["flex_value"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        public List<string> getAllDescription()
        {

            string sql = "select description from wms_account_flex";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["description"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

       


        // 传入DataRow,将其转换为ModelDepartment
        private ModelDepartment toModel(DataRow dr)
        {
            ModelDepartment model = new ModelDepartment();

            //通过循环为ModelDepartment赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelDepartment).GetProperties())
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