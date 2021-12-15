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
    public partial class Checkemployee : Form
    {
        public Checkemployee()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet();

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Views.Employee, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "员工表");

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



        //这里查询员工只用输入一个信息即可。
        private void button3_Click(object sender, EventArgs e)
        {
            //检查输入
            if (textBox1.Text.Length == 0&&textBox2.Text.Length==0)
            {
                MessageBox.Show("请至少填写一个信息！");
                if (textBox1.Text.Length == 0)
                {
                    textBox1.Focus();
                }
                else
                {
                    textBox2.Focus();
                }
                return;
            }

           
            try
            {
                if (textBox1.Text.Length >0 ||textBox2.Text.Length>0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables["员工表"].Clone();


                    DataRow[] dataRow = ds.Tables["员工表"].Select("员工编号=" + ((textBox1.Text.Length>0)?(int.Parse(textBox1.Text)):0)+ " OR 员工姓名='"+textBox2.Text+"'");
                    foreach (DataRow row in dataRow)
                    {
                        dt.Rows.Add(row.ItemArray);
                    };

                    dataGridView1.DataSource = dt;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (dataGridView1.Rows.Count ==1)
            {
                MessageBox.Show("该员工不存在！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.Tables["员工表"];

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource=null;
        }



        private void textBox1_KeyDow(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(sender, e);
            }
            if(e.KeyCode == Keys.Escape)
            {
                textBox1.Text = "";
            }

        }
        private void textBox2_KeyDow(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                textBox2.Text = "";
            }

        }
        //按回车键响应查询
        //按Escape清空
    }
}
