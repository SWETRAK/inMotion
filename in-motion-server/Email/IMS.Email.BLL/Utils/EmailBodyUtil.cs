using System.Reflection;
using Microsoft.Extensions.Logging;

namespace IMS.Email.BLL.Utils;

public static class EmailBodyUtil
{
    public static string GetSuccessLoginBody(ILogger logger)
    {
        return GetResource("IMS.Email.BLL.resources.correctlogin.html", logger);
    }

    public static string GetFailedLogginAttemptsBody(ILogger logger)
    {
        return GetResource("IMS.Email.BLL.resources.failedlogin.html", logger);
    }
    
    public static string GetAccountActivationBody(ILogger logger)
    {
        return GetResource("IMS.Email.BLL.resources.activateaccount.html", logger);
    }
    
    private static string GetResource(string resourceName, ILogger logger)
    {
        string data;
        try
        {
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (resource != null)
            {
                using var sr = new StreamReader(resource);
                data = sr.ReadToEnd();
            }
            else
            {
                data = string.Empty;
            }
        }
        catch (Exception exception)
        {
            logger.LogError(exception ,"Error reading file from assembly {FileName}, Exception with message {Message}", resourceName, exception.Message);
            data = string.Empty;
        }
        return data;
    }
}