namespace IMS.Shared.Messaging;

public static class EventsBusNames
{
    public const string ValidateJwtEventName = "validate-jwt-event";
    public const string SendUserLoggedInEmail = "send-user-logged-in";
    public const string SendFailureLoggedInEmail = "send-failure-logged-in";
    public const string SendAccountActivationEmail = "send-account-activation-email";

    public const string GetUserInfoName = "get-user-info";
    public const string GetUsersInfoName = "get-users-info";
    
    public const string CheckFriendshipStatus = "check-friendship-status";
}