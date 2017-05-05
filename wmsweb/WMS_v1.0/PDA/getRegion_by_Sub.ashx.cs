using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;

namespace WMS_v1._0.PDA
{
    /// <summary>
    /// poreturn_getlist 的摘要说明
    /// </summary>
    public class getRegion_by_Sub : IHttpHandler
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
            RegionDC regionDC = new RegionDC();
            DataSet ds = regionDC.getRegion_nameBySubinventory_name(context.Request["name"]);

            List<string> modellist = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["region_name"].ToString());
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
        // 传入DataRow,将其转换为ModelWO
        private ModelRegion toModel(DataRow dr)
        {
            ModelRegion model = new ModelRegion();

            //通过循环为ModelWO赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelRegion).GetProperties())
            {
                //如果数据库的字段为空，跳过其赋值
                if (dr[propertyInfo.Name].ToString() == "")
                {
                    continue;
                }
                //赋值
                model.GetType().GetProperty(propertyInfo.Name).SetValue(model, dr[propertyInfo.Name], null);
            }
            return model;
        }
    }
}