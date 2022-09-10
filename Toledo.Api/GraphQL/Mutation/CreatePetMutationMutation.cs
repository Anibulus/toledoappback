using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

public record PetInput(
    string? Name,
    EnumGender? Gender,
    string? Breed,
    EnumPetSize? Size,
    DateTime? DateOfBirth,
    bool? Sterilized,
    string? LocationOfSterilization,
    string? Address,
    string? Ubication,
    double? Longitude,
    double? Latitude,
    string? Zone,
    string? Notes,
    string? PetType,
    Guid? UserId
);

public record PetPayload(Pet pet);

[ExtendObjectType(OperationTypeNames.Mutation)]
public class CreatePetMutation
{
    public async Task<PetPayload> CreatePet(
        PetInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        if (input.UserId is null)
            throw new GraphQLException(
                nameof(input.UserId) + Errors.Exceptions.ELEMENT_CANNOT_BE_NULL
            );

        var pet = new Pet
        {
            Name = input.Name ?? "",
            Gender = input.Gender ?? EnumGender.OTHER,
            Breed = input.Breed ?? "",
            Size = input.Size ?? EnumPetSize.MEDIUM,
            DateOfBirth = input.DateOfBirth ?? DateTime.Now,
            Sterilized = input.Sterilized ?? false,
            LocationOfSterilization = input.LocationOfSterilization,
            Address = input.Address ?? "",
            Ubication = input.Ubication ?? "",
            Longitude = input.Longitude ?? 0,
            Latitude = input.Latitude ?? 0,
            Zone = input.Zone ?? "",
            Notes = input.Notes ?? "",
            PetType = input.PetType ?? "",
            UserId = (Guid)input.UserId
        };

        context.Pets.Add(pet);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new GraphQLException(e.Message);
        }

        return new PetPayload(pet);
    }
}
