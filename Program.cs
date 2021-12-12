using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


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
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Login());
            //
             link2db.constr = "default";
            //Application.Run(new Exec());
            //
            //Application.Run(new Link2DB());
            Application.Run(new AddGoods());
            Console.WriteLine("Hello,World!");


        }
    }
}
