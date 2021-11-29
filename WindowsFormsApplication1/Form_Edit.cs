using System;
using System.IO;
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
    public partial class Form_Edit : Form
    {
        public Form_Edit()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlStr = "UPDATE Orders SET status='" + comboBox1.Text + "' WHERE id_orders ='" + textBox1.Text + "'";
            if (MessageBox.Show("Are you sure you want to replace the data?", "Replace",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (MySqlConnection con1 = new MySqlConnection(h.conStr))
                {
                    MySqlCommand cmd = new MySqlCommand(sqlStr, con1);

                    con1.Open();
                    cmd.ExecuteNonQuery();
                    con1.Close();
                    MessageBox.Show("Record editing was successful");
                }
                this.Close();
            }
        }
    }
}
