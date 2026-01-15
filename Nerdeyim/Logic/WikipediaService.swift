import Foundation

class WikipediaService {
    private let baseURL = "https://tr.wikipedia.org/w/api.php"
    
    func fetchNearbyPlaces(lat: Double, lon: Double, filter: String = "") async throws -> [Place] {
        // MÜHENDİSLİK DOKUNUŞU: generator=search kullanıyoruz ki 'filter' kelimesi konuma baskın gelsin.
        var urlString = ""
        
        if filter.isEmpty {
            // Genel arama: Sadece yakındaki her şeyi getir
            urlString = "\(baseURL)?action=query&prop=extracts&exintro&explaintext&generator=geosearch&ggscoord=\(lat)|\(lon)&ggsradius=10000&ggslimit=15&format=json"
        } else {
            // Kategorik arama: Belirli kelimeleri (savaş, bilim vb.) o konumun çevresinde ara
            urlString = "\(baseURL)?action=query&prop=extracts&exintro&explaintext&generator=search&gsrsearch=\(filter)&gsrcoord=\(lat)|\(lon)&gsrradius=15000&gsrlimit=15&format=json"
        }
        
        guard let url = URL(string: urlString.addingPercentEncoding(withAllowedCharacters: .urlQueryAllowed) ?? urlString) else {
            throw URLError(.badURL)
        }
        
        let (data, _) = try await URLSession.shared.data(from: url)
        
        // Hata ayıklama için: Gelen JSON'u görelim (İsteğe bağlı)
        // print(String(data: data, encoding: .utf8) ?? "")
        
        let decoder = JSONDecoder()
        let response = try decoder.decode(WikipediaResponse.self, from: data)
        return Array(response.query.pages.values)
    }
}
