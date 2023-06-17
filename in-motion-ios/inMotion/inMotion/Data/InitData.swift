//
//  InitData.swift
//  inMotion
//
//  Created by Kamil Pietrak on 17/06/2023.
//

import Foundation
import CoreData

public struct InitData {
    
    public static func CheckIfEmpty(context: NSManagedObjectContext) -> Bool{
        let request: NSFetchRequest<User> = User.fetchRequest()
        do {
            let result = try context.fetch(request)
            if(result.count == 0) {
                return true
            }
            return false
        } catch {
            let nserror = error as NSError
            print("Unresolved error \(nserror), \(nserror.userInfo)")
        }
        return false
    }
    
    public static func InitData(context: NSManagedObjectContext){
        
        let user1 = User(context: context)
        user1.id = UUID()
        user1.nickname = "swetrak"
        user1.profile_photo = "google-logo"
        user1.email = "kamilpietrak123@gmail.com"
        user1.password = "Ssmr1234"
        
        let user2 = User(context: context)
        user2.id = UUID()
        user2.nickname = "userDwa"
        user2.profile_photo = "facebook-logo"
        user2.email = "kamilpietrak123@icloud.com"
        user2.password = "Password2"
        
        let post1 = Post(context: context)
        post1.id = UUID()
        post1.author = user1
        post1.localization_longitude = 21.00558
        post1.localization_latitude = 52.23105
        post1.localization_name = "Warsaw"
        post1.video_link = "facebook-logo"
        
        let post2 = Post(context: context)
        post2.id = UUID()
        post2.author = user1
        post2.localization_longitude = 21.00558
        post2.localization_latitude = 52.23105
        post2.localization_name = "Warsaw"
        post2.video_link = "google-logo"
        
        let comment1 = Comment(context: context)
        comment1.id = UUID()
        comment1.post = post1
        comment1.time = Date.now
        comment1.author = user1
        comment1.comment = "Test comment 1"
        
        let comment2 = Comment(context: context)
        comment2.id = UUID()
        comment2.post = post1
        comment2.time = Date.now
        comment2.author = user2
        comment2.comment = "Test comment 2"
        
        let friendship1 = Friendship(context: context)
        friendship1.status = FriendshipStatusEnum.Requested.rawValue
        friendship1.id = UUID()
        friendship1.userOne = user2
        friendship1.userTwo = user1
        
        if (context.hasChanges) {
            do {
                try context.save()
            } catch {
                let nserror = error as NSError
                print("Unresolved error \(nserror), \(nserror.userInfo)")
            }
        }
    }
}
