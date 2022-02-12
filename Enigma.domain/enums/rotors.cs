using System;
using System.Collections.Generic;
using System.Reflection;

namespace Enigma.domain
{
    public enum Rotors
    {
        I,
        II,
        III,
        IV,
        V,
        VI,
        VII,
        VIII
    }

    public static class RortorsExt
    {
        private static Assembly thisAssembly = typeof(Rotors).GetTypeInfo().Assembly;
        private static Dictionary<Rotors, Func<Stream>> streams = new  Dictionary<Rotors, Func<Stream>>{
            { Rotors.I, () =>  thisAssembly.GetManifestResourceStream("Enigma.domain.rotors.rotor1.json")},
            { Rotors.II, () =>  thisAssembly.GetManifestResourceStream("Enigma.domain.rotors.rotor2.json")},
            { Rotors.III, () =>  thisAssembly.GetManifestResourceStream("Enigma.domain.rotors.rotor3.json")},
            { Rotors.IV, () =>  thisAssembly.GetManifestResourceStream("Enigma.domain.rotors.rotor4.json")},
            { Rotors.V, () =>  thisAssembly.GetManifestResourceStream("Enigma.domain.rotors.rotor5.json")},
            { Rotors.VI, () =>  thisAssembly.GetManifestResourceStream("Enigma.domain.rotors.rotor6.json")},
            { Rotors.VII, () =>  thisAssembly.GetManifestResourceStream("Enigma.domain.rotors.rotor7.json")},
            { Rotors.VIII, () =>  thisAssembly.GetManifestResourceStream("Enigma.domain.rotors.rotor8.json")}
        };

        public static Stream GetStream(this Rotors rotor)
        {
            return streams[rotor]();
        }
    }
}