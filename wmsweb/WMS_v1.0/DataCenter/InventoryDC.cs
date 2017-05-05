using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1;

namespace WMS_v1._0.DataCenter  //作者：周雅雯 最后一次修改时间：2016/8/14
{
    public class InventoryDC    //库存DC，没有对应Model，直接返回数据集
    {


        //获取库存总表
        public DataSet getITEMS_ONHAND_QTY_DETAIL(string Item_name, string Subinventory_name)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_material_io 后的内容，即查询条件
            string sqlTail = "";

            //当Item_name有值时
            if (string.IsNullOrWhiteSpace(Item_name) == false)
            {
                sqlTail += "AND item_name=@Item_name ";
            }
            //当Subinventory_name有值时
            if (string.IsNullOrWhiteSpace(Subinventory_name) == false)
            {
                sqlTail += "AND Subinventory =@Subinventory_name ";
            }
          
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM  WMS_ITEMS_ONHAND_QTY_DETAIL ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM  WMS_ITEMS_ONHAND_QTY_DETAIL where 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("Item_name", Item_name),
                    new SqlParameter("Subinventory_name", Subinventory_name),
                 
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        //获取库存明细表
        public DataSet getMaterial_io(string Item_name, string Subinventory_name)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_material_io 后的内容，即查询条件
            string sqlTail = "";

            //当Item_name有值时
            if (string.IsNullOrWhiteSpace(Item_name) == false)
            {
                sqlTail += "AND item_id=(select item_id from wms_pn where item_name=@Item_name) ";
            }
            //当Subinventory_name有值时
            if (string.IsNullOrWhiteSpace(Subinventory_name) == false)
            {
                sqlTail += "AND frame_key=(select frame_key from wms_frame where region_key=(select region_key from wms_region where subinventory=(select subinventory_key from wms_subinventory where Subinventory_name =@Subinventory_name))) ";
            }

            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT *,item_name=(select item_name from wms_pn where item_id=wms_material_io.item_id),frame_name=(select frame_name from wms_frame where frame_key=wms_material_io.frame_key) FROM  wms_material_io  ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT *,item_name=(select item_name from wms_pn where item_id=wms_material_io.item_id),frame_name=(select frame_name from wms_frame where frame_key=wms_material_io.frame_key) FROM  wms_material_io  where 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("Item_name", Item_name),
                    new SqlParameter("Subinventory_name", Subinventory_name),
                 
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }



        //获取库存信息  时间：2016/8/14
        public DataSet getInverntory(string Item_name, string Subinventory_name, string DateCode)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_material_io 后的内容，即查询条件
            string sqlTail = "";

            //当Item_name有值时
            if (string.IsNullOrWhiteSpace(Item_name) == false)
            {
                sqlTail += "AND a2.Item_id in(select a2.Item_id from  wms_pn a1,wms_material_io a2  where a1.item_name like '%'+@Item_name+'%' and a2.item_id=a1.item_id ) ";
            }
            //当Subinventory_name有值时
            if (string.IsNullOrWhiteSpace(Subinventory_name) == false)
            {
                sqlTail += "AND Subinventory LIKE '%'+@Subinventory_name+'%' ";
            }
            //当DateCode有值时
            if (string.IsNullOrWhiteSpace(DateCode) == false)
            {
                sqlTail += "AND DateCode LIKE '%'+@DateCode+'%' ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM  wms_pn a1,wms_material_io a2  where  a2.item_id=a1.item_id";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_pn a1,wms_material_io  a2  where a2.item_id=a1.item_id " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("Item_name", Item_name),
                    new SqlParameter("Subinventory_name", Subinventory_name),
                    new SqlParameter("DateCode", DateCode),
                };

            DataSet ds = DB.select(sqlAll, parameters);

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
        /// 通过交易人员，查找交易信息
        /// </summary>
        /// <param name="create_user"></param>
        /// <returns></returns>
        public DataSet getTransactionByUser(string create_user)
        {
            string sql = "select * from wms_transaction_operation where create_user like '%' + @user + '%' ";

            SqlParameter[] parameters = { 
                new SqlParameter("user", create_user)                            
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