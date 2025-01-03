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
    public partial class products : Form
    {
        public products()
        {
            InitializeComponent();
        }
        MySqlConnection baglan = new MySqlConnection("Server=89.117.169.1;Database=u717747209_vsdb;Uid=u717747209_vsuser;Pwd='Vsarikaya06.';");
        public string alanadi, alanadi2, alanadi3, alanadi4, alanadi5;
        public string alankosulu, alankosulu2, alankosulu3, alankosulu4, alankosulu5;

        private void button7_Click(object sender, EventArgs e)
        {
            if (U_urunbilgisi.Text != "" || U_urunadedi.Text != "" && U_kategori.Text !="")
            {
                baglan.Open();
                string kayit = "UPDATE products SET product_quantity='"+U_urunadedi.Text+ "' Where products_no='" + U_urunbilgisi.Text + "' OR products_name ='" + U_urunadedi.Text + "'";
                MySqlCommand komut = new MySqlCommand(kayit, baglan);
                MySqlDataAdapter da = new MySqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglan.Close();
            }
            else
            {
                MessageBox.Show("bütüm alanları doldulurunuz.");
            }
        }
        private void tabControl1_Click(object sender, EventArgs e)
        {
            LISTELE();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LISTELE();
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
                baglan.Open();
                string sql = "Select * From products where products_no=@urunno";
                MySqlParameter prm1 = new MySqlParameter("urunno", S_id.Text.Trim());
                MySqlCommand komut = new MySqlCommand(sql, baglan);
                komut.Parameters.Add(prm1);
                MySqlDataAdapter da = new MySqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    baglan.Close();
                    baglan.Open();
                    MySqlCommand sil = new MySqlCommand("delete from products where  products_no='" + S_id.Text + "'", baglan);
                    sil.ExecuteNonQuery();
                    MessageBox.Show("Sisteme başarıyla silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    baglan.Close();
                }
                else
                {
                    baglan.Close();
                    MessageBox.Show("Sisteme sistemde böyle bir kayıt yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }
        private void S_Clearpictures_Click(object sender, EventArgs e)
        {
            S_id.Clear();
            S_urun_adeti.Clear();
        }
        private void fulldelete(object sender, EventArgs e)
        {
            baglan.Open();
            string sql = "Select * From products where products_no=@urunno";
            MySqlParameter prm1 = new MySqlParameter("urunno", S_id.Text.Trim());
            MySqlCommand komut = new MySqlCommand(sql, baglan);
            komut.Parameters.Add(prm1);
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                baglan.Close();
                baglan.Open();
                MySqlCommand sil = new MySqlCommand("delete from products where  products_no='" + S_id.Text + "'", baglan);
                sil.ExecuteNonQuery();
                MessageBox.Show("Sisteme başarıyla silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                baglan.Close();
            }
            else
            {
                baglan.Close();
                MessageBox.Show("Sisteme sistemde böyle bir kayıt yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void urunadedidusur(object sender, EventArgs e)
        {
            int urunadeti;
            baglan.Open();
            MySqlCommand urunsorgu = new MySqlCommand("select product_quantity from products where products_no=@p_no", baglan);
            urunsorgu.Parameters.AddWithValue("@p_no", S_id.Text);
            MySqlDataReader dr = urunsorgu.ExecuteReader();
            if (dr.Read())
            {
                urunadeti = Convert.ToInt32(dr["product_quantity"]);
                if (S_urun_adeti.Text != "")
                {
                    baglan.Close();
                    baglan.Open();
                    urunadeti -= int.Parse(S_urun_adeti.Text);
                    if(urunadeti>= 0)
                    {
                    MySqlCommand urunadedinidusur = new MySqlCommand("UPDATE products SET product_quantity='" + urunadeti + "' Where products_no='" + S_id.Text + "'", baglan);
                    urunadedinidusur.ExecuteNonQuery();
                    MessageBox.Show("işleminiz başarılı bir şekilde gerçekleşmiştir."+urunadeti.ToString()+" adet ürününüz kaldı.");
                    }
                    else if (urunadeti < 0)
                    {
                        MessageBox.Show("yeterince ürününüz yok!");
                    }

                }
                else if (S_urun_adeti.Text == "")
                {
                    MessageBox.Show("ürün adeti kısmını bos bırakmayınız!");
                }
            }
            else
            {
                MessageBox.Show("böyle ürün yok");
            }
            baglan.Close();
        }

        private void rakamgirme(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void silrakamgir(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void updaterakamgir(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void updatemetod(object sender, EventArgs e)
        {
            if(U_id.Text !="" && U_kategori.Text !="" &&  U_urunadi.Text != "" && U_urunadedi.Text != "" && U_fiyat.Text != "" && U_urunbilgisi.Text != "")
            {
                baglan.Open();
                string sql = "Select * From products where products_no=@urunno";
                MySqlParameter prm1 = new MySqlParameter("urunno", U_id.Text.Trim());
                MySqlCommand komut = new MySqlCommand(sql, baglan);
                komut.Parameters.Add(prm1);
                MySqlDataAdapter da = new MySqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    baglan.Close();
                    baglan.Open();
                    MySqlCommand updatealldata = new MySqlCommand("UPDATE products SET products_category ='" + U_kategori.Text + "',products_name='" + U_urunadi.Text + "'," +"product_quantity='" + U_urunadedi.Text + "',products_price ='" + U_fiyat.Text + "',description  ='" + U_urunbilgisi.Text + "' Where products_no='" + U_id.Text + "'", baglan);
                    updatealldata.ExecuteNonQuery();
                    MessageBox.Show("Sisteme başarıyla güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    baglan.Close();
                }
                else
                {
                    baglan.Close();
                    MessageBox.Show("Sistemde böyle bir kayıt yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if(U_id.Text == "")
            {
                MessageBox.Show("id kısmi bos bırakılamaz");
            }
            else if (U_kategori.Text == "")
            {
                MessageBox.Show("kategori kısmi bos bırakılamaz");
            }
            else if (U_urunadi.Text == "")
            {
                MessageBox.Show("urunadı kısmi bos bırakılamaz");
            }
            else if (U_urunadedi.Text == "")
            {
                MessageBox.Show("urunadeti kısmi bos bırakılamaz");
            }
            else if (U_fiyat.Text == "")
            {
                MessageBox.Show("ürün fiyatı kısmi bos bırakılamaz");
            }
            else if (U_urunbilgisi.Text == "")
            {
                MessageBox.Show("ürün bilgisi kısmi bos bırakılamaz");
            }
        }
        private void Sendpictures_Click(object sender, EventArgs e)
        {
            if (E_id.Text != "" && E_urun_adi.Text != "" && E_fiyat.Text != "" && E_text.Text != "" && E_urun_adedi.Text != "")
            {
                try
                {
                    baglan.Open();
                    string sql = "Select * From products where products_name=@urunadi";
                    MySqlParameter prm1 = new MySqlParameter("urunadi", E_urun_adi.Text.Trim());
                    MySqlCommand komut = new MySqlCommand(sql, baglan);
                    komut.Parameters.Add(prm1);
                    MySqlDataAdapter da = new MySqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        baglan.Close();
                        MessageBox.Show("ayni isme sahip ürününüz var.");
                    }
                    else
                    {
                        baglan.Close();
                        try
                        {
                            baglan.Open();
                            MySqlCommand ekle = new MySqlCommand("insert into products(products_category,products_name,product_quantity,products_price,description) values ('" + E_id.Text + "','" + E_urun_adi.Text + "','" + E_urun_adedi.Text + "','" + E_fiyat.Text + "','" + E_text.Text + "')", baglan);
                            object sonuc = null;
                            sonuc = ekle.ExecuteNonQuery();
                            if (sonuc != null)
                                MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("Sisteme eklenemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            baglan.Close();
                        }
                        catch (Exception HataYakala)
                        {
                            MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception Hatayakala)
                {
                }
            }
            else if (E_id.Text == "")
            {
                MessageBox.Show("kategori kısmı bos bırakılamaz");
            }
            else if (E_urun_adi.Text == "")
            {
                MessageBox.Show("ürün adı kısmı bos bırakılamaz");
            }
            else if (E_urun_adedi.Text == "")
            {
                MessageBox.Show("ürün adedi kısmı bos bırakılamaz");
            }
            else if (E_fiyat.Text == "")
            {
                MessageBox.Show("fiyat kısmı bos bırakılamaz");
            }
            else if (E_text.Text == "")
            {
                MessageBox.Show("ürün bilgisi kısmı bos bırakılamaz");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            id.Clear();
            kategori.Clear();
            urun_adedi.Clear();
            urun_adi.Clear();
            fiyat.Clear();
        }

        private void passwordpictures_Click(object sender, EventArgs e)
        {
            if (id.Text != "" && kategori.Text != "" && urun_adi.Text != "" && fiyat.Text != "" && urun_adedi.Text != "")
            {
                baglan.Open();
                string kayit = "Select * From products where products_no='" + id.Text + "'and products_category='" + kategori.Text + "' and products_name ='" + urun_adi.Text + "' and products_price='" + fiyat.Text + "'and product_quantity='" + urun_adedi.Text + "'";
                MySqlCommand komut = new MySqlCommand(kayit, baglan);
                MySqlDataAdapter da = new MySqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglan.Close();
            }
            else if (id.Text != "" && kategori.Text != "" && urun_adi.Text != "" && fiyat.Text != "")
            {
                baglan.Open();
                string kayit = "Select * From products where products_no='" + id.Text + "'and products_category='" + kategori.Text + "' and products_name ='" + urun_adi.Text + "' and products_price='" + fiyat.Text + "'";
                MySqlCommand komut = new MySqlCommand(kayit, baglan);
                MySqlDataAdapter da = new MySqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglan.Close();
            }
            else if (id.Text != "" && kategori.Text != "" && urun_adi.Text != "" && urun_adedi.Text != "")
            {
                baglan.Open();
                string kayit = "Select * From products where products_no='" + id.Text + "'and products_category='" + kategori.Text + "' and products_name ='" + urun_adi.Text + "' and product_quantity='" + urun_adedi.Text + "'";
                MySqlCommand komut = new MySqlCommand(kayit, baglan);
                MySqlDataAdapter da = new MySqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglan.Close();
            }
            else if (id.Text != "" && kategori.Text != "" && fiyat.Text != "" && urun_adedi.Text != "")
            {
                baglan.Open();
                string kayit = "Select * From products where products_no='" + id.Text + "'and products_category='" + kategori.Text + "' and products_price='" + fiyat.Text + "' and product_quantity='" + urun_adedi.Text + "'";
                MySqlCommand komut = new MySqlCommand(kayit, baglan);
                MySqlDataAdapter da = new MySqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglan.Close();
            }
            else if (kategori.Text != "" && urun_adi.Text != "" && fiyat.Text != "" && urun_adedi.Text != "")
            {
                baglan.Open();
                string kayit = "Select * From products where products_category='" + kategori.Text + "'and products_name='" + urun_adi.Text + "' and products_price ='" + fiyat.Text + "' and product_quantity='" + urun_adedi.Text + "'";
                MySqlCommand komut = new MySqlCommand(kayit, baglan);
                MySqlDataAdapter da = new MySqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglan.Close();
            }
            else if (urun_adi.Text != "" && fiyat.Text != "" && id.Text != "" && urun_adedi.Text != "")
            {
                baglan.Open();
                string kayit = "Select * From products where products_name='" + urun_adi.Text + "'and products_price='" + fiyat.Text + "' and products_no='" + id.Text + "' and product_quantity='" + urun_adedi.Text + "'";
                MySqlCommand komut = new MySqlCommand(kayit, baglan);
                MySqlDataAdapter da = new MySqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglan.Close();
            }
            else if (id.Text != "" && kategori.Text != "" && urun_adi.Text != "")
            {
                alanadi = "products_no";
                alankosulu = id.Text;
                alanadi2 = "products_category";
                alankosulu2 = kategori.Text;
                alanadi3 = "products_name";
                alankosulu3 = urun_adi.Text;
                LISTELE3();
            }
            else if (id.Text != "" && kategori.Text != "" && fiyat.Text != "")
            {
                alanadi = "products_no";
                alankosulu = id.Text;
                alanadi2 = "products_category";
                alankosulu2 = kategori.Text;
                alanadi3 = "products_price";
                alankosulu3 = fiyat.Text;
                LISTELE3();
            }
            else if (id.Text != "" && kategori.Text != "" && urun_adedi.Text != "")
            {
                alanadi = "products_no";
                alankosulu = id.Text;
                alanadi2 = "products_category";
                alankosulu2 = kategori.Text;
                alanadi3 = "product_quantity";
                alankosulu3 = urun_adedi.Text;
                LISTELE3();
            }
            else if (id.Text != "" && urun_adi.Text != "" && fiyat.Text != "")
            {
                alanadi = "products_no";
                alankosulu = id.Text;
                alanadi2 = "products_name";
                alankosulu2 = urun_adi.Text;
                alanadi3 = "products_price";
                alankosulu3 = fiyat.Text;
                LISTELE3();
            }
            else if (id.Text != "" && urun_adi.Text != "" && urun_adedi.Text != "")
            {
                alanadi = "products_no";
                alankosulu = id.Text;
                alanadi2 = "products_name";
                alankosulu2 = urun_adi.Text;
                alanadi3 = "product_quantity";
                alankosulu3 = urun_adedi.Text;
                LISTELE3();
            }
            else if (id.Text != "" && fiyat.Text != "" && urun_adedi.Text != "")
            {
                alanadi = "products_no";
                alankosulu = id.Text;
                alanadi2 = "products_price";
                alankosulu2 = fiyat.Text;
                alanadi3 = "product_quantity";
                alankosulu3 = urun_adedi.Text;
                LISTELE3();
            }
            else if (kategori.Text != "" && urun_adi.Text != "" && fiyat.Text != "")
            {
                alanadi = "products_category";
                alankosulu = kategori.Text;
                alanadi2 = "products_name";
                alankosulu2 = urun_adi.Text;
                alanadi3 = "products_price";
                alankosulu3 = fiyat.Text;
                LISTELE3();
            }
            else if (kategori.Text != "" && urun_adi.Text != "" && urun_adedi.Text != "")
            {
                alanadi = "products_category";
                alankosulu = kategori.Text;
                alanadi2 = "products_name";
                alankosulu2 = urun_adi.Text;
                alanadi3 = "product_quantity";
                alankosulu3 = urun_adedi.Text;
                LISTELE3();
            }
            else if (kategori.Text != "" && fiyat.Text != "" && urun_adedi.Text != "")
            {
                alanadi = "products_category";
                alankosulu = kategori.Text;
                alanadi2 = "products_price";
                alankosulu2 = fiyat.Text;
                alanadi3 = "product_quantity";
                alankosulu3 = urun_adedi.Text;
                LISTELE3();
            }
            else if (urun_adi.Text != "" && fiyat.Text != "" && urun_adedi.Text != "")
            {
                alanadi = "products_name";
                alankosulu = urun_adi.Text;
                alanadi2 = "products_price";
                alankosulu2 = fiyat.Text;
                alanadi3 = "product_quantity";
                alankosulu3 = urun_adedi.Text;
                LISTELE3();
            }
            else if (id.Text != "" && kategori.Text != "")
            {
                alanadi = "products_no";
                alankosulu = id.Text;
                alanadi2 = "products_category";
                alankosulu2 = kategori.Text;
                LISTELE2();
            }
            else if (id.Text != "" && urun_adi.Text != "")
            {
                alanadi = "products_no";
                alankosulu = id.Text;
                alanadi2 = "products_name";
                alankosulu2 = urun_adi.Text;
                LISTELE2();
            }
            else if (id.Text != "" && fiyat.Text != "")
            {
                alanadi = "products_no";
                alankosulu = id.Text;
                alanadi2 = "products_price";
                alankosulu2 = fiyat.Text;
                LISTELE2();
            }
            else if (kategori.Text != "" && urun_adi.Text != "")
            {
                alanadi = "products_category";
                alankosulu = kategori.Text;
                alanadi2 = "products_name";
                alankosulu2 = urun_adi.Text;
                LISTELE2();
            }
            else if (kategori.Text != "" && fiyat.Text != "")
            {
                alanadi = "products_category";
                alankosulu = kategori.Text;
                alanadi2 = "products_price";
                alankosulu2 = fiyat.Text;
                LISTELE2();
            }
            else if (urun_adi.Text != "" && fiyat.Text != "")
            {
                alanadi = "products_name";
                alankosulu = urun_adi.Text;
                alanadi2 = "products_price";
                alankosulu2 = fiyat.Text;
                LISTELE2();
            }
            else if (id.Text != "" && urun_adedi.Text != "")
            {
                alanadi = "products_no";
                alankosulu = id.Text;
                alanadi2 = "product_quantity";
                alankosulu2 = urun_adedi.Text;
                LISTELE2();
            }
            else if (kategori.Text != "" && urun_adedi.Text != "")
            {
                alanadi = "products_category";
                alankosulu = kategori.Text;
                alanadi2 = "product_quantity";
                alankosulu2 = urun_adedi.Text;
                LISTELE2();
            }
            else if (urun_adi.Text != "" && urun_adedi.Text != "")
            {
                alanadi = "products_name";
                alankosulu = urun_adi.Text;
                alanadi2 = "product_quantity";
                alankosulu2 = urun_adedi.Text;
                LISTELE2();
            }
            else if (fiyat.Text != "" && urun_adedi.Text != "")
            {
                alanadi = "products_price";
                alankosulu = fiyat.Text;
                alanadi2 = "product_quantity";
                alankosulu2 = urun_adedi.Text;
                LISTELE2();
            }
            else if (id.Text != "")
            {
                alanadi = "products_no";
                alankosulu = id.Text;
                LISTELE1();
            }
            else if (kategori.Text != "")
            {
                alanadi = "products_category";
                alankosulu = kategori.Text;
                LISTELE1();
            }
            else if (urun_adi.Text != "")
            {
                alanadi = "products_name";
                alankosulu = urun_adi.Text;
                LISTELE1();
            }
            else if (fiyat.Text != "")
            {
                alanadi = "products_price";
                alankosulu = fiyat.Text;
                LISTELE1();
            }
            else if (urun_adedi.Text != "")
            {
                alanadi = "product_quantity";
                alankosulu = urun_adedi.Text;
                LISTELE1();
            }
        }

        public void LISTELE()
        {
            baglan.Open();
            string kayit = "Select * From products";
            MySqlCommand komut = new MySqlCommand(kayit, baglan);
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
        }
        public void LISTELE1()
        {
            baglan.Open();
            string kayit = "Select * From products where "+alanadi+"='" + alankosulu + "'";
            MySqlCommand komut = new MySqlCommand(kayit, baglan);
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
        }
        public void LISTELE2()
        {
            baglan.Open();
            string kayit = "Select * From products where " + alanadi + "='" + alankosulu + "' and  " + alanadi2 + "='" + alankosulu2 + "'";
            MySqlCommand komut = new MySqlCommand(kayit, baglan);
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
        }
        public void LISTELE3()
        {
            baglan.Open();
            string kayit = "Select * From products where " + alanadi + "='" + alankosulu + "' and  " + alanadi2 + "='" + alankosulu2 + "'and  " + alanadi3 + "='" + alankosulu3 + "'";
            MySqlCommand komut = new MySqlCommand(kayit, baglan);
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LISTELE();
        }
    }
}
