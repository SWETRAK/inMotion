//
//  ProfileView.swift
//  Friendsproj
//
//  Created by student on 06/06/2023.
//

import SwiftUI

struct ProfileView: View {
    @State var name: String = "inMotion"
    @State var username: String = "username"
    var body: some View {
        VStack{
            HStack{
                Button{} label: {
                    Image(systemName: "multiply") }
                Spacer()
                Text(name)
                Spacer()
            }
            GeometryReader {
                            proxy in
                ZStack(alignment: .topTrailing) {
                                Image("profilepic")
                                    .resizable()
                                    .frame(width: proxy.size.width, height: proxy.size.width)
                                Image("profilepic")
                                    .resizable()
                                    .frame(width: proxy.size.width/2, height: proxy.size.width/2) .padding()
                }
                            
            } .padding()
            
            Text(username)
            Divider() .overlay(Color.blue)
            Text("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ullamcorper ante non nulla interdum ultrices. Morbi consequat lacus nisl, in ornare augue venenatis eget. Sed eget nisi turpis. Sed ac dolor vulputate, hendrerit libero eget, iaculis nisi. Curabitur lacus enim, feugiat ut tempus a, porttitor a sem. Mauris egestas ultrices nunc eget fermentum. Nunc ipsum odio, rhoncus vel malesuada in, faucibus et turpis. ")
            
           
            
        } .padding()
    }
}

struct ProfileView_Previews: PreviewProvider {
    static var previews: some View {
        ProfileView()
    }
}
