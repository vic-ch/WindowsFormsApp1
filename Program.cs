using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.系统管理;
using WindowsFormsApp1.进货统计;
using WindowsFormsApp1.销售统计;
using WindowsFormsApp1.业绩查看;
using WindowsFormsApp1.查看数据表;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Login());
            //
             //link2db.constr = "default";
            //Application.Run(new Exec());
            //
            Application.Run(new Link2DB());
            //Application.Run(new AddGoods());
            //Application.Run(new SellGoods());
            //Application.Run(new RetreatGoods());
            //Application.Run(new Buycount());
            //Application.Run(new Sellcount());
            //Application.Run(new Showsell());
            //Application.Run(new Checkremain());
            //Application.Run(new Checksold());
            //Application.Run(new Checkaddgoods());
            //Application.Run(new Checkemployee());
            //Application.Run(new Checkemployee());
            //Application.Run(new Checkmanufacturer());

            Console.WriteLine("Hello,World!");


        }
    }
}
