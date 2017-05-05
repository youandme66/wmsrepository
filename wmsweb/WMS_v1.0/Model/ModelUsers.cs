using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS_v1._0.Model
{
    public class ModelUsers
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

        private string user_name;
        /// <summary>
        /// 用户名
        /// </summary>
        public string User_name
        {
            get { return user_name; }
            set { user_name = value; }
        }

        private string password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
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

        private string enabled;
        /// <summary>
        /// 是否可用
        /// </summary>
        public string Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private DateTime create_time = DateTime.Now;
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
        /// 创建者
        /// </summary>
        public string Create_by
        {
            get { return create_by; }
            set { create_by = value; }
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

        private string update_by;
        /// <summary>
        /// 更改者
        /// </summary>
        public string Update_by
        {
            get { return update_by; }
            set { update_by = value; }
        }

        private string dept_no;
        /// <summary>
        /// 部门
        /// </summary>
        public string Dept_no
        {
            get { return dept_no; }
            set { dept_no = value; }
        }
        #endregion

        public string toString()
        {
            return "user_id=" + user_id + ",user_name=" + user_name + ",password=" + password + ",description="
                + description + ",enabled=" + enabled + ",create_time=" + create_time + ",create_by=" + create_by+",update_time=" + 
                update_time + ",update_by=" + update_by+",dept_no="+dept_no;
        }
    }
}