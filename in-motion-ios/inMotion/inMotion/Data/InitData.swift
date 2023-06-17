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
        user1.nickname = "Monia"
        user1.profile_photo = "avatar-one"
        user1.email = "monia@wp.pl"
        user1.password = "Monia123"
        
        let user2 = User(context: context)
        user2.id = UUID()
        user2.nickname = "Jamir"
        user2.profile_photo = "avatar-two"
        user2.email = "jamir@gmail.com"
        user2.password = "Jamir123"
        
        let user3 = User(context: context)
        user3.id = UUID()
        user3.nickname = "Maurycy"
        user3.profile_photo = "avatar-three"
        user3.email = "maurycy@icloud.com"
        user3.password = "Maurycy123"
        
        let post1 = Post(context: context)
        post1.id = UUID()
        post1.author = user1
        post1.localization_longitude = 21.00558
        post1.localization_latitude = 52.23105
        post1.localization_name = "Warsaw"
        post1.video_link = "warsaw"
        
        let post2 = Post(context: context)
        post2.id = UUID()
        post2.author = user2
        post2.localization_longitude = 2.34257
        post2.localization_latitude = 48.86470
        post2.localization_name = "Paris"
        post2.video_link = "paris"
        
        let post3 = Post(context: context)
        post3.id = UUID()
        post3.author = user3
        post3.localization_longitude = -73.96871
        post3.localization_latitude = 40.77437
        post3.localization_name = "New York"
        post3.video_link = "newyork"
        
        let comment1 = Comment(context: context)
        comment1.id = UUID()
        comment1.post = post1
        comment1.time = Date.now
        comment1.author = user2
        comment1.comment = "Cool view man"
        
        let comment2 = Comment(context: context)
        comment2.id = UUID()
        comment2.post = post1
        comment2.time = Date.now
        comment2.author = user3
        comment2.comment = "Awesome"
        
        let comment3 = Comment(context: context)
        comment3.id = UUID()
        comment3.post = post2
        comment3.time = Date.now
        comment3.author = user1
        comment3.comment = "Awesome weather in paris"
        
        let friendship1 = Friendship(context: context)
        friendship1.status = FriendshipStatusEnum.Requested.rawValue
        friendship1.id = UUID()
        friendship1.userOne = user2
        friendship1.userTwo = user1
        
        let friendship2 = Friendship(context: context)
        friendship2.status = FriendshipStatusEnum.Requested.rawValue
        friendship2.id = UUID()
        friendship2.userOne = user3
        friendship2.userTwo = user1
        
        let friendship3 = Friendship(context: context)
        friendship3.status = FriendshipStatusEnum.Requested.rawValue
        friendship3.id = UUID()
        friendship3.userOne = user2
        friendship3.userTwo = user3
        
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
