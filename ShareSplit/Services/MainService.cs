using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShareSplit.Shared.Interfaces;
using ShareSplit.Shared.Model;

namespace ShareSplit;

public class MainService(
    ILogger<MainService> logger,
    IHostApplicationLifetime applicationLifetime,
    IShareSplitService shareSplitService
    ) : IHostedService
{

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting main service.");


        var votingInstructions = new CumulatedVotingInstruction[]
        {
             new(1,100,0,0),
             new(2,100,0,0),
             new(3,33,33,34),
             new(4,37,63,0),
             new(5,7,5,88),
        };

        var tickets = shareSplitService.SplitShares(votingInstructions);

        applicationLifetime.StopApplication();

        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}