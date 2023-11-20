//
//  FriendsAppStateExtension.swift
//  inMotion
//
//  Created by Kamil Pietrak on 08/11/2023.
//

import Foundation

// MARK: - Friends Methods
extension AppState {
    
    func createFriendshipHttpRequest(otherUserId: UUID,
                                 successCreateFriendshipAction: @escaping (InvitationFriendshipDto) -> Void,
                                 failureCreateFriendshipAction: @escaping (ImsHttpError) -> Void) {
        
        var request = URLRequest(url: URL(string: "\(self.httpBaseUrl)/friends/api/friends/\(otherUserId.uuidString)")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token!)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.POST.rawValue
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureCreateFriendshipAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 201)
                {
                    if let safeImsMessage: ImsHttpMessage<InvitationFriendshipDto> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: InvitationFriendshipDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.invitedFriendships.append(userInfoDataSafe)
                            }
                            successCreateFriendshipAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureCreateFriendshipAction(safeError);
                    }
                }
            }
        }
        
        task.resume()
    }
    
    func acceptFriendshipHttpRequest(friendshipId: UUID,
                                 successAcceptUserAction: @escaping (AcceptedFriendshipDto) -> Void,
                                 failureAcceptUserAction: @escaping (ImsHttpError) -> Void) {
        
        var request = URLRequest(url: URL(string: "\(self.httpBaseUrl)/friends/api/friends/accept/\(friendshipId.uuidString)")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.PUT.rawValue
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureAcceptUserAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<AcceptedFriendshipDto> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: AcceptedFriendshipDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.requestedFriendships.removeAll { element in
                                    return element.id == friendshipId
                                }
                                self.acceptedFriendships.append(userInfoDataSafe)
                            }
                            successAcceptUserAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureAcceptUserAction(safeError);
                    }
                }
            }
        }
        
        task.resume()
    }
    
    func rejectFriendshipHttpRequest(friendshipId: UUID,
                                 successRejectFriendshipAction: @escaping (RejectedFriendshipDto) -> Void,
                                 failureRejectFriendshipAction: @escaping (ImsHttpError) -> Void) {
        
        var request = URLRequest(url: URL(string: "\(self.httpBaseUrl)/friends/api/friends/reject/\(friendshipId.uuidString)")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.PATCH.rawValue
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureRejectFriendshipAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<RejectedFriendshipDto> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: RejectedFriendshipDto = safeImsMessage.data {
                            self.requestedFriendships.removeAll { element in
                                return element.id == friendshipId
                            }
                            successRejectFriendshipAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureRejectFriendshipAction(safeError);
                    }
                }
            }
        }
        
        task.resume()
    }
    
    func revertFriendshipHttpRequest(friendshipId: UUID,
                                 successRevertFriendshipAction: @escaping (Bool) -> Void,
                                 failureRevertFriendshipAction: @escaping (ImsHttpError) -> Void) {
        
        var request = URLRequest(url: URL(string: "\(self.httpBaseUrl)/friends/api/friends/revert/\(friendshipId.uuidString)")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.DELETE.rawValue

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
          guard let data = data else {
              if let error = error as? NSError {
                  failureRevertFriendshipAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
              }
            return
          }
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Bool> = JsonUtil.decodeJsonData(data: data) {
                        if let userInfoDataSafe: Bool = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.invitedFriendships.removeAll { x in
                                    return x.id == friendshipId
                                }
                            }
                            successRevertFriendshipAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureRevertFriendshipAction(safeError);
                    }
                }
            }
        }

        task.resume()
    }
    
    func unfiendsFriendshipHttpRequest(friendshipId: UUID,
                                 successUnfriendFriendshipAction: @escaping (RejectedFriendshipDto) -> Void,
                                 failureUnfriendFriendshipAction: @escaping (ImsHttpError) -> Void) {
        
        var request = URLRequest(url: URL(string: "\(self.httpBaseUrl)/friends/api/friends/unfriend/\(friendshipId.uuidString)")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.DELETE.rawValue

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
          guard let data = data else {
              if let error = error as? NSError {
                  failureUnfriendFriendshipAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
              }
            return
          }
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<RejectedFriendshipDto> = JsonUtil.decodeJsonData(data: data) {
                        if let userInfoDataSafe: RejectedFriendshipDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.acceptedFriendships.removeAll { friendship in
                                    return friendship.id == userInfoDataSafe.id
                                }
                            }
                            successUnfriendFriendshipAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureUnfriendFriendshipAction(safeError);
                    }
                }
            }
        }

        task.resume()
    }
}

// MARK: - FriendsList Methods
extension AppState {
    
    func getListOfAcceptedFriendshipHttpRequest(successGetRelations: @escaping ([AcceptedFriendshipDto]) -> Void,
                                                failureGetRelations: @escaping (ImsHttpError) -> Void) {
        
        var request = URLRequest(url: URL(string: "\(self.httpBaseUrl)/friends/api/friends/lists/accepted")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.GET.rawValue
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureGetRelations(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Array<AcceptedFriendshipDto>> = JsonUtil.decodeJsonData(data: data) {
                        if let userInfoDataSafe: Array<AcceptedFriendshipDto> = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.acceptedFriendships = userInfoDataSafe
                            }
                            successGetRelations(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        // INFO: This throws 404 if user has no friends
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureGetRelations(safeError);
                    }
                }
            }
        }
        
        task.resume()
    }
    
    func getListOfRequestedFriendshipHttpRequest(successGetRelations: @escaping ([RequestFriendshipDto]) -> Void,
                                                 failureGetRelations: @escaping (ImsHttpError) -> Void) {
        
        var request = URLRequest(url: URL(string: "\(self.httpBaseUrl)/friends/api/friends/lists/requested")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.GET.rawValue
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureGetRelations(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Array<RequestFriendshipDto>> = JsonUtil.decodeJsonData(data: data) {
                        if let userInfoDataSafe: Array<RequestFriendshipDto> = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.requestedFriendships = userInfoDataSafe
                            }
                            successGetRelations(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureGetRelations(safeError);
                    }
                }
            }
        }
        
        task.resume()
    }
    
    func getListOfInvitedFriendshipHttpRequest(successGetRelations: @escaping ([InvitationFriendshipDto]) -> Void,
                                               failureGetRelations: @escaping (ImsHttpError) -> Void) {
        
        var request = URLRequest(url: URL(string: "\(self.httpBaseUrl)/friends/api/friends/lists/invited")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.GET.rawValue
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureGetRelations(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Array<InvitationFriendshipDto>> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: Array<InvitationFriendshipDto> = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.invitedFriendships = userInfoDataSafe
                            }
                            successGetRelations(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureGetRelations(safeError);
                    }
                }
            }
        }
        
        task.resume()
    }
}
