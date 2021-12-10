using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApp1
{
 
    public partial class Login : Form
    {
             


        public Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            SqlConnection conn;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString);//new一个connection对象，并获取App.config文件中的con的connectionString的值作为这个对象的构造函数的参数
            
            string sql = "select 用户名,密码 from userdb where 用户名='"+txb_username.Text+"'and 密码='"+txb_password.Text+"'";//获取textBox中的字符串并组成查询语句

            SqlCommand cmd = new SqlCommand(sql, conn);//new一个Command对象，
            
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();//通过ExecuteReader方法执行cmd命令，并将查询结果返回给reader
                                     //此处也可以仅使用用户名查询到密码并返回密码值与textBox的值比对。

            if (reader.Read())      //如果有返回行，表示在表中查到这个账号并且密码正确
            {
                CurrentUser.name = reader.GetString(0);
                Console.WriteLine(CurrentUser.name);
                CurrentUser.status = 1;
 
                this.Hide();
            }
            else                    //没有返回一行，表示在数据库中没有找到输入的账号密码
            {
                MessageBox.Show("用户名或密码输入错误！", "ERROR",MessageBoxButtons.OK, MessageBoxIcon.Error) ;
            }
            conn.Close();

            Exec exec = new Exec();
            exec.Show();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
    }
}
