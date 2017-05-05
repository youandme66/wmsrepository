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

namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/7/26
{
    public class Material_ioDC//在手数量明细表（wms_material_io）对应的DataCenter
    {
        //通过item_name将Onhand_Qty修改成Onhand_Qty-return_qty   PO退回对应的操作
        public Boolean updateOnhand_QtyByItem_nameAndReturn_qty(string Item_name, int Return_qty)
        {

            string sql = "update wms_material_io "
                        + "set Onhand_Qty = Onhand_Qty-@Return_qty "
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



        public int getOnhand_QtyBySome(int item_id, int frame_key, string subinventory, string datecode)
        {

            string sql = "select onhand_qty from wms_material_io "
                        + "where item_id=@item_id and frame_key=@frame_key and subinventory=@subinventory and datecode=@datecode ";

            SqlParameter[] parameters = {
                new SqlParameter("item_id", item_id),
                new SqlParameter("frame_key", frame_key),
                new SqlParameter("subinventory", subinventory),
                new SqlParameter("datecode", datecode)
            };
            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count > 0)   //查询操作成功
            {
                int onhand_qty = int.Parse(ds.Tables[0].Rows[0]["onhand_qty"].ToString());
                return onhand_qty;
            }
            else
                return -1;

        }
        //通过料号、库别、料架查询库存明细表信息      作者：姜江     修改时间：2016/11/19
        public List<ModelMaterial_io> getItems_onhand_qty_detailByITEM_NAMEandSubinventoryandFrame_key(string item_name, string subinventory, string frame_name)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_material_io where item_id = (select item_id from wms_pn where item_name = @item_name) and subinventory=@subinventory and frame_key=(select frame_key from wms_frame where frame_name = @frame_name)";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
                new SqlParameter("subinventory", subinventory),
                new SqlParameter("frame_name", frame_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelMaterial_io> ModelMaterial_iolist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelMaterial_iolist = new List<ModelMaterial_io>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelMaterial_iolist.Add(toModel(DateSetRows));
                }
                return ModelMaterial_iolist;
            }
            else
            {
                return ModelMaterial_iolist;
            }
        }

        public DataSet getItems_onhand_qty_detailByITEM_NAME(string item_name)
        {

            //通过SQL语句，获取DateSet
            string sql = "select wms_material_io.onhand_qty,wms_material_io.subinventory,wms_material_io.datecode,wms_pn.item_name,wms_frame.frame_name from wms_material_io,wms_pn,wms_frame where wms_material_io.item_id = (select item_id from wms_pn where item_name = @item_name) and wms_frame.frame_key=wms_material_io.frame_key and  wms_pn.item_id=wms_material_io.item_id";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
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


        public DataSet getWo_noSeachByWo_no(string wo_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select wms_items_onhand_qty_detail.onhand_quantiy,wms_items_onhand_qty_detail.subinventory,wms_items_onhand_qty_detail.item_name,wms_items_onhand_qty_detail.create_time,wms_wo.wo_no,wms_wo.target_qty from wms_items_onhand_qty_detail,wms_wo where wms_items_onhand_qty_detail.item_name = (select part_no from wms_wo where wo_no = @wo_no) and wms_wo.wo_no=@wo_no";

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

        public bool updateStatus(string item_name, string datecode, string frame_name, string status,DateTime time)
        {
            string sql = "update wms_material_io set last_reinspect_status=@status,last_reinspect_time=@nowtime " +
                "where item_id = (select item_id from wms_pn where item_name = @item_name) and " +
                "datecode = @datecode and frame_key = (select frame_key from WMS_frame where frame_name = @frame_name)";
            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
                new SqlParameter("datecode", datecode),
                new SqlParameter("frame_name", frame_name),
                new SqlParameter("status", status),
                new SqlParameter("nowtime", time),
            };
            DB.connect();

            int num = DB.update(sql, parameters);
            if (num > 0)
                return true;
            else return false;

        }

        private ModelMaterial_io toModel(DataRow dr)
        {
            ModelMaterial_io model = new ModelMaterial_io();

            //通过循环为ModelItems_onhand_qty_detail赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelMaterial_io).GetProperties())
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