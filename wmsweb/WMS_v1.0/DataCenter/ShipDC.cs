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
    public class ShipDC
    {

        public DataSet getShipAndLines(string wo_no)
        {
            string sql = "SELECT b.wo_no,b.ship_man,a.part_no,a.request_qty,a.picked_qty,a.customer_id,a.status,a.create_time,a.update_time,a.ship_key  FROM wms_ship a,wms_ship_lines b where a.ship_key=b.ship_key and  b.wo_no like '%'+@wo_no+'%' ";

            DB.connect();

            SqlParameter[] parameter = {
                new SqlParameter("wo_no",wo_no)
                                       };

            DataSet ds = DB.select(sql, parameter);

            if (ds != null)
                return ds;
            else
                return null;
        }

        public List<string> getCustomerName()
        {

            string sql = "select * from wms_customers2";
            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["customer_name"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        public string getCustomerNameById(string customer_id)
        {

            string sql = "select customer_name from wms_customers2 where customer_key = @customer_key";

            SqlParameter[] parameter = {
                new SqlParameter("customer_key",customer_id)};

            DB.connect();
            DataSet ds = DB.select(sql, parameter);
            string customer_name = "";
            try
            {
                customer_name = ds.Tables[0].Rows[0]["customer_name"].ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return customer_name;

        }

        public string getCustomerIdByName(string customer_name)
        {

            string sql = "select customer_key from wms_customers2 where customer_name = @customer";

            SqlParameter[] parameter = {
                new SqlParameter("customer",customer_name)};
            DB.connect();
            DataSet ds = DB.select(sql, parameter);
            if (ds != null)
                return ds.Tables[0].Rows[0]["customer_key"].ToString();
            else
                return null;

        }

        //工单出货界面
        //根据出货单号查找客户代码及客户名称
        public DataSet getCustomerNameAndIdByAShipNo(string ship_no)
        {

            string sql = "select b.customer_name,b.customer_key,a.status,c.item_name from wms_ship a " +
                    "join wms_customers2 b on b.customer_key = a.customer_id " +
                    "join wms_pn c on c.item_id = a.part_no " +
                    "where a.ship_no = @ship_no ";

            SqlParameter[] parameter = {
                new SqlParameter("ship_no",ship_no)};

            DB.connect();
            DataSet ds = DB.select(sql, parameter);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }


        //工单出货页面
        //根据工单编号查询料号
        public DataSet getShipMassage(string ship_no)
        {

            string sql = "select * from wms_ship_lines a " +
                "join wms_ship b on b.ship_key = a.ship_key " + 
                "where b.ship_no = @ship_no ";

            SqlParameter[] parameter = {
                new SqlParameter("ship_no",ship_no)
        };
            DB.connect();
            DataSet ds = DB.select(sql, parameter);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        //工单出货页面
        //根据工单编号开工量
        public List<int> getTargetQtyByWoNo(string wo_no)
        {

            string sql = "select target_qty,turnin_qty,shipped_qty from wms_wo where wo_no = @wo_no";

            SqlParameter[] parameter = {
                new SqlParameter("wo_no",wo_no)};
            DB.connect();
            DataSet ds = DB.select(sql, parameter);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<int> list = new List<int>();
                list.Add(int.Parse(ds.Tables[0].Rows[0]["target_qty"].ToString()));
                list.Add(int.Parse(ds.Tables[0].Rows[0]["turnin_qty"].ToString()));
                list.Add(int.Parse(ds.Tables[0].Rows[0]["shipped_qty"].ToString()));
                return list;
            }
            else
                return null;
        }

        //工单出货页面
        //根据工单编号查询料号
        public string getPartNoByWoNo(string wo_no)
        {

            string sql = "select part_no from wms_wo where wo_no = @wo_no";

            SqlParameter[] parameter = {
                new SqlParameter("wo_no",wo_no)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameter);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0]["part_no"].ToString();
            else
                return null;
        }

        //工单出货页面
        //根据出货编号查询料号
        public string getPartNoByShip_no(string ship_no)
        {
            string sql = "select item_name from wms_ship " +
                    "join wms_pn on wms_pn.item_id = wms_ship.part_no " +
                    "where ship_no = @ship_no";

            SqlParameter[] parameter = {
                new SqlParameter("ship_no",ship_no)};
            DB.connect();
            DataSet ds = DB.select(sql, parameter);
            if (ds != null)
                return ds.Tables[0].Rows[0]["item_name"].ToString();
            else
                return null;
        }

        //工单出货页面
        //根据出货编号查询需求量和出货量
        public DataSet getResQtyAndPicQtyByShip_no(string ship_no)
        {

            string sql = "select request_qty,picked_qty from wms_ship where ship_no = @ship_no";

            SqlParameter[] parameter = {
                new SqlParameter("ship_no",ship_no)};
            DB.connect();
            DataSet ds = DB.select(sql, parameter);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }


        /// <summary>
        /// 得到出货总表全部数据的Model
        /// </summary>
        /// <returns></returns>
        public List<ModelShip> getShipList()
        {
            string sql = "SELECT * FROM wms_ship";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<ModelShip> modellist = new List<ModelShip>();
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

        public ModelShip getLastShip_no()
        {
            string sql = "select top 1 * from wms_ship order by [ship_no] desc";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            if (ds == null)
            {
                return null;

            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                    return toModel(ds.Tables[0].Rows[0]);
                else
                    return new ModelShip();
            }

        }

        public int getLast_id()
        {
            string sql = "select IDENT_CURRENT('wms_ship') as last_id";
            DB.connect();
            DataSet ds = DB.select(sql, null);

            if (ds == null || ds.Tables[0].Rows.Count <= 0)
                return 0;
            else
            {
                try
                {
                    int last_id = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                    return last_id;
                }
                catch (Exception ex)
                {

                }
            }
            return 0;
        }


        public bool queryShip_no(string ship_no)
        {
            string sql = "select ship_no from wms_ship where ship_no=@ship_no";
            SqlParameter[] parameters = {
                new SqlParameter("ship_no", ship_no)
            };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds == null || ds.Tables[0].Rows.Count <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// 通过客户id和出货人员新增一条出货总表的数据，并返回其Model
        /// </summary>
        /// <param name="customer_id"></param>
        /// <param name="ship_man"></param>
        /// <returns></returns>
        public DataSet insertAndReturnShip(string ship_no, int customer_id, int part_no, int request_qty, int picked_qty)
        {
            string sql = "insert into wms_ship(ship_no,customer_id,part_no,request_qty,picked_qty,status) " +
                "values(@ship_no,@customer_id,@part_no,@request_qty,@picked_qty,'0') ";

            SqlParameter[] parameters = {
                new SqlParameter("ship_no", ship_no),
                new SqlParameter("customer_id", customer_id),
                new SqlParameter("part_no", part_no),
                new SqlParameter("request_qty", request_qty),
                new SqlParameter("picked_qty", picked_qty)
            };

            DB.connect();
            int flag = DB.insert(sql, parameters);

            if (flag == 1)
            {
                return searchShipByCustomer_name("");
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更新出货总表，并返回所有Model
        /// </summary>
        /// <returns></returns>
        public DataSet updateShip(int ship_key, int customer_id, int part_no, int request_qty, int picked_qty)
        {
            string sql = "update wms_ship set customer_id=@customer_id,part_no=@part_no,request_qty=@request_qty,picked_qty=@picked_qty,update_time = GETDATE() where ship_key = @ship_key ";

            SqlParameter[] parameters = {
                new SqlParameter("ship_key", ship_key),
                new SqlParameter("customer_id", customer_id),
                new SqlParameter("part_no", part_no),
                new SqlParameter("request_qty", request_qty),
                new SqlParameter("picked_qty", picked_qty)
            };

            DB.connect();
            int flag = DB.update(sql, parameters);

            if (flag == 1)
            {
                return searchShipByCustomer_name("");
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除一条出货总表数据，并返回所有Model
        /// </summary>
        /// <param name="ship_key"></param>
        /// <returns></returns>
        public DataSet deleteShipById(int ship_key)
        {
            string sql = "delete from wms_ship where ship_key = @ship_key ; delete from wms_ship_lines where ship_key = @ship_key";

            SqlParameter[] parameters = {
                new SqlParameter("ship_key", ship_key)
            };

            DB.connect();
            int flag = DB.delete(sql, parameters);

            if (flag == 1)
            {
                return searchShipByCustomer_name("");
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 按customer_id查询出货总表数据
        /// </summary>
        /// <param name="customer_name"></param>
        /// <returns></returns>
        public DataSet searchShipByCustomer_name(string customer_name)
        {
            string sql = "select wms_ship.*,wms_customers2.customer_name,wms_pn.item_name from wms_ship " +
                "join wms_pn on wms_pn.item_id =wms_ship.part_no " +
                "join wms_customers2 on wms_customers2.customer_key = wms_ship.customer_id " +
                "where wms_customers2.customer_name like '%' + @customer_name + '%'";

            SqlParameter[] parameters = {
                new SqlParameter("customer_name", customer_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);
            return ds;
        }

        //通过料号和出货单号获取ship_key
        public string getship_keyBypart_no_and_ship_no(string ship_no, string part_no)
        {
            string sql = "select ship_key from wms_ship a " +
                "join wms_pn b on b.item_id=a.part_no " +
                "where b.item_name = @part_no and a.ship_no = @ship_no ";

            SqlParameter[] parameters = {
                new SqlParameter("part_no", part_no),
                new SqlParameter("ship_no", ship_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0][0].ToString();
            return null;
        }

        public bool query_ship_lines(string ship_key, string wo_no)
        {
            string sql = "select ship_lines_key from wms_ship_lines " +
                "where ship_key = @ship_key and wo_no = @wo_no ";

            SqlParameter[] parameters = {
                new SqlParameter("ship_key", ship_key),
                new SqlParameter("wo_no", wo_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return true;
            return false;
        }

        //工单出货页面
        //工单出货修改库存总表、工单表、出货总表、出货明细表、交易表的数据
        public Boolean workShipping(string ship_no, string wo_no, string part_no, int picked_qty, bool status, string ship_man, DateTime ship_time)
        {
            string ship_key = getship_keyBypart_no_and_ship_no(ship_no, part_no);
            if (ship_key == null)
                return false;
            string sql;
            string sqlHead;
            string sqlSecond;
            string sqlThird;
            string sqlTail;
            string sql6;
            //根据料号修改库存总表的在手量
            sqlHead = "update wms_items_onhand_qty_detail set onhand_quantiy = onhand_quantiy-@picked_qty where item_name = @part_no; ";

            //根据工单号修改工单表的出货量、出货人和出货时间
            sqlSecond = "update wms_wo set shipped_qty=shipped_qty+@picked_qty,update_time=@ship_time where wo_no=@wo_no;";

            if (status == false)
            {
                //加上此次的出货量不满足出货需求时，根据出货单号修改出货总表的出货量和出货时间
                sqlThird = "update wms_ship set picked_qty=picked_qty+@picked_qty,update_time=@ship_time where ship_no=@ship_no;";
            }
            else
            {
                //加上此次的出货量满足出货需求时，根据出货单号修改出货总表的出货量、出货时间和状态
                sqlThird = "update wms_ship set picked_qty=picked_qty+@picked_qty,update_time=@ship_time,status=1 where ship_no=@ship_no;";
            }
            //存在该ship_lines，更新
            if (query_ship_lines(ship_key, wo_no))
                //修改出货明细表的出货量、出货时间、出货人
                sqlTail = "update wms_ship_lines set picked_qty=picked_qty+@picked_qty,update_time=@ship_time,ship_man=@ship_man where wo_no=@wo_no;";
            else//不存在该ship_lines，新增
                sqlTail = "insert into wms_ship_lines (ship_key,part_no,wo_no,picked_qty,ship_man,create_time) values(@ship_key,@part_no,@wo_no,@picked_qty,@ship_man,@ship_time) ;";

            //插入数据到交易表中
            sql6 = "insert into wms_transaction_operation (invoice_no,item_name,transaction_type,transaction_qty,transaction_time,create_user) values(@ship_no,@part_no,'shipping',@picked_qty,@ship_time,@ship_man)";

            sql = sqlHead + sqlSecond + sqlThird + sqlTail + sql6;

            SqlParameter[] parameters = {
                new SqlParameter("ship_key", ship_key),
                new SqlParameter("part_no", part_no),
                new SqlParameter("wo_no", wo_no),
                new SqlParameter("picked_qty", picked_qty),
                new SqlParameter("ship_man", ship_man),
                new SqlParameter("ship_time", ship_time),
                new SqlParameter("ship_no", ship_no)
                };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.tran(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        public ModelShip getshipbykey(int ship_key)
        {
            string sql = "select * from wms_ship where ship_key = @ship_key ";
            SqlParameter[] parameters = {
                new SqlParameter("ship_key", ship_key)
            };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            ModelShip model = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    model = toModel(dr);
                }
            }
            return model;
        }
        public ModelShip getshipbyship_linekey(int ship_lines_key)
        {
            string sql = "select wms_ship.* from wms_ship " +
                "join wms_ship_lines on wms_ship_lines.ship_key = wms_ship.ship_key " +
                "where wms_ship_lines.ship_lines_key = @ship_lines_key ";
            SqlParameter[] parameters = {
                new SqlParameter("ship_lines_key", ship_lines_key)
            };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            ModelShip model = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    model = toModel(dr);
                }
            }
            return model;
        }
        private ModelShip toModel(DataRow dr)
        {
            ModelShip model = new ModelShip();

            foreach (PropertyInfo propertyInfo in typeof(ModelShip).GetProperties())
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