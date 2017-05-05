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

namespace WMS_v1._0.DataCenter//作者：伍雪松 最后一次修改时间：2017/3/7
{
    public class Wms_6in1_detailDC //Wms_6in1_detail表对应的DateCenter
    {
        public string insert_and_get_id(string pn, string qty, string lot_no, string datecode, string vendor_code, string user_name)
        {
            string insert_sql = "Insert into wms_6in1_detail (pn,qty,lot_no,datecode,vendor_code,create_time,user_name) values (@pn,@qty,@lot_no,@datecode,@vendor_code,getdate(),@user_name )";

            string query_sql = "select id from wms_6in1_detail where pn=@pn and qty=@qty and lot_no=@lot_no and datecode=@datecode and vendor_code=@vendor_code and user_name=@user_name order by id desc";

            SqlParameter[] parameters = {
                new SqlParameter("pn", pn),
                new SqlParameter("qty", qty),
                new SqlParameter("lot_no", lot_no),
                new SqlParameter("datecode", datecode),
                new SqlParameter("vendor_code", vendor_code),
                new SqlParameter("user_name", user_name),
            };

            DB.connect();

            int flag = DB.insert(insert_sql, parameters);

            if (flag == 1)
            {
                DataSet ds = DB.select(query_sql, parameters);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0]["id"].ToString();
                else
                    return "-404";
            }
            else
                return "-1";
        }

    }
}