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
    public partial class Table1 : Form
    {
        public Table1()
        {
            InitializeComponent();
        }

        private void Table1_Load(object sender, EventArgs e)
        {
            h.bs1 = new BindingSource();
            h.bs1.DataSource = h.myfunDT("SELECT * FROM Orders");
            dataGridView1.DataSource = h.bs1;
            //h.bs1.Sort = dataGridView1.Columns[2].Name;
         
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Silver;

          
        }


         private void btSearch_Click(object sender, EventArgs e)
         {

            if (btSearch.Checked)
            {
                textBox1.Visible = true;
                label1.Visible = true;
                label1.Text = "Пошук";
                textBox1.Focus();
            }
            else
            {
                textBox1.Visible = false;
                label1.Visible = false;
            }
            /**/
        }

         private void textBox1_Leave(object sender, EventArgs e)
         {
             textBox1.Visible = false;
             label1.Visible = false;
         }

         private void textBox1_TextChanged(object sender, EventArgs e)
         {
             for (int i = 0; i < dataGridView1.RowCount; i++)
             {
                 dataGridView1.Rows[i].Selected = false;
                 for (int j = 0; j < dataGridView1.ColumnCount; j++)
                     if (dataGridView1.Rows[i].Cells[j].Value != null)
                         if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                         {
                             dataGridView1.Rows[i].Selected = true;
                             break;
                         }
             }
         }
        
         private void btFilter_Click(object sender, EventArgs e)
         {
             if (btFilter.Checked)
             {
                 label2.Visible = true;
                 label3.Visible = true;
                 textBox2.Visible = true;
                 label2.Text = "Введіть фільтр: <ім'я поля> <логічний знак> <значення>:";
                 label3.Text = "Enter";
                 textBox2.Focus();
             }
             else
             {   
                 textBox2.Visible = false;
                 label2.Visible = false;
                 label3.Visible = false;
                 h.bs1.Filter = " ";
             }
         }

         private void textBox2_KeyDown(object sender, KeyEventArgs e)
         {
             if (e.KeyCode == Keys.Enter)
             {
                 h.bs1.Filter = textBox2.Text;
                 textBox2.Visible = false;
                 label2.Visible = false;
                 label3.Visible = false;
             }
         }

         private void textBox2_Leave(object sender, EventArgs e)
         {
             h.bs1.Filter = textBox2.Text;
             textBox2.Visible = false;
             label2.Visible = false;
             label3.Visible = false;
         }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           /* Form3 f5 = new Form3();
            f5.ShowDialog();
            h.bs1.DataSource = h.myfunDT("select * from Orders");
            dataGridView1.DataSource = h.bs1;
               */  }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            h.curVal0 = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            h.keyName = dataGridView1.Columns[0].Name;

            Form_Del f6 = new Form_Del();
            f6.ShowDialog();

            h.bs1.DataSource = h.myfunDT("select * from Orders");
            dataGridView1.DataSource = h.bs1;
        }

        private void dataGridView1_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            /*int curColidx = dataGridView1.CurrentCellAddress.X;
            int curRowidx = dataGridView1.CurrentCellAddress.Y;
            string curColName0 = dataGridView1.Columns[0].Name;
            string curColName = dataGridView1.Columns[curColidx].Name;
            h.curVal0 = dataGridView1[0, curRowidx].Value.ToString();

            string newCurCellVal = e.Value.ToString();
                
                if (curColName == "id_scientist" || curColName == "First_name" || curColName == "Last_name" || curColName == "field_of_activity_in_science")
            {
                newCurCellVal = "'" + newCurCellVal + "'";
            }
            string sqlStr = "UPDATE Scientists SET" + curColName + " = " + newCurCellVal + " WHERE " + curColName0 + " = " + h.curVal0;

            using (MySqlConnection con1 = new MySqlConnection(h.conStr))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStr, con1);

                con1.Open();
                cmd.ExecuteNonQuery();
                con1.Close();
            }*/
    }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            h.curVal0 = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            h.keyName = dataGridView1.Columns[0].Name;

            Form_Edit f7 = new Form_Edit();
            f7.ShowDialog();

            h.bs1.DataSource = h.myfunDT("select * from Orders");
            dataGridView1.DataSource = h.bs1;
        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
