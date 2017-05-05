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
    public class IssueHeaderDC//领料主表（IssueHeader）对应的DataCenter
    {

        public string getWareHouseByInvoice_no(string invoice_no) {

            string sql = "select issued_sub_key from wms_mtl_issue_header where invoice_no = @invoice_no";

            SqlParameter[] parameter = { new SqlParameter("invoice_no",invoice_no)};

            DB.connect();

            DataSet ds = DB.select(sql,parameter);

            return ds.Tables[0].Rows[0]["issued_sub_key"].ToString();

        }


        /// <summary>
        /// 通过工单号获得需求量（工单领料）
        /// </summary>
        /// <param name="wo_no"></param>
        /// <returns></returns>
        public string getTarget(string wo_no) {

            string sql = "select target_qty from wms_wo where wo_no = @wo_no";

            SqlParameter[] parameter = { new SqlParameter("wo_no",wo_no) };

            DB.connect();

            DataSet ds = DB.select(sql,parameter);

            return ds.Tables[0].Rows[0]["target_qty"].ToString();

        }


        /// <summary>
        /// 获得料架
        /// </summary>
        /// <returns></returns>
        public List<string> getFrame_key() {


            string sql = "select * from WMS_frame where enabled = 'Y'";        


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


        /// <summary>
        /// 获取工单号
        /// </summary>
        /// <returns></returns>
        public List<string> getWo_no() {

            string sql = "select * from wms_wo";

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

        public List<string> getInvoice_no()
        {

            string sql = "select invoice_no from wms_mtl_issue_header";

            DB.connect();

            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["invoice_no"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// 获取制程号
        /// </summary>
        /// <returns></returns>
        public List<string> getOperations() {

            string sql = "select * from wms_wip_operations";


            DB.connect();

            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["route"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        
        }


        /// <summary>
        /// 通过领料单号得到ID
        /// </summary>
        /// <param name="invoice_no"></param>
        /// <returns></returns>
        public string getIDByInvoice_no(string invoice_no) {

            string sql = "select issue_header_id from wms_mtl_issue_header where invoice_no = @invoice_no";

            SqlParameter[] parameter = { new SqlParameter("invoice_no",invoice_no) };

            DB.connect();

            DataSet ds = DB.select(sql,parameter);

            return ds.Tables[0].Rows[0]["issue_header_id"].ToString();

        }

        //通过领料单号INVOICE_NO，查询领料主表信息
        public List<ModelMtl_issue_header> getIssueHeaderByINVOICE_NO(string invoice_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_mtl_issue_header where INVOICE_NO = @invoice_no";

            SqlParameter[] parameters = {
                new SqlParameter("INVOICE_NO", invoice_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelMtl_issue_header> modelMtl_issue_header_list = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                modelMtl_issue_header_list = new List<ModelMtl_issue_header>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelMtl_issue_header_list.Add(toModel(datarow));
                }
            }
            return modelMtl_issue_header_list;
        }

        //通过领料IDIssue_header_id，查询领料主表信息
        public List<ModelMtl_issue_header> getIssueHeaderByIssue_header_id(int Issue_header_id)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_mtl_issue_header where Issue_header_id = @Issue_header_id";

            SqlParameter[] parameters = {
                new SqlParameter("Issue_header_id", Issue_header_id)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelMtl_issue_header> modelMtl_issue_header_list = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                modelMtl_issue_header_list = new List<ModelMtl_issue_header>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelMtl_issue_header_list.Add(toModel(datarow));
                }
            }
            return modelMtl_issue_header_list;
        }

        //通过模糊查询领料单号INVOICE_NO，获取领料主表信息
        public List<ModelMtl_issue_header> getIssueHeaderByLikeINVOICE_NO(string invoice_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_mtl_issue_header where INVOICE_NO LIKE '%'+@invoice_no+'%'";

            SqlParameter[] parameters = {
                new SqlParameter("INVOICE_NO", invoice_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelMtl_issue_header> modelMtl_issue_header_list = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                modelMtl_issue_header_list = new List<ModelMtl_issue_header>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelMtl_issue_header_list.Add(toModel(datarow));
                }
            }
            return modelMtl_issue_header_list;
        }

        //通过模糊查询领料IDIssue_header_id，查询领料主表信息
        public List<ModelMtl_issue_header> getIssueHeaderByLikeIssue_header_id(int Issue_header_id)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_mtl_issue_header where Issue_header_id LIKE @Issue_header_id";

            SqlParameter[] parameters = {
                new SqlParameter("Issue_header_id", Issue_header_id)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelMtl_issue_header> modelMtl_issue_header_list = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                modelMtl_issue_header_list = new List<ModelMtl_issue_header>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelMtl_issue_header_list.Add(toModel(datarow));
                }
            }
            return modelMtl_issue_header_list;
        }

        //通过领料单号 invoice_no 查询出领料主表和领料从表的信息
        public DataSet getIssueByinvoice_no(string invoice_no)
        {
            //通过SQL语句，获取DateSet
            string sql = "select wms_mtl_issue_header.invoice_no,wms_mtl_issue_header.issue_type,wms_mtl_issue_header.wo_no,wms_mtl_issue_header.issued_sub_key,wms_issue_line.locator,wms_issue_line.status,wms_issue_line.frame_key,wms_mtl_issue_header.create_time,wms_mtl_issue_header.operation_seq_num,wms_mtl_issue_header.issue_man,wms_issue_line.item_name,wms_issue_line.required_qty,wms_issue_line.simulated_qty,wms_issue_line.issued_qty,wms_issue_line.issue_line_id " 
                            +"from wms_mtl_issue_header ,wms_issue_line "
                            + "where wms_mtl_issue_header.invoice_no=@invoice_no AND wms_mtl_issue_header.issue_header_id=wms_issue_line.issue_header_id";
           
            SqlParameter[] parameters = {
                new SqlParameter("invoice_no", invoice_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            return ds;
        }

        public string getIssueStatus(int issue_line_id)
        {
            //通过SQL语句，获取DateSet
            string sql = "select wms_issue_line.status from wms_issue_line where wms_issue_line.issue_line_id=@issue_line_id";

            SqlParameter[] parameters = {
                new SqlParameter("issue_line_id", issue_line_id)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            return ds.Tables[0].Rows[0]["status"].ToString();
        }


        //查询出整张表中的信息
        public List<ModelMtl_issue_header> getIssueHeader()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT * FROM wms_mtl_issue_header";

            SqlParameter[] parameters = null;

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelMtl_issue_header> modelMtl_issue_header_list = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                modelMtl_issue_header_list = new List<ModelMtl_issue_header>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelMtl_issue_header_list.Add(toModel(datarow));
                }
            }
            return modelMtl_issue_header_list;
        }

        //通过自增长的createtime，获取最新一条领料主表信息
        public ModelMtl_issue_header getTheNewestIssueHeaderByCreatetime()
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT TOP 1.*  FROM wms_mtl_issue_header ORDER BY create_time DESC";

            SqlParameter[] parameters = null;
              

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return toModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        

        //向领料主表中插入数据
        public Boolean insertIssueHeader(string invoice_no, string issue_type, string wo_no, string issued_sub_key)
        {

            string sql = "insert into wms_mtl_issue_header "
                       + "(INVOICE_NO,WO_NO,ISSUE_TYPE,ISSUED_SUB_KEY)values "
                       + "(@invoice_no,@wo_no,@issue_type,@issued_sub_key) ";

            SqlParameter[] parameters = {
                new SqlParameter("INVOICE_NO",invoice_no),
                new SqlParameter("ISSUE_TYPE",issue_type),
                new SqlParameter("WO_NO",wo_no),
                new SqlParameter("ISSUED_SUB_KEY",issued_sub_key)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //向领料主表中插入数据
        public Boolean insertIssueHeader(string invoice_no, string wo_no, string issued_sub_key, DateTime create_time, DateTime update_time, string operation_seq_num, string issue_type, string issue_man, string customer_key)
        {

            string wo_key = getWo_key(wo_no);

            string sql = "insert into wms_mtl_issue_header "
                       + "(invoice_no,wo_no,issued_sub_key,create_time,update_time,operation_seq_num,issue_type,issue_man,customer_key,wo_key)values "
                       + "(@invoice_no,@wo_no,@issued_sub_key,@create_time,@update_time,@operation_seq_num,@issue_type,@issue_man,@customer_key,@wo_key) ";

            SqlParameter[] parameters = {
                new SqlParameter("INVOICE_NO",invoice_no),
                new SqlParameter("WO_NO",wo_no),
                new SqlParameter("ISSUED_SUB_KEY",issued_sub_key),
                new SqlParameter("CREATE_TIME",create_time),
                new SqlParameter("UPDATE_TIME",update_time),
                new SqlParameter("OPERATION_SEQ_NUM",operation_seq_num),
                new SqlParameter("ISSUE_TYPE",issue_type),
                new SqlParameter("ISSUE_MAN",issue_man),
                new SqlParameter("CUSTOMER_KEY",customer_key),
                new SqlParameter("WO_KEY",wo_key)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        public string getWo_key(string wo_no) {

            string sql = "select wo_key from wms_wo where wo_no = @wo_no";

            SqlParameter[] parameter = { new SqlParameter("wo_no",wo_no)};

            DB.connect();

            DataSet ds = DB.select(sql,parameter);

            return ds.Tables[0].Rows[0]["wo_key"].ToString();

        }





        //更新领料主表中的部分数据
        public Boolean updateIssueHeader(string invoice_no, string issue_type, string wo_no, string issued_sub_key)
        {
            string sql = "update wms_mtl_issue_header "
                        + "set INVOICE_NO = @invoice_no,ISSUE_TYPE = @issue_type,WO_NO = @wo_no,ISSUED_SUB_KEY=@issued_sub_key "
                        + "where INVOICE_NO = @invoice_no";

            SqlParameter[] parameters = {
                new SqlParameter("INVOICE_NO",invoice_no),
                new SqlParameter("ISSUE_TYPE",issue_type),
                new SqlParameter("WO_NO",wo_no),
                new SqlParameter("ISSUED_SUB_KEY",issued_sub_key),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //删除领料主表中的单条数据
        public Boolean deleteIssueHeader(string invoice_no)
        {
            string sql = "delete from wms_mtl_issue_header "
                        + "where INVOICE_NO = @invoice_no";

            SqlParameter[] parameters = {
                new SqlParameter("INVOICE_NO",invoice_no),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /**作者：周雅雯 修改时间：2016/8/9
        * 领料单页面对应的Debit（扣账）操作
        * 扣除庫存总表的庫存，扣除庫存明細表中对应仓库的庫存量，将信息插入到交易表中
        */
        public Boolean DebitAction(string item_name, int issued_qty, string frame_key, string issued_sub_key, DateTime transaction_time, int issue_line_id)
        {
            string sql =
                //Debit扣账
                //+ "--减少庫存总表的庫存"
                         "UPDATE wms_items_onhand_qty_detail SET onhand_quantiy=onhand_quantiy - @issued_qty  WHERE subinventory=@issued_sub_key AND item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name);"

                        //+ "--减少庫存明細表中对应仓库的庫存量"
                        + "UPDATE wms_material_io SET onhand_qty=onhand_qty - @issued_qty WHERE subinventory=@issued_sub_key AND item_id = (SELECT item_id FROM wms_pn WHERE item_name=@item_name) AND frame_key=(select frame_key from wms_frame where frame_name=@frame_key);"

                        //+ "--将数据插入到交易表"
                        + "INSERT INTO wms_transaction_operation (transaction_qty,transaction_type,transaction_time) VALUES (@issued_qty,'Issue_Debit ',@transaction_time)"

                       + "UPDATE wms_issue_line SET status='Y'  WHERE issue_line_id=@issue_line_id";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name),
                new SqlParameter("issued_qty", issued_qty),
                new SqlParameter("issued_sub_key", issued_sub_key),
                new SqlParameter("frame_key", frame_key),
                new SqlParameter("transaction_time", transaction_time),
                new SqlParameter("issue_line_id", issue_line_id),
                };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.tran(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //领料单页面对应的Debit（扣账）操作
        //public Boolean DebitAction(DataTable dataTable,string warehouse)
        //{
        //    //对dataTable中的每行数据进行操作
        //    foreach (DataRow datarow in dataTable.Rows)
        //    {
        //        IssueHeaderDC issueHeaderDC = new IssueHeaderDC();
        //        Boolean flag = false;

        //        //领料单页面对应的Debit扣账）操作.此处ssued_sub_key 库别K值默认为下标为7
        //        flag = issueHeaderDC.DebitAction(datarow["ITEM_NAME"].ToString(), int.Parse(datarow["ISSUED_QTY"].ToString()), datarow["FRAME_KEY"].ToString(),warehouse);
        //        //操作失败时，则返回false
        //        if (flag == false)
        //            return false;
        //    }
        //    return true;
        //}




        // 传入DataRow,将其转换为ModelMtl_issue_header
        private ModelMtl_issue_header toModel(DataRow dr)
        {
            ModelMtl_issue_header model = new ModelMtl_issue_header();

            //通过循环为ModelMtl_issue_header赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelMtl_issue_header).GetProperties())
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