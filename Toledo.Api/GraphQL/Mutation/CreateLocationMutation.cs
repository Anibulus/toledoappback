using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

public record LocationInput(
    Guid? UserId,
    double? Longitude,
    double? Latitude,
    string? Zone,
    string? Address
);

public record LocationPayload(Toledo.Core.Entities.Location location);

[ExtendObjectType(OperationTypeNames.Mutation)]
public class CreateLocationMutation
{
    [Authorize]
    public async Task<LocationPayload> CreateLocation(
        LocationInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        if (input.UserId is null)
            throw new GraphQLException(
                nameof(input.UserId) + Errors.Exceptions.ELEMENT_CANNOT_BE_NULL
            );

        var location = new Toledo.Core.Entities.Location
        {
            UserId = (Guid)input.UserId,
            Longitude = input.Longitude,
            Latitude = input.Latitude,
            Zone = input.Zone ?? "",
            Address = input.Address ?? ""
        };

        context.Locations.Add(location);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new GraphQLException(e.Message);
        }

        return new LocationPayload(location);
    }
}
