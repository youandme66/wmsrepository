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

namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/8/16
{
    public class ParametersDC//参数表（wms_parameters）对应的DateCenter
    {

        //查询出整张表中的信息
        public List<ModelParameters> getParameters()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT * FROM wms_parameters";

            SqlParameter[] parameters = null;

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelParameters> modelParameters_list = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                modelParameters_list = new List<ModelParameters>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelParameters_list.Add(toModel(datarow));
                }
            }
            return modelParameters_list;
        }

        //通过数据表名Lookup_type，查询参数表信息
        public List<ModelParameters> getParametersByLookup_type(int Lookup_type)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_parameters where Lookup_type = @Lookup_type";

            SqlParameter[] parameters = {
                new SqlParameter("Lookup_type", Lookup_type)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelParameters> modelParameters_list = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                modelParameters_list = new List<ModelParameters>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelParameters_list.Add(toModel(datarow));
                }
            }
            return modelParameters_list;
        }



        /**作者：周雅雯 时间：2016/8/16
         * 参数设定页面需要的查询方法
         * 注意：此处因为要将数据库字段int型进行模糊查询，比如operation_seq_num，故传参时传string而不是int，后面进行数据库上的动态转换
         **/
        public List<ModelParameters> getPnBySome(string lookup_type, string lookup_code, string meaning, string description, string enabled)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当lookup_type有值时
            if (string.IsNullOrWhiteSpace(lookup_type) == false)
            {
                sqlTail += "AND CONVERT(NVARCHAR(10),lookup_type) LIKE '%'+@lookup_type+'%'   ";
            }
            //当lookup_code有值时
            if (string.IsNullOrWhiteSpace(lookup_code) == false)
            {
                sqlTail += "AND lookup_code LIKE '%'+@lookup_code+'%' ";
            }
            //当meaning有值时
            if (string.IsNullOrWhiteSpace(meaning) == false)
            {
                sqlTail += "AND meaning LIKE '%'+@meaning+'%' ";
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
                sqlAll = "SELECT * FROM wms_parameters ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_parameters WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                new SqlParameter("lookup_type",lookup_type),
                new SqlParameter("lookup_code",lookup_code),
                new SqlParameter("meaning",meaning),
                new SqlParameter("description",description),
                new SqlParameter("enabled",enabled),
            };

            DataSet ds = DB.select(sqlAll, parameters);

            List<ModelParameters> ModelParameterslist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelParameterslist = new List<ModelParameters>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelParameterslist.Add(toModel(DateSetRows));
                }
                return ModelParameterslist;
            }
            else
            {
                return ModelParameterslist;
            }
        }

        //向参数表中插入数据
        public Boolean insertParameters(int lookup_type, string lookup_code, string meaning, string description, string enabled,int create_by,DateTime create_time)
        {

            string sql = "insert into wms_parameters "
                       + "(lookup_type,lookup_code,meaning,description,enabled,create_by,create_time)values "
                       + "(@lookup_type,@lookup_code,@meaning,@description,@enabled,@create_by,@create_time) ";

            SqlParameter[] parameters = {
                new SqlParameter("lookup_type",lookup_type),
                new SqlParameter("lookup_code",lookup_code),
                new SqlParameter("meaning",meaning),
                new SqlParameter("description",description),
                new SqlParameter("enabled",enabled),
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

        //更新参数表中的部分数据
        public Boolean updateParameters(int lookup_type, string lookup_code, string meaning, string description, string enabled,int update_by,DateTime update_time)
        {
            string sql = "update wms_parameters "
                        + "set lookup_type = @lookup_type,lookup_code = @lookup_code,meaning = @meaning,description = @description ,enabled = @enabled,update_by=@update_by,update_time=@update_time "
                        + "where lookup_type = @lookup_type";

            SqlParameter[] parameters = {
                new SqlParameter("lookup_type",lookup_type),
                new SqlParameter("lookup_code",lookup_code),
                new SqlParameter("meaning",meaning),
                new SqlParameter("description",description),
                new SqlParameter("enabled",enabled),
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

        //删除参数表中的单条数据
        public Boolean deleteParameters(int lookup_type)
        {
            string sql = "delete from wms_parameters "
                        + "where lookup_type = @lookup_type";

            SqlParameter[] parameters = {
                new SqlParameter("lookup_type",lookup_type)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        // 传入DataRow,将其转换为ModelParameters
        private ModelParameters toModel(DataRow dr)
        {
            ModelParameters model = new ModelParameters();

            //通过循环为ModelParameters赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelParameters).GetProperties())
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