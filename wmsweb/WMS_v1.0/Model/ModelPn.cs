using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model       //作者：周雅雯 最后一次修改时间：2016/8/2
{
    public class ModelPn                            //料号表（Pn）对应的Model
    {
        private int item_id;                        //主键，料号ID

        public int Item_id
        {
            get { return item_id; }
            set { item_id = value; }
        }
        private string item_name;                   //料号

        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }

        private string item_desc;                   //料号描述

        public string Item_desc
        {
            get { return item_desc; }
            set { item_desc = value; }
        }

        private string uom;                         //单位

        public string Uom
        {
            get { return uom; }
            set { uom = value; }
        }


        private DateTime create_time = DateTime.Now;//创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }

        private string create_user;                 //创建人员

        public string Create_user
        {
            get { return create_user; }
            set { create_user = value; }
        }
        private DateTime update_time = DateTime.Now;//更新时间

        private string update_user;                 //更新人员

        public string Update_user
        {
            get { return update_user; }
            set { update_user = value; }
        }

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
        
    }
}