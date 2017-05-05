using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1;

namespace WMS_v1._0.DataCenter
{
    public class AnalogAcquisitionDC
    {
        /// <summary>
        /// 模拟查询
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="subinventory"></param>
        /// <returns></returns>
       

        //根据wo_no查询需求表
        public DataSet getSomeByWo_No(string wo_no)
        {
            string sql = "select * from wms_requirement_operation where wo_no=@wo_no";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no", wo_no)
            };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }

        //根据工单号查询该工单退料订单退料总量
        public DataSet get_item_by_wo_no(string wo_no)
        {
            string sql = "select wo_no=@wo_no sum(return_qty) as return_qty_sum from wms_return_line where return_wo_no=@wo_no and flag='0'";

            SqlParameter[] parameters = {
                new SqlParameter("wo_no", wo_no)
            };

            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }
        //根据工单号查询模拟表
        public DataSet get_simulite_by_wo_no(string wo_no)
        {
            string sql = "select * from wms_simulate_operation where wo_no=@wo_no";
            SqlParameter[] parameters ={
                                          new SqlParameter("wo_no",wo_no)
                                      };
            DB.connect();
            DataSet ds = DB.select(sql, parameters);
            return ds;
        }
        public int add_wo_simulate(string wo_no)
        {
            string sql = "insert into wms_simulate_operation(wo_no,wo_key,requirement_qty,item_id) select a.wo_no,b.wo_key,required_qty,c.item_id from wms_requirement_operation a inner join wms_wo b on a.wo_no=b.wo_no inner join wms_pn c on a.item_name=c.item_name where a.wo_no=@wo_no";
            SqlParameter[] parameters ={
                                          new SqlParameter("wo_no",wo_no)
                                      };
            DB.connect();
            int panduan = DB.insert(sql, parameters);
            return panduan;
        }
        public int update_wo_simulate(int simulate_id,string wo_no)
        {
            string sql = "update wms_simulate_operation set simulate_id=(select wo_key from wms_wo where wo_no=@wo_no)*100000000+@simulate_id where wo_no=@wo_no";
            SqlParameter[] parameters ={
                                          new SqlParameter("simulate_id",simulate_id),
                                          new SqlParameter("wo_no",wo_no)
                                      };
            DB.connect();
            int panduan = DB.update(sql, parameters);
            return panduan;
        }
        //拿出最新需求
        public DataSet get_new_demand(params string[] wo_no)
        {
            string sql = "select * from wms_requirement_operation where wo_no in (";
            for (int i = 0; i < wo_no.Length; i++)
            {
                if (i < wo_no.Length - 1)
                {
                    sql = sql + "@wo_no"+i+",";
                }
                else
                {
                    sql = sql + "@wo_no"+i+")";
                }
            }
            List<SqlParameter> params_wo_no=new List<SqlParameter>();
            for (int i = 0; i < wo_no.Length; i++)
            {
                params_wo_no.Add(new SqlParameter("wo_no" + i, wo_no[i]));
            }
            SqlParameter[] Params = params_wo_no.ToArray();
            DB.connect();
            DataSet ds = DB.select(sql, Params);
            return ds;
        }
        public int update_simulate(params string[] wo_no)
        {
            string sql = "update wms_simulate_operation set requirement_qty=(select required_qty from wms_requirement_operation b where b.wo_no=a.wo_no and b.item_name=(select item_name from wms_pn d where  a.item_id=d.item_id)) from wms_simulate_operation a where a.wo_no in (";
            for (int i = 0; i < wo_no.Length; i++)
            {
                if (i < wo_no.Length - 1)
                {
                    sql = sql + "@wo_no" + i + ",";
                }
                else
                {
                    sql = sql + "@wo_no" + i + ")";
                }
            }
            List<SqlParameter> params_wo_no = new List<SqlParameter>();
            for (int i = 0; i < wo_no.Length; i++)
            {
                params_wo_no.Add(new SqlParameter("wo_no" + i, wo_no[i]));
            }
            SqlParameter[] Params = params_wo_no.ToArray();
            DB.connect();
            int panduan=DB.update(sql, Params);
            return panduan;
        }
        public DataSet get_new_simulate(params string[] wo_no)
        {
            string sql = "select * from wms_simulate_operation where wo_no in (";
            for (int i = 0; i < wo_no.Length; i++)
            {
                if (i < wo_no.Length - 1)
                {
                    sql = sql + "@wo_no" + i + ",";
                }
                else
                {
                    sql = sql + "@wo_no" + i + ")";
                }
            }
            List<SqlParameter> params_wo_no = new List<SqlParameter>();
            for (int i = 0; i < wo_no.Length; i++)
            {
                params_wo_no.Add(new SqlParameter("wo_no" + i, wo_no[i]));  
            }
            SqlParameter[] Params = params_wo_no.ToArray();
            DB.connect();
            DataSet ds = DB.select(sql, Params);
            return ds;
        }
        //开始模拟
        public int start_simulate(params string[] wo_no)
        {
            DB.connect();
            for (int i = 0; i < wo_no.Length; i++)//循环用户输入的工单号
            {
                int[] item_id;
                int[] requirement_qty;
                int[] second_qty;//定义各个数组变量名，起始地址
                string sql1 = "select b.item_id,required_qty-c.qty as second_qty,required_qty from wms_requirement_operation a inner join wms_pn b on a.item_name=b.item_name left join (select sum(simulated_qty) as qty,wo_no,item_id from wms_simulate_operation group by wo_no,item_id having wo_no=@wo_no) as c on c.item_id=b.item_id where a.wo_no=@wo_no";
                SqlParameter[] parameters = {
                new SqlParameter("wo_no", wo_no[i])
               };
                DataSet ds1=DB.select(sql1, parameters);//取出料号和需求量
                if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                {
                    return 0;
                }
                item_id=new int[ds1.Tables[0].Rows.Count];
                requirement_qty=new int[ds1.Tables[0].Rows.Count];
                second_qty=new int[ds1.Tables[0].Rows.Count];//声明料号，需求量数组
                for (int i1 = 0; i1 < ds1.Tables[0].Rows.Count; i1++)
                {
                    item_id[i1] = int.Parse(ds1.Tables[0].Rows[i1][0].ToString());
                    requirement_qty[i1] = int.Parse(ds1.Tables[0].Rows[i1][2].ToString());//把料号和需求量数据存到数组中
                    if(ds1.Tables[0].Rows[i1][1].ToString()==null || ds1.Tables[0].Rows[i1][1].ToString()==""){                     //判断是否是第二次模拟
                        second_qty[i1]=-1;
                    }else{
                        second_qty[i1]=int.Parse(ds1.Tables[0].Rows[i1][1].ToString());
                    }
                }
                lock (this)
                {
                    for(int i1=0;i1<item_id.Length;i1++){//循环每个单号所需要的料号
                    string sql6="select sum(left_qty) from wms_material_io where item_id=@item_id and last_reinspect_status='PASS'";//计算料架总的剩余量
                    SqlParameter [] parameters5={
                                                    new SqlParameter("item_id",item_id[i1])
                                                };
                    DataSet ds7=DB.select(sql6,parameters5);
                    if (ds7 == null || ds7.Tables[0].Rows.Count == 0)
                    {
                        return 0;
                    }
                    int required_qty=0;
                    int all_left_qty = 0;
                    if (ds7.Tables[0].Rows.Count != 0)
                    {
                    if (ds7.Tables[0].Rows[0][0].ToString() != "")
                    {
                        all_left_qty = int.Parse(ds7.Tables[0].Rows[0][0].ToString());
                    }
                    }
                    if(second_qty[i1]==-1){
                    required_qty=requirement_qty[i1];
                    }else{
                    required_qty=second_qty[i1];
                    }
                    if(all_left_qty>required_qty){ //比较料架此料号的总的剩余量是否大于此料号的需求量
                    //for (i2 = 1; ; i2++)//如果大于此料号的需求量，则算出需要多少个料架去模拟。
                    //{
                     //   lock (this)
                     //   {
                     //   string sql5="select top @i2 sum(left_qty) from wms_material_io where left_qty>0 and item_id=@item_id and unique_id not in(select unique_id from wms_lock) order by datecode asc;";
                     //   string sql2 = "select top @i2 left_qty,unique_id from wms_material_io where left_qty>0 and item_id=@item_id and unique_id not in(select unique_id from wms_lock) order by datecode asc;";
                     //   SqlParameter[] parameters2 ={
                     //                                  new SqlParameter("i2",i2),
                     //                                  new SqlParameter("item_id",item_id[i2])
                     //                              };
                     //  DataSet ds2=DB.select(sql2, parameters2);
                     //  DataSet ds3=DB.select(sql5,parameters2);
                     //  if (int.Parse(ds3.Tables[0].Rows[0][0].ToString()) >= requirement_qty[i2])
                     //  {
                     //      left_qty=new int[ds2.Tables[0].Rows.Count];
                     //      unique_id1=new int[ds2.Tables[0].Rows.Count];
                     //      string sql4="insert into wms_lock(unique_id) values(";//放入第三张表
                     //      List<SqlParameter> unique_id = new List<SqlParameter>();
                     //      for (int i3 = 0; i < ds2.Tables[0].Rows.Count; i3++)
                     //      {
                     //          if(i3<ds2.Tables[0].Rows.Count-1){
                     //           sql4=sql4+"?,";
                     //          }
                     //          else{
                     //              sql4=sql4+"?)";
                     //          }
                     //          left_qty[i3] = int.Parse(ds2.Tables[0].Rows[i3][0].ToString());
                     //          unique_id1[i3]=int.Parse(ds2.Tables[0].Rows[i3][1].ToString());//把当前需要模拟的料架的剩余量和唯一id给取出来
                     //          //unique_id.Add(new SqlParameter("@unique_id"+i3,int.Parse(ds2.Tables[0].Rows[i3][1].ToString());
                     //      }
                     //      SqlParameter [] parameters4=unique_id.ToArray();
                     //      DB.insert(sql4,parameters4);
                     //          break;
                     //  }
                     //}

                        int alreadyNum = 0;
                        while (alreadyNum < required_qty)
                        {
                           int tempReqNum = required_qty - alreadyNum;
                           string sql2 = "select top 1 left_qty,unique_id from wms_material_io where item_id=@item_id and left_qty!=0 and last_reinspect_status='PASS' order by datecode asc";
                           SqlParameter[] parameters2={
                            new SqlParameter("item_id",item_id[i1])
                        };
                           string sql8 = "select count(item_id),wo_key from wms_simulate_operation where item_id=@item_id and wo_no=@wo_no group by wo_key";
                           SqlParameter[] parameters7 ={
                                                          new SqlParameter("item_id",item_id[i1]),
                                                          new SqlParameter("wo_no",wo_no[i])
                                                      };
                           DataSet ds3 = DB.select(sql8, parameters7);
                           string guilv;
                           string guilv2;
                           if (ds3.Tables[0].Rows.Count == 0 || ds3.Tables[0].Rows[0][0].ToString() == "" || ds3.Tables[0].Rows[0][1].ToString() == "")
                           {
                               guilv = "0";
                               string sql7 = "select wo_key from wms_wo where wo_no=@wo_no";
                               SqlParameter[] parameters8 ={
                                                               new SqlParameter("wo_no",wo_no[i])
                                                           };
                               DataSet ds4 = DB.select(sql7, parameters8);
                               guilv2 = ds4.Tables[0].Rows[0][0].ToString();
                           }
                           else
                           {
                               //guilv = ds3.Tables[0].Rows[0][0].ToString();
                               guilv = "0";
                               guilv2 = ds3.Tables[0].Rows[0][1].ToString();
                           }
                          DataSet ds2=DB.select(sql2,parameters2);//取出当前模拟料号以及datecode最小的也就是先进的料号和剩余量不为零的
                          DateTime d = DateTime.Now;
                          int simulate_id = int.Parse(d.ToString("yyMMdd")+guilv2+guilv);//计算备料ID
                            if (int.Parse(ds2.Tables[0].Rows[0][0].ToString()) <= tempReqNum)
                          {
                              string sql3 = "update wms_material_io set simulated_qty=simulated_qty+@simulated_qty where unique_id=@unique_id;"+
                                  "insert into wms_simulate_operation(unique_id,simulate_id,wo_no,item_id,requirement_qty,simulated_qty) values(@unique_id,@simulate_id,@wo_no,@item_id,@requirement_qty,@simulated_qty)";
                              SqlParameter[] parameters3 ={
                                                             new SqlParameter("unique_id",int.Parse(ds2.Tables[0].Rows[0][1].ToString())),
                                                             new SqlParameter("simulate_id",simulate_id),
                                                             new SqlParameter("wo_no",wo_no[i]),
                                                             new SqlParameter("item_id",item_id[i1]),
                                                             new SqlParameter("requirement_qty",requirement_qty[i1]),
                                                             new SqlParameter("simulated_qty",int.Parse(ds2.Tables[0].Rows[0][0].ToString()))
                                                         };
                             int fail1= DB.tran(sql3, parameters3);
                             if (fail1==0)
                             {
                                 return 0;
                             }
                            alreadyNum=alreadyNum+int.Parse(ds2.Tables[0].Rows[0][0].ToString());
                          }
                          else
                          {
                              string sql4 = "update wms_material_io set simulated_qty=simulated_qty+@simulated_qty where unique_id=@unique_id;"+
                                  "insert into wms_simulate_operation(unique_id,simulate_id,wo_no,item_id,requirement_qty,simulated_qty) values(@unique_id,@simulated_id,@wo_no,@item_id,@requirement_qty,@simulated_qty)";
                              SqlParameter [] parameters4={
                                                              new SqlParameter("unique_id",int.Parse(ds2.Tables[0].Rows[0][1].ToString())),
                                                              new SqlParameter("simulated_id",simulate_id),
                                                              new SqlParameter("wo_no",wo_no[i]),
                                                              new SqlParameter("item_id",item_id[i1]),
                                                              new SqlParameter("requirement_qty",requirement_qty[i1]),
                                                              new SqlParameter("simulated_qty",tempReqNum)
                                                          };
                               int fail2= DB.tran(sql4,parameters4);
                                if(fail2==0){
                                    return  0;
                                }
                                alreadyNum=alreadyNum+tempReqNum;//添加已模拟量
                          }
                        }
                    //}
                    //for (int i3 = 0; i3 < i2-1; i++)//循环更新在手数量明细表
                    //{
                    //    string sql3 = "update wms_material_io set simulated_qty=simulated_qty+left_qty,left_qty=0 where  left_qty>0 and item_id=@item_id and unique_id=(select top 1 unique_id from wms_material_io where left_qty>0 and item_id=1 order by datecode asc);"+
                    //        "";
                    //    SqlParameter[] parameters3 ={
                    //                                    new SqlParameter("item_id",@item_id)
                    //                                };
                    //    DB.update(sql3, parameters3);
                    //    if (i3 == i2 - 1)
                    //    {
                    //        string sql4 = "update wms_material_io ";
                    //    }
                    //}

                    }else{
                        string sql8 = "select count(item_id),wo_key from wms_simulate_operation where item_id=@item_id and wo_no=@wo_no group by wo_key";
                        SqlParameter[] parameters7 ={
                                                          new SqlParameter("item_id",item_id[i1]),
                                                          new SqlParameter("wo_no",wo_no[i])
                                                      };
                        DataSet ds3 = DB.select(sql8, parameters7);
                        string guilv;
                        string guilv2;
                        if (ds3.Tables[0].Rows.Count == 0 || ds3.Tables[0].Rows[0][0].ToString() == "" || ds3.Tables[0].Rows[0][1].ToString() == "")
                        {
                            guilv = "0";
                            string sql7 = "select wo_key from wms_wo where wo_no=@wo_no";
                            SqlParameter[] parameters8 ={
                                                               new SqlParameter("wo_no",wo_no[i])
                                                           };
                            DataSet ds4 = DB.select(sql7, parameters8);
                            guilv2 = ds4.Tables[0].Rows[0][0].ToString();
                        }
                        else
                        {
                            //guilv = ds3.Tables[0].Rows[0][0].ToString();
                            guilv = "0";
                            guilv2 = ds3.Tables[0].Rows[0][1].ToString();
                        }
                        DateTime d = DateTime.Now;
                        int simulate_id = int.Parse(d.ToString("yyMMdd")+guilv2+guilv);//计算备料ID
                        string sql9 = "select count(item_id) from wms_material_io where item_id=@item_id and left_qty>0 and last_reinspect_status='PASS'";
                        SqlParameter[] parameters11 ={
                                                         new SqlParameter("item_id",item_id[i1])
                                                     };
                       DataSet ds11=DB.select(sql9, parameters11);
                       if (ds11.Tables[0].Rows.Count != 0 && ds11.Tables[0].Rows[0][0].ToString() != "")
                       {
                           string sql5 = "insert into wms_simulate_operation(unique_id,wo_no,item_id,requirement_qty,simulated_qty) (select top 1 c.unique_id,a.wo_no,b.item_id,a.required_qty,c.left_qty from wms_requirement_operation a inner join wms_pn b on a.item_name=b.item_name inner join wms_material_io c on b.item_id=c.item_id where wo_no=@wo_no and b.item_id=@item_id and c.left_qty>0 and c.last_reinspect_status='PASS');" +
                           "update top (1) wms_material_io set simulated_qty=simulated_qty+left_qty where item_id=@item_id and last_reinspect_status='PASS' and left_qty>0;" +
                           "update wms_simulate_operation set simulate_id=@simulate_id where simulate_id is null";
                           SqlParameter[] parameters6 ={
                                                    new SqlParameter("wo_no",wo_no[i]),
                                                    new SqlParameter("item_id",item_id[i1]),
                                                    new SqlParameter("simulate_id",simulate_id)
                                                    };
                           for (int i11 = 0; i11 < int.Parse(ds11.Tables[0].Rows[0][0].ToString()); i11++)
                           {
                             if (i11 > 0)
                               {

                                   simulate_id = int.Parse(d.ToString("yyMMdd") + guilv2 +guilv);
                                   parameters6[2].Value = simulate_id;

                               }//一次性模拟多个料号但是模拟的次数不同
                         int fail3=DB.tran(sql5,parameters6);
                         if(fail3==0){
                            return 0;
                         }
                           }
                       }
                       
                        
                    }   
                }//料号模拟结束
                }
                
            }//单号模拟结束
            int success=1;
            return success;
        }
    }
}