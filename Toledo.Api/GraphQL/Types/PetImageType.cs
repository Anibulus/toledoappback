namespace Toledo.Api.GraphQL.Types;

public class PetImageType : ObjectType<PetImage>
{
    protected override void Configure(IObjectTypeDescriptor<PetImage> descriptor)
    {
        descriptor.
            Field(st => st.Pet)
            .ResolveWith<PetImageTypeResolver>(x => x.GetLocation(default!, default!))
            .UseDbContext<ToledoContext>();
    }

    private class PetImageTypeResolver
    {
        public Pet? GetLocation([Parent] PetImage petImage, [ScopedService] ToledoContext context)
        {
            return context.Pets.FirstOrDefault(x => x.Id == petImage.PetId);
        }
    }

}