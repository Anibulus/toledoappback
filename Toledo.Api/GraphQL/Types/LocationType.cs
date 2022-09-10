namespace Toledo.Api.GraphQL.Types;

public class LocationType : ObjectType<Toledo.Core.Entities.Location>
{
    protected override void Configure(IObjectTypeDescriptor<Toledo.Core.Entities.Location> descriptor)
    {
        descriptor.
            Field(st => st.User)
            .ResolveWith<LocationTypeResolver>(x => x.GetUser(default!, default!))
            .UseDbContext<ToledoContext>();
    }

    private class LocationTypeResolver
    {
        public User? GetUser([Parent] Toledo.Core.Entities.Location location, [ScopedService] ToledoContext context)
        {
            return context.Users.FirstOrDefault(x => x.Id == location.UserId);
        }
    }

}