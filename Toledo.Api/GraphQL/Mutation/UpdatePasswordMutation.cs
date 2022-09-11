using HotChocolate.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Toledo.Core.Enumerations;
using Toledo.Infrastructure.Services;

namespace Toledo.Api.GraphQL.Mutation;

public record UpdatePasswordInput(Guid UserId, string OldPassword, string Password);

[ExtendObjectType(OperationTypeNames.Mutation)]
public class UpdatePasswordMutation
{
    [Authorize]
    public async Task<UserPayload> UpdatePassword(
        UpdatePasswordInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context,
        [Service] IOptions<TokenSettings> tokenSettings
    )
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Id == input.UserId);

        if (user is null)
            throw new GraphQLException(Errors.Exceptions.USER_NOT_FOUND);

        string? salt = user.PasswordSalt;
        string oldHashPassword = GenerateHashPassword(input.OldPassword, ref salt);

        if (!oldHashPassword.Equals(user.Password))
            throw new GraphQLException(Errors.Exceptions.INCORRECT_CREDENTIALS);

        salt = null;
        string newHashPassword = GenerateHashPassword(input.Password, ref salt);

        user.PasswordSalt = salt!;
        user.Password = newHashPassword;

        context.Users.Update(user);
        await context.SaveChangesAsync();

        return new UserPayload(user);
    }

    [GraphQLIgnore]
    public static string GenerateHashPassword(string password, ref string? salt)
    {
        salt ??= SecurityHelper.GenerateSalt();
        return SecurityHelper.HashPassword(password, salt, 10101, 70);
    }
}
