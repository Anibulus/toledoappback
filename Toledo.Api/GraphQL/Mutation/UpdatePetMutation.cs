using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Toledo.Core.Enumerations;

namespace Toledo.Api.GraphQL.Mutation;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class UpdatePetMutation
{
    [Authorize]
    public async Task<PetPayload> UpdatePet(Guid id,
        PetInput input,
        ClaimsPrincipal claimsPrincipal,
        ToledoContext context
    )
    {
        var pet = context.Pets.FirstOrDefault(x => x.Id == id);
        if (pet is null)
            throw new GraphQLException(Errors.Exceptions.ID_NOT_FOUND);

        if (input.Name is not null)
            pet.Name = input.Name;

        if (input.Gender is not null)
            pet.Gender = (EnumGender)input.Gender;

        if (input.Breed is not null)
            pet.Breed = input.Breed;

        if (input.Size is not null)
            pet.Size = (EnumPetSize)input.Size;

        if (input.DateOfBirth is not null)
            pet.DateOfBirth = (DateTime)input.DateOfBirth;

        if (input.Sterilized is not null)
            pet.Sterilized = (bool)input.Sterilized;

        if (input.LocationOfSterilization is not null)
            pet.LocationOfSterilization = input.LocationOfSterilization;

        if (input.Address is not null)
            pet.Address = input.Address;

        if (input.Ubication is not null)
            pet.Ubication = input.Ubication;

        if (input.Longitude is not null)
            pet.Longitude = (double)input.Longitude;

        if (input.Latitude is not null)
            pet.Latitude = (double)input.Latitude;

        if (input.Zone is not null)
            pet.Zone = input.Zone;

        if (input.Notes is not null)
            pet.Notes = input.Notes;

        if (input.PetType is not null)
            pet.PetType = input.PetType;

        if (input.UserId is not null)
            pet.UserId = (Guid)input.UserId;


        context.Pets.Update(pet);

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
