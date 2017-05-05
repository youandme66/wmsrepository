using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    /// <summary>
    /// PO表身
    /// </summary>
    public class ModelPO_line
    {
        #region Model
        private int po_line_id;
        /// <summary>
        /// PO表身id
        /// </summary>
        public int Po_line_id
        {
            get { return po_line_id; }
            set { po_line_id = value; }
        }

        private int po_header_id;
        /// <summary>
        /// PO表头id
        /// </summary>
        public int Po_header_id
        {
            get { return po_header_id; }
            set { po_header_id = value; }
        }

        private int line_num;
        /// <summary>
        /// Line序号
        /// </summary>
        public int Line_num
        {
            get { return line_num; }
            set { line_num = value; }
        }

        private int item_id;
        /// <summary>
        /// 料号
        /// </summary>
        public int Item_id
        {
            get { return item_id; }
            set { item_id = value; }
        }

        private int request_qty;
        /// <summary>
        /// 总量
        /// </summary>
        public int Request_qty
        {
            get { return request_qty; }
            set { request_qty = value; }
        }

        //private string closed = "Y";
        ///// <summary>
        ///// 开关状态
        ///// </summary>
        //public string Closed
        //{
        //    get { return closed; }
        //    set { closed = value; }
        //}

        private string cancel_flag = "N";
        /// <summary>
        /// 是否取消
        /// </summary>
        public string Cancel_flag
        {
            get { return cancel_flag; }
            set { cancel_flag = value; }
        }
        #endregion

        public string toString()
        {
            return "po_line_id=" + po_line_id + ",po_header_id=" + po_header_id + ",line_num=" + line_num + ",item_id=" + item_id + ",request_qty="
                + request_qty + ",cancel_flag=" + cancel_flag;
        }
    }
}