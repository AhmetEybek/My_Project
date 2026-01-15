import Foundation
import SwiftData

@Model // Bu işaret, veritabanına kaydedileceğini belirtir
class FavoritePlace {
    @Attribute(.unique) var id: Int
    var title: String
    var desc: String
    var dateAdded: Date
    
    init(id: Int, title: String, desc: String) {
        self.id = id
        self.title = title
        self.desc = desc
        self.dateAdded = Date()
    }
}
