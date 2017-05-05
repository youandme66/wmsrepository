using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    /// <summary>
    /// 模拟表
    /// </summary>
    public class ModelSimulate
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

        private string wo_key;
        /// <summary>
        /// 工单Key值
        /// </summary>
        public string Wo_key
        {
            get { return wo_key; }
            set { wo_key = value; }
        }

        private string item_id;
        /// <summary>
        /// 料号ID
        /// </summary>
        public string Item_id
        {
            get { return item_id; }
            set { item_id = value; }
        }

        private int simulated_qty;
        /// <summary>
        /// 模拟数量
        /// </summary>
        public int Simulated_qty
        {
            get { return simulated_qty; }
            set { simulated_qty = value; }
        }

        private string invoice_no;
        /// <summary>
        /// 单据号
        /// </summary>
        public string Invoice_no
        {
            get { return invoice_no; }
            set { invoice_no = value; }
        }

        private string issued_status = "N";
        /// <summary>
        /// I:发料状态  P:辅料数量  default'N'
        /// </summary>
        public string Issued_status
        {
            get { return issued_status; }
            set { issued_status = value; }
        }

        private int pickup_qty = 0;
        /// <summary>
        /// 辅料数量    default 0
        /// </summary>
        public int Pickup_qty
        {
            get { return pickup_qty; }
            set { pickup_qty = value; }
        }

        private int issued_qty = 0;
        /// <summary>
        /// 发料数量
        /// </summary>
        public int Issued_qty
        {
            get { return issued_qty; }
            set { issued_qty = value; }
        }
        #endregion

        public string toString()
        {
            return "simulate_line_id=" + simulate_line_id + ",simulate_id=" + simulate_id + ",wo_key=" + wo_key + ",item_id=" + item_id + ",simulated_qty="
                + simulated_qty + ",invoice_no=" + invoice_no + ",issued_status=" + issued_status + ",pickup_qty=" + pickup_qty + ",issued_qty=" + issued_qty;
        }
    }
}