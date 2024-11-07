using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SERHATDEMIR\SQLEXPRESS;Initial Catalog=Rehberr;Integrated Security=True;");
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select *from Kisiler",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void temizle()
        {
            txtad.Text = "";
            txtıd.Text = "";
            txtsoyad.Text = "";
            txtmaıl.Text = "";
            msktelefon.Text = "";
            txtad.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            openFileDialog1.Title = "Profil Fotoğrafını Seç";
            openFileDialog1.Filter = "Jpeg Dosyaları(*.jpg)|*.jpg|Png Dosyaları(*.png)|*.png|Gif Dosyaları(*.gif)|*.gif";
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Kisiler(AD,SOYAD,TELEFON,MAIL,PROFILFOTO) values(@P1,@P2,@P3,@P4,@P5)", baglanti);
            komut.Parameters.AddWithValue("@P1", txtad.Text);
            komut.Parameters.AddWithValue("@P2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@P3",msktelefon.Text);
            komut.Parameters.AddWithValue("@P4", txtmaıl.Text);
            komut.Parameters.AddWithValue("@P5", txtprofılfoto.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
               
                

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtıd.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            msktelefon.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtmaıl.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtprofılfoto.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult soru = new DialogResult();
            soru = MessageBox.Show("Seçilen kaydı silmek istediğinize emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soru == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete from Kisiler where ID=" + txtıd.Text, baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kişi sistemden silindi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                listele();
                temizle();
            }
            else
            {

            }

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            DialogResult soru = new DialogResult();
            soru = MessageBox.Show("Seçilen kaydı güncellemek istediğinize emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (soru == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Update Kisiler set AD=@P1,SOYAD=@P2,TELEFON=@P3,MAIL=@P4,PROFILFOTO=@P5 Where ID=@P5", baglanti);
                komut.Parameters.AddWithValue("@P1", txtad.Text);
                komut.Parameters.AddWithValue("@P2", txtsoyad.Text);
                komut.Parameters.AddWithValue("@P3", msktelefon.Text);
                komut.Parameters.AddWithValue("@P4", txtmaıl.Text);
                komut.Parameters.AddWithValue("@P5", txtıd.Text);
                komut.Parameters.AddWithValue("@P6", txtprofılfoto.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kişi sistemde Guncellendi ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
                temizle();
            }
            else
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            txtprofılfoto.Text = openFileDialog1.FileName.ToString();
        }

        private void txtprofılfoto_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = txtprofılfoto.Text;
        }
    }
}
