using System;
using System.Collections.Generic;

namespace HastaneOtomasyonu
{   
    class Hasta
    {
        public int HastaID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
    }

    class Randevu
    {
        public int RandevuID { get; set; }
        public int HastaID { get; set; }
        public string DoktorAdi { get; set; }
        public DateTime Tarih { get; set; }
    }

    class Program
    {
        static List<Hasta> Hastalar = new List<Hasta>();
        static List<Randevu> Randevular = new List<Randevu>();
        static int HastaIDCounter = 1;
        static int RandevuIDCounter = 1;

        static void Main(string[] args)
        {
            int secim;

            do
            {
                Console.WriteLine("1. Hasta Ekle");
                Console.WriteLine("2. Randevu Al");
                Console.WriteLine("3. Randevu Listele");
                Console.WriteLine("4. Randevu Sil");
                Console.WriteLine("5. Çıkış");
                Console.Write("Seçiminizi yapın: ");
                secim = int.Parse(Console.ReadLine());

                switch (secim)
                {
                    case 1:
                        HastaEkle();
                        break;
                    case 2:
                        RandevuAl();
                        break;
                    case 3:
                        RandevulariListele();
                        break;
                    case 4:
                        RandevuSil();
                        break;
                    case 5:
                        Console.WriteLine("Programdan çıkılıyor...");
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }

            } while (secim != 5);
        }

        static void HastaEkle()
        {
            Hasta yeniHasta = new Hasta();
            yeniHasta.HastaID = HastaIDCounter++;
            Console.Write("Ad: ");
            yeniHasta.Ad = Console.ReadLine();
            Console.Write("Soyad: ");
            yeniHasta.Soyad = Console.ReadLine();
            Console.Write("Telefon: ");
            yeniHasta.Telefon = Console.ReadLine();
            Hastalar.Add(yeniHasta);
            Console.WriteLine("Hasta başarıyla eklendi.");
        }

        static void RandevuAl()
        {
            Console.WriteLine("Hasta ID'lerini listelemek için 1'e, devam etmek için herhangi bir tuşa basın.");
            if (Console.ReadLine() == "1")
            {
                foreach (var Hasta in Hastalar)
                {
                    Console.WriteLine($"ID: {Hasta.HastaID}, Ad: {Hasta.Ad} {Hasta.Soyad}");
                }
            }

            Console.Write("Randevu alınacak Hasta ID: ");
            int hastaID = int.Parse(Console.ReadLine());

            var hasta = Hastalar.Find(h => h.HastaID == hastaID);
            if (hasta == null)
            {
                Console.WriteLine("Geçersiz Hasta ID!");
                return;
            }

            Randevu yeniRandevu = new Randevu();
            yeniRandevu.RandevuID = RandevuIDCounter++;
            yeniRandevu.HastaID = hastaID;

            Console.Write("Doktor Adı: ");
            yeniRandevu.DoktorAdi = Console.ReadLine();

            Console.Write("Randevu Tarihi (yyyy-MM-dd): ");
            yeniRandevu.Tarih = DateTime.Parse(Console.ReadLine());

            Randevular.Add(yeniRandevu);
            Console.WriteLine("Randevu başarıyla alındı.");
        }

        static void RandevulariListele()
        {
            if (Randevular.Count == 0)
            {
                Console.WriteLine("Hiç randevu yok.");
                return;
            }

            foreach (var randevu in Randevular)
            {
                var hasta = Hastalar.Find(h => h.HastaID == randevu.HastaID);
                Console.WriteLine($"Randevu ID: {randevu.RandevuID}, Hasta: {hasta.Ad} {hasta.Soyad}, Doktor: {randevu.DoktorAdi}, Tarih: {randevu.Tarih.ToShortDateString()}");
            }
        }

        static void RandevuSil()
        {
            Console.Write("Silinecek Randevu ID: ");
            int randevuID = int.Parse(Console.ReadLine());

            var randevu = Randevular.Find(r => r.RandevuID == randevuID);
            if (randevu == null)
            {
                Console.WriteLine("Geçersiz Randevu ID!");
                return;
            }

            Randevular.Remove(randevu);
            Console.WriteLine("Randevu başarıyla silindi.");
        }
    }
}