using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace myBD
{
    public partial class Table3 : Form
    {
        public Table3()
        {
            InitializeComponent();
        }

        private void Table3_Load(object sender, EventArgs e)
        {
            h.bs1 = new BindingSource();
            h.bs1.DataSource = h.myfunDT("SELECT * FROM Product");
            dataGridView1.DataSource = h.bs1;


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

       
    }
}
