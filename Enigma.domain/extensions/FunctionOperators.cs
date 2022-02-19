namespace Enigma.domain.extensions;

public static class FunctionOperators
{
    public static B Pipe<A,B>(this A obj, Func<A,B> func) => func(obj);
}