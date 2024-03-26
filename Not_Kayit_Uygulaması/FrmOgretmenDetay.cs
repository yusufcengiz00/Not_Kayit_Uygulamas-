using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Not_Kayit_Uygulaması
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=Cengiz\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=True;");

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO TBLDERS(OGRNUMARA, OGRAD, OGRSOYAD) values (@p1 , @p2 , @p3 )",baglanti);
            cmd.Parameters.AddWithValue("@p1", maskNumara.Text);
            cmd.Parameters.AddWithValue("@p2", textBoxAd.Text);
            cmd.Parameters.AddWithValue("@p3", textBoxSoyad.Text);

            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            maskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBoxAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textBoxSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            textBoxSınav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            textBoxSınav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            textBoxSınav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void buttonGuncelle_Click(object sender, EventArgs e)
        {
            double ortalama , s1 , s2 , s3 ;
            string durum;

            s1= Convert.ToDouble(textBoxSınav1.Text);
            s2 = Convert.ToDouble(textBoxSınav2.Text);
            s3 = Convert.ToDouble(textBoxSınav3.Text);

            ortalama = (s1 + s2 + s3) / 3;
            lblOrt.Text = ortalama.ToString();

            if (ortalama >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            baglanti.Open();
            SqlCommand cmd = new SqlCommand("UPDATE TBLDERS set OGRS1=@p1 , OGRS2=@p2 , OGRS3=@p3 , ORTALAMA=@p4 , DURUM=@p5 WHERE OGRNUMARA = @p6",baglanti );

            cmd.Parameters.AddWithValue("@p1", textBoxSınav1.Text);
            cmd.Parameters.AddWithValue("@p2", textBoxSınav2.Text);
            cmd.Parameters.AddWithValue("@p3", textBoxSınav3.Text);
            cmd.Parameters.AddWithValue("@p4", decimal.Parse( lblOrt.Text) );
            cmd.Parameters.AddWithValue("@p5", durum);
            cmd.Parameters.AddWithValue("@p6", maskNumara.Text);

            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
        }
    }
}
