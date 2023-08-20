//
//  UserRepository.swift
//  inMotion
//
//  Created by Kamil Pietrak on 17/06/2023.
//

import Foundation
import CoreData

public class UserRepository {
    
    public static func GetUserByEmailAndPassword(context: NSManagedObjectContext, email: String, password: String) -> User? {
        let request: NSFetchRequest<User> = User.fetchRequest()
        let predictate = NSPredicate(format: "email == %@ AND password == %@", email, password)
        request.predicate = predictate
        do {
            let result = try context.fetch(request)
            if(result.count == 1) {
                return result[0]
            }
            return nil
        } catch {
            let nserror = error as NSError
            print("Unresolved error \(nserror), \(nserror.userInfo)")
        }
        return nil
    }
    
    public static func GetUserByEmail(context: NSManagedObjectContext, email: String) -> User? {
        let request: NSFetchRequest<User> = User.fetchRequest()
        let predictate = NSPredicate(format: "email == %@", self.email)
        request.predicate = predictate
        do {
            let result = try Context.fetch(request)
            if(result.count == 1) {
                return result[0]
            }
            return nil
        } catch {
            print("Error fetchig data from context \(error)")
        }
        return n
    }
    
    public static func Save(context: NSManagedObjectContext, user: User){
        do {
            try context.save()
        } catch {
            let nserror = error as NSError
            print("Unresolved error \(nserror), \(nserror.userInfo)")
        }
    }
}
