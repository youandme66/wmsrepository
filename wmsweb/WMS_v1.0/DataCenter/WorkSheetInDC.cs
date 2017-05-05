using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1;

namespace WMS_v1._0.DataCenter
{
    public class WorkSheetInDC
    {
        //通过工單表中的工單号wo_no，查询出工單入庫打印所需的数据
        public DataSet getSomeByWo_no(string wo_no)
        {
            //通过SQL语句，获取DateSet
            string sql = "select  *,item_name=(select wms_pn.item_name from wms_pn where item_id=wms_material_io.item_id),frame_name=(select WMS_frame.frame_name from WMS_frame where frame_key=wms_material_io.frame_key) from  wms_material_io where item_id=(select item_id from wms_pn where item_name =  (select part_no from wms_wo where wo_no = @wo_no))";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no", wo_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)
                return ds;
            else
                return null;
        }


        public DataSet getItemOnhandQty(string wo_no)
        {
            string sql = "select * from wms_items_onhand_qty_detail where item_name=(select part_no from wms_wo where wo_no=@wo_no)";
            SqlParameter[] parameters ={
                                           new SqlParameter("wo_no",wo_no)
                                       };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ds;
            }
        }
        /// <summary>
        /// 通过工单找到料号id
        /// </summary>
        /// <param name="wo_no"></param>
        /// <returns></returns>
        public string getItem_IdByWO_no(string wo_no) 
        {
            string selectItem_Name = "select part_no from wms_wo where wo_no = @wo_no";
            string selectItem_Id = "select item_id from wms_pn where item_name = @item_name";
            SqlParameter[] item_nameParameter = {
                new SqlParameter("wo_no", wo_no)
            };
            
            DB.connect();
            DataSet ds = DB.select(selectItem_Name, item_nameParameter);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                SqlParameter[] item_idParameter = {
                    new SqlParameter("item_name", ds.Tables[0].Rows[0]["part_no"].ToString())
                };

                DataSet tempDs = DB.select(selectItem_Id, item_idParameter);
                if (tempDs != null && tempDs.Tables[0].Rows.Count > 0)
                {
                    return tempDs.Tables[0].Rows[0]["item_id"].ToString();
                }
                else 
                {
                    return null;
                }
                
            }
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// 通过工单找到库存表，将在手量更新为number
        /// </summary>
        /// <param name="wo_no"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet workSheetIn(string wo_no, int number,string user,string item_name,string subinventory,int frame_key,string datecode)
        {
            
            string updateMaterial = "update wms_material_io set onhand_qty = onhand_qty+@number, update_time = GETDATE(),last_reinspect_time=GETDATE() where item_id = @item_id and frame_key=@frame_key and datecode=@datecode;";
            string insertMaterial = "insert into wms_material_io(item_id,frame_key,datecode,onhand_qty,last_reinspect_status,last_reinspect_time) values(@item_id,@frame_key,@datecode,@number,'PASS',GETDATE());";
            string insertOnhand = "insert into wms_items_onhand_qty_detail(item_name,item_id,onhand_quantiy,subinventory) values(@item_name,@item_id,@number,@subinventory);";
            string updatewo_no = "update wms_wo set turnin_qty=turnin_qty+@number,update_time=GETDATE() where wo_no=@wo_no;";
            string insertTran = "insert wms_transaction_operation  (transaction_qty,transaction_type,item_name,transaction_time,create_user) values (@number,'workSheetIn',@item_name,GETDATE(),@user) ";
            string updateOnhand = "update wms_items_onhand_qty_detail set onhand_quantiy =onhand_quantiy+@number, update_time = GETDATE() where item_id = @item_id and subinventory=@subinventory; ";
            string selectMaterial = " select * from wms_material_io where item_id=@item_id and frame_key=@frame_key and datecode=@datecode;";
            
            string selectsql = "select * from wms_items_onhand_qty_detail where item_id = @item_id and subinventory=@subinventory";

            string item_id = getItem_IdByWO_no(wo_no);
            SqlParameter[] updateparameters = {
                new SqlParameter("item_id", item_id),
                new SqlParameter("number", number), 
                new SqlParameter("user", user),
                new SqlParameter("item_name",item_name),
                new SqlParameter("wo_no",wo_no),
                new SqlParameter("subinventory",subinventory),
                new SqlParameter("frame_key",frame_key),
                new SqlParameter("datecode",datecode)
            };

            SqlParameter[] selectparameter = {
                new SqlParameter("item_id", item_id),
                new SqlParameter("subinventory",subinventory)
            };
            DataSet ds = DB.select(selectsql, selectparameter);
            DataSet ds2 = DB.select(selectMaterial, updateparameters);
            if (ds.Tables[0].Rows.Count == 0 && ds2.Tables[0].Rows.Count==0)
            {
                
                int flag = DB.tran(insertOnhand +insertMaterial+ updatewo_no + insertTran, updateparameters);
                if (flag > 0)
                {
                    return DB.select(selectsql, selectparameter);
                }
                else
                {
                    return null;
                }
            }
            else if(ds.Tables[0].Rows.Count>0&&ds2.Tables[0].Rows.Count==0)
            {
                int flag = DB.tran(updateOnhand+insertMaterial+updatewo_no+insertTran, updateparameters);
                if (flag > 0)
                {
                    return DB.select(selectsql, selectparameter);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                int flag = DB.tran(updateOnhand + updateMaterial + updatewo_no + insertTran, updateparameters);
                if (flag > 0)
                {
                    return DB.select(selectsql, selectparameter);
                }
                else
                {
                    return null;
                }
            }
            
        }

        /// <summary>
        /// 通过工单进行模糊查询
        /// </summary>
        /// <param name="wo_no"></param>
        /// <returns></returns>
        public DataSet search(string wo_no) 
        {
            //通过工单查找料号
            string selectWO = "select * from wms_wo b1, wms_pn b2 where b1.wo_no like '%' + @wo_no + '%' AND b2.item_name = b1.part_no ";
            string sql = "select * from wms_items_onhand_qty_detail b1, wms_material_io b2 where b1.item_name = @item_name AND b2.item_id =@item_id ";

            SqlParameter[] selectWOparameters = { 
                new SqlParameter("wo_no", wo_no)                                 
            };

            DB.connect();
            DataSet ds = DB.select(selectWO, selectWOparameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                DataSet searchds = new DataSet();
                DataSet temp = new DataSet();
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    SqlParameter[] parameters = { 
                        new SqlParameter("item_id", ds.Tables[0].Rows[i]["item_id"]),
                        new SqlParameter("item_name", ds.Tables[0].Rows[i]["item_name"])
                    };

                    

                    temp = DB.select(sql, parameters);

                    if (temp != null && temp.Tables[0].Rows.Count > 0)
                    {
                        searchds.Merge(temp, true, MissingSchemaAction.AddWithKey);
                    }
                    else
                    {
                        return null;
                    }
                    i++;
                }
                return searchds;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// 通过料号得到库存中的在手量
        /// </summary>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public List<string> getOnhand_qtyAndItem_NameByItem_id(string item_id)
        {
            string sql = "select onhand_quantiy, item_name from wms_items_onhand_qty_detail where item_id = @item_id";

            SqlParameter[] parameter = { 
                new SqlParameter("item_id", item_id)                           
            };

            DataSet ds = DB.select(sql, parameter);
            List<string> list = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list.Add(ds.Tables[0].Rows[0]["onhand_quantiy"].ToString());
                list.Add(ds.Tables[0].Rows[0]["item_name"].ToString());
                return list;
            }
            else
            {
                return null;
            }
        }
        //public List<string> getOnhand_qtyAndItem_NameByWo_no(string wo_no)
        //{
        //    string sql = "select onhand_quantiy, item_name from wms_items_onhand_qty_detail where wo_no = @wo_no";

        //    SqlParameter[] parameter = { 
        //        new SqlParameter("wo_no", wo_no)                           
        //    };

        //    DataSet ds = DB.select(sql, parameter);
        //    List<string> list = new List<string>();
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        list.Add(ds.Tables[0].Rows[0]["onhand_quantiy"].ToString());
        //        list.Add(ds.Tables[0].Rows[0]["item_name"].ToString());
        //        return list;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}