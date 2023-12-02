//
//  PostsAppStateExtension.swift
//  inMotion
//
//  Created by Kamil Pietrak on 22/11/2023.
//

import Foundation

    //MARK: - POSTS METHODS
    
extension AppState {
    
    public func GetUserPost(
        onSuccess: @escaping (GetPostResponseDto) -> Void,
        onFailure: @escaping (ImsHttpError) -> Void) {
            
            var request = URLRequest(url: URL(string: self.httpBaseUrl + "/posts/api/posts")!,timeoutInterval: Double.infinity)
            request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")
            
            request.httpMethod = HTTPMethods.GET.rawValue
            
            let task = URLSession.shared.dataTask(with: request) { data, response, error in
                guard let data = data else {
                    if let error = error as? NSError {
                        onFailure(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                    }
                    return
                }
                if let httpResponse = response as? HTTPURLResponse {
                    if(httpResponse.statusCode == 200)
                    {
                        if let safeImsMessage: ImsHttpMessage<GetPostResponseDto> = JsonUtil.decodeJsonData(data: data) {
                            if let userInfoDataSafe:GetPostResponseDto = safeImsMessage.data {
                                onSuccess(userInfoDataSafe);
                            }
                        }
                    } else {
                        if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                            if (httpResponse.statusCode == 500)
                            {
                                safeError.status = 500
                            }
                            onFailure(safeError);
                        }
                    }
                }
            }
            
            task.resume()
        }
    
    public func CreatePost(
        requestData: CreatePostRequestDto,
        onSuccess: @escaping (CreatePostResponseDto) -> Void,
        onFailure: @escaping (ImsHttpError) -> Void) {
            
            let postData = JsonUtil.encodeJsonStringFromObject(requestData)
            print("Upload")
            var request = URLRequest(url: URL(string: self.httpBaseUrl + "/posts/api/posts")!,timeoutInterval: Double.infinity)
            request.addValue("application/json", forHTTPHeaderField: "Content-Type")
            request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")
            
            request.httpMethod = HTTPMethods.POST.rawValue
            request.httpBody = postData
            
            let task = URLSession.shared.dataTask(with: request) { data, response, error in
                guard let data = data else {
                    if let error = error as? NSError {
                        onFailure(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                    }
                    return
                }
                if let httpResponse = response as? HTTPURLResponse {
                    if(httpResponse.statusCode == 200)
                    {
                        if let safeImsMessage: ImsHttpMessage<CreatePostResponseDto> = JsonUtil.decodeJsonData(data: data) {
                            if let userInfoDataSafe: CreatePostResponseDto = safeImsMessage.data {
                                onSuccess(userInfoDataSafe);
                            }
                        }
                    } else {
                        if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                            if (httpResponse.statusCode == 500)
                            {
                                safeError.status = 500
                            }
                            onFailure(safeError);
                        }
                    }
                }
            }
            task.resume()
        }
    
    public func GetOtherUserPost(
        authorId: UUID,
        onSuccess: @escaping (GetPostResponseDto) -> Void,
        onFailure: @escaping (ImsHttpError) -> Void) {
            
            var request = URLRequest(url: URL(string: self.httpBaseUrl + "/posts/api/posts/" + authorId.uuidString.lowercased())!,timeoutInterval: Double.infinity)
            request.addValue("Bearer \(self.token ?? String.Empty)", forHTTPHeaderField: "Authorization")
            
            request.httpMethod = HTTPMethods.GET.rawValue
            
            let task = URLSession.shared.dataTask(with: request) { data, response, error in
                guard let data = data else {
                    if let error = error as? NSError {
                        onFailure(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                    }
                    return
                }
                if let httpResponse = response as? HTTPURLResponse {
                    if(httpResponse.statusCode == 200)
                    {
                        if let safeImsMessage: ImsHttpMessage<GetPostResponseDto> = JsonUtil.decodeJsonData(data: data) {
                            if let userInfoDataSafe:GetPostResponseDto = safeImsMessage.data {
                                onSuccess(userInfoDataSafe);
                            }
                        }
                    } else {
                        if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                            if (httpResponse.statusCode == 500)
                            {
                                safeError.status = 500
                            }
                            onFailure(safeError);
                        }
                    }
                }
            }
            
            task.resume()
        }
    
