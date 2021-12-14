using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.进货统计;
using WindowsFormsApp1.销售统计;

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
             link2db.constr = "default";
            //Application.Run(new Exec());
            //
            //Application.Run(new Link2DB());
            //Application.Run(new AddGoods());
            //.Application.Run(new SellGoods());
            //Application.Run(new RetreatGoods());
            //Application.Run(new Buycount());
            Application.Run(new Sellcount());

            Console.WriteLine("Hello,World!");


        }
    }
}
