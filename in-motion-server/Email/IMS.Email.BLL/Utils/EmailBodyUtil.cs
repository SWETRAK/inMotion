using System.Reflection;

namespace IMS.Email.BLL.Utils;

public static class EmailBodyUtil
{
    public static string GetSuccessLoginBody()
    {
        string data;
        try
        {
            data = Assembly.GetExecutingAssembly().GetManifestResourceStream("resources/correctlogin.html")?.ToString();
        }
        catch (Exception exception)
        {
            data = string.Empty;
        }
        return data;
    }

    public static string GetFailedLogginAttemptsBody()
    {
        string data;
        try
        {
            data = Assembly.GetExecutingAssembly().GetManifestResourceStream("resources/failedlogin.html")?.ToString();
        }
        catch (Exception exception)
        {
            data = string.Empty;
        }
        return data;
    }
}