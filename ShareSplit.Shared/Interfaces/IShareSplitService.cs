using ShareSplit.Shared.Model;

namespace ShareSplit.Shared.Interfaces;

public interface IShareSplitService
{
    IEnumerable<Ticket> SplitShares(IEnumerable<CumulatedVotingInstruction> votingInstructions);
}