using API.Account.Login;
using API.MIddleware;
using Core;
using Data;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Logging.ClearProviders();
builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

 var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                        ?? throw new ArgumentNullException(nameof(builder.Configuration));

 builder.Services.AddDbContext<AppDbContext>(options =>
 {
     options.UseSqlite(connectionString);
 });

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

// builder.Services.AddIdentityCore<User>(opt => {
//     opt.User.RequireUniqueEmail = true;
// })
// .AddRoles<IdentityRole>()
// .AddEntityFrameworkStores<AppDbContext>()
// .AddApiEndpoints();
//builder.Services.AddData(builder.Configuration);
builder.Services.AddCore();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    // var jwtSecurityScheme = new OpenApiSecurityScheme
    // {
    //     BearerFormat = "JWT",
    //     Name = "Authorization",
    //     In = ParameterLocation.Header,
    //     Type = SecuritySchemeType.ApiKey,
    //     Scheme = JwtBearerDefaults.AuthenticationScheme,
    //     Description = "Put Bearer + your token in the box below",
    //     Reference = new OpenApiReference {
    //         Id = JwtBearerDefaults.AuthenticationScheme,
    //         Type = ReferenceType.SecurityScheme
    //     }
    // };

    // c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    // c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    //     {
    //         jwtSecurityScheme, Array.Empty<string>()
    //     }
    // });
});

builder.Services.AddCors();

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
// builder.Services.AddAuthorizationBuilder();
//builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>(ServiceLifetime.Transient);

var app = builder.Build();

// Adds /register, /login and /refresh endpoints
app.MapIdentityApi<User>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseSwaggerUI(c => {
    //     c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
    // }); 
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors(opt => {
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:7170");
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await context.Database.EnsureCreatedAsync();
    await DbInitializer.Initialize(context, userManager);
}
catch(Exception ex)
{
    logger.LogError(ex, "A problem occurred during migration");
}

app.Run();
