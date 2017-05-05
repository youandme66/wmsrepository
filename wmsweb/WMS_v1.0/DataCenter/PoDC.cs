using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication1;

namespace WMS_v1._0.DataCenter
{
    /*
     * PODC：
     * 此PoDC中的方法服务于：PO设定、PO暂收、PO允收、PO入库这4个页面即PO逻辑
     * 负责人：郭一朦
     * 
     * */
    public class PoDC
    {

        // 获得数据库中所有厂商代码 
        public List<string> getAllVendor_key()
        {
            string sql = "select vendor_key from wms_customers  ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["vendor_key"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        //根据po单号、vendor_key两者模糊搜索PO单头中数据
        public DataSet getPO_HeaderBySome(string po_no, string vendor_key)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";

            //当po_no有值时
            if (string.IsNullOrWhiteSpace(po_no) == false)
            {
                sqlTail += "AND po_no LIKE '%'+@po_no+'%' ";
            }
            //当vendor_key有值时
            if (string.IsNullOrWhiteSpace(vendor_key) == false)
            {
                sqlTail += "AND vendor_key LIKE '%'+@vendor_key+'%' ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_po_header ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_po_header  WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("po_no", po_no),
                    new SqlParameter("vendor_key", vendor_key)
                };

            DataSet ds = DB.select(sqlAll, parameters);

            return ds;
        }

