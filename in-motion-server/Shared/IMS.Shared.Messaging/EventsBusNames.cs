namespace IMS.Shared.Messaging;

public static class EventsBusNames
{
    public const string ValidateJwtEventName = "validate-jwt-event";
    public const string SendUserLoggedInEmail = "send-user-logged-in";
    public const string SendFailureLoggedInEmail = "send-failure-logged-in";
}