using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class UpdatePetImageMutation
{
    [Authorize]
    public async Task<PetImagePayload> UpdatePetImage(
        Guid id,
        PetImageInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        var petImage = context.PetImages.FirstOrDefault(x => x.Id == id);
        if (petImage is null)
            throw new GraphQLException(Errors.Exceptions.ID_NOT_FOUND);

        if (input.Url is not null)
            petImage.Url = input.Url;

        if (input.PetId is not null)
            petImage.PetId = (Guid)input.PetId;

        context.PetImages.Update(petImage);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new GraphQLException(e.Message);
        }

        return new PetImagePayload(petImage);
    }
}
