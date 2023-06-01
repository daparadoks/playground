namespace DHondt;

public class SimulationTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Calculate()
    {
        var politicalParties = new List<PoliticalParty>
        {
            new PoliticalParty("pt1", 185000),
            new PoliticalParty("pt2", 123800),
            //new PoliticalParty("pt2a", 67000),
            //new PoliticalParty("pt2b", 41700),
            new PoliticalParty("pt3", 51000),
            new PoliticalParty("other", 11000)
        };
        var calculator = new DHondtCalculator(politicalParties, "city1", 5);
        var result = calculator.GetResults();
    }
}