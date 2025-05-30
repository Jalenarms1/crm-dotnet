using System.Text.Json;
using CRMApi.Data;
using CRMApi.Middleware;
using CRMApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

DotNetEnv.Env.Load();

var connectionString = Environment.GetEnvironmentVariable("DB_CONN");
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(connectionString,
        sqlopts => sqlopts.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null)
    )
);

builder.Services.AddCors(options =>
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy
            .WithOrigins("http://localhost:5268")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    })
);

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PatientService>();



builder.Services.AddControllers()
    .AddJsonOptions(options => 
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    );



var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.OpenConnectionAsync();
            await dbContext.Database.CloseConnectionAsync();
        });
    }

}
catch
{
    Console.WriteLine("Initiating db startup");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseMiddleware<LoggerMiddleware>();
app.UseMiddleware<UserMiddleware>();
app.UseStaticFiles();

app.UseRouting();



app.UseHttpsRedirection();

app.MapControllers();

app.MapFallbackToFile("index.html");

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
