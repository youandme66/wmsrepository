using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using WebApplication1;
using WMS_v1._0.Model;
using System.Data.SqlClient;
using System.Data;

namespace WMS_v1._0.DataCenter  //作者：周雅雯 最后一次修改时间：2016/10/12
{
    public class IssuelineDC    //领料从表（Issueline）对应的DataCenter
    {

        public List<ModelMtl_issue_line> getAllbyIssueHeaderId(string issue_header_id)
        {

            string sql = "select * from wms_issue_line where issue_header_id = @issue_header_id";

            SqlParameter[] parameter = { new SqlParameter("issue_header_id", issue_header_id) };

            DB.connect();

            DataSet ds = DB.select(sql, parameter);

            List<ModelMtl_issue_line> ModelMtl_issue_linelist = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                ModelMtl_issue_linelist = new List<ModelMtl_issue_line>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    ModelMtl_issue_linelist.Add(toModel(datarow));
                }
            }
            return ModelMtl_issue_linelist;

        }

        //通过领料主表中的领料单号INVOICE_NO，查询领料从表信息
        public List<ModelMtl_issue_line> getIssuelineByINVOICE_NO(string invoice_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_issue_line where issue_header_id = (select issue_header_id from wms_mtl_issue_header where invoice_no=@invoice_no)";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no", invoice_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelMtl_issue_line> ModelMtl_issue_linelist = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                ModelMtl_issue_linelist = new List<ModelMtl_issue_line>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    ModelMtl_issue_linelist.Add(toModel(datarow));
                }
            }
            return ModelMtl_issue_linelist;
        }

        //通过料号item_name，查询领料从表信息
        public List<ModelMtl_issue_line> getIssuelineByitem_name(string item_name)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_issue_line where item_name = @item_name";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelMtl_issue_line> ModelMtl_issue_linelist = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                ModelMtl_issue_linelist = new List<ModelMtl_issue_line>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    ModelMtl_issue_linelist.Add(toModel(datarow));
                }
            }
            return ModelMtl_issue_linelist;
        }

        //通过模糊查询领料工单号issue_wo_no，查询领料从表信息
        public List<ModelMtl_issue_line> getIssuelineByLikeissue_wo_no(int issue_wo_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_issue_line where issue_wo_no = @issue_wo_no";

            SqlParameter[] parameters = {
                new SqlParameter("issue_wo_no", issue_wo_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelMtl_issue_line> ModelMtl_issue_linelist = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                ModelMtl_issue_linelist = new List<ModelMtl_issue_line>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    ModelMtl_issue_linelist.Add(toModel(datarow));
                }
            }
            return ModelMtl_issue_linelist;
        }

        //通过模糊查询料号item_name，查询领料从表信息
        public List<ModelMtl_issue_line> getIssuelineByLikeitem_name(string item_name)
        {

            //通过SQL语句，获取DateSet
            string sql = "select * from wms_issue_line where item_name Like @item_name";

            SqlParameter[] parameters = {
                new SqlParameter("item_name", item_name)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            List<ModelMtl_issue_line> ModelMtl_issue_linelist = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                ModelMtl_issue_linelist = new List<ModelMtl_issue_line>(ds.Tables[0].Rows.Count);

                //如果有多行时，此时应该返回几个对象。用循环和列表实现 返回一个列表的对象
                foreach (DataRow datarow in ds.Tables[0].Rows)
                {
                    ModelMtl_issue_linelist.Add(toModel(datarow));
                }
            }
            return ModelMtl_issue_linelist;
        }

        //通过领料主表中的领料单号invoice_no，查询出领料单打印所需的数据
        public DataSet getSomeByinvoice_no(string invoice_no)
        {

            //通过SQL语句，获取DateSet
            string sql = "select  wms_issue_line.item_name,subinventory_name =(select subinventory_name from wms_subinventory where subinventory_key=(select issued_sub_key from wms_issue_line where issue_header_id=(select issue_header_id from wms_mtl_issue_header where invoice_no=@invoice_no ))),locator,frame_key,datecode=(select datecode from wms_material_io where item_id=(select item_id from wms_pn where item_name=(select item_name from wms_issue_line where issue_header_id=(select issue_header_id from wms_mtl_issue_header where invoice_no=@invoice_no )))),required_qty,simulated_qty,issued_qty,remark from wms_issue_line where issue_header_id=(select issue_header_id from wms_mtl_issue_header where invoice_no=@invoice_no) ";

            SqlParameter[] parameters = {
                new SqlParameter("invoice_no", invoice_no)
            };

            DB.connect();

            DataSet ds = DB.select(sql, parameters);

            if (ds != null)
                return ds;
            else
                return null;
        }


        //向领料从表中插入部分数据 
        public Boolean insertMtl_issue_line(string item_name, int required_qty)
        {
            string sql = "insert into wms_issue_line "
                       + "(item_name,required_qty)values "
                       + "(@item_name,@required_qty)";

            SqlParameter[] parameters = {
                new SqlParameter("item_name",item_name),
                new SqlParameter("required_qty",required_qty),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        //向领料从表中插入部分数据 
        public Boolean insertMtl_issue_line(string item_name, string required_qty ,string simulated_qty ,string issued_qty ,string frame_key, string issue_header_id)
        {
            string sql = "insert into wms_issue_line "
                       + "(item_name,required_qty,simulated_qty,issued_qty,frame_key,issue_header_id)values "
                       + "(@item_name,@required_qty,@simulated_qty,@issued_qty,@frame_key,@issue_header_id)";

            SqlParameter[] parameters = {
                new SqlParameter("item_name",item_name),
                new SqlParameter("required_qty",required_qty),
                new SqlParameter("simulated_qty",simulated_qty),
                new SqlParameter("issued_qty",issued_qty),
                new SqlParameter("frame_key",frame_key),
                new SqlParameter("issue_header_id",issue_header_id)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //向领料从表中插入部分数据 
        public Boolean insertMtl_issue_line(DataTable dataTable ,string invoice_no)
        {
            //通过invoice_no获取领料主表的对象
            IssueHeaderDC issueHeaderDC = new IssueHeaderDC();
            
            string id = issueHeaderDC.getIDByInvoice_no(invoice_no);
            

          //将dataTable中的每行数据插入数据库中
          foreach (DataRow datarow in dataTable.Rows)
          {
            //将每行数据插入数据库中
            IssuelineDC issueDC = new IssuelineDC();

            bool flag = issueDC.insertMtl_issue_line(datarow["ITEM_NAME"].ToString(), datarow["REQUIRED_QTY"].ToString(), datarow["SIMULATED_QTY"].ToString(), datarow["ISSUED_QTY"].ToString(), datarow["FRAME_KEY"].ToString(),id);
            //若插入失败时，则返回false
            if (flag == false)
                   return false;
          }
            return true;
        }

        //更新料号与领料量
        public Boolean update_issue(int issue_line_id, string item_name, int issued_qty)
        {
            string sql = "update wms_issue_line "
                       + "set item_name=@item_name,issued_qty=@issued_qty "
                       + "where issue_line_id=@issue_line_id";

            SqlParameter[] parameters = {
                new SqlParameter("item_name",item_name),
                new SqlParameter("issued_qty",issued_qty),
                 new SqlParameter("issue_line_id",issue_line_id),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        public Boolean update_issue_line(string item_name, int issued_qty)
        {
            string sql = "update wms_issue_line "
                       + "set item_name=@item_name,issued_qty=@issued_qty "
                       + "where item_name=@item_name";

            SqlParameter[] parameters = {
                new SqlParameter("item_name",item_name),
                new SqlParameter("issued_qty",issued_qty),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }


        //更新领料从表中的部分数据 
        public Boolean updateMtl_issue_line(string item_name, int required_qty)
        {
            string sql = "update wms_issue_line "
                       + "set item_name=@item_name,required_qty=@required_qty "
                       + "where item_name=@item_name";

            SqlParameter[] parameters = {
                new SqlParameter("item_name",item_name),
                new SqlParameter("required_qty",required_qty),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        //删除领料从表中的单行数据 
        public Boolean deleteMtl_issue_line(string item_name)
        {
            string sql = "delete from wms_issue_line "
                       + "where item_name=@item_name";

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

        //按领料ID删除表中单行数据
        public Boolean delete_issue_line(int issue_line_id)
        {
            string sql = "delete from wms_issue_line "
                       + "where issue_line_id=@issue_line_id";

            SqlParameter[] parameters = {
                new SqlParameter("issue_line_id",issue_line_id),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        // 传入DataRow,将其转换为ModelMtl_issue_line
        private ModelMtl_issue_line toModel(DataRow dr)
        {
            ModelMtl_issue_line model = new ModelMtl_issue_line();

            //通过循环为ModelPn赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelMtl_issue_line).GetProperties())
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