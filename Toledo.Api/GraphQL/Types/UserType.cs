namespace Toledo.Api.GraphQL.Types;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.
            Field(st => st.Location)
            .ResolveWith<UserTypeResolver>(x => x.GetLocation(default!, default!))
            .UseDbContext<ToledoContext>();
        descriptor.
            Field(st => st.Pets)
            .ResolveWith<UserTypeResolver>(x => x.GetPets(default!, default!))
            .UseDbContext<ToledoContext>();
    }

    private class UserTypeResolver
    {
        public Toledo.Core.Entities.Location? GetLocation([Parent] User user, [ScopedService] ToledoContext context)
        {
            return context.Locations.FirstOrDefault(x => x.UserId == user.Id);
        }
        public IQueryable<Pet> GetPets([Parent] User user, [ScopedService] ToledoContext context)
        {
            return context.Pets.Where(x => x.UserId == user.Id);
        }
    }

}