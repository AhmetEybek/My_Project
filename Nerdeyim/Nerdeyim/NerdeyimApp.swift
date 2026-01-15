import SwiftUI
import SwiftData

@main
struct NerdeyimApp: App {
    var body: some Scene {
        WindowGroup {
            HomeView()
        }
        .modelContainer(for: FavoritePlace.self) // Veritabanı kabını buraya bağlıyoruz
    }
}
