﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using WMS_v1._0.DataCenter;
using System.Data;
namespace WMS_v1._0.PDA
{
    /// <summary>
    /// LD_warehouse 的摘要说明
    /// </summary>
    public class LD_warehouse : IHttpHandler
    {

        public string toJson(DataSet ds)
        {
            StringBuilder json = new StringBuilder();

            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return "null";

            }

            json.Append("[");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                json.Append("{\"Name\":\"");
                json.Append(dr["region_name"]);
                json.Append("\"},");
            }

            return json.ToString().Substring(0, json.ToString().LastIndexOf(",")) + "]";
        }


        /*得到并关联仓库(select标签)*/

        public void ProcessRequest(HttpContext context)
        {

            RegionDC region_dc = new RegionDC();

            List<string> list = new List<string>();

            DataSet ds = region_dc.getRegion_nameBySubinventory_name(context.Request["warehouse_name"]);



            string json = toJson(ds);

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