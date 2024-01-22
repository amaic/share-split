using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;

namespace ShareSplit;

public class ShareSplitNode
{
    public static ShareSplitNode CreateShareSplitTree(int totalVotes)
    {
        if (totalVotes < 1) throw new ArgumentException("Total votes must be at least 1.", nameof(totalVotes));

        return new ShareSplitNode(null, totalVotes);
    }

    public static readonly ShareSplitNode Empty = new(null, 0);

    private ShareSplitNode(ShareSplitNode? parent, int votes)
    {
        Parent = parent;
        Votes = votes;
    }

    public ShareSplitNode? Parent { get; init; }
    public int Votes { get; init; }

    public ShareSplitNode? SplitNodeA { get; private set; }
    public ShareSplitNode? SplitNodeB { get; private set; }


    public bool IsRoot => Parent == null && Votes > 0;
    public bool IsLeaf => SplitNodeA == null && Votes > 0;
    public bool IsEmpty => Votes == 0;

    public IEnumerable<ShareSplitNode> DemandVotes(params int[] demandedVotesList)
    {
        if (demandedVotesList.Sum() > Votes) throw new ArgumentException("Sum of demanded votes list exceeds available votes.", nameof(demandedVotesList));
        if (demandedVotesList.Where(dv => dv < 0).Any()) throw new ArgumentException("All demanded votes must be at least 0.", nameof(demandedVotesList));

        var assignedVotesList = new List<ShareSplitNode>();
        foreach (var demandedVotes in demandedVotesList)
        {
            if (demandedVotes == 0)
            {
                assignedVotesList.Add(Empty);
            }
            else
            {
                var assignedVotes = GetVotes(demandedVotes, assignedVotesList) ?? throw new Exception("Demanded votes not available.");
                assignedVotesList.Add(assignedVotes);
            }
        }

        return assignedVotesList.ToArray();
    }

    private ShareSplitNode? GetVotes(int demandedVotes, IEnumerable<ShareSplitNode> blockedVotes)
    {
        if (demandedVotes > Votes)
        {
            return null;
        }
        else if (demandedVotes == Votes)
        {
            return blockedVotes.Contains(this) ? null : this;
        }
        else
        {
            if (IsLeaf)
            {
                SplitNodeA = new ShareSplitNode(this, demandedVotes);
                SplitNodeB = new ShareSplitNode(this, Votes - demandedVotes);
                return SplitNodeA;
            }
            else
            {
                ShareSplitNode poolA;
                ShareSplitNode poolB;
                if (SplitNodeA!.Votes > SplitNodeB!.Votes)
                {
                    poolA = SplitNodeB;
                    poolB = SplitNodeA;
                }
                else
                {
                    poolA = SplitNodeA;
                    poolB = SplitNodeB;
                }

                var assignedVotes = poolA.GetVotes(demandedVotes, blockedVotes);
                assignedVotes ??= poolB.GetVotes(demandedVotes, blockedVotes);
                return assignedVotes;
            }
        }
    }
}