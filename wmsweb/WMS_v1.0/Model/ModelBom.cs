using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model//bom表(wms_requirement_operation）对应model
{
    public class ModelBom//作者：周雅雯 最后一次修改时间：2016/8/9
    {
        private int requirement_line_id;

        public int Requirement_line_id
        {
            get { return requirement_line_id; }
            set { requirement_line_id = value; }
        }
        private string wo_no;       //工单编号

        public string Wo_no
        {
            get { return wo_no; }
            set { wo_no = value; }
        }
        private string item_name;   //料号

        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }
        private int operation_seq_num;//制程

        public int Operation_seq_num
        {
            get { return operation_seq_num; }
            set { operation_seq_num = value; }
        }
        private int required_qty;     //需要用量

        public int Required_qty
        {
            get { return required_qty; }
            set { required_qty = value; }
        }
        private DateTime create_time; //创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private DateTime update_time; //更新时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
    }
}