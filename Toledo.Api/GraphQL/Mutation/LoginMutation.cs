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

        if (!user.Active)
            throw new GraphQLException(Errors.Exceptions.USER_IS_NOT_ACTIVE);

        #region Validate password
        string? salt = user.PasswordSalt;
        string pwdHashed = UpdatePasswordMutation.GenerateHashPassword(input.Password, ref salt);

        if (!user.Password.Equals(pwdHashed))
            throw new GraphQLException(Errors.Exceptions.INCORRECT_CREDENTIALS);
        #endregion
        
        user.LoginCount++;
        user.LastLogin = DateTime.UtcNow;

        context.Users.Update(user);
        await context.SaveChangesAsync();

        var token = TokenService.GenerateToken(tokenSettings, user.Id, user.Role.ToString());

        return new LoginPayload(user, token);
    }
}
