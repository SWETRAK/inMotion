//
//  PersonRowView.swift
//  inMotion
//
//  Created by Kamil Pietrak on 17/06/2023.
//

import SwiftUI
import SDWebImageSwiftUI

struct PersonRowView: View {
    
    @EnvironmentObject public var appState: AppState
    @State private var data: Data? = nil
    
    public var fullUserInfo: FullUserInfoDto
    
    var body: some View {
        HStack{
            if let safeData = data {
                
                AnimatedImage(data: safeData)
                    .resizable()
                    .frame(width: 50, height: 50, alignment: .center)
                
            } else {
                Image("avatar-placeholder")
                    .resizable()
                    .frame(width: 50, height: 50, alignment: .center)
                
            }
            VStack(alignment: .leading){
                Text(self.fullUserInfo.nickname)
                    .fontWeight(Font.Weight.bold)
                    .frame(maxWidth: .infinity, alignment: .leading)
            }
            .frame(maxWidth: .infinity, alignment: .leading)
            Spacer()
        }
        .frame(alignment: .leading)
        .onAppear {
            self.LoadProfilePicture()
        }
    }
    
    func LoadProfilePicture() {
        
        self.appState.getUserGifHttpRequest(
            userId: self.fullUserInfo.id ) { (data: Data) in
                DispatchQueue.main.async {
                    self.data = data
                }
            } failureGetUserProfileGifUrl: { (error: ImsHttpError) in }
        
    }
}

//struct PersonRowView_Previews: PreviewProvider {
//    static var previews: some View {
//        PersonRowView()
//    }
//}
