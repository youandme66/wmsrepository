using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model                   //作者：周雅雯 最后一次修改时间：2016/8/2
{
    public class ModelExchange_header       //调拨单主表（Exchange_header）对应的Model
    {
        private int exchange_header_id;     //主键

        public int Exchange_header_id
        {
            get { return exchange_header_id; }
            set { exchange_header_id = value; }
        }
        private string invoice_no;             //调拨单据号

        public string Invoice_no
        {
            get { return invoice_no; }
            set { invoice_no = value; }
        }

        private int out_locator_id;         //调出locator

        public int Out_locator_id
        {
            get { return out_locator_id; }
            set { out_locator_id = value; }
        }
       
       
        private int out_subinventory_key;   //调出库别K值

        public int Out_subinventory_key
        {
            get { return out_subinventory_key; }
            set { out_subinventory_key = value; }
        }

        private int in_locator_id;          //调入locator

        public int In_locator_id
        {
            get { return in_locator_id; }
            set { in_locator_id = value; }
        }

        private int in_subinventory_key;    //调出库别K值

        public int In_subinventory_key
        {
            get { return in_subinventory_key; }
            set { in_subinventory_key = value; }
        }
        private string status;              //状态

        public string Status
        {
            get { return status; }
            set { status = value; }
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
        private string exchange_wo_no;      //调拨工单号

        public string Exchange_wo_no
        {
            get { return exchange_wo_no; }
            set { exchange_wo_no = value; }
        }

        private string update_wo_no;       //更新人员

        public string Update_wo_no
        {
            get { return update_wo_no; }
            set { update_wo_no = value; }
        }
    }
}