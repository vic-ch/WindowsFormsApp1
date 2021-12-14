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
        private static string thisday;
        private static string thismonth;
        private static string thisyear;
        private static string thisseason;
        private static string thisday_group;
        private static string thismonth_group;
        private static string thisseason_group;
        private static string thisyear_group;
        public static string Thisday
        {
            get => thisday;
            set => thisday = 
                "SELECT *" +
                "FROM " + value + " AS A " +
                "WHERE (SELECT " + (value == "sell" ? "销售" : "进货") + "年 FROM  " + value + "  WHERE 商品编号=A.商品编号)=(SELECT DATEPART(YY, GETDATE())) " +
                "AND(SELECT "+(value=="sell"?"销售":"进货")+"月 FROM  " + value + "  WHERE 商品编号 = A.商品编号)=(SELECT DATEPART(MM, GETDATE()) ) " +
                "AND(SELECT  " + (value == "sell" ? "销售" : "进货") + "日 FROM  " + value + "  WHERE 商品编号 = A.商品编号)=(SELECT DATEPART(DD, GETDATE()))";
        }
        public static string Thismonth 
        {
            get => thismonth;
            set => thismonth =
                "SELECT *" +
                "FROM " + value+" AS A " +
                "WHERE (SELECT " + (value == "sell" ? "销售" : "进货") + "年 FROM  " + value + "  WHERE 商品编号=A.商品编号)=(SELECT DATEPART(YY, GETDATE())) " +
                "AND(SELECT " + (value == "sell" ? "销售" : "进货") + "月 FROM  " + value + "  WHERE 商品编号 = A.商品编号)=(SELECT DATEPART(MM, GETDATE()) ) "; 
        }
        public static string Thisyear 
        {
            get => thisyear; 
            set => thisyear =
                "SELECT *" +
                "FROM "+ value+" AS A " +
                "WHERE (SELECT " + (value == "sell" ? "销售" : "进货") + "年 FROM  " + value + "  WHERE 商品编号=A.商品编号)=(SELECT DATEPART(YY, GETDATE()))"; 
        }
        public static string Thisseason 
        {
            get => thisseason;
            set => thisseason = 
                "SELECT * " +
                "FROM "+value+" AS A " +
                "WHERE (SELECT " + (value == "sell" ? "销售" : "进货") + "年 FROM  " + value + "  WHERE 商品编号=A.商品编号)=(SELECT DATEPART(YY, GETDATE())) " +
                "AND(SELECT DATEPART(QQ, CONCAT((SELECT " + (value == "sell" ? "销售" : "进货") + "年 FROM  " + value + "  WHERE 商品编号 = A.商品编号),'-',(SELECT  " + (value == "sell" ? "销售" : "进货") + "月 FROM  " + value + "  WHERE 商品编号=A.商品编号),'-',(SELECT  " + (value == "sell" ? "销售" : "进货") + "日 FROM  " + value + "  WHERE 商品编号=A.商品编号))))  = (SELECT DATEPART(QQ, GETDATE()))";
        }
        public static string Thisday_group 
        { 
            get => thisday_group; 
            set => thisday_group = 
                "SELECT A.生产厂商 AS 生产厂商,SUM(A.总金额) AS 总金额 " +
                "FROM "+value+ " AS A WHERE (SELECT " + (value == "sell" ? "销售" : "进货") + "年 FROM  " + value + "  WHERE 商品编号=A.商品编号)=(SELECT DATEPART(YY, GETDATE())) " +
                "AND(SELECT " + (value == "sell" ? "销售" : "进货") + "月 FROM  " + value + "  WHERE 商品编号 = A.商品编号)=(SELECT DATEPART(MM, GETDATE()) ) " +
                "AND(SELECT " + (value == "sell" ? "销售" : "进货") + "日 FROM  " + value + "  WHERE 商品编号 = A.商品编号)=(SELECT DATEPART(DD, GETDATE())) " +
                "GROUP BY 生产厂商"; 
        }
        public static string Thismonth_group 
        {
            get => thismonth_group;
            set => thismonth_group =
                "SELECT A.生产厂商 AS 生产厂商,SUM(A.总金额) AS 总金额 " +
                "FROM "+value+" AS A " +
                "WHERE(SELECT " + (value == "sell" ? "销售" : "进货") + "年 FROM  " + value + "  WHERE 商品编号 = A.商品编号)=(SELECT DATEPART(YY, GETDATE())) " +
                "AND(SELECT " + (value == "sell" ? "销售" : "进货") + "月 FROM  " + value + "  WHERE 商品编号 = A.商品编号)=(SELECT DATEPART(MM, GETDATE()) ) " +
                "GROUP BY 生产厂商"; 
        }
        public static string Thisseason_group 
        { 
            get => thisseason_group;
            set => thisseason_group =
                "SELECT A.生产厂商,SUM(A.总金额) AS 总金额 FROM " + value + " AS A WHERE(SELECT " + (value == "sell" ? "销售" : "进货") + "年 " +
                "FROM  " + value + "  WHERE 商品编号 = A.商品编号)=(SELECT DATEPART(YY, GETDATE()))    " +
                "AND(SELECT DATEPART(QQ, CONCAT((SELECT  " + (value == "sell" ? "销售" : "进货") + "年 FROM  " + value + "  WHERE 商品编号 = A.商品编号),'-',(SELECT  " + (value == "sell" ? "销售" : "进货") + "月 FROM  " + value + "  WHERE 商品编号=A.商品编号),'-',(SELECT  " + (value == "sell" ? "销售" : "进货") + "日 FROM  " + value + "  WHERE 商品编号=A.商品编号))))  = (SELECT DATEPART(QQ, GETDATE()))" +
                "GROUP BY 生产厂商";
                }
        public static string Thisyear_group 
        {
            get => thisyear_group;
            set => thisyear_group =
                "SELECT A.生产厂商 AS 生产厂商 ,SUM(A.总金额) AS 总金额 " +
                "FROM "+value+" AS A " +
                "WHERE (SELECT " + (value == "sell" ? "销售" : "进货") + "年 FROM  " + value + "  WHERE 商品编号=A.商品编号)=(SELECT DATEPART(YY, GETDATE())) " +
                "GROUP BY 生产厂商";
        }
    }
    //显示进货、销售信息共用这个类
    //记录查询进货销售信息的视图 的SQL语句 
}
