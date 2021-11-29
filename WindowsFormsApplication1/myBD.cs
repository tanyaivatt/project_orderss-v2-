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
using Telegram.Bot;

namespace myBD
{
    public partial class myBD : Form
    {

        public string[,] matrix1, matrix2;
        private BotHandlers handlers;
        public myBD(BotHandlers handlers)
        {
            this.handlers = handlers;
            InitializeComponent();
            h.conStr = "server = 193.93.216.145; CharacterSet = cp1251;" +
              "user = sqlkns21_1_it; database = sqlkns21_1_it; password = kns20_it; convert zero datetime=True";

            DataTable dt = h.myfunDT("select * from Client");

            int count = dt.Rows.Count;

            matrix1 = new string[count, 3];
            for (int i = 0; i < count; i++)
            {
                matrix1[i, 0] = dt.Rows[i].Field<int>("id_client").ToString();
                matrix1[i, 1] = dt.Rows[i].Field<string>("client");
                matrix1[i, 2] = dt.Rows[i].Field<string>("adress");

                comboBox1.Items.Add(matrix1[i, 1]);
            }
            //comboBox1.Text = matrix1[0, 1]; 
            
            DataTable dt2 = h.myfunDT("select * from Product");

            int count1 = dt2.Rows.Count;

            matrix2 = new string[count1, 3];
            for (int i = 0; i < count1; i++)
            {
                matrix2[i, 0] = dt2.Rows[i].Field<int>("id_product").ToString();
                matrix2[i, 1] = dt2.Rows[i].Field<string>("name_of_product");
                //matrix2[j, 1] = dt2.Rows[j].Field<string>("price");
                comboBox2.Items.Add(matrix2[i, 1]);
            }
            //comboBox2.Text = matrix2[0, 1];
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con1 = new MySqlConnection(h.conStr))
            {
                string tb1 = textBox1.Text;
                string tb2 = comboBox3.Text;
                string tb3 = textBox2.Text;
                string tb4 = comboBox1.Text;
                string tb5 = comboBox2.Text;
                string tb6 = textBox5.Text;

                string sql = "INSERT INTO Orders" +
                                       "(id_orders, status, date, client, name_of_product, price)" +
                                       " VALUES (@TK1, @TK2, @TK3, @TK4, @TK5, @TK6)";
                MySqlCommand cmd = new MySqlCommand(sql, con1);
                cmd.Parameters.AddWithValue("@TK1", tb1);
                cmd.Parameters.AddWithValue("@TK2", tb2);
                cmd.Parameters.AddWithValue("@TK3", tb3);
                cmd.Parameters.AddWithValue("@TK4", tb4);
                cmd.Parameters.AddWithValue("@TK5", tb5);
                cmd.Parameters.AddWithValue("@TK6", tb6);

                con1.Open();
                cmd.ExecuteNonQuery();
                con1.Close();

                MessageBox.Show("Addind entry was successful");
            }
            //this.Close();
        }
        private void myBD_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f1 = new Form1(handlers);
            f1.Show();
        }

        private void myBD_Load(object sender, EventArgs e)
        {
            MessageBox.Show("тип користувача " + h.typeuser);
        }

        private void tabl1StripMenuItem_Click(object sender, EventArgs e)
        {
            Table1 f1 = new Table1();
            f1.ShowDialog();
        }

        private void tabl2StripMenuItem_Click(object sender, EventArgs e)
        {
            Table2 f2 = new Table2();
            f2.ShowDialog();
        }

        private void tabl3StripMenuItem_Click(object sender, EventArgs e)
        {
            Table3 f3 = new Table3();
            f3.ShowDialog();
        }

        private void проПрограмуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f4 = new Form2();
            f4.ShowDialog(); 
        }

      
    }
}
