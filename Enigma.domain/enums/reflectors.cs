using System.Reflection;

namespace Enigma.domain
{
    public enum Reflectors
    {
       B,
       C
    }

    public static class ReflectorsExt
    {
        private static Assembly thisAssembly = typeof(Reflectors).GetTypeInfo().Assembly;
        private static Dictionary<Reflectors, Func<Stream>> streams = new  Dictionary<Reflectors, Func<Stream>>{
            { Reflectors.B, () =>  thisAssembly.GetManifestResourceStream("Enigma.domain.reflectors.ukw-b.json")},
            { Reflectors.C, () =>  thisAssembly.GetManifestResourceStream("Enigma.domain.reflectors.ukw-c.json")}
        };

        public static Stream GetStream(this Reflectors reflector)
        {
            return streams[reflector]();
        }
    }
}