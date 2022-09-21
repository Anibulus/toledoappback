using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.AspNetCore.Serialization;
using HotChocolate.Execution;
using System.Net;
namespace Toledo.Api.GraphQL.Filter;

public class YuboHttpResult: DefaultHttpResultSerializer
{
	public override HttpStatusCode GetStatusCode(IExecutionResult result)
	{
		return result switch
		{
			QueryResult => HttpStatusCode.OK,
			DeferredQueryResult => HttpStatusCode.OK,
			BatchQueryResult => HttpStatusCode.OK,
			_ => HttpStatusCode.InternalServerError
		};
	}
}
