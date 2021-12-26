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


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //重载窗口关闭事件。

        private void btn_login_Click(object sender, EventArgs e)
        {
            //new一个connection对象，并获取App.config文件中的con的connectionString的值作为这个对象的构造函数的参数
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    conn.Open();
                    string sql = "select 用户名,密码 from userdb where 用户名=@name and 密码=@pwd";
                    //获取textBox中的字符串并组成查询语句

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 20));
                        cmd.Parameters.Add(new SqlParameter("@pwd", SqlDbType.NVarChar, 20));

                        cmd.Parameters["@name"].Value = txb_username.Text;
                        cmd.Parameters["@pwd"].Value = txb_password.Text;


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            //通过ExecuteReader方法执行cmd命令，并将查询结果返回给reader
                            //此处也可以仅使用用户名查询到密码并返回密码值与textBox的值比对。

                            if (reader.Read())      //如果有返回行，表示在表中查到这个账号并且密码正确
                            {
                                CurrentUser.name = reader.GetString(0);
                                Console.WriteLine(CurrentUser.name);
                                CurrentUser.status = 1;

                                //进入exec界面
                                this.Hide();
                                Exec exec = new Exec();
                                exec.Show();
                            }
                            else                    //没有返回一行，表示在数据库中没有找到输入的账号密码
                            {
                                MessageBox.Show("用户名或密码输入错误！", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

            //测试
            txb_password.Text = "yy";
            txb_username.Text = "yy";
        }


    }
}
