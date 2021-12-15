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


namespace WindowsFormsApp1.业绩查看
{
    public partial class Showsell : Form
    {
        public Showsell()
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
                    using (SqlCommand cmd = new SqlCommand(Views.Query_employeesell, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            int count = adapter.Fill(ds, "员工销售额");

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

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("请输入员工号！");
                textBox1.Focus();
                return;
            }

            //检查输入
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if (textBox1.Text[i] < '0' || textBox1.Text[i] > '9')
                {
                    MessageBox.Show("请输入数字！");
                    textBox1.Text = "";
                    textBox1.Focus();
                    return;
                }
            }

            if (textBox1.Text.Length > 10)
            {
                MessageBox.Show("输入过长！请重新输入");
                textBox1.Text= "";
                textBox1.Focus();
                return;
            }

            try
            {
                if (textBox1.Text.Length > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables["员工销售额"].Clone();
                    DataRow[] dataRow = ds.Tables["员工销售额"].Select("业务员编号=" + int.Parse(textBox1.Text) + "");
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
            dataGridView1.DataSource = ds.Tables["员工销售额"];

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
        //按回车键响应查询
        //按Escape清空
    }
}
