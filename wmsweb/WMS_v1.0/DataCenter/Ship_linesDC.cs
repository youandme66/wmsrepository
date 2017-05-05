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
    public class Ship_linesDC
    {
        /// <summary>
        /// 得到出货明细表全部数据的Model
        /// </summary>
        /// <returns></returns>
        public List<ModelShip_lines> getShip_linesList()
        {
            string sql = "SELECT * FROM wms_ship_lines";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<ModelShip_lines> modellist = new List<ModelShip_lines>();
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
        /// 得到最后一行数据
        /// </summary>
        /// <returns></returns>
        public ModelShip_lines getLastShip_lines()
        {
            string sql = "select top 1 * from wms_ship_lines order by [ship_lines_key] desc ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return toModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 新增一条出货明细表的数据，并返回Model
        /// </summary>
        /// <param name="ship_key"></param>
        /// <param name="wo_no"></param>
        /// <param name="picked_qty"></param>
        /// <returns></returns>
        public bool insertAndReturnShip_lines(int ship_key, string wo_no, int picked_qty, string ship_man)
        {
            string insert = "insert into wms_ship_lines(ship_key, wo_no, picked_qty, part_no,ship_man,create_time) " +
                "values(@ship_key, @wo_no, @picked_qty, (select wms_ship.part_no from wms_ship where wms_ship.ship_key=@ship_key),@ship_man,GETDATE()) ";


            SqlParameter[] parameters = {
                    new SqlParameter("ship_key", ship_key),
                    new SqlParameter("picked_qty", picked_qty),
                    new SqlParameter("wo_no", wo_no),
                    new SqlParameter("ship_man", ship_man)
                };

            DB.connect();

            int flag = 0;
            flag = DB.insert(insert, parameters);

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
        /// 更新一条出货明细表数据，并返回所有Model
        /// </summary>
        /// <param name="ship_lines_key"></param>
        /// <param name="wo_no"></param>
        /// <param name="picked_qty"></param>
        /// <returns></returns>
        public List<ModelShip_lines> updateShipByCustomer_idAndShip_man(int ship_lines_key, string wo_no, int picked_qty)
        {
            string sql = "update wms_ship_lines set wo_no = @wo_no , picked_qty = @picked_qty,update_time = GETDATE() where ship_lines_key = @ship_lines_key ";

            SqlParameter[] parameters = {
                new SqlParameter("ship_lines_key", ship_lines_key),
                new SqlParameter("wo_no", wo_no),
                new SqlParameter("picked_qty", picked_qty)
            };

            DB.connect();
            List<ModelShip_lines> modellist = new List<ModelShip_lines>();
            int flag = DB.update(sql, parameters);

            if (flag == 1)
            {
                return getShip_linesList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除一条出货明细表数据，并返回所有Model
        /// </summary>
        /// <param name="ship_lines_key"></param>
        /// <returns></returns>
        public List<ModelShip_lines> deleteShipById(int ship_lines_key)
        {
            string sql = "delete from wms_ship_lines where ship_lines_key = @ship_lines_key ";

            SqlParameter[] parameters = {
                new SqlParameter("ship_lines_key", ship_lines_key)
            };

            DB.connect();
            int flag = DB.delete(sql, parameters);

            if (flag == 1)
            {
                return getShip_linesList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 按工单编号查询出货明细表数据
        /// </summary>
        /// <param name="wo_no"></param>
        /// <returns></returns>
        public List<ModelShip_lines> searchShipByWo_no(string wo_no)
        {
            string sql = "select * from wms_ship_lines where wo_no like '%' + @wo_no + '%'";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no", wo_no)
            };

            DB.connect();

            if (String.IsNullOrWhiteSpace(wo_no))
            {
                return getShip_linesList();
            }
            else
            {
                DataSet ds = DB.select(sql, parameters);
                List<ModelShip_lines> listmodel = new List<ModelShip_lines>();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        listmodel.Add(toModel(dr));
                    }

                    return listmodel;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 通过ship_key找到所有出货明细
        /// </summary>
        /// <param name="ship_key"></param>
        /// <returns></returns>
        public DataSet searchShip_linesByShip_key(int ship_key)
        {
            string sql = "select wms_ship_lines.*,wms_pn.item_name from wms_ship_lines " +
                "join wms_pn on wms_pn.item_id = wms_ship_lines.part_no ";
            if (ship_key > 0)
                sql = sql + " where ship_key = @ship_key";

            SqlParameter[] parameters = {
                new SqlParameter("ship_key", ship_key)
            };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }

        /// <summary>
        /// 通过wo_no在wms_wo中查询是否存在此数据
        /// </summary>
        /// <param name="wo_no"></param>
        /// <returns></returns>
        public bool getWOByWO_NO(string wo_no)
        {
            string sql = "select * from wms_wo where wo_no = @wo_no";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no",wo_no)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判断该WO_NO是否存在
        public bool getship_line_keyBywo(string wo_no, int ship_key)
        {
            string sql = "select ship_lines_key from wms_ship_lines where wo_no = @wo_no and ship_key=@ship_key";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no",wo_no),
                 new SqlParameter("ship_key",ship_key)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int getPicked_qtyByWo_no(string wo_no, int ship_key)
        {
            string sql = "select picked_qty from wms_ship_lines where wo_no = @wo_no and ship_key =@ship_key";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no",wo_no),
                new SqlParameter("ship_key",ship_key)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        /**作者：周雅雯 修改时间：2016/8/5
         * 根据wo_no查询出货明细表中所有数据， 和出货总表中的 客户ID、出货人员
         */
        public DataSet getSomeDataByWo_no(string wo_no)
        {
            //通过SQL语句，获取DateSet
            //string sql = "select *,(select customer_id from wms_ship where ship_key=(select ship_key from wms_ship_lines where wo_no=@wo_no)),(select ship_man from wms_ship where ship_key=(select ship_key from wms_ship_lines where wo_no=@wo_no)) "
            //              + "from wms_ship_lines "
            //              + "where wo_no=@wo_no ";
            string sql = "select b1.*,b2.customer_id,b2.ship_man " +
                         "from wms_ship_lines b1 ,wms_ship b2 " +
                         "where b1.wo_no like '%'+@wo_no+'%' and " +
                         "b2.ship_key=b1.ship_key ";
            SqlParameter[] parameters = {
                new SqlParameter("wo_no", wo_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            return ds;
        }

        /**作者：周雅雯 修改时间：2016/8/5
         * 根据wo_no更新出货量picked_qty,返回更新后的数据
         */
        public bool updateIssueHeader(string wo_no, int ship_key, int shipping_num, DateTime update_time)
        {
            string sql = "update wms_ship_lines "
                        + "set picked_qty = picked_qty+ @shipping_num,update_time = @update_time "
                        + "where wo_no = @wo_no and ship_key= @ship_key";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no",wo_no),
                 new SqlParameter("ship_key",ship_key),
                new SqlParameter("shipping_num",shipping_num),
                new SqlParameter("update_time",update_time)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)  //更新操作成功时
            {
                return true;
            }
            else//更新操作失败时
                return false;
        }

        /**作者：周雅雯 修改时间：2016/9/14
        * 出货页面对应的出货操作 
        * 更新出货明细表，修改在手总量表，修改在手明细表，插入交易表
        */
        public Boolean Shipping(string wo_no, string item_name, int shipping_qty, string customer_name, DateTime create_time, string login_id, int status)
        {

            string sql =
                    //"--更新出货明细表"
                    "UPDATE wms_ship_lines SET picked_qty= picked_qty+ @shipping_qty,update_time=@create_time  WHERE wo_no=@wo_no;"

                    //"--更新出货总表"
                    + "UPDATE wms_ship SET picked_qty= picked_qty+ @shipping_qty,update_time=@create_time,status=@status  WHERE ship_key=(select ship_key from wms_ship_lines where wo_no=@wo_no);"

                    //--修改在手总量表
                    + "UPDATE wms_items_onhand_qty_detail SET onhand_quantiy= onhand_quantiy- @shipping_qty,update_time=@create_time  WHERE item_name=@item_name;"

                    // "--修改在手明细表"
                    + "UPDATE wms_material_io SET onhand_qty= onhand_qty- @shipping_qty,update_time=@create_time  WHERE item_id=(select item_id from wms_pn where item_name=@item_name);"

                    // "--将数据插入到交易表"
                    + "INSERT INTO wms_transaction_operation (transaction_qty,transaction_type,transaction_time,create_user) VALUES (@shipping_qty,'Shipping',@create_time,@login_id)";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no", wo_no),
                new SqlParameter("item_name", item_name),
                new SqlParameter("shipping_qty", shipping_qty),
                new SqlParameter("customer_name", customer_name),
                new SqlParameter("create_time", create_time),
                new SqlParameter("login_id", login_id),
                new SqlParameter("status", status)
                };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.tran(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        private ModelShip_lines toModel(DataRow dr)
        {
            ModelShip_lines model = new ModelShip_lines();

            foreach (PropertyInfo propertyInfo in typeof(ModelShip_lines).GetProperties())
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