using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    public class ModelSubinventory
    {
        #region Model
        private int subinventory_key;
        /// <summary>
        /// 库别KEY值
        /// </summary>
        public int Subinventory_key
        {
            get { return subinventory_key; }
            set { subinventory_key = value; }
        }

        private string subinventory_name;
        /// <summary>
        /// 库别名称
        /// </summary>
        public string Subinventory_name
        {
            get { return subinventory_name; }
            set { subinventory_name = value; }
        }

        private string enabled;
        /// <summary>
        /// 是否可用
        /// </summary>
        public string Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private string description;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private DateTime create_time;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }

        private string create_by;
        /// <summary>
        /// 创建人员
        /// </summary>
        public string Create_by
        {
            get { return create_by; }
            set { create_by = value; }
        }

        private DateTime update_time;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }

        private string update_by;
        /// <summary>
        /// 更新人员
        /// </summary>
        public string Update_by
        {
            get { return update_by; }
            set { update_by = value; }
        }
        #endregion

        public string toString()
        {
            return "subinventory_key=" + subinventory_key + ",Subinventory_name=" + Subinventory_name + ",description=" + description + ",enabled=" +
                enabled + ",create_time=" + create_time + ",create_by=" + create_by + ",update_time=" + update_time + ",update_by=" + update_by;
        }
    }
}