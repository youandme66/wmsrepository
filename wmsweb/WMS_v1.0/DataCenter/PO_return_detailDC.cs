using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;

namespace WMS_v1._0.DataCenter//PO退回明细表(wms_po_return_detail）对应DC
{
    public class PO_return_detailDC//作者：周雅雯 最后一次修改时间：2016/8/2
    {
        //向PO退回明细表中插入数据,此处对于return_header_id，line_num 还需考虑
        public Boolean insertPO_return_detail(string receipt_no,string item_name,int required_qty,string return_sub,DateTime return_time,string return_wo_no)
        {

            string sql = "insert into wms_po_return_header "
                       + "(return_header_id,po_no,item_name,required_qty,return_sub,return_time,return_wo_no)values "
                       + "((select return_header_id from wms_po_return_header where receipt_no=@receipt_no ),@receipt_no,@item_name,@required_qty,@return_sub,@return_time,@return_wo_no) ";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no",receipt_no),
                new SqlParameter("item_name",item_name),
                new SqlParameter("required_qty",required_qty),
                new SqlParameter("return_sub",return_sub),
                new SqlParameter("return_time",return_time),
                new SqlParameter("return_wo_no",return_wo_no)
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