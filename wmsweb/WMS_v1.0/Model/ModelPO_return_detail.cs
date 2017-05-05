using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model//PO退回明细表(wms_po_return_detail）对应model
{
    public class ModelPO_return_detail//作者：周雅雯 最后一次修改时间：2016/8/2
    {
        private int return_header_id;

        public int Return_header_id
        {
            get { return return_header_id; }
            set { return_header_id = value; }
        }
        private int return_line_id;

        public int Return_line_id
        {
            get { return return_line_id; }
            set { return_line_id = value; }
        }
        private int line_num;       //个数

        public int Line_num
        {
            get { return line_num; }
            set { line_num = value; }
        }
        private string po_no;       //采购单号

        public string Po_no
        {
            get { return po_no; }
            set { po_no = value; }
        }
        private string item_name;   //料号

        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }
        private int item_desc;      //料号备注

        public int Item_desc
        {
            get { return item_desc; }
            set { item_desc = value; }
        }
        private string uom;         //单位

        public string Uom
        {
            get { return uom; }
            set { uom = value; }
        }
        private int required_qty;   //退回数量

        public int Required_qty
        {
            get { return required_qty; }
            set { required_qty = value; }
        }
        private string return_sub;  //退回储位

        public string Return_sub
        {
            get { return return_sub; }
            set { return_sub = value; }
        }
        private DateTime return_time;//退回时间

        public DateTime Return_time
        {
            get { return return_time; }
            set { return_time = value; }
        }
        private string return_wo_no; //退回人员ID

        public string Return_wo_no
        {
            get { return return_wo_no; }
            set { return_wo_no = value; }
        }
        private DateTime update_time;//更新时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
        private string update_wo_no; //更新人员单号

        public string Update_wo_no
        {
            get { return update_wo_no; }
            set { update_wo_no = value; }
        }
    }
}