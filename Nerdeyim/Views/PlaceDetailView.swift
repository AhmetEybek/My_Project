import SwiftUI
import SwiftData

struct PlaceDetailView: View {
    let place: Place
    
    // Veritabanı işlemlerini yapmak için 'modelContext'
    @Environment(\.modelContext) private var modelContext
    // Favori olup olmadığını kontrol etmek için veritabanını sorgula
    @Query private var favorites: [FavoritePlace]
    
    var isFavorite: Bool {
        favorites.contains { $0.id == place.id }
    }
    
    var body: some View {
        ScrollView {
            VStack(alignment: .leading, spacing: 20) {
                Text(place.title).font(.largeTitle).bold()
                Text(place.description ?? "Bilgi yok.").font(.body)
            }
            .padding()
        }
        .navigationTitle("Detaylar")
        .toolbar {
            Button(action: toggleFavorite) {
                Image(systemName: isFavorite ? "heart.fill" : "heart")
                    .foregroundColor(.red)
            }
        }
    }
    
    func toggleFavorite() {
        if isFavorite {
            // Favorilerden sil
            if let index = favorites.firstIndex(where: { $0.id == place.id }) {
                modelContext.delete(favorites[index])
            }
        } else {
            // Favorilere ekle
            let newFav = FavoritePlace(id: place.id, title: place.title, desc: place.description ?? "")
            modelContext.insert(newFav)
        }
    }
}
