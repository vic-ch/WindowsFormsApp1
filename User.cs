using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;



namespace WindowsFormsApp1
{


    //当前用户
    public static class CurrentUser
    {
        public static string name { set; get; }
        public static int status { get; set; }
    }


    //封装一个 字符串 conStr 作为连接字符串的 name。
    public static class link2db
    {
        public static string constr { set; get; }
    }


}
