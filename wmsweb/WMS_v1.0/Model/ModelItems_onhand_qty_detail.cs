using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model //作者：周雅雯 最后一次修改时间：2016/8/2
{
    public class ModelItems_onhand_qty_detail   //在手总量表（Items_onhand_qty_detail）对应的Model
    {
        private string item_name;               //料号

        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }
        private int item_id;                    //外键，与PN表串的K值

        public int Item_id
        {
            get { return item_id; }
            set { item_id = value; }
        }
        private int onhand_quantiy;             //在手总量

        public int Onhand_quantiy
        {
            get { return onhand_quantiy; }
            set { onhand_quantiy = value; }
        }
        private string subinventory;           //库别

        public string Subinventory
        {
            get { return subinventory; }
            set { subinventory = value; }
        }
        private DateTime create_time = DateTime.Now;           //创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private DateTime update_time = DateTime.Now;           //更新时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }

    }
}