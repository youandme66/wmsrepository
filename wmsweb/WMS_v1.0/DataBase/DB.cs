using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    public class DB
    {
        static SqlConnection conn;
        public static void connect()
        {
            string connectStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conn = new SqlConnection(connectStr);
        }
        public static int insert(string str, SqlParameter[] cmdParms)
        {
            //添加数据
            SqlCommand sqlcomman = new SqlCommand();
            sqlcomman.Connection = conn;
            sqlcomman.CommandText = str;
            try
            {
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                    {
                        sqlcomman.Parameters.Add(parm);
                    }
                }
                conn.Open();
                sqlcomman.ExecuteNonQuery();
                conn.Close();
                sqlcomman.Parameters.Clear();
                return 1;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static int delete(string str, SqlParameter[] cmdParms)
        {
            //删除数据
            SqlCommand sqlcomman = new SqlCommand();
            sqlcomman.Connection = conn;
            sqlcomman.CommandText = str;
            try
            {
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                    {
                        sqlcomman.Parameters.Add(parm);
                    }
                }
                conn.Open();
                int DeleteCount = sqlcomman.ExecuteNonQuery();
                conn.Close();
                sqlcomman.Parameters.Clear();
                if (DeleteCount > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static DataSet select(string str, SqlParameter[] cmdParms)
        {
            //查询数据
            SqlCommand sqlcomman = new SqlCommand();
            sqlcomman.Connection = conn;
            sqlcomman.CommandText = str;
            DataSet ds = new DataSet();
            try
            {
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                    {
                        sqlcomman.Parameters.Add(parm);
                    }
                }
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlcomman);
                da.Fill(ds, "inquire");
                conn.Close();
                sqlcomman.Parameters.Clear();
                return ds;
            }
            catch (Exception ex)
            {
                return ds = null;
            }
        }
        public static int update(string str, SqlParameter[] cmdParms)
        {
            //更新数据
            SqlCommand sqlcomman = new SqlCommand();
            sqlcomman.Connection = conn;
            sqlcomman.CommandText = str;
            try
            {
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                    {
                        sqlcomman.Parameters.Add(parm);
                    }
                }
                conn.Open();
                int updateCount = sqlcomman.ExecuteNonQuery();
                conn.Close();
                sqlcomman.Parameters.Clear();
                if (updateCount > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static int tran(string str, SqlParameter[] cmdParms)
        {
            conn.Open();
            SqlCommand sqlcomman = conn.CreateCommand();
            SqlTransaction transaction;
            transaction = conn.BeginTransaction("Tran");
            sqlcomman.Connection = conn;
            sqlcomman.Transaction = transaction;
            string[] spstr = str.Split(';');
            int i;
            try
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    sqlcomman.Parameters.Add(parm);
                }
                for (i = 0; i < spstr.Length; i++)
                {
                    sqlcomman.CommandText = spstr[i];
                    try
                    {
                        sqlcomman.ExecuteScalar();
                    }
                    catch (Exception ex3)
                    {
                        return 0;
                    }
                }
                
                transaction.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    return 0;
                }
                catch (Exception ex2)
                {
                    return 0;
                }
            }
            finally
            {
                conn.Close();
                sqlcomman.Parameters.Clear();
            }
        }
    }
}