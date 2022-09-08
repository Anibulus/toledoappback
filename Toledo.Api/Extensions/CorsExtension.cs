using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toledo.Api.Extensions;

public static class CorsExtension
{
  public static WebApplicationBuilder RegisterCors(this WebApplicationBuilder builder)
  {
    builder.Services.AddCors(option =>
    {
      option.AddPolicy("allowedOrigin",
          builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
          );
    });
    return builder;
  }

  public static void UseWebCors(this WebApplication app)
  {
    app.UseCors("allowedOrigin");
  }
}