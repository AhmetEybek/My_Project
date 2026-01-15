import SwiftUI
import SwiftData

struct FavoritesView: View {
    // Veritabanındaki favorileri çekiyoruz
    @Query(sort: \FavoritePlace.dateAdded, order: .reverse) var favoritePlaces: [FavoritePlace]
    @Environment(\.modelContext) private var modelContext
    
    var body: some View {
        List {
            if favoritePlaces.isEmpty {
                ContentUnavailableView("Henüz Favori Yok", systemImage: "heart.slash", description: Text("Keşfettiğin yerleri beğenerek buraya ekleyebilirsin."))
            } else {
                ForEach(favoritePlaces) { fav in
                    // Favori öğesini normal Place modeline çevirip detaya gönderiyoruz
                    NavigationLink(destination: PlaceDetailView(place: Place(id: fav.id, title: fav.title, description: fav.desc))) {
                        VStack(alignment: .leading, spacing: 5) {
                            Text(fav.title)
                                .font(.headline)
                            Text(fav.desc)
                                .font(.subheadline)
                                .foregroundColor(.secondary)
                                .lineLimit(2)
                        }
                    }
                    .swipeActions {
                        Button(role: .destructive) {
                            modelContext.delete(fav)
                        } label: {
                            Label("Sil", systemImage: "trash")
                        }
                    }
                }
            }
        }
        .navigationTitle("Favorilerim")
    }
}
