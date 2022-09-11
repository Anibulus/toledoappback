using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using HotChocolate.AspNetCore.Authorization;
using Toledo.Api.ActionFilters;

namespace Toledo.Api.GraphQL.Mutation;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class DeleteLocationMutation
{
    [Authorize]
    //[MayPerfom]
    //[ServiceFilter(typeof(MayPerfom))]
    public async Task<Guid> DeleteLocation(
        Guid id,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        Console.WriteLine("\n\n\n==Getting in");
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
