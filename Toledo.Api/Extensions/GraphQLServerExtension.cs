using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Data.Filters.Expressions;
using Toledo.Api.GraphQL.Filter;
using Toledo.Api.GraphQL.Mutation;
using Toledo.Api.GraphQL.Types;

namespace Toledo.Api.Extensions;

public static class GraphQLServerExtension
{
    public static WebApplicationBuilder RegisterGraphQLServer(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddGraphQLServer()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
            .AddConvention<IFilterConvention>(new FilterConvention(x => x.AddDefaults()))
            .AddConvention<IFilterConvention>(
                new FilterConventionExtension(descriptor =>
                {
                    descriptor.ArgumentName("filter");
                })
            )
            .AddType<UserType>()
            .AddType<LocationType>()
            .AddType<PetType>()
            .AddType<PetImageType>()
            .AddType<PetDiseaseType>()
            .AddQueryType<Global>()
            .AddMutationType()
            .AddTypeExtension<CreateLocationMutation>()
            .AddTypeExtension<UpdateLocationMutation>()
            .AddTypeExtension<DeleteLocationMutation>()
            .AddTypeExtension<CreatePetDiseaseMutation>()
            .AddTypeExtension<UpdatePetDiseaseMutation>()
            .AddTypeExtension<DeletePetDiseaseMutation>()
            .AddTypeExtension<CreatePetImageMutation>()
            .AddTypeExtension<UpdatePetImageMutation>()
            .AddTypeExtension<DeletePetImageMutation>()
            .AddTypeExtension<CreatePetMutation>()
            .AddTypeExtension<UpdatePetMutation>()
            .AddTypeExtension<DeletePetMutation>()
            .AddTypeExtension<CreateUserMutation>()
            .AddTypeExtension<UpdateUserMutation>()
            .AddTypeExtension<DeleteUserMutation>()
            .AddTypeExtension<LoginMutation>()
            .ConfigureResolverCompiler(c => c.AddService<ToledoContext>())
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            .AddConvention<IFilterConvention>(
                new FilterConventionExtension(
                    x =>
                        x.AddProviderExtension(
                            new QueryableFilterProviderExtension(
                                y => y.AddFieldHandler<QueryableStringInvariantContainsHandler>()
                            )
                        )
                )
            );

        //.PublishSchemaDefinition(c => c.SetName("toledo").AddTypeExtensionsFromFile("./Stitching.graphql"));

        return builder;
    }
}
