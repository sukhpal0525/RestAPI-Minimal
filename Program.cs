using Microsoft.EntityFrameworkCore;
using RestAPI_Minimal;
using RestAPI_Minimal.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add SwaggerUI and SwaggerGen functionality
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Create DB context
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")
    ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();


// Endpoints


app.MapGet("/Customers", () =>
{
    return new List<Customer>
    {
        new Customer
        {
            CustomerNumber = "CUST1",
            FirstName = "Sukhpal",
            LastName = "Shergill"
        }
    };

})
.WithName("Customers");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
