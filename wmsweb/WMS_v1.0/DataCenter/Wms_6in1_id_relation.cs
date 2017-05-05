using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1;

namespace WMS_v1._0.DataCenter
{
    public class Wms_6in1_id_relation
    {
        /***
         *  还没有改好，需求不明确，不可用，子查询结果包含多个值（wo_no）
         * 
         * 
         **/

        public bool insert(string prior_id, string current_id, string type, string qty, string lot_no, string datecode, string vendor_code, string create_by)
        {

            string insert_sql = "Insert into wms_6in1_id_relation (prior_id,current_id,create_time,type,qty,lot_no,datecode,vendor_code,create_by) values (@prior_id,@current_id,getdate(),@type,@qty,@lot_no,@datecode,@vendor_code,@create_by )";


            SqlParameter[] parameters = {
                new SqlParameter("current_id", current_id),
                new SqlParameter("qty", qty),
                new SqlParameter("lot_no", lot_no),
                new SqlParameter("datecode", datecode),
                new SqlParameter("vendor_code", vendor_code),
                new SqlParameter("create_by", create_by),
                new SqlParameter("prior_id", prior_id),
                new SqlParameter("type", type),
            };

            DB.connect();

            int flag = DB.insert(insert_sql, parameters);

            if (flag >= 1)
                return true;
            return false;
        }
    }
}