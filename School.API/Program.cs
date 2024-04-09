using DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using School.Contracts.Interfaces;
using School.Service.Implementations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection for services
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();

// Configure in-memory database
builder.Services.AddDbContext<SchoolContext>(options =>
        options.UseInMemoryDatabase("SchoolDb"));

// Add authentication (assuming JWT-based)
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//  .AddJwtBearer(options => // Configure JWT settings);

// Add services for controllers (**fix here**)
builder.Services.AddControllers();

// Add Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Management API", Version = "v1" });
});


// Build the application
var app = builder.Build();

// Enable middleware
app.UseHttpsRedirection();
//app.UseAuthentication(); // Enable authentication (optional)
app.UseRouting();
//app.UseAuthorization();  (optional)
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management API V1");
});

app.Run();