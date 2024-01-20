using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var hostBuilder = Host.CreateApplicationBuilder();

var host = hostBuilder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Starting host.");

host.Run();

logger.LogInformation("Host stopped.");
