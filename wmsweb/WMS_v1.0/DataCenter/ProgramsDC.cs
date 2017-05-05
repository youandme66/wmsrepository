using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1;

namespace WMS_v1._0.DataCenter
{
    public class ProgramsDC
    {
        /// <summary>
        /// 得到界面表的全部数据
        /// </summary>
        /// <returns></returns>
        public DataSet getAllPrograms()
        {
            string sql = "select * from wms_programs ";

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

        /// <summary>
        /// 插入一条界面表数据
        /// </summary>
        /// <param name="program_name"></param>
        /// <param name="description"></param>
        /// <param name="enabled"></param>
        /// <param name="create_by"></param>
        /// <returns></returns>
        public bool insertProgram(string program_name, string description, string enabled, int create_by)
        {
            string sql = "insert into wms_programs(program_name, description, enabled, create_by) values(@program_name, @description, @enabled, @create_by) ";

            SqlParameter[] parameters = { 
                new SqlParameter("program_name", program_name),                            
                new SqlParameter("description", description),                            
                new SqlParameter("enabled", enabled),                            
                new SqlParameter("create_by", create_by)                            
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
        /// 删除一条界面表数据
        /// </summary>
        /// <param name="program_id"></param>
        /// <returns></returns>
        public bool deleteProgram(int program_id)
        {
            string sql = "delete from wms_programs where program_id = @program_id ";

            SqlParameter[] parameters = { 
                new SqlParameter("program_id", program_id)                           
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
        /// 更新一条界面表数据
        /// </summary>
        /// <param name="program_id"></param>
        /// <param name="program_name"></param>
        /// <param name="description"></param>
        /// <param name="enabled"></param>
        /// <param name="update_by"></param>
        /// <returns></returns>
        public bool updateProgram(int program_id, string program_name, string description, string enabled, int update_by)
        {
            string sql = "update wms_programs set program_name = @program_name, description = @description, enabled = @enabled, update_by = @update_by, update_time = GETDATE() where program_id = @program_id";
            
            SqlParameter[] parameters = {
                new SqlParameter("program_id", program_id),                            
                new SqlParameter("program_name", program_name),                            
                new SqlParameter("description", description),                            
                new SqlParameter("enabled", enabled),                          
                new SqlParameter("update_by", update_by)                  
            };

            DB.connect();
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

         public DataSet searchProgram(string program_name, string enabled, string description)
        {
            string sql = "select *,CREATE_name=(select user_name from wms_users where user_id=wms_programs.create_by),UPDATE_name=(select user_name from wms_users where user_id=wms_programs.update_by)  from wms_programs where 1=1 ";

            if (!String.IsNullOrWhiteSpace(program_name))
            {
                sql += "AND program_name = @program_name  ";
            }
            if (!String.IsNullOrWhiteSpace(enabled))
            {
                sql += "AND enabled = @enabled ";
            }
            if (!String.IsNullOrWhiteSpace(description))
            {
                sql += "AND description like '%' + @description + '%' ";
            }
            
            SqlParameter[] parameters = {
                new SqlParameter("program_name", program_name),                            
                new SqlParameter("enabled", enabled),                            
                new SqlParameter("description", description),                            
                                
            };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count>0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

         /**作者周雅雯，时间：2016/11/18
         * wms_programs表中的所有的program_name数据
         * **/
         public List<string> getAllProgram_name()
         {

             string sql = "select program_name from wms_programs   ";

             DB.connect();
             DataSet ds = DB.select(sql, null);

             List<string> modellist = new List<string>();

             if (ds != null && ds.Tables[0].Rows.Count > 0)
             {
                 foreach (DataRow dr in ds.Tables[0].Rows)
                 {
                     modellist.Add(dr["program_name"].ToString());
                 }
                 return modellist;
             }
             else
             {
                 return null;
             }
         }

         /**作者周雅雯，时间：2016/9/2
         * wms_programs表中的所有where enabled=‘Y’（可用的）的program_name数据
         * **/
         public List<string> getAllEnabledProgram_name()
         {

             string sql = "select program_name from wms_programs where enabled='Y'  ";

             DB.connect();
             DataSet ds = DB.select(sql, null);

             List<string> modellist = new List<string>();

             if (ds != null && ds.Tables[0].Rows.Count > 0)
             {
                 foreach (DataRow dr in ds.Tables[0].Rows)
                 {
                     modellist.Add(dr["program_name"].ToString());
                 }
                 return modellist;
             }
             else
             {
                 return null;
             }
         }
    }
}