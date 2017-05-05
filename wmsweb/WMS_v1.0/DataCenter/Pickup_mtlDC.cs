using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1;

namespace WMS_v1._0.DataCenter
{
    public class Pickup_mtlDC
    {
        //通过備料表中的備料单号simulate_id，查询出備料單打印所需的数据
        public DataSet getSomeBySimulate_id(int simulate_id)
        {
            
            //通过SQL语句，获取DateSet
            string sql = "select *,operation_seq_num_name=(select wms_wip_operations.route from wms_wip_operations where route_id=operation_seq_num),subinventory_name=(select subinventory_name from wms_subinventory where subinventory_key=wms_pickup_mtl.subinventory_key)  from  wms_pickup_mtl where simulate_id=@simulate_id ";

            SqlParameter[] parameters = {
                new SqlParameter("simulate_id", simulate_id)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// 发料
        /// </summary>
        /// <param name="simulate_id"></param>
        /// <returns></returns>
        public DataSet Issue(int simulate_id, int number,int item_id)
        {
            string updatePickup_mtl = "update wms_pickup_mtl set issued_qty = issued_qty+@number, issued_status = 'Y', update_time = GETDATE() "
                + " where simulate_line_id = @simulate_id and item_id=@item_id;";
            string updateSimulate_operation = "update wms_simulate_operation set issued_qty = issued_qty+@number, status = N'发料' "
                + "where simulate_id = @simulate_id and item_id=@item_id";
            string sql = "select a.simulate_id,a.simulate_line_id,a.item_id,a.item_name,b.frame_name,a.pickup_qty,a.issued_qty,a.datecode,a.operation_seq_num,a.issued_status,a.create_time,a.update_time from wms_pickup_mtl a inner join WMS_frame b on a.frame_key=b.frame_key where a.simulate_line_id=@simulate_id and a.item_id=@item_id";
            SqlParameter[] parameteres = { 
                new SqlParameter("simulate_id", simulate_id),
                new SqlParameter("number", number),
                new SqlParameter("item_id",item_id)             
            };

            DB.connect();
            int flag = DB.tran(updatePickup_mtl + updateSimulate_operation, parameteres);
            if (flag == 1)
            {
               DataSet ds=DB.select(sql, parameteres);
               return ds;

            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到全部备料数据
        /// </summary>
        /// <returns></returns>
        private DataSet getAll()
        {
            string sql = "select b1.simulate_id,b1.update_time,b2.*, b3.*, b4.*  from wms_pickup_mtl b1, wms_simulate_operation b2, wms_items_onhand_qty_detail b3, wms_material_io b4 where b3.item_id = b1.item_id and b4.item_id = b1.item_id and b2.simulate_id = b1.simulate_id ";
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
        //通过备料单号搜索备料单
        public DataSet searchPickupBySimulate(string simulate_id)
        {
            string sql = "select a.simulate_id,a.simulate_line_id,a.item_id,a.item_name,b.frame_name,a.pickup_qty,a.issued_qty,a.datecode,a.operation_seq_num,a.issued_status,a.create_time,a.update_time from wms_pickup_mtl a inner join WMS_frame b on a.frame_key=b.frame_key where a.simulate_line_id like '%'+@simulate_id+'%'";
            SqlParameter[] parameteres = { 
                new SqlParameter("simulate_id", simulate_id)                             
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameteres);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        //通过备料单号搜索模拟单
        public DataSet searchBySimulate(string simulate_id)
        {
            string select = "select a.simulate_line_id,a.simulate_id,a.wo_no,a.wo_key,a.item_id,b.item_name,a.requirement_qty,a.simulated_qty,a.[status],a.pickup_qty,a.issued_qty from wms_simulate_operation a inner join wms_pn b on a.item_id=b.item_id where simulate_id like '%'+@simulate_id+'%' ";

            SqlParameter[] parameteres = { 
                new SqlParameter("simulate_id", simulate_id)                             
            };

            DB.connect();
            DataSet ds = DB.select(select, parameteres);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        ///// <summary>
        ///// 查询数据
        ///// </summary>
        ///// <param name="simulate_id"></param>
        ///// <returns></returns>
        //public DataSet searchBySimulate(string simulate_id)
        //{
        //    string select = "select b1.*, b2.WO_NO, b2.requirement_qty, b3.*, b4.* "
        //        + "from wms_pickup_mtl b1, wms_simulate_operation b2, wms_items_onhand_qty_detail b3, wms_material_io b4 "
        //        + "where b1.simulate_id like '%' + @simulate_id + '%' and b3.item_id = b1.item_id and b4.item_id = b1.item_id and b2.simulate_id = b1.simulate_id ";

        //    SqlParameter[] parameteres = { 
        //        new SqlParameter("simulate_id", simulate_id)                             
        //    };

        //    DB.connect();
        //    DataSet ds = DB.select(select, parameteres);

        //    if(ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        return ds;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// 工单备料
        /// </summary>
        /// <param name="simulate_id"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet preparation(int simulate_id,string item_id, int number,string datecode,int unique)
        {
            DB.connect();
            string selectsql = "select a.simulate_line_id,a.simulate_id,a.wo_no,a.wo_key,a.item_id,b.item_name,a.requirement_qty,a.simulated_qty,a.[status],a.pickup_qty,a.issued_qty from wms_simulate_operation a inner join wms_pn b on a.item_id=b.item_id where a.simulate_id=@simulate_id";

            SqlParameter[] parameters = { 
                new SqlParameter("simulate_id", simulate_id),
                new SqlParameter("item_id",item_id),
                new SqlParameter("number", number),
                new SqlParameter("datecode",datecode),
                new SqlParameter("unique",unique)
            };
            var sql1 = "select * from wms_pickup_mtl where simulate_line_id = @simulate_id and item_name=@item_id and frame_key=(select frame_key from wms_material_io where unique_id=@unique)";
           DataSet ds1=DB.select(sql1, parameters);
           if (ds1.Tables[0].Rows.Count == 0)
           {
               string sql = "insert into wms_pickup_mtl(simulate_line_id,item_id,item_name,frame_key) (select a.simulate_id,a.item_id,c.item_name,b.frame_key from wms_simulate_operation a inner join wms_material_io b on a.unique_id=b.unique_id inner join wms_pn c on b.item_id=c.item_id where c.item_name=@item_id and a.simulate_id=@simulate_id and b.unique_id=@unique);" +
                    "update wms_pickup_mtl set pickup_qty=pickup_qty+@number,datecode=@datecode where simulate_line_id=@simulate_id and item_name=@item_id and frame_key=(select frame_key from wms_material_io where unique_id=@unique);" +
                    "update wms_simulate_operation set status = N'备料', pickup_qty = pickup_qty+@number where simulate_id = @simulate_id and item_id=(select item_id from wms_pn where item_name=@item_id) and unique_id=@unique";
               int flag = DB.tran(sql, parameters);
               if (flag > 0)
               {
                   DataSet ds = DB.select(selectsql, parameters);
                   if (ds != null)
                   {
                       return ds;
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
           else
           {
               string sql = "update wms_pickup_mtl set pickup_qty=pickup_qty+@number,datecode=@datecode where simulate_line_id=@simulate_id and item_name=@item_id and frame_key=(select frame_key from wms_material_io where unique_id=@unique);" +
                    "update wms_simulate_operation set status = N'备料', pickup_qty = pickup_qty+@number where simulate_id = @simulate_id and item_id=(select item_id from wms_pn where item_name=@item_id) and unique_id=@unique";
               int flag = DB.tran(sql, parameters);
               if (flag > 0)
               {
                   DataSet ds = DB.select(selectsql, parameters);
                   if (ds != null)
                   {
                       return ds;
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
        }
        //查询工单备料料量
        public int[] search_pickup_num(int simulate_id, string item_name,string datecode)
        {
            int[] array = new int[2];
            string sql = "select a.simulated_qty-a.pickup_qty,a.unique_id from wms_simulate_operation a inner join wms_material_io b on a.unique_id=b.unique_id where simulate_id=@simulate_id and a.item_id=(select item_id from wms_pn where item_name=@item_name) and (a.simulated_qty-a.pickup_qty)>0 and b.datecode=@datecode;";
            //string sql2 = "select left_qty from wms_material_io where unique_id=@unique_id";
            SqlParameter[] parameters ={
                                           new SqlParameter("simulate_id",simulate_id),
                                           new SqlParameter("item_name",item_name),
                                           new SqlParameter("datecode",datecode)
                                       };
            DB.connect();
           DataSet ds=DB.select(sql, parameters);
            if(ds.Tables[0].Rows.Count==0){
                return array;
            }else{
                array[0] = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                array[1] = int.Parse(ds.Tables[0].Rows[0][1].ToString());
                //SqlParameter[] parameters1 = {
                //                                new SqlParameter("unique_id",array[1])
                //                            };
                //ds = DB.select(sql2, parameters1);
                //array[2] = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                return array;
            }
        }
        //查询备料单的可发料量
        public int search_issue_num(int simulate_id, int item_id)
        {
            int number;
            string sql = "select pickup_qty-issued_qty from wms_pickup_mtl where simulate_line_id=@simulate_id and item_id=@item_id";
            SqlParameter[] parameters ={
                                           new SqlParameter("simulate_id",simulate_id),
                                           new SqlParameter("item_id",item_id)
                                       };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            if (ds.Tables[0].Rows.Count == 0)
            {
                number = -1;
            }
            else
            {
                number = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            return number;
        }
        //发料
        public int issued_qty(string simulate_line_id)
        {
            string sql = "update wms_material_io set simulated_qty=a.simulated_qty-(b.pickup_qty-b.issued_qty),onhand_qty=a.onhand_qty-(b.pickup_qty-b.issued_qty) from wms_material_io a,wms_simulate_operation b where a.unique_id=b.unique_id and a.item_id=b.item_id and b.simulate_id=@simulate_line_id;" + 
                "update wms_items_onhand_qty_detail set onhand_quantiy=onhand_quantiy-(a.pickup_qty-a.issued_qty) from wms_simulate_operation a,wms_items_onhand_qty_detail b where a.item_id=b.item_id and b.subinventory=(select subinventory_name from wms_subinventory where subinventory_key=(select subinventory from WMS_region where region_key=(select region_key from WMS_frame where frame_key=(select frame_key from wms_material_io where unique_id=a.unique_id)))) and a.simulate_id=@simulate_line_id;"+
                "update wms_pickup_mtl set issued_status='Y',issued_qty=(select b.pickup_qty from wms_pickup_mtl b where a.item_id=b.item_id and a.simulate_line_id=b.simulate_line_id and a.simulate_id=b.simulate_id) from wms_pickup_mtl a where a.simulate_line_id=@simulate_line_id;" +
                "update wms_simulate_operation set status=N'发料',issued_qty=(select b.issued_qty from wms_pickup_mtl b where a.simulate_id=b.simulate_line_id and a.item_id=b.item_id and b.frame_key=(select frame_key from wms_material_io where unique_id=a.unique_id)) from wms_simulate_operation a where a.simulate_id=@simulate_line_id";
            SqlParameter[] parameters ={
                                           new SqlParameter("simulate_line_id",simulate_line_id)
                                       };
            DB.connect();
            int i=DB.tran(sql,parameters);
            return i;
        }
        //检查是否可以发料
        public DataSet  is_issued(string simulate_line_id)
        {
            string sql = "select pickup_qty-issued_qty from wms_simulate_operation where simulate_id=@simulate_line_id and (pickup_qty-issued_qty)>0";
            SqlParameter [] parameters = {
                                             new SqlParameter("simulate_line_id",simulate_line_id)
                                         };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }
    }
}