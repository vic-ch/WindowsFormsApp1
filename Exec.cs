using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.进货统计;
using WindowsFormsApp1.销售统计;
using WindowsFormsApp1.系统管理;
using WindowsFormsApp1.业绩查看;
using WindowsFormsApp1.查看数据表;

namespace WindowsFormsApp1
{
    public partial class Exec : Form
    {
        public Exec()
        {
            InitializeComponent();
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //重载窗口关闭事件。

        #region button点击事件
        private void button1_交易管理_Click(object sender, EventArgs e)
        {
            Button_control();
            button1_交易管理.Visible = false;
            button9_进货登记.Visible = true;
            button10_销售登记.Visible = true;
            button11_退货登记.Visible = true;
            button12_进货厂商登记.Visible = true;
            button9_进货登记.Focus();
        }
        private void button2_进货统计_Click(object sender, EventArgs e)
        {
            Button_control();
            //button2.Visible = false;
            Buycount buycount = new Buycount();
            buycount.Show();           

        }
        private void button3_销售管理_Click(object sender, EventArgs e)
        {
            Button_control();
            //button3.Visible = false;
            Sellcount sellcount = new Sellcount();
            sellcount.Show();

        }
        private void button4_业绩查看_Click(object sender, EventArgs e)
        {
            Button_control();
            //button4.Visible = false;
            Showsell showsell = new Showsell();
            showsell.Show();

        }
        private void button5_查看数据表_Click(object sender, EventArgs e)
        {
            Button_control();
            button5_查看数据表.Visible = false;
            button13_现存货物.Visible = true;
            button14_已售货物.Visible = true;
            button15_进货货物.Visible = true;
            button16_退货货物.Visible = true;
            button17_员工信息.Visible = true;
            button18_进货商信息.Visible = true;
        }

        //系统管理
        private void button6_系统管理_Click(object sender, EventArgs e)
        {
            Button_control();
            button6_系统管理.Visible = false;
            button7_修改密码.Visible = true;
            button8_添加成员.Visible = true;
            button7_修改密码.Focus();
        }

        //点击大按钮时：隐藏所有小button，显示所有大button
        private void Button_control()
        {
            button1_交易管理.Visible = true;
            button2_进货统计.Visible = true;
            button3_销售统计.Visible = true;
            button4_业绩查看.Visible = true;
            button5_查看数据表.Visible = true;
            button6_系统管理.Visible = true;

            button7_修改密码.Visible = false;
            button8_添加成员.Visible = false;
            button9_进货登记.Visible = false;
            button10_销售登记.Visible=false;
            button11_退货登记.Visible=false;
            button12_进货厂商登记.Visible=false;
            button13_现存货物.Visible=false;
            button14_已售货物.Visible=false;
            button15_进货货物.Visible=false;
            button16_退货货物.Visible = false;
            button17_员工信息.Visible = false;
            button18_进货商信息.Visible = false;

        }


        //修改密码
        private void button7_修改密码_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("HELLO!");
            ModifyPassword modifyPassword = new ModifyPassword();
            modifyPassword.Show();
        }

        
        //添加成员
        private void button8_添加成员_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
        }

        private void button9_进货登记_Click(object sender, EventArgs e)
        {
            AddGoods addGoods = new AddGoods();
            addGoods.Show();
        }

        private void button10_销售登记_Click(object sender, EventArgs e)
        {
            SellGoods sellGoods = new SellGoods();
            sellGoods.Show();
        }

        private void button11_退货登记_Click(object sender, EventArgs e)
        {
            RetreatGoods retreatGoods = new RetreatGoods();
            retreatGoods.Show();
        }

        private void button12_进货厂商登记_Click(object sender, EventArgs e)
        {
            AddGoods addGoods=new AddGoods();  
            addGoods.Show();
        }

        private void button13_现存货物_Click(object sender, EventArgs e)
        {
            Checkremain checkremain = new Checkremain();
            checkremain.Show();
        }

        private void button14_已售货物_Click(object sender, EventArgs e)
        {
            Checksold checkold = new Checksold();
            checkold.Show();
        }

        private void button15_进货货物_Click(object sender, EventArgs e)
        {
            Checkaddgoods checkaddgoods = new Checkaddgoods();  
            checkaddgoods.Show();
        }

        private void button16_退货货物_Click(object sender, EventArgs e)
        {
            Checkretreat checkretreat = new Checkretreat();
            checkretreat.Show();
        }

        private void button17_员工信息_Click(object sender, EventArgs e)
        {
            Checkemployee checkemployee = new Checkemployee();
            checkemployee.Show();
        }

        private void button18_进货商信息_Click(object sender, EventArgs e)
        {
            Checkmanufacturer checkmanufacturer = new Checkmanufacturer();
            checkmanufacturer.Show();
        }
#endregion

        #region 菜单点击事件
        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button7_修改密码_Click(sender, e);
        }

        private void 添加成员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button8_添加成员_Click(sender, e);
        }

        private void 进货登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button9_进货登记_Click(sender,e);
        }

        private void 销售登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button10_销售登记_Click(sender, e);
        }

        private void 退货登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button11_退货登记_Click(sender, e);
        }

        private void 进货厂商登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button12_进货厂商登记_Click(sender, e);
        }

        private void 现存货物ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button13_现存货物_Click(sender,e);
        }

        private void 已售货物ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button14_已售货物_Click(sender, e);
        }

        private void 进货货物ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button15_进货货物_Click(sender, e);
        }

        private void 退货货物ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button16_退货货物_Click(sender, e);
        }

        private void 员工信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button17_员工信息_Click(sender, e);
        }

        private void 进货商信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button18_进货商信息_Click(sender, e);
        }

        private void 进货统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_进货统计_Click(sender, e);
        }

        private void 销售统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_销售管理_Click(sender, e);
        }

        private void 业绩查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4_业绩查看_Click(sender, e);
        }
        #endregion
    }
}
