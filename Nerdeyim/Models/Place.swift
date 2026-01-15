import Foundation

struct Place: Codable, Identifiable {
    let id: Int
    let title: String
    let description: String?
    
    enum CodingKeys: String, CodingKey {
        case id = "pageid"
        case title
        case description = "extract"
    }
}

struct WikipediaResponse: Codable {
    let query: WikipediaQuery
}

struct WikipediaQuery: Codable {
    let pages: [Int: Place]
}
