//
// Created by Kamil Pietrak on 01/11/2023.
//

import Foundation

// MARK: Email Auth Methods

extension AppState {

    public func loginWithEmailAndPasswordHttpRequest(requestData: LoginUserWithEmailAndPasswordDto,
                                                     successLoginAction: @escaping (UserInfoDto) -> Void,
                                                     failureLoginAction: @escaping (ImsHttpError) -> Void)
    {
        var request = URLRequest(url: URL(string: "http://localhost/auth/api/email/login")!, timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = JsonUtil.encodeJsonStringFromObject(requestData)

        let task = URLSession.shared.dataTask(with: request) { data, response, error in

            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<UserInfoDto>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                            }
                            successLoginAction(userInfoDataSafe);
                        }
                    }
                    //TODO: Add validation action
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        // TODO: Implement proper error handling
                        failureLoginAction(safeError);
                    }
                }
            }
        }
        task.resume()
    }

    public func registerUserWithEmailAndPassword(registerData: RegisterUserWithEmailAndPasswordDto,
                                                 successRegisterAction: @escaping (SuccessfulRegistrationResponseDto) -> Void,
                                                 failureRegisterAction: @escaping (ImsHttpError) -> Void){
        let postData = JsonUtil.encodeJsonStringFromObject(registerData)
        var request = URLRequest(url: URL(string: "http://localhost/auth/api/email/register")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in

            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 201)
                {
                    if let safeImsMessage: ImsHttpMessage<SuccessfulRegistrationResponseDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<SuccessfulRegistrationResponseDto>.self) {
                        if let successfulRegistrationResponseSafe: SuccessfulRegistrationResponseDto = safeImsMessage.data {
                            successRegisterAction(successfulRegistrationResponseSafe)
                            // TODO: Add info about succesfull register and
                        }
                    }
                } else if (httpResponse.statusCode == 400) {
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        failureRegisterAction(safeError)
                        // TODO: Implement proper error handling
                        print(safeError.status, safeError.errorMessage, safeError.errorType)
                    }
                }
            }
        }
        task.resume()
    }

    //TODO: Add logging with facebook
    //TODO: Add logging with google

    public func updateUserPassword(requestData: UpdatePasswordDto,
                                   successPasswordChangeAction: @escaping (UserInfoDto) -> Void,
                                   failurePasswordChangeAction: @escaping (ImsHttpError) -> Void) {
        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: "http://localhost/auth/api/email/password/update")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.PUT.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in

            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Bool> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<UserInfoDto>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: Bool = safeImsMessage.data {
                            successPasswordChangeAction(userInfoDataSafe);
                        }
                    }
                    //TODO: Add validation action
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        // TODO: Implement proper error handling
                        failurePasswordChangeAction(safeError);
                    }
                }
            }
        }
        task.resume()
    }
}