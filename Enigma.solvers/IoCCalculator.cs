namespace Enigma.solvers;
public static class IoCCalculator
{
    private static Dictionary<char, int> GetInitialDictionary() 
    { 
        var d = new Dictionary<char, int>();
        for (int i = 65; i < 91; i++)
        {
            d.Add((char) i, 0);
        }
        return d;
    }

    public static double CalculateIoC(string s)
    {
        s = s.ToUpper();
        var chars = s.ToCharArray();
        var counters = GetInitialDictionary();
        foreach (char c in counters.Keys) 
        {
            counters[c] = chars.Where(d => c == d).Count();
        } 
        var numerator = counters.Select(c => c.Value * (c.Value - 1)).Sum();
        var denominator = chars.Count() * (chars.Count() - 1);
        var result = Math.Round(((double)numerator) / denominator, 5);
        return result;
    }
}
