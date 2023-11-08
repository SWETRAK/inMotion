//
//  UserAppStateExtension.swift
//  inMotion
//
//  Created by Kamil Pietrak on 08/11/2023.
//

import Foundation

// MARK: - User Methods

extension AppState {
    
    func getUserByIdHttpRequest(userId: UUID,
                                successGetUserAction: @escaping (FullUserInfoDto) -> Void,
                                failureGetUserAction: @escaping (ImsHttpError) -> Void) {
        var request = URLRequest(url: URL(string: "\(self.httpBaseUrl)/api/users/\(userId.uuidString)")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token!)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.GET.rawValue
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureGetUserAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            
            
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<FullUserInfoDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<FullUserInfoDto>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: FullUserInfoDto = safeImsMessage.data {
                            successGetUserAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureGetUserAction(safeError);
                    }
                }
            }
        }
        
        task.resume()
    }
    
    func updateUserBioHttpRequest(requestData: UpdateUserBioDto,
                                  successUpdateUserBioAction: @escaping (UpdatedUserBioDto) -> Void,
                                  failureUpdateUserBioAction: @escaping (ImsHttpError) -> Void) {
        
        let postData = JsonUtil.encodeJsonStringFromObject(requestData)
        
        var request = URLRequest(url: URL(string: "\(self.httpBaseUrl)/users/api/users/update/bio")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token!)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.PUT.rawValue
        request.httpBody = postData
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureUpdateUserBioAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UpdatedUserBioDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<UpdatedUserBioDto>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: UpdatedUserBioDto = safeImsMessage.data {
                            successUpdateUserBioAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureUpdateUserBioAction(safeError);
                    }
                }
            }
        }
        task.resume()
    }
}

// MARK: - User Profile Video Methods

extension AppState {
    
    func getUserProfileVideoByUserId(userId: UUID,
                                     successGetUserVideoInfoAction: @escaping (UserProfileVideoDto) -> Void,
                                     failureGetUserVideoInfoAction: @escaping (ImsHttpError) -> Void) {
        
        var request = URLRequest(url: URL(string: "\(self.httpBaseUrl)/users/api/users/videos/byUser/\(userId.uuidString)")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token!)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.GET.rawValue
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureGetUserVideoInfoAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserProfileVideoDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<UserProfileVideoDto>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: UserProfileVideoDto = safeImsMessage.data {
                            successGetUserVideoInfoAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureGetUserVideoInfoAction(safeError);
                    }
                }
            }
        }
        task.resume()
    }
    
    func getUserProfileVideoById(videoId: UUID,
                                     successGetUserVideoInfoAction: @escaping (UserProfileVideoDto) -> Void,
                                     failureGetUserVideoInfoAction: @escaping (ImsHttpError) -> Void) {
        
        var request = URLRequest(url: URL(string: "\(self.httpBaseUrl)/users/api/users/videos/byVideo/\(videoId.uuidString)")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token!)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.GET.rawValue
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureGetUserVideoInfoAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserProfileVideoDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<UserProfileVideoDto>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: UserProfileVideoDto = safeImsMessage.data {
                            successGetUserVideoInfoAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureGetUserVideoInfoAction(safeError);
                    }
                }
            }
        }
        task.resume()
    }
}


