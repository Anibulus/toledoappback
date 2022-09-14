using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;
using Toledo.Core.DTO;

namespace Toledo.Api.GraphQL.Mutation;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class DeletePetImageMutation
{
    [Authorize]
    public async Task<DeletedId> DeletePetImage(
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

        return new DeletedId(petImage.Id);
    }
}
