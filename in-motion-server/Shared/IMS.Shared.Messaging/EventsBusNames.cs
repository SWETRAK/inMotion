namespace IMS.Shared.Messaging;

public static class EventsBusNames
{
    public const string ValidateJwtEventName = "validate-jwt-event";
    public const string SendUserLoggedInEmailName = "send-user-logged-in";
    public const string SendFailureLoggedInEmailName = "send-failure-logged-in";
    public const string SendAccountActivationEmailName = "send-account-activation-email";

    public const string GetUserInfoName = "get-user-info";
    public const string GetUsersInfoName = "get-users-info";

    public const string CheckFriendshipStatusName = "check-friendship-status";

    public const string GetBaseUserInfoName = "get-base-user-info";
    public const string GetBaseUsersInfoName = "get-base-users-info";
    
    public const string GetBaseUserInfoByNicknameName = "get-base-user-info-by-nickname";

    public const string GetUserFriendsName = "get-user-friends";

    public static class CustomRabbitConfigurationNames
    {
        public const string ValidateJwtQueueName = "queue:jwt-validator";
        public const string ValidateJwtExchangeName = "exchange:jwt-validator";
        public const string ValidateJwtRoutingKeyName = "routing:jwt-validator";

        public const string UpdatePostVideoQueueName = "queue:update-post-video";
        public const string UpdatePostVideoExchangeName = "exchange:update-post-video";
        public const string UpdatePostVideoRoutingKeyName = "routing:update-post-video";

        public const string UpdateProfileVideoQueueName = "queue:update-profile-video";
        public const string UpdateProfileVideoExchangeName = "exchange:update-profile-video";
        public const string UpdateProfileVideoRoutingKeyName = "routing:update-profile-video";
    }
}