#include <stdio.h>
#include <stdlib.h>
#include <math.h> // math.h kütüphanesi eklendi

int main() {
    double sayi1, sayi2, sonuc;
    int secim;

    while (1) { // Sonsuz döngü
        printf("\nHesap Makinesi\n");
        printf("--------------\n");
        printf("1. Toplama\n");
        printf("2. Cikarma\n");
        printf("3. Carpma\n");
        printf("4. Bolme\n");
        printf("5. Kare Alma\n");
        printf("6. Karekok Hesaplama\n");
        printf("7. Cikis\n");

        printf("Seciminizi girin (1-7): ");

        if (scanf("%d", &secim) != 1) {
            printf("Gecersiz giris. Lutfen bir sayi girin.\n");
            while (getchar() != '\n'); // Giriş buffer'ını temizle
            continue;
        }

        if (secim == 7) {
            printf("Cikis yapiliyor...\n");
            break;
        }

        if (secim < 1 || secim > 6) {
            printf("Gecersiz secim. Lutfen 1-7 arasinda bir sayi girin.\n");
            continue;
        }

        printf("Birinci sayiyi girin: ");
        if (scanf("%lf", &sayi1) != 1) { // %lf double için
            printf("Gecersiz sayi girisi.\n");
            while (getchar() != '\n');
            continue;
        }

        if (secim != 5 && secim != 6) {
            printf("Ikinci sayiyi girin: ");
            if (scanf("%lf", &sayi2) != 1) {
                printf("Gecersiz sayi girisi.\n");
                while (getchar() != '\n');
                continue;
            }
        }

        switch (secim) {
            case 1:
                sonuc = sayi1 + sayi2;
                printf("%.2lf + %.2lf = %.2lf\n", sayi1, sayi2, sonuc);
                break;
            case 2:
                sonuc = sayi1 - sayi2;
                printf("%.2lf - %.2lf = %.2lf\n", sayi1, sayi2, sonuc);
                break;
            case 3:
                sonuc = sayi1 * sayi2;
                printf("%.2lf * %.2lf = %.2lf\n", sayi1, sayi2, sonuc);
                break;
            case 4:
                if (sayi2 == 0) {
                    printf("0'a bolme hatasi!\n");
                } else {
                    sonuc = sayi1 / sayi2;
                    printf("%.2lf / %.2lf = %.2lf\n", sayi1, sayi2, sonuc);
                }
                break;
            case 5:
                sonuc = pow(sayi1, 2); // pow fonksiyonu
                printf("%.2lf'nin karesi = %.2lf\n", sayi1, sonuc);
                break;
            case 6:
                if (sayi1 < 0) {
                    printf("Negatif sayinin karekoku hesaplanamaz!\n");
                } else {
                    sonuc = sqrt(sayi1); // sqrt fonksiyonu
                    printf("%.2lf'nin karekoku = %.2lf\n", sayi1, sonuc);
                }
                break;
            default:
                printf("Gecersiz secim.\n"); // Gerekli değil ama eklendi
        }
    }

    return 0;
}