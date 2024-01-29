using Microsoft.Extensions.Logging;
using ShareSplit.Shared.Interfaces;

namespace ShareSplit.Shared.Model;

public class ShareSplitService : IShareSplitService
{
    public ShareSplitService(
        ILogger<ShareSplitService> logger
    )
    {
        _logger = logger;
    }
    private readonly ILogger<ShareSplitService> _logger;

    public IEnumerable<Ticket> SplitShares(IEnumerable<CumulatedVotingInstruction> votingInstructions)
    {
        var tickets = new List<Ticket>();

        int? totalShares = null;
        foreach (var votingInstruction in votingInstructions)
        {
            totalShares ??= votingInstruction.Total;

            if (totalShares != votingInstruction.Total) throw new InvalidDataException($"Total shares {totalShares} must be equal over all voting instructions. Conflict on agendaItem {votingInstruction.AgendaItemNumber}.");
        }


        if (totalShares == null) return tickets;

        var shareSplitTree = ShareSplitNode.CreateShareSplitTree(totalShares!.Value);

        foreach (var votingInstruction in votingInstructions)
        {
            var shareSplits = shareSplitTree.DemandVotes(votingInstruction.Approval, votingInstruction.Rejection, votingInstruction.Abstention);
            
        }


        return tickets.ToArray();
    }
}