using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class DeletePetImageMutation
{
    public async Task<Guid> DeletePetImage(
        Guid id,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        var petImage = context.PetImages.FirstOrDefault(x => x.Id == id);
        if (petImage is null)
            throw new GraphQLException(Errors.Exceptions.ID_NOT_FOUND);

        context.PetImages.Remove(petImage);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new GraphQLException(e.Message);
        }

        return petImage.Id;
    }
}
