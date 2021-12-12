using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Exec : Form
    {
        public Exec()
        {
            InitializeComponent();
        }


        //重载窗口关闭事件。
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            Set_all_small_btn_invisible();
            button6.Visible = true;
        }

        //系统管理
        private void button6_Click(object sender, EventArgs e)
        {
            Set_all_small_btn_invisible();
            button6.Visible = false;
            button7.Visible = true;
            button8.Visible = true;
            button7.Focus();
        }

        //隐藏所有小button的方法
        private void Set_all_small_btn_invisible()
        {
            button7.Visible = false;
            button8.Visible = false;
            //button9.Visible=false;
            //button10.Visible=false;
            //button11.Visible=false;
            //button12.Visible=false;
            //button13.Visible=false;
            //button14.Visible=false;
            //button15.Visible=false;
        }


        //修改密码
        private void button7_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("HELLO!");
            ModifyPassword modifyPassword = new ModifyPassword();
            modifyPassword.Show();
        }

        
        //添加成员
        private void button8_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
        }
    }
}
