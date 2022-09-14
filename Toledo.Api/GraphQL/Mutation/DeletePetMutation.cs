using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;
using Toledo.Core.DTO;

namespace Toledo.Api.GraphQL.Mutation;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class DeletePetMutation
{
    [Authorize]
    public async Task<DeletedId> DeletePet(
        Guid id,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        var pet = context.Pets.FirstOrDefault(x => x.Id == id);
        if (pet is null)
            throw new GraphQLException(Errors.Exceptions.ID_NOT_FOUND);

        context.Pets.Remove(pet);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new GraphQLException(e.Message);
        }

        return new DeletedId(pet.Id);
    }
}
