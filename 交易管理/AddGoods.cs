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

namespace WindowsFormsApp1.交易管理
{
    public partial class AddGoods : Form
    {
        public AddGoods()
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
                    MessageBox.Show("数据库中没有这个生产厂商！请先添加", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //由于数据库的这个表并不检查重复插入完全相同的商品，
                //

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ToString()))
                    {
                        string sql = "SELECT count(*) FROM GOODSREMAIN WHERE " +
                          "商品名=@商品名 AND " +
                          "生产厂商=生产厂商 AND " +
                          "型号=@型号 ";

                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.Add(new SqlParameter("@商品名",SqlDbType.NVarChar,20)) ;
                            cmd.Parameters.Add(new SqlParameter("@生产厂商",SqlDbType.NVarChar,20)) ;
                            cmd.Parameters.Add(new SqlParameter("@型号",SqlDbType.NVarChar,20)) ;
                           

                            cmd.Parameters["@商品名"].Value=textBox2.Text;
                            cmd.Parameters["@生产厂商"].Value = comboBox1.Text;
                            cmd.Parameters["@型号"].Value = textBox4.Text;
                            
                                
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if  (reader.GetInt32(0)!=0)
                                    {
                                        sql = "BEGIN TRANSACTION " +
                                            "INSERT INTO ADDGOODS (商品名,生产厂商,型号,单价,数量,进货年,进货月,进货日,业务员编号,总金额) VALUES (@商品名,@生产厂商,@型号,@单价,@数量,@进货年,@进货月,@进货日,@业务员编号,@总金额) " +
                                            "UPDATE GOODSREMAIN SET 数量=数量+@数量 WHERE 商品名=@商品名 AND 生产厂商=@生产厂商 AND 型号=@型号 " +
                                            "COMMIT ";
                                        using (SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                                        {
                                            try
                                            {
                                                conn2.Open();
                                                using (SqlCommand cmd2 = new SqlCommand(sql, conn2))
                                                {
                                                    cmd2.Parameters.Add(new SqlParameter("@商品名", SqlDbType.NVarChar, 20));
                                                    cmd2.Parameters.Add(new SqlParameter("@生产厂商", SqlDbType.NVarChar, 20));
                                                    cmd2.Parameters.Add(new SqlParameter("@型号", SqlDbType.NVarChar, 20));
                                                    cmd2.Parameters.Add(new SqlParameter("@单价", SqlDbType.Money));
                                                    cmd2.Parameters.Add(new SqlParameter("@数量", SqlDbType.Int));
                                                    cmd2.Parameters.Add(new SqlParameter("@进货年", SqlDbType.SmallInt));
                                                    cmd2.Parameters.Add(new SqlParameter("@进货月", SqlDbType.SmallInt));
                                                    cmd2.Parameters.Add(new SqlParameter("@进货日", SqlDbType.SmallInt));
                                                    cmd2.Parameters.Add(new SqlParameter("@业务员编号", SqlDbType.Int));
                                                    cmd2.Parameters.Add(new SqlParameter("@总金额", SqlDbType.Money));

                                                    cmd2.Parameters["@商品名"].Value = textBox2.Text;
                                                    cmd2.Parameters["@生产厂商"].Value = comboBox1.Text;
                                                    cmd2.Parameters["@型号"].Value = textBox4.Text;
                                                    cmd2.Parameters["@单价"].Value = textBox5.Text;
                                                    cmd2.Parameters["@数量"].Value = textBox6.Text;
                                                    cmd2.Parameters["@进货年"].Value = numericUpDown1.Value;
                                                    cmd2.Parameters["@进货月"].Value = numericUpDown2.Value;
                                                    cmd2.Parameters["@进货日"].Value = numericUpDown3.Value;
                                                    cmd2.Parameters["@业务员编号"].Value = textBox10.Text;
                                                    cmd2.Parameters["@总金额"].Value = textBox11.Text;

                                                    cmd2.ExecuteNonQuery();
                                                    MessageBox.Show("添加成功！");
                                                    this.Close();
                                                    //一条数据插入成功后，弹出关闭窗口选项。
                                                }
                                            }catch(Exception exx)
                                            {
                                                MessageBox.Show(exx.Message+"ssss");
                                            }

                                        }
                                    }
                                    else
                                    {
                                        //goodsremain中没有这个商品

                                        sql = "BEGIN TRANSACTION " +
                                            "INSERT INTO ADDGOODS (商品名,生产厂商,型号,单价,数量,进货年,进货月,进货日,业务员编号,总金额) VALUES (@商品名,@生产厂商,@型号,@单价,@数量,@进货年,@进货月,@进货日,@业务员编号,@总金额) " +
                                            "INSERT INTO GOODSREMAIN (商品名,生产厂商,型号,数量) VALUES (@商品名,@生产厂商,@型号,@数量) " +
                                            "COMMIT ";
                                        using (SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                                        {
                                            conn2.Open();
                                            using (SqlCommand cmd2 = new SqlCommand(sql, conn2))
                                            {
                                                cmd2.Parameters.Add(new SqlParameter("@商品名", SqlDbType.NVarChar, 20));
                                                cmd2.Parameters.Add(new SqlParameter("@生产厂商", SqlDbType.NVarChar, 20));
                                                cmd2.Parameters.Add(new SqlParameter("@型号", SqlDbType.NVarChar, 20));
                                                cmd2.Parameters.Add(new SqlParameter("@单价", SqlDbType.Money));
                                                cmd2.Parameters.Add(new SqlParameter("@数量", SqlDbType.Int));
                                                cmd2.Parameters.Add(new SqlParameter("@进货年", SqlDbType.SmallInt));
                                                cmd2.Parameters.Add(new SqlParameter("@进货月", SqlDbType.SmallInt));
                                                cmd2.Parameters.Add(new SqlParameter("@进货日", SqlDbType.SmallInt));
                                                cmd2.Parameters.Add(new SqlParameter("@业务员编号", SqlDbType.Int));
                                                cmd2.Parameters.Add(new SqlParameter("@总金额", SqlDbType.Money));

                                                cmd2.Parameters["@商品名"].Value = textBox2.Text;
                                                cmd2.Parameters["@生产厂商"].Value = comboBox1.Text;
                                                cmd2.Parameters["@型号"].Value = textBox4.Text;
                                                cmd2.Parameters["@单价"].Value = textBox5.Text;
                                                cmd2.Parameters["@数量"].Value = textBox6.Text;
                                                cmd2.Parameters["@进货年"].Value = numericUpDown1.Value;
                                                cmd2.Parameters["@进货月"].Value = numericUpDown2.Value;
                                                cmd2.Parameters["@进货日"].Value = numericUpDown3.Value;
                                                cmd2.Parameters["@业务员编号"].Value = textBox10.Text;
                                                cmd2.Parameters["@总金额"].Value = textBox11.Text;

                                                cmd2.ExecuteNonQuery();
                                                MessageBox.Show("添加成功！");
                                                this.Close();
                                                //一条数据插入成功后，弹出关闭窗口选项。
                                            }

                                        }
                                    }
                                }
                            }
                               
                        }

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




