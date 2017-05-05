using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;

namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/8/16
{
    public class FrameDC//料架表（Frame）对应的DateCenter
    {


        public int getFram_keyByFrame_name(string frame_name)
        {
            string sql = "select frame_key from wms_pn where frame_name=@frame_name  ";


            SqlParameter[] parameters = {
                new SqlParameter("frame_name", frame_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)   //查询操作成功
            {
                int frame_key = int.Parse(ds.Tables[0].Rows[0]["frame_key"].ToString());
                return frame_key;
            }
            else
                return -1;
        }


        /// <summary>
        ///  通过区域ID来获得区域名
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        public string getAreaNameById(string AreaId)
        {

            string sql = "select region_name from WMS_region where region_key = @AreaId ";

            SqlParameter[] parameter = { new SqlParameter("AreaId", AreaId) };

            DB.connect();

            DataSet ds = DB.select(sql, parameter);

            string name;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                name = ds.Tables[0].Rows[0]["region_name"].ToString();
                return name;
            }
            else
            {
                return null;
            }

        }



        /// <summary>
        /// 通过区域名来获取区域ID
        /// </summary>
        /// <param name="AreaName"></param>
        /// <returns></returns>
        public string getAreaIdByName(string AreaName)
        {

            string sql = "select region_key from WMS_region where region_name = @AreaName ";

            SqlParameter[] parameter = { new SqlParameter("AreaName", AreaName) };

            DB.connect();

            DataSet ds = DB.select(sql, parameter);

            string key;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                key = ds.Tables[0].Rows[0]["region_key"].ToString();
                return key;
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        ///  通过用户ID来获得用户名
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        public string getUserNameById(string UserId)
        {

            string sql = "select user_name from wms_users where user_id = @UserId ";

            SqlParameter[] parameter = { new SqlParameter("UserId", UserId) };

            DB.connect();

            DataSet ds = DB.select(sql, parameter);

            string name;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                name = ds.Tables[0].Rows[0]["user_name"].ToString();
                return name;
            }
            else
            {
                return null;
            }

        }



        /// <summary>
        /// 通过区域名来获取区域ID
        /// </summary>
        /// <param name="AreaName"></param>
        /// <returns></returns>
        public string getUserIdByName(string UserName)
        {

            string sql = "select user_id from wms_users where user_name = @UserName ";

            SqlParameter[] parameter = { new SqlParameter("UserName", UserName) };

            DB.connect();

            DataSet ds = DB.select(sql, parameter);

            string key;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                key = ds.Tables[0].Rows[0]["user_id"].ToString();
                return key;
            }
            else
            {
                return null;
            }

        }




        //获取所有的getAreaName
        public List<string> getAreaName()
        {

            string sql = "select * from WMS_region";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["region_name"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }

        }


        //通过FRAME_NAME料架名，获取ModelFrame对象的列表集合
        public List<ModelFrame> getFrameByFRAME_NAME(string frame_name)
        {
            //通过SQL语句，获取DateSet
            string sql = "select * from WMS_frame where FRAME_NAME = @frame_name";

            SqlParameter[] parameters = {
                new SqlParameter("FRAME_NAME", frame_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelFrame> ModelFramelist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelFramelist = new List<ModelFrame>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelFramelist.Add(toModel(DateSetRows));
                }
                return ModelFramelist;
            }
            else
            {
                return ModelFramelist;
            }
        }

        //通过模糊查询FRAME_NAME料架名，获取ModelFrame对象的列表集合
        public List<ModelFrame> getFrameByLikeFRAME_NAME(string frame_name)
        {
            //通过SQL语句，获取DateSet
            string sql = "select * from WMS_frame where FRAME_NAME LIKE @frame_name";

            SqlParameter[] parameters = {
                new SqlParameter("FRAME_NAME", frame_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelFrame> ModelFramelist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelFramelist = new List<ModelFrame>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelFramelist.Add(toModel(DateSetRows));
                }
                return ModelFramelist;
            }
            else
            {
                return ModelFramelist;
            }
        }

        /** 通过frame_name获得Frame_key
         * 调拨单页面需要方法
         **/
        public int getFrame_key(string frame_name)
        {
            string sql = "select Frame_key from wms_frame where frame_name=@frame_name";

            DB.connect();

            SqlParameter[] parameters = {
                new SqlParameter("frame_name", frame_name)
            };

            DataSet ds = DB.select(sql, parameters);

            int frame_key = -1;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    frame_key = int.Parse(dr["Frame_key"].ToString());
                }
                return frame_key;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 根据库别key值查找料架名
        /// </summary>
        /// <param name="subinventory"></param>
        /// <returns></returns>
        public DataSet getFrameBySubinventory_key(string subinventory_key)
        {
            string sql = "select Frame_name from wms_frame where subinventory_key=@subinventory_key";

            DB.connect();

            SqlParameter[] parameters = {
                new SqlParameter("subinventory_key", subinventory_key)
            };

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



        /// <summary>
        /// 得到料架表中所有数据
        /// </summary>
        /// <returns></returns>
        public List<ModelFrame> getFrameList()
        {
            string sql = "select * from wms_frame";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<ModelFrame> modellist = new List<ModelFrame>();
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

        /// <summary>
        /// 获得数据库中所有料架名
        /// </summary>
        /// <returns></returns>
        public DataSet getAllFrame_name()
        {
            string sql = "select [frame_name],frame_key from wms_frame";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            if (ds != null)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// 新增一条料架表数据，返回所有数据
        /// </summary>
        /// <param name="frame_name"></param>
        /// <param name="enabled"></param>
        /// <param name="description"></param>
        /// <param name="create_by"></param>
        /// <param name="region_key"></param>
        /// <returns></returns>
        public bool insertFrame(string frame_name, string enabled, string description, string create_by, string region_name)
        {
            string region_key = getAreaIdByName(region_name);

            string sql = "insert into wms_frame(frame_name, enabled, description, create_by, region_key) values(@frame_name, @enabled, @description, @create_by, @region_key)";

            SqlParameter[] parameters = {
                new SqlParameter("frame_name", frame_name),
                new SqlParameter("enabled", enabled),
                new SqlParameter("description", description),
                new SqlParameter("create_by", create_by),
                new SqlParameter("region_key", region_key)
            };

            DB.connect();
            int flag = DB.insert(sql, parameters);

            if (flag == 1)
                return true;
            else
                return false;

        }

        /// <summary>
        /// 根据库别名找到对应的库别key值
        /// </summary>
        /// <param name="subinventory_name"></param>
        /// <returns></returns>
        public string getSubinventory_keyByName(string subinventory_name)
        {

            string sql = "select subinventory_key from wms_subinventory where subinventory_name = @subinventory_name";

            SqlParameter[] Parameter_name = { new SqlParameter("subinventory_name", subinventory_name) };

            DB.connect();

            DataSet ds = DB.select(sql, Parameter_name);

            string subinventory_key = ds.Tables[0].Rows[0]["subinventory_key"].ToString();

            return subinventory_key;

        }


        public string getSubinventory_nameByKey(string subinventory_key)
        {

            string sql = "select subinventory_name from wms_subinventory where subinventory_key = @subinventory_key";

            SqlParameter[] Parameter_name = { new SqlParameter("subinventory_key", subinventory_key) };

            DB.connect();

            DataSet ds = DB.select(sql, Parameter_name);

            string subinventory_name = ds.Tables[0].Rows[0]["subinventory_name"].ToString();

            return subinventory_name;

        }


        /// <summary>
        /// 删除一条料架数据
        /// </summary>
        /// <param name="frame_key"></param>
        /// <returns></returns>
        public bool deleteFrameById(string frame_key)
        {
            string sql = "delete from wms_frame where frame_key = @frame_key";

            SqlParameter[] parameters = {
                new SqlParameter("frame_key", frame_key)
            };

            DB.connect();
            int flag = DB.delete(sql, parameters);

            if (flag == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条料架数据
        /// </summary>
        /// <param name="frame_key"></param>
        /// <param name="frame_name"></param>
        /// <param name="update_by"></param>
        /// <param name="enabled"></param>
        /// <param name="description"></param>
        /// <param name="region_key"></param>
        /// <returns></returns>
        public bool updateSubinventory(string frame_key, string frame_name, string update_by, string enabled, string description, string region_name)
        {
            string region_key = getAreaIdByName(region_name);
            string sql = "update wms_frame set frame_name = @frame_name , update_by = @update_by , enabled = @enabled , description = @description , region_key = @region_key, update_time = GETDATE() where frame_key = @frame_key ";

            SqlParameter[] parameters = {
                new SqlParameter("frame_key", frame_key),
                new SqlParameter("frame_name", frame_name),
                new SqlParameter("update_by", update_by),
                new SqlParameter("enabled", enabled),
                new SqlParameter("description", description),
                new SqlParameter("region_key", region_key)
            };

            DB.connect();
            int flag = DB.update(sql, parameters);

            if (flag == 1)
                return true;
            else
                return false;

        }

        /// <summary>
        /// 多条件查询料架表
        /// </summary>
        /// <param name="create_by"></param>
        /// <param name="frame_name"></param>
        /// <param name="subinventory_key"></param>
        /// <param name="update_by"></param>
        /// <param name="enabled"></param>
        /// <param name="region_key"></param>
        /// <returns></returns>
        public DataSet searchRegionByFourParameters(string create_by, string frame_name, string update_by, string enabled, string region_name)
        {

            string sql = "select wms_frame.*,wms_region.region_name from wms_frame " +
                "join wms_region on wms_region.region_key= wms_frame.region_key " +
                "where 1=1 ";

            int region_key = 0;
            if (!string.IsNullOrWhiteSpace(region_name))
            {
                try
                {
                    region_key = int.Parse(getAreaIdByName(region_name));
                }
                catch (Exception ex)
                {
                    region_key = 0;
                } 
            }

            SqlParameter[] parameters = {
                new SqlParameter("create_by", create_by),
                new SqlParameter("frame_name", frame_name),
                new SqlParameter("update_by", update_by),
                new SqlParameter("enabled", enabled),
                new SqlParameter("region_key", region_key)
            };

            DB.connect();

            DataSet ds = new DataSet();
            List<ModelFrame> modellist = new List<ModelFrame>();

            if (!string.IsNullOrWhiteSpace(update_by))
            {
                sql += "AND update_by like '%' + @update_by + '%' ";
            }
            if (!string.IsNullOrWhiteSpace(create_by))
            {
                sql += "AND create_by like '%' + @create_by + '%'";
            }
            if (!string.IsNullOrWhiteSpace(enabled))
            {
                sql += "AND enabled = @enabled";
            }
            if (!string.IsNullOrWhiteSpace(frame_name))
            {
                sql += "AND frame_name like '%' + @frame_name + '%' ";
            }
            if (region_key != 0)
            {
                sql += "AND region_key = @region_key";
            }

            ds = DB.select(sql, parameters);

            return ds;

        }

        public List<string> getAllFrameKey()
        {
            string sql = "select frame_key from wms_frame where enabled = 'Y' ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["frame_key"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        // 传入DataRow,将其转换为ModelFrame
        private ModelFrame toModel(DataRow dr)
        {
            ModelFrame model = new ModelFrame();

            //通过循环为ModelFrame赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelFrame).GetProperties())
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