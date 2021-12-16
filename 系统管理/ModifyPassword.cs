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


namespace WindowsFormsApp1.系统管理
{
    public partial class ModifyPassword : Form
    {
        public ModifyPassword()
        {
            InitializeComponent();
        }

        private void ModifyPassword_Load(object sender, EventArgs e)
        {
            if (CurrentUser.status == 1)
            {
                textBox1.Text = CurrentUser.name;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text.Length == 0|| textBox3.Text.Length == 0|| textBox4.Text.Length == 0)
            {
                MessageBox.Show("密码不能为空!");
                return;
            }
            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("密码不一致!");
                return;
            }
            if (textBox2.Text == textBox3.Text)
            {
                MessageBox.Show("密码与修改前相同或当前密码输入错误!");
                return;
            }
            //Console.WriteLine();

            #region 
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT 密码 FROM USERDB WHERE 用户名=@name";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 10));
                        cmd.Parameters["@name"].Value = CurrentUser.name;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //检查原来的密码是否正确
                                if (textBox2.Text == reader.GetString(0))
                                {
                                    string sql1 = "UPDATE USERDB SET 密码=@pwd WHERE 用户名=@name";
                                    string sql2 = "SELECT 密码 FROM USERDB WHERE 用户名=@name";
                                    using (SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                                    {
                                        conn2.Open();

                                        using (SqlCommand cmd1 = new SqlCommand(sql1, conn2))
                                        {

                                            cmd1.Parameters.Add(new SqlParameter("@name",SqlDbType.NVarChar, 10));
                                            cmd1.Parameters.Add(new SqlParameter("@id",SqlDbType.NVarChar, 10));
                                            cmd1.Parameters["@name"].Value=CurrentUser.name;
                                            cmd1.Parameters["@pwd"].Value=textBox3.Text;
                                            if (cmd1.ExecuteNonQuery() == 1)
                                            {

                                                //必须建立另一个sqlconnection给sqldatareader而不能再使用上面的sqlconnection,否则会提示close sqldatareader
                                                using (SqlConnection conn3 = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                                                {
                                                    conn3.Open();

                                                    using (SqlCommand cmd2 = new SqlCommand(sql2, conn3))
                                                    {
                                                        cmd2.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 10));
                                                        cmd2.Parameters["@name"].Value = textBox3.Text;
                                                        using (SqlDataReader reader2 = cmd2.ExecuteReader())
                                                        {
                                                            if (reader2.Read())
                                                            {
                                                                if (textBox3.Text == reader2.GetString(0))
                                                                {
                                                                    MessageBox.Show("修改成功！下次登录时生效");

                                                                    CurrentUser.status = 0;
                                                                    this.Close();


                                                                    //TODO
                                                                    //设置修改密码后注销登录状态。
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("修改失败！");
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("修改失败！");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("密码错误！", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("请检查当前登录状态", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
