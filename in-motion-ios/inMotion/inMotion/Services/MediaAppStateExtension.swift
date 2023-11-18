//
//  MediaAppStateExtension.swift
//  inMotion
//
//  Created by Kamil Pietrak on 18/11/2023.
//

import Foundation

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
                    print(data)
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
                    print(data)
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
                        print(safeImsMessage.frontVideo)
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
    
    // let fileData = try NSData(contentsOfFile: paramSrc, options: []) as Data // Ta linijka jest tutaj ważna
    func uploadProfileVideo(
        fileData: Data,
        successUploadProfileVideo: @escaping (ProfileVideoUploadInfoDto) -> Void,
        failureUploadFrofileVideo: @escaping (ImsHttpError) -> Void) {
            
            let boundary = "Boundary-\(UUID().uuidString)"
            var body = ""
            let paramName = "mp4File"
            body += "--\(boundary)\r\n"
            body += "Content-Disposition:form-data; name=\"\(paramName)\""
            let fileContent = String(data: fileData, encoding: .utf8)!
            body += "; filename=\"\(UUID.init().uuidString)\"\r\n"
            + "Content-Type: \"content-type header\"\r\n\r\n\(fileContent)\r\n"
            body += "--\(boundary)--\r\n";
            let postData = body.data(using: .utf8)
            
            var request = URLRequest(url: URL(string: self.httpBaseUrl + "/media/api/profile/video")!,timeoutInterval: Double.infinity)
            request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")
            request.addValue("multipart/form-data; boundary=\(boundary)", forHTTPHeaderField: "Content-Type")
            
            request.httpMethod = HTTPMethods.POST.rawValue
            request.httpBody = postData
            
            let task = URLSession.shared.dataTask(with: request) { data, response, error in
                guard let data = data else {
                    if let error = error as? NSError {
                        failureUploadFrofileVideo(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                    }
                    return
                }
                
                if let httpResponse = response as? HTTPURLResponse {
                    if(httpResponse.statusCode == 201)
                    {
                        if let safeImsMessage: ProfileVideoUploadInfoDto = JsonUtil.decodeJsonData(data: data) {
                            print(safeImsMessage.mp4FileGetAddress)
                            successUploadProfileVideo(safeImsMessage)
                        }
                    } else {
                        if let safeError: ErrorResponse = JsonUtil.decodeJsonData(data: data) {
                            let error = ImsHttpError(status: httpResponse.statusCode, errorMessage: safeError.message, errorType: "")
                            failureUploadFrofileVideo(error)
                        }
                    }
                }
            }
            task.resume()
        }
    
    func uploadPostVideo(frontVideo: Data,
                         backVideo: Data,
                         postId: UUID,
                         successUploadProfileVideo: @escaping (PostUploadInfoDto) -> Void,
                         failureUploadProfileVideo: @escaping (ImsHttpError) -> Void) {
        
        let parameters = [
          [
            "key": "frontVideo",
            "type": "file"
          ],
          [
            "key": "backVideo",
            "type": "file"
          ],
          [
            "key": "postId",
            "value": postId.uuidString,
            "type": "text"
          ]] as [[String: Any]]

        let boundary = "Boundary-\(UUID().uuidString)"
        var body = ""
        for param in parameters {
          let paramName = param["key"] as! String
          body += "--\(boundary)\r\n"
          body += "Content-Disposition:form-data; name=\"\(paramName)\""
          if param["contentType"] != nil {
            body += "\r\nContent-Type: \(param["contentType"] as! String)"
          }
          let paramType = param["type"] as! String
          if paramType == "text" {
            let paramValue = param["value"] as! String
            body += "\r\n\r\n\(paramValue)\r\n"
          } else {
              let fileData = paramName == "frontVideo" ? frontVideo : backVideo
            let fileContent = String(data: fileData, encoding: .utf8)!
              body += "; filename=\"\(UUID.init().uuidString)\"\r\n"
              + "Content-Type: \"content-type header\"\r\n\r\n\(fileContent)\r\n"
          }
        }
        body += "--\(boundary)--\r\n";
        let postData = body.data(using: .utf8)

        var request = URLRequest(url: URL(string: "http://localhost:8081/media/api/post")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")
        request.addValue("multipart/form-data; boundary=\(boundary)", forHTTPHeaderField: "Content-Type")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureUploadProfileVideo(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: PostUploadInfoDto = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.getVideosPath)
                        successUploadProfileVideo(safeImsMessage)
                    }
                } else {
                    if let safeError: ErrorResponse = JsonUtil.decodeJsonData(data: data) {
                        let error = ImsHttpError(status: httpResponse.statusCode, errorMessage: safeError.message, errorType: "")
                        failureUploadProfileVideo(error)
                    }
                }
            }
        }
        task.resume()
    }
}
