using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using Telegram.Bot;



namespace myBD
{
    public partial class Form1 : Form
    {
        public string[,] matrix;

        private BotHandlers handlers;
        public Form1(BotHandlers handlers)
        {
            this.handlers = handlers;
            InitializeComponent();
            h.conStr = "server = 193.93.216.145; CharacterSet = cp1251;" +
                "user = sqlkns21_1_it; database = sqlkns21_1_it; password = kns20_it;";
            h.conStr2 = "server = 193.93.216.145; CharacterSet = cp1251;" +
                "user = user; database = sqlkns21_1_it; password = 2;";

            DataTable dt = h.myfunDT("select * from MyUser");

            int count = dt.Rows.Count;

            matrix = new string[count, 4];
            for (int i = 0; i < count; i++)
            {
                matrix[i, 0] = dt.Rows[i].Field<int>("idMyUser").ToString();
                matrix[i, 1] = dt.Rows[i].Field<string>("UserName");
                matrix[i, 2] = dt.Rows[i].Field<int>("Type").ToString();
                matrix[i, 3] = dt.Rows[i].Field<string>("Password");
                comboBox1.Items.Add(matrix[i, 1]);
            }
            comboBox1.Text = matrix[0, 1];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox2.Text = h.EncryptedPassword(textBox1.Text);
            for(int i = 0; i < matrix.GetLength(0); i++)
                if(String.Equals(comboBox1.Text, matrix[i, 1]))
                    if (String.Equals(h.EncryptedPassword(textBox1.Text), matrix[i, 3]))
                    {
                        h.typeuser = matrix[i, 2];
                        this.Hide();
                        myBD f0 = new myBD(handlers);
                        f0.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Введіть правильний пароль!");
                    }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    static class h
    {
        public static string conStr { get; set; }
        public static string conStr2 { get; set; }
        public static string typeuser { get; set; }
        public static string typeuser2 { get; set; }
        public static BindingSource bs1 { get; set; }
        public static string curVal0 { get; set; }
        public static string keyName { get; set; }
        public static string pathToFoto {get; set; }

        public static DataTable myfunDT(string sqlStr)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(h.conStr))
            {
                MySqlCommand com = new MySqlCommand(sqlStr, con);
                try
                {
                    con.Open();
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dt.Load(dr);
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return dt;
        }
        public static string EncryptedPassword(string s)//шифрування паролю
        {
            if (string.Compare(s, "null", true) == 0)
                return "NULL";
            //переводимо рядок в масив байтів
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //створюємо об'єкт для отримання засобів шифрування
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();

            //обчислюємо хеш представлення в байтах
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формуємо суцільну стрічку із масиву
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }

}
