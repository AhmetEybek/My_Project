import Foundation
import CoreLocation
import UserNotifications
import Combine // BU ŞART!

class LocationManager: NSObject, ObservableObject, CLLocationManagerDelegate {
    private let manager = CLLocationManager()
    @Published var location: CLLocation?
    private var lastNotificationDate: Date?

    override init() {
        super.init()
        manager.delegate = self
        manager.desiredAccuracy = kCLLocationAccuracyHundredMeters
        UNUserNotificationCenter.current().requestAuthorization(options: [.alert, .sound]) { _, _ in }
    }

    func request() {
        manager.requestAlwaysAuthorization()
        manager.startUpdatingLocation()
    }

    func locationManager(_ manager: CLLocationManager, didUpdateLocations locations: [CLLocation]) {
        guard let newLocation = locations.last else { return }
        self.location = newLocation
        
        if lastNotificationDate == nil || Date().timeIntervalSince(lastNotificationDate!) >= 1800 {
            sendNearbyNotification(loc: newLocation)
            lastNotificationDate = Date()
        }
    }

    private func sendNearbyNotification(loc: CLLocation) {
        let content = UNMutableNotificationContent()
        content.title = "Nerdeyim? - Yakınlarda İlginç Bir Yer Var!"
        content.body = "Çevrendeki sırları keşfetmek için dokun."
        content.sound = .default
        let request = UNNotificationRequest(identifier: UUID().uuidString, content: content, trigger: nil)
        UNUserNotificationCenter.current().add(request)
    }
}
