//
//  YourFriendRowView.swift
//  Friendsproj
//
//  Created by student on 13/06/2023.
//

import SwiftUI
import CoreData
import SDWebImageSwiftUI

struct YourFriendRowView: View {
    @EnvironmentObject public var appState: AppState
    @State private var data: Data? = nil
    
    public var friendInfo: FriendInfoDto
    
    var body: some View {
        HStack{
            if let safeData = self.data {
                
                AnimatedImage(data: safeData)
                    .resizable()
                    .frame(width: 50, height: 50, alignment: .center)
                
            } else {
                Image("avatar-placeholder")
                    .resizable()
                    .frame(width: 50, height: 50, alignment: .center)
                
            }
            VStack(alignment: .leading){
                Text(self.friendInfo.nickname)
                    .fontWeight(Font.Weight.bold)
                    .frame(maxWidth: .infinity, alignment: .leading)
            }
            .frame(maxWidth: .infinity, alignment: .leading)
            
            Spacer()
        }
        .frame(alignment: .leading)
        .onAppear{
            self.LoadProfilePicture()
        }
    }
    
    private func LoadProfilePicture() {
        self.appState.getUserGifHttpRequest(
            userId: self.friendInfo.id ) { (data: Data) in
                DispatchQueue.main.async {
                    self.data = data
                }
            } failureGetUserProfileGifUrl: { (error: ImsHttpError) in }
        
    }
}

//struct YourFriendRowView_Previews: PreviewProvider {
//    static var previews: some View {
//        YourFriendRowView(friendship: .constant())
//    }
//}
