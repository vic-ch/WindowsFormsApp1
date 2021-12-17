using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{


    public static class Views
    {
        //为什么要封装
        //public修饰的属性，不够安全；private修饰的属性，无法使用
        //所以，用到了封装：封装就是 隐藏对象的信息，但要流出访问的接口

        private const string add_thisday="SELECT * FROM ADD_THISDAY ";
        private const string add_thismonth="SELECT * FROM ADD_THISMONTH ";
        private const string add_thisseason = "SELECT * FROM ADD_THISSEASON ";
        private const string add_thisyear="SELECT * FROM ADD_THISYEAR ";
        private const string add_thisday_group="SELECT * FROM ADD_THISDAY_GROUP ";
        private const string add_thismonth_group="SELECT * FROM ADD_THISMONTH_GROUP ";
        private const string add_thisseason_group="SELECT * FROM ADD_THISSEASON_GROUP ";
        private const string add_thisyear_group="SELECT * FROM ADD_THISYEAR_GROUP ";

        private const string sell_thisday="SELECT * FROM SELL_THISDAY ";
        private const string sell_thismonth="SELECT * FROM SELL_THISMONTH ";
        private const string sell_thisseason = "SELECT * FROM SELL_THISSEASON ";
        private const string sell_thisyear="SELECT * FROM SELL_THISYEAR ";
        private const string sell_thisday_group="SELECT * FROM SELL_THISDAY_GROUP ";
        private const string sell_thismonth_group="SELECT * FROM SELL_THISMONTH_GROUP ";
        private const string sell_thisseason_group="SELECT * FROM SELL_THISSEASON_GROUP ";
        private const string sell_thisyear_group="SELECT * FROM SELL_THISYEAR_GROUP ";


        private const string query_employeesell = "SELECT * FROM QUERY_EMPLOYEESELL";
        private const string goodsreamin = "SELECT * FROM GOODSREMAIN";

        private const string sell = "SELECT *FROM SELL";
        private const string retreat = "SELECT *FROM RETREAT";
        private const string addgoods = "SELECT *FROM ADDGOODS";
        private const string employee = "SELECT *FROM EMPLOYEE";
        private const string manufacturer = "SELECT *FROM MANUFACTURER";
        public static string Add_thisday => add_thisday;

        public static string Add_thismonth => add_thismonth;

        public static string Add_thisseason => add_thisseason;

        public static string Add_thisyear => add_thisyear;

        public static string Add_thisday_group => add_thisday_group;

        public static string Add_thismonth_group => add_thismonth_group;

        public static string Add_thisseason_group => add_thisseason_group;

        public static string Add_thisyear_group => add_thisyear_group;

        public static string Sell_thisday => sell_thisday;

        public static string Sell_thismonth => sell_thismonth;

        public static string Sell_thisseason => sell_thisseason;

        public static string Sell_thisyear => sell_thisyear;

        public static string Sell_thisday_group => sell_thisday_group;

        public static string Sell_thismonth_group => sell_thismonth_group;

        public static string Sell_thisseason_group => sell_thisseason_group;

        public static string Sell_thisyear_group => sell_thisyear_group;

        public static string Query_employeesell => query_employeesell;

        public static string Goodsreamin => goodsreamin;

        public static string Sell => sell;

        public static string Retreat => retreat;

        public static string Addgoods => addgoods;

        public static string Employee => employee;

        public static string Manufacturer => manufacturer;
    }
    //显示进货、销售信息共用这个类
    //记录查询进货销售信息的视图 的SQL语句 
}
