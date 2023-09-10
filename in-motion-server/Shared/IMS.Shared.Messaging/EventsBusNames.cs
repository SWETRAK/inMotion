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
}