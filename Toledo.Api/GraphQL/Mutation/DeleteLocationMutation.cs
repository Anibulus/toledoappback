using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class DeleteLocationMutation
{
    public async Task<Guid> DeleteLocation(
        Guid id,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        var location = context.Locations.FirstOrDefault(x => x.Id == id);
        if (location is null)
            throw new GraphQLException(Errors.Exceptions.ID_NOT_FOUND);

        context.Locations.Remove(location);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new GraphQLException(e.Message);
        }

        return location.Id;
    }
}
