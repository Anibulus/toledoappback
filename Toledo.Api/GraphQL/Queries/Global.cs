using System.Security.Claims;
using HotChocolate.AspNetCore.Authorization;
using Toledo.Api.ActionFilters;

namespace Toledo.Api.GraphQL.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class Global
    {
        [UseProjection]
        public User? GetUserById(Guid id, ClaimsPrincipal claimsPrincipal, ToledoContext context)
        {
            //var userIdStr = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            return context.Users.FirstOrDefault(x=>x.Id == id);
        }

        [Authorize]
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<User> ListUsers(ClaimsPrincipal claimsPrincipal, ToledoContext context)
        {
            //var userIdStr = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            return context.Users.AsQueryable();
        }

        [Authorize]
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Toledo.Core.Entities.Location> ListLocations(
            ClaimsPrincipal claimsPrincipal,
            ToledoContext context
        )
        {
            return context.Locations.AsQueryable();
        }

        [UseProjection]
        public Pet? GetPetById(Guid id, ClaimsPrincipal claimsPrincipal, ToledoContext context)
        {
            //var userIdStr = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            return context.Pets.FirstOrDefault(x=>x.Id == id);
        }

        
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Pet> ListPets(ClaimsPrincipal claimsPrincipal, ToledoContext context)
        {
            return context.Pets.AsQueryable();
        }

        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<PetImage> ListPetImages(
            ClaimsPrincipal claimsPrincipal,
            ToledoContext context
        )
        {
            return context.PetImages.AsQueryable();
        }

        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<PetDisease> ListPetDiseases(ClaimsPrincipal claimsPrincipal, ToledoContext context)
        {
            return context.PetDiseases.AsQueryable();
        }
    }
}
