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

namespace WMS_v1._0.DataCenter//作者：周雅雯 最后一次修改时间：2016/10/26
{
    public class WoDC//工单表（wms_wo）对应的DateCenter
    {

        //查询出整张表中的信息
        public List<ModelWO> getWo()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT * FROM wms_wo";

            SqlParameter[] parameters = null;

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelWO> modelWo_list = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                modelWo_list = new List<ModelWO>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelWo_list.Add(toModel(datarow));
                }
            }
            return modelWo_list;
        }


        /**作者周雅雯，时间：2016/11/19
         * 根据return_wo_no 返回该工单下对应的Target_qty 
         * */
        public int getTarget_qtyByReturn_wo_no(string return_wo_no)
        {
            string sql = "select Target_qty from wms_wo where wo_no=@return_wo_no  ";

            SqlParameter[] parameters = {
                new SqlParameter("return_wo_no", return_wo_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count > 0)   //查询操作成功
            {
                int Target_qty = int.Parse(ds.Tables[0].Rows[0]["Target_qty"].ToString());
                return Target_qty;
            }
            else
                return -1;
        }


        public List<int> getAll_qtyByReturn_wo_no(string return_wo_no)
        {
            string sql = "select target_qty,turnin_qty,shipped_qty from wms_wo where wo_no=@return_wo_no  ";

            SqlParameter[] parameters = {
                new SqlParameter("return_wo_no", return_wo_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);
            List<int> list = new List<int>();
            if (ds.Tables[0].Rows.Count > 0)   //查询操作成功
            {
                for (int i = 0; i < 3; i++)
                {
                    list.Add(int.Parse(ds.Tables[0].Rows[0][i].ToString()));
                }
                return list;
            }
            else
                return null;
        }


        /**作者周雅雯，时间：2016/11/19
        * 根据return_wo_no 返回该工单下对应的Target_qty 
        * */
        public int getWo_keyByReturn_wo_no(string return_wo_no)
        {
            string sql = "select wo_key from wms_wo where wo_no=@return_wo_no  ";

            SqlParameter[] parameters = {
                new SqlParameter("return_wo_no", return_wo_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)   //查询操作成功
            {
                int Target_qty = int.Parse(ds.Tables[0].Rows[0]["wo_key"].ToString());
                return Target_qty;
            }
            else
                return -1;
        }

        //通过工单号Wo_no，查询工单表信息
        public List<ModelWO> getWOByWo_no(string Wo_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_wo where Wo_no = @Wo_no";

            SqlParameter[] parameters = {
                new SqlParameter("Wo_no", Wo_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelWO> modelWO_list = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                modelWO_list = new List<ModelWO>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelWO_list.Add(toModel(datarow));
                }
            }
            return modelWO_list;
        }
        //通过工单号Wo_no，查询工单表信息
        public DataSet getWOBySimulate_id(string simulate_id)
        {

            //通过SQL语句，获取DateSet
            string sql = "select c.item_name,b.requirement_qty,b.simulated_qty,a.left_qty,d.frame_name,a.datecode,a.update_time from wms_material_io a inner join wms_simulate_operation b on a.unique_id=b.unique_id inner join wms_pn c on b.item_id=c.item_id inner join wms_frame d on d.frame_key=a.frame_key where b.simulate_id=@simulate_id;";

            SqlParameter[] parameters = {
                new SqlParameter("simulate_id", simulate_id)
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

        /**作者周雅雯，时间：2016/9/2
         * wms_wo表中的所有的Wo_no数据
         * **/
        public List<string> getAllWo_no()
        {

            string sql = "select wo_no from wms_wo  ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["wo_no"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        /**作者周雅雯，时间：2016/9/3
         * 返回wms_wo表中的所有可用的工单号Wo_no
         * **/
        public List<string> getAllUsedWo_no()
        {

            string sql = "select wo_no from wms_wo where status='1' or status='Y' ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["wo_no"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        /**作者：周雅雯 时间：2016/8/10
         * 工单设定页面需要的查询方法
         * 通过wo_no工单号，status状态，target_qty工单对应数量，part_no料号，获取ModelWO对象的列表集合
         * 注意：此处因为要将数据库字段int型进行模糊查询，比如operation_seq_num，故传参时传string而不是int，后面进行数据库上的动态转换
         **/
        public List<ModelWO> getWoBySome(string wo_no, string status, string target_qty, string part_no)
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
            //当target_qty有值时
            if (string.IsNullOrWhiteSpace(target_qty) == false)
            {
                sqlTail += "AND CONVERT(NVARCHAR(10),target_qty) LIKE '%'+@target_qty+'%'  ";
            }
            //当status有值时
            if (string.IsNullOrWhiteSpace(status) == false)
            {
                sqlTail += "AND status LIKE '%'+@status+'%' ";
            }
            //当part_no有值时
            if (string.IsNullOrWhiteSpace(part_no) == false)
            {
                sqlTail += "AND part_no LIKE '%'+@part_no+'%' ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_wo";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_wo WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("wo_no", wo_no),
                    new SqlParameter("status", status),
                    new SqlParameter("target_qty", target_qty),
                    new SqlParameter("part_no", part_no),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            List<ModelWO> ModelWOlist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelWOlist = new List<ModelWO>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelWOlist.Add(toModel(DateSetRows));
                }
                return ModelWOlist;
            }
            else
            {
                return ModelWOlist;
            }
        }


        //向工单表中插入数据
        public Boolean insertWo(string wo_no, string status, int target_qty, string part_no, string version, DateTime create_time)
        {

            string sql = "insert into wms_wo "
                       + "(wo_no,status,target_qty,part_no,create_time)values "
                       + "(@wo_no,@status,@target_qty,@part_no,@create_time);insert into wms_requirement_operation(wo_no,bom_version,item_name,operation_seq_num,required_qty)" +
" select d.wo_no,bom_version,item_name,route_id,qty from" +
" (select e.bom_version,e.item_name,e.operation_qty*(select target_qty from wms_wo where wo_no=@wo_no) as qty,f.route_id " +
"from wms_bom e inner join wms_wip_operations f on e.operation_seq_num=f.route where bom_version=@version and bom_item_name=(select c.part_no from wms_wo c where wo_no=@wo_no)) as a,(select wo_no from wms_wo b where wo_no=@wo_no) as d";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no",wo_no),
                new SqlParameter("status",status),
                new SqlParameter("target_qty",target_qty),
                new SqlParameter("part_no",part_no),
                new SqlParameter("create_time",create_time),
                new SqlParameter("version",version)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.tran(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //更新工单表中的部分数据
        public Boolean updateWo(string wo_no, string status, int target_qty, string part_no, string version, DateTime update_time)
        {
            string sql = "update wms_wo "
                        + "set wo_no = @wo_no,status = @status,target_qty = @target_qty,part_no = @part_no,update_time=@update_time "
                        + "where wo_no = @wo_no;delete from wms_requirement_operation where wo_no=@wo_no;" +
                        "insert into wms_requirement_operation(wo_no,bom_version,item_name,operation_seq_num,required_qty)" +
                        " select d.wo_no,bom_version,item_name,route_id,qty from" +
                        " (select e.bom_version,e.item_name,e.operation_qty*(select target_qty from wms_wo where wo_no=@wo_no) as qty,f.route_id " +
                        "from wms_bom e inner join wms_wip_operations f on e.operation_seq_num=f.route where bom_version=@version and bom_item_name=(select c.part_no from wms_wo c where wo_no=@wo_no)) as a,(select wo_no from wms_wo b where wo_no=@wo_no) as d";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no",wo_no),
                new SqlParameter("status",status),
                new SqlParameter("target_qty",target_qty),
                new SqlParameter("part_no",part_no),
                new SqlParameter("update_time",update_time),
                new SqlParameter("version",version)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //删除工单表中的单条数据
        public Boolean deleteWo(string wo_no)
        {
            string sql = "delete from wms_wo "
                        + "where wo_no = @wo_no";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no",wo_no),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }
        //ajax获取当前工单已存量，料号，工单制作量
        public List<string> getWo_noByInvooice(string wo_no)
        {
            string sql = "select target_qty,part_no,turnin_qty+shipped_qty from wms_wo where wo_no=@wo_no";
            SqlParameter[] parameters ={
                                           new SqlParameter("wo_no",wo_no)
                                       };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            List<string> list = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list.Add(ds.Tables[0].Rows[0]["part_no"].ToString());
                list.Add(ds.Tables[0].Rows[0][0].ToString());
                list.Add(ds.Tables[0].Rows[0][2].ToString());
                return list;
            }
            else
            {
                return null;
            }
        }
        //已入库的数量
        public int getLeftNum(string wo_no)
        {
            string sql = "select target_qty-turnin_qty-shipped_qty from wms_wo where wo_no=@wo_no";
            SqlParameter[] parameters ={
                                           new SqlParameter("wo_no",wo_no)
                                       };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return -1;
            }
            else
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
        }

        //Ajax 根据料号获取工单编号
        public List<string> getWo_no_by_item_name(string item_name)
        {
            string sql = "select wo_no from wms_wo where part_no = @item_name";
            SqlParameter[] parameters =
                {
                    new SqlParameter("item_name",item_name)
                };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            List<string> list = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(dr["wo_no"].ToString());
            }
            return list;

        }
        //Ajax 根据工单编号获取料号
        public List<string> getItem_name_by_wo_no(string wo_no)
        {
            string sql = "select part_no from wms_wo where wo_no = @wo_no";
            SqlParameter[] parameters =
                {
                    new SqlParameter("wo_no",wo_no)
                };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            List<string> list = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(dr["part_no"].ToString());
            }
            return list;

        }


        public DataSet getWo_noBypart_no(string part_no)
        {
            string sql = "select * from wms_wo where part_no = @part_no";
            SqlParameter[] parameters =
                {
                    new SqlParameter("part_no",part_no)
                };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }

        // 传入DataRow,将其转换为ModelWO
        private ModelWO toModel(DataRow dr)
        {
            ModelWO model = new ModelWO();

            //通过循环为ModelWO赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelWO).GetProperties())
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