using Employee_Management_System_Backend.Data;
using Employee_Management_System_Backend.Services;
using Employee_Management_System_Backend.src.Extensions;
using Employee_Management_System_Backend.src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configures the Identity system to manage users and roles using Entity Framework Core.
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()
                         .AllowAnyHeader()
                         .AllowAnyMethod();
        });
});

builder.Services.AddCustomJwtAuth(builder.Configuration);
builder.Services.AddSwaggerGenJwtAuth();


var app = builder.Build();

// Use CORS policy
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var port = builder.Configuration["Kestrel:Endpoints:Https:Url"];
if (!string.IsNullOrEmpty(port))
{
    app.Logger.LogInformation($"HTTPS Redirection to port: {port}");
}
else
{
    app.Logger.LogWarning("HTTPS Redirection port is not set.");
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
