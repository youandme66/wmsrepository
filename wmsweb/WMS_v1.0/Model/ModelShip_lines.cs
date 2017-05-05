using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model               //作者：周雅雯 最后一次修改时间：2016/9/27
{
    public class ModelShip_lines : Object        //出货明细表（Ship_lines）对应的Model
    {
        private int ship_lines_key;

        public int Ship_lines_key       //出货身表id
        {
            get { return ship_lines_key; }
            set { ship_lines_key = value; }
        }

        private int ship_key;           //出货Key值

        public int Ship_key
        {
            get { return ship_key; }
            set { ship_key = value; }
        }
      

        private string part_no;            //料号

        public string Part_no
        {
            get { return part_no; }
            set { part_no = value; }
        }

        private string wo_no;           //工单编号

        public string Wo_no
        {
            get { return wo_no; }
            set { wo_no = value; }
        }

        //private int request_qty;        //需求量

        //public int Request_qty
        //{
        //    get { return request_qty; }
        //    set { request_qty = value; }
        //}

        private int picked_qty;         //出货量

        public int Picked_qty
        {
            get { return picked_qty; }
            set { picked_qty = value; }
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