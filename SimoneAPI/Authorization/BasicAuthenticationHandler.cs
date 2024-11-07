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

        var (username, password) = GetUsernameAndPasswordFromAuthHeader(header);
        var user = userAuthenticationService.Authentication(username, password);

        var authenticateResult = user is AuthorizedUser authorizedUser
            ? AuthenticateResult.Success(CreateAuthenticationTicket(authorizedUser))
            : AuthenticateResult.Fail("Authenticate failed");

        return await Task.FromResult(authenticateResult);
    }

    private static (string username, string password) GetUsernameAndPasswordFromAuthHeader(AuthenticationHeaderValue header)
    {
        var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(header.Parameter!)).Split(':');
        var username = credentials.FirstOrDefault() ?? string.Empty;
        var password = credentials.LastOrDefault() ?? string.Empty;

        return (username, password);
    }

    private AuthenticationTicket CreateAuthenticationTicket(AuthorizedUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.UserData, user.Username),
            new Claim(ClaimTypes.Role, user.Privileges.GetDisplayName()),
            new Claim(ClaimTypes.Expiration, DateTime.Now.AddHours(2).ToString(CultureInfo.InvariantCulture))
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return ticket;
    }
}