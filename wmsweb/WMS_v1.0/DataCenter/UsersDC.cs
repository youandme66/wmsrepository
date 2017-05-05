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
    public class UsersDC
    {
        /// <summary>
        /// 登录判断
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool login(string name, string pass)
        {
            ModelUsers user = searchUsersByName(name);

            if (user != null && String.IsNullOrEmpty(user.Password) && String.IsNullOrEmpty(pass))
            {
                return true;
            }

            if (user != null && !String.IsNullOrEmpty(user.Password) && user.Password.Equals(pass))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oldpass"></param>
        /// <param name="newpass"></param>
        /// <returns></returns>
        public bool resetPassWord(string name, string oldpass, string newpass)
        {
            string sql = "update wms_users set PASSWORD = @Password, update_time = GETDATE() where USER_NAME = @Username";
            ModelUsers user = searchUsersByName(name);

            if (user == null)
            {
                return false;
            }

            SqlParameter[] parameters = { 
                new SqlParameter("Password", newpass),
                new SqlParameter("Username", name)            
            };

            DB.connect();
            if (String.IsNullOrEmpty(user.Password))
            {
                DB.update(sql, parameters);
                return true;
            }
            else if (user.Password.Equals(oldpass))
            {
                DB.update(sql, parameters);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 通过用户名查找ModelUsers
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ModelUsers searchUsersByName(string name)
        {
            string sql = "select * from wms_users where USER_NAME = @username";

            SqlParameter[] parameters = { 
                new SqlParameter("username", name)                            
            };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //只是数据库的一行数据，如果需要多行，用循环和列表实现
                return toModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 通过id查找用户名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string searchUserNameByID(int id)
        {
            string sql = "select user_name from wms_users where user_id = @id";

            SqlParameter[] parameters = { 
                new SqlParameter("id", id)                            
            };
            
            DB.connect();
            DataSet ds = DB.select(sql, parameters);

            return ds.Tables[0].Rows[0]["User_Name"].ToString();
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dept_no"></param>
        /// <returns></returns>
        public string register(string name, string dept_no, string pass)
        {
            if (searchUsersByName(name) != null)
            {
                return "用户名已存在！";
            }
            else
            {
                string sql = "insert into wms_users(USER_NAME,Dept_no,Password,enabled) values(@name,@dept_no,@pass,'Y')";
                SqlParameter[] parameters ={
                    new SqlParameter("name", name),
                    new SqlParameter("dept_no", dept_no),
                    new SqlParameter("pass", pass)
                };

                DB.connect();
                int flag = DB.insert(sql, parameters);

                if (flag == 1)
                {
                    return "注册成功！";
                }
                else
                {
                    return "注册失败！";
                }
            }
        }


        /**作者：周雅雯 时间：2016/8/29
         * 用户设定页面需要的插入方法
         **/
        public Boolean insertUsers(string user_name, string description, string enabled, string create_by, string dept_no)
        {

            string sql = "insert into wms_users "
                       + "(user_name,description,enabled,create_by,dept_no)values "
                       + "(@user_name,@description,@enabled,@create_by,@dept_no) ";

            SqlParameter[] parameters = {
                new SqlParameter("user_name",user_name),
                new SqlParameter("description",description),
                new SqlParameter("enabled",enabled),
                new SqlParameter("create_by",create_by),
                new SqlParameter("dept_no",dept_no),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.insert(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /**作者：周雅雯 时间：2016/8/10
         * 用户设定页面需要的删除方法
         **/
        public Boolean deleteUsers(int user_id)
        {
            string sql = "delete from wms_users "
                        + "where user_id = @user_id";

            SqlParameter[] parameters = {
                new SqlParameter("user_id",user_id),
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.delete(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /**作者：周雅雯 时间：2016/8/29
         * 用户设定页面需要的更新方法
         **/
        public Boolean updateUsers(int user_id, string user_name, string description, string enabled, string update_by, string dept_no, DateTime update_time)
        {
            string sql = "update wms_users "
                        + "set user_name=@user_name, description = @description,enabled = @enabled,update_by=@update_by,dept_no = @dept_no,update_time=@update_time "
                        + "where user_id = @user_id";

            SqlParameter[] parameters = {
                new SqlParameter("user_name",user_name),
                new SqlParameter("description",description),
                new SqlParameter("enabled",enabled),
                new SqlParameter("update_by",update_by),
                new SqlParameter("dept_no",dept_no),
                new SqlParameter("update_time",update_time),
                new SqlParameter("user_id",user_id)
            };

            DB.connect();

            //返回受影响行数InfluenceNum
            int InfluenceNum = DB.update(sql, parameters);

            if (InfluenceNum > 0)
                return true;
            else
                return false;
        }

        /**作者：周雅雯 时间：2016/8/13
        * 用户设定页面需要的查询方法
        **/
        public DataSet getUsersBySome(string user_id,string user_name, string description, string enabled, string dept_no)
        {
            //完整查询内容
            string sqlAll = "";
            //* from wms_pn 后的内容，即查询条件
            string sqlTail = "";


            //当user_name有值时
            if (string.IsNullOrWhiteSpace(user_name) == false )
            {
                sqlTail += "AND user_name LIKE '%'+@user_name+'%' ";
            }
            //当description有值时
            if (string.IsNullOrWhiteSpace(description) == false )
            {
                sqlTail += "AND description LIKE '%'+@description+'%' ";
            }
            //当enabled有值时
            if (string.IsNullOrWhiteSpace(enabled) == false )
            {
                sqlTail += "AND enabled LIKE '%'+@enabled+'%' ";
            }
            //当dept_no有值时
            if (string.IsNullOrWhiteSpace(dept_no) == false )
            {
                sqlTail += "AND dept_no LIKE '%'+@dept_no+'%' ";
            }
            //不包含条件查询时
            if (sqlTail.Length <= 0)
            {
                sqlAll = "SELECT * FROM wms_users where user_name<>@user_id";
            }
            //包含条件查询时
            else
            {
                sqlAll = "SELECT * from wms_users where user_name<>@user_id " + sqlTail;
            }

            DB.connect();

            SqlParameter[] parameters = {
                new SqlParameter("user_id",user_id),
                new SqlParameter("user_name",user_name),
                new SqlParameter("description",description),
                new SqlParameter("enabled",enabled),
                new SqlParameter("dept_no",dept_no),
            };

            DataSet ds = DB.select(sqlAll, parameters);

            return ds;
        }
        public DataSet getUser(string user_name)
        {
            string sql = "select * from wms_users where user_name=@user_name;";
            DB.connect();
            SqlParameter[] parameters ={
                        new SqlParameter("user_name",user_name),
                                      };
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }

        /**作者周雅雯，时间：2016/9/2
        * wms_user表中的所有where enabled=‘Y’（可用的）的user_name数据
        * **/
        public List<string> getAllEnabledUser_name()
        {

            string sql = "select user_name from wms_users where enabled='Y'  ";

            DB.connect();
            DataSet ds = DB.select(sql, null);

            List<string> modellist = new List<string>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modellist.Add(dr["user_name"].ToString());
                }
                return modellist;
            }
            else
            {
                return null;
            }
        }

        private ModelUsers toModel(DataRow dr)
        {
            ModelUsers model = new ModelUsers();

            //通过循环为ModelPO_Header赋值，其中为数据值为空时，DateTime类型的空值为：0001/1/1 0:00:00    int类型得空值为： 0，其余的还没试验
            foreach (PropertyInfo propertyInfo in typeof(ModelUsers).GetProperties())
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