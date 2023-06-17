//
//  PersonRowView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 17/06/2023.
//

import SwiftUI

struct PersonRowView: View {
    @Environment(\.managedObjectContext) private var viewContext
    @EnvironmentObject var person: User
    
    var body: some View {
        HStack{
            Image(person.profile_photo ?? "google-logo").resizable().frame(width:50, height:50)
            VStack(alignment: .leading){
                Text(person.nickname ?? "nickname").fontWeight(Font.Weight.bold).frame(maxWidth: .infinity, alignment: .leading)
            }
            .frame(maxWidth: .infinity, alignment: .leading)
            Spacer()
        }
        .frame(alignment: .leading)
    }
}

struct PersonRowView_Previews: PreviewProvider {
    static var previews: some View {
        PersonRowView()
    }
}
