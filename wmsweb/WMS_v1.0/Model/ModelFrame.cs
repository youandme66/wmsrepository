using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    /// <summary>
    /// 料架表
    /// </summary>
    public class ModelFrame
    {
        #region Model
        private int frame_key;
        /// <summary>
        /// 料架
        /// </summary>
        public int Frame_key
        {
            get { return frame_key; }
            set { frame_key = value; }
        }

        private string frame_name;
        /// <summary>
        /// 料架名
        /// </summary>
        public string Frame_name
        {
            get { return frame_name; }
            set { frame_name = value; }
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

        private int subinventory_key;
        /// <summary>
        /// 库别
        /// </summary>
        public int Subinventory_key
        {
            get { return subinventory_key; }
            set { subinventory_key = value; }
        }

        private int region_key;
        /// <summary>
        /// 区域
        /// </summary>
        public int Region_key
        {
            get { return region_key; }
            set { region_key = value; }
        }
        #endregion

        public string toString()
        {
            return "subinventory_key=" + subinventory_key + ",region_key=" + region_key + ",frame_key=" + frame_key + ",frame_name=" + frame_name + ",enabled=" +
                enabled + ",create_time=" + create_time + ",create_by=" + create_by + ",update_time=" + update_time + ",update_by=" + update_by
                + ",description=" + description;
        }
    }
}