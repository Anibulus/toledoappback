using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toledo.Core.Interfaces;
using Toledo.Infrastructure.Repositories;

namespace Toledo.Api.Extensions;

public static class AppServicesExtension
{
  public static void RegisterAppServices(this WebApplicationBuilder builder)
  {
    //builder.Services.AddSingleton<ICountryService, CountryService>();
    //builder.Services.AddTransient<IMemberService, MemberService>();
    //builder.Services.AddTransient<ICountryService, CountryService>();
    builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
  }
}
