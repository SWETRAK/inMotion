//
//  MainWallPost.swift
//  inMotion
//
//  Created by Kamil Pietrak on 06/06/2023.
//

import SwiftUI

struct MainWallPost: View {
    var body: some View {
        VStack {
            
            HStack{
                Image("google-logo")
                    .resizable()
                    .frame(width: 70, height: 70)
                VStack{
                    Text("Nickname")
                    Text("Last seen 12")
                }
            }.frame(maxWidth: .infinity, alignment: .leading)
            
            GeometryReader {
                proxy in
                ZStack(alignment: .topTrailing) {
                    Image("google-logo")
                        .resizable()
                        .frame(width: proxy.size.width, height: proxy.size.width)
                    Image("facebook-logo")
                        .resizable()
                        .frame(width: proxy.size.width/2, height: proxy.size.width/2)
                        .padding()
                }
            }
            
            HStack{
                HStack{
                    Text("👍")
                    Text("12")
                }
                
            }
            
            Divider()
//                .foregroundColor(Color("accent"))
            Spacer()
            
        }.padding()
    }
}

struct MainWallPost_Previews: PreviewProvider {
    static var previews: some View {
        MainWallPost()
    }
}
