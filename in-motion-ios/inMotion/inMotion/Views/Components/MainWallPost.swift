import SwiftUI

struct MainWallPost: View {
    
    @State var liked: Bool = true
    var body: some View {
        NavigationLink {
            PostDetailsView()
        } label: {
            VStack {
                HStack{
                    Image("google-logo")
                        .resizable()
                        .frame(width: 70, height: 70)
                    VStack(alignment: .leading){
                        Text("Nickname")
                        Text("Last seen 12")
                    }
                }.frame(maxWidth: .infinity, alignment: .leading)
                
                Image("google-logo")
                    .resizable()
                    .frame(width: UIScreen.main.bounds.width-20, height: UIScreen.main.bounds.width-20)
                
                HStack{
                    HStack{
                        Image(systemName: liked ? "heart.fill" : "heart") // heart.fill if liked
                            .resizable()
                            .foregroundColor(liked ? .red : .black) // .red if liked
                            .frame(width: 20, height: 20)
                            .onTapGesture {
                                self.liked = !self.liked
                            }
                        Text("12")
                    }
                    
                    Spacer()
                    
                    HStack{
                        Image(systemName: "text.bubble.rtl")
                            .resizable()
                            .frame(width: 20, height: 20)
                        Text("12")
                    }
                }
                Divider()
                
            }.padding()
        }
        .buttonStyle(.plain)
        
    }
}

struct MainWallPost_Previews: PreviewProvider {
    static var previews: some View {
        MainWallPost()
    }
}
