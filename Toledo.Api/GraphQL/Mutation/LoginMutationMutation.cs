using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

public record LoginInput(string Email, string Password);

[ExtendObjectType(OperationTypeNames.Mutation)]
public class LoginMutation
{
    public async Task<UserPayload> Login(
        LoginInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        var user = await context.Users.FirstOrDefaultAsync(
            user => user.Email.Equals(input.Email) && user.Password.Equals(input.Password)
        );

        if (user is null)
            throw new GraphQLException(Errors.Exceptions.USER_NOT_FOUND);

        context.Users.Add(user);

        return new UserPayload(user);
    }
}
