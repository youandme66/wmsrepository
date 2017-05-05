using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model                   //作者：周雅雯 最后一次修改时间：2016/7/18
{
    public class ModelReturn_header         //退料总表（Return_header）对应的Model
    {
        private int return_header_id;       //主键

        public int Return_header_id
        {
            get { return return_header_id; }
            set { return_header_id = value; }
        }
        private string invoice_no;          //退料单据号

        public string Invoice_no
        {
            get { return invoice_no; }
            set { invoice_no = value; }
        }

        private string return_type;         //退料类型

        public string Return_type
        {
            get { return return_type; }
            set { return_type = value; }
        }

        private int return_sub_key;         //库别K值

        public int Return_sub_key
        {
            get { return return_sub_key; }
            set { return_sub_key = value; }
        }
        private int status;                 //状态

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private DateTime create_time = DateTime.Now;       //创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private DateTime update_time = DateTime.Now;       //更新时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
        private int return_wo_no;           //退料工单号

        public int Return_wo_no
        {
            get { return return_wo_no; }
            set { return_wo_no = value; }
        }

        private string return_man;          //退料人员

        public string Return_man
        {
            get { return return_man; }
            set { return_man = value; }
        }
        private string remark;              //备注

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}