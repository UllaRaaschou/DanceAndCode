namespace SimoneAPI.Authorization;

public enum UserPrivileges
{
    User = 1,
    Administrator = 2
}

public record User(string Username, string Password);

public record AuthorizedUser(string Username, string Password, UserPrivileges Privileges): User(Username, Password);
public record NotAuthorizedUser(string Username, string Password) : User(Username, Password);