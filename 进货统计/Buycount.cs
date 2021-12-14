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

namespace WindowsFormsApp1.进货统计
{
    public partial class Buycount : Form
    {
        public Buycount()
        {
            InitializeComponent();
        }


        DataSet ds = new DataSet();

        private void Buycount_Load(object sender, EventArgs e)
        {
            prepare();
            Console.WriteLine(Views.Thisday);
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
                    using (SqlCommand cmd = new SqlCommand(Views.Thisday, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "进货统计1");
                            Console.WriteLine(count);
                            dataGridView1.DataSource = ds.Tables["进货统计1"];
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
                    using (SqlCommand cmd = new SqlCommand(Views.Thisday_group, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "进货统计汇总1");
                            Console.WriteLine(count);
                            dataGridView2.DataSource = ds.Tables["进货统计汇总1"];
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
                    using (SqlCommand cmd = new SqlCommand(Views.Thismonth, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "进货统计2");
                            Console.WriteLine(count);
                            dataGridView4.DataSource = ds.Tables["进货统计2"];
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
                    using (SqlCommand cmd = new SqlCommand(Views.Thismonth_group, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "进货统计汇总2");
                            Console.WriteLine(count);
                            dataGridView3.DataSource = ds.Tables["进货统计汇总2"];
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
                    Views.Thisday = "goods";
                    using (SqlCommand cmd = new SqlCommand(Views.Thisseason, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "进货统计3");
                            Console.WriteLine(count);
                            dataGridView6.DataSource = ds.Tables["进货统计3"];
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
                    using (SqlCommand cmd = new SqlCommand(Views.Thisseason_group, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "进货统计汇总3");
                            Console.WriteLine(count);
                            dataGridView5.DataSource = ds.Tables["进货统计汇总3"];
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
                    using (SqlCommand cmd = new SqlCommand(Views.Thisyear, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "进货统计4");
                            Console.WriteLine(count);
                            dataGridView8.DataSource = ds.Tables["进货统计4"];
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
                    using (SqlCommand cmd = new SqlCommand(Views.Thisyear_group, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "进货统计汇总");
                            Console.WriteLine(count);
                            dataGridView7.DataSource = ds.Tables["进货统计汇总"];
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
                MessageBox.Show("in tag 4 :"+ex.Message);
            }

        }

        private void prepare()
        {
            Views.Thisday="goods";
            Views.Thismonth = "goods";
            Views.Thisyear = "goods";
            Views.Thisseason = "goods";
            Views.Thisday_group = "goods";
            Views.Thismonth_group = "goods";
            Views.Thisseason_group = "goods";
            Views.Thisyear_group = "goods";
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
