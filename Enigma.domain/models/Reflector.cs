namespace Enigma.domain.models
{
    public class Reflector
    {
        public Dictionary<char, char> Map { get; set; }
        public char Get(char input) => Map[input];
    }
}