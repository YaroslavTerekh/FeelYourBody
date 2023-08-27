using FluentValidation;
using FYB.API.Middleware;
using FYB.BL.Behaviors;
using FYB.Data.DbConnection;
using FYB.BL.Services;
using FYB.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FYB.Data.Common;
using FYB.Data.Constants;
using FYB.BL.Settings.Abstractions;
using FYB.BL.Settings.Realizations;
using Hangfire;
using Newtonsoft.Json;
using FYB.BL.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDataContext(builder.Configuration);

builder.Services.AddIdentity<User, ApplicationRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();


builder.Services.AddValidatorsFromAssembly(AppDomain.CurrentDomain.GetAssemblies().Where(t => t.FullName.Contains("BL")).First());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

var liqPaySettings = new LiqPaySettings(builder.Configuration);
var twillioSettings = new TwilioSettings(builder.Configuration);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient<ITwilioSettings, TwilioSettings>(_ => twillioSettings);
builder.Services.AddTransient<ILiqPaySettings, LiqPaySettings>(_ => liqPaySettings);
builder.Services.AddCustomServices();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHangfire((sp, config) =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
    config.UseSerializerSettings(new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
}
);
builder.Services.AddHangfireServer();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.Admin, policy =>
    {
        policy.RequireRole(Role.Admin.ToString());
    });

    options.AddPolicy(Policies.Users, policy =>
    {
        policy.RequireRole(Role.SimpleUser.ToString());
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FeelYourBody",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Replace with your React app's URL
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
    });
});

var app = builder.Build();
var scope = app.Services.CreateScope();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var path = Path.Combine(app.Services.GetRequiredService<IWebHostEnvironment>().ContentRootPath, "uploads", "videos");
Directory.CreateDirectory(path);

var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
dataContext.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomExceptionHandler();

app.UseCors("CORS");

app.MapControllers();

app.UseHangfireDashboard();

IHangfireJobsService hangfireJobgsService = scope.ServiceProvider.GetRequiredService<IHangfireJobsService>();
hangfireJobgsService.CreateFileDeletingJob();
hangfireJobgsService.CreateInvisibleFilesDeletingJob();

app.Run();


// Feel your body - Yaroslav Terekh 
// Gmail: yarolslavterekh@gmail.com
// LinkedIn: https://www.linkedin.com/in/yaroslav-terekh-476826266/
// Github: https://github.com/YaroslavTerekh