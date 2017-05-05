using System;
using System.Data;
using System.Data.SqlClient;
using WebApplication1;
using WMS_v1._0.Model;
using System.Collections.Generic;

namespace WMS_v1._0.DataCenter
{
    public class ReinspectHeaderDC
    {
        /// <summary>
        /// 根据料号，datecode,库别获取 需要复验 的数据
        /// last_reinspect_status =peng 则需要复验
        /// </summary>
        /// <param name="item_name"></param>
        /// <param name="datecode"></param>
        /// <param name="subinventory"></param>
        /// <returns></returns>
        public DataSet get_All_By_itemname_datecode_subiventory(string item_name, string datecode, string subinventory)
        {
            string sql = "select * from wms_material_io " +
                "join wms_pn on wms_pn.item_id = wms_material_io.item_id " +
                "where subinventory = @subinventory and " +
                "wms_pn.item_name = @item_name and " +
                "wms_material_io.datecode = @datecode ";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
                new SqlParameter("datecode", datecode),
                new SqlParameter("subinventory", subinventory)
            };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            return ds;
        }



        //通过ITEM_NAME料号，获取ModelPn对象的列表集合
        public int getSome(string item_name, string datecode, string frame_name, string status)
        {

            //通过SQL语句，获取DateSet
            string sql = "select unique_id from wms_reinspect_header where item_name = @item_name and datecode=@datecode and frame_name=@frame_name and status=@status";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
                new SqlParameter("datecode", datecode),
                new SqlParameter("frame_name", frame_name),
                new SqlParameter("status", status)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {

                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }


        public DataSet get_All(string item_name, string datecode, string frame_name, string status, string remark, string check_user, DateTime check_time)
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
            //当datecode有值时
            if (string.IsNullOrWhiteSpace(datecode) == false)
            {
                sqlTail += "AND datecode LIKE '%'+@datecode+'%' ";
            }
            //当frame_name有值时
            if (string.IsNullOrWhiteSpace(frame_name) == false)
            {
                sqlTail += "AND frame_name=@frame_name ";
            }
            //当status有值时
            if (string.IsNullOrWhiteSpace(status) == false)
            {
                sqlTail += "AND status=@status";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_reinspect_header ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_reinspect_header WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("datecode", datecode),
                    new SqlParameter("frame_name", frame_name),
                    new SqlParameter("status", status),
                };

            DataSet ds = DB.select(sqlAll, parameters);


            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                return ds;
            }
            else
            {
                return null;
            }
        }





        public bool insertReinspectHeader(string item_name, string datecode, string frame_name, string status, string remark, string check_user, DateTime check_time)
        {
            string sql = "insert into wms_reinspect_header " +
                "(item_name,item_id,datecode,frame_name,status,remark,check_user,check_time) values " +
                "(@item_name,(select wms_pn.item_id from wms_pn where wms_pn.item_name=@item_name),@datecode,@frame_name,@status,@remark,@check_user,@check_time)";


            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
                new SqlParameter("datecode", datecode),
                new SqlParameter("frame_name", frame_name),
                new SqlParameter("status", status),
                new SqlParameter("remark", remark),
                new SqlParameter("check_user", check_user),
                new SqlParameter("check_time", check_time)
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
        /// 根据料号，datecode,库别查询 wms_reinspect_header中该料号是否已复验
        /// </summary>
        /// <param name="item_name"></param>
        /// <param name="datecode"></param>
        /// <param name="subinventory"></param>
        /// <returns></returns>
        public DataSet select_By_itemname_datecode_sub(string item_name, string datecode, string subinventory)
        {
            string sql = "select * from wms_reinspect_header " +
                "where item_name = @item_name and datecode = @datecode and subinventory_name = @subinventory ";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
                new SqlParameter("datecode", datecode),
                new SqlParameter("subinventory", subinventory)
            };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }

        //复验作业下拉框内容
        //public List<string> getAllPn_Header()
        //{
        //    string sql = "select pn_head from wms_reinspect_parameters ";

        //    DB.connect();
        //    DataSet ds = DB.select(sql, null);

        //    List<string> modellist = new List<string>();
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            modellist.Add(dr["pn_head"].ToString());
        //        }
        //        return modellist;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


        //查询出last_reinspect_status的信息，通过item_id,datecode,frame_key
        public List<string> getLineNumByPoHeaderId(int item_id, string datecode, int frame_key)
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT last_reinspect_status FROM wms_material_io where item_id=@item_id and  datecode=@datecode and  frame_key=@frame_key";

            SqlParameter[] parameters = {
                     new SqlParameter("item_id", item_id),
                     new SqlParameter("datecode", datecode),
                     new SqlParameter("frame_key", frame_key),
                };

            DB.connect();


            DataSet ds = DB.select(sql, parameters);


            List<string> last_reinspect_status__list = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    last_reinspect_status__list.Add(dr["last_reinspect_status"].ToString());
                }
                return last_reinspect_status__list;
            }
            else
            {
                return null;
            }
        }

        public DataSet getallpending(int curdate)
        {
            //通过SQL语句，获取DateSet
            string sql = "select distinct a.*,b.item_name,wms_frame.frame_name from wms_material_io a " +
                "join wms_pn b on a.item_id = b.item_id "+
                "join wms_frame on wms_frame.frame_key = a.frame_key and wms_frame.enabled ='Y' " +
                "join wms_reinspect_parameters c on b.item_name like c.pn_head + '%' " +
                "where "+
                "a.onhand_qty>0 and a.last_reinspect_status ='PENDING' order by b.item_name ";

            SqlParameter[] parameters = {
                new SqlParameter("curdate", curdate),
            };

            DB.connect();

            return DB.select(sql, parameters);

            
        }

        public DataSet getallpass()
        {
            //通过SQL语句，获取DateSet
            string sql = "select * from wms_reinspect_header where status ='PASS' order by check_time desc";
            
            DB.connect();

            return  DB.select(sql, null);
        }

        public DataSet getTime(int curdate)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_reinspect_header where (convert(int,datecode)-convert(int,@curdate))<=1 ";

            SqlParameter[] parameters = {
                new SqlParameter("curdate", curdate),
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {

                return ds;
            }
            else
            {
                return null;
            }
        }

        public Boolean updateReinspectHeader(int unique_id,string status, string remark, string check_user, DateTime check_time)
        {
            string sql = "update wms_reinspect_header set status = @status,remark=@remark,check_user=@check_user,check_time=@check_time " +
                "where unique_id=@unique_id ";

            SqlParameter[] parameters = {
                new SqlParameter("unique_id", unique_id),
                new SqlParameter("status", status),
                new SqlParameter("remark", remark),
                new SqlParameter("check_user", check_user),
                new SqlParameter("check_time", check_time)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }
        
    }
}