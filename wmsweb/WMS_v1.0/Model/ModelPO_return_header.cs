using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model//PO退回总表(wms_po_return_header）对应model
{
    public class ModelPO_return_header //作者：周雅雯 最后一次修改时间：2016/8/2
    {
        private int return_header_id;

        public int Return_header_id
        {
            get { return return_header_id; }
            set { return_header_id = value; }
        }
        private string receipt_no;      //暂收单号

        public string Receipt_no
        {
            get { return receipt_no; }
            set { receipt_no = value; }
        }
        private string vendor_name;     //客户名称

        public string Vendor_name
        {
            get { return vendor_name; }
            set { vendor_name = value; }
        }
        private int return_region;      //退回区域

        public int Return_region
        {
            get { return return_region; }
            set { return_region = value; }
        }
        private DateTime return_time;   //调拨时间

        public DateTime Return_time
        {
            get { return return_time; }
            set { return_time = value; }
        }
        private string return_wo_no;    //操作人员ID

        public string Return_wo_no
        {
            get { return return_wo_no; }
            set { return_wo_no = value; }
        }
        private DateTime update_time;   //更新时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
        private string update_wo_no;    //更新人员ID

        public string Update_wo_no
        {
            get { return update_wo_no; }
            set { update_wo_no = value; }
        }
        
    }
}