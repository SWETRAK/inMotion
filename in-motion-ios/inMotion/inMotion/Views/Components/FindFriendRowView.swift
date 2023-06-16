//
//  FindFriendRowView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 14/06/2023.
//

import SwiftUI

import SwiftUI

struct FindFriendRowView: View {
    var body: some View {
        HStack(alignment: .center){
            Image("google-logo").resizable().frame(width:50, height:50)
            VStack(alignment: .leading){
                Text("Username")
                Text("Last seen: 1h ago")
            }
            Spacer()
            Button{
                
            } label: {
                Image(systemName: "plus")
            } . padding(.horizontal)
                .buttonStyle(.plain)
        } .padding(.horizontal)
    }
}


struct FindFriendRowView_Previews: PreviewProvider {
    static var previews: some View {
        FindFriendRowView()
    }
}
