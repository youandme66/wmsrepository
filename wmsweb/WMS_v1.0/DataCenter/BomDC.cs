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

namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/11/12
{
    public class BomDC //bom表(wms_requirement_operation）对应的DateCenter
    {

        //查询出整张表中的信息
        public DataSet getBom()
        {
            //通过SQL语句，获取DateSet
            string sql = "SELECT * FROM wms_bom";

            SqlParameter[] parameters = null;

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)
                return ds;
            else
                return null;
        }

        //通过工单号Wo_no，查询Bom表信息
        public List<ModelBom> getBomByWo_no(string Wo_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_requirement_operation where Wo_no = @Wo_no";

            SqlParameter[] parameters = {
                new SqlParameter("Wo_no", Wo_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelBom> modelBom_list = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                modelBom_list = new List<ModelBom>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelBom_list.Add(toModel(datarow));
                }
            }
            return modelBom_list;
        }

        /**作者：周雅雯 时间：2016/11/12
         * BOM设定页面需要的查询方法
         * 通过wo_no工单编号，item_name料号，operation_seq_num制程，获取ModelBom对象的列表集合
         * 注意：此处因为要将数据库字段int型进行模糊查询，比如operation_seq_num，故传参时传string而不是int，后面进行数据库上的动态转换
         **/
        public DataSet getBomByLikeSome(string wo_no, string item_name, int operation_seq_num)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当wo_no有值时
            if (string.IsNullOrWhiteSpace(wo_no) == false )
            {
                sqlTail += "AND wo_no LIKE '%'+@wo_no+'%' ";
            }
            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false )
            {
                sqlTail += "AND item_name LIKE '%'+@item_name+'%' ";
            }
            //当operation_seq_num有值时
            if (operation_seq_num>0)
            {
                sqlTail += "AND operation_seq_num =@operation_seq_num ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT *,route=(select route from wms_wip_operations where route_id=wms_requirement_operation.operation_seq_num) FROM wms_requirement_operation ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT *,route=(select route from wms_wip_operations where route_id=wms_requirement_operation.operation_seq_num) FROM wms_requirement_operation WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("wo_no", wo_no),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("operation_seq_num", operation_seq_num),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds != null)
                return ds;
            else
                return null;
        }

        /**作者：周雅雯 时间：2016/11/12
        * BOM设定页面需要的查询方法(不模糊查询）
        * 通过wo_no工单编号，item_name料号，operation_seq_num制程，获取ModelBom对象的列表集合
        * 注意：此处因为要将数据库字段int型进行模糊查询，比如operation_seq_num，故传参时传string而不是int，后面进行数据库上的动态转换
        **/
        public DataSet getBomBySome(string big_item_name, string item_name, string operation_seq_num,string version)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当wo_no有值时
            if (string.IsNullOrWhiteSpace(big_item_name) == false)
            {
                sqlTail += "AND bom_item_name =@big_item_name ";
            }
            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name =@item_name ";
            }
            //当operation_seq_num有值时
            if (string.IsNullOrWhiteSpace(operation_seq_num) == false)
            {
                sqlTail += "AND operation_seq_num =@operation_seq_num ";
            }
            //当version有值时
            if (string.IsNullOrWhiteSpace(version) == false)
            {
                sqlTail += "AND bom_version=@version ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_bom ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_bom WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("big_item_name", big_item_name),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("operation_seq_num", operation_seq_num),
                    new SqlParameter("version",version)
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds.Tables[0].Rows.Count >= 1)
                return ds;
            else
                return null;
        }
        /**作者：周雅雯 时间：2016/11/12
         * BOM设定页面需要的查询方法(不模糊查询)
         * 通过wo_no工单编号，item_name料号，operation_seq_num制程，获取ModelBom对象的列表集合
         * 注意：此处因为要将数据库字段int型进行模糊查询，比如operation_seq_num，故传参时传string而不是int，后面进行数据库上的动态转换
         **/
        public DataSet getBomByThree(string big_item_name, string item_name, string version)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当wo_no有值时
            if (string.IsNullOrWhiteSpace(big_item_name) == false)
            {
                sqlTail += "AND bom_item_name =@big_item_name ";
            }
            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name =@item_name ";
            }
            //当version有值时
            if (string.IsNullOrWhiteSpace(version) == false)
            {
                sqlTail += "AND bom_version=@version ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_bom ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_bom WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("big_item_name", big_item_name),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("version",version)
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds.Tables[0].Rows.Count >= 1)
                return ds;
            else
                return null;
        }
        public DataSet getBomByThree(string big_item_name, string item_name, string version, int required_qty)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当wo_no有值时
            if (string.IsNullOrWhiteSpace(big_item_name) == false)
            {
                sqlTail += "AND bom_item_name =@big_item_name ";
            }
            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name =@item_name ";
            }
            //当version有值时
            if (string.IsNullOrWhiteSpace(version) == false)
            {
                sqlTail += "AND bom_version=@version ";
            }
            if (required_qty > 0)
            {
                sqlTail += "AND operation_qty=@required_qty";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_bom ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_bom WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("big_item_name", big_item_name),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("version",version),
                    new SqlParameter("required_qty",required_qty)
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds.Tables[0].Rows.Count >= 1)
                return ds;
            else
                return null;
        }
        //public DataSet getBomBySome(string wo_no, string item_name, int operation_seq_num, int required_qty)
        //{
        //    //完整查询内容
        //    string sqlAll = "";
        //    //* from wms_pn 后的内容，即查询条件
        //    string sqlTail = "";

        //    //当wo_no有值时
        //    if (string.IsNullOrWhiteSpace(wo_no) == false)
        //    {
        //        sqlTail += "AND bom_item_name=@wo_no ";
        //    }
        //    //当item_name有值时
        //    if (string.IsNullOrWhiteSpace(item_name) == false)
        //    {
        //        sqlTail += "AND item_name =@item_name ";
        //    }
        //    //当operation_seq_num有值时
        //    if (operation_seq_num > 0)
        //    {
        //        sqlTail += "AND operation_seq_num =@operation_seq_num ";
        //    }
        //    //当required_qty有值时
        //    if (required_qty > 0)
        //    {
        //        sqlTail += "AND required_qty =@required_qty ";
        //    }
        //    //不包含条件查询时
        //    if (sqlTail.Length <= 0)
        //    {
        //        sqlAll = "SELECT *,route=(select route from wms_wip_operations where route_id=wms_requirement_operation.operation_seq_num) FROM wms_requirement_operation ";
        //    }
        //    //包含条件查询时
        //    else
        //    {
        //        sqlAll = "SELECT *,route=(select route from wms_wip_operations where route_id=wms_requirement_operation.operation_seq_num) FROM wms_requirement_operation WHERE 1=1 " + sqlTail;
        //    }

        //    DB.connect();

        //    SqlParameter[] parameters = {
        //            new SqlParameter("wo_no", wo_no),
        //            new SqlParameter("item_name", item_name),
        //            new SqlParameter("operation_seq_num", operation_seq_num),
        //            new SqlParameter("required_qty", required_qty),
        //        };

        //    DataSet ds = DB.select(sqlAll, parameters);

        //    if (ds.Tables[0].Rows.Count >= 1)
        //        return ds;
        //    else
        //        return null;
        //}

        /**作者：周雅雯 时间：2016/11/12
         * BOM设定页面需要的查询方法
         * 通过wo_no工单编号，item_name料号，operation_seq_num制程，获取ModelBom对象的列表集合
         * 注意：此处因为要将数据库字段int型进行模糊查询，比如operation_seq_num，故传参时传string而不是int，后面进行数据库上的动态转换
         **/
        public DataSet getBomByLikeSome(string wo_no, string item_name, int operation_seq_num ,int required_qty)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当wo_no有值时
            if (string.IsNullOrWhiteSpace(wo_no) == false)
            {
                sqlTail += "AND wo_no LIKE '%'+@wo_no+'%' ";
            }
            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name LIKE '%'+@item_name+'%' ";
            }
            //当operation_seq_num有值时
            if (operation_seq_num > 0)
            {
                sqlTail += "AND operation_seq_num =@operation_seq_num ";
            }
            //当required_qty有值时
            if (required_qty > 0)
            {
                sqlTail += "AND required_qty =@required_qty ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT *,route=(select route from wms_wip_operations where route_id=wms_requirement_operation.operation_seq_num) FROM wms_requirement_operation ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT *,route=(select route from wms_wip_operations where route_id=wms_requirement_operation.operation_seq_num) FROM wms_requirement_operation WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("wo_no", wo_no),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("operation_seq_num", operation_seq_num),
                    new SqlParameter("required_qty", required_qty),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds != null)
                return ds;
            else
                return null;
        }

        //向bom表中插入数据
        public Boolean insertBom(string big_item_name, string item_name, string operation_seq_num,string version,int required_qty,string create_by, DateTime create_time)
        {

            string sql = "insert into wms_bom "
                       + "(bom_item_name,item_name,operation_seq_num,bom_version,operation_qty,create_man,create_time) values"
                       + "(@big_item_name,@item_name,@operation_seq_num,@version,@required_qty,@create_by,@create_time) ";

            SqlParameter[] parameters = {
                new SqlParameter("big_item_name",big_item_name),
                new SqlParameter("item_name",item_name),
                new SqlParameter("operation_seq_num",operation_seq_num),
                new SqlParameter("required_qty",required_qty),
                new SqlParameter("create_time",create_time),
                new SqlParameter("version",version),
                new SqlParameter("create_by",create_by)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //更新bom表中的部分数据
        public Boolean updateBom(int requirement_line_id, string item_name, string operation_seq_num, int required_qty,string bom_version,string update_by, DateTime update_time)
        {
            string sql = "update wms_bom "
                        + "set item_name = @item_name,operation_seq_num = @operation_seq_num,operation_qty = @required_qty ,bom_version=@bom_version,update_by=@update_by,update_time=@update_time "
                        + "where bom_id = @requirement_line_id ";

            SqlParameter[] parameters = {
                new SqlParameter("requirement_line_id",requirement_line_id),
                new SqlParameter("update_by",update_by),
                new SqlParameter("bom_version",bom_version),
                new SqlParameter("item_name",item_name),
                new SqlParameter("operation_seq_num",operation_seq_num),
                new SqlParameter("required_qty",required_qty),
                new SqlParameter("update_time",update_time),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //删除bom表中的单条数据
        public Boolean deleteBom(int requirement_line_id)
        {
            string sql = "delete from wms_bom "
                        + "where bom_id = @requirement_line_id";

            SqlParameter[] parameters = {
                new SqlParameter("requirement_line_id",requirement_line_id)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }
        //获取bom的所有大料号
        public DataSet getBom_item()
        {
            string sql = "select bom_item_name from wms_bom group by bom_item_name";
            SqlParameter[] parameters = null;
            DB.connect();
            DataSet ds = DB.select(sql,parameters);
            return ds;
        }
        //根据大料号获取所有版本号
        public DataSet getBomVersion(string bom_item_name)
        {
            string sql = "select bom_version from wms_bom where bom_item_name=@bom_item_name group by bom_version";
            SqlParameter[] parameters ={
                                           new SqlParameter("bom_item_name",bom_item_name)
                                       };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }
        // 传入DataRow,将其转换为ModelBom
        private ModelBom toModel(DataRow dr)
        {
            ModelBom model = new ModelBom();

            //通过循环为ModelBom赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelBom).GetProperties())
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