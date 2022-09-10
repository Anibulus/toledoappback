using HotChocolate.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Toledo.Core.Enumerations;
using Toledo.Infrastructure.Services;

namespace Toledo.Api.GraphQL.Mutation;

public record LoginInput(string Email, string Password);

public record LoginPayload(User user, string autentication);

[ExtendObjectType(OperationTypeNames.Mutation)]
public class LoginMutation
{
    public async Task<LoginPayload> Login(
        LoginInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context,
        [Service] IOptions<TokenSettings> tokenSettings
    )
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Email.Equals(input.Email));

        if (user is null)
            throw new GraphQLException(Errors.Exceptions.USER_NOT_FOUND);

        #region Validate password
        string pwd = input.Password;
        string salt = user.PasswordSalt;
        string pwdHashed = SecurityHelper.HashPassword(pwd, salt, 10101, 70);

        if (!user.Password.Equals(pwdHashed))
            throw new GraphQLException(Errors.Exceptions.INCORRECT_CREDENTIALS);
        #endregion

        user.LoginCount = user.LoginCount++;
        user.LastLogin = DateTime.UtcNow;

        context.Users.Update(user);
        await context.SaveChangesAsync();

        var token = TokenService.GenerateToken(tokenSettings, user.Id, user.Role.ToString());

        return new LoginPayload(user, token);
    }
}
