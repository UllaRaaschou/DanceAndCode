using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;

namespace SimoneAPI.Authorization;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly UserAuthenticationService userAuthenticationService = new();

    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder)
    {
        
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!AuthenticationHeaderValue.TryParse(Request.Headers.Authorization, out var header))
        {
            return await Task.FromResult(AuthenticateResult.Fail("Missing Authorization header"));
        }

        var (username, password) = GetNotAuthorizedUserInfo(header);
        var userInfo = userAuthenticationService.AuthenticationUser(username, password);

        if (userInfo is NotAuthorizedUser)
        {
            return await Task.FromResult(AuthenticateResult.Fail("Authenticate failed"));
        }

        var authUser = (AuthorizedUser)userInfo;
        var claims = new[]
        {
            new Claim(ClaimTypes.UserData, userInfo.Username),
            new Claim(ClaimTypes.Expiration, DateTime.Now.AddHours(2).ToString(CultureInfo.InvariantCulture)),
            new Claim(ClaimTypes.Role, authUser.Privileges.GetDisplayName())
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return await Task.FromResult(AuthenticateResult.Success(ticket));
    }

    private static (string username, string password) GetNotAuthorizedUserInfo(AuthenticationHeaderValue header)
    {
        var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(header.Parameter!)).Split(':');
        var username = credentials.FirstOrDefault() ?? string.Empty;
        var password = credentials.LastOrDefault() ?? string.Empty;

        return (username, password);
    }
}
