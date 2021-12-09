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
            set_all_small_btn_invisible();
            button6.Visible = true;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            set_all_small_btn_invisible();
            button6.Visible = false;
            button7.Visible = true;
            button8.Visible = true;
            button7.Focus();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("HELLO!");
            ModifyPassword modifyPassword = new ModifyPassword();
            modifyPassword.Show();
        }


        private void set_all_small_btn_invisible()
        {
            button7.Visible=false;
            button8.Visible=false;
            //button9.Visible=false;
            //button10.Visible=false;
            //button11.Visible=false;
            //button12.Visible=false;
            //button13.Visible=false;
            //button14.Visible=false;
            //button15.Visible=false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
        }
    }
}
