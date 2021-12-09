using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;



namespace WindowsFormsApp1
{
    public static class CurrentUser
    {
        public static string name { set; get; }
        public static int status { get; set; }
    }



}
