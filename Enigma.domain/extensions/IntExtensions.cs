namespace Enigma.domain.extensions;

public static class IntExtensions
{
    public static int Normalize(this int value)
    {
        if (value < 0) return value + 26;
        if (value >= 26) return value - 26;
        return value;
    }

    public static char GetLetterFromOffset(this int offset) =>
        (char) (offset + 65);
}