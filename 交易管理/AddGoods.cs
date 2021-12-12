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
    public partial class AddGoods : Form
    {
        public AddGoods()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定要关闭窗口吗？", "!", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                base.OnFormClosing(e);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void AddGoods_Load(object sender, EventArgs e)
        {
            textBox1.Text = "编号自动添加";
            textBox1.ReadOnly = true;

            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString);
                string sql = "SELECT 厂商名称 FROM MANUFACTURER";
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                     comboBox1.Items.Add(sqlDataReader.GetString(0));
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error");
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                DateTime dateTime = DateTime.Now;
                numericUpDown1.Text = dateTime.Year.ToString();
                numericUpDown2.Text = dateTime.Month.ToString();
                numericUpDown3.Text = dateTime.Day.ToString();


            }
        }

        private void t()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ToString());
                string sql = "INSERT INTO GOODS (商品名,生产厂商,型号,单价,数量,进货年,进货月,进货日,业务员编号,总金额) VALUES ('" + textBox2.Text + "','" + comboBox1.SelectedItem + "','" + textBox4.Text + "'," + textBox5.Text +","+textBox6.Text+ "," + numericUpDown1.Value + "," + numericUpDown2.Value + "," + numericUpDown3.Value + "," + textBox10.Text + "," + textBox11.Text + ")";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                int n = sqlCommand.ExecuteNonQuery();
                if (n != 1)
                {
                    MessageBox.Show("插入错误！", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // select 生产厂商, 商品名, 型号, 单价, 数量, 总金额, 进货年, 进货月, 进货日, 业务员编号 from goods where 商品编号 = (select MAX(商品编号) from goods )
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length==0||textBox2.Text.Length==0 || comboBox1.Text.Length==0 || textBox4.Text.Length == 0 || textBox5.Text.Length == 0 || textBox6.Text.Length == 0 || textBox10.Text.Length == 0||textBox11.Text.Length==0)
            {
                if (MessageBox.Show("商品信息未填写完整！确认提交吗", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)==DialogResult.OK) 
                {
                    t();
                    MessageBox.Show("添加成功！");
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            else
            {
                t();
            }
        }

        //每月天数不同
        //当月份变化时，自动修改每月最大天数。月份为2时判断闰年。
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            switch (numericUpDown2.Value)
            {
                case 1:
                    numericUpDown3.Maximum = 31;break;
                case 2:
                    if (numericUpDown1.Value % 4 == 0 && numericUpDown1.Value % 100 != 0 || numericUpDown1.Value % 400 == 0) 
                    { 
                        numericUpDown3.Maximum = 29; 
                    }
                    else
                    {
                        numericUpDown3.Maximum = 28;
                    } break;

                case 3:
                    numericUpDown3.Maximum = 31; break;
                case 4:
                    numericUpDown3.Maximum = 30; break;
                case 5:
                    numericUpDown3.Maximum = 31; break;
                case 6:
                    numericUpDown3.Maximum = 30; break;
                case 7:
                    numericUpDown3.Maximum = 31; break;
                case 8:
                    numericUpDown3.Maximum = 31; break;
                case 9:
                    numericUpDown3.Maximum = 30; break;
                case 10:
                    numericUpDown3.Maximum = 31; break;
                case 11:
                    numericUpDown3.Maximum = 30; break;
                case 12:
                    numericUpDown3.Maximum = 31; break;
            }
        }

        //如果更改年份，而且月份框中为2，则修改每月最大天数。
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value == 2)
            {
                if (numericUpDown1.Value % 4 == 0 && numericUpDown1.Value % 100 != 0 || numericUpDown1.Value % 400 == 0)
                {
                    numericUpDown3.Maximum = 29;
                }
                else
                {
                    numericUpDown3.Maximum = 28;
                }
            }

        }
    }
}
