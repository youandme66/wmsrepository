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

namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/8/17
{
    public class WatchdogDC//权限表（wms_watchdog）对应的DateCenter
    {
        //向权限表中插入数据  权限表设定页面
        public Boolean insertWatchdog(string user_name, string program_name, string enabled, string create_by, string update_by, DateTime create_time)
        {

            string sql = "insert into wms_watchdog "
                       + "(user_id,user_name,program_name,program_id,enabled,create_by,update_by,create_time)values "
                       + "((select user_id from wms_users where user_name=@user_name),@user_name,@program_name,(select program_id from wms_programs where program_name=@program_name),@enabled,@create_by,@update_by,@create_time) ";

            SqlParameter[] parameters = {
                new SqlParameter("user_name",user_name),
                new SqlParameter("program_name",program_name),
                new SqlParameter("enabled",enabled),
                new SqlParameter("create_by",create_by),
                new SqlParameter("update_by",update_by),
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


        //删除权限表中的单条数据 权限表设定页面所需删除方法
        public Boolean deleteWatchdog(int id)
        {
            string sql = "delete from wms_watchdog where id=@id";

            SqlParameter[] parameters = {
                new SqlParameter("id",id),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //更新权限表中的部分数据 权限表设定页面所需更新方法
        public Boolean updateWatchdog(int id, string enabled, string update_by, DateTime update_time)
        {
            string sql = "update wms_watchdog "
                        + "set enabled = @enabled,update_by = @update_by,update_time=@update_time "
                        + "where id=@id";

            SqlParameter[] parameters = {
                new SqlParameter("id",id),     
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

        /**作者：周雅雯 时间：2016/11/15
         * 料号设定页面需要的查询方法
         **/
        public List<ModelWatchdog> getWatchdogBySome(string user_name,string program_name, string enabled)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";
            //当user_id有值时
            if (string.IsNullOrWhiteSpace(user_name) == false)
            {
                sqlTail += "AND user_name =@user_name ";
            }
            //当program_name有值时
            if (string.IsNullOrWhiteSpace(program_name) == false)
            {
                sqlTail += "AND program_name =@program_name  ";
            }
            //当enabled有值时
            if (string.IsNullOrWhiteSpace(enabled) == false)
            {
                sqlTail += "AND enabled =@enabled ";
            }
          
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_watchdog ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_watchdog WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("user_name", user_name),
                    new SqlParameter("program_name", program_name),
                    new SqlParameter("enabled", enabled)
                };

            DataSet ds = DB.select(sqlAll, parameters);

            List<ModelWatchdog> ModelWatchdoglist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelWatchdoglist = new List<ModelWatchdog>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelWatchdoglist.Add(toModel(DateSetRows));
                }
                return ModelWatchdoglist;
            }
            else
            {
                return ModelWatchdoglist;
            }
        }

        /**作者：周雅雯 时间：2016/8/17
        * 料号设定页面需要的查询方法
        * 注意：此处因为要将数据库字段int型进行模糊查询，比如operation_seq_num，故传参时传string而不是int，后面进行数据库上的动态转换
        **/
        public List<ModelWatchdog> getWatchdogBySome(string user_name)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            //当user_name有值时
            if (string.IsNullOrWhiteSpace(user_name) == false)
            {
                sqlAll += "SELECT * FROM wms_watchdog where user_name=@user_name   ";
            }       
            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("user_name", user_name),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            List<ModelWatchdog> ModelWatchdoglist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelWatchdoglist = new List<ModelWatchdog>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelWatchdoglist.Add(toModel(DateSetRows));
                }
                return ModelWatchdoglist;
            }
            else
            {
                return ModelWatchdoglist;
            }
        }


        // 传入DataRow,将其转换为ModelWatchdog
        private ModelWatchdog toModel(DataRow dr)
        {
            ModelWatchdog model = new ModelWatchdog();

            //通过循环为ModelWatchdog赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelWatchdog).GetProperties())
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