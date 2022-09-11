using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

public record UpdateUserInput(
    string? DNI,
    string? Name,
    string? Email,
    EnumRole? Role,
    EnumGender? Gender,
    string? Phone,
    string? Observation
);

[ExtendObjectType(OperationTypeNames.Mutation)]
public class UpdateUserMutation
{
    [Authorize]
    public async Task<UserPayload> UpdateUser(
        Guid id,
        UpdateUserInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        var user = context.Users.FirstOrDefault(x => x.Id == id);
        if (user is null)
            throw new GraphQLException(Errors.Exceptions.ID_NOT_FOUND);

        if (input.DNI is not null)
            user.DNI = input.DNI;

        if (input.Name is not null)
            user.Name = input.Name;

        if (input.Email is not null)
            user.Email = input.Email;

        if (input.Role is not null)
            user.Role = (EnumRole)input.Role;

        if (input.Gender is not null)
            user.Gender = (EnumGender)input.Gender;

        if (input.Phone is not null)
            user.Phone = input.Phone;

        if (input.Observation is not null)
            user.Observation = input.Observation;

        context.Users.Update(user);

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
