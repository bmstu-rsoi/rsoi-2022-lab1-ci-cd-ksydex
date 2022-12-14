using API.Data;
using API.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration; // allows both to access and to set up the config
var environment = builder.Environment;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(x => { x.AddProfile<DefaultMappingProfile>(); });

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSnakeCaseNamingConvention();
    x.UseNpgsql((environment.IsProduction()
                    ? GetProductionDbConnectionString(null)
                    : GetProductionDbConnectionString(configuration.GetConnectionString("DefaultConnection"))) ??
                throw new NullReferenceException("Database URL is not set!"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}
catch (Exception e)
{
    // ignored
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "An error occurred seeding the DB");
}

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

static string? GetProductionDbConnectionString(string? v)
{
    var connectionUrl = v ?? Environment.GetEnvironmentVariable("DATABASE_URL");

    if (connectionUrl == null) return null;
    
    connectionUrl = connectionUrl.Replace("postgres://", string.Empty);
    var userPassSide = connectionUrl.Split("@")[0];
    var hostSide = connectionUrl.Split("@")[1];

    var user = userPassSide.Split(":")[0];
    var password = userPassSide.Split(":")[1];
    var host = hostSide.Split("/")[0];
    var database = hostSide.Split("/")[1].Split("?")[0];

    return $"Host={host};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true";
}