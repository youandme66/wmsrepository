using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using WMS_v1._0.DataCenter;

namespace WMS_v1._0.PDA
{
    /// <summary>
    /// getArea 的摘要说明
    /// </summary>
    public class getArea : IHttpHandler
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
            FrameDC shipdc = new FrameDC();

            List<string> list = new List<string>();

            list = shipdc.getAreaName();

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