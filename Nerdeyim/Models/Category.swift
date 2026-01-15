import SwiftUI

enum AppCategory: String, CaseIterable {
    case history = "Tarih"
    case science = "Bilim"
    case war = "Savaşlar"
    case general = "Genel"
    
    var color: Color {
        switch self {
        case .history: return Color(red: 0.7, green: 0.5, blue: 0.3)
        case .science: return .blue
        case .war: return .red
        case .general: return .indigo
        }
    }
    
    var icon: String {
        switch self {
        case .history: return "book.closed.fill"
        case .science: return "atom"
        case .war: return "shield.fill"
        case .general: return "map.fill"
        }
    }
    
    // Wikipedia API'sine daha spesifik komutlar gönderiyoruz
    var searchKey: String {
        switch self {
        case .history: return "tarihi+eser|antik|müze" // 'tarihi eser' veya 'antik' veya 'müze'
        case .science: return "bilim|rasathane|teknoloji"
        case .war: return "savaş|kale|cephe|anıt"
        case .general: return ""
        }
    }
}
