//
//  RequestsView.swift
//  Friendsproj
//
//  Created by student on 13/06/2023.
//

import SwiftUI

struct RequestsView: View {
    @Environment(\.managedObjectContext) private var viewContext
    
    @State var nickname: String = "";
    @State var name: String = "inMotion";
    @State var friends = [
    Friend(nickname: "Macius12", lastseen: "4 minutes ago", avatar: "profilepic"),
    Friend(nickname: "steph_moustache", lastseen: "Active now", avatar: "profilepic"),
    Friend(nickname: "Eloelo", lastseen: "5 hours ago", avatar: "profilepic"),];    var body: some View {
        VStack {
            HStack{
                Button{} label: {
                    Image(systemName: "multiply") }
                Spacer()
                Text(name)
                Spacer()
            }
            HStack{
                Button{} label: {
                    Text("FRIENDS").foregroundColor(.gray)
                }
                Spacer()
                Button{} label: {
                    Text("REQUESTS").foregroundColor(.black)
                }
            } .padding(30)
            
            
            VStack(alignment: .leading, spacing: 5){
                RequestFriendRowView(friend: $friends[0])
                RequestFriendRowView(friend: $friends[1])
                RequestFriendRowView(friend: $friends[2])
            }
            Spacer()
        }
        .padding()
    }}

struct RequestsView_Previews: PreviewProvider {
    static var previews: some View {
        RequestsView()
    }
}
