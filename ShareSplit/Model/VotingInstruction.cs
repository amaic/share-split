namespace ShareSplit;

public class VotingInstruction(int yes, int no, int abstention)
{
    public int Yes { get; set; } = yes;
    public int No { get; set; } = no;
    public int Abstention { get; set; } = abstention;

    public int Total => Yes + No + Abstention;
}