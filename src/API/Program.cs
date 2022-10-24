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
builder.Services.AddAutoMapper(x =>
{
    x.AddProfile<DefaultMappingProfile>();
});

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSnakeCaseNamingConvention();
    x.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
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
    var context = services.GetRequiredService<DbContext>();
    context.Database.Migrate();
}
catch (Exception e)
{
    // ignored
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "An error occurred seeding the DB");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();