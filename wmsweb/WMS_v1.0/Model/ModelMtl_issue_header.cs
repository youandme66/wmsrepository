using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model                   //作者：周雅雯 最后一次修改时间：2016/7/18
{
    public class ModelMtl_issue_header      //领料主表（Mtl_issue_header）对应的Model
    {
        private int issue_header_id;        //主键，领料ID

        public int Issue_header_id
        {
            get { return issue_header_id; }
            set { issue_header_id = value; }
        }

        private string invoice_no;

        public string Invoice_no
        {
            get { return invoice_no; }
            set { invoice_no = value; }
        }
        private string wo_no;               //工单

        public string Wo_no
        {
            get { return wo_no; }
            set { wo_no = value; }
        }
        private int wo_key;          //外键，工单ID

        public int Wo_key
        {
            get { return wo_key; }
            set { wo_key = value; }
        }


        private string issued_sub_key;          //库别

        public string Issued_sub_key
        {
            get { return issued_sub_key; }
            set { issued_sub_key = value; }
        }

        
        private DateTime create_time = DateTime.Now;        //创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private DateTime update_time = DateTime.Now;        //更新时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
        private string operation_seq_num;                      //制程

        public string Operation_seq_num
        {
            get { return operation_seq_num; }
            set { operation_seq_num = value; }
        }
        private string issue_type;      //领料类型

        public string Issue_type
        {
            get { return issue_type; }
            set { issue_type = value; }
        }

        private string issue_man;       //领料人

        public string Issue_man
        {
            get { return issue_man; }
            set { issue_man = value; }
        }
        private string customer_key;    //客户代码

        public string Customer_key
        {
            get { return customer_key; }
            set { customer_key = value; }
        }
        private string remark;          //备注

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}