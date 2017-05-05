using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model               //作者：周雅雯 最后一次修改时间：2016/7/16
{
    public class ModelReturn_line       //退料明细表（Return_line）对应的Model
    {
        private int return_line_id;     //主键

        public int Return_line_id
        {
            get { return return_line_id; }
            set { return_line_id = value; }
        }
        private int return_header_id;   //外键，主从表的K值

        public int Return_header_id
        {
            get { return return_header_id; }
            set { return_header_id = value; }
        }
        private int line_num;

        public int Line_num
        {
            get { return line_num; }
            set { line_num = value; }
        }
        private string return_wo_no;    //退料工单号

        public string Return_wo_no
        {
            get { return return_wo_no; }
            set { return_wo_no = value; }
        }
        private int operation_seq_num;  //制程

        public int Operation_seq_num
        {
            get { return operation_seq_num; }
            set { operation_seq_num = value; }
        }
        private string item_name;       //料号

        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }
        //private int iqc_qty = 0;        //检验量

        //public int Iqc_qty
        //{
        //    get { return iqc_qty; }
        //    set { iqc_qty = value; }
        //}
        //private DateTime iqc_time = DateTime.Now;   //检验时间

        //public DateTime Iqc_time
        //{
        //    get { return iqc_time; }
        //    set { iqc_time = value; }
        //}
        private int return_qty = 0;     //退料数量

        public int Return_qty
        {
            get { return return_qty; }
            set { return_qty = value; }
        }
        private string uom;                //单位
        public string UOM
        {
            get { return uom; }
            set { uom = value; }
        }
        private string locator;
        public string Locator
        {
            get { return locator; }
            set { locator = value; }
        }
        private int return_sub_key;     //库别K值

        public int Return_sub_key
        {
            get { return return_sub_key; }
            set { return_sub_key = value; }
        }
        private DateTime return_time = DateTime.Now;    //退料时间

        public DateTime Return_time
        {
            get { return return_time; }
            set { return_time = value; }
        }
        //private DateTime create_time = DateTime.Now;    //创建时间

        //public DateTime Create_time
        //{
        //    get { return create_time; }
        //    set { create_time = value; }
        //}
        private DateTime update_time = DateTime.Now;    //修改时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
    }
}