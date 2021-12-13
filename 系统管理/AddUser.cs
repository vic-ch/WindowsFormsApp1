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

/**
 * 2021-12-09
 *实现了修改密码功能
 *实现了添加用户功能
 *
 **/


namespace WindowsFormsApp1
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
            {
                MessageBox.Show("用户名或密码不能为空！");
            }
            else
            {
                if (textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show("密码不一致！请重新输入", "error");
                    return;
                }
                else
                {
                    #region 

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                    {
                        conn.Open();
                        string sql = "SELECT 用户名 FROM USERDB WHERE 用户名='" + textBox1.Text + "'";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    MessageBox.Show("已存在用户名为“" + textBox1.Text + "”的用户");
                                }
                                else
                                {
                                    string sql1 = "INSERT INTO USERDB (用户名, 密码)VALUES('" + textBox1.Text + "','" + textBox2.Text + "')";
                                    string sql2 = "SELECT 用户名 FROM USERDB WHERE 用户名='" + textBox1.Text + "'";
                                    using (SqlCommand cmd1 = new SqlCommand(sql1, conn))
                                    {
                                        if (cmd1.ExecuteNonQuery() == 1)
                                        {
                                            using (SqlCommand cmd2 = new SqlCommand(sql2, conn))
                                            {
                                                using(SqlDataReader reader2 = cmd2.ExecuteReader())
                                                {
                                                    if (reader2.Read())
                                                    {
                                                        if (textBox1.Text == reader2.GetString(0))
                                                        {
                                                            MessageBox.Show("添加成功！");
                                                            this.Close();
                                                        }
                                                    }

                                                    else
                                                    {
                                                        MessageBox.Show("添加失败！");
                                                    }
                                                }
                                            }
                                           
                                        }
                                        else
                                        {
                                            MessageBox.Show("添加失败！");
                                        }
                                    }
                                        
                                }
                            }

                        }


                    }


                    #endregion 
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