    public func GetPublicPosts(
        requestDto: ImsPaginationRequestDto,
        onSuccess: @escaping ([GetPostResponseDto]) -> Void,
        onFailure: @escaping (ImsHttpError) -> Void) {
            
            let postData = JsonUtil.encodeJsonStringFromObject(requestDto)
            
            var request = URLRequest(url: URL(string: self.httpBaseUrl + "/posts/api/posts/public")!,timeoutInterval: Double.infinity)
            request.addValue("application/json", forHTTPHeaderField: "Content-Type")
            request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")
            
            request.httpMethod = HTTPMethods.GET.rawValue
            request.httpBody = postData
            
            let task = URLSession.shared.dataTask(with: request) { data, response, error in
                guard let data = data else {
                    if let error = error as? NSError {
                        onFailure(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                    }
                    return
                }
                if let httpResponse = response as? HTTPURLResponse {
                    if(httpResponse.statusCode == 200)
                    {
                        if let safeImsMessage: ImsHttpMessage<[GetPostResponseDto]> = JsonUtil.decodeJsonData(data: data) {
                            if let userInfoDataSafe: [GetPostResponseDto] = safeImsMessage.data {
                                onSuccess(userInfoDataSafe);
                            }
                        }
                    } else {
                        if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                            if (httpResponse.statusCode == 500)
                            {
                                safeError.status = 500
                            }
                            onFailure(safeError);
                        }
                    }
                }
            }
            task.resume()
        }
    
    public func GetFriendsPosts(
        onSuccess: @escaping ([GetPostResponseDto]) -> Void,
        onFailure: @escaping (ImsHttpError) -> Void){
            
            var request = URLRequest(url: URL(string: self.httpBaseUrl + "/posts/api/posts/friends")!,timeoutInterval: Double.infinity)
            request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")
            
            request.httpMethod = HTTPMethods.GET.rawValue
            
            let task = URLSession.shared.dataTask(with: request) { data, response, error in
                guard let data = data else {
                    if let error = error as? NSError {
                        onFailure(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                    }
                    return
                }
                if let httpResponse = response as? HTTPURLResponse {
                    if(httpResponse.statusCode == 200)
                    {
                        if let safeImsMessage: ImsHttpMessage<[GetPostResponseDto]> = JsonUtil.decodeJsonData(data: data) {
                            if let userInfoDataSafe: [GetPostResponseDto] = safeImsMessage.data {
                                onSuccess(userInfoDataSafe);
                            }
                        }
                    } else {
                        if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                            if (httpResponse.statusCode == 500)
                            {
                                safeError.status = 500
                            }
                            onFailure(safeError);
                        }
                    }
                }
            }
            task.resume()
        }
    
    public func UpdatePost(
        postId: UUID,
        requestData: EditPostRequestDto,
        onSuccess: @escaping (GetPostResponseDto) -> Void,
        onFailure: @escaping (ImsHttpError) -> Void ) {
            
            let postData = JsonUtil.encodeJsonStringFromObject(requestData)
            
            var request = URLRequest(url: URL(string: self.httpBaseUrl + "/posts/api/posts/" + postId.uuidString.lowercased())!,timeoutInterval: Double.infinity)
            request.addValue("application/json", forHTTPHeaderField: "Content-Type")
            request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")
            
            request.httpMethod = HTTPMethods.PUT.rawValue
            request.httpBody = postData
            
            let task = URLSession.shared.dataTask(with: request) { data, response, error in
                guard let data = data else {
                    if let error = error as? NSError {
                        onFailure(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                    }
                    return
                }
                if let httpResponse = response as? HTTPURLResponse {
                    if(httpResponse.statusCode == 200)
                    {
                        if let safeImsMessage: ImsHttpMessage<GetPostResponseDto> = JsonUtil.decodeJsonData(data: data) {
                            if let userInfoDataSafe: GetPostResponseDto = safeImsMessage.data {
                                onSuccess(userInfoDataSafe);
                            }
                        }
                    } else {
                        if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                            if (httpResponse.statusCode == 500)
                            {
                                safeError.status = 500
                            }
                            onFailure(safeError);
                        }
                    }
                }
            }
            task.resume()
        }
}

// MARK: - POST COMMENT METHODS

extension AppState {
    
