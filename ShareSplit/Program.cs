using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShareSplit;
using ShareSplit.Shared.Interfaces;
using ShareSplit.Shared.Model;

var hostBuilder = Host.CreateApplicationBuilder();

hostBuilder.Services.Configure<ShareSplitOptions>(hostBuilder.Configuration.GetSection(ShareSplitOptions.DefaultConfigSectionName));
hostBuilder.Services.AddSingleton<IShareSplitService,ShareSplitService>();
hostBuilder.Services.AddHostedService<MainService>();

var host = hostBuilder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Starting host.");

host.Run();

logger.LogInformation("Host stopped.");
