using System;

public class HesapMakinesi
{
    public static void Main(string[] args)
    {
        double deger1 = 0, deger2 = 0, sonuc;
        int secim;

        while (true)
        {
            Console.WriteLine("\n**********Hesap Makinesi***********");
            Console.WriteLine("----------------");
            Console.WriteLine("1. Toplama");
            Console.WriteLine("2. Cikarma");
            Console.WriteLine("3. Carpma");
            Console.WriteLine("4. Bolme");
            Console.WriteLine("5. Kare Alma");
            Console.WriteLine("6. Karekok Hesaplama");
            Console.WriteLine("7. Cikis");

            Console.Write("Seciminizi girin (1-7): ");

            if (!int.TryParse(Console.ReadLine(), out secim))
            {
                Console.WriteLine("Gecersiz giris. Lutfen bir sayi girin.");
                continue; // Döngünün başına döner
            }

            if (secim == 7)
            {
                Console.WriteLine("Cikis yapiliyor...");
                break; // Döngüden çıkar
            }

            if (secim < 1 || secim > 6)
            {
                Console.WriteLine("Gecersiz secim. Lutfen 1-7 arasinda bir sayi girin.");
                continue;
            }

            Console.Write("Birinci sayiyi girin: ");
            if (!double.TryParse(Console.ReadLine(), out deger1))
            {
                Console.WriteLine("Gecersiz sayi girisi.");
                continue;
            }

            if (secim != 5 && secim != 6) // Kare alma ve karekök için ikinci sayı gerekmez
            {
                Console.Write("Ikinci sayiyi girin: ");
                if (!double.TryParse(Console.ReadLine(), out deger2))
                {
                    Console.WriteLine("Gecersiz sayi girisi.");
                    continue;
                }
            }

            switch (secim)
            {
                case 1:
                    sonuc = deger1 + deger2;
                    Console.WriteLine($"{deger1} + {deger2} = {sonuc}");
                    break;
                case 2:
                    sonuc = deger1 - deger2;
                    Console.WriteLine($"{deger1} - {deger2} = {sonuc}");
                    break;
                case 3:
                    sonuc = deger1 * deger2;
                    Console.WriteLine($"{deger1} * {deger2} = {sonuc}");
                    break;
                case 4:
                    if (deger2 == 0)
                    {
                        Console.WriteLine("0'a bolme hatasi!");
                    }
                    else
                    {
                        sonuc = deger1 / deger2;
                        Console.WriteLine($"{deger1} / {deger2} = {sonuc}");
                    }
                    break;
                case 5:
                    sonuc = Math.Pow(deger1, 2);
                    Console.WriteLine($"{deger1}'in karesi = {sonuc}");
                    break;
                case 6:
                    if (deger1 < 0)
                    {
                        Console.WriteLine("Negatif sayinin karekoku hesaplanamaz!");
                    }
                    else
                    {
                        sonuc = Math.Sqrt(deger1);
                        Console.WriteLine($"{deger1}'in karekoku = {sonuc}");
                    }
                    break;

            }
        }
    }
}