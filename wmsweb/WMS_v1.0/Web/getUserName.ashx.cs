using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using WMS_v1._0.DataCenter;

namespace WMS_v1._0.Web
{
    /// <summary>
    /// getUserName 的摘要说明
    /// </summary>
    public class getUserName : IHttpHandler
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
            UsersDC get_user_name = new UsersDC();
            List<string> list = new List<string>();           
            list = get_user_name.getAllEnabledUser_name();                   
           
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