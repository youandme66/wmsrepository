using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    /// <summary>
    /// 客户信息
    /// </summary>
    public class ModelCustomers
    {
        #region Model
        private int customer_key;
        /// <summary>
        /// 客户代码
        /// </summary>
        public int Customer_key
        {
            get { return customer_key; }
            set { customer_key = value; }
        }

        private string customer_name;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string Customer_name
        {
            get { return customer_name; }
            set { customer_name = value; }
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

        private string create_by;
        /// <summary>
        /// 创建者
        /// </summary>
        public string Create_by
        {
            get { return create_by; }
            set { create_by = value; }
        }

        private DateTime update_time = DateTime.Now;
        /// <summary>
        /// 更改时间
        /// </summary>
        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }

        private string update_by;
        /// <summary>
        /// 更改者
        /// </summary>
        public string Update_by
        {
            get { return update_by; }
            set { update_by = value; }
        }

        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        #endregion

        public string toString()
        {
            return "custmoer_name=" + customer_name + ",customer_code=" + customer_key + ",create_time=" + create_time +
                ",create_by=" + create_by + ",update_time=" + update_time + ",update_by=" + update_by;
        }
    }
}