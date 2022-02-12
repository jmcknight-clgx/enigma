using System;
using System.Reflection;
using System.Text.Json;
using Enigma.domain.dtos;

namespace Enigma.domain
{
    public class Encryptor
    {
        private List<Rotor> rotors;
        private List<Rotor> reverseRotors;
        private Reflector relfector;
        
        public Encryptor(RotorSettings Gear1, RotorSettings Gear2, RotorSettings Gear3, Reflectors reflector)
        {
            this.rotors = new List<Rotor>{
                Gear1.SetupRotor(),
                Gear2.SetupRotor(),
                Gear3.SetupRotor()
            };
            using Stream stream = reflector.GetStream();
            this.relfector = JsonSerializer.Deserialize<Reflector>(stream);
            this.reverseRotors = new List<Rotor> {
                rotors[2],
                rotors[1],
                rotors[0]
            };
        }

        public string Encrypt(string text) 
        {
            var charArray = text.ToCharArray();
            // TODO: plugboard

            // deal with rotor and reflectors pathway
            // TODO: rotation of rotors
            charArray = charArray.Select(c => {
                foreach(Rotor r in this.rotors) c = r.GetNext(c);
                c = this.relfector.Get(c);
                foreach(Rotor r in this.reverseRotors) c = r.GetReverse(c);
                return c;
            }).ToArray();
            return new String(charArray);
        }
    }
}
