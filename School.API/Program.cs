using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using School.API.Tools;
using School.Contracts.Interfaces;
using School.Service.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Access configuration data (IConfiguration is injected automatically)
var configuration = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(configuration);

// Dependency Injection for services
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ApiKeyAuthFilter>();

builder.Services.AddHttpContextAccessor();
// Configure in-memory database
builder.Services.AddDbContext<SchoolContext>(options =>
        options.UseInMemoryDatabase("SchoolDb"));

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

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

if (app.Environment.IsProduction() || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management API V1");
});

app.Run();