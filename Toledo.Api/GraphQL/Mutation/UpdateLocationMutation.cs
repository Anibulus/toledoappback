using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class UpdateLocationMutation
{
    [Authorize]
    public async Task<LocationPayload> UpdateLocation(
        Guid id,
        LocationInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        var location = context.Locations.FirstOrDefault(x => x.Id == id);
        if (location is null)
            throw new GraphQLException(Errors.Exceptions.ID_NOT_FOUND);

        if (input.Address is not null)
            location.Address = input.Address;

        if (input.Latitude is not null)
            location.Latitude = input.Latitude;

        if (input.Longitude is not null)
            location.Longitude = input.Longitude;

        if (input.UserId is not null)
            location.UserId = (Guid)input.UserId;

        context.Locations.Update(location);

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
