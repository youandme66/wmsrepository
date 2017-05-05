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

namespace WMS_v1._0.DataCenter
{
    //“库存管理”菜单栏里页面设计到的方法
    public class StorageDC    
    {
        // 获得料号表中所有料名 
        public List<string> getAllItem_name()
        {
            string sql = "select item_name from wms_pn  ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["item_name"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        //获取料架表中的所有料架
        public List<string> getAllFrame_name()
        {

            string sql = "select frame_name from WMS_frame ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["frame_name"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }



        //获取库存明细表中所有数据(涉及id或key值的字段连同name也要查出来,还需要库存名，为了和主表连接)
        public DataSet getStorage_DetailAndItem_nameAndFrame_nameAndSubinventory_name()
        {

            //通过SQL语句，获取DateSet
            string sql = "select m.*,p.item_name,f.frame_name,s.subinventory_name from wms_material_io m join wms_pn p on p.item_id = m.item_id join WMS_frame f on f.frame_key = m.frame_key join wms_subinventory s on s.subinventory_key = (select subinventory from WMS_region where region_key = (select region_key = (select region_key from WMS_frame where frame_key = 1)))";

            SqlParameter[] parameters = {
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);


            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        
            //获取料架表中的所有料架
        public List<string> getAllSubinventory_name()
        {

            string sql = "select subinventory_name from WMS_subinventory ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

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


        //库存查询：模糊查询库存总表
        public DataSet getStorageBySome(string item_name, string subinventory)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name LIKE '%'+@item_name+'%' ";
            }
            //当subinventory_name有值时
            if (string.IsNullOrWhiteSpace(subinventory) == false)
            {
                sqlTail += "AND subinventory LIKE '%'+@subinventory +'%' ";
            }
         
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_items_onhand_qty_detail ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_items_onhand_qty_detail WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("subinventory", subinventory)
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
















































































        /* 交易明细查询
         * 查询交易表信息
         */
        public DataSet getTransactionBySome(string transaction_type, DateTime start_time, DateTime end_time, string item_name, string invoice_no, string po_no)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当transaction_type有值时
            if (string.IsNullOrWhiteSpace(transaction_type) == false)
            {
                sqlTail += "AND transaction_type LIKE '%'+@transaction_type+'%' ";
            }
            //对start_time和end_time做处理，默认他们两个都有值
            sqlTail += "AND  transaction_time BETWEEN @start_time AND @end_time ";


            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name LIKE '%'+@item_name+'%' ";
            }
            //当invoice_no有值时
            if (string.IsNullOrWhiteSpace(invoice_no) == false)
            {
                sqlTail += "AND invoice_no LIKE '%'+@invoice_no+'%' ";
            }
            //当po_no有值时
            if (string.IsNullOrWhiteSpace(po_no) == false)
            {
                sqlTail += "AND po_no LIKE '%'+@po_no+'%' ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_transaction_operation ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_transaction_operation WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("transaction_type", transaction_type),
                    new SqlParameter("start_time", start_time),
                    new SqlParameter("end_time", end_time),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("invoice_no", invoice_no),
                    new SqlParameter("po_no", po_no),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds!=null&&ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
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