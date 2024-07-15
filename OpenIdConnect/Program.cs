using Microsoft.EntityFrameworkCore;
using OpenIdConnect.Db;
using OpenIdConnect.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddGithubOidcConfigurations(builder.Configuration);
builder.Services.AddGoogleOidcConfigurations(builder.Configuration);
builder.Services.AddAppServices();
builder.Services.AddDbContext<OidcDb>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("OidcDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
