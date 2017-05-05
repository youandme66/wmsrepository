using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    /// <summary>
    /// PO单头
    /// </summary>
    public class ModelPO_Header
    {
        #region
        private int po_header_id;
        /// <summary>
        /// Header_id
        /// </summary>
        public int Po_header_id
        {
            get { return po_header_id; }
            set { po_header_id = value; }
        }

        private string po_no;
        /// <summary>
        /// PO单号
        /// </summary>
        public string Po_no
        {
            get { return po_no; }
            set { po_no = value; }
        }

        private string vendor_key;  //新更改的字段，根据最新逻辑 2016/8.12 zyw

        public string Vendor_key
        {
            get { return vendor_key; }
            set { vendor_key = value; }
        }

        
        /// <summary>
        /// 厂商代码
        /// </summary>
        

        private DateTime create_time;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }

        private int create_by;
        /// <summary>
        /// 创建者
        /// </summary>
        public int Create_by
        {
            get { return create_by; }
            set { create_by = value; }
        }

        private DateTime update_time;
        /// <summary>
        /// 更改时间
        /// </summary>
        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }

        private int update_by;
        /// <summary>
        /// 更改者
        /// </summary>
        public int Update_by
        {
            get { return update_by; }
            set { update_by = value; }
        }
        #endregion

        public string toString()
        {
            return "po_header_id=" + po_header_id + ",po_no=" + po_no + ",vendor_id=" + vendor_key + ",create_time=" + create_time + ",create_by="
                + create_by + ",update_by=" + update_by + ",update_time=" + update_time;
        }
    }
}