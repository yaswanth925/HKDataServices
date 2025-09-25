using FluentValidation;
using FluentValidation.AspNetCore;
using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Repository;
using HKDataServices.Service;
using HKDataServices.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


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

// ---------------- FluentValidation ----------------
// Registers all validators in the assembly (e.g., UsersFormDtoValidator)
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// This will scan assembly for validators like UsersFormDtoValidator
builder.Services.AddValidatorsFromAssemblyContaining<UsersFormDtoValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<UpdateTrackingStatusFormDtoValidator>();

// Example: If you want custom messages globally
ValidatorOptions.Global.LanguageManager.Enabled = true;
ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en");

// ---------------- Swagger ----------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HKDataServices API", Version = "v1" });
});

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
								   
									 
    // c.RoutePrefix = string.Empty;
});

app.MapControllers();
app.Run();
