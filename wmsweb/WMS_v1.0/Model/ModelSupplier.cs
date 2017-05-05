using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    /// <summary>
    /// 供应商相关资料维护
    /// </summary>
    public class ModelSupplier
    {
        #region Model
        private int vendor_id;
        /// <summary>
        /// 厂商标号
        /// </summary>
        public int Vendor_id
        {
            get { return vendor_id; }
            set { vendor_id = value; }
        }

        private string vendor_name;
        /// <summary>
        /// 厂商名
        /// </summary>
        public string Vendor_name
        {
            get { return vendor_name; }
            set { vendor_name = value; }
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

        private string vendor_key;
        /// <summary>
        /// 厂商代码
        /// </summary>
        public string Vendor_key
        {
            get { return vendor_key; }
            set { vendor_key = value; }
        }
        #endregion

        public string toString()
        {
            return "vendor_id=" + vendor_id + ",vendor_name=" + vendor_name + ",create_time=" + create_time +
                ",create_by=" + create_by + ",update_time=" + update_time + ",update_by=" + update_by + ",vendor_code=" + vendor_key;
        }
    }
}