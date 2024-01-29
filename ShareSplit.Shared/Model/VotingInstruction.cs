using ShareSplit.Shared.Types;

namespace ShareSplit.Shared.Model;

public class VotingInstruction(int agendaItemNumber, VotingOption votingOption)
{
    public int AgendaItemNumber { get; private set; } = agendaItemNumber;

    public VotingOption VotingOption { get; private set; } = votingOption;
}