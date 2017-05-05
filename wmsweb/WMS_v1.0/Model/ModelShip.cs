using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    /// <summary>
    /// 出货表
    /// </summary>
    public class ModelShip
    {
        #region Model
        private int ship_key;
        /// <summary>
        /// 出货ID
        /// </summary>
        public int Ship_key
        {
            get { return ship_key; }
            set { ship_key = value; }
        }

        private string ship_no;
        /// <summary>
        /// 出货单号
        /// </summary>
        public string Ship_no
        {
            get { return ship_no; }
            set { ship_no = value; }
        }

        private int part_no;
        public int Part_no
        {
            get { return part_no; }
            set { part_no = value; }
        }

        private int request_qty;
        public int Request_qty
        {
            get { return request_qty; }
            set { request_qty = value; }
        }

        private int picked_qty;
        public int Picked_qty
        {
            get { return picked_qty; }
            set { picked_qty = value; }
        }

        private int customer_id;
        /// <summary>
        /// 客户ID
        /// </summary>
        public int Customer_id
        {
            get { return customer_id; }
            set { customer_id = value; }
        }

        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private DateTime create_time;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }

        private DateTime update_time;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }

        private string create_man;
        public string Create_man
        {
            get
            {
                return create_man;
            }

            set
            {
                create_man = value;
            }
        }

        private string update_man;
        public string Update_man
        {
            get
            {
                return update_man;
            }

            set
            {
                update_man = value;
            }
        }
        
        #endregion
    }
}