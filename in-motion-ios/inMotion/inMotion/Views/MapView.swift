//
//  MapView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 16/06/2023.
//

import MapKit
import SwiftUI

struct MapView: View {
    @Binding public var mapDetails: MapDetail
    @State private var region: MKCoordinateRegion = MKCoordinateRegion()
    
    var body: some View {
        VStack{
            Map (
                coordinateRegion: $region,
                interactionModes: MapInteractionModes.all,
                showsUserLocation: true,
                annotationItems: [mapDetails]) { info in
                    MapMarker(coordinate: info.coordinates, tint: .blue)
                }
        }.onAppear{
            CreateRegion()
        }
    }
    
    private func CreateRegion() {
        self.region = MKCoordinateRegion(center: mapDetails.coordinates, latitudinalMeters: 250, longitudinalMeters: 250)
    }
}

struct MapView_Previews: PreviewProvider {
    static var previews: some View {
        MapView(mapDetails: .constant(MapDetail(name: "Test", latitude: 52.23105, longitude: 21.00558)))
    }
}