    func GetPostCommentsForPostHttpRequest(
        postId: UUID,
        onSuccess: @escaping ([PostCommentDto]) -> Void,
        onFailure: @escaping (ImsHttpError) -> Void) {
            
            var request = URLRequest(url: URL(string: self.httpBaseUrl + "/posts/api/posts/comments/\(postId.uuidString.lowercased())")!,timeoutInterval: Double.infinity)
            request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")
            
            request.httpMethod = HTTPMethods.GET.rawValue
            
            let task = URLSession.shared.dataTask(with: request) { data, response, error in
                guard let data = data else {
                    if let error = error as? NSError {
                        onFailure(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                    }
                    return
                }
                if let httpResponse = response as? HTTPURLResponse {
                    if(httpResponse.statusCode == 200)
                    {
                        if let safeImsMessage: ImsHttpMessage<[PostCommentDto]> = JsonUtil.decodeJsonData(data: data) {
                            if let userInfoDataSafe: [PostCommentDto] = safeImsMessage.data {
                                onSuccess(userInfoDataSafe);
                            }
                        }
                    } else {
                        if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                            if (httpResponse.statusCode == 500)
                            {
                                safeError.status = 500
                            }
                            onFailure(safeError);
                        }
                    }
                }
            }
            task.resume()
        }
    
    func CreatePostForPostHttpRequest(
        requestData: CreatePostCommentDto,
        onSuccess: @escaping (PostCommentDto) -> Void,
        onFailure: @escaping (ImsHttpError) -> Void ) {
            
            let postData = JsonUtil.encodeJsonStringFromObject(requestData)
            
            var request = URLRequest(url: URL(string: self.httpBaseUrl + "/posts/api/posts/comments")!,timeoutInterval: Double.infinity)
            request.addValue("application/json", forHTTPHeaderField: "Content-Type")
            request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")
            
            request.httpMethod = HTTPMethods.POST.rawValue
            request.httpBody = postData
            
            let task = URLSession.shared.dataTask(with: request) { data, response, error in
                guard let data = data else {
                    if let error = error as? NSError {
                        onFailure(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                    }
                    return
                }
                if let httpResponse = response as? HTTPURLResponse {
                    if(httpResponse.statusCode == 200)
                    {
                        if let safeImsMessage: ImsHttpMessage<PostCommentDto> = JsonUtil.decodeJsonData(data: data) {
                            if let userInfoDataSafe: PostCommentDto = safeImsMessage.data {
                                onSuccess(userInfoDataSafe);
                            }
                        }
                    } else {
                        if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                            if (httpResponse.statusCode == 500)
                            {
                                safeError.status = 500
                            }
                            onFailure(safeError);
                        }
                    }
                }
            }
            task.resume()
        }
    
    
    func EditPoctCommentHttpRequest(
        postCommentId: UUID,
        requestData: UpdatePostCommentDto,
        onSuccess: @escaping (PostCommentDto) -> Void,
        onFailure: @escaping (ImsHttpError) -> Void ) {
            
            let postData = JsonUtil.encodeJsonStringFromObject(requestData)
            
            var request = URLRequest(url: URL(string: self.httpBaseUrl + "/posts/api/posts/comments/\(postCommentId.uuidString.lowercased())")!,timeoutInterval: Double.infinity)
            request.addValue("application/json", forHTTPHeaderField: "Content-Type")
            request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")
            
            request.httpMethod = HTTPMethods.PUT.rawValue
            request.httpBody = postData
            
            let task = URLSession.shared.dataTask(with: request) { data, response, error in
                guard let data = data else {
                    if let error = error as? NSError {
                        onFailure(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                    }
                    return
                }
                if let httpResponse = response as? HTTPURLResponse {
                    if(httpResponse.statusCode == 200)
                    {
                        if let safeImsMessage: ImsHttpMessage<PostCommentDto> = JsonUtil.decodeJsonData(data: data) {
                            if let userInfoDataSafe: PostCommentDto = safeImsMessage.data {
                                onSuccess(userInfoDataSafe);
                            }
                        }
                    } else {
                        if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                            if (httpResponse.statusCode == 500)
                            {
                                safeError.status = 500
                            }
                            onFailure(safeError);
                        }
                    }
                }
            }
            task.resume()
        }
    
    func RemovePostCommentHttpRequest(
        postCommentId: UUID,
        onSuccess: @escaping (Bool) -> Void,
        onFailure: @escaping (ImsHttpError) -> Void
    ) {
        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/posts/api/posts/comments/\(postCommentId.uuidString.lowercased())")!,timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.DELETE.rawValue

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    onFailure(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }
            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Bool> = JsonUtil.decodeJsonData(data: data) {
                        if let userInfoDataSafe: Bool = safeImsMessage.data {
                            onSuccess(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        onFailure(safeError);
                    }
                }
            }
        }
        task.resume()
    }
    
    
}
