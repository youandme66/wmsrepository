using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model           //作者：周雅雯 最后一次修改时间：2016/7/18
{
    public class Model6in1_detail   //六合一明细表（6in1_detail）对应的Model
    {
        private string pn;

        public string Pn
        {
            get { return pn; }
            set { pn = value; }
        }
        private int qty;            //数量

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        private string lot_no;      //lot号

        public string Lot_no
        {
            get { return lot_no; }
            set { lot_no = value; }
        }
        private string date_code;   //生产日期

        public string Date_code
        {
            get { return date_code; }
            set { date_code = value; }
        }
        private string vendor_code; //厂商

        public string Vendor_code
        {
            get { return vendor_code; }
            set { vendor_code = value; }
        }
        private string id;          //流水码

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private DateTime create_time = DateTime.Now;//创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private string user_name;   //列印人员工号

        public string User_name
        {
            get { return user_name; }
            set { user_name = value; }
        }
    }
}