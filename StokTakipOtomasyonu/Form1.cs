using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace StokTakipOtomasyonu
{
    public partial class Form1 : Form
    {
        // --- VERİ LİSTELERİ ---
        public List<Urun> Urunler = new List<Urun>();
        public List<Musteri> Musteriler = new List<Musteri>();
        public List<Satis> Satislar = new List<Satis>();

        //  SENİN MASAÜSTÜ KLASÖRÜN
        string resimKlasoru = @"C:\Users\chefl\Desktop\StokFotolari";

        // ---  RENKLER ---
        Color primaryColor = Color.FromArgb(44, 62, 80);
        Color secondaryColor = Color.FromArgb(52, 152, 219);
        Color accentColor = Color.FromArgb(231, 76, 60);
        Color successColor = Color.FromArgb(39, 174, 96); // Yeşil (Yeni)
        Color warningColor = Color.FromArgb(241, 196, 15); // Sarı (Yeni)
        Color bgColor = Color.FromArgb(236, 240, 241);
        Color textColor = Color.FromArgb(44, 62, 80);
        Font mainFont = new Font("Segoe UI", 10, FontStyle.Regular);
        Font titleFont = new Font("Segoe UI", 12, FontStyle.Bold);
        Font bigNumberFont = new Font("Segoe UI", 24, FontStyle.Bold); // Dashboard sayıları için

        // --- ARAYÜZ ELEMANLARI ---
        TabControl sekmeler = new TabControl();

        // Tab 0 (Yeni): Dashboard
        Label lblDashToplamUrunSayi, lblDashKritikStokSayi, lblDashToplamSatisTutar, lblDashToplamBorcTutar;

        // Tab 1: Ürün
        TextBox txtUrunAd = new TextBox(); TextBox txtFiyat = new TextBox(); NumericUpDown numStok = new NumericUpDown();
        PictureBox picUrunOnizleme = new PictureBox(); Button btnResimSec = new Button(); Button btnUrunEkle = new Button(); Button btnSifirla = new Button();
        DataGridView gridUrunler = new DataGridView(); string geciciResimYolu = "";

        // Tab 2: Müşteri
        TextBox txtMusteriAd = new TextBox(); TextBox txtTelefon = new TextBox(); TextBox txtMusteriNot = new TextBox();
        Button btnMusteriEkle = new Button(); DataGridView gridMusteriler = new DataGridView();

        // Tab 3: Satış
        FlowLayoutPanel panelUrunGaleri = new FlowLayoutPanel(); ComboBox cmbMusteriSec = new ComboBox(); Label lblSecilenUrun = new Label();
        NumericUpDown numSatisAdet = new NumericUpDown(); Button btnSatisYap = new Button(); Label lblTutar = new Label(); DataGridView gridSatislar = new DataGridView();
        Urun satisIcinSecilenUrun = null;

        public Form1()
        {
            InitializeComponent();
            VerileriYukle();
            AyarlariYap();
            ArayuzuOlustur();
            DashboardGuncelle(); // Açılışta verileri hesapla
        }

        // --- KAYIT/YÜKLEME (V14 ile Aynı) ---
        private void VerileriKaydet()
        {
            try
            {
                XmlSerializer xmlUrun = new XmlSerializer(typeof(List<Urun>)); using (StreamWriter sw = new StreamWriter("urunler.xml")) xmlUrun.Serialize(sw, Urunler);
                XmlSerializer xmlMusteri = new XmlSerializer(typeof(List<Musteri>)); using (StreamWriter sw = new StreamWriter("musteriler.xml")) xmlMusteri.Serialize(sw, Musteriler);
                XmlSerializer xmlSatis = new XmlSerializer(typeof(List<Satis>)); using (StreamWriter sw = new StreamWriter("satislar.xml")) xmlSatis.Serialize(sw, Satislar);
            }
            catch { }
        }
        private void VerileriYukle()
        {
            try
            {
                if (File.Exists("urunler.xml")) { XmlSerializer xml = new XmlSerializer(typeof(List<Urun>)); using (StreamReader sr = new StreamReader("urunler.xml")) Urunler = (List<Urun>)xml.Deserialize(sr); } else VarsayilanlariYukle();
                if (File.Exists("musteriler.xml")) { XmlSerializer xml = new XmlSerializer(typeof(List<Musteri>)); using (StreamReader sr = new StreamReader("musteriler.xml")) Musteriler = (List<Musteri>)xml.Deserialize(sr); } else VarsayilanlariYukle();
                if (File.Exists("satislar.xml")) { XmlSerializer xml = new XmlSerializer(typeof(List<Satis>)); using (StreamReader sr = new StreamReader("satislar.xml")) Satislar = (List<Satis>)xml.Deserialize(sr); }
            }
            catch { VarsayilanlariYukle(); }
        }
        private void VarsayilanlariYukle()
        {
            if (Musteriler.Count == 0) { 
                Musteriler.Add(new Musteri { AdSoyad = "Habip Akkoyun", Telefon = "0566 666 66 66", Not = "vip" }); 
                Musteriler.Add(new Musteri { AdSoyad = "Mert Yılmaz", Telefon = "0511 111 11 11", Not = "Standart" });
                Musteriler.Add(new Musteri { AdSoyad = "Berdan Bal", Telefon = "0522 222 22 22", Not = "vip" });
                Musteriler.Add(new Musteri { AdSoyad = "Mert Yılmaz", Telefon = "0533 333 33 33", Not = "Standart" });
                Musteriler.Add(new Musteri { AdSoyad = "Kerim Özyıldız", Telefon = "0555 555 55 55", Not = "vip" });
                Musteriler.Add(new Musteri { AdSoyad = "Ayşe Demir", Telefon = "0544 444 44 44", Not = "Standart" }); }
            if (Urunler.Count == 0) { Urunler.Add(new Urun { Ad = "Gaming Laptop", Fiyat = 45000, Stok = 5, ResimDosyaAdi = "laptop.jpg", KlasorYolu = resimKlasoru }); Urunler.Add(new Urun { Ad = "Mekanik Klavye", Fiyat = 2500, Stok = 20, ResimDosyaAdi = "klavye.jpg", KlasorYolu = resimKlasoru }); Urunler.Add(new Urun { Ad = "Kablosuz Mouse", Fiyat = 1200, Stok = 50, ResimDosyaAdi = "mouse.jpg", KlasorYolu = resimKlasoru }); Urunler.Add(new Urun { Ad = "Oyuncu Monitörü", Fiyat = 8500, Stok = 10, ResimDosyaAdi = "monitor.jpg", KlasorYolu = resimKlasoru }); Urunler.Add(new Urun { Ad = "Kulaklık", Fiyat = 3000, Stok = 3, ResimDosyaAdi = "kulaklik.jpg", KlasorYolu = resimKlasoru }); }
        }

        // --- GÖRSEL AYARLAR ---
        private void AyarlariYap() { this.Text = "Stok Takip V15 (Profesyonel Dashboard)"; this.Size = new Size(1150, 780); this.StartPosition = FormStartPosition.CenterScreen; this.Font = mainFont; this.BackColor = bgColor; }
        private void StilButon(Button btn, Color renk) { btn.FlatStyle = FlatStyle.Flat; btn.FlatAppearance.BorderSize = 0; btn.BackColor = renk; btn.ForeColor = Color.White; btn.Font = new Font("Segoe UI", 10, FontStyle.Bold); btn.Cursor = Cursors.Hand; }
        private void StilGrid(DataGridView grid) { grid.BackgroundColor = Color.White; grid.BorderStyle = BorderStyle.None; grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; grid.EnableHeadersVisualStyles = false; grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; grid.ColumnHeadersDefaultCellStyle.BackColor = primaryColor; grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold); grid.ColumnHeadersHeight = 40; grid.DefaultCellStyle.SelectionBackColor = secondaryColor; grid.DefaultCellStyle.SelectionForeColor = Color.White; grid.DefaultCellStyle.Font = mainFont; grid.RowHeadersVisible = false; grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; grid.RowTemplate.Height = 60; }

        // --- 🆕 DASHBOARD KART OLUŞTURUCU (Yardımcı Metot) ---
        private Panel DashboardKartiOlustur(string baslik, out Label lblSayi, Color renk, Point konum)
        {
            Panel pnl = new Panel { Size = new Size(250, 150), Location = konum, BackColor = renk, Padding = new Padding(15) };
            Label lblBaslik = new Label { Text = baslik, ForeColor = Color.WhiteSmoke, Font = titleFont, Dock = DockStyle.Top, AutoSize = false, Height = 30 };
            lblSayi = new Label { Text = "0", ForeColor = Color.White, Font = bigNumberFont, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter };
            pnl.Controls.Add(lblSayi); pnl.Controls.Add(lblBaslik);
            return pnl;
        }

        private void ArayuzuOlustur()
        {
            sekmeler.Dock = DockStyle.Fill; sekmeler.Font = new Font("Segoe UI", 11);

            // --- 🆕 TAB 0: DASHBOARD (Özet Paneli) ---
            TabPage tabDash = new TabPage("📊 Genel Bakış");
            tabDash.BackColor = bgColor;
            Panel dashContainer = new Panel { Location = new Point(30, 30), Size = new Size(1080, 650) };

            dashContainer.Controls.Add(DashboardKartiOlustur("Toplam Ürün Çeşidi", out lblDashToplamUrunSayi, secondaryColor, new Point(0, 0)));
            dashContainer.Controls.Add(DashboardKartiOlustur("Kritik Stoklu Ürünler ⚠️", out lblDashKritikStokSayi, accentColor, new Point(270, 0)));
            dashContainer.Controls.Add(DashboardKartiOlustur("Toplam Satış Tutarı", out lblDashToplamSatisTutar, successColor, new Point(540, 0)));
            dashContainer.Controls.Add(DashboardKartiOlustur("Toplam Müşteri Borcu", out lblDashToplamBorcTutar, warningColor, new Point(810, 0)));

            Label lblHosgeldin = new Label { Text = "Stok Takip Sistemine Hoşgeldiniz", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = primaryColor, Location = new Point(0, 180), AutoSize = true };
            dashContainer.Controls.Add(lblHosgeldin);
            tabDash.Controls.Add(dashContainer);

            // TAB 1: ÜRÜN (V14 ile Aynı)
            TabPage tabUrun = new TabPage("📦 Ürün Yönetimi"); 
            tabUrun.BackColor = Color.White;
            Panel panelUrunForm = new Panel() { Location = new Point(20, 20), Size = new Size(1080, 180), BackColor = bgColor, Padding = new Padding(20) };
            Label l1 = new Label() { Text = "Ürün Adı:", Location = new Point(20, 30), AutoSize = true, ForeColor = textColor }; txtUrunAd.Location = new Point(120, 25); txtUrunAd.Width = 250; txtUrunAd.Font = mainFont;
            Label l2 = new Label() { Text = "Fiyat (TL):", Location = new Point(20, 75), AutoSize = true, ForeColor = textColor }; txtFiyat.Location = new Point(120, 70); txtFiyat.Width = 250; txtFiyat.Font = mainFont;
            Label l3 = new Label() { Text = "Stok Adedi:", Location = new Point(20, 120), AutoSize = true, ForeColor = textColor }; numStok.Location = new Point(120, 115); numStok.Width = 250; numStok.Font = mainFont;
            picUrunOnizleme.Location = new Point(400, 25); picUrunOnizleme.Size = new Size(120, 120); picUrunOnizleme.BorderStyle = BorderStyle.FixedSingle; picUrunOnizleme.SizeMode = PictureBoxSizeMode.StretchImage; picUrunOnizleme.BackColor = Color.White;
            btnResimSec.Text = "Resim Seç"; btnResimSec.Location = new Point(530, 25); btnResimSec.Size = new Size(120, 40); StilButon(btnResimSec, secondaryColor); btnResimSec.Click += BtnResimSec_Click;
            btnUrunEkle.Text = "ÜRÜNÜ KAYDET"; btnUrunEkle.Location = new Point(530, 105); btnUrunEkle.Size = new Size(180, 40); StilButon(btnUrunEkle, primaryColor); btnUrunEkle.Click += BtnUrunEkle_Click;
            btnSifirla.Text = "SİSTEMİ SIFIRLA 🗑️"; btnSifirla.Location = new Point(850, 105); btnSifirla.Size = new Size(180, 40); StilButon(btnSifirla, accentColor); btnSifirla.Click += BtnSifirla_Click;
            panelUrunForm.Controls.Add(l1); panelUrunForm.Controls.Add(txtUrunAd); panelUrunForm.Controls.Add(l2); panelUrunForm.Controls.Add(txtFiyat); panelUrunForm.Controls.Add(l3); panelUrunForm.Controls.Add(numStok); panelUrunForm.Controls.Add(picUrunOnizleme); panelUrunForm.Controls.Add(btnResimSec); panelUrunForm.Controls.Add(btnUrunEkle); panelUrunForm.Controls.Add(btnSifirla);
            gridUrunler.Location = new Point(20, 220); gridUrunler.Size = new Size(1080, 450); StilGrid(gridUrunler); gridUrunler.RowTemplate.Height = 80;
            tabUrun.Controls.Add(panelUrunForm); tabUrun.Controls.Add(gridUrunler);

            // TAB 2: MÜŞTERİ (V14 ile Aynı)
            TabPage tabMusteri = new TabPage("👥 Müşteriler"); tabMusteri.BackColor = Color.White;
            Panel panelMusteriForm = new Panel() { Location = new Point(20, 20), Size = new Size(1080, 180), BackColor = bgColor, Padding = new Padding(20) };
            Label lm1 = new Label() { Text = "Adı Soyadı:", Location = new Point(20, 30), AutoSize = true, ForeColor = textColor }; txtMusteriAd.Location = new Point(120, 25); txtMusteriAd.Width = 300; txtMusteriAd.Font = mainFont;
            Label lm2 = new Label() { Text = "Telefon:", Location = new Point(20, 75), AutoSize = true, ForeColor = textColor }; txtTelefon.Location = new Point(120, 70); txtTelefon.Width = 300; txtTelefon.Font = mainFont;
            Label lm3 = new Label() { Text = "Müşteri Notu:", Location = new Point(20, 120), AutoSize = true, ForeColor = textColor }; txtMusteriNot.Location = new Point(120, 115); txtMusteriNot.Width = 300; txtMusteriNot.Font = mainFont;
            btnMusteriEkle.Text = "MÜŞTERİ EKLE"; btnMusteriEkle.Location = new Point(450, 115); btnMusteriEkle.Size = new Size(200, 40); StilButon(btnMusteriEkle, Color.SeaGreen); btnMusteriEkle.Click += BtnMusteriEkle_Click;
            panelMusteriForm.Controls.Add(lm1); panelMusteriForm.Controls.Add(txtMusteriAd); panelMusteriForm.Controls.Add(lm2); panelMusteriForm.Controls.Add(txtTelefon); panelMusteriForm.Controls.Add(lm3); panelMusteriForm.Controls.Add(txtMusteriNot); panelMusteriForm.Controls.Add(btnMusteriEkle);
            gridMusteriler.Location = new Point(20, 220); gridMusteriler.Size = new Size(1080, 450); StilGrid(gridMusteriler);
            tabMusteri.Controls.Add(panelMusteriForm); tabMusteri.Controls.Add(gridMusteriler);

            // TAB 3: SATIŞ (V14 ile Aynı)
            TabPage tabSatis = new TabPage("🛒 Ürün Satışı"); tabSatis.BackColor = Color.White;
            Label lblGaleri = new Label() { Text = "ÜRÜN GALERİSİ", Location = new Point(20, 15), AutoSize = true, Font = titleFont, ForeColor = primaryColor };
            panelUrunGaleri.Location = new Point(20, 50); panelUrunGaleri.Size = new Size(600, 620); panelUrunGaleri.BackColor = bgColor; panelUrunGaleri.AutoScroll = true; panelUrunGaleri.BorderStyle = BorderStyle.None;
            GroupBox grupIslem = new GroupBox() { Text = "Satış İşlemi", Location = new Point(640, 50), Size = new Size(460, 620), Font = mainFont, ForeColor = primaryColor };
            Label ls1 = new Label() { Text = "Müşteri Seç:", Location = new Point(20, 40), AutoSize = true, ForeColor = textColor }; cmbMusteriSec.Location = new Point(20, 65); cmbMusteriSec.Width = 420; cmbMusteriSec.DropDownStyle = ComboBoxStyle.DropDownList; cmbMusteriSec.Font = mainFont; cmbMusteriSec.SelectedIndexChanged += (s, e) => FiyatHesapla();
            Label ls2 = new Label() { Text = "Seçili Ürün:", Location = new Point(20, 110), AutoSize = true, ForeColor = textColor }; lblSecilenUrun.Text = "Lütfen bir ürün seçin..."; lblSecilenUrun.Location = new Point(20, 135); lblSecilenUrun.ForeColor = accentColor; lblSecilenUrun.Font = titleFont; lblSecilenUrun.AutoSize = true;
            Label ls3 = new Label() { Text = "Adet:", Location = new Point(20, 180), AutoSize = true, ForeColor = textColor }; numSatisAdet.Location = new Point(20, 205); numSatisAdet.Width = 100; numSatisAdet.Minimum = 1; numSatisAdet.Value = 1; numSatisAdet.Font = titleFont; numSatisAdet.ValueChanged += (s, e) => FiyatHesapla();
            lblTutar.Text = "TUTAR: 0.00 TL"; lblTutar.Location = new Point(140, 205); lblTutar.Font = new Font("Segoe UI", 18, FontStyle.Bold); lblTutar.ForeColor = primaryColor; lblTutar.AutoSize = true;
            btnSatisYap.Text = "SATIŞI TAMAMLA"; btnSatisYap.Location = new Point(20, 260); btnSatisYap.Size = new Size(420, 50); StilButon(btnSatisYap, Color.DarkOrange); btnSatisYap.Click += BtnSatisYap_Click;
            gridSatislar.Location = new Point(20, 330); gridSatislar.Size = new Size(420, 270); StilGrid(gridSatislar); gridSatislar.RowTemplate.Height = 40;
            grupIslem.Controls.Add(ls1); grupIslem.Controls.Add(cmbMusteriSec); grupIslem.Controls.Add(ls2); grupIslem.Controls.Add(lblSecilenUrun); grupIslem.Controls.Add(ls3); grupIslem.Controls.Add(numSatisAdet); grupIslem.Controls.Add(lblTutar); grupIslem.Controls.Add(btnSatisYap); grupIslem.Controls.Add(gridSatislar);
            tabSatis.Controls.Add(lblGaleri); tabSatis.Controls.Add(panelUrunGaleri); tabSatis.Controls.Add(grupIslem);

            sekmeler.TabPages.Add(tabDash); // 🆕 Dashboard en başa eklendi
            sekmeler.TabPages.Add(tabUrun); sekmeler.TabPages.Add(tabMusteri); sekmeler.TabPages.Add(tabSatis);
            this.Controls.Add(sekmeler);

            GridGuncelle(gridUrunler, Urunler); GridGuncelle(gridSatislar, Satislar); MusteriGridGuncelle(); GaleriYenile(); ComboBoxlariDoldur();
        }

        //DASHBOARD GÜNCELLEME METODU
        private void DashboardGuncelle(){
            //1. Toplam Ürün Çeşidi
            lblDashToplamUrunSayi.Text = Urunler.Count.ToString();
            //2. Kritik Stok (5'ten az olanlar)
            int kritikSayi = Urunler.Count(u => u.Stok < 5);
            lblDashKritikStokSayi.Text = kritikSayi.ToString();
            lblDashKritikStokSayi.ForeColor = kritikSayi > 0 ? Color.Yellow : Color.White;
            //3. Toplam Satış Tutarı                            //Kritik varsa sarı yap
            decimal toplamSatis = Satislar.Sum(s => s.Tutar);
            lblDashToplamSatisTutar.Text = toplamSatis.ToString("N0") + " ₺";
            //4. Toplam Müşteri Borcu
            decimal toplamBorc = Musteriler.Sum(m => m.Borc);
            lblDashToplamBorcTutar.Text = toplamBorc.ToString("N0") + " ₺";
        }


        // --- OLAYLAR (Events) ---
        private void BtnSifirla_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tüm veriler silinecek! Emin misiniz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (File.Exists("urunler.xml")) File.Delete("urunler.xml"); if (File.Exists("musteriler.xml")) File.Delete("musteriler.xml"); if (File.Exists("satislar.xml")) File.Delete("satislar.xml");
                Urunler.Clear(); Musteriler.Clear(); Satislar.Clear();
                VarsayilanlariYukle(); VerileriKaydet();
                GridGuncelle(gridUrunler, Urunler); GridGuncelle(gridSatislar, Satislar); MusteriGridGuncelle(); GaleriYenile(); ComboBoxlariDoldur();
                DashboardGuncelle(); // 🆕 Sıfırlanınca dashboardu da yenile
                MessageBox.Show("Sistem sıfırlandı!");
            }
        }

        private void BtnResimSec_Click(object sender, EventArgs e) { OpenFileDialog dosya = new OpenFileDialog(); dosya.Filter = "Resimler|*.jpg;*.jpeg;*.png"; if (dosya.ShowDialog() == DialogResult.OK) { geciciResimYolu = dosya.FileName; try { using (var fs = new FileStream(geciciResimYolu, FileMode.Open, FileAccess.Read)) { picUrunOnizleme.Image = Image.FromStream(fs); } } catch { MessageBox.Show("Resim hatası!"); } } }
        private void BtnUrunEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUrunAd.Text)) return;
            string yeniDosyaAdi = ""; if (!string.IsNullOrEmpty(geciciResimYolu)) { string uzanti = Path.GetExtension(geciciResimYolu); yeniDosyaAdi = Guid.NewGuid().ToString() + uzanti; try { File.Copy(geciciResimYolu, Path.Combine(resimKlasoru, yeniDosyaAdi), true); } catch { } }
            Urunler.Add(new Urun { Ad = txtUrunAd.Text, Fiyat = decimal.TryParse(txtFiyat.Text, out decimal f) ? f : 0, Stok = (int)numStok.Value, ResimDosyaAdi = yeniDosyaAdi, KlasorYolu = resimKlasoru });
            txtUrunAd.Clear(); txtFiyat.Clear(); numStok.Value = 0; picUrunOnizleme.Image = null; geciciResimYolu = "";
            VerileriKaydet(); GridGuncelle(gridUrunler, Urunler); GaleriYenile();
            DashboardGuncelle(); // 🆕 Yeni ürün eklenince dashboardu yenile
            MessageBox.Show("Ürün Eklendi!");
        }

        private void BtnMusteriEkle_Click(object sender, EventArgs e) { 
            Musteriler.Add(new Musteri { AdSoyad = txtMusteriAd.Text, Telefon = txtTelefon.Text, Not = txtMusteriNot.Text, Borc = 0 });
            VerileriKaydet(); MusteriGridGuncelle(); ComboBoxlariDoldur(); 
            txtMusteriAd.Clear(); txtTelefon.Clear(); txtMusteriNot.Clear(); 
            MessageBox.Show("Müşteri Kaydedildi."); }
        private void GaleriYenile() { panelUrunGaleri.Controls.Clear(); foreach (var urun in Urunler) { Panel kart = new Panel(); kart.Size = new Size(180, 250); kart.BackColor = Color.White; kart.BorderStyle = BorderStyle.None; kart.Margin = new Padding(10); kart.Cursor = Cursors.Hand; kart.Click += (s, e) => UrunSec(urun); PictureBox pic = new PictureBox(); pic.Size = new Size(160, 140); pic.Location = new Point(10, 10); pic.SizeMode = PictureBoxSizeMode.Zoom; pic.Click += (s, e) => UrunSec(urun); string yol = Path.Combine(resimKlasoru, urun.ResimDosyaAdi); if (File.Exists(yol)) { try { using (var fs = new FileStream(yol, FileMode.Open, FileAccess.Read)) { pic.Image = Image.FromStream(fs); } } catch { pic.BackColor = accentColor; } } else pic.BackColor = Color.Gray; kart.Controls.Add(pic); Label lblAd = new Label { Text = urun.Ad, Location = new Point(10, 160), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = primaryColor }; Label lblFiyat = new Label { Text = urun.Fiyat.ToString("N2") + " TL", Location = new Point(10, 185), ForeColor = secondaryColor, Font = new Font("Segoe UI", 10, FontStyle.Bold) }; Label lblStok = new Label { Text = "Stok: " + urun.Stok, Location = new Point(10, 210), ForeColor = urun.Stok < 5 ? accentColor : textColor }; if (urun.Stok < 5) lblStok.Text += " ⚠️"; lblAd.Click += (s, e) => UrunSec(urun); lblFiyat.Click += (s, e) => UrunSec(urun); kart.Controls.Add(lblAd); kart.Controls.Add(lblFiyat); kart.Controls.Add(lblStok); panelUrunGaleri.Controls.Add(kart); } }
        private void UrunSec(Urun urun) { satisIcinSecilenUrun = urun; lblSecilenUrun.Text = urun.Ad; lblSecilenUrun.ForeColor = primaryColor; FiyatHesapla(); }
        private void BtnSatisYap_Click(object sender, EventArgs e)
        {
            if (cmbMusteriSec.SelectedItem == null || satisIcinSecilenUrun == null) return; if (satisIcinSecilenUrun.Stok < numSatisAdet.Value) { MessageBox.Show("Yetersiz Stok!"); return; }
            Musteri m = (Musteri)cmbMusteriSec.SelectedItem; int adet = (int)numSatisAdet.Value; decimal birimFiyat = satisIcinSecilenUrun.Fiyat; bool vipMi = m.Not.ToLower().Contains("vip"); if (vipMi) birimFiyat = birimFiyat * 0.95m;
            decimal tutar = birimFiyat * adet; satisIcinSecilenUrun.Stok -= adet; m.Borc += tutar;
            Satislar.Add(new Satis { Musteri = m.AdSoyad, Urun = satisIcinSecilenUrun.Ad, Tutar = tutar });
            VerileriKaydet(); GridGuncelle(gridSatislar, Satislar); GaleriYenile(); MusteriGridGuncelle();
            DashboardGuncelle(); // 🆕 Satış yapılınca dashboardu yenile
            try { File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SonSatisFisi.txt"), $"--- FİŞ ---\n{DateTime.Now}\nMüşteri: {m.AdSoyad}\nÜrün: {satisIcinSecilenUrun.Ad}\nAdet: {adet}\nTutar: {tutar:N2}"); MessageBox.Show($"Satış Başarılı! Fiş Yazıldı."); } catch { }
        }
        private void FiyatHesapla() { if (satisIcinSecilenUrun != null) { decimal fiyat = satisIcinSecilenUrun.Fiyat; if (cmbMusteriSec.SelectedItem != null) { Musteri m = (Musteri)cmbMusteriSec.SelectedItem; if (m.Not.ToLower().Contains("vip")) { fiyat = fiyat * 0.95m; lblTutar.ForeColor = Color.Green; } else lblTutar.ForeColor = primaryColor; } lblTutar.Text = (fiyat * numSatisAdet.Value).ToString("N2") + " TL"; } }
        private void GridGuncelle(DataGridView grid, object data) { grid.DataSource = null; grid.DataSource = data; try { if (grid == gridUrunler) { if (grid.Columns["ResimDosyaAdi"] != null) grid.Columns["ResimDosyaAdi"].Visible = false; if (grid.Columns["KlasorYolu"] != null) grid.Columns["KlasorYolu"].Visible = false; if (grid.Columns.Contains("Gorsel")) { grid.Columns["Gorsel"].HeaderText = ""; grid.Columns["Gorsel"].Width = 80; if (grid.Columns["Gorsel"] is DataGridViewImageColumn imgCol) imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom; } foreach (DataGridViewRow row in grid.Rows) { if (Convert.ToInt32(row.Cells["Stok"].Value) < 5) row.DefaultCellStyle.BackColor = Color.FromArgb(250, 219, 216); } } if (grid.Columns["Fiyat"] != null) grid.Columns["Fiyat"].DefaultCellStyle.Format = "N2"; if (grid.Columns["Tutar"] != null) grid.Columns["Tutar"].DefaultCellStyle.Format = "N2"; if (grid.Columns["Borc"] != null) grid.Columns["Borc"].DefaultCellStyle.Format = "N2"; } catch { } }
        private void MusteriGridGuncelle() { gridMusteriler.DataSource = null; gridMusteriler.DataSource = Musteriler; }
        private void ComboBoxlariDoldur() { cmbMusteriSec.DataSource = null; cmbMusteriSec.DataSource = Musteriler; cmbMusteriSec.DisplayMember = "AdSoyad"; }
    }

    // --- SINIFLAR (V14 ile Aynı) ---
    public class Urun
    {
        public string Ad { get; set; }
        public decimal Fiyat { get; set; }
        public int Stok { get; set; }
        public string ResimDosyaAdi { get; set; }
        public string KlasorYolu { get; set; }
        [XmlIgnore] public Image Gorsel { get { if (string.IsNullOrEmpty(KlasorYolu) || string.IsNullOrEmpty(ResimDosyaAdi)) return null; string yol = Path.Combine(KlasorYolu, ResimDosyaAdi); if (File.Exists(yol)) { try { using (var fs = new FileStream(yol, FileMode.Open, FileAccess.Read)) { return Image.FromStream(fs); } } catch { return null; } } return null; } }
    }
    public class Musteri { public string AdSoyad { get; set; } public string Telefon { get; set; } public string Not { get; set; } = ""; public decimal Borc { get; set; } }
    public class Satis { public DateTime Tarih { get; set; } = DateTime.Now; public string Musteri { get; set; } public string Urun { get; set; } public decimal Tutar { get; set; } }
}