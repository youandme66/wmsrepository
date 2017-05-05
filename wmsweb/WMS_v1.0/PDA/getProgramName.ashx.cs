using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using WMS_v1._0.DataCenter;

namespace WMS_v1._0.PDA
{
    /// <summary>
    /// getProgramName 的摘要说明
    /// </summary>
    public class getProgramName : IHttpHandler
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
            ProgramsDC get_program_name = new ProgramsDC();            

            List<string> list = new List<string>();

            list = get_program_name.getAllEnabledProgram_name();                  

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