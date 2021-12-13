﻿using System;
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

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
            {
                conn.Open();
                string sql = "SELECT 密码 FROM USERDB WHERE 用户名='" + CurrentUser.name + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //检查原来的密码是否正确
                            if (textBox2.Text == reader.GetString(0))
                            {
                                string sql1 = "UPDATE USERDB SET 密码='" + textBox3.Text + "' WHERE 用户名='" + CurrentUser.name + "'";
                                string sql2 = "SELECT 密码 FROM USERDB WHERE 用户名='" + CurrentUser.name + "'";

                                using (SqlCommand cmd1 = new SqlCommand(sql1, conn))
                                {
                                    if (cmd1.ExecuteNonQuery() == 1)
                                    {
                                        using (SqlCommand cmd2 = new SqlCommand(sql2, conn))
                                        {
                                            using (SqlDataReader reader2 = cmd2.ExecuteReader())
                                            {
                                                if (reader2.Read())
                                                {
                                                    if (textBox3.Text == reader2.GetString(0))
                                                    {
                                                        MessageBox.Show("修改成功！");

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
                                    else
                                    {
                                        MessageBox.Show("修改失败！");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("密码错误！","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("请检查当前登录状态", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }


            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
