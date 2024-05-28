using DataAggregator.Application.Services;
using DataAggregator.Application.Services.Contracts;
using DataAggregator.Domain.Entities.SpecificCustomers;
using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.Repositories;
using DataAggregator.Infrastructure.Repositories.Base.Contracts;
using DataAggregator.Infrastructure.RepositoryFactories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TestContext>(
    options => options.UseSqlServer(
        dbConnectionString,
        options =>
        {
            options.EnableRetryOnFailure();
        }));

builder.Services.AddScoped<ICustomerRepository<Customer2, int>, CustomerRepository_2>();
builder.Services.AddScoped<ICustomerRepository<Customer101, int>, CustomerRepository_101>();
builder.Services.AddScoped<ICustomerRepository<Customer145, string>, CustomerRepository_145>();
builder.Services.AddScoped<IEventRepository<int>, EventRepository_2>();
builder.Services.AddScoped<IEventRepository<int>, EventRepository_101>();
builder.Services.AddScoped<IEventRepository<string>, EventRepository_145>();
builder.Services.AddScoped<INotificationBroker, NotificationBroker>();

builder.Services.AddScoped<CustomerRepositoryFactory>();
builder.Services.AddScoped<EventRepositoryFactory>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllers();
AddSerilog(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void AddSerilog(WebApplicationBuilder builder)
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    {
        var configuration = hostingContext.Configuration;
        loggerConfiguration.WriteTo.Console();

        //var appInsightsConnectionString = configuration["ApplicationInsights:ConnectionString"];
        //if (!string.IsNullOrWhiteSpace(appInsightsConnectionString))
        //{
        //    loggerConfiguration.WriteTo.ApplicationInsights(appInsightsConnectionString, TelemetryConverter.Traces);
        //}
    });
}