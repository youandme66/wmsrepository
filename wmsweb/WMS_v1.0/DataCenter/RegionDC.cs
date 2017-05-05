using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;

namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/9/17
{
    public class RegionDC//区域表（Region）对应的DateCenter
    {

        /// <summary>
        /// 通过仓库id得到该仓库的区域集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet getRegion_nameBySubinventory_id(string id ){

            string sql = "select * from WMS_region where  subinventory = @subinventory_id";

            DB.connect();

            SqlParameter[] parameters = {
                                         new SqlParameter("subinventory_id", id)};

            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                return ds;
            }
            else
            {
                return null;
            }

        }




        /** 获得所有可用的区域名
         **/
        public DataSet getEnableRegion()
        {
            string sql = "select region_name from WMS_region where enabled='Y'";

            DB.connect();

            SqlParameter[] parameters = null;

            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
              
                return ds;
            }
            else
            {
                return null;
            }
        }


        //通过REGION_NAME区域名，获取ModelRegion对象的列表集合
        public List<ModelRegion> getRegionByREGION_NAME(string region_name)
        {
            //通过SQL语句，获取DateSet
            string sql = "select * from WMS_region where REGION_NAME = @region_name";

            SqlParameter[] parameters = {
                new SqlParameter("REGION_NAME", region_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelRegion> ModelRegionlist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelRegionlist = new List<ModelRegion>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelRegionlist.Add(toModel(DateSetRows));
                }
                return ModelRegionlist;
            }
            else
            {
                return ModelRegionlist;
            }
        }

        //通过模糊查询REGION_NAME区域名，获取ModelRegion对象的列表集合
        public List<ModelRegion> getRegionByLikeREGION_NAME(string region_name)
        {
            //通过SQL语句，获取DateSet
            string sql = "select * from WMS_region where REGION_NAME LIKE @region_name";

            SqlParameter[] parameters = {
                new SqlParameter("REGION_NAME", region_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelRegion> ModelRegionlist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelRegionlist = new List<ModelRegion>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelRegionlist.Add(toModel(DateSetRows));
                }
                return ModelRegionlist;
            }
            else
            {
                return ModelRegionlist;
            }

        }

        /// <summary>
        /// 得到区域表的所有数据
        /// </summary>
        /// <returns></returns>
        public DataSet getRegionList()
        {
            string sql = "SELECT *,subinventory_name=(select subinventory_name from wms_subinventory where subinventory_key=wms_region.subinventory) FROM wms_region";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }


        //根据subinventory_name，查询并返回wms_region中所有的region_name
        public DataSet getRegion_nameBySubinventory_name(string subinventory_name)
        {
            string sql = "select region_name ,region_key from wms_region where subinventory = @subinventory_name ";

            SqlParameter[] parameters = {
                 new SqlParameter("subinventory_name", subinventory_name)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            return ds;

        }

        public DataSet getRegion_nameBySub_name(string subinventory_name) {
            string sql = "select region_name,region_key from wms_region where subinventory=(select subinventory_key from wms_subinventory where subinventory_name=@subinventory_name);";
            SqlParameter[] parameters ={
                                          new SqlParameter("subinventory_name",subinventory_name)
                                     };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }

        public DataSet getRegion_nameBySubinventory_key(int subinventory_key)
        {
            string sql = "select region_name ,region_key from wms_region where subinventory = (select subinventory_name from wms_subinventory  where subinventory_key=@subinventory_key) ";

            SqlParameter[] parameters = {
                 new SqlParameter("subinventory_key",subinventory_key)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null)
                return ds;
            else
                return null;

        }

        /// <summary>
        /// 新增一条区域表数据，并返回所有数据
        /// </summary>
        /// <param name="region_name"></param>
        /// <param name="subinventory_key"></param>
        /// <param name="create_by"></param>
        /// <param name="enabled"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public DataSet insertRegion(string region_name, string subinventory, string create_by, string enabled, string description)
        {
            string sql = "insert into wms_region(region_name, subinventory , create_by, enabled, description) values(@region_name, (select subinventory_key from wms_subinventory where subinventory_name=@subinventory), @create_by, @enabled, @description) ";

            SqlParameter[] parameters = { 
                new SqlParameter("region_name", region_name),
                new SqlParameter("subinventory", subinventory),
                new SqlParameter("create_by", create_by),
                new SqlParameter("enabled", enabled),
                new SqlParameter("description", description)
            };

            DB.connect();
            int flag = DB.insert(sql, parameters);

            if (flag == 1)
            {
                return getRegionList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更新一条区域表数据，并返回所有数据
        /// </summary>
        /// <param name="region_key"></param>
        /// <param name="region_name"></param>
        /// <param name="subinventory_key"></param>
        /// <param name="update_by"></param>
        /// <param name="enabled"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public DataSet updateRegion(string region_key, string region_name, string subinventory, string update_by, string enabled, string description)
        {
            string sql = "update wms_region set region_name = @region_name , subinventory = (select subinventory_key from wms_subinventory where subinventory_name=@subinventory) , update_by = @update_by , enabled = @enabled , description = @description ,update_time = GETDATE() where region_key = @region_key ";

            SqlParameter[] parameters = { 
                new SqlParameter("region_key", region_key),
                new SqlParameter("region_name", region_name),
                new SqlParameter("subinventory", subinventory),
                new SqlParameter("update_by", update_by),
                new SqlParameter("enabled", enabled),
                new SqlParameter("description", description)
            };

            DB.connect();

            List<ModelRegion> modellist = new List<ModelRegion>();
            int flag = DB.update(sql, parameters);

            if (flag == 1)
            {
                return getRegionList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除一条区域表数据，并返回所有Model
        /// </summary>
        /// <param name="region_key"></param>
        /// <returns></returns>
        public DataSet deleteRegionById(string region_key)
        {
            string sql = "delete from wms_region where region_key = @region_key ";

            SqlParameter[] parameters = { 
                new SqlParameter("region_key", region_key)                            
            };

            DB.connect();
            int flag = DB.delete(sql, parameters);

            if (flag == 1)
            {
                return getRegionList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 按条件查询区域
        /// </summary>
        /// <param name="region_name"></param>
        /// <param name="subinventory_key"></param>
        /// <param name="create_by"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public DataSet searchRegionByFourParameters(string region_name, string subinventory, string create_by, string enabled)
        {   
           
            string sql = "select *,subinventory_name=(select subinventory_name from wms_subinventory where subinventory_key=wms_region.subinventory) from wms_region where 1=1 ";
           
            SqlParameter[] parameters = { 
                new SqlParameter("region_name", region_name),         
                new SqlParameter("subinventory",subinventory ),
                new SqlParameter("create_by", create_by),
                new SqlParameter("enabled", enabled)
            };

            DB.connect();

            DataSet ds = new DataSet();

            if (!string.IsNullOrWhiteSpace(region_name))
            {
                sql += "AND region_name like '%' + @region_name +'%' ";
            }
            if (!string.IsNullOrWhiteSpace(subinventory))
            {
                sql += "AND subinventory =(select subinventory_key from wms_subinventory where subinventory_name=@subinventory)";
            }
            if (!string.IsNullOrWhiteSpace(create_by))
            {
                sql += "AND create_by like '%' + @create_by + '%' ";
            }
            if (!string.IsNullOrWhiteSpace(enabled))
            {
                sql += "AND enabled like '%' + @enabled + '%' ";
            }

            ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }

        }

        // 传入DataRow,将其转换为ModelRegion
        private ModelRegion toModel(DataRow dr)
        {
            ModelRegion model = new ModelRegion();

            //通过循环为ModelRegion赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
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