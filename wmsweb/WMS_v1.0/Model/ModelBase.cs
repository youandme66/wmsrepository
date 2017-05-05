using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WMS_v1._0.Model//作者：周雅雯 最后一次修改时间：2016/7/20  （还未完善）
{
    public class ModelBase :Object   //所有Model的父类
    {
        public static string toJson(object jsonObject)      //对象转换为json（还未实现）
        {
            string jsonString = "";
            return jsonString;
        }

        public static string toJson(IEnumerable array)      //对象集合转化为json（还未实现）
        {
            string jsonString = "";
            return jsonString;
        }

        public static string ToJson(string value)           // String转换为Json     
        {              
            if (string.IsNullOrEmpty(value))             
            {                 
                 return string.Empty;            
            }               
            string temstr;             
            temstr = value;              
            temstr = temstr.Replace("{", "｛").Replace("}", "｝").Replace(":", "：").Replace(",", "，").Replace("[", "【").Replace("]", "】").Replace(";", "；").Replace("\n", "<br/>").Replace("\r", "");               temstr = temstr.Replace("\t", "   ");
            temstr = temstr.Replace("'", "\'");
            temstr = temstr.Replace(@"\", @"\\");
            temstr = temstr.Replace("\"", "\"\""); 
            return temstr;
         }

        private static string DeleteLast(string str)        // 删除结尾字符
        { 
            if (str.Length > 1)
            {
                return str.Substring(0, str.Length - 1); 
            } 
            return str; 
        }

    }
}