using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model//作者：周雅雯 最后一次修改时间：2016/7/16
{
    public class ModelReinspect_paremeters              //复验参数表（Reinspect_paremeters）对应的Model
    {
        private string lookup_type = "REINSPECT";

        public string Lookup_type
        {
            get { return lookup_type; }
            set { lookup_type = value; }
        }
        private string reinspect_week;                  //复验周期

        public string Reinspect_week
        {
            get { return reinspect_week; }
            set { reinspect_week = value; }
        }
        private DateTime create_time = DateTime.Now;    //创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private DateTime update_time = DateTime.Now;    //更新时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
        private int reinspect_qty;                      //复验数量

        public int Reinspect_qty
        {
            get { return reinspect_qty; }
            set { reinspect_qty = value; }
        }
    }
}