using System.Security.Claims;
using System.Text;
using EventService.API.DependencyInjection;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Event Service API",
        Description = "API for managing Events and EventStatuses using Clean Architecture and CQRS.",
        Contact = new OpenApiContact
        {
            Name = "Ticketech Team",
            Email = "support@TicketechTeam.local"
        }
    });
});


// Base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);

    options.EnableSensitiveDataLogging();   
    options.EnableDetailedErrors();         
    options.LogTo(Console.WriteLine,        
        Microsoft.Extensions.Logging.LogLevel.Information);
});


// Inyección de dependencias
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwt = builder.Configuration.GetSection("Jwt");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwt["Issuer"],
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"])),
        RoleClaimType = "userRole",
    };

}); 

var app = builder.Build();

// Middleware personalizado para agarrar Exceptions
app.UseMiddleware<EventService.API.Middlewares.ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Service API v1");
        c.RoutePrefix = "swagger"; 
    });
}

// CORS
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();  
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
