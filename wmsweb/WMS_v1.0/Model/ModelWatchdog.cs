using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    public class ModelWatchdog
    {
        #region Model

        private int id;
        /// <summary>
        ///id
        /// </summary>
        /// 
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int user_id;
        /// <summary>
        /// 用户id
        /// </summary>
        /// 
        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }
        private string user_name;

        public string User_name
        {
            get { return user_name; }
            set { user_name = value; }
        }

        private int program_id;
        /// <summary>
        /// 界面ID
        /// </summary>
        public int Program_id
        {
            get { return program_id; }
            set { program_id = value; }
        }

        private string program_name;
        /// <summary>
        /// 界面名称
        /// </summary>
        public string Program_name
        {
            get { return program_name; }
            set { program_name = value; }
        }

        private string description;
        /// <summary>
        /// 界面描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
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

        private string version;
        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get { return version; }
            set { version = value; }
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
    }
}