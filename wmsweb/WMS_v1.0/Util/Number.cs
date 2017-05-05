using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Util
{
    public class Number
    {
        public static string code(int header_id, int m)
        {
            if (header_id ==0)
            {
                int j;
                string end2 = "00001";
                string month1 = System.DateTime.Now.Month.ToString();
                string year1 = System.DateTime.Now.Year.ToString();
                year1 = year1.Substring(2, 2);
                j = month1.Length;
                for (; j < 2; j++)
                {
                    month1 = "0" + month1;
                }
                string invoice2="";
                if (m == 1)
                {
                    invoice2 = "Q" + year1 + month1 + end2;
                }
                else if (m == 2)
                {
                    invoice2 = "F" + year1 + month1 + end2;
                }
                else if (m == 3)
                {
                    invoice2 = "T" + year1 + month1 + end2;
                }
                return invoice2;            
            }
            else
            {
                int i;
                Int64 end = header_id;
                end = end % 99998 == 0 ? 99998 + 1 : end % 99999 + 1;
                string end1 = end.ToString();
                i = end1.Length;
                for (; i < 5; i++)
                {
                    end1 = "0" + end1;
                }
                string month = System.DateTime.Now.Month.ToString();
                string year = System.DateTime.Now.Year.ToString();
                year = year.Substring(2, 2);
                i = month.Length;
                for (; i < 2; i++)
                {
                    month = "0" + month;
                }
                string invoice2="";
                if (m == 1)
                {
                    invoice2 = "Q" + year + month + end1;
                }
                else if (m == 2)
                {
                    invoice2 = "F" + year + month + end1;
                }
                else if (m == 3)
                {
                    invoice2 = "T" + year + month + end1;
                }
                return invoice2;
            }
        }
    }
}