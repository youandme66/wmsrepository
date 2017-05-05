using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
namespace WMS_v1._0.DataCenter
{
    /// <summary>
    /// getCustomerNames 的摘要说明
    /// </summary>
    public class getCustomerNames : IHttpHandler
    {

        public string toJson(List<string> str)
        {
            StringBuilder json = new StringBuilder();

            if (str == null)
            {
                return "null";

            }

            json.Append("[");
            foreach (var item in str)
            {
                json.Append("{\"Name\":\"");
                json.Append(item);
                json.Append("\"},");
            }

            return json.ToString().Substring(0, json.ToString().LastIndexOf(",")) + "]";
        }

        public void ProcessRequest(HttpContext context)
        {
            ShipDC shipdc = new ShipDC();

            List<string> list = new List<string>();

            list = shipdc.getCustomerName();

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