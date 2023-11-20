//
// Created by Kamil Pietrak on 01/11/2023.
//

import Foundation

// MARK: - Email Auth Methods

extension AppState {

    public func loginWithEmailAndPasswordHttpRequest(requestData: LoginUserWithEmailAndPasswordDto,
                                                     successLoginAction: @escaping (UserInfoDto) -> Void,
                                                     failureLoginAction: @escaping (ImsHttpError) -> Void) {

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/email/login")!, timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = JsonUtil.encodeJsonStringFromObject(requestData)

        let task = URLSession.shared.dataTask(with: request) { data, response, error in

            guard let data = data else {
                if let error = error as? NSError {
                    failureLoginAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                                self.userDefaults.set(self.token, forKey: "token")
                                self.getUserByIdHttpRequest(
                                    userId: userInfoDataSafe.id,
                                    successGetUserAction:{ (fullUserInfo: FullUserInfoDto) in
                                        DispatchQueue.main.async {
                                            self.fullUserInfo = fullUserInfo
                                        }
                                    },
                                    failureGetUserAction: { (error: ImsHttpError) in })
                            }
                            successLoginAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if var safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        if (httpResponse.statusCode == 500)
                        {
                            safeError.status = 500
                        }
                        failureLoginAction(safeError);
                    }
                }
            }
        }
        task.resume()
    }

    public func registerUserWithEmailAndPasswordHttpRequest(registerData: RegisterUserWithEmailAndPasswordDto,
                                                            successRegisterAction: @escaping (SuccessfulRegistrationResponseDto) -> Void,
                                                            validationRegisterAction: @escaping (Dictionary<String, [String]>) -> Void,
                                                            failureRegisterAction: @escaping (ImsHttpError) -> Void){

        let postData = JsonUtil.encodeJsonStringFromObject(registerData)
        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/email/register")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in

            guard let data = data else {
                if let error = error as? NSError {
                    failureRegisterAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 201)
                {
                    if let safeImsMessage: ImsHttpMessage<SuccessfulRegistrationResponseDto> = JsonUtil.decodeJsonData(data: data) {
                        if let successfulRegistrationResponseSafe: SuccessfulRegistrationResponseDto = safeImsMessage.data {
                            successRegisterAction(successfulRegistrationResponseSafe)
                        }
                    }
                } else if (httpResponse.statusCode == 400) {
                    if let safeImsMessage: ValidationErrorDto = JsonUtil.decodeJsonData(data: data) {
                        validationRegisterAction(safeImsMessage.errors)
                    }
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        failureRegisterAction(safeError)
                    }
                }
            }
        }
        task.resume()
    }
    
    public func updateUserPasswordHttpRequest(requestData: UpdatePasswordDto,
                                              successPasswordChangeAction: @escaping (Bool) -> Void,
                                              failurePasswordChangeAction: @escaping (ImsHttpError) -> Void) {

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)
        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/email/password/update")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.PUT.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in

            guard let data = data else {
                if let error = error as? NSError {
                    failurePasswordChangeAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Bool> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: Bool = safeImsMessage.data {
                            successPasswordChangeAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        // TODO: Implement proper error handling
                        failurePasswordChangeAction(safeError);
                    }
                }
            }
        }

        task.resume()
    }

    public func addPasswordHttpRequest(requestData: AddPasswordDto,
                                       successAddPasswordAction: @escaping (Bool) -> Void,
                                       failureAddPasswordAction: @escaping (ImsHttpError) -> Void) {

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/email/password/add")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.PUT.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureAddPasswordAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Bool> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: Bool = safeImsMessage.data {
                            if var user = self.user {
                                user.providers.append("Password")
                                self.user = user
                            }
                            successAddPasswordAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        failureAddPasswordAction(safeError);
                    }
                }
            }
        }

        task.resume()
    }
}

// MARK: - User Http Methods

extension AppState {

