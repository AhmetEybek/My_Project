import 'package:flutter/material.dart';

void main() {
  runApp(EvAsansorApp()); //Uygulamayı başlatan ana fonksiyon
}

class EvAsansorApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      home: AsansorSayfasi(), //Ana sayfa olarak AsansorSayfasi seçtim
    );
  }
}

class AsansorSayfasi extends StatefulWidget {
  @override
  _AsansorSayfasiState createState() => _AsansorSayfasiState();
}

class _AsansorSayfasiState extends State<AsansorSayfasi> {
  int currentPage = 0; //Şu anki sayfa indeksi
  int selectedPrice = 0; //Seçilen butonun fiyatı
  int totalPrice = 0; //Toplam maliyet
  int? selectedButton; //Seçilen buton indeksi
  int? selectedDay; //Seçilen gün

  double buttonWidth = 90; //Buton genişliği
  double buttonHeight = 45; //Buton yüksekliği
  Map<String, String> selectedOptions = {}; //Seçilenleri tutar

  List<String> titles = [ //Sayfa Başlıkları
    "LOJİSTİK TAŞIMACILIK",
    "TAŞIMA GÜNÜ",
    "EŞYA MİKTARI/ÇEŞİDİ",
    "ALINACAK KAT",
    "İNDİRİLECEK KAT",
    "TAŞIMA TÜRÜ",
    "FATURA"
  ];

  List<List<String>> buttonTexts = [ //Her bi butonun metni
    [],
    ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "28", "29", "30", "31"],
    ["1+0", "1+1", "2+1", "3+1", "4+1", "5+1", "Ofis Malz.", "İnşaat Malz.", "Depo Malz."],
    ["Zemin Kat", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26"],
    ["Zemin Kat", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26"],
    ["Yakın Mesafe", "Merkez İçi Taşıma", "İlçeye Taşıma", "Şehir Dışı Yakın Mesafe", "Şehir Dışı Taşıma", "Ülke Dışı Taşıma"],
    []
  ];

  List<List<int>> buttonPrices = [ //Her butonun değeri
    [],
    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    [1000, 1500, 2000, 2500, 3000, 3500, 4500, 5500, 6500],
    [500, 750, 1250, 1500, 1750, 2000, 2250, 2500, 2750, 3000, 3250, 3500, 3750, 4000, 4250, 4500, 4750, 5000, 5250, 5500, 5750, 6000, 6250, 6500, 6750, 7000, 7250],
    [500, 750, 1250, 1500, 1750, 2000, 2250, 2500, 2750, 3000, 3250, 3500, 3750, 4000, 4250, 4500, 4750, 5000, 5250, 5500, 5750, 6000, 6250, 6500, 6750, 7000, 7250],
    [5000, 10000, 15000, 30000, 100000, 250000],
    []
  ];

  void selectButton(int index) { //Butona tıklanıncaa seçimi yeniler
    setState(() {
      selectedButton = index;
      selectedPrice = buttonPrices[currentPage][index];
    });
  }

  void selectDay(int day) { //Gün seçilince yeniler
    setState(() {
      selectedDay = day;
    });
  }

  void nextPage() { //İleri butonuna basılınca yapılacaklar
    setState(() {
      if (selectedButton != null) {
        totalPrice += selectedPrice;
        selectedOptions[titles[currentPage]] = buttonTexts[currentPage][selectedButton!];
      }
      if (currentPage == 1 && selectedDay != null) {
        selectedOptions[titles[currentPage]] = "Gün: $selectedDay";
      }
      selectedButton = null;
      selectedPrice = 5;
      currentPage++;
    });
  }

  void previousPage() { //Geri butonuna basılınca yapılıcaklar
    setState(() {
      if (currentPage > 0) {
        currentPage--;
        if (selectedOptions.containsKey(titles[currentPage])) {
          selectedOptions.remove(titles[currentPage]);
        }
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar( //Sayfalardaki başlık yapısı
        title: Text(
          titles[currentPage], //Bulunduğun sayfanın başlığını gösterir
          style: TextStyle(fontWeight: FontWeight.bold),
        ),
      ),
      body: Center(
        child: currentPage == 0 //İlk sayfadaysan logoyu açar
            ? Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [Image.asset('assets/logo.png')],
        )
            : currentPage == 6 //Son sayfadaysan Seçilenler ve Maliyet i açar
            ? Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text("Seçilenler:", style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold)),
            SizedBox(height: 16),
            ...selectedOptions.entries.map(
                  (entry) => Column(
                children: [
                  Text("${entry.key}:", style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold, color: Colors.black)),
                  Text(entry.value, style: TextStyle(fontSize: 20)),
                  SizedBox(height: 16),
                ],
              ),
            ),
            Text("\nMALİYET: \$${totalPrice}", style: TextStyle(fontSize: 28, fontWeight: FontWeight.bold)),
          ],
        )
            : currentPage == 1 //İkinci sayfadaysan tarih seçme yi gösterir
            ? Wrap(
          spacing: 20,
          runSpacing: 20,
          children: List.generate(
            buttonTexts[currentPage].length,
                (index) => SizedBox(
              width: buttonWidth,
              height: buttonHeight,
              child: ElevatedButton(
                onPressed: () => selectButton(index), //Seçilen butona işlemi yap
                style: ElevatedButton.styleFrom(
                  backgroundColor: selectedButton == index ? Colors.red : Colors.yellow, //İşlem olarak butonu kırmızı yap
                  foregroundColor: Colors.black,
                ),
                child: Text(buttonTexts[currentPage][index],
                style: TextStyle(fontSize: 10)),
              ),
            ),
          ),
        )
            : Wrap( //Diğer sayfalarda butonları göster
          spacing: 20,
          runSpacing: 20,
          children: List.generate(
            buttonTexts[currentPage].length,
                (index) => SizedBox(
              width: buttonWidth,
              height: buttonHeight,
              child: ElevatedButton(
                onPressed: () => selectButton(index), //Seçilen butona işlem yap
                style: ElevatedButton.styleFrom(
                  backgroundColor: selectedButton == index ? Colors.red : Colors.yellow, //İşlem olarak butonu kırmızı yap
                  foregroundColor: Colors.black,
                ),
                child: Text(buttonTexts[currentPage][index],
                style: TextStyle(fontSize: 10)),
              ),
            ),
          ),
        ),
      ),
      bottomNavigationBar: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            if (currentPage > 0) //Geri butonu(İlk sayfada görünmez)
              FloatingActionButton(onPressed: previousPage, child: Icon(Icons.arrow_back)),
            if (currentPage < 6) //İleri butonu(Son sayfada görünmez)
              FloatingActionButton(onPressed: nextPage, child: Icon(Icons.arrow_forward)),
            FloatingActionButton(
              onPressed: () {
                setState(() { //Ana sayfaya dönünce maliyet sıfırlanır
                  currentPage = 0;
                  totalPrice = 0;
                  selectedOptions.clear();
                  selectedButton = null;
                });
              },
              backgroundColor: Colors.blue, //Ana sayfa butonu ilk sayfaya götürür
              child: Icon(Icons.home),
            ),
          ],
        ),
      ),
    );
  }
}
