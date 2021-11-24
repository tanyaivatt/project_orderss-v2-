using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace myBD
{
    public partial class Form_Del : Form
    {
        public Form_Del()
        {
            InitializeComponent();
        }

        private void Form_Del_Load(object sender, EventArgs e)
        {
            textBox1.Text = " " + h.keyName + " = " + h.curVal0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlStr = "DELETE FROM Orders WHERE" + textBox1.Text;
            if (MessageBox.Show("Are you sure you want to delete the record?", "Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (MySqlConnection con1 = new MySqlConnection(h.conStr))
                {
                    MySqlCommand cmd = new MySqlCommand(sqlStr, con1);

                    con1.Open();
                    cmd.ExecuteNonQuery();
                    con1.Close();
                }
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
