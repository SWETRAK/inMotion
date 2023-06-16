//
//  MapView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 16/06/2023.
//

import MapKit
import SwiftUI

struct MapView: View {
    
    @Binding var mapDetails: MapDetail
    @State var region: MKCoordinateRegion = MKCoordinateRegion()
    
    var body: some View {
        //region = MKCoordinateRegion(center: mapDetails.coordinates, latitudinalMeters: 250, longitudinalMeters: 250)
        Map (
            coordinateRegion: $region,
            interactionModes: MapInteractionModes.all,
            showsUserLocation: true,
            annotationItems: [mapDetails]) { info in
                MapMarker(coordinate: info.coordinates, tint: .blue)
            }
    }
}

struct MapView_Previews: PreviewProvider {
    static var previews: some View {
        MapView(mapDetails: .constant(MapDetail(name: "Test", latitude: 1232.24, longitude: 45326.4)))
    }
}
