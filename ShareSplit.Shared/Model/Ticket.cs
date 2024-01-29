namespace ShareSplit.Shared.Model;

public class Ticket(int ticketNumber, int shares)
{
    public int TicketNumber { get; private set; } = ticketNumber;

    public int Shares { get; private set; } = shares;

    public VotingInstruction[] VotingInstructions { get; set; } = [];
}