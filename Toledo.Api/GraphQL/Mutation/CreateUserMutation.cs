using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;
using Toledo.Infrastructure.Services;

namespace Toledo.Api.GraphQL.Mutation;

public record CreateUserInput(
    string? DNI,
    string? Name,
    string? Email,
    string? Password,
    EnumRole? Role,
    EnumGender? Gender,
    string? Phone,
    string? Observation
);

public record UserPayload(User user);

[ExtendObjectType(OperationTypeNames.Mutation)]
public class CreateUserMutation
{
    [Authorize]
    public async Task<UserPayload> CreateUser(
        CreateUserInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        if (input.DNI is null)
            throw new GraphQLException(input.DNI+Errors.Exceptions.ELEMENT_CANNOT_BE_NULL);

        var isUserDuplicated = context.Users.Any(user => user.DNI.Equals(input.DNI));

        if (isUserDuplicated)
            throw new GraphQLException(Errors.Exceptions.DNI_DUPLICATED);

        string? salt = null;
        string pwdHashed = UpdatePasswordMutation.GenerateHashPassword(input.Password ?? "", ref salt);

        var user = new User
        {
            DNI = (string)input.DNI,
            Name = input.Name ?? "",
            Email = input.Email ?? "",
            PasswordSalt = salt!,
            Password = pwdHashed,
            Role = input.Role ?? EnumRole.USER,
            Gender = input.Gender ?? EnumGender.OTHER,
            Phone = input.Phone ?? "",
            Observation = input.Observation,
            Active = true
        };

        context.Users.Add(user);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new GraphQLException(e.Message);
        }

        return new UserPayload(user);
    }
}
