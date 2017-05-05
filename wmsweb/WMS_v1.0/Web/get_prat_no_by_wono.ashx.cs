using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WMS_v1._0.DataCenter;

namespace WMS_v1._0.Web
{
    /// <summary>
    /// Summary description for get_prat_no_by_wono
    /// </summary>
    public class get_prat_no_by_wono : IHttpHandler
    {
        public string toJson(List<string> str)
        {
            StringBuilder json = new StringBuilder();

            if (str == null || str.Count == 0)
            {
                return "[]";
            }

            json.Append("[");
            foreach (var item in str)
            {
                json.Append("{\"Name\":\"");
                json.Append(item);
                json.Append("\"},");
            }
            return json.ToString().Substring(0, json.Length - 1) + "]";
        }

        public void ProcessRequest(HttpContext context)
        {
            string wo_no = context.Request["wo_no"];
            WoDC dc = new WoDC();

            List<string> list = dc.getItem_name_by_wo_no(wo_no);

            string json = toJson(list);

            context.Response.ContentType = "text/plain";

            context.Response.Write(json);
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