        //通过PO_NO返回POHeader所有数据
        public DataSet getPOHeaderByPo_no(string Po_no)
        {
            //通过SQL语句，获取DateSet
            string sql = "select * from wms_po_header where po_no = @po_no";

            SqlParameter[] parameters = {
                new SqlParameter("Po_no", Po_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            return ds;
        }

        //向po单头表中插入部分数据
        public Boolean insertPoHeader(string po_no, string vendor_key, int create_by, DateTime create_time)
        {

            string sql = "insert into wms_po_header "
                       + "(po_no,vendor_key,create_by,create_time)values "
                       + "(@po_no,@vendor_key,@create_by,@create_time) ";

            SqlParameter[] parameters = {
                new SqlParameter("po_no",po_no),
                new SqlParameter("vendor_key",vendor_key),
                new SqlParameter("create_by",create_by),
                new SqlParameter("create_time",create_time),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        //更新po单头表中的部分数据
        public Boolean updatePoHeader(string po_no,string vendor_key, int update_by ,DateTime update_time)
        {
            string sql = "update wms_po_header "
                        + "set po_no = @po_no,vendor_key = @vendor_key,update_by = @update_by,update_time=@update_time "
                        + "where po_no = @po_no";

            SqlParameter[] parameters = {
                new SqlParameter("po_no",po_no),
                new SqlParameter("vendor_key",vendor_key),
                new SqlParameter("update_by",update_by),
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


        //删除po单头表中单条数据
        public Boolean deletePoHeader(string po_no)
        {
            string sql = //删除PO单头
                        "delete from wms_po_header where po_no = @po_no";
            SqlParameter[] parameters = {
                new SqlParameter("po_no",po_no),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        //查询出PO单头整张表中的信息
        public DataSet getPO_Header()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT * FROM wms_po_header";

            SqlParameter[] parameters = null;

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count >= 1)
                return ds;
            else
                return null;
        }



        //根据Item_name 返回item_id 
        public int getItem_idByItem_name(string item_name)
        {
            string sql = "select item_id from wms_pn where item_name=@item_name  ";


            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count >= 1)   //查询操作成功
            {
                int item_id = int.Parse(ds.Tables[0].Rows[0]["item_id"].ToString());
                return item_id;
            }
            else
                return -1;
        }

        //根据PO单号返回对应单头下单身的所有line_num
        public DataSet getLine_numByPo_no(string po_no)
        {
            string sql = "select line_num from wms_po_line where po_header_id in(select po_header_id from wms_po_header where po_no=@po_no)  ";

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("po_no", po_no)
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


        //向po单身表中插入部分数据
        public Boolean insertPO_line(string po_no,int item_id, int request_qty,string cancel_flag)
        {
            POLineDC polinedc = new POLineDC();
            string sql;
            if(getLine_numByPo_no(po_no)==null)
                 sql = "insert into wms_po_line "
                       + "(po_header_id,line_num,item_id,request_qty,cancel_flag)values "
                       + "((select po_header_id from wms_po_header where po_no=@po_no),1,@item_id,@request_qty,@cancel_flag) ";
            else
                 sql = "insert into wms_po_line "
                       + "(po_header_id,line_num,item_id,request_qty,cancel_flag)values "
                       + "((select po_header_id from wms_po_header where po_no=@po_no),(select TOP 1 line_num from wms_po_line where po_header_id in(select po_header_id from wms_po_header where po_no=@po_no) order by line_num DESC)+1,@item_id,@request_qty,@cancel_flag) ";

            SqlParameter[] parameters = {
                new SqlParameter("po_no",po_no),
                new SqlParameter("item_id",item_id),
                new SqlParameter("request_qty",request_qty),
                new SqlParameter("cancel_flag",cancel_flag),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        //通过po_no查询PO单身中所有数据
        public DataSet getPOLineByPo_no(string po_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT wms_po_line.*,po_no=@po_no,wms_pn.item_name from wms_po_line left join wms_pn on wms_pn.item_id=wms_po_line.item_id left join wms_po_header on wms_po_header.po_header_id=wms_po_line.po_header_id where wms_po_header.po_no =@po_no";

            SqlParameter[] parameters = {
                    new SqlParameter("po_no", po_no),
                };
               
            DB.connect();
            DataSet ds = DB.select(sql, parameters);


            if (ds.Tables[0].Rows.Count>=1)
                return ds;
            else
                return null;
        }


        //查询出PO_line整张表中的信息,包括对应的PO单头表中的PO单号Po_no
        public DataSet getPOLineandPo_no()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT wms_po_header.po_no,wms_pn.item_name,wms_po_line.*  FROM wms_po_line inner join wms_po_header on wms_po_line.po_header_id=wms_po_header.po_header_id inner join wms_pn on wms_pn.item_id=wms_po_line.item_id ";

            SqlParameter[] parameters = null;

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)
                return ds;
            else
                return null;
        }


         // 更新po单身表中的部分数据
        public Boolean updatePO_line(string po_no, int item_id, int request_qty, string cancel_flag,int line_num)
        {
            string sql = "update wms_po_line "
                        + "set item_id = @item_id,request_qty = @request_qty,cancel_flag=@cancel_flag "
                        + "where po_header_id = (select po_header_id from wms_po_header where po_no=@po_no) and line_num = @line_num";


            SqlParameter[] parameters = {
                new SqlParameter("po_no",po_no),
                new SqlParameter("item_id",item_id),
                new SqlParameter("request_qty",request_qty),
                new SqlParameter("cancel_flag",cancel_flag),
                new SqlParameter("line_num",line_num)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }



        //根据PO单身id删除PO单身中数据
        public Boolean deletePO_lineByPo_line_Id(int po_line_id)
        {
            string sql = "delete from wms_po_line "
                        + "where po_line_id = @po_line_id";

            SqlParameter[] parameters = {
                new SqlParameter("po_line_id",po_line_id),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        //通过po_no判断暂收表中是否有对应数据
        public Boolean getReceive_mtlByPo_no(string po_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_receive_mtl where po_no = @po_no";

            SqlParameter[] parameters = {
                    new SqlParameter("po_no", po_no),
                };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);


            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            else
                return false;
        }

//--------------------------------------------以上部分为PO设定涉及到的方法-------------

        /**"如果一个PO单号数量已经收料完成了，就不用再显示出来了"
        *   PO暂收中下拉框中 只返回还需要收料的PO单号
        *   (这里在查找PO单号时有两种情况可以显示：
         *       1还未开始收料的PO单号即在暂收表未有对应的数据:
         *          1.1 暂收表中根本无此单头的数据
         *          1.2 暂收表中有此单头的数据，无其某个具体单身的数据
         *       2该PO单号对应的暂收表数据中料未收完.
         *       
         *  sql语句4个条件（PO单身中cancel_flag必须为‘Y’&& （1.1||1.2||2））
        **/
        public DataSet getAllValidPo_no()
        {
            string sql = "SELECT distinct h.po_no FROM wms_po_line l left join wms_po_header h on l.po_header_id = h.po_header_id where  l.cancel_flag='Y' and (l.po_line_id not in (select po_line_id from wms_receive_mtl) or l.po_header_id not in (select po_header_id from wms_receive_mtl) or (SELECT SUM(rcv_qty) FROM wms_receive_mtl r where r.po_header_id = l.po_header_id and r.po_line_id = l.po_line_id) < (SELECT request_qty FROM wms_po_line  WHERE l.po_line_id = po_line_id and l.po_header_id = po_header_id)) ";

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


        //查询出整张表中的信息，通过line_num  
        public DataSet getPOLineByLine_num(string po_no, int line_num)
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT * FROM wms_po_line where line_num=@line_num and po_header_id=(select po_header_id from wms_po_header where po_no=@po_no)";

            SqlParameter[] parameters = {
                    new SqlParameter("line_num", line_num),
                    new SqlParameter("po_no", po_no)
                };

            DB.connect();


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

        //根据item_id 返回Item_name 
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


         //根据Po_header_id和line_id返回rcv_qty 总暂收数量(存在同一PO单头+PO单身多次收料的情况)
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


       /* PO暂收对应的操作 
        * 根據料號+datecod+po+數量收料，做賬時將數據插入到暫收表、交易表中
        * */
        public Boolean poSuspense(string item_name, int item_id, string datecode, int rcv_qty, string po_no, string vendor_code, int po_header_id, int po_line_id, DateTime transaction_time, string create_user)
        {
            string sql =
                //+ "--将数据插入到暂收表中"
                         "insert into wms_receive_mtl "
                        + "(accepted_qty,deliver_qty,RETURN_qty,item_name,item_id,datecode,rcv_qty,po_no,vendor_code,po_header_id,po_line_id)values "
                        + "('0','0','0',@item_name,@item_id,@datecode,@rcv_qty,@po_no,@vendor_code,@po_header_id,@po_line_id);"
                //+ "--将数据插入到交易表"
                        + "INSERT INTO wms_transaction_operation (transaction_qty,transaction_type,transaction_time,create_user,po_no,item_name) VALUES (@rcv_qty,'PoSuspense',@transaction_time,@create_user,@po_no,@item_name)";


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



        /*PO暂收,PO入库页面需要的查询方法
        * 通过receipt_no暂收单号，item_name料号，po_no采购单号，获取ModelReceive_mtl对象的列表集合（vendor_code自动转换为vendor_name）
         * */
        public DataSet getReceive_mtlBySome(string receipt_no, string item_name, string po_no)
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
                sqlAll = "SELECT *,vendor_name,line_num FROM wms_receive_mtl join wms_customers on wms_customers.vendor_key = wms_receive_mtl.vendor_code join wms_po_line on wms_po_line.po_line_id = wms_receive_mtl.po_line_id ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT *,vendor_name,line_num FROM wms_receive_mtl join wms_customers on wms_customers.vendor_key = wms_receive_mtl.vendor_code join wms_po_line on wms_po_line.po_line_id = wms_receive_mtl.po_line_id WHERE 1=1 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("receipt_no", receipt_no),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("po_no", po_no),
                };

            DataSet ds = DB.select(sqlAll, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }


        /*根据PO单号返回对应单头下单身的所有line_num (注意！该单身的cancel_flag必须为‘Y’)
         *  已暂收的单身但是未暂收满的可以显示（该单身的cancel_flag必须为‘Y’)）
         *  未暂收的单身可以显示（该单身的cancel_flag必须为‘Y’)）
         * */
        public DataSet getValidLine_numByPo_no(string po_no)
        {
            string sql = "select line_num from wms_po_line where po_header_id =(select po_header_id from wms_po_header where po_no=@po_no) and cancel_flag = 'Y' and(((SELECT SUM(rcv_qty) FROM wms_receive_mtl r where r.po_header_id = (select po_header_id from wms_po_header where po_no =@po_no ) and r.po_line_id = wms_po_line.po_line_id) < (SELECT request_qty FROM wms_po_line l WHERE l.po_line_id = wms_po_line.po_line_id and l.po_header_id =(select po_header_id from wms_po_header where po_no = @po_no ))) or  (wms_po_line.po_line_id not in (select po_line_id from wms_receive_mtl where po_no=@po_no)))  ";

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("po_no", po_no)
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


//-----------------------------------------------------------------以上部分为PO暂收涉及到的方法-------------------------------

        //如果已经做完允收的暂收单号，就不要显示在这边
        public List<string> getAllValidReceipt_no()
        {

            //string sql = "select receipt_no from wms_receive_mtl r where ((select rcv_qty from wms_receive_mtl where receipt_no = r.receipt_no) > ((select accepted_qty from wms_receive_mtl where receipt_no = r.receipt_no) + (select return_qty from wms_receive_mtl where receipt_no = r.receipt_no))) ";
            string sql = "select receipt_no from wms_receive_mtl r where r.accepted_qty= 0 and r.return_qty= 0";

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

        //用暂收单号查询暂收表里的数据
        public DataSet searchReceive_mtlByReceipt_no(string receipt_no)
        {
            string sql = "select * from wms_receive_mtl where RECEIPT_NO =@receipt_no";

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                
                return ds;
            }
            else
            {
                return null;
            }
        }


        //根据receipt_no 暂收单号返回rcv_qty 暂收数量
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

         /* 暂收表修改操作
         * 通过暂收单号（receipt_no）查询到需要修改的数据，
         * 再根据传过来的参数修改允收量（accepted_qty）和退回量（return_qty）
        */
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

//-----------------------------------------------以上为PO允收页面----------------------------------------------------------


        //PO入库页面查询完成未入库的暂收单信息  (在查询暂收表数据时，可收量大于0才有入库的必要，才需要显示出来)
        public DataSet getReceive_mtlByNotInstorage(string receipt_no, string item_name, string po_no)
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
                sqlAll = "SELECT * FROM wms_receive_mtl where status='N' and accepted_qty > 0 ";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * FROM wms_receive_mtl WHERE status='N' and accepted_qty > 0 " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("receipt_no", receipt_no),
                    new SqlParameter("item_name", item_name),
                    new SqlParameter("po_no", po_no),
                };

            DataSet ds = DB.select(sqlAll, parameters);


            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                return ds;
            }
            else
            {
                return null;
            }
        }



        //获取料架表中的所有料架
        public List<string> getAllFrame_name()
        {

            string sql = "select frame_name from WMS_frame where enabled = 'Y'";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["frame_name"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }


        

        //根据料架名找到对应的库别名（料架-->区域-->库别  ，注意：料架表中的料架名是唯一的）
        public string getSubinventory_nameByFrame_name(string frame_name)
        {
            string sql = "select subinventory_name from wms_subinventory where subinventory_key = (select subinventory from WMS_region where region_key = (select region_key from WMS_frame where frame_name=@frame_name))";


            SqlParameter[] parameters = {
                new SqlParameter("frame_name", frame_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)   //查询操作成功
            {
                string subinventory_name = ds.Tables[0].Rows[0]["subinventory_name"].ToString();
                return subinventory_name;
            }
            else
                return null;
        }

        

        //根据料架名找到对应的id
        public int getFrame_keyByFrame_name(string frame_name)
        {
            string sql = "select frame_key from WMS_frame where frame_name=@frame_name";


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

         //根据库别名找到对应的id
        public int getSubinventory_keyBySubinventory_name(string subinventory_name)
        {
            string sql = "select subinventory_key from wms_subinventory where subinventory_name=@subinventory_name";


            SqlParameter[] parameters = {
                new SqlParameter("subinventory_name", subinventory_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)   //查询操作成功
            {
                int subinventory_key = int.Parse(ds.Tables[0].Rows[0]["subinventory_key"].ToString());
                return subinventory_key;
            }
            else
                return -1;
        }


        //库存总表中根据 料名+库别名 搜索对应的数据
        public DataSet getItems_onhand_qty_detailByITEM_NAMEandSubinventory(string item_name, string subinventory)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from WMS_ITEMS_ONHAND_QTY_DETAIL where ITEM_NAME = @item_name and subinventory=@subinventory";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
                new SqlParameter("subinventory", subinventory)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);


            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {

                return ds;
            }
            else
            {
                return null;
            }
        }


        //通过料名、料架、DATECODE 查询库存明细表信息      
        public DataSet getItems_onhand_qty_detailByITEM_NAMEandSubinventoryandFrame_key(string item_name, string datecode, int frame_key)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_material_io where item_id = (select item_id from wms_pn where item_name = @item_name) and datecode=@datecode and frame_key=@frame_key";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
                new SqlParameter("datecode", datecode),
                new SqlParameter("frame_key", frame_key)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {

                return ds;
            }
            else
            {
                return null;
            }
        }



        /* PO入库对应的操作 
        * 修改暫收表入庫數量(DELIVER_QTY)，修改庫存總表庫存量，庫存明細表里對應的料號+datecode數量，在交易表中插入入庫信息
        */
        public Boolean inStorage(string receipt_no, string item_name, int deliver_qty, string datecode, string frame_name, string issued_sub_key, DateTime transaction_time, string status, bool flag, bool flag1, string create_user)
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
                sqlThird = "INSERT INTO wms_material_io (item_id,onhand_qty,frame_key,datecode,create_time,last_reinspect_status,last_reinspect_time)VALUES((select item_id from wms_pn where item_name = @item_name),@deliver_qty,(select frame_key from wms_frame where frame_name = @frame_name),@datecode,@transaction_time,'PASS',@transaction_time);";
            }
            else
            {
                //+ "--修改庫存明细表庫存量"
                sqlThird = "UPDATE wms_material_io SET onhand_qty=onhand_qty + @deliver_qty, update_time=@transaction_time,last_reinspect_status='PASS',last_reinspect_time = @transaction_time WHERE  item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name) and frame_key=(select frame_key from wms_frame where frame_name = @frame_name);";
            }
            //+ "--将数据插入到交易表"
            sqlTail = "INSERT INTO wms_transaction_operation (transaction_qty,transaction_type,transaction_time,item_name,po_no,create_time,create_user) VALUES (@deliver_qty,'PoInStorage',@transaction_time,@item_name,(select po_no from wms_receive_mtl where receipt_no = @receipt_no),@transaction_time,@create_user )";

            sql = sqlHead + sqlSecond + sqlThird + sqlTail;

            SqlParameter[] parameters = {
                new SqlParameter("receipt_no", receipt_no),
                new SqlParameter("item_name", item_name),
                new SqlParameter("frame_name", frame_name),
                new SqlParameter("deliver_qty", deliver_qty),
                new SqlParameter("datecode", datecode),
                new SqlParameter("issued_sub_key", issued_sub_key),
                new SqlParameter("transaction_time", transaction_time),
                new SqlParameter("create_user", create_user)
                };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.tran(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        public DataSet getPOLineByPo_noAndItem_name(string po_no,string item_name)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_po_line where po_header_id = (select po_header_id from wms_po_header where po_no=@po_no) and item_id = (select item_id from wms_pn where item_name = @item_name)";

            SqlParameter[] parameters = {
                                new SqlParameter("po_no", po_no),
                                new SqlParameter("item_name", item_name)

            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count > 0)   //如果存在一行及以上数据
            {
                return ds;
            }
            else
            {
                return null;
            }
        }


    }
}


