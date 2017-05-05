using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    /// <summary>
    /// 辅料表
    /// </summary>
    public class ModelPickup
    {
        #region Model
        private int simulate_line_id;
        /// <summary>
        /// 模拟子序号ID
        /// </summary>
        public int Simulate_line_id
        {
            get { return simulate_line_id; }
            set { simulate_line_id = value; }
        }

        private int simulate_id;
        /// <summary>
        /// 模拟ID
        /// </summary>
        public int Simulate_id
        {
            get { return simulate_id; }
            set { simulate_id = value; }
        }

        private int rcv_mtl_unique_id;
        /// <summary>
        /// 物料接收唯一值
        /// </summary>
        public int Rcv_mtl_unique_id
        {
            get { return rcv_mtl_unique_id; }
            set { rcv_mtl_unique_id = value; }
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

        private int pickup_qty = 0;
        /// <summary>
        /// 辅料数量 default 0
        /// </summary>
        public int Pickup_qty
        {
            get { return pickup_qty; }
            set { pickup_qty = value; }
        }

        private int subinventory_key;
        /// <summary>
        /// 模拟厂库ID
        /// </summary>
        public int Subinventory_key
        {
            get { return subinventory_key; }
            set { subinventory_key = value; }
        }

        private DateTime craete_time;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Craete_time
        {
            get { return craete_time; }
            set { craete_time = value; }
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

        private string datecode;
        /// <summary>
        /// 生产日期
        /// </summary>
        public string Datecode
        {
            get { return datecode; }
            set { datecode = value; }
        }

        private int issued_qyt = 0;
        /// <summary>
        /// 发料数量  default 0
        /// </summary>
        public int Issued_qyt
        {
            get { return issued_qyt; }
            set { issued_qyt = value; }
        }

        private int operation_seo__num;
        /// <summary>
        /// 制程号
        /// </summary>
        public int Operation_seo__num
        {
            get { return operation_seo__num; }
            set { operation_seo__num = value; }
        }

        private string issued_status;
        /// <summary>
        /// 发料状态（Y/N） default 'N' not null
        /// </summary>
        public string Issued_status
        {
            get { return issued_status; }
            set { issued_status = value; }
        }
        #endregion

        public string toString()
        {
            return "simulate_line_id=" + simulate_line_id + ",simulate_id=" + simulate_id + ",rcv_mtl_unique_id=" + rcv_mtl_unique_id + ",item_name=" +
                item_name + ",pickup_qty=" + pickup_qty + ",subinventory_key=" + subinventory_key + ",create_time=" + craete_time + ",update_time=" +
                update_time + ",datecode=" + datecode + ",issued_qyt=" + issued_qyt + ",operation_seo__num=" + operation_seo__num + ",issued_status=" +
                issued_status;
        }
    }
}