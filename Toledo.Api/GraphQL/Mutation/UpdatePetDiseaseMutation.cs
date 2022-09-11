using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class UpdatePetDiseaseMutation
{
    [Authorize]
    public async Task<PetDiseasePayload> UpdatePetDisease(
        Guid id,
        PetDiseaseInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        var petDisease = context.PetDiseases.FirstOrDefault(x => x.Id == id);
        if (petDisease is null)
            throw new GraphQLException(Errors.Exceptions.ID_NOT_FOUND);

        if (input.Description is not null)
            petDisease.Description = input.Description;

        if (input.PetId is not null)
            petDisease.PetId = (Guid)input.PetId;

        context.PetDiseases.Update(petDisease);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new GraphQLException(e.Message);
        }

        return new PetDiseasePayload(petDisease);
    }
}
