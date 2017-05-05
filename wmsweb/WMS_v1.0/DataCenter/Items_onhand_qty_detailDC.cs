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

namespace WMS_v1._0.DataCenter //作者：周雅雯 最后一次修改时间：2016/7/26
{
    public class Items_onhand_qty_detailDC //在手总量表（Items_onhand_qty_detail）对应的DataCenter
    {

        //通过ITEM_NAME料号，获取ModelItems_onhand_qty_detail对象的列表集合
        public List<ModelItems_onhand_qty_detail> getItems_onhand_qty_detailByITEM_NAME(string item_name)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from WMS_ITEMS_ONHAND_QTY_DETAIL where ITEM_NAME = @item_name";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelItems_onhand_qty_detail> ModelItems_onhand_qty_detaillist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelItems_onhand_qty_detaillist = new List<ModelItems_onhand_qty_detail>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelItems_onhand_qty_detaillist.Add(toModel(DateSetRows));
                }
                return ModelItems_onhand_qty_detaillist;
            }
            else
            {
                return ModelItems_onhand_qty_detaillist;
            }
        }


        public List<ModelItems_onhand_qty_detail> getItems_onhand_qty_detailByITEM_NAMEandSubinventory(string item_name, string subinventory)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from WMS_ITEMS_ONHAND_QTY_DETAIL where ITEM_NAME = @item_name and subinventory=@subinventory";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
                new SqlParameter("subinventory", subinventory)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelItems_onhand_qty_detail> ModelItems_onhand_qty_detaillist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelItems_onhand_qty_detaillist = new List<ModelItems_onhand_qty_detail>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelItems_onhand_qty_detaillist.Add(toModel(DateSetRows));
                }
                return ModelItems_onhand_qty_detaillist;
            }
            else
            {
                return ModelItems_onhand_qty_detaillist;
            }
        }


        public DataSet getItems_onhand_qty_detailByWo_no(string wo_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select SUM(onhand_quantiy) AS onhand from WMS_ITEMS_ONHAND_QTY_DETAIL where item_name = (select item_name from wms_wo where wo_no = @wo_no)";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no", wo_no),
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



        //通过模糊查询ITEM_NAME料号，获取ModelItems_onhand_qty_detail对象的列表集合
        public List<ModelItems_onhand_qty_detail> getItems_onhand_qty_detailByLikeITEM_NAME(string item_name)
        {
            //通过SQL语句，获取DateSet
            string sql = "select * from WMS_ITEMS_ONHAND_QTY_DETAIL where ITEM_NAME LIKE @item_name";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelItems_onhand_qty_detail> ModelItems_onhand_qty_detaillist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelItems_onhand_qty_detaillist = new List<ModelItems_onhand_qty_detail>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelItems_onhand_qty_detaillist.Add(toModel(DateSetRows));
                }
                return ModelItems_onhand_qty_detaillist;
            }
            else
            {
                return ModelItems_onhand_qty_detaillist;
            }
        }


        //通过item_name将Onhand_Quantiy修改成Onhand_Quantiy-return_qty   PO退回对应的操作
        public Boolean updateOnhand_QuantiyByItem_nameAndReturn_qty(string Item_name, int Return_qty)
        {

            string sql = "update WMS_ITEMS_ONHAND_QTY_DETAIL "
                        + "set Onhand_Quantiy = Onhand_Quantiy-@Return_qty "
                        + "where Item_id IN (select Item_id from wms_pn where Item_name=@Item_name)";

            SqlParameter[] parameters = { 
                new SqlParameter("Item_name", Item_name),
                new SqlParameter("Return_qty", Return_qty)            
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        // 传入DataRow,将其转换为ModelItems_onhand_qty_detail
        private ModelItems_onhand_qty_detail toModel(DataRow dr)
        {
            ModelItems_onhand_qty_detail model = new ModelItems_onhand_qty_detail();

            //通过循环为ModelItems_onhand_qty_detail赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelItems_onhand_qty_detail).GetProperties())
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


        public Boolean sheetin(string item_name, int deliver_qty, string datecode, string frame_name, string issued_sub_key, DateTime transaction_time, bool flag, bool flag1)
        {
            string sql;
            string sqlSecond;
            string sqlThird;
            string sqlTail;
            if (flag == false)
            {
                //+ "插入庫存總表庫数据"
                sqlSecond = "INSERT INTO wms_items_onhand_qty_detail (item_name,item_id,onhand_quantiy,subinventory,create_time)VALUES(@item_name,(select item_id from wms_pn where item_name = @item_name),@deliver_qty,@issued_sub_key,@transaction_time);";
            }
            else
            {
                //+ "--修改庫存總表庫存量"
                sqlSecond = "update wms_items_onhand_qty_detail set onhand_quantiy = onhand_quantiy + @deliver_qty, update_time=@transaction_time where item_id = (select item_id from wms_pn where item_name = @item_name) and subinventory =@issued_sub_key;";
            }
            if (flag1 == false)
            {
                //+ "插入庫存明细表庫数据"
                sqlThird = "INSERT INTO wms_material_io (item_id,onhand_qty,frame_key,subinventory,datecode,create_time)VALUES((select item_id from wms_pn where item_name = @item_name),@deliver_qty,(select frame_key from wms_frame where frame_name = @frame_name),@issued_sub_key,@datecode,@transaction_time);";
            }
            else
            {
                //+ "--修改庫存明细表庫存量"
                sqlThird = "UPDATE wms_material_io SET onhand_qty=onhand_qty + @deliver_qty, update_time=@transaction_time WHERE  item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name) and subinventory=@issued_sub_key and frame_key=(select frame_key from wms_frame where frame_name = @frame_name);";
            }
            //+ "--将数据插入到交易表"
            sqlTail = "INSERT INTO wms_transaction_operation (transaction_qty,transaction_type,transaction_time) VALUES (@deliver_qty,'workSheetIn',@transaction_time)";

            sql = sqlSecond + sqlThird + sqlTail;

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
                new SqlParameter("frame_name", frame_name),
                new SqlParameter("deliver_qty", deliver_qty),
                new SqlParameter("datecode", datecode),
                new SqlParameter("issued_sub_key", issued_sub_key),
                new SqlParameter("transaction_time", transaction_time)
                };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.tran(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

    }
}