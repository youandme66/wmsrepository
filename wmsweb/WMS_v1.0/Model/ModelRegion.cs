using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    /// <summary>
    /// 区域表
    /// </summary>
    public class ModelRegion
    {
        #region Model
        private int region_key;
        /// <summary>
        /// 区域
        /// </summary>
        public int Region_key
        {
            get { return region_key; }
            set { region_key = value; }
        }

        private string region_name;
        /// <summary>
        /// 区域名
        /// </summary>
        public string Region_name
        {
            get { return region_name; }
            set { region_name = value; }
        }

        private int subinventory_key;
        /// <summary>
        /// 库别
        /// </summary>
        public int Subinventory_key
        {
            get { return subinventory_key; }
            set { subinventory_key = value; }
        }

        private int create_by;
        /// <summary>
        /// 创建者
        /// </summary>
        public int Create_by
        {
            get { return create_by; }
            set { create_by = value; }
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

        private int update_by;
        /// <summary>
        /// 更改者
        /// </summary>
        public int Update_by
        {
            get { return update_by; }
            set { update_by = value; }
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
        #endregion

        public string toString()
        {
            return "subinventory_key=" + subinventory_key + ",region_key=" + region_key + ",region_name=" + region_name + ",enabled=" +
                enabled + ",create_time=" + create_time + ",create_by=" + create_by + ",update_time=" + update_time + ",update_by=" + update_by
                + ",description="+description;
        }
    }
}