namespace DHondt;

public class DHondtCalculator
{
    public DHondtCalculator(IReadOnlyCollection<PoliticalParty> politicalParties, string city, int deputyLimit)
    {
        PoliticalParties = politicalParties;
        City = city;
        DeputyLimit = deputyLimit;
        Result = new List<ElectionResult>();
        DeputyByParties = new List<string>();
    }
    
    public IReadOnlyCollection<PoliticalParty> PoliticalParties { get; }
    public string City { get; }
    public int DeputyLimit { get; }
    private IList<ElectionResult> Result { get; }
    private IList<string> DeputyByParties { get; }

    public IReadOnlyCollection<ElectionResult> GetResults()
    {
        Calculate();
        return Result.ToList();
    }

    private void Calculate()
    {
        if (DeputyLimit <= 0)
            throw new DeputyCountNotSetException();

        var currentDeputy = 1;
        while (true)
        {
            if(currentDeputy> DeputyLimit)
                break;

            var firstParty = GetFirst();
            GiveDeputy(firstParty.Name);
            foreach (var politicalParty in PoliticalParties)
            {
                Console.WriteLine($"{politicalParty.Name}: {ToOwnDeputyCount(politicalParty.Name)}");
            }
            currentDeputy++;
            Console.WriteLine("==============>");
        }
    }

    private void GiveDeputy(string partyName)
    {
        DeputyByParties.Add(partyName);
    }

    private PoliticalParty GetFirst() => PoliticalParties
        .OrderByDescending(x => x.ToVoteCount(ToOwnDeputyCount(x.Name)))
        .First();

    private int ToOwnDeputyCount(string partyName) =>
        DeputyByParties
            .Count(x => x == partyName);
}

public class ElectionResult
{
    public string PartyName { get; }
    public int DeputyCount { get; }
}

public class PoliticalParty
{
    public PoliticalParty(string name, int voteCount)
    {
        Name = name;
        VoteCount = voteCount;
    }
    public string Name { get; }
    public int VoteCount { get; }

    public int ToVoteCount(int ownDeputyCount) => VoteCount / (ownDeputyCount + 1);
}