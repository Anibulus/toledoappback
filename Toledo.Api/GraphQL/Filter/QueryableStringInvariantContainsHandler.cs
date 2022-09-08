using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

namespace Toledo.Api.GraphQL.Filter;

//BUSQUEDA CON ICASESENSITIVE
public class QueryableStringInvariantContainsHandler : QueryableStringOperationHandler
{
  public QueryableStringInvariantContainsHandler(InputParser inputParser) : base(inputParser) { }

  protected override int Operation => DefaultFilterOperations.Contains;
  public override Expression HandleOperation(QueryableFilterContext context, IFilterOperationField field, IValueNode value, object parsedValue)
  {
    Expression property = context.GetInstance();
    if (parsedValue is string str)
    {
      var toLower = Expression.Call(property, typeof(string).GetMethod("ToLower", Type.EmptyTypes));
      return Expression.Call(toLower, typeof(string).GetMethod(nameof(string.Contains), new Type[] { typeof(string) }), Expression.Constant(str.ToLower()));
    }
    throw new InvalidOperationException();
  }

}
