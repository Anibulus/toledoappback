using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

public record UpdateUserInput(
    string? DNI,
    string? DNIType,
    string? Name,
    string? Email,
    EnumRole? Role,
    EnumGender? Gender,
    string? Phone,
    string? Observation,
    LocationInput? Location
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
        var user = context.Users.Include(x=>x.Location).FirstOrDefault(x => x.Id == id);
        if (user is null)
            throw new GraphQLException(Errors.Exceptions.ID_NOT_FOUND);

        if (input.DNI is not null)
            user.DNI = input.DNI;

        if (input.DNIType is not null)
            user.DNIType = input.DNIType;

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

            if (input.Location is not null && user.Location is not null)
            {
                UpdateLocationMutation locationMutation = new();
                await locationMutation.UpdateLocation(user.Location.Id,
                    new(
                        user.Id,
                        input.Location.Longitude,
                        input.Location.Latitude,
                        input.Location.Zone,
                        input.Location.Address
                    ),
                    claimsPrincipal,
                    context
                );
            }
        }
        catch (Exception e)
        {
            throw new GraphQLException(e.Message);
        }

        return new UserPayload(user);
    }
}
