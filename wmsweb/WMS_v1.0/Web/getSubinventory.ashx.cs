using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;

namespace WMS_v1._0.Web
{
    /// <summary>
    /// getSubinventory 的摘要说明 获取所有可用的库别名
    /// </summary>
    public class getSubinventory : IHttpHandler
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
            SubinventoryDC subDC = new SubinventoryDC();
            DataSet ds = subDC.getAllUsedSubinventory_name();

            List<string> modellist = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["subinventory_name"].ToString());
                }
            }
            string json = toJson(modellist);
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