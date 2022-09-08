using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Data.Filters.Expressions;
using Toledo.Api.GraphQL.Filter;

namespace Toledo.Api.Extensions;

public static class GraphQLServerExtension
{
  public static WebApplicationBuilder RegisterGraphQLServer(this WebApplicationBuilder builder)
  {
    builder.Services
    .AddGraphQLServer()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddConvention<IFilterConvention>(new FilterConvention(x =>
        x.AddDefaults()))
    .AddConvention<IFilterConvention>(new FilterConventionExtension(descriptor =>
    {
      descriptor.ArgumentName("filter");
    }))
    .AddQueryType<Global>()
    //.AddMutationType()
    //    .AddTypeExtension<CreateMemberMutation>()
    .ConfigureResolverCompiler(c => c.AddService<ToledoContext>())
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddConvention<IFilterConvention>(new 
                FilterConventionExtension(x => x.AddProviderExtension(
                    new QueryableFilterProviderExtension(y => y.AddFieldHandler<QueryableStringInvariantContainsHandler>()))));

    //.PublishSchemaDefinition(c => c.SetName("toledo").AddTypeExtensionsFromFile("./Stitching.graphql"));
    
    return builder;
  }
}
