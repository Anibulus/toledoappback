using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

public record PetDiseaseInput(string? Description, Guid? PetId);

public record PetDiseasePayload(PetDisease petDisease);

[ExtendObjectType(OperationTypeNames.Mutation)]
public class CreatePetDiseaseMutation
{
    [Authorize]
    public async Task<PetDiseasePayload> CreatePetDisease(
        PetDiseaseInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        if (input.PetId is null)
            throw new GraphQLException(
                nameof(input.PetId) + Errors.Exceptions.ELEMENT_CANNOT_BE_NULL
            );

        var petDisease = new PetDisease
        {
            Description = input.Description ?? "",
            PetId = (Guid)input.PetId
        };

        context.PetDiseases.Add(petDisease);

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
