using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    public class ModelDepartment
    {
        private int department_id;

        public int Department_id
        {
            get { return department_id; }
            set { department_id = value; }
        }

        private string flex_value;

        public string Flex_value
        {
            get { return flex_value; }
            set { flex_value = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

      

        private string enabled;

        public string Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private DateTime create_time;

        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }

        private DateTime update_time;

        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }

        private string create_user;

        public string Create_user
        {
            get { return create_user; }
            set { create_user = value; }
        }

        private string update_user;

        public string Update_user
        {
            get { return update_user; }
            set { update_user = value; }
        }
    }
}