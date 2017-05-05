using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    public class ModelPrograms
    {
        #region Model
        private int user_id;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }

        private int program_id;
        /// <summary>
        /// 进程ID
        /// </summary>
        public int Program_id
        {
            get { return program_id; }
            set { program_id = value; }
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

        private int create_by;
        /// <summary>
        /// 制造者
        /// </summary>
        public int Create_by
        {
            get { return create_by; }
            set { create_by = value; }
        }

        private DateTime create_time = DateTime.Now;
        /// <summary>
        /// 制造时间
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

        private DateTime update_time = DateTime.Now;
        /// <summary>
        /// 更改时间
        /// </summary>
        public DateTime Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
        #endregion

        public string toString()
        {
            return "user_id=" + user_id + ",program_id=" + program_id + "enabled=" + enabled + ",create_by=" + create_by + ",create_time=" + create_time +
                ",update_by=" + update_by + ",update_time=" + update_time;
        }
    } 
}