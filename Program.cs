using FluentValidation;
using FluentValidation.AspNetCore;
using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Repository;
using HKDataServices.Service;
using HKDataServices.Settings;
using HKDataServices.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

static string BuildConnectionString(IConfiguration config)
{
    var mode = (config["Db:Mode"] ?? "Windows").Trim().ToLowerInvariant();
    var csb = new SqlConnectionStringBuilder
    {
        TrustServerCertificate = bool.TryParse(config["Db:TrustServerCertificate"], out var tsc) ? tsc : true,
        MultipleActiveResultSets = true
    };

    if (mode == "windows")
    {
        csb.DataSource = config["Db:Windows:Server"]!;
        csb.InitialCatalog = config["Db:Windows:Database"]!;
        csb.IntegratedSecurity = true;
    }
    else if (mode == "sql")
    {
        csb.DataSource = config["Db:Sql:Server"]!;
        csb.InitialCatalog = config["Db:Sql:Database"]!;
        csb.UserID = config["Db:Sql:User"]!;
        csb.Password = config["Db:Sql:Password"]!;
        csb.IntegratedSecurity = false;
    }
    else throw new InvalidOperationException("Db:Mode must be 'Windows' or 'Sql'.");

    return csb.ConnectionString;
}

var connectionString = BuildConnectionString(builder.Configuration);

// ---------------- EF Core ----------------
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sql =>
    {
        sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        sql.CommandTimeout(60);
    }));

// ---------------- Repositories / Services ----------------
builder.Services.AddScoped<IUpdateTrackingStatusRepository, UpdateTrackingStatusRepository>();
builder.Services.AddScoped<IUpdateTrackingStatusService, UpdateTrackingStatusService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
builder.Services.AddScoped<IPreSalesTargetRepository, PreSalesTargetRepository>();
builder.Services.AddScoped<IPreSalesTargetService, PreSalesTargetService>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IPreSalesActivityRepository, PreSalesActivityRepository>();
builder.Services.AddScoped<IPreSalesActivityService, PreSalesActivityService>();
// ---------------- FluentValidation ----------------
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<UsersFormDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateTrackingStatusFormDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PreSalesTargetDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CustomersDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PreSalesActivityDtoValidator>();

ValidatorOptions.Global.LanguageManager.Enabled = true;
ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en");

// Register JwtSettings (appsettings.json → JwtSettings class)
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

// ---------------- JWT Authentication ----------------
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// ---------------- Swagger ----------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HKDataServices API", Version = "v1" });

    // Add JWT Auth to Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {your token}'"
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
                }
            },
            new string[] {}
        }
    });
});

// ---------------- Custom Validation Messages ----------------
builder.Services.Configure<ValidationMessages>(
    builder.Configuration.GetSection("ValidationMessages"));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new
            {
                Field = e.Key,
                Errors = e.Value.Errors.Select(err => err.ErrorMessage).ToArray()
            });

        return new BadRequestObjectResult(new
        {
            Message = "Validation failed",
            Errors = errors
        });
    };
});

var app = builder.Build();

// ---------------- Middleware ----------------
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HKDataServices API v1");
});

// Important: Add Authentication + Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();