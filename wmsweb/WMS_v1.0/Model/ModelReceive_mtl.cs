using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    /// <summary>
    /// 暂收表
    /// </summary>
    public class ModelReceive_mtl
    {
        #region Model
        private int unique_id;
        /// <summary>
        /// ID
        /// </summary>
        public int Unique_id
        {
            get { return unique_id; }
            set { unique_id = value; }
        }

        private int lot_number;
        /// <summary>
        /// 批号
        /// </summary>
        public int Lot_number
        {
            get { return lot_number; }
            set { lot_number = value; }
        }

        private string receipt_no;
        /// <summary>
        /// 暂收单号
        /// </summary>
        public string Receipt_no
        {
            get { return receipt_no; }
            set { receipt_no = value; }
        }

        private int item_id;
        /// <summary>
        /// 料号ID
        /// </summary>
        public int Item_id
        {
            get { return item_id; }
            set { item_id = value; }
        }

        private string item_name;
        /// <summary>
        /// 料号
        /// </summary>
        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }

        private int frame_key;
        /// <summary>
        /// 料架key值
        /// </summary>
        public int Frame_key
        {
            get { return frame_key; }
            set { frame_key = value; }
        }

        private int rcv_qty;
        /// <summary>
        /// 暂收量
        /// </summary>
        public int Rcv_qty
        {
            get { return rcv_qty; }
            set { rcv_qty = value; }
        }

        private int accepted_qty;
        /// <summary>
        /// 允收数量
        /// </summary>
        public int Accepted_qty
        {
            get { return accepted_qty; }
            set { accepted_qty = value; }
        }

        private int return_qty;
        /// <summary>
        /// 退回量
        /// </summary>
        public int Return_qty
        {
            get { return return_qty; }
            set { return_qty = value; }
        }

        private int deliver_qty;
        /// <summary>
        /// 入库量
        /// </summary>
        public int Deliver_qty
        {
            get { return deliver_qty; }
            set { deliver_qty = value; }
        }

        private string po_no;
        /// <summary>
        /// 采购单号
        /// </summary>
        public string Po_no
        {
            get { return po_no; }
            set { po_no = value; }
        }

        private int po_header_id;
        /// <summary>
        /// PO表头ID
        /// </summary>
        public int Po_header_id
        {
            get { return po_header_id; }
            set { po_header_id = value; }
        }

        private int po_line_id;
        /// <summary>
        /// PO表身ID
        /// </summary>
        public int Po_line_id
        {
            get { return po_line_id; }
            set { po_line_id = value; }
        }

        private string status;
        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string vendor_code;
        /// <summary>
        /// 厂商代码
        /// </summary>
        public string Vendor_code
        {
            get { return vendor_code; }
            set { vendor_code = value; }
        }

        private DateTime receive_time;
        /// <summary>
        /// 暂收时间
        /// </summary>
        public DateTime Receive_time
        {
            get { return receive_time; }
            set { receive_time = value; }
        }

        private DateTime create_time = DateTime.Now;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }

        private DateTime update_time = DateTime.Now;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }

        private string datecode;
        /// <summary>
        /// 生产日期
        /// </summary>
        public string Datecode
        {
            get { return datecode; }
            set { datecode = value; }
        }

        #endregion

        public string toString()
        {
            return "unique_id=" + unique_id + ",lot_number=" + lot_number + ",receipt_no=" + receipt_no + ",item_id=" + item_id + ",item_name=" + item_name +
                ",rcv_qty=" + rcv_qty + ",accepted_qty=" + accepted_qty + ",return_qty=" + return_qty + ",deliver_qty=" + deliver_qty + ",po_no=" +
                po_no + ",po_header_id=" + po_header_id + ",po_line_id=" + po_line_id + ",vendor_code=" + vendor_code + ",receive_time=" + receive_time
                + ",create_time=" + create_time + ",update_time=" + update_time + ",datecode=" + datecode ;
        }
    }
}