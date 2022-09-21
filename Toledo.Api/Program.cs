DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.RegisterDbContext();
builder.RegisterAuthentication();
builder.RegisterCors();
builder.RegisterAppServices();
builder.RegisterGraphQLServer();

var app = builder.Build();
app.ExecuteMigrations();
app.UseWebCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapGraphQL();

app.MapGet("/", () => "AAAAAAA CONCHALE VALE CHICO");

//KUBERNETES
//liveness, readiness and startup probes for containers
app.MapGet("/liveness", () => "Liveness Toledo");
app.MapGet("/readiness", () => "Readiness Toledo");

using (var context = new ToledoContext())
{
    context.Database.EnsureCreated();

    if(context.Users.Count() == 0)
    {
        string? salt = null;
        string hashedPassword = Toledo.Api.GraphQL.Mutation.UpdatePasswordMutation.GenerateHashPassword("12345678", ref salt);
        context.Users.Add(new()
        {
            Email = "johan@gmail.com",
            Gender = Toledo.Core.Enumerations.EnumGender.OTHER,
            Name = "Main User",
            Role = Toledo.Core.Enumerations.EnumRole.SUPER_ADMIN,
            Password = hashedPassword,
            PasswordSalt=(string)salt!
            
        });
        context.SaveChanges();
    }
}

await app.RunAsync();