namespace SimoneAPI.Authorization
{
    public class UserAuthenticationService
    {
        private readonly List<AuthorizedUser> users =
        [
            new("Ulla", "123456", UserPrivileges.User),
            new("Simone", "123456", UserPrivileges.Administrator)
        ];

        public User Authentication(string username, string password)
        {
            var authenticationUser = users.FirstOrDefault(user => 
                string.Equals(user.Username, username, StringComparison.CurrentCultureIgnoreCase) && 
                string.Equals(user.Password, password, StringComparison.CurrentCultureIgnoreCase)
            );
 
            return authenticationUser is not null  ? authenticationUser : new NotAuthorizedUser(username, password);
        }
    }
}