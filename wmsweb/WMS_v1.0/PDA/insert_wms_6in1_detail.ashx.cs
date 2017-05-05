using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WMS_v1._0.DataCenter;

namespace WMS_v1._0.PDA
{
    /// <summary>
    /// Summary description for insert_wms_6in1_detail
    /// </summary>
    public class insert_wms_6in1_detail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string pn = "", qty = "", lot_no = "", datecode = "", vendor_code = "", user_name = "";
            if (context.Request.HttpMethod.Equals("GET"))
            {
                pn = context.Request.QueryString["pn"];
                qty = context.Request.QueryString["qty"];
                lot_no = context.Request.QueryString["lot_no"];
                datecode = context.Request.QueryString["datecode"];
                user_name = context.Request.QueryString["user_name"];
                vendor_code = context.Request.QueryString["vendor_code"];

            }
            else if (context.Request.HttpMethod.Equals("POST"))
            {
                pn = context.Request.Form["pn"];
                qty = context.Request.Form["qty"];
                lot_no = context.Request.Form["lot_no"];
                datecode = context.Request.Form["datecode"];
                user_name = context.Request.Form["user_name"];
                vendor_code = context.Request.Form["vendor_code"];
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Error Request");
            }
            Wms_6in1_detailDC dc = new Wms_6in1_detailDC();
            string id = dc.insert_and_get_id(pn, qty, lot_no, datecode, vendor_code, user_name);
            
            context.Response.ContentType = "text/plain";
            context.Response.Write(id);
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