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
    //
    public class POLineDC
    {
        //查询出料号表的所有料号名
        public List<string> getitemName()
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

        //查询出PO_line整张表中的信息,包括对应的PO单头表中的PO单号Po_no
        public DataSet getPOLine()
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

        //查询出整张表中的信息，通过line_num  
        public List<ModelPO_line> getPOLineByLine_num(string po_no,int line_num)
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT * FROM wms_po_line where line_num=@line_num and po_header_id=(select po_header_id from wms_po_header where po_no=@po_no)";

            SqlParameter[] parameters = {
                    new SqlParameter("line_num", line_num),
                    new SqlParameter("po_no", po_no)
                };

            DB.connect();


            DataSet ds = DB.select(sql, parameters);

           
            List<ModelPO_line> modelPOLine_list = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                modelPOLine_list = new List<ModelPO_line>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    modelPOLine_list.Add(toModel(datarow));
                }
            }
            return modelPOLine_list;
        }


        //查询出line_num的信息，通过po_header_id
        public List<int> getLineNumByPoHeaderId(int po_header_id)
        {

            //通过SQL语句，获取DateSet
            string sql = "SELECT line_num FROM wms_po_line where po_header_id=@po_header_id";

            SqlParameter[] parameters = {
                    new SqlParameter("po_header_id", po_header_id),
                };

            DB.connect();


            DataSet ds = DB.select(sql, parameters);


            List<int> line_num__list = new List<int>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    line_num__list.Add(int.Parse(dr["line_num"].ToString()));
                }
                return line_num__list;
            }
            else
            {
                return null;
            }




        }




        /**作者：周雅雯 时间：2016/9/3
         * 返回所有的Line_num
         **/
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

        /**作者：周雅雯 时间：2016/9/7
         * Po/Poline页面需要的查询方法,当更新一条数据成功时，调用此方法显示刚刚更新的数据
         * 通过item_id料号ID,获取ModelPO_line对象的列表集合
         **/
        public DataSet getPO_lineByItem_id(int item_id,int request_qty)
        {
            string sql = "SELECT wms_po_header.po_no,wms_pn.item_name,wms_po_line.*  FROM wms_po_line inner join wms_po_header on wms_po_line.po_header_id=wms_po_header.po_header_id inner join wms_pn on wms_pn.item_id=@item_id AND request_qty=@request_qty ";

            DB.connect();

            SqlParameter[] parameters = {
                    new SqlParameter("item_id", item_id),
                    new SqlParameter("request_qty", request_qty),
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

        /**作者周雅雯，时间：2016/8/6
         * PO/POline设定页面
         * 向po单身表中插入部分数据
         **/
        public Boolean insertPO_line(string po_no,int item_id, int request_qty,string cancel_flag)
        {
            POLineDC polinedc = new POLineDC();
            string sql;
            if(polinedc.getLine_numByPo_no(po_no)==null)
                 sql = "insert into wms_po_line "
                       + "(po_header_id,line_num,item_id,request_qty,cancel_flag,closed)values "
                       + "((select po_header_id from wms_po_header where po_no=@po_no),1,@item_id,@request_qty,@cancel_flag,'Y') ";
            else
                 sql = "insert into wms_po_line "
                       + "(po_header_id,line_num,item_id,request_qty,cancel_flag,closed)values "
                       + "((select po_header_id from wms_po_header where po_no=@po_no),(select TOP 1 line_num from wms_po_line where po_header_id in(select po_header_id from wms_po_header where po_no=@po_no) order by line_num DESC)+1,@item_id,@request_qty,@cancel_flag,'Y') ";

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
        /**作者周雅雯，时间：2016/8/6
        * PO/POline设定页面
        * 更新po单身表中的部分数据
        **/
        public Boolean updatePO_line(string po_no, int item_id, int request_qty, string cancel_flag)
        {
            string sql = "update wms_po_line "
                        + "set item_id = @item_id,request_qty = @request_qty,cancel_flag=@cancel_flag "
                        + "where po_header_id = (select po_header_id from wms_po_header where po_no=@po_no)";


            SqlParameter[] parameters = {
                new SqlParameter("po_no",po_no),
                new SqlParameter("item_id",item_id),
                new SqlParameter("request_qty",request_qty),
                new SqlParameter("cancel_flag",cancel_flag),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }
        /**作者周雅雯，时间：2016/8/6
        * PO/POline设定页面
        * 删除po单身表中单条数据
        **/
        public Boolean deletePO_line(string po_no)
        {
            string sql = "delete from wms_po_line "
                        + "where po_header_id = (select po_header_id from wms_po_header where po_no=@po_no)";

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


        /// <summary>
        /// 通过传入的PO_NO，返回ModelPO_line对象
        /// </summary>
        /// <param name="po_no"></param>
        /// <returns></returns>
        public List<ModelPO_line> getPOLineByPO_NO(string po_no, string line)
        {
            string sqlline = "select b.Po_header_id,b.Po_line_id,b.Line_num,b.Item_id,b.Request_qty,b.closed,b.Cancel_flag from wms_po_header a  inner join wms_po_line b on(a.PO_HEADER_ID = b.Po_header_id AND a.OR PO_NO = @po_no) where b.Line_num = @line";

            string sqlpo_no = "select b.Po_header_id,b.Po_line_id,b.Line_num,b.Item_id,b.Request_qty,b.closed,b.Cancel_flag from wms_po_line a  inner join wms_po_header b on(b.PO_HEADER_ID = a.Po_header_id) where b.PO_NO = @po_no";
            SqlParameter[] parameters1 = {
                new SqlParameter("po_no", po_no)
            };

            SqlParameter[] parameters2 = {
                new SqlParameter("po_no", po_no),
                new SqlParameter("line", line)
            };

            List<ModelPO_line> polist = null;

            DB.connect();
            DataSet ds = null;
            if (String.IsNullOrEmpty(line))
            {
                ds = DB.select(sqlpo_no, parameters1);
            }
            else
            {
                ds = DB.select(sqlline, parameters2);
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    polist.Add(toModel(dr));
                }
                return polist;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 传入DataRow,将其转换为PO单身对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private ModelPO_line toModel(DataRow dr)
        {
            ModelPO_line model = new ModelPO_line();

            foreach (PropertyInfo propertyInfo in typeof(ModelPO_line).GetProperties())
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

        /**
        * PO/POline设定页面
        * 通过po_no查询数据 1
        **/
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



    }
}