using System;
using System.Data;
using System.Web;
using WMS_v1._0.DataCenter;
using WMS_v1._0.Model;

namespace WMS_v1._0.Web
{
    /// <summary>
    /// Summary description for getNew_ship_no
    /// </summary>
    public class get_new_ship_no : IHttpHandler
    {
        /// <summary>
        /// 生成下一个出货单号
        /// </summary>
        /// <returns></returns>
        protected string nextShip_no()
        {
            ShipDC ship_dc = new ShipDC();
            string lastship_no = "";
            string head = "P";
            string end = "0001";
            DateTime date = DateTime.Now;
            //获取当前的时间
            string time = date.ToString("yyyyMMdd");
            //获取最后一次的ship_no
            ModelShip modelship = ship_dc.getLastShip_no();
            int last_id = ship_dc.getLast_id();//数据库中最后一次存储的ID
            int ship_key;
            if (modelship == null)
                return null;
            else
            {
                ship_key = modelship.Ship_key;
                lastship_no = modelship.Ship_no;
            }
            if (lastship_no == null)
            {
                end = ((last_id + 1) % 10000).ToString("D4");// "0001";
            }
            else
            {
                int s = lastship_no.Length;
                string last_date = lastship_no.Substring(1, 8);//上一条数据的日期
                if (string.Compare(last_date, time, StringComparison.Ordinal) < 0)
                    end = "0001";
                else
                {
                    string last_no = lastship_no.Substring(s - 4, 4);
                    //获取后后四位，转换为数字
                    int last = int.Parse(last_no);
                    //格式化整型，D:十进制，4：字符串长度
                    end = (last_id - ship_key + last + 1).ToString("D4");
                    if (end.Equals("10000"))
                        end = "0001";
                }
            }
            return head + time + end;
        }

        public void ProcessRequest(HttpContext context)
        {
            string json = nextShip_no();

            context.Response.ContentType = "text/plain";

            context.Response.Write(json);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}