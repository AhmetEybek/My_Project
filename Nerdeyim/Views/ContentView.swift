import SwiftUI
import CoreLocation

struct ContentView: View {
    var selectedCategory: AppCategory = .general
    @StateObject var locationManager = LocationManager()
    @State private var places: [Place] = []
    @State private var isLoading = false
    private let wikiService = WikipediaService()
    
    var body: some View {
        VStack {
            if let loc = locationManager.location {
                if isLoading {
                    ProgressView("\(selectedCategory.rawValue) keşfediliyor...")
                        .tint(selectedCategory.color)
                } else if places.isEmpty {
                    VStack(spacing: 20) {
                        Image(systemName: selectedCategory.icon)
                            .font(.system(size: 50))
                            .foregroundColor(selectedCategory.color)
                        Text("\(selectedCategory.rawValue) Keşfine Hazır mısın?")
                        
                        Button("Çevremde Ne Var?") {
                            Task { await searchPlaces(for: loc) }
                        }
                        .buttonStyle(.borderedProminent)
                        .tint(selectedCategory.color)
                    }
                } else {
                    List(places) { place in
                        NavigationLink(destination: PlaceDetailView(place: place)) {
                            VStack(alignment: .leading, spacing: 8) {
                                Text(place.title).font(.headline).foregroundColor(selectedCategory.color)
                                if let description = place.description {
                                    Text(description).font(.subheadline).lineLimit(2).foregroundColor(.secondary)
                                }
                            }
                        }
                    }
                    .listStyle(.insetGrouped)
                }
            } else {
                Text("Konum bekleniyor...").onAppear { locationManager.request() }
            }
        }
        .navigationTitle(selectedCategory.rawValue)
        .navigationBarTitleDisplayMode(.inline)
    }
    
    func searchPlaces(for location: CLLocation) async {
        isLoading = true
        do {
            let foundPlaces = try await wikiService.fetchNearbyPlaces(
                lat: location.coordinate.latitude,
                lon: location.coordinate.longitude,
                filter: selectedCategory.searchKey
            )
            self.places = foundPlaces
        } catch {
            print("Hata: \(error.localizedDescription)")
        }
        isLoading = false
    }
}
