using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WMS_v1._0.DataCenter;

namespace WMS_v1._0.PDA
{
    /// <summary>
    /// Summary description for insert_wms_6in1_id_relation
    /// </summary>
    public class insert_wms_6in1_id_relation : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string current_id = "", qty = "", lot_no = "", datecode = "", vendor_code = "", create_by = "", prior_id = "", type = "";
            if (context.Request.HttpMethod.Equals("GET"))
            {
                prior_id = context.Request.QueryString["prior_id"];
                current_id = context.Request.QueryString["current_id"];
                type = context.Request.QueryString["type"];
                qty = context.Request.QueryString["qty"];
                lot_no = context.Request.QueryString["lot_no"];
                datecode = context.Request.QueryString["datecode"];
                vendor_code = context.Request.QueryString["vendor_code"];
                create_by = context.Request.QueryString["create_by"];
            }
            else if (context.Request.HttpMethod.Equals("POST"))
            {
                prior_id = context.Request.Form["prior_id"];
                current_id = context.Request.Form["current_id"];
                type = context.Request.Form["type"];
                qty = context.Request.Form["qty"];
                lot_no = context.Request.Form["lot_no"];
                datecode = context.Request.Form["datecode"];
                vendor_code = context.Request.Form["vendor_code"];
                create_by = context.Request.Form["create_by"];
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Error Request");
            }
            Wms_6in1_id_relation dc = new Wms_6in1_id_relation();
            bool b = dc.insert(prior_id,current_id, type, qty, lot_no, datecode, vendor_code, create_by);
            string result = "";
            if (b)
            {
                result = "1";
            }
            else
            {
                result = "-1";
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}