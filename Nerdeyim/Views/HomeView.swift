import SwiftUI

struct HomeView: View {
    var body: some View {
        NavigationView {
            VStack(spacing: 25) {
                VStack(spacing: 10) {
                    Image(systemName: "safari.fill").font(.system(size: 60)).foregroundColor(.blue)
                    Text("Merhaba Patron!").font(.title).bold()
                    Text("Bugün keşfedecek çok şey var.").foregroundColor(.secondary)
                }
                .padding(.top, 40)
                
                VStack(spacing: 15) {
                    NavigationLink(destination: ContentView(selectedCategory: .general)) {
                        HLabel(title: "Hepsini Keşfet", icon: "map.circle.fill", color: .indigo)
                    }
                    NavigationLink(destination: FavoritesView()) {
                        HLabel(title: "Favori Yerlerim", icon: "heart.circle.fill", color: .red)
                    }
                }
                .padding(.horizontal)
                
                VStack(alignment: .leading) {
                    Text("Kategoriler").font(.headline).padding(.leading)
                    ScrollView(.horizontal, showsIndicators: false) {
                        HStack(spacing: 15) {
                            ForEach(AppCategory.allCases.filter { $0 != .general }, id: \.self) { cat in
                                NavigationLink(destination: ContentView(selectedCategory: cat)) {
                                    CategoryCard(category: cat)
                                }
                            }
                        }
                        .padding(.horizontal)
                    }
                }
                Spacer()
            }
            .navigationBarHidden(true)
        }
    }
}

// YARDIMCI GÖRÜNÜMLER (Scope hatalarını çözen kısım)
struct HLabel: View {
    let title: String; let icon: String; let color: Color
    var body: some View {
        HStack {
            Image(systemName: icon).font(.title)
            Text(title).font(.headline)
            Spacer(); Image(systemName: "chevron.right")
        }
        .padding().background(color.opacity(0.1)).foregroundColor(color).cornerRadius(15)
    }
}

struct CategoryCard: View {
    let category: AppCategory
    var body: some View {
        VStack(spacing: 10) {
            Image(systemName: category.icon).font(.title).foregroundColor(category.color)
            Text(category.rawValue).font(.caption).bold().foregroundColor(.primary)
        }
        .frame(width: 90, height: 90).background(category.color.opacity(0.15)).cornerRadius(15)
    }
}
