using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WMS_v1._0.DataCenter;

namespace WMS_v1._0.Web
{
    /// <summary>
    /// Summary description for getWono_by_itemname
    /// </summary>
    public class getWono_by_itemname : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string item_name = context.Request["item_name"];
            WoDC dc = new WoDC();

            List<string> list = dc.getWo_no_by_item_name(item_name);

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
    }
}