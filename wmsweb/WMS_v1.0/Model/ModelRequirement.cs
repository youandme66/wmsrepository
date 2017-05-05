using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    /// <summary>
    /// 工单用料需求表
    /// </summary>
    public class ModelRequirement
    {
        #region Model
        private int requirement_line_id;
        /// <summary>
        /// 需求ID
        /// </summary>
        public int Requirement_line_id
        {
            get { return requirement_line_id; }
            set { requirement_line_id = value; }
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

        private string item_name;
        /// <summary>
        /// 料号
        /// </summary>
        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }

        private int operation_seq_num;
        /// <summary>
        /// 制程
        /// </summary>
        public int Operation_seq_num
        {
            get { return operation_seq_num; }
            set { operation_seq_num = value; }
        }

        private int required_qty;
        /// <summary>
        /// 需用料量
        /// </summary>
        public int Required_qty
        {
            get { return required_qty; }
            set { required_qty = value; }
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
        #endregion

        public string toString()
        {
            return "requirement_line_id=" + requirement_line_id + ",wo_no=" + wo_no + ",item_name=" + item_name + ",operation_seq_num=" +
                operation_seq_num + ",required_qty=" + required_qty + ",create_time=" + create_time + ",update_time=" + update_time;
        }
    }
}