        //添加厂商
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox12.Text.Length == 0 || textBox13.Text.Length == 0 ||  textBox14.Text.Length == 0 || textBox15.Text.Length == 0 )
            {
                MessageBox.Show("厂商信息未填写完整！", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (x(textBox12.Text))
                {
                    MessageBox.Show("数据库中已经有这个厂商！", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    //why choose "using"
                    //只在连接范围内打开和关闭。
                    //确保对象的正确处理和释放。
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                    {
                        conn.Open(); 
                        string sql = "INSERT INTO MANUFACTURER (厂商名称,法人代表,电话,厂商地址)VALUES(@name,@legalresp,@tell,@address)";

                        using (SqlCommand cmd=new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 20));
                            cmd.Parameters.Add(new SqlParameter("@legalresp", SqlDbType.NVarChar, 20));
                            cmd.Parameters.Add(new SqlParameter("@tell", SqlDbType.NVarChar, 20));
                            cmd.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar, 100));
                            cmd.Parameters["@name"].Value = textBox12.Text.Trim();
                            cmd.Parameters["@legalresp"].Value = textBox13.Text.Trim();
                            cmd.Parameters["@tell"].Value = textBox14.Text.Trim();
                            cmd.Parameters["@address"].Value = textBox15.Text.Trim();
                            cmd.ExecuteNonQuery();
                            sql = "SELECT 厂商名称,法人代表,电话,厂商地址 FROM MANUFACTURER WHERE 厂商名称=@name";
                            using (SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString))
                            {
                                conn2.Open();
                                using (SqlCommand cmd2 = new SqlCommand(sql, conn2))
                                {
                                    cmd2.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 20));
                                    cmd2.Parameters["@name"].Value = textBox12.Text.Trim();
                                    using (SqlDataReader reader = cmd2.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            MessageBox.Show("添加成功！");
                                            comboBox1.Items.Clear();
                                            add_items();
                                        }
                                    }
                                }
                            }
                            
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

        private void button4_Click(object sender, EventArgs e)
        {
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox12.Focus();
        }


        //改变单价时自动修改总价
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            //当未填写tb6时,检查tb5的值是否能转为数字。
            //不赋值
            if (textBox5.Text.Length != 0&&textBox6.Text.Length==0)
            {
                if (float.TryParse(textBox5.Text, out float value) == true)
                {
                    return;
                }
                else
                {                
                    MessageBox.Show("请输入数值");
                    textBox5.Focus(); //将光标锁定，直到输入正确格式才能离开，避免输入错误
                    return;
                }
            }

            //已经填写了6，又返回来变动5时，检查
            //赋值
            if (textBox6.Text.Length != 0 && textBox5.Text.Length != 0)
            {
                if (float.TryParse(textBox5.Text, out float value) == true)
                {
                    textBox11.Text = (float.Parse(textBox5.Text) * int.Parse(textBox6.Text)).ToString("F2");
                    return;
                }
                else
                {
                    MessageBox.Show("请输入数值");
                    textBox5.Focus(); //将光标锁定，直到输入正确格式才能离开，避免输入错误
                    return;
                }
            }

        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            //当未填写tb5并且先填了6,检查tb6的值是否能转为数字。
            if (textBox6.TextLength != 0&&textBox5.TextLength==0)
            {
                  if(int.TryParse(textBox6.Text,out int value) == true)
                {
                    return;
                }
                else
                {
                    MessageBox.Show("请输入数值");
                    textBox6.Focus();
                    return;
                }
            }

            //先填写了5，又返回来变动6时，检查
            //赋值
            if (textBox6.Text.Length != 0 && textBox5.Text.Length != 0)
            {
                 if(int.TryParse(textBox6.Text,out int value) == true)
                {
                    textBox11.Text = (float.Parse(textBox5.Text) * int.Parse(textBox6.Text)).ToString("F2");
                    return;
                }
                else
                {
                    MessageBox.Show("请输入数值");
                    textBox6.Focus();
                    return;
                }
            }
        }
    }
}
