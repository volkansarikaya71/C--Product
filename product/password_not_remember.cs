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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=crud;Uid=root;Pwd='';");
        public void anasayfayonlendir()
        {
            LOGIN Anasayfa = new LOGIN();
            Anasayfa.Show();
            this.Hide();
        }

        public void guncelle()
        {
            if (Kullaniciadi.Text != "" && Telefon_Numarası.Text != "" && Yeni_sifre.Text != "")
            {
                try
                {
                    baglan.Open();
                    string sql = "Select * From admin where members_name=@kullanici and Phone_number=@Telefonno";
                    MySqlParameter prm1 = new MySqlParameter("kullanici", Kullaniciadi.Text.Trim());
                    MySqlParameter prm2 = new MySqlParameter("Telefonno", Telefon_Numarası.Text.Trim());
                    MySqlCommand komut = new MySqlCommand(sql, baglan);
                    komut.Parameters.Add(prm1);
                    komut.Parameters.Add(prm2);
                    MySqlDataAdapter da = new MySqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Başarılı bir şekilde güncellendi.");
                        MySqlCommand update = new MySqlCommand("update admin set members_password='" + Yeni_sifre.Text + "'", baglan);
                        update.ExecuteNonQuery();
                        baglan.Close();
                        anasayfayonlendir();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya Telefon numarası yanlış!!");
                        baglan.Close();
                    }
                }
                catch
                {
                }
            }
            else
            {
                if (Kullaniciadi.Text == "")
                {
                    MessageBox.Show("Kullanıcı adı kısmını boş bırakmayınız");
                }
                else if (Telefon_Numarası.Text == "")
                {
                    MessageBox.Show("telefon numarası kısmını boş bırakmayınız");
                }
                else if (Yeni_sifre.Text == "")
                {
                    MessageBox.Show("Yeni şifre kısmını boş bırakmayınız");
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            anasayfayonlendir();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            guncelle();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            guncelle();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("volkansarikaya71@gmail.com");
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/volkansarikaya71");
        }
    }
}
