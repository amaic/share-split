using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ShareSplit;

public class MainService(
    ILogger<MainService> logger,
    IHostApplicationLifetime applicationLifetime
    ) : IHostedService
{

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting main service.");

        var votiningInstructions = new VotingInstruction[]
        {
            new (3,0,0),
            new (0,3,0),
            new (0,0,3)
        };

        var singleVotes = new HashSet<int>();

        int? total = null;
        foreach (var votingInstruction in votiningInstructions)
        {
            if (total == null)
            {
                total = votingInstruction.Total;
            }
            else
            {
                if (total != votingInstruction.Total) throw new Exception("The sum of votes must be equal over all voting instructions.");
            }
        }

        var shareSplitTree = ShareSplitNode.CreateShareSplitTree(total!.Value);
        var x = shareSplitTree.DemandVotes(1,0,2);


        applicationLifetime.StopApplication();

        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}