using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1;

namespace WMS_v1._0.DataCenter
{
    public class Reinspect_parameterDC
    {
        /// <summary>
        /// 得到复验参数设定表的所有数据
        /// </summary>
        /// <returns></returns>
        public DataSet getAllReinspect_parameters()
        {
            string sql = "select * from wms_reinspect_parameters ";

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
        /// 插入一条数据
        /// </summary>
        /// <param name="pn_head"></param>
        /// <param name="reinspect_week"></param>
        /// <param name="reinspect_qty"></param>
        /// <returns></returns>
        public DataSet insertReinspect_parameters(string pn_head, string reinspect_week, string reinspect_qty)
        {
            string sql = "insert into wms_reinspect_parameters(pn_head, reinspect_week, reinspect_qty) values(@pn_head, @reinspect_week, @reinspect_qty)";

            SqlParameter[] parameters = {
                new SqlParameter("pn_head", pn_head),
                new SqlParameter("reinspect_week", reinspect_week),
                new SqlParameter("reinspect_qty", reinspect_qty)
            };

            DB.connect();
            int flag = DB.insert(sql, parameters);

            if (flag == 1)
            {
                return getAllReinspect_parameters();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="unique_id"></param>
        /// <returns></returns>
        public DataSet deleteReinspect_parameters(string unique_id)
        {
            string sql = "delete from wms_reinspect_parameters where unique_id = @unique_id ";

            SqlParameter[] parameters = {
                new SqlParameter("unique_id", unique_id)
            };

            DB.connect();
            int flag = DB.delete(sql, parameters);

            if (flag == 1)
            {
                return getAllReinspect_parameters();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更新一套数据
        /// </summary>
        /// <param name="unique_id"></param>
        /// <param name="pn_head"></param>
        /// <param name="reinspect_week"></param>
        /// <param name="reinspect_qty"></param>
        /// <returns></returns>
        public DataSet updateReinspect_parameters(string unique_id, string pn_head, string reinspect_week, string reinspect_qty)
        {
            string sql = "update wms_reinspect_parameters set pn_head = @pn_head, reinspect_week = @reinspect_week, reinspect_qty = @reinspect_qty, update_time = GETDATE() where unique_id = @unique_id ";

            SqlParameter[] parameters = {
                new SqlParameter("unique_id", unique_id),
                new SqlParameter("pn_head", pn_head),
                new SqlParameter("reinspect_week", reinspect_week),
                new SqlParameter("reinspect_qty", reinspect_qty)
            };

            DB.connect();
            int flag = DB.update(sql, parameters);

            if (flag == 1)
            {
                return getAllReinspect_parameters();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="pn_head"></param>
        /// <param name="reinspect_week"></param>
        /// <returns></returns>
        public DataSet searchReinspect_parameters(string pn_head, string reinspect_week)
        {
            string sql = "select * from wms_reinspect_parameters where 1=1 ";

            if (!String.IsNullOrWhiteSpace(pn_head))
            {
                sql += "AND pn_head like '%' + @pn_head + '%' ";
            }
            if (!String.IsNullOrWhiteSpace(reinspect_week))
            {
                sql += "AND reinspect_week like '%' + @reinspect_week + '%' ";
            }

            SqlParameter[] parameters = {
                new SqlParameter("pn_head", pn_head),
                new SqlParameter("reinspect_week", reinspect_week)
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

        public bool checkItem_name(String item_name)
        {
            string sql = "select * from wms_reinspect_parameters where @item_name like pn_head + '%'";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name)
            };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            if (ds.Tables[0].Rows.Count > 0) return true;
            return false;

        }
    }
}