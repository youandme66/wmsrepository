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

    //By zrk
    public class Receive_mtlDC
    {

        /**作者周雅雯，时间：2016/10/29
         * 根据Po_header_id和line_id返回rcv_qty 暂收数量
         * */
        public int getRcv_qtyByPo_header_idAndline_id(int po_header_id, int po_line_id)
        {
            string sql = "select rcv_qty from wms_receive_mtl where po_header_id=@po_header_id AND po_line_id=@po_line_id  ";

            SqlParameter[] parameters = {
                new SqlParameter("po_header_id", po_header_id),
                new SqlParameter("po_line_id", po_line_id),
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);
            int rcv_qty = 0;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
              
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        rcv_qty += int.Parse(dr["rcv_qty"].ToString());
                    }
                    catch
                    {
                        rcv_qty = 0;
                    }
                }
                return rcv_qty;
            }else
                return -1;
        }

        /// <summary>
        /// 插入数据到暂收表
        /// </summary>
        /// <param name="unique_id"></param>
        /// <param name="lot_number"></param>
        /// <param name="receipt_no"></param>
        /// <param name="item_id"></param>
        /// <param name="item_name"></param>
        /// <param name="rcv_qty"></param>
        /// <param name="accepted_qty"></param>
        /// <param name="return_qty"></param>
        /// <param name="deliver_qty"></param>
        /// <param name="po_no"></param>
        /// <param name="po_header_id"></param>
        /// <param name="po_line_id"></param>
        /// <param name="vendor_code"></param>
        /// <param name="receive_time"></param>
        /// <param name="create_time"></param>
        /// <param name="update_time"></param>
        /// <param name="datecode"></param>
        /// <returns></returns>
        public bool insertReceive_mtl(int lot_number, string receipt_no, int item_id, string item_name, int rcv_qty, int accepted_qty,
            int return_qty, int deliver_qty, string po_no, int po_header_id, int po_line_id, string vendor_code, DateTime receive_time,
            DateTime create_time, DateTime update_time, string datecode)
        {
            string sql = "insert into wms_receive_mtl(LOT_NUMBER,RECEIPT_NO,ITEM_ID,ITEM_NAME,DATECODE,RCV_QTY,ACCEPTED_qty,DELIVER_QTY,RETURN_qty,PO_NO,PO_HEADER_ID,PO_LINE_ID,VENDOR_CODE,RECEIVE_TIME,CREATE_TIME,UPDATE_TIME) values(@UNIQUE_ID, @LOT_NUMBER, @RECEIPT_NO, @ITEM_ID, @ITEM_NAME, @DATECODE, @RCV_QTY, @ACCEPTED_qty, @DELIVER_QTY, @RETURN_qty, @PO_NO, @PO_HEADER_ID, @PO_LINE_ID, @VENDOR_CODE, @RECEIVE_TIME, @CREATE_TIME, @UPDATE_TIME)";

            SqlParameter[] parameters = {
                new SqlParameter("LOT_NUMBER", lot_number), new SqlParameter("RECEIPT_NO", receipt_no),
                new SqlParameter("ITEM_ID", item_id), new SqlParameter("ITEM_NAME", item_name), new SqlParameter("DATECODE", datecode),
                new SqlParameter("RCV_QTY", rcv_qty), new SqlParameter("ACCEPTED_qty", accepted_qty), new SqlParameter("DELIVER_QTY", deliver_qty),
                new SqlParameter("RETURN_qty", return_qty), new SqlParameter("PO_NO", po_no), new SqlParameter("PO_HEADER_ID", po_header_id),
                new SqlParameter("PO_LINE_ID", po_line_id), new SqlParameter("VENDOR_CODE", vendor_code), new SqlParameter("RECEIVE_TIME", receive_time),
                new SqlParameter("CREATE_TIME", create_time), new SqlParameter("UPDATE_TIME", update_time)
            };

            DB.connect();
            int flag = DB.insert(sql, parameters);

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
        /// 模糊查询
        /// </summary>
        /// <param name="receipt_no"></param>
        /// <param name="po_no"></param>
        /// <param name="item_name"></param>
        /// <returns></returns>
        public List<ModelReceive_mtl> searchReceive_mtlByReceipt_noAndPOIdOrItemName(string receipt_no, string po_no, string item_name)
        {
            string sql = "select * from wms_receive_mtl where 1=1 ";
            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no),
                new SqlParameter("po_no", po_no),
                new SqlParameter("item_name", item_name)
            };

            List<ModelReceive_mtl> modellist = null;
            DataSet ds = null;
            DB.connect();

            if (!string.IsNullOrWhiteSpace(receipt_no))
            {
                sql += "AND receipt_no like '%' + @receipt_no +'%' ";
            }
            if (!string.IsNullOrWhiteSpace(po_no))
            {
                sql += "AND po_no like '%' + @po_no + '%' ";
            }
            if (!string.IsNullOrWhiteSpace(item_name))
            {
                sql += "AND item_name like '%' + @item_name + '%' ";
            }

            ds = DB.select(sql, parameters);

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
        //更改人：周雅雯 时间：2016/7/30  原因：不需要更新交易表wms_transaction_operation中交易类型TRANSACTION_TYPE
        /// <summary>
        ///// 删除一条Receive_mtl数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteReceive_mtl(string receipt_no)
        {

            string sql = "delete from wms_receive_mtl where RECEIPT_NO = @receipt_no";
            //+ "update wms_transaction_operation set TRANSACTION_TYPE = @type where invoice_no = @receipt_no";

            SqlParameter[] parameters = { 
                new SqlParameter("receipt_no", receipt_no),
                new SqlParameter("type", "reject")           
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


        //<summary>
        //通过receipt_no修改一条数据状态为2，并修改在手总量表的在手总量
        //</summary>
        //<param name="receipt_no"></param>
        //<returns></returns>
        public ModelReceive_mtl changeStatusByReceipt_no(string receipt_no)
        {
            string sql = "update wms_receive_mtl set STATUS = @status, update_time = GETDATE() where RECEIPT_NO = @receipt_no";

            SqlParameter[] parameters = { 
                new SqlParameter("receipt_no", receipt_no),
                new SqlParameter("status", 2)            
            };

            DB.connect();
            int flag = DB.update(sql, parameters);

            if (flag == 1)
            {
                string select = "select * from wms_receive_mtl where RECEIPT_NO = @receipt_no";

                DataSet ds = DB.select(select, parameters);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string update = "update WMS_ITEMS_ONHAND_QTY_DETAIL set ONHAND_QUANTIY = @onhand_quantiy, update_time = GETDATE() where ITEM_NAME = @item_name";

                    SqlParameter[] updateparameters = { 
                        new SqlParameter("onhand_quantiy", ds.Tables[0].Rows[0]["RCV_QTY"]),
                        new SqlParameter("item_name", ds.Tables[0].Rows[0]["ITEM_NAME"])          
                    };

                    int tempfalg = DB.update(update, updateparameters);

                    return toModel(ds.Tables[0].Rows[0]); ;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }




        /**作者周雅雯，时间：2016/7/26
         * 比较暂收数量（rcv_qty）和允收量（Accepted_qty ）是否相等，
         * 若不相等则把入库量（deliver_qty）修改为允收量（Accepted_qty）
         * **/
        public Boolean CompareRec_QtyAndAccepted_qty(string receipt_no)
        {
            //通过SQL语句，获取DateSet
            string sql = "select rcv_qty,Accepted_qty from wms_receive_mtl where receipt_no = @receipt_no";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)   //查询操作成功
            {
                //比较暂收数量（Rec_Qty）和允收量（Accepted_qty ）

                //如果不等
                if (ds.Tables[0].Rows[0]["rcv_qty"] != ds.Tables[0].Rows[0]["Accepted_qty"])
                {
                    Receive_mtlDC receive_dc = new Receive_mtlDC();
                    Boolean flag;

                    //则把入库量（deliver_qty）修改为允收量（Accepted_qty）
                    flag = receive_dc.updateDeliver_qtyByReceipt_no(receipt_no);
                    if (flag == true)   //修改成功
                        return true;
                    else
                        return false;
                }
                //如果相等，则返回真，不修改
                else
                    return true;
            }
            else            //查询操作失败
                return false;
        }


        /**作者周雅雯，时间：2016/8/1
        * 通过暂收单号获取入库量（deliver_qty）
        * **/
        public int getDeliver_qtyByReceipt_no(string receipt_no)
        {
            //通过SQL语句，获取DateSet
            string sql = "select deliver_qty from wms_receive_mtl where receipt_no = @receipt_no";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)   //查询操作成功
            {
                int deliver_qty = int.Parse(ds.Tables[0].Rows[0]["deliver_qty"].ToString());
                return deliver_qty;
            }
            else
                return -1;

        }

        //如果已经做完允收的暂收单号，就不要显示在这边
        public List<string> getAllValidReceipt_no()
        {

            string sql = "select receipt_no from wms_receive_mtl r where ((select rcv_qty from wms_receive_mtl where receipt_no = r.receipt_no) > ((select accepted_qty from wms_receive_mtl where receipt_no = r.receipt_no) + (select return_qty from wms_receive_mtl where receipt_no = r.receipt_no))) ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["receipt_no"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        /**作者周雅雯，时间：2016/9/2
         * 返回暂收表中的所有暂收单号receipt_no
         * **/
        public List<string> getAllReceipt_no()
        {

            string sql = "select receipt_no from wms_receive_mtl  ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["receipt_no"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }


        /**作者周雅雯，时间：2016/7/26
         * 把入库量（deliver_qty）修改为允收量（Accepted_qty）
         * */
        public Boolean updateDeliver_qtyByReceipt_no(string receipt_no)
        {
            string sql = "update wms_receive_mtl "
                      + "set deliver_qty = Accepted_qty "
                      + "where receipt_no = @receipt_no";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no)
                };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /**作者周雅雯，时间：2016/7/26
         * 修改在手总量表wms_items_onhand_qty_detail的“在手总量（Onhand_quantiy）”
         * 和wms_material_io在手数量明细表的“在手量(Onhand_qty)”,
         * 将这两个数量增加，增加数量为暂收表“入库量（deliver_qty）
         * **/
        public Boolean updateOnhand_quantiyAndOnhand_qtyByReceipt_no(string receipt_no)
        {
            string sql = "update wms_items_onhand_qty_detail "
                      + "set Onhand_quantiy =Onhand_quantiy +(select deliver_qty from wms_receive_mtl where receipt_no=@receipt_no ) "
                      + "where item_id IN (select item_id from wms_receive_mtl where receipt_no=@receipt_no); "
                      + "update wms_material_io "
                      + "set Onhand_qty =Onhand_qty +(select deliver_qty from wms_receive_mtl where receipt_no=@receipt_no ) "
                      + "where item_id IN (select item_id from wms_receive_mtl where receipt_no=@receipt_no) "
                      + "AND datecode IN (select datecode from wms_receive_mtl where receipt_no=@receipt_no) ";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /**作者：周雅雯 修改时间：2016/7/30
         * 入库按钮对应的操作 
         */
        public Boolean inStorage(string receipt_no)
        {
            //1.比较暂收数量（Rec_Qty）和允收量，若不相等则把入库量（deliver_qty）修改为允收量（Accepted_qty）
            Receive_mtlDC receive_dc1 = new Receive_mtlDC();
            Boolean flag1 = receive_dc1.CompareRec_QtyAndAccepted_qty(receipt_no);

            //2.修改庫存總表庫存量，庫存明細表里對應的料號+datecode 的數量
            Receive_mtlDC receive_dc2 = new Receive_mtlDC();
            Boolean flag2 = receive_dc2.updateOnhand_quantiyAndOnhand_qtyByReceipt_no(receipt_no);

            //通过暂收单号获取入库量
            Receive_mtlDC receive_dc3 = new Receive_mtlDC();
            int deliver_qty = receive_dc3.getDeliver_qtyByReceipt_no(receipt_no);
            if (deliver_qty == -1)  //获取入库量失败时
                return false;

            //3.在交易表中插入入庫信息
            Transaction_operationDC transaction_operationdc = new Transaction_operationDC();
            DateTime transaction_time = DateTime.Now;
            Boolean flag3 = transaction_operationdc.insertTransaction_operation(deliver_qty, "ruku", transaction_time);

            if (flag1 == true && flag2 == true && flag3 == true)  //当且仅当三个操作都满足要求时，返回true
                return true;
            else
                return false;
        }


        /**作者：周雅雯 修改时间：2016/8/12
        * PO暂收对应的操作 
        * 根據料號+datecod+po+數量收料，做賬時將數據插入到暫收表、交易表中
        */
        public Boolean poSuspense(string item_name, int item_id, string datecode, int rcv_qty, string po_no, string vendor_code, int po_header_id, int po_line_id, DateTime transaction_time, string create_user)
        {
            string sql =
                //+ "--将数据插入到暂收表中"
                         "insert into wms_receive_mtl "
                        + "(accepted_qty,deliver_qty,RETURN_qty,item_name,item_id,datecode,rcv_qty,po_no,vendor_code,po_header_id,po_line_id)values "
                        + "('0','0','0',@item_name,@item_id,@datecode,@rcv_qty,@po_no,@vendor_code,@po_header_id,@po_line_id);"
                //+ "--将数据插入到交易表"
                        + "INSERT INTO wms_transaction_operation (transaction_qty,transaction_type,transaction_time,create_user) VALUES (@rcv_qty,'PoSuspense',@transaction_time,@create_user)";


            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
                 new SqlParameter("item_id", item_id),
                new SqlParameter("datecode", datecode),
                new SqlParameter("rcv_qty", rcv_qty),
                new SqlParameter("po_no", po_no),
                 new SqlParameter("vendor_code", vendor_code),
                new SqlParameter("po_header_id", po_header_id),                     
                new SqlParameter("po_line_id", po_line_id),
                new SqlParameter("transaction_time", transaction_time),
                new SqlParameter("create_user", create_user),
                };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.tran(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        /**作者：周雅雯 时间：2016/8/12
        * PO暂收,PO入库页面需要的查询方法
        * 通过receipt_no暂收单号，item_name料号，po_no采购单号，获取ModelReceive_mtl对象的列表集合
        **/
        public List<ModelReceive_mtl> getReceive_mtlBySome(string receipt_no, string item_name, string po_no)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当receipt_no有值时
            if (string.IsNullOrWhiteSpace(receipt_no) == false)
            {
                sqlTail += "AND receipt_no LIKE '%'+@receipt_no+'%' ";
            }
            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name LIKE '%'+@item_name+'%' ";
            }
            //当po_no有值时
            if (string.IsNullOrWhiteSpace(po_no) == false)
            {
                sqlTail += "AND po_no LIKE '%'+@po_no+'%' ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_receive_mtl ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_receive_mtl WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("receipt_no", receipt_no),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("po_no", po_no),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            List<ModelReceive_mtl> ModelReceive_mtllist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelReceive_mtllist = new List<ModelReceive_mtl>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelReceive_mtllist.Add(toModel(DateSetRows));
                }
                return ModelReceive_mtllist;
            }
            else
            {
                return ModelReceive_mtllist;
            }
        }

        //PO入库页面查询完成未入库的暂收单信息
        public List<ModelReceive_mtl> getReceive_mtlByNotInstorage(string receipt_no, string item_name, string po_no)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当receipt_no有值时
            if (string.IsNullOrWhiteSpace(receipt_no) == false)
            {
                sqlTail += "AND receipt_no LIKE '%'+@receipt_no+'%' ";
            }
            //当item_name有值时
            if (string.IsNullOrWhiteSpace(item_name) == false)
            {
                sqlTail += "AND item_name LIKE '%'+@item_name+'%' ";
            }
            //当po_no有值时
            if (string.IsNullOrWhiteSpace(po_no) == false)
            {
                sqlTail += "AND po_no LIKE '%'+@po_no+'%' ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_receive_mtl where status='N' ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_receive_mtl WHERE status='N'  " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("receipt_no", receipt_no),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("po_no", po_no),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            List<ModelReceive_mtl> ModelReceive_mtllist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                ModelReceive_mtllist = new List<ModelReceive_mtl>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    ModelReceive_mtllist.Add(toModel(DateSetRows));
                }
                return ModelReceive_mtllist;

            }
            else
            {
                return ModelReceive_mtllist;
            }
        }


        /**作者：周雅雯 修改时间：2016/8/9               修改：姜江          时间：2016/11/19
        * PO入库对应的操作 
        * 修改暫收表入庫數量(DELIVER_QTY)，修改庫存總表庫存量，庫存明細表里對應的料號+datecode數量，在交易表中插入入庫信息
        */
        public Boolean inStorage(string receipt_no, string item_name, int deliver_qty, string datecode, string frame_name, string issued_sub_key, DateTime transaction_time, string status, bool flag, bool flag1)
        {
            string sql;
            string sqlHead;
            string sqlSecond;
            string sqlThird;
            string sqlTail;
            if (status == "N")
            {
                sqlHead =
                    //+ "--修改暫收表入庫數量(DELIVER_QTY) "
                        "update wms_receive_mtl "
                       + "set deliver_qty = deliver_qty+@deliver_qty, datecode= @datecode, update_time=@transaction_time "
                       + "where receipt_no = @receipt_no; ";
            }
            else
            {
                sqlHead =
                    //+ "--修改暫收表入庫數量(DELIVER_QTY) 并修改入库状态"
                "update wms_receive_mtl "
               + "set deliver_qty = deliver_qty+@deliver_qty, datecode= @datecode,status='Y',update_time=@transaction_time "
               + "where receipt_no = @receipt_no; ";
            }
            if (flag == false)
            {
                //+ "插入庫存總表庫数据"
                sqlSecond = "INSERT INTO wms_items_onhand_qty_detail (item_name,item_id,onhand_quantiy,subinventory,create_time)VALUES(@item_name,(select item_id from wms_pn where item_name = @item_name),@deliver_qty,@issued_sub_key,@transaction_time);";
            }
            else
            {
                //+ "--修改庫存總表庫存量"
                sqlSecond = "update wms_items_onhand_qty_detail set onhand_quantiy = onhand_quantiy + @deliver_qty, update_time=@transaction_time where item_id = (select item_id from wms_pn where item_name = @item_name) and subinventory=@issued_sub_key;";
            }
            if (flag1 == false)
            {
                //+ "插入庫存明细表庫数据"
                sqlThird = "INSERT INTO wms_material_io (item_id,onhand_qty,frame_key,subinventory,datecode,create_time)VALUES((select item_id from wms_pn where item_name = @item_name),@deliver_qty,(select frame_key from wms_frame where frame_name = @frame_name),@issued_sub_key,@datecode,@transaction_time);";
            }
            else
            {
                //+ "--修改庫存明细表庫存量"
                sqlThird = "UPDATE wms_material_io SET onhand_qty=onhand_qty + @deliver_qty, update_time=@transaction_time WHERE  item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name) and subinventory=@issued_sub_key and frame_key=(select frame_key from wms_frame where frame_name = @frame_name);";
            }
            //+ "--将数据插入到交易表"
            sqlTail = "INSERT INTO wms_transaction_operation (transaction_qty,transaction_type,transaction_time) VALUES (@deliver_qty,'PoInStorage',@transaction_time)";

            sql = sqlHead + sqlSecond + sqlThird + sqlTail;

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no),
                new SqlParameter("item_name", item_name),
                new SqlParameter("frame_name", frame_name),
                new SqlParameter("deliver_qty", deliver_qty),
                new SqlParameter("datecode", datecode),
                new SqlParameter("issued_sub_key", issued_sub_key),
                new SqlParameter("transaction_time", transaction_time)
                };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.tran(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        /**作者：周雅雯 修改时间：2016/8/9
         * PO退回对应的操作 
         * 修改暂收表中数据,将数据插入到PO退回总表,将数据插入到PO退回明细表,将数据插入到交易表
         */
        public Boolean poReturn_first(string receipt_no, String vendor_key, int line_num, string po_no, String item_name, int required_qty, string return_sub, string return_region, string return_user, DateTime return_time, DateTime transaction_time)
        {
            string sql =
                //"--修改暂收表中数据"
                "UPDATE wms_receive_mtl SET return_qty=return_qty + @required_qty WHERE receipt_no=@receipt_no;" +

                 // "--将数据插入到PO退回总表"//修改：查找subinventory——key通过subinventory_name
                 "INSERT INTO wms_po_return_header (receipt_no,vendor_name,return_region,return_wo_no,return_time) VALUES (@receipt_no,(SELECT vendor_name FROM wms_customers WHERE vendor_key=@vendor_key),(SELECT region_key FROM wms_region WHERE region_name=@return_region),@return_wo_no,@return_time);" +

                       // "--将数据插入到PO退回明细表"
                 "INSERT INTO wms_po_return_detail (return_header_id,line_num,po_no,item_name, required_qty,return_sub,return_wo_no,return_time) VALUES ((SELECT return_header_id FROM wms_po_return_header WHERE receipt_no=@receipt_no AND return_time =@return_time),@line_num,@po_no,@item_name,@required_qty,(SELECT subinventory_key FROM wms_subinventory WHERE subinventory_name=@return_sub),@return_wo_no,@return_time);" +

                       // "--将数据插入到交易表"
                 "INSERT INTO wms_transaction_operation (item_name,po_no,transaction_qty,transaction_type,transaction_time,create_user) VALUES (@item_name,@po_no,@required_qty,'PoReturn',@transaction_time,@return_wo_no)";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no),
                new SqlParameter("required_qty", required_qty),
                new SqlParameter("vendor_key", vendor_key),
                new SqlParameter("return_region", return_region),
                new SqlParameter("return_wo_no", return_user),
                new SqlParameter("return_time", return_time),
                new SqlParameter("line_num", line_num),
                new SqlParameter("po_no", po_no),
                new SqlParameter("item_name", item_name),
                new SqlParameter("return_sub", return_sub),
                new SqlParameter("transaction_time", transaction_time)
                };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.tran(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /**作者：周雅雯 修改时间：2016/8/9
         * PO退回对应的操作 
         * 删除暂收表中数据,将数据插入到PO退回总表,将数据插入到PO退回明细表,将数据插入到交易表
         */
        public Boolean poReturn_second(string receipt_no, String vendor_key, int line_num, string po_no, String item_name, int required_qty, string return_sub, string return_region, string return_wo_no, DateTime return_time, DateTime transaction_time)
        {
            string sql =
                //"--删除暂收表中数据"
                 "DELETE FROM wms_receive_mtl WHERE receipt_no=@receipt_no;" +

                 // "--将数据插入到PO退回总表"//修改：查找subinventory——key通过subinventory_name
                 "INSERT INTO wms_po_return_header (receipt_no,vendor_name,return_region,return_wo_no,return_time) VALUES (@receipt_no,(SELECT vendor_name FROM wms_customers WHERE vendor_key=@vendor_key),(SELECT region_key FROM wms_region WHERE region_name=@return_region),@return_wo_no,@return_time);" +

                 // "--将数据插入到PO退回明细表"
                 "INSERT INTO wms_po_return_detail (return_header_id,line_num,po_no,item_name, required_qty,return_sub,return_wo_no,return_time) VALUES ((SELECT return_header_id FROM wms_po_return_header WHERE receipt_no=@receipt_no AND return_time =@return_time),@line_num,@po_no,@item_name,@required_qty,(SELECT subinventory_key FROM wms_subinventory WHERE subinventory_name=@return_sub),@return_wo_no,@return_time);" +

                 // "--将数据插入到交易表"
                 "INSERT INTO wms_transaction_operation (item_name,po_no,transaction_qty,transaction_type,transaction_time,create_user) VALUES (@item_name,@po_no,@required_qty,'PoReturn',@transaction_time,@return_wo_no)";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no),
                new SqlParameter("vendor_key", vendor_key),
                new SqlParameter("line_num", line_num),
                new SqlParameter("po_no", po_no),
                new SqlParameter("item_name", item_name),
                new SqlParameter("required_qty", required_qty),
                new SqlParameter("return_sub", return_sub),
                new SqlParameter("return_region", return_region),
                new SqlParameter("return_wo_no", return_wo_no),
                new SqlParameter("return_time", return_time),
                new SqlParameter("transaction_time", transaction_time),
                };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.tran(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }





        /**作者周雅雯，时间：2016/7/26
         * PO暂收对应操作
         * 往暂收表(wms_receive_mtl)中插入一条数据，除了传过来的参数，还有PO_HEADER_ID、PO_LINE_ID（从单身表里拿）,ACCEPTED_QTY
         * ACCEPTED_QTY字段的存储为：PO单身表的Request_qty 减去在手总量表的在手总量
         * **/
        public Boolean insertReceive_mtl(int item_id, string datecode, int rcv_qty, int deliver_qty, string po_no, string vendor_code, DateTime receive_time, DateTime update_time)
        {

            string sql = "insert into wms_receive_mtl "
                       + "(item_id,datecode,rcv_qty,deliver_qty,po_no,vendor_code,receive_time,update_time,po_header_id,po_line_id,accepted_qty)values "
                       + "(@item_id,@datecode,@rcv_qty,@deliver_qty,@po_no,@vendor_code,@receive_time,@update_time, "
                       + "(select po_header_id from wms_po_header where po_no=@po_no), "
                       + "(select po_line_id from wms_po_line where po_header_id IN (select po_header_id from wms_po_header where po_no=@po_no)), "
                       + "(select request_qty from wms_po_line where po_header_id IN (select po_header_id from wms_po_header where po_no=@po_no)-(select onhand_quantiy from wms_items_onhand_qty_detail where item_id=@item_id)))) ";

            SqlParameter[] parameters = {
                new SqlParameter("item_id",item_id),
                new SqlParameter("datecode",datecode),
                new SqlParameter("rcv_qty",rcv_qty),
                new SqlParameter("deliver_qty",deliver_qty),
                new SqlParameter("po_no",po_no),
                new SqlParameter("vendor_code",vendor_code),
                new SqlParameter("receive_time",receive_time),
                new SqlParameter("update_time",update_time),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /**作者周雅雯，时间：2016/8/1
         * PO暂收对应操作
         * 根據料號+datecod+po+數量收料
         * **/
        public Boolean insertReceive_mtl(int item_id, string datecode, int rcv_qty, string po_no)
        {

            string sql = "insert into wms_receive_mtl "
                       + "(item_id,datecode,rcv_qty,po_no)values "
                       + "(@item_id,@datecode,@rcv_qty,@po_no)";


            SqlParameter[] parameters = {
                new SqlParameter("item_id",item_id),
                new SqlParameter("datecode",datecode),
                new SqlParameter("rcv_qty",rcv_qty),
                new SqlParameter("po_no",po_no)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }




        /**作者周雅雯，时间：2016/7/27
         * PO退回对应操作
         * 根据receipt_no 暂收单号在 暂收表 中 找到rcv_qty 暂收数量，return_qty退回量
         * 把Rcv_qty的值减少return_num(用户填写的退回量),return_qty的值加上return_num，
         * */
        public Boolean updateRcv_qtyAndReturn_qty(string receipt_no, int return_num)
        {
            string sql = "update wms_receive_mtl "
                    + "set Rcv_qty=Rcv_qty-@return_num,return_qty=return_qty+@return_num "
                    + "where receipt_no=@receipt_no";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no",receipt_no),
                new SqlParameter("return_num",return_num)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;


        }

        /**作者周雅雯，时间：2016/9/2
        * PO允收对应操作
        * 根据receipt_no 暂收单号返回rcv_qty 暂收数量
        * */
        public int getRcv_qtyByReceipt_no(string receipt_no)
        {
            string sql = "select rcv_qty from wms_receive_mtl where receipt_no=@receipt_no  ";


            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)   //查询操作成功
            {
                int rcv_qty = int.Parse(ds.Tables[0].Rows[0]["rcv_qty"].ToString());
                return rcv_qty;
            }
            else
                return -1;
        }

        /// <summary>
        /// 通过暂收单号查询五个表中相应的数据
        /// </summary>
        /// <param name="receipt_no"></param>
        /// <param name="po_no"></param>
        /// <param name="item_name"></param>
        /// <returns></returns>
        public DataSet getSomeFieldsByReceipt_no(string receipt_no)
        {
            //涉及到的字段和表
            string sql = "select * "
                + " from wms_receive_mtl b1,wms_po_line b2 where b2.po_line_id = b1.po_line_id AND b1.RECEIPT_NO = @receipt_no";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no)
            };

            DataSet ds = null;
            DB.connect();
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

        /// <summary>
        /// 通过几个参数查询五个表中相应的数据
        /// </summary>
        /// <param name="receipt_no"></param>
        /// <param name="po_no"></param>
        /// <param name="item_name"></param>
        /// <returns></returns>
        public DataSet getSomeFieldsByReceipt_noAndPO_noAndItem_name(string receipt_no, string po_no, string item_name)
        {
            //涉及到的字段和表
            string sql = "select * "
                + " from wms_receive_mtl b1,wms_po_line b2 where b2.po_line_id = b1.po_line_id AND b1.RECEIPT_NO like '%' + @receipt_no + '%' ";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no),
                new SqlParameter("po_no", po_no),
                new SqlParameter("item_name", item_name)
            };

            DataSet ds = null;
            DB.connect();

            if (!string.IsNullOrWhiteSpace(po_no))
            {
                sql += "AND po_no like '%' + @po_no + '%' ";
            }
            if (!string.IsNullOrWhiteSpace(item_name))
            {
                sql += "AND item_name like '%' + @item_name + '%' ";
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


        private ModelReceive_mtl toModel(DataRow dr)
        {
            ModelReceive_mtl model = new ModelReceive_mtl();

            foreach (PropertyInfo propertyInfo in typeof(ModelReceive_mtl).GetProperties())
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


        /**作者GYM，时间：2016/8/3   
         * 暂收表查询操作  (精确查询)
         * 通过暂收单号查询暂收表里的数据（receipt_no）
         * */
        public List<ModelReceive_mtl> searchReceive_mtlByReceipt_no(string receipt_no)
        {
            string sql = "select * from wms_receive_mtl where RECEIPT_NO =@receipt_no";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);
            List<ModelReceive_mtl> modellist = null;

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                modellist = new List<ModelReceive_mtl>(ds.Tables[0].Rows.Count);

                foreach (DataRow DateSetRows in ds.Tables[0].Rows)
                {
                    modellist.Add(toModel(DateSetRows));
                }
                return modellist;
            }
            else
            {
                return modellist;
            }
        }


        /**作者GYM，时间：2016/8/3   
         * 暂收表修改操作
         * 通过暂收单号（receipt_no）查询到需要修改的数据，
         * 再根据传过来的参数修改允收量（accepted_qty）和退回量（return_qty）。
         * */
        public Boolean updateAccepted_qtyAndReturn_qty(string receipt_no, int accepted_qty, int return_qty)
        {
            string sql = "update wms_receive_mtl "
                    + "set Accepted_qty=@accepted_qty,Return_qty=@return_qty "
                    + "where receipt_no=@receipt_no";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no",receipt_no),
                new SqlParameter("accepted_qty",accepted_qty),
                new SqlParameter("return_qty",return_qty)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }



        internal bool updateAccepted_qtyAndReturn_qty()
        {
            throw new NotImplementedException();
        }
    }
}