using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    /// <summary>
    /// 工单表
    /// </summary>
    public class ModelWO
    {
        #region Model
        private int wo_key;
        /// <summary>
        /// 工单ID
        /// </summary>
        public int Wo_key
        {
            get { return wo_key; }
            set { wo_key = value; }
        }

        private string wo_no;
        /// <summary>
        /// 工单号
        /// </summary>
        public string Wo_no
        {
            get { return wo_no; }
            set { wo_no = value; }
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

        private int target_qty;
        /// <summary>
        /// 数量
        /// </summary>
        public int Target_qty
        {
            get { return target_qty; }
            set { target_qty = value; }
        }

        private string part_no;
        /// <summary>
        /// 新料号
        /// </summary>
        public string Part_no
        {
            get { return part_no; }
            set { part_no = value; }
        }
        private int turnin_qty;
        /// <summary>
        /// 入库量
        /// </summary>
        public int Turnin_qty
        {
            get { return turnin_qty; }
            set { turnin_qty = value; }
        }
        private int shipped_qty;
        /// <summary>
        /// 出货量
        /// </summary>
        public int Shipped_qty
        {
            get { return shipped_qty; }
            set { shipped_qty = value; }
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

        private DateTime release_date;
        /// <summary>
        /// 开启时间
        /// </summary>
        public DateTime Release_date
        {
            get { return release_date; }
            set { release_date = value; }
        }

        private DateTime close_date;
        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime Close_date
        {
            get { return close_date; }
            set { close_date = value; }
        }
        #endregion

        public string toString()
        {
            return "wo_no=" + wo_no + ",wo_key=" + wo_key + ",status=" + status + ",target_qty=" + target_qty + ",part_no=" + part_no +",turnin_qty="+ turnin_qty+",shipped_qty="+shipped_qty+",create_time="
                + create_time + ",update_time=" + update_time + ",release_date=" + release_date + ",close_date=" + close_date;
        }
    }
}