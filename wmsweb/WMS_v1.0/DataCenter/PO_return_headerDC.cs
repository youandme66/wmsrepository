using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;

namespace WMS_v1._0.DataCenter//PO退回总表(wms_po_return_detail）对应DC
{
    public class PO_return_headerDC//作者：周雅雯 最后一次修改时间：2016/8/2
    {
        //向PO退回总表中插入数据
        public Boolean insertPO_return_header(string receipt_no, string vendor_name, int return_region, DateTime return_time)
        {

            string sql = "insert into wms_po_return_header "
                       + "(receipt_no,vendor_name,return_region,return_time)values "
                       + "(@receipt_no,@vendor_name,@return_region,@return_time) ";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no",receipt_no),
                new SqlParameter("vendor_name",vendor_name),
                new SqlParameter("return_region",return_region),
                new SqlParameter("return_time",return_time)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }
    }
}