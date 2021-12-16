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


namespace WindowsFormsApp1.查看数据表
{
    public partial class Checkmanufacturer : Form
    {
        public Checkmanufacturer()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet();

        private void Form1_Load(object sender, EventArgs e)
        {
            dataload();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;

            tsbsave.Enabled = false;
        }

        private void dataload()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Views.Manufacturer, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            ds.Clear();
                            int count = adapter.Fill(ds, "进货商表");
                            dataGridView1.DataSource = ds.Tables["进货商表"];

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(this.Size.Width - 60, dataGridView1.Size.Height);
            groupBox1.Size=new Size(this.Size.Width-60,groupBox1.Size.Height);
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text=dataGridView1[0,dataGridView1.CurrentCell.RowIndex].Value.ToString();
            textBox2.Text = dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            textBox3.Text = dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            textBox4.Text = dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            textBox5.Text = dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString();
        }

        private void tsbedit_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            tsbsave.Enabled = true;
        }

        private void tsbcancel_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            tsbsave.Enabled=false;
        }

        private void tsbsave_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString()
                ||textBox2.Text != dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()
                ||textBox3.Text != dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString()
                ||textBox4.Text != dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString()
                ||textBox5.Text != dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString())
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                    {
                        conn.Open();
                        string sql = "UPDATE MANUFACTURER SET 厂商名称=@name ,法人代表 =@legalresp, 电话=@tell, 厂商地址=@address  WHERE 厂商编号=@id";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 20));
                            cmd.Parameters.Add(new SqlParameter("@legalresp", SqlDbType.NVarChar, 20));
                            cmd.Parameters.Add(new SqlParameter("@tell", SqlDbType.NVarChar, 20));
                            cmd.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar, 100));
                            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                            cmd.Parameters["@name"].Value=textBox2.Text.Trim();
                            cmd.Parameters["@legalresp"].Value=textBox3.Text.Trim();
                            cmd.Parameters["@tell"].Value=textBox4.Text.Trim();
                            cmd.Parameters["@address"].Value=textBox5.Text.Trim();
                            cmd.Parameters["@id"].Value=textBox1.Text.Trim();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("修改成功！");

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dataload();
                tsbsave.Enabled = false;
            }
        }

        private void tsbdel_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length>0)
            {
                if(MessageBox.Show("确定要删除 " + textBox2.Text + " 吗？", "!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                        {
                            conn.Open();
                            string sql = "DELETE MANUFACTURER WHERE 厂商编号=@id";
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                cmd.Parameters.Add(new SqlParameter("@id",SqlDbType.Int));
                                cmd.Parameters["@id"].Value = textBox1.Text;
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("修改成功！");
                                dataload();
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

    }
}
