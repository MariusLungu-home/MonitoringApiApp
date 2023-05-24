using MonitoringApi.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddCheck<RandomHealthCheck>("Site health check")
    .AddCheck<RandomHealthCheck>("Database health check");

builder.Services.AddHealthChecksUI(opt =>
    {
        opt.AddHealthCheckEndpoint("api", "/health");
        opt.SetEvaluationTimeInSeconds(5); // in Production should be every 1 minute or so
        opt.SetMinimumSecondsBetweenFailureNotifications(10);
    }).AddInMemoryStorage();

builder.Services.AddWatchDogServices();

var app = builder.Build();

app.UseWatchDogExceptionLogger();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions
{ 
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();
app.UseWatchDog(opt =>
{
    // read value from appsettings.json
    opt.WatchPageUsername = app.Configuration.GetValue<string>("WatchDog:UserName");
    opt.WatchPagePassword = app.Configuration.GetValue<string>("WatchDog:Password");
});

app.Run();