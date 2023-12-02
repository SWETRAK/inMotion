//
//  MediaAppStateExtension.swift
//  inMotion
//
//  Created by Kamil Pietrak on 18/11/2023.
//

import Foundation
import Alamofire

extension AppState {
    
    func getUserVideoHttpRequest(userId: UUID,
                                 successGetUserProfileVideoUrl: @escaping (Data) -> Void,
                                 failureGetUserProfileVideoUrl: @escaping (ImsHttpError) -> Void){
        
        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/media/api/profile/video/mp4/\(userId.uuidString.lowercased())")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.GET.rawValue
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureGetUserProfileVideoUrl(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    successGetUserProfileVideoUrl(data)
                } else {
                    if let safeError: ErrorResponse = JsonUtil.decodeJsonData(data: data) {
                        let error = ImsHttpError(status: httpResponse.statusCode, errorMessage: safeError.message, errorType: "")
                        failureGetUserProfileVideoUrl(error)
                    }
                }
            }
        }
        task.resume()
    }
    
    func getUserGifHttpRequest(userId: UUID,
                               successGetUserProfileGifUrl: @escaping (Data) -> Void,
                               failureGetUserProfileGifUrl: @escaping (ImsHttpError) -> Void) {
        
        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/media/api/profile/video/gif/\(userId.uuidString.lowercased())")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.GET.rawValue
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureGetUserProfileGifUrl(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    successGetUserProfileGifUrl(data)
                } else {
                    if let safeError: ErrorResponse = JsonUtil.decodeJsonData(data: data) {
                        let error = ImsHttpError(status: httpResponse.statusCode, errorMessage: safeError.message, errorType: "")
                        failureGetUserProfileGifUrl(error)
                    }
                }
            }
        }
        task.resume()
    }
    
    func getPostVideosUrls(postId: UUID,
                           successGetPostVideoUrls: @escaping (PostDto) -> Void,
                           failureGetPostVideoUrls: @escaping (ImsHttpError) -> Void) {
        
        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/media/api/post/\(postId.uuidString.lowercased())")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")
        
        request.httpMethod = HTTPMethods.GET.rawValue
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureGetPostVideoUrls(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: PostDto = JsonUtil.decodeJsonData(data: data) {
                        successGetPostVideoUrls(safeImsMessage)
                    }
                } else {
                    if let safeError: ErrorResponse = JsonUtil.decodeJsonData(data: data) {
                        let error = ImsHttpError(status: httpResponse.statusCode, errorMessage: safeError.message, errorType: "")
                        failureGetPostVideoUrls(error)
                    }
                }
            }
        }
        task.resume()
    }
    
    func uploadProfileVideoAlamofire(
        filePath: URL,
        onSuccess: @escaping (ProfileVideoUploadInfoDto) -> Void,
        onFailure: @escaping (ImsHttpError) -> Void) {
            
            let headers: HTTPHeaders = [
                "Authorization": "Bearer \(self.token ?? String.Empty)",
            ]
            
            AF.upload(multipartFormData: { (multipartFormData) in
                multipartFormData.append(filePath, withName: "mp4File")
            }, to: self.httpBaseUrl + "/media/api/profile/video", headers: headers)
            .uploadProgress { progress in
                print("Upload Progress: \(progress.fractionCompleted)")
            }
            .responseDecodable(of: ProfileVideoUploadInfoDto.self) { response in
                guard let data = response.data else {
                    if let error = response.error as? NSError {
                        onFailure(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                    }
                    return
                }
                
                if let httpResponse = response.response{
                    if(httpResponse.statusCode == 201)
                    {
                        if let safeImsMessage: ProfileVideoUploadInfoDto = JsonUtil.decodeJsonData(data: data) {
                            onSuccess(safeImsMessage)
                        }
                    } else {
                        if let safeError: ErrorResponse = JsonUtil.decodeJsonData(data: data) {
                            let error = ImsHttpError(status: httpResponse.statusCode, errorMessage: safeError.message, errorType: "")
                            onFailure(error)
                        }
                    }
                }
            }
        }
    
    func uploadPostVideoAlamofire(
        frontFilePath: URL,
        backFilePath: URL,
        postId: UUID,
        onSuccess: @escaping (PostUploadInfoDto) -> Void,
        onFailure: @escaping (ImsHttpError) -> Void) {
        
        let headers: HTTPHeaders = [
            "Authorization": "Bearer \(self.token ?? String.Empty)",
        ]
        
        AF.upload(multipartFormData: { (multipartFormData) in
            multipartFormData.append(frontFilePath, withName: "frontVideo")
            multipartFormData.append(backFilePath, withName: "backVideo")
        }, to: self.httpBaseUrl + "/media/api/post?postID=\(postId.uuidString.lowercased())", headers: headers)
        .uploadProgress { progress in
            print("Upload Progress: \(progress.fractionCompleted)")
        }
        .responseDecodable(of: PostUploadInfoDto.self) { response in
            guard let data = response.data else {
                if let error = response.error as? NSError {
                    onFailure(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            
            if let httpResponse = response.response{
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: PostUploadInfoDto = JsonUtil.decodeJsonData(data: data) {
                        onSuccess(safeImsMessage)
                    }
                } else {
                    if let safeError: ErrorResponse = JsonUtil.decodeJsonData(data: data) {
                        let error = ImsHttpError(status: httpResponse.statusCode, errorMessage: safeError.message, errorType: "")
                        onFailure(error)
                    }
                }
            }
        }
    }
}

