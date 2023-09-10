using System.Security.Claims;
using System.Text.Encodings.Web;
using IMS.Shared.Messaging.Messages;
using IMS.Shared.Messaging.Messages.JWT;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IMS.Shared.Messaging.Authorization;

public class RemoteTokenAuthenticationSchemeHandler: AuthenticationHandler<RemoteTokenAuthenticationSchemeOptions>
{
    public const string SchemaName = "RemoteTokenAuthenticationScheme";
    private readonly IRequestClient<ImsBaseMessage<RequestJwtValidationMessage>> _requestClient;
    
    public RemoteTokenAuthenticationSchemeHandler(
        IOptionsMonitor<RemoteTokenAuthenticationSchemeOptions> options, 
        ILoggerFactory logger, 
        UrlEncoder encoder,
        ISystemClock clock, 
        IRequestClient<ImsBaseMessage<RequestJwtValidationMessage>> requestClient
    ) : base(options, logger, encoder, clock)
    {
        _requestClient = requestClient;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var jwtAuthString = Context.Request.Headers.Authorization.FirstOrDefault();

        if (jwtAuthString is null || jwtAuthString.Equals(string.Empty))
        {
            return AuthenticateResult.Fail("Authentication failed, request with no token");
        }


        var jwtTokenArray = jwtAuthString.Split(" ");
        if (jwtTokenArray.Length != 2 || !jwtTokenArray[0].Equals("Bearer"))
        {
            return AuthenticateResult.Fail("Authentication failed, incorrect token format");
        }
        
        var message = new ImsBaseMessage<RequestJwtValidationMessage>
        {
            Data = new RequestJwtValidationMessage
            {
                JwtToken = jwtTokenArray[1]
            }
        };

        var userInfoMessageResponse = await _requestClient.GetResponse<ImsBaseMessage<ValidatedUserInfoMessage>>(message);

        if (userInfoMessageResponse.Message.Data is null)
        {
            return AuthenticateResult.Fail("Authentication failed, Received data from server is null");
        }
        
        var principal = new ClaimsPrincipal(new ClaimsIdentity(
            new Claim[]
            {
                new(ClaimTypes.NameIdentifier, userInfoMessageResponse.Message.Data.Id),
                new(ClaimTypes.Email, userInfoMessageResponse.Message.Data.Email),
                new(ClaimTypes.Name, userInfoMessageResponse.Message.Data.Nickname),
                new(ClaimTypes.Role, userInfoMessageResponse.Message.Data.Role)
            },
            SchemaName
        ));
                    
        Context.User = principal;
        var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}