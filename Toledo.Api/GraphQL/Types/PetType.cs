using HotChocolate.AspNetCore.Authorization;

namespace Toledo.Api.GraphQL.Types;

public class PetType : ObjectType<Pet>
{
    protected override void Configure(IObjectTypeDescriptor<Pet> descriptor)
    {
        descriptor.
            Field(st => st.Diseases)
            .ResolveWith<PetTypeResolver>(x => x.GetPetDiseases(default!, default!))
            .UseDbContext<ToledoContext>();
        descriptor.
            Field(st => st.PetImages)
            .ResolveWith<PetTypeResolver>(x => x.GetImages(default!, default!))
            .UseDbContext<ToledoContext>();
        descriptor.
            Field(st => st.Years)
            .ResolveWith<PetTypeResolver>(x => x.GetYears(default!, default!))
            .UseDbContext<ToledoContext>();
        descriptor.
            Field(st => st.Silvestre)
            .ResolveWith<PetTypeResolver>(x => x.GetSilvestre(default!, default!))
            .UseDbContext<ToledoContext>();
        descriptor.
            Field(st => st.User)
            .ResolveWith<PetTypeResolver>(x => x.GetUser(default!, default!))
            .UseDbContext<ToledoContext>();
    }

    private class PetTypeResolver
    {
        public IQueryable<PetDisease> GetPetDiseases([Parent] Pet pet, [ScopedService] ToledoContext context)
        {
            return context.PetDiseases.Where(x => x.PetId == pet.Id);
        }
        public IQueryable<PetImage> GetImages([Parent] Pet pet, [ScopedService] ToledoContext context)
        {
            return context.PetImages.Where(x => x.PetId == pet.Id);
        }
        public int GetYears([Parent] Pet pet, [ScopedService] ToledoContext context)
        {
            int age = 0;
            age = DateTime.Now.Year - pet.DateOfBirth.Year;
            if (DateTime.Now.DayOfYear < pet.DateOfBirth.DayOfYear)
                age = age - 1;
            return age;
        }
        public bool GetSilvestre([Parent] Pet pet, [ScopedService] ToledoContext context)
        {
            return pet.UserId is null;
        }
        [Authorize]
        public User? GetUser([Parent] Pet pet, [ScopedService] ToledoContext context)
        {
            return context.Users.FirstOrDefault(x=> x.Id == pet.UserId);
        }
    }

}