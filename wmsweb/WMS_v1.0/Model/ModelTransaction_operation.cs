using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model//作者：周雅雯 最后一次修改时间：2016/10/22
{
    public class ModelTransaction_operation//交易表（Transaction_operation）对应的Model
    {
        private int wms_transaction_id;     //主键

        public int Wms_transaction_id
        {
            get { return wms_transaction_id; }
            set { wms_transaction_id = value; }
        }
        private int transaction_qty;        //交易数量

        public int Transaction_qty
        {
            get { return transaction_qty; }
            set { transaction_qty = value; }
        }

        private string item_name;

        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }

        private string po_no;

        public string Po_no
        {
            get { return po_no; }
            set { po_no = value; }
        }

        private string invoice_no;          //暂收单号

        public string Invoice_no
        {
            get { return invoice_no; }
            set { invoice_no = value; }
        }

        //private int subinventory_key;       //外键，串仓库表的库别K值

        //public int Subinventory_key
        //{
        //    get { return subinventory_key; }
        //    set { subinventory_key = value; }
        //}
        //private int frame_key;              //外键，串料架表的料架K值

        //public int Frame_key
        //{
        //    get { return frame_key; }
        //    set { frame_key = value; }
        //}
        private string transaction_type;    //交易类型

        public string Transaction_type
        {
            get { return transaction_type; }
            set { transaction_type = value; }
        }
        private DateTime transaction_time = DateTime.Now;   //交易时间

        public DateTime Transaction_time
        {
            get { return transaction_time; }
            set { transaction_time = value; }
        }
        private DateTime create_time = DateTime.Now;        //创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }

        private string create_user;                         //交易人员

        public string Create_user
        {
            get { return create_user; }
            set { create_user = value; }
        }

        private DateTime update_time = DateTime.Now;        //更新时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }



    }
}