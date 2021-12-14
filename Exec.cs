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
            Button_control();
            button1.Visible = false;
            button9.Visible = true;
            button10.Visible = true;
            button11.Visible = true;
            button12.Visible = true;
            button9.Focus();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Button_control();
            button2.Visible = false;

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Button_control();
            button3.Visible = false;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Button_control();
            button4.Visible = false;

        }
        private void button5_Click(object sender, EventArgs e)
        {
            Button_control();
            button5.Visible = false;
        }

        //系统管理
        private void button6_Click(object sender, EventArgs e)
        {
            Button_control();
            button6.Visible = false;
            button7.Visible = true;
            button8.Visible = true;
            button7.Focus();
        }

        //点击大按钮时：隐藏所有小button，显示所有大button
        private void Button_control()
        {
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;

            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible=false;
            button11.Visible=false;
            button12.Visible=false;
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

        private void button9_Click(object sender, EventArgs e)
        {
            AddGoods addGoods = new AddGoods();
            addGoods.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SellGoods sellGoods = new SellGoods();
            sellGoods.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            RetreatGoods retreatGoods = new RetreatGoods();
            retreatGoods.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            AddGoods addGoods=new AddGoods();  
            addGoods.Show();
        }
    }
}
