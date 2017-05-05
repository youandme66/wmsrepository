using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model//作者：周雅雯 最后一次修改时间：2016/7/18
{
    public class ModelWip_operations//制程表（wms_wip_operations）对应的Model
    {
        private int department_idorg_id;//制程编号

        public int Department_idorg_id
        {
            get { return department_idorg_id; }
            set { department_idorg_id = value; }
        }
        private string description;//制程说明

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int create_by;//创建者

        public int Create_by
        {
            get { return create_by; }
            set { create_by = value; }
        }

        private DateTime create_time;//创建时间

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private int update_by;//更改者

        public int Update_by
        {
            get { return update_by; }
            set { update_by = value; }
        }
        private DateTime update_time;//更新时间

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
    }
}