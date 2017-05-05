using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WMS_v1._0.DataCenter;

namespace WMS_v1._0.Web
{
    /// <summary>
    /// 获取warehouse列表
    /// </summary>
    public class WareHouse : IHttpHandler
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


        /*得到并关联仓库(select标签)*/

        public void ProcessRequest(HttpContext context)
        {

            SubinventoryDC subinventorydc = new SubinventoryDC();

            List<string> list = new List<string>();

            list = subinventorydc.getAllSubinventory();

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