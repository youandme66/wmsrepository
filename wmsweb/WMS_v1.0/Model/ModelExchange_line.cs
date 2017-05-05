using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model               //作者：周雅雯 最后一次修改时间：2016/8/2
{
    public class ModelExchange_line     //调拨单从表（Exchange_line）对应的Model
    {
        private int exchange_line_id;   //主键

        public int Exchange_line_id
        {
            get { return exchange_line_id; }
            set { exchange_line_id = value; }
        }
        private int exchange_header_id; //主从表的K值

        public int Exchange_header_id
        {
            get { return exchange_header_id; }
            set { exchange_header_id = value; }
        }
        private int item_id;            //料号ID

        public int Item_id
        {
            get { return item_id; }
            set { item_id = value; }
        }
        private string item_name;       //料号

        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }

        private int operation_seq_num;  //制程

        public int Operation_seq_num
        {
            get { return operation_seq_num; }
            set { operation_seq_num = value; }
        }

        private int required_qty;       //需求量

        public int Required_qty
        {
            get { return required_qty; }
            set { required_qty = value; }
        }
        private int exchanged_qty;      //调拨数量

        public int Exchanged_qty
        {
            get { return exchanged_qty; }
            set { exchanged_qty = value; }
        }
        private DateTime exchanged_time = DateTime.Now;  //调拨时间

        public DateTime Exchanged_time
        {
            get { return exchanged_time; }
            set { exchanged_time = value; }
        }

      

        private DateTime create_time = DateTime.Now;    //创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private DateTime update_time = DateTime.Now;    //更新时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
        private string exchange_wo_no;                  //调拨人员ID

        public string Exchange_wo_no
        {
            get { return exchange_wo_no; }
            set { exchange_wo_no = value; }
        }

        private string update_wo_no;                    //更新人员ID

        public string Update_wo_no
        {
            get { return update_wo_no; }
            set { update_wo_no = value; }
        }

        private string remark;                          //备注

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}