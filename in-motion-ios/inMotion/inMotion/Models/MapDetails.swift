//
//  MapDetails.swift
//  inMotion
//
//  Created by Kamil Pietrak on 16/06/2023.
//

import MapKit
import Foundation

struct MapDetail: Identifiable {
    
    let id = UUID()
    let name: String
    let latitude: Double
    let longitude: Double
    var coordinates: CLLocationCoordinate2D {
        CLLocationCoordinate2D(latitude: self.latitude, longitude: self.longitude)
    }
}
