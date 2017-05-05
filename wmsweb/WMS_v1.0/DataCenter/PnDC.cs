using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;
using WMS_v1._0.Model;
using System.Collections;

namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/8/13
{
    public class PnDC      //料号表（Pn）对应的DateCenter
    {
        //查询出整张表中的信息
        public List<ModelPn> getPn()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT * FROM wms_pn";

            SqlParameter[] parameters = null;

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelPn> modelPn_list = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                modelPn_list = new List<ModelPn>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelPn_list.Add(toModel(datarow));
                }
            }
            return modelPn_list;
        }


        // 获得数据库中所有料号 2016/9/2
        public List<string> getAllItem_name()
        {
            string sql = "select item_name from wms_pn  ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["item_name"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }


        //通过ITEM_NAME料号，获取ModelPn对象的列表集合
        public List<ModelPn> getPnByITEM_NAME(string item_name)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_pn where ITEM_NAME = @item_name";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelPn> ModelPnlist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelPnlist = new List<ModelPn>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelPnlist.Add(toModel(DateSetRows));
                }
                return ModelPnlist;
            }
            else
            {
                return ModelPnlist;
            }
        }

        /**作者周雅雯，时间：2016/9/3
         * 根据item_id 返回Item_name 
         * */
        public string getItem_nameByItem_id(int item_id)
        {
            string sql = "select item_name from wms_pn where item_id=@item_id  ";


            SqlParameter[] parameters = {
                new SqlParameter("item_id", item_id)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)   //查询操作成功
            {
                string item_name = ds.Tables[0].Rows[0]["item_name"].ToString();
                return item_name;
            }
            else
                return null;
        }

        /**作者周雅雯，时间：2016/9/4
         * 根据Item_name 返回item_id 
         * */
        public int getItem_idByItem_name(string item_name)
        {
            string sql = "select item_id from wms_pn where item_name=@item_name  ";


            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)   //查询操作成功
            {
                int item_id = int.Parse(ds.Tables[0].Rows[0]["item_id"].ToString());
                return item_id;
            }
            else
                return -1;
        }


        /**作者：周雅雯 时间：2016/8/13
         * 料号设定页面需要的查询方法
         **/
        public List<ModelPn> getPnBySome(string item_name, string item_desc, string uom)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name LIKE '%'+@item_name+'%' ";
            }
            //当item_desc有值时
            if (string.IsNullOrWhiteSpace(item_desc) == false)
            {
                sqlTail += "AND item_desc LIKE '%'+@item_desc+'%' ";
            }
            //当uom有值时
            if (string.IsNullOrWhiteSpace(uom) == false)
            {
                sqlTail += "AND uom LIKE '%'+@uom+'%' ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_pn ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_pn WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("item_desc", item_desc),
                    new SqlParameter("uom", uom),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            List<ModelPn> ModelPnlist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelPnlist = new List<ModelPn>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelPnlist.Add(toModel(DateSetRows));
                }
                return ModelPnlist;
            }
            else
            {
                return ModelPnlist;
            }
        }


        //通过模糊查询ITEM_NAME料号，获取ModelPn对象的列表集合
        public List<ModelPn> getPnByLikeITEM_NAME(string item_name)
        {
            //通过SQL语句，获取DateSet
            string sql = "select * from wms_pn where ITEM_NAME LIKE @item_name";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelPn> ModelPnlist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelPnlist = new List<ModelPn>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelPnlist.Add(toModel(DateSetRows));
                }
                return ModelPnlist;
            }
            else
            {
                return ModelPnlist;
            }
        }


        //向料号表中插入数据 料号设定页面所需插入方法
        public Boolean insertPn(string item_name, string item_desc, string uom, DateTime create_time, string create_user)
        {

            string sql = "insert into wms_pn "
                       + "(item_name,item_desc,uom,create_time,create_user)values "
                       + "(@item_name,@item_desc,@uom,@create_time,@create_user) ";

            SqlParameter[] parameters = {
                new SqlParameter("item_name",item_name),
                new SqlParameter("item_desc",item_desc),
                new SqlParameter("uom",uom),
                new SqlParameter("create_time",create_time),
                new SqlParameter("create_user",create_user),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //更新料号表中的部分数据 料号设定页面所需更新方法
        public Boolean updatePn(string item_name, string item_desc, string uom, DateTime update_time, string update_user)
        {
            string sql = "update wms_pn "
                        + "set item_name = @item_name,item_desc = @item_desc,uom = @uom,update_time=@update_time,update_user=@update_user "
                        + "where item_name = @item_name";

            SqlParameter[] parameters = {
                new SqlParameter("item_name",item_name),
                new SqlParameter("item_desc",item_desc),
                new SqlParameter("uom",uom),
                new SqlParameter("update_time",update_time),
                new SqlParameter("update_user",update_user),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //删除料号表中的单条数据 料号设定页面所需删除方法
        public Boolean deletePn(string item_name)
        {
            string sql =//删除库存总表中料号对应信息
                        "delete from wms_pn where item_name=@item_name";

            SqlParameter[] parameters = {
                new SqlParameter("item_name",item_name),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }
        //根据库别选出料架
        public DataSet getFrameNameBySubinventory(string subinventory)
        {
            string sql = "select frame_name,frame_key from WMS_frame where region_key in (select region_key from WMS_region where subinventory =(select subinventory_key from wms_subinventory where subinventory_name=@subinventory))";
            SqlParameter[] parameters ={
                                           new SqlParameter("subinventory",subinventory)
                                       };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ds;
            }
        }


        // 传入DataRow,将其转换为ModelPn
        private ModelPn toModel(DataRow dr)
        {
            ModelPn model = new ModelPn();

            //通过循环为ModelPn赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelPn).GetProperties())
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