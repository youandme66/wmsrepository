using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;

namespace WMS_v1._0.DataCenter
{
    public class Requirement_operationDC
    {
        /// <summary>
        /// 通过制程得到用料需求表的数据
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public List<ModelRequirement> getRequirementByOperation(int operation)
        {
            string sql = "select * from wms_requirement_operation where OPERATION_SEQ_NUM = @operation";

            SqlParameter[] parameters = {
                new SqlParameter("operation", operation)            
            };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            List<ModelRequirement> modellist = new List<ModelRequirement>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(toModel(dr));
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        private ModelRequirement toModel(DataRow dr)
        {
            ModelRequirement model = new ModelRequirement();

            foreach (PropertyInfo propertyInfo in typeof(ModelRequirement).GetProperties())
            {
                if (dr[propertyInfo.Name].ToString() == "")
                {
                    continue;
                }
                model.GetType().GetProperty(propertyInfo.Name).SetValue(model, dr[propertyInfo.Name], null);
            }

            return model;
        }
    }
}