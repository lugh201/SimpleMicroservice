using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;

var builder = WebApplication.CreateBuilder(args);



// Add Ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5555")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Add Ocelot services to the container
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Exception handling middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An unhandled exception has occurred.");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
    }
});

// Configure the HTTP request pipeline
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.UseCors("AllowSpecificOrigin");

// Use Ocelot middleware
try
{
    await app.UseOcelot();
}
catch (AggregateException ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    foreach (var innerEx in ex.InnerExceptions)
    {
        logger.LogError(innerEx, "An error occurred while starting Ocelot.");
    }
    throw; // Re-throw to stop the application if Ocelot fails to start
}

app.Run();
