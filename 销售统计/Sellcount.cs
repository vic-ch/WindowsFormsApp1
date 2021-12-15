using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using WindowsFormsApp1;

namespace WindowsFormsApp1.销售统计
{
    public partial class Sellcount : Form
    {
        public Sellcount()
        {
            InitializeComponent();
        }


        DataSet ds = new DataSet();

        private void Buycount_Load(object sender, EventArgs e)
        {
            showtag1();
            showtag2();
            showtag3();
            showtag4();
        }

        private void showtag1()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Views.Sell_thisday, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "销售统计1");
                            dataGridView1.DataSource = ds.Tables["销售统计1"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("in tag 1 :" + ex.Message);
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Views.Sell_thisday_group, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "销售统计汇总1");
                            dataGridView2.DataSource = ds.Tables["销售统计汇总1"];
                            int c = 0;
                            while (1 + count != 0)
                            {
                                c += Convert.ToInt32(dataGridView2.Rows[count].Cells[1].Value);
                                textBox1.Text = c.ToString("f2");
                                count--;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("in tag 1 :" + ex.Message);
            }

        }
        private void showtag2()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Views.Sell_thismonth, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "销售统计2");
                            dataGridView4.DataSource = ds.Tables["销售统计2"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("in tag 2 :" + ex.Message);
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Views.Sell_thismonth_group, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "销售统计汇总2");
                            dataGridView3.DataSource = ds.Tables["销售统计汇总2"];
                            int c = 0;
                            while (1 + count != 0)
                            {
                                c += Convert.ToInt32(dataGridView3.Rows[count].Cells[1].Value);
                                textBox2.Text = c.ToString("f2");
                                count--;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("in tag 2 :" + ex.Message);
            }


        }
        private void showtag3()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Views.Sell_thisseason, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "销售统计3");
                            dataGridView6.DataSource = ds.Tables["销售统计3"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("in tag 3 :" + ex.Message);
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Views.Sell_thisseason_group, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "销售统计汇总3");
                            dataGridView5.DataSource = ds.Tables["销售统计汇总3"];
                            int c = 0;
                            while (1 + count != 0)
                            {
                                c += Convert.ToInt32(dataGridView5.Rows[count].Cells[1].Value);
                                textBox3.Text = c.ToString("f2");
                                count--;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("in tag 3 :" + ex.Message);
            }


        }
        private void showtag4()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Views.Sell_thisyear, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "销售统计4");
                            dataGridView8.DataSource = ds.Tables["销售统计4"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("in tag 4 :" + ex.Message);
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Views.Sell_thisyear_group, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "销售统计汇总");
                            dataGridView7.DataSource = ds.Tables["销售统计汇总"];
                            int c = 0;
                            while (1 + count != 0)
                            {
                                c += Convert.ToInt32(dataGridView7.Rows[count].Cells[1].Value);
                                textBox4.Text = c.ToString("f2");
                                count--;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("in tag 4 :" + ex.Message);
            }

        }






        private void Buycount_Resize(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(this.Size.Width - 60, dataGridView1.Size.Height);
            dataGridView2.Size = new Size(this.Size.Width - 60, dataGridView2.Size.Height);
            dataGridView3.Size = new Size(this.Size.Width - 60, dataGridView3.Size.Height);
            dataGridView4.Size = new Size(this.Size.Width - 60, dataGridView4.Size.Height);
            dataGridView5.Size = new Size(this.Size.Width - 60, dataGridView5.Size.Height);
            dataGridView6.Size = new Size(this.Size.Width - 60, dataGridView6.Size.Height);
            dataGridView7.Size = new Size(this.Size.Width - 60, dataGridView7.Size.Height);
            dataGridView8.Size = new Size(this.Size.Width - 60, dataGridView8.Size.Height);
            tabControl1.Size = new Size(this.Size.Width - 60, tabControl1.Size.Height);
            tabPage1.Size = new Size(this.Size.Width -60,tabPage1.Size.Height);
            tabPage2.Size = new Size(this.Size.Width -60,tabPage2.Size.Height);
            tabPage3.Size = new Size(this.Size.Width -60,tabPage3.Size.Height);
            tabPage4.Size = new Size(this.Size.Width -60,tabPage4.Size.Height);
        }

    }
}
