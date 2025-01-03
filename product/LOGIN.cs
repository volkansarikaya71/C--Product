using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace product
{
    public partial class LOGIN : Form
    {
        public LOGIN()
        {
            InitializeComponent();
        }
        MySqlConnection baglan = new MySqlConnection("");

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                string sql = "Select * From admin where members_name=@kullanici and members_password=@sifre";
                MySqlParameter prm1 = new MySqlParameter("kullanici", Kullaniciadi.Text.Trim());
                MySqlParameter prm2 = new MySqlParameter("sifre", Parola.Text.Trim());
                MySqlCommand komut = new MySqlCommand(sql, baglan);
                komut.Parameters.Add(prm1);
                komut.Parameters.Add(prm2);
                MySqlDataAdapter da = new MySqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    baglan.Close();
                    products anasayfa = new products();
                    anasayfa.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış!!");
                    baglan.Close();
                }
            }
            catch
            {
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPassword.Checked)
            {
                Parola.PasswordChar = '\0';
            }
            else
            {
                Parola.PasswordChar = '*';
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 Sifremiunuttum = new Form2();
            Sifremiunuttum.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("volkansarikaya71@gmail.com");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/volkansarikaya71");
        }
    }
    
}
