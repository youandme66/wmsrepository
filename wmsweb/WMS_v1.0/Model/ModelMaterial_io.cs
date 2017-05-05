using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model               //作者：周雅雯 最后一次修改时间：2016/8/2
{
    public class ModelMaterial_io       //在手数量明细表（Material_io）对应的Model
    {
        private int unique_id;          //自增id

        public int Unique_id
        {
            get { return unique_id; }
            set { unique_id = value; }
        }

        private int item_id;            //外键，与PN表串的K值

        public int Item_id
        {
            get { return item_id; }
            set { item_id = value; }
        }
        private int frame_key;          //料架K值

        public int Frame_key
        {
            get { return frame_key; }
            set { frame_key = value; }
        }

        private string subinventory;           //库别

        public string Subinventory
        {
            get { return subinventory; }
            set { subinventory = value; }
        }

        private string datecode;      //生产日期

        public string Datecode
        {
            get { return datecode; }
            set { datecode = value; }
        }
        private int onhand_qty;         //在手量

        public int Onhand_qty
        {
            get { return onhand_qty; }
            set { onhand_qty = value; }
        }
        private int simulated_qty = 0;   //模拟量

        public int Simulated_qty
        {
            get { return simulated_qty; }
            set { simulated_qty = value; }
        }

        private int left_qty = 0;       //剩余量

        public int Left_qty
        {
            get { return left_qty; }
            set { left_qty = value; }
        }

        private string return_flag;     //退料标志

        public string Return_flag
        {
            get { return return_flag; }
            set { return_flag = value; }
        }
        private DateTime last_reinspect_time;

        public DateTime Last_reinspect_time
        {
            get { return last_reinspect_time; }
            set { last_reinspect_time = value; }
        }
        private string last_reinspect_status = "PASS";

        public string Last_reinspect_status
        {
            get { return last_reinspect_status; }
            set { last_reinspect_status = value; }
        }
        private DateTime create_time = DateTime.Now;   //创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private DateTime update_time = DateTime.Now;   //更新时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }

    }
}