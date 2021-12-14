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
    public partial class SellGoods : Form
    {
        public SellGoods()
        {
            InitializeComponent();
        }

        //重载关闭窗口事件，避免误操作关闭。
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

        //form load
        private void AddGoods_Load(object sender, EventArgs e)
        {
            textBox1.Text = "编号自动添加";
            textBox1.ReadOnly = true;
            checkBox1.Checked = true;

            add_items();
                     
        }
        //往comboBox1中添加下拉选项。
        private void add_items()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                {
                    string sql = "SELECT 厂商名称 FROM MANUFACTURER";
                    using (SqlCommand sqlCommand = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                comboBox1.Items.Add(sqlDataReader.GetString(0));
                            }
                        }
                    }                        
                }         
            }
            catch (Exception ex)
            {
                MessageBox.Show("在添加 厂商名称 下拉选项时发生以下错误:"+ex.Message, "error");
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
        //检查数据库中是否有输入的生产厂商名称。
        private bool x(string str)
        {
            bool flag = false;
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (str == comboBox1.Items[i].ToString())
                {
                    flag = true;
                }
            }
            if (flag)
            {
                return true;
            }
            else 
            {
                return false; 
            }
        }
        //插入前检查填写完整
        //检索数据库，确保不重复插入。
        private void button1_Click(object sender, EventArgs e)
        {
           
            if(textBox1.Text.Length==0||textBox2.Text.Length==0 || comboBox1.Text.Length==0 || textBox4.Text.Length == 0 || textBox5.Text.Length == 0 || textBox6.Text.Length == 0 || textBox10.Text.Length == 0||textBox11.Text.Length==0)
            {
                MessageBox.Show("商品信息未填写完整！", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!x(comboBox1.Text))
                {
                    MessageBox.Show("没有这个生产厂商的产品！", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                //检查是否有这个商品的信息（goods表）
                //检索剩余
                try
                {
                    using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                    {
                        conn.Open();
                        string sql = "SELECT 商品名,型号,生产厂商,数量 FROM GOODSREMAIN ";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                bool flag_exists = false;
                                bool flag_remain = false;
                                while (reader.Read())
                                {
                                    if (reader.GetString(0) == textBox2.Text && reader.GetString(1) == textBox4.Text && reader.GetString(2) == comboBox1.Text)
                                    {   
                                        flag_exists = true;
                                        if (reader.GetDecimal(3) > int.Parse(textBox6.Text))
                                        {
                                            flag_remain= true;
                                            MessageBox.Show("查询到该商品库存数量充足！","！", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) ;
                                        }
                                    }
                                }
                                if (!flag_exists)
                                {
                                    MessageBox.Show("未查询到该商品！");
                                    return;
                                }
                                if (!flag_remain)
                                {
                                    MessageBox.Show("该商品库存数量不足！");
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //插入销售信息
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ToString()))
                    {
                        conn.Open();
                        string  sql = "INSERT INTO SELL (商品名,生产厂商,型号,单价,数量,销售年,销售月,销售日,业务员编号,总金额) VALUES ('" + textBox2.Text + "','" + comboBox1.Text + "','" + textBox4.Text + "'," + textBox5.Text + "," + textBox6.Text + "," + numericUpDown1.Value + "," + numericUpDown2.Value + "," + numericUpDown3.Value + "," + textBox10.Text + "," + textBox11.Text + ")";

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("销售成功！");
                            this.Close();
                            //一条数据插入成功后，弹出关闭窗口选项。

                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                   // throw ex;
                }                
            }
        }
        //由于存储的是本项目日期在数据库中以smallint存储，为防止输入不存在的日期，要在输入时拒绝接收错误的日期。
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
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            comboBox1.Text= "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            checkBox1.Checked = false;
            checkBox1.Checked = true;
            textBox10.Text = "";
            textBox11.Text = "";
            textBox2.Focus();
        }


        //改变单价时自动修改总价
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Length != 0 && textBox5.Text.Length != 0)
            {
                textBox11.Text = (int.Parse(textBox5.Text) * int.Parse(textBox6.Text)).ToString();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length != 0 && textBox6.Text.Length != 0)
            {
                textBox11.Text = (int.Parse(textBox5.Text) * int.Parse(textBox6.Text)).ToString();
            }
        }
    }
}
