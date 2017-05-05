using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model                   //作者：周雅雯 最后一次修改时间：2016/7/18
{
    public class ModelMtl_issue_line     //领料从表（Mtl_issue_line）对应的Model
    {
        private int issue_line_id;          //主键

        public int Issue_line_id
        {
            get { return issue_line_id; }
            set { issue_line_id = value; }
        }
        private int issue_header_id;        //外键，主从表的K值

        public int Issue_header_id
        {
            get { return issue_header_id; }
            set { issue_header_id = value; }
        }
        private int line_num;

        public int Line_num
        {
            get { return line_num; }
            set { line_num = value; }
        }

        public int status
        {
            get { return status; }
            set { status = value; }
        }
        private string item_name;           //料号

        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }
        private string remark;              //备注

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private int required_qty;           //需求量

        public int Required_qty
        {
            get { return required_qty; }
            set { required_qty = value; }
        }
        private int simulated_qty;          //模拟量

        public int Simulated_qty
        {
            get { return simulated_qty; }
            set { simulated_qty = value; }
        }
        private int issued_qty = 0;             //领料量

        public int Issued_qty
        {
            get { return issued_qty; }
            set { issued_qty = value; }
        }
        private int issued_sub_key;         //库别K值

        public int Issued_sub_key
        {
            get { return issued_sub_key; }
            set { issued_sub_key = value; }
        }
        private DateTime create_time = DateTime.Now;       //创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private DateTime update_time = DateTime.Now;       //更新时间

        public DateTime Update_time             //更新时间
        {
            get { return update_time; }
            set { update_time = value; }
        }
        private string consume_flag = "N";

        public string Consume_flag
        {
            get { return consume_flag; }
            set { consume_flag = value; }
        }

        private string frame_key;

        public string Frame_key                 //料架号
        {
            get { return frame_key; }
            set { frame_key = value; }
        }
        private string update_wo_no;

        public string Update_wo_no
        {
            get { return update_wo_no; }
            set { update_wo_no = value; }
        }

    }
}