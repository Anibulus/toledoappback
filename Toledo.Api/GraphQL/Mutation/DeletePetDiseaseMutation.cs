using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;
using Toledo.Core.DTO;

namespace Toledo.Api.GraphQL.Mutation;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class DeletePetDiseaseMutation
{
    [Authorize]
    public async Task<DeletedId> DeletePetDisease(
        Guid id,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        var petDisease = context.PetDiseases.FirstOrDefault(x => x.Id == id);
        if (petDisease is null)
            throw new GraphQLException(Errors.Exceptions.ID_NOT_FOUND);

        context.PetDiseases.Remove(petDisease);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new GraphQLException(e.Message);
        }

        return new DeletedId(petDisease.Id);
    }
}