    public func getLoggedInUserHttpRequest (successGetUserAction: @escaping (UserInfoDto) -> Void,
                                            failureGetUserAction: @escaping (ImsHttpError) -> Void){

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/user")!, timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

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
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status) // 204 is ok
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                                self.userDefaults.set(self.token, forKey: "token")
                                self.getUserByIdHttpRequest(
                                    userId: userInfoDataSafe.id,
                                    successGetUserAction:{ (fullUserInfo: FullUserInfoDto) in
                                        DispatchQueue.main.async {
                                            self.fullUserInfo = fullUserInfo
                                        }
                                    },
                                    failureGetUserAction: { (error: ImsHttpError) in })
                            }
                            successGetUserAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        self.user = nil
                        self.logged = false
                        self.token = nil
                        self.userDefaults.removeObject(forKey: "token")
                        failureGetUserAction(safeError);
                    }
                }
            }
        }

        task.resume()
    }

    public func updateUserEmailHttpRequest (requestData: UpdateEmailDto,
                                            successEmailUpdateAction: @escaping (UserInfoDto) -> Void,
                                            failureEmailUpdateAction: @escaping (ImsHttpError) -> Void) {

        let postData = JsonUtil.encodeJsonStringFromObject(requestData);

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/user/email")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.PUT.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureEmailUpdateAction(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status) // 204 is ok
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                                self.userDefaults.set(self.token, forKey: "token")
                            }
                            successEmailUpdateAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        failureEmailUpdateAction(safeError);
                    }
                }
            }
        }

        task.resume()
    }

    public func updateUserNicknameHttpRequest(requestData: UpdateNicknameDto,
                                              successNicknameUpdateAction: @escaping (UserInfoDto) -> Void,
                                              failureNicknameUpdateAction: @escaping (ImsHttpError) -> Void) {

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/user/nickname")!,timeoutInterval: Double.infinity)
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
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status) // 204 is ok
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                                self.userDefaults.set(self.token, forKey: "token")
                            }
                            successNicknameUpdateAction(userInfoDataSafe);
                        }
                    }
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        failureNicknameUpdateAction(safeError);
                    }
                }
            }
        }

        task.resume()
    }
}

// MARK: - Facebook Auth Methods
// TODO: Implement and test in future

extension AppState {

    public func loginWithFacebookHttpRequest(requestData: AuthenticateWithFacebookProviderDto,
                                             successRegisterWithFacebook: @escaping (UserInfoDto) -> Void,
                                             failureRegisterWithFacebook: @escaping (ImsHttpError) -> Void) {

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/facebook/login")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureRegisterWithFacebook(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                                self.userDefaults.set(self.token, forKey: "token")
                            }
                            successRegisterWithFacebook(userInfoDataSafe);
                        }
                    }
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        failureRegisterWithFacebook(safeError);
                    }
                }
            }
        }

        task.resume()
    }

    public func addFacebookProviderHttpRequest(requestData: AuthenticateWithFacebookProviderDto,
                                               successAddFacebookProvider: @escaping (Bool) -> Void,
                                               failureAddFacebookProvider: @escaping (ImsHttpError) -> Void){

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/facebook/add")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureAddFacebookProvider(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Bool> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: Bool = safeImsMessage.data {
                            successAddFacebookProvider(userInfoDataSafe);
                        }
                    }
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        failureAddFacebookProvider(safeError);
                    }
                }
            }
        }

        task.resume()
    }
}

// MARK: - Google Auth Methods

extension AppState {

    public func loginWithGoogleHttpRequest(requestData: AuthenticateWithGoogleProviderDto,
                                           successRegisterWithGoogle: @escaping (UserInfoDto) -> Void,
                                           failureRegisterWithGoogle: @escaping (ImsHttpError) -> Void) {

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/google/login")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureRegisterWithGoogle(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                                self.userDefaults.set(self.token, forKey: "token")
                                self.getUserByIdHttpRequest(
                                    userId: userInfoDataSafe.id,
                                    successGetUserAction:{ (fullUserInfo: FullUserInfoDto) in
                                        DispatchQueue.main.async {
                                            self.fullUserInfo = fullUserInfo
                                        }
                                    },
                                    failureGetUserAction: { (error: ImsHttpError) in })
                            }
                            successRegisterWithGoogle(userInfoDataSafe);
                        }
                    }
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        failureRegisterWithGoogle(safeError);
                    }
                }
            }
        }

        task.resume()
    }

    public func addGoogleProviderHttpRequest(requestData: AuthenticateWithGoogleProviderDto,
                                               successAddGoogleProvider: @escaping (Bool) -> Void,
                                               failureAddGoogleProvider: @escaping (ImsHttpError) -> Void){

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/google/add")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                if let error = error as? NSError {
                    failureAddGoogleProvider(ImsHttpError(status: 500,  errorMessage: error.localizedDescription, errorType: ""))
                }
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Bool> = JsonUtil.decodeJsonData(data: data) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: Bool = safeImsMessage.data {
                            successAddGoogleProvider(userInfoDataSafe);
                        }
                    }
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data) {
                        failureAddGoogleProvider(safeError);
                    }
                }
            }
        }

        task.resume()
    }
}

