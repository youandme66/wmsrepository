using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;

namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/11/6
{
    public class Wip_operationDC//制程信息表（wms_wip_operations）对应的DateCenter
    {
        /**
         * 根据制程代号 返回制程ID 
         * */
        public int getRoute_idByRoute(string route)
        {
            string sql = "select Route_id from wms_wip_operations where route=@route  ";


            SqlParameter[] parameters = {
                new SqlParameter("route", route)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)   //查询操作成功
            {
                int Route_id = int.Parse(ds.Tables[0].Rows[0]["Route_id"].ToString());
                return Route_id;
            }
            else
                return -1;
        }




        //查询并返回wms_wip_operation中所有的route制程代号
        public DataSet getRegion_nameBySubinventory_name()
        {
            string sql = "select route_id,route from wms_wip_operations ";

            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// 得到制程表全部数据
        /// </summary>
        /// <returns></returns>
        public DataSet getAllWip_operation()
        {
            string sql = "select * from wms_wip_operations ";

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

        public DataSet getAllRoute()
        {
            string sql = "select [route],route_id from wms_wip_operations ";
            DB.connect();
            DataSet ds = DB.select(sql, null);

            if (ds != null)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// 插入一条制程数据
        /// </summary>
        /// <param name="route"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public DataSet insertWip_operation(string route, string description, string create_by)
        {
            string sql = "insert into wms_wip_operations(route, description, create_by) values(@route, @description, @create_by)";

            SqlParameter[] parameters = { 
                new SqlParameter("route", route),
                new SqlParameter("description", description),
                new SqlParameter("create_by", create_by)
            };

            DB.connect();
            int flag = DB.insert(sql, parameters);

            if (flag == 1)
            {
                return getAllWip_operation();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除一条制程数据
        /// </summary>
        /// <param name="route_id"></param>
        /// <returns></returns>
        public DataSet deleteWip_operation(string route) 
        {
            string sql = "delete from wms_wip_operations where route = @route ";

            SqlParameter[] parameters = { 
                new SqlParameter("route", route)           
            };

            DB.connect();
            int flag = DB.delete(sql, parameters);

            if (flag == 1)
            {
                return getAllWip_operation();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更新制程表数据
        /// </summary>
        /// <param name="route_id"></param>
        /// <param name="route"></param>
        /// <param name="description"></param>
        /// <param name="update_by"></param>
        /// <returns></returns>
        public DataSet updateWip_operation(string route_id, string route, string description, string update_by)
        {
            string sql = "update wms_wip_operations set route = @route, description = @description, update_by = @update_by, update_time = GETDATE() where route_id = @route_id ";
            
            SqlParameter[] parameters = {
                new SqlParameter("route_id", route_id),                            
                new SqlParameter("route", route),                            
                new SqlParameter("description", description),                            
                new SqlParameter("update_by", update_by)                          
            };

            DB.connect();
            int flag = DB.update(sql, parameters);

            if (flag == 1)
            {
                return getAllWip_operation();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="route"></param>
        /// <param name="create_by"></param>
        /// <param name="update_by"></param>
        /// <returns></returns>
        public DataSet searchWip_operation(string route, string create_by, string update_by)
        {
            string sql = "select * from wms_wip_operations where 1=1 ";

            if(!String.IsNullOrWhiteSpace(route))
            {
                sql += "AND route like '%' + @route + '%' ";
            }
            if (!String.IsNullOrWhiteSpace(create_by))
            {
                sql += "AND create_by like '%' + @create_by + '%' ";
            }
            if (!String.IsNullOrWhiteSpace(update_by))
            {
                sql += "AND update_by like '%' + @update_by + '%' ";
            }

            SqlParameter[] parameters = {
                new SqlParameter("route", route),                            
                new SqlParameter("create_by", create_by),                            
                new SqlParameter("update_by", update_by)                            
            };

            DB.connect();
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

    }
}