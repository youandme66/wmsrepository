using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WMS_v1._0.DataCenter;

namespace WMS_v1._0.Web.JavaScript
{
    /// <summary>
    /// wo_no 的摘要说明
    /// </summary>
    public class wo_noPDA : IHttpHandler
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
                WoDC header_dc = new WoDC();

                List<string> list = new List<string>();

                list = header_dc.getAllWo_no();

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
