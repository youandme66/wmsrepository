using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model//参数表(wms_parameters）对应model
{
    public class ModelParameters//作者：周雅雯 最后一次修改时间：2016/8/2
    {
        private int lookup_type;        //数据表名

        public int Lookup_type
        {
            get { return lookup_type; }
            set { lookup_type = value; }
        }
        private string lookup_code;     //对应栏位

        public string Lookup_code
        {
            get { return lookup_code; }
            set { lookup_code = value; }
        }
        private string meaning;         //数据字段

        public string Meaning
        {
            get { return meaning; }
            set { meaning = value; }
        }
        private string description;     //数据描述

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string enabled;         //是否可用

        public string Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
        private int create_by;          //创建者

        public int Create_by
        {
            get { return create_by; }
            set { create_by = value; }
        }
        private DateTime create_time;   //创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private int update_by;          //更改者

        public int Update_by
        {
            get { return update_by; }
            set { update_by = value; }
        }
        private DateTime update_time;   //更新时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
    }
}