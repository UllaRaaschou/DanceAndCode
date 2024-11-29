using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SimoneAPI.Authorization;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.EndpointExtensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);






// Tilføj logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();



// Add services to the container.
builder.Services.AddDbContext<SimoneDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Basic authentication and authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new List<string>()
        }
    });
});
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("dateOnly", typeof(DateOnlyRouteConstraint));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*");
    });
});


builder.Services.AddProblemDetails();
builder.Services.AddAuthentication("BasicAuthorization")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthorization", null);//.AddJwtBearer();
//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
//    .RequireAuthenticatedUser()
//    .Build();
//});
builder.Services.AddAuthorization();


builder.Services.AddSingleton<CalendarDataModel>(); // Registrering i Dependency Injection - Containeren


//Har profiler som parametre, som scannes for mapping-konfigurationer
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Build the application
var app = builder.Build();



app.UseCors();


//Register endpoints
app.UseAuthentication();
app.UseAuthorization();
app.RegisterDancersEndpoints();
app.RegisterTeamsEndpoints();
app.RegisterAttendanceEndpoints();
app.RegisterStaffEndpoints();
app.RegisterRelationEndpoints();
app.RegisterWorkingHoursEndpoints();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SimoneDbContext>();
    //context.Database.EnsureCreated();
    context.Database.Migrate();
}


// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment()) 
    {
    app.UseExceptionHandler();
    }




app.UseHttpsRedirection();

//if (!app.Environment.IsDevelopment()) 
//{
//    app.UseAuthentication();
//    app.UseAuthorization();
//}

// TODO remove!!!!!
//app.RegisterDancersEndpoints();
//app.RegisterTeamsEndpoints();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});



app.Run();


