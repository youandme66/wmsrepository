using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model                   //作者：周雅雯 最后一次修改时间：2016/7/16
{
    public class Model6in1_id_relation      //六合一关系表（6in1_id_relation）对应的Model
    {
        private string prior_id;            //之前的流水码

        public string Prior_id
        {
            get { return prior_id; }
            set { prior_id = value; }
        }
        private string current_id;          //现在的流水码

        public string Current_id
        {
            get { return current_id; }
            set { current_id = value; }
        }
        private DateTime create_time = DateTime.Now;       //创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private string type;                //料号

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private int qty;                    //旧的流水码对应的数量

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        private string lot_no;              //旧流水码对应的LOT_NO

        public string Lot_no
        {
            get { return lot_no; }
            set { lot_no = value; }
        }

        private string date_code;
        public string Date_code       //生产日期
        {
            get { return date_code; }
            set { date_code = value; }
        }
        private string vendor_code;         //厂商

        public string Vendor_code
        {
            get { return vendor_code; }
            set { vendor_code = value; }
        }
        private string wo_no;               //工单号

        public string Wo_no
        {
            get { return wo_no; }
            set { wo_no = value; }
        }
    }
}