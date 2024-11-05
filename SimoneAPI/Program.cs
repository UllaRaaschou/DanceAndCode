using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.EndpointExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SimoneDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("TokenAuthNZ", new()
    {
        Name = "Authorization",
        Description = "Token basen authentication and authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme(){
                Reference = new OpenApiReference()
                {
                    Type  =ReferenceType.SecurityScheme,
                    Id = "TokenAuthNZ"
                } },
            new List<string>()
        }
    });
});
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("dateOnly", typeof(DateOnlyRouteConstraint));
});

builder.Services.AddProblemDetails();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();


builder.Services.AddSingleton<CalendarDataModel>();


//Har profiler som parametre, som scannes for mapping-konfigurationer
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

//TODO: Register endpoints
// NAMING IS IMPORTENT !!
app.RegisterDancersEndpoints();
app.RegisterTeamsEndpoints();
app.RegisterAttendanceEndpoints();
app.RegisterStaffEndpoints();


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

if (!app.Environment.IsDevelopment()) 
{
    app.UseAuthentication();
    app.UseAuthorization();
}

// TODO remove!!!!!
//app.RegisterDancersEndpoints();
//app.RegisterTeamsEndpoints();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});



app.Run();


