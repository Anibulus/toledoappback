namespace Toledo.Api.GraphQL.Types;

public class PetDiseaseType : ObjectType<PetDisease>
{
    protected override void Configure(IObjectTypeDescriptor<PetDisease> descriptor)
    {
        descriptor.
            Field(st => st.Pet)
            .ResolveWith<PetDiseaseTypeResolver>(x => x.GetLocation(default!, default!))
            .UseDbContext<ToledoContext>();
    }

    private class PetDiseaseTypeResolver
    {
        public Pet? GetLocation([Parent] PetDisease petDisease, [ScopedService] ToledoContext context)
        {
            return context.Pets.FirstOrDefault(x => x.Id == petDisease.PetId);
        }
    }

}