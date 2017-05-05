 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1;

namespace WMS_v1._0.DataCenter
{
    public class Simulate_operation
    {
        /// <summary>
        /// 得到模拟表最后一行数据的id
        /// </summary>
        /// <returns></returns>
        public int getLastId()
        {
            string sql = "SELECT TOP 1 [simulate_line_id] FROM wms_simulate_operation Order By [simulate_line_id] DESC ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0]["simulate_line_id"].ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 生成模拟表数据
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="subinventory"></param>
        /// <returns></returns>
        public DataSet insertSimulate_operationByOperationAndSubinventory_key(int operation, int subinventory)
        {
            string selectsql = "select * "
                + "from wms_requirement_operation b1,wms_material_io b2,wms_frame b3,wms_pn b4 "
                + "where b1.operation_seq_num = @operation AND b4.item_name = b1.item_name AND b3.subinventory_key = @subinventory AND b2.frame_key = b3.frame_key AND b2.item_id = b4.item_id";

            string updatesql = "update wms_material_io set simulated_qty = @number, update_time = GETDATE() where frame_key = @frame_key AND item_id = @item_id;update wms_issue_line set simulated_qty = @number where frame_key = @frame_name";

            string insertwms_simulate_operation = "insert into wms_simulate_operation(item_id, simulated_qty, wo_no, wo_key, requirement_qty) values(@item_id,@number,@wo_no,@wo_key,@requirement_qty)";

            string insertwms_po_line = "insert into wms_po_line(item_id, request_qty) values(@item_id, @request_qty)";

            string insertwms_pickup_mtl = "insert into wms_pickup_mtl(simulate_line_id, item_name, subinventory_key, item_id, operation_seq_num) values(@simulate_line_id, @item_name, @subinventory_key, @item_id, @operation)";

            SqlParameter[] selectparameters = { 
                new SqlParameter("operation", operation),
                new SqlParameter("subinventory", subinventory)            
            };

            SqlParameter[] selectparameters1 = { 
                new SqlParameter("operation", operation),
                new SqlParameter("subinventory", subinventory)            
            };

            DB.connect();
            DataSet ds = DB.select(selectsql, selectparameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;                
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    int flag = 0;
                    //如果需求量已经等于模拟量，则不需要模拟
                    if ((int)ds.Tables[0].Rows[i]["simulated_qty"] == (int)ds.Tables[0].Rows[i]["required_qty"])
                    {
                        continue;
                    }
                    //如果需求量小于等于在手量，将在手明细表的模拟量更新为需求量;否则将在手明细表的模拟量更新为在手量并生成一条PO单身表数据（将缺料量写入）;在更新在手明细表时，同时通过料架key值更新领料单的模拟量
                    if ((int)ds.Tables[0].Rows[i]["required_qty"] <= (int)ds.Tables[0].Rows[i]["onhand_qty"])
                    {
                        SqlParameter[] updateparameters = { 
                            new SqlParameter("frame_key", (int)ds.Tables[0].Rows[i]["frame_key"]),
                            new SqlParameter("item_id", (int)ds.Tables[0].Rows[i]["item_id"]) ,
                            new SqlParameter("number", (int)ds.Tables[0].Rows[i]["required_qty"]),
                            new SqlParameter("frame_name", ds.Tables[0].Rows[i]["frame_name"])
                        };

                        flag = DB.update(updatesql, updateparameters);
                    }
                    else
                    {
                        SqlParameter[] updateparameters = { 
                            new SqlParameter("frame_key", (int)ds.Tables[0].Rows[i]["frame_key"]),
                            new SqlParameter("item_id", (int)ds.Tables[0].Rows[i]["item_id"]) ,
                            new SqlParameter("number", (int)ds.Tables[0].Rows[i]["onhand_qty"])
                        };
                        SqlParameter[] insertparameters = { 
                            new SqlParameter("item_id", (int)ds.Tables[0].Rows[i]["item_id"]),
                            new SqlParameter("request_qty", (int)ds.Tables[0].Rows[i]["required_qty"] - (int)ds.Tables[0].Rows[i]["onhand_qty"])      
                        };

                        DB.insert(insertwms_po_line, insertparameters);
                        flag = DB.update(updatesql, updateparameters);
                    }

                    //生成一条模拟表数据和备料数据
                    if (flag > 0)
                    {
                        string str = "select * "
                            + "from wms_requirement_operation b1,wms_material_io b2,wms_frame b3,wms_pn b4, wms_wo b5 "
                            + "where b1.operation_seq_num = @operation AND b4.item_name = b1.item_name AND b3.subinventory_key = @subinventory AND b2.frame_key = b3.frame_key AND b2.item_id = b4.item_id AND b5.wo_no = b1.wo_no AND b5.part_no = b1.item_name";
                        DataSet temp = DB.select(str, selectparameters);
                        SqlParameter[] insertparameters = { 
                            new SqlParameter("item_id", (int)temp.Tables[0].Rows[i]["item_id"]) ,
                            new SqlParameter("number", (int)temp.Tables[0].Rows[i]["simulated_qty"]),
                            new SqlParameter("wo_no", temp.Tables[0].Rows[i]["wo_no"]),    
                            new SqlParameter("wo_key", (int)temp.Tables[0].Rows[i]["wo_key"]),    
                            new SqlParameter("requirement_qty", (int)temp.Tables[0].Rows[i]["required_qty"])   
                        };

                        DB.insert(insertwms_simulate_operation, insertparameters);

                        SqlParameter[] pickup_mtlparameters = { 
                            new SqlParameter("item_name", temp.Tables[0].Rows[i]["item_name"]) ,
                            new SqlParameter("subinventory_key", (int)temp.Tables[0].Rows[i]["subinventory_key"])  ,
                            new SqlParameter("simulate_line_id", getLastId()),    
                            new SqlParameter("item_id", temp.Tables[0].Rows[i]["item_id"]),    
                            new SqlParameter("operation", operation),    
                        };
                        DB.insert(insertwms_pickup_mtl, pickup_mtlparameters);
                    }

                    i++;
                }   
            }
            //获得最后返回到页面的数据
            DB.connect();
            DataSet dataset = DB.select(selectsql, selectparameters1);

            if (dataset != null && dataset.Tables[0].Rows.Count > 0)
            {
                return dataset;
            }
            else
            {
                return null;
            }
        }
        public int getSimulateByWo(string wo_no)
        {
            string sql = "select * from wms_simulate_operation where wo_no=@wo_no";
            SqlParameter[] parameters ={
                                           new SqlParameter("wo_no",wo_no)
                                       };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}