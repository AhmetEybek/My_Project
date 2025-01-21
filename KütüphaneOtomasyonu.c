#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAKSIMUM_KITAP 100

// Kitap bilgilerini tutan yapı
typedef struct yazar_bilgi {
    int yazarKodu;
    char yazarSoyadi[20];
    char yazarAdi[20];
    int dogumYili;
    char ozgecmis[500];
} yazar;

typedef struct kitap_bilgi {
    char tur[20];
    int kitapKodu;
    char kitapAd[20];
    int sayfa;
    int basimYili;
    yazar yazarBilgi;
    int stokSayisi;
} kitap;

// Kitapları tutmak için dizi ve sayaç
kitap kitaplar[MAKSIMUM_KITAP];
int kitapSayisi = 0;

// Yeni kitap ekleme işlevi
void kitapEkle() {
    if (kitapSayisi >= MAKSIMUM_KITAP) {
        printf("Kitap listesi dolu! Yeni kitap eklenemez.\n");
        return;
    }

    kitap *yeniKitap = &kitaplar[kitapSayisi];

    printf("\n--- Kitap Ekleme Ekranı ---\n");
    printf("Kitap Türü: ");
    scanf("%s", yeniKitap->tur);
    printf("Kitap Kodu: ");
    scanf("%d", &yeniKitap->kitapKodu);
    printf("Kitap Adı: ");
    scanf("%s", yeniKitap->kitapAd);
    printf("Sayfa Sayısı: ");
    scanf("%d", &yeniKitap->sayfa);
    printf("Basım Yılı: ");
    scanf("%d", &yeniKitap->basimYili);
    printf("Stok Sayısı: ");
    scanf("%d", &yeniKitap->stokSayisi);
    printf("Yazar Kodu: ");
    scanf("%d", &yeniKitap->yazarBilgi.yazarKodu);
    printf("Yazar Adı: ");
    scanf("%s", yeniKitap->yazarBilgi.yazarAdi);
    printf("Yazar Soyadı: ");
    scanf("%s", yeniKitap->yazarBilgi.yazarSoyadi);
    printf("Yazar Doğum Yılı: ");
    scanf("%d", &yeniKitap->yazarBilgi.dogumYili);
    printf("Yazar Özgeçmişi: ");
    scanf(" %[^\n]", yeniKitap->yazarBilgi.ozgecmis); // Boşluklu giriş için

    kitapSayisi++;
    printf("Kitap başarıyla eklendi!\n");
}

// Kitapları listeleme işlevi
void kitapListele() {
    if (kitapSayisi == 0) {
        printf("Hiçbir kitap bulunamadı!\n");
        return;
    }

    printf("\n--- Kütüphanedeki Kitaplar ---\n");
    for (int i = 0; i < kitapSayisi; i++) {
        kitap *k = &kitaplar[i];
        printf("Kitap Kodu: %d\n", k->kitapKodu);
        printf("Kitap Adı: %s\n", k->kitapAd);
        printf("Kitap Türü: %s\n", k->tur);
        printf("Sayfa Sayısı: %d\n", k->sayfa);
        printf("Basım Yılı: %d\n", k->basimYili);
        printf("Stok Sayısı: %d\n", k->stokSayisi);
        printf("Yazar: %s %s\n", k->yazarBilgi.yazarAdi, k->yazarBilgi.yazarSoyadi);
        printf("Yazar Doğum Yılı: %d\n", k->yazarBilgi.dogumYili);
        printf("Yazar Özgeçmişi: %s\n", k->yazarBilgi.ozgecmis);
        printf("---------------------------\n");
    }
}

// Ana menü
void menuGoster() {
    printf("\n--- ANA MENÜ ---\n");
    printf("1 - Kitap Ekle\n");
    printf("2 - Kitap Listele\n");
    printf("3 - Çıkış\n");
    printf("Seçiminizi girin: ");
}

int main() {
    int secim;

    while (1) {
        menuGoster();
        scanf("%d", &secim);

        switch (secim) {
            case 1:
                kitapEkle();
                break;
            case 2:
                kitapListele();
                break;
            case 3:
                printf("Programdan çıkılıyor...\n");
                return 0;
            default:
                printf("Geçersiz seçim! Lütfen tekrar deneyin.\n");
                break;
        }

        // İşlem tamamlandığında kullanıcıyı ana menüye döndür
        printf("\nAna menüye dönülüyor...\n");
    }
}