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
    public partial class Link2DB : Form
    {
        public Link2DB()
        {
            InitializeComponent();
        }

        private void Link2DB_Load(object sender, EventArgs e)
        {
            get_default_settings();
            checkBox1.Checked = true;//窗体加载时默认选中使用默认连接
            
        }


        //定义一个结构体，来临时存放
        private struct link
        {
            public string conStrName;//字符串名
            public string server;
            public string database;
            //public string name;
            //public string password;
        }

        link l; //实例化到全局。
       

        //窗口加载时
        //读取default connectionString 并获取默认信息 传递到文本框中。
        protected void get_default_settings()
        {
            #region 获取server
            string cfig = ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString();        
            int front = cfig.IndexOf("server=");
            string x1 = cfig.Substring(front);
            string x2;

            int back = x1.IndexOf(";");
            if (back == -1) back = x1.Length;
            x2 = x1.Substring(7, back-7);
            l.server= x2;

            #endregion

            #region 获取database
             cfig = ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString();
             front = cfig.IndexOf("database=");
            x1 = cfig.Substring(front, cfig.Length - front);
            back = x1.IndexOf(";");
            if (back == -1) back = x1.Length;
            x2 = x1.Substring(0 + 9, back - 9);
            l.database= x2;
            #endregion

            comboBox_server.Items.Add(l.server);//在下拉选项框中加入default server
            comboBox_database.Items.Add(l.database);//在下拉选项框中加入default database
            comboBox_server.SelectedIndex=0;
            comboBox_database.SelectedIndex = 0;

        }


        //用户自定义登录方式
        //连接字符串
        private void Join_str()
        {
            //由于添加connectionString名称不能重复，方便起见，使用当前日期时间作为字符串名称。
            //！每一次使用非默认方式链接数据库，都将被记录经app.config中作为connectionString字符串
            DateTime datetime = DateTime.Now;
            l.conStrName = datetime.ToString();


            string connectionString;
            ConnectionStringSettings setConnStr;

            if (radioButton2.Checked)
            {
                //设置连接字符串
                connectionString = "server=" + comboBox_server.Text + ";database=" + comboBox_database.Text + ";User ID=" + textBox1.Text + ";PassWord=" + textBox2.Text;
                setConnStr = new ConnectionStringSettings(l.conStrName, connectionString, "System.Data.SqlClient");
            }
            else
            {
                //设置连接字符串
                connectionString = "server=" + comboBox_server.Text + ";database=" + comboBox_database.Text + ";Initial Catalog=sellsystem;Integrated Security=True";
                setConnStr = new ConnectionStringSettings(l.conStrName, connectionString, "System.Data.SqlClient");
            }

                //打开当前应用程序的app.config文件，进行操作  
                Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                //由于没有更新连接字符串的方法，所以这里直接再添加一个连接字符串  
                appConfig.ConnectionStrings.ConnectionStrings.Add(setConnStr);
                appConfig.Save();

                //强制重新载入配置文件的ConnectionStrings配置节  
                ConfigurationManager.RefreshSection("connectionStrings");


            #region
            // 
            ////获取Configuration对象
            //Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ////根据Key读取<add>元素的Value
            //string name = config.AppSettings.Settings["name"].Value;
            ////写入<add>元素的Value
            //config.AppSettings.Settings["name"].Value = "fx163";
            ////增加<add>元素
            //config.AppSettings.Settings.Add("url", "http://www.fx163.net");
            ////删除<add>元素
            //config.AppSettings.Settings.Remove("name");
            ////一定要记得保存，写不带参数的config.Save()也可以
            //config.Save(ConfigurationSaveMode.Modified);
            ////刷新，否则程序读取的还是之前的值（可能已装入内存）
            //System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            #endregion
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = radioButton2.Checked;
            textBox2.Enabled = radioButton2.Checked;
        }

        
        private void comboBox_server_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_database.SelectedIndex= comboBox_server.SelectedIndex;
        }

        private void checkBox_Use_default_settings_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                radioButton1.Checked = true;
                comboBox_server.SelectedIndex = 0;
                comboBox_database.SelectedIndex = 0;
            }
           
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Sign_in_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //选中了"使用默认链接"
                //则使用默认字符串
                link2db.constr = "default";
                if (TestLink())
                {
                    Login login = new Login();
                    this.Hide();
                    login.Show();
                }
            }
            else
            {
                Join_str();
                link2db.constr = l.conStrName;
                if (TestLink())
                {
                    Login login = new Login();
                    this.Hide();
                    login.Show();
                }
            }
            
        }

        private bool TestLink()
        {
            try
            {
                SqlConnection conn;
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings[link2db.constr].ConnectionString);
                conn.Open();
                string sql = "select 'hello world!' as a";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {      
                    MessageBox.Show("数据库链接成功！", reader.GetString(0) );
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    MessageBox.Show("数据库链接失败！");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
    public static class link2db
    {
        public static string constr { set; get; }//封装一个 字符串 conStr 作为连接字符串的 name。
    }
}
