using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YILANOYUNU
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            
            InitializeComponent();
        }

        yilan yilanimiz = new yilan();
        yon yonumuz;// yön değerini null olarak gördüğü için hata veriyor bu yüzden başlangıç değeri belirlemeliyiz
        bool yem_varmi = false;
        Random rastg = new Random();
        PictureBox pb_yem;
        int skor = 0;
        PictureBox[] pb_yilanparcalari; //bakşa bir yerde ya da fonsiyomda kullanmak istediğimiz için yeni dizi üretttiğimiz kısımdan önce diziye isim veriyoruz
                                        // 0 boyutlu bir dizi yarattık


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            yeni_oyun();
        }
        private void yeni_oyun()
        {
            yem_varmi = false;
            skor = 0;
            yilanimiz = new yilan();
            yonumuz = new yon(-10, 0);
            pb_yilanparcalari = new PictureBox[0];
            for (int i = 0; i < 3; i++)
            {
                Array.Resize(ref pb_yilanparcalari, pb_yilanparcalari.Length + 1);
                pb_yilanparcalari[i] = Pb_ekle();
            }
            timer1.Start();
            button1.Enabled = false;
            button2.Enabled = false;
        }
        private PictureBox Pb_ekle()
        {
           
            PictureBox Pb = new PictureBox();
            Pb.Size = new Size(10, 10);
            Pb.BackColor = Color.White;
            Pb.Location = yilanimiz.GetPos(pb_yilanparcalari.Length - 1);
            panel1.Controls.Add(Pb);
            return Pb;

        }
        private void pb_guncelle()
        {
            for (int i = 0; i < pb_yilanparcalari.Length ; i++)
            {
                pb_yilanparcalari[i].Location = yilanimiz.GetPos(i);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                if (yonumuz._y != 10) //buradaki if leri koyarak yılanın aşağı giderken yukarı dönmesini ve sağa giderken sola dönmesini engelledik
                {
                    yonumuz = new yon(0, -10);
                }
                
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                if (yonumuz._y != -10)
                {
                    yonumuz = new yon(0, 10);
                }
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (yonumuz._x != -10)
                {
                    yonumuz = new yon(10, 0);
                }

            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (yonumuz._x != 10)
                {
                    yonumuz = new yon(-10, 0);
                }
            }
        }
    
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            label1.Text = "SKOR : " + skor.ToString();
            yilanimiz.ilerle(yonumuz);
            pb_guncelle();
            yem_olustur();//yazdığımız her fonksiyondqa çalışması için buraya eklemeliyiz yoksa çalışmaz
            yem_yedi_mi(); //timer her seferinde bunları kontrol edecek
            yılan_kendine_çarptı();
            duvarlara_çarpti();
        }
        
        public void yem_olustur()
        {
            if (!yem_varmi )
            {
                PictureBox pb = new PictureBox();
                pb.Size = new Size(10, 10);
                pb.BackColor = Color.Green;
                pb.Location = new Point(rastg.Next(panel1.Width / 10) * 10, rastg.Next(panel1.Height / 10) * 10);
                pb_yem = pb;
                yem_varmi = true;
                panel1.Controls.Add(pb);
            }

        }
        public void yem_yedi_mi()
        {
            if (yilanimiz.GetPos(0) == pb_yem.Location)  
            {
                //yılanın baş kısmıının konumu yemin konumuna eşitse yemi yedi
                skor += 10;
                yilanimiz.buyu();
                Array.Resize(ref pb_yilanparcalari, pb_yilanparcalari.Length + 1);
                pb_yilanparcalari[pb_yilanparcalari.Length - 1] = Pb_ekle();
                yem_varmi = false;
                panel1.Controls.Remove(pb_yem);

            }
        }
        public void yılan_kendine_çarptı()
        {
            for (int i = 1; i < yilanimiz.Yılan_buyukluğu ; i++)
            {
                if (yilanimiz.GetPos(0) == yilanimiz.GetPos(i))
                {
                    yenildi();
                    //timer ın içinde oluşturmazsak çalışmaz
                }
            }
        }

        public void duvarlara_çarpti()
        {
            Point p = yilanimiz.GetPos(0);
            if (p.X< 0 || p.X > panel1.Width-10 || p.Y <0  || p.Y > panel1.Height-10)
            {
                //timer ın içinde oluşturmazsak çalışmaz
                yenildi();
            }
        }
        private void yenildi()
        {
            timer1.Stop();
            MessageBox.Show("OYUN BİTTİ : YENİLDİN! ");
            button1.Enabled = true;
            button2.Enabled = true;

        }

        private void label1_Click(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            yeni_oyun();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
