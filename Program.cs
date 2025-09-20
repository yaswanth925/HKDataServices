using HKDataServices.Controllers.API;
using HKDataServices.Repository;
using HKDataServices.Service;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

// ---- EF Core ----
builder.Services.AddDbContextPool<ApplicationDbContext>(opts =>
    opts.UseSqlServer(connectionString, sql =>
    {
        sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        sql.CommandTimeout(60);
    }));

// ---- Repos/Services ----
builder.Services.AddScoped<IUpdateTrackingStatusRepository, UpdateTrackingStatusRepository>();
builder.Services.AddScoped<IUpdateTrackingStatusService, UpdateTrackingStatusService>();

// ---- Controllers ----
builder.Services.AddControllers();

// ---- Swagger (required to fix your error) ----
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HKDataServices API", Version = "v1" });
});

var app = builder.Build();

// ---- Middleware ----
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HKDataServices API v1");
    // keep default: UI at /swagger
    // if you prefer root, uncomment:
    // c.RoutePrefix = string.Empty;
});

app.MapControllers();
app.Run();