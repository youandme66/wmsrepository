using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using WMS_v1._0.DataCenter;

namespace WMS_v1._0.Web
{
    /// <summary>
    /// getDepartmentDescription 的摘要说明
    /// </summary>
    public class getDepartmentDescription : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DepartmentDC Department = new DepartmentDC();
            List<string> list = new List<string>();
            list = Department.getAllDescription();

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
    }
}