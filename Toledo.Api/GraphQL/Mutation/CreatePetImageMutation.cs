using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

public record PetImageInput(string? Url, Guid? PetId);

public record PetImagePayload(PetImage petImage);

[ExtendObjectType(OperationTypeNames.Mutation)]
public class CreatePetImageMutation
{
    public async Task<PetImagePayload> CreatePetImage(
        PetImageInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        if (input.PetId is null)
            throw new GraphQLException(
                nameof(input.PetId) + Errors.Exceptions.ELEMENT_CANNOT_BE_NULL
            );

        var petImage = new PetImage { PetId = (Guid)input.PetId, Url = input.Url ?? "" };

        context.PetImages.Add(petImage);

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
