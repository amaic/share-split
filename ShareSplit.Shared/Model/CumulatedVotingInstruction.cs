namespace ShareSplit.Shared.Model;

public class CumulatedVotingInstruction(int agendaItemNumber, int approval, int rejection, int abstention)
{
    public int AgendaItemNumber { get; private set; } = agendaItemNumber;
    
    public int Approval { get; private set; } = approval;
    public int Rejection { get; private set; } = rejection;
    public int Abstention { get; private set; } = abstention;

    public int Total => Approval + Rejection + Abstention;
}