using Payment.Bank.Api.Extensions;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel
            .Override("Microsoft", LogEventLevel.Information)
            .WriteTo.Console()
            .CreateBootstrapLogger();

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var applicationName = typeof(Program).Assembly.GetName().Name;

try
{
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        Args = args,
        ApplicationName = applicationName,
        EnvironmentName = environmentName,
        ContentRootPath = Directory.GetCurrentDirectory()
    });

    builder.AddConfigurations();
    builder.AddCoreOptions();
    builder.AddCoreServices();
    builder.AddConfiguredSerilog();

    builder.Host.UseDefaultServiceProvider((context, options) =>
    {
        options.ValidateOnBuild = true;
        options.ValidateScopes = context.HostingEnvironment.IsDevelopment() ||
                                 context.HostingEnvironment.IsStaging() ||
                                 context.HostingEnvironment.IsEnvironment("Test") ||
                                 context.HostingEnvironment.IsEnvironment("Docker");
    });

    var app = builder.Build();
    app.Lifetime.ApplicationStarted.Register(() => Log.Information($"The application {applicationName} is started"));
    app.Lifetime.ApplicationStopping.Register(() => Log.Information($"The application {applicationName} is shutting down..."));
    app.Lifetime.ApplicationStopped.Register(() => Log.Information($"The application {applicationName} is stopped"));

    app.UseSerilogRequestLogging();
    app.UseLoggingMiddleware();
    app.UseSwaggerEndpoints();
    app.UseCorsPolicy();
    app.MapControllers();
    app.UseHttpsRedirection();

    await app.RunAsync();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, $"The Application {applicationName} terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync().ConfigureAwait(false);
}
