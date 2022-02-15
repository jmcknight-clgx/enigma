using System;
using System.Reflection;
using System.Text.Json;
using Enigma.domain.models;

namespace Enigma.domain
{
    public class Encryptor
    {
        private List<Rotor> rotors;
        private List<Rotor> reverseRotors;
        private Reflector reflector;
        
        // Gear1 is right rotor which rotates first
        public Encryptor(RotorSettings Gear1, RotorSettings Gear2, RotorSettings Gear3, Reflectors reflector)
        {
            this.rotors = new List<Rotor>{
                Gear1.SetupRotor(),
                Gear2.SetupRotor(),
                Gear3.SetupRotor()
            };
            using Stream stream = reflector.GetStream();
            this.reflector = JsonSerializer.Deserialize<Reflector>(stream);
            this.reverseRotors = new List<Rotor> {
                rotors[2],
                rotors[1],
                rotors[0]
            };
        }

        public string Encrypt(string text) 
        {
            var charArray = text.ToCharArray();
            // deal with rotor and reflectors pathway
            charArray = charArray.Select(c => {
                // TODO: plugboard
                //rotor rotors
                bool shouldMoveNextRotor = true;
                int previousPartialRotations = 0;
                foreach( Rotor r in rotors)
                {
                    if (shouldMoveNextRotor) 
                        shouldMoveNextRotor = r.MoveRotorAndShouldMoveNext();
                }
                // pass through rotors
                foreach (Rotor r in this.rotors)
                {
                    c = r.GetNext(c, previousPartialRotations);
                    previousPartialRotations = r.PartialRotations;
                }
                // refect back to rotors
                c = this.reflector.Get(c);
                // pass through the rotors in reverse
                foreach (Rotor r in this.reverseRotors)
                {
                    previousPartialRotations = r.PartialRotations;
                    c = r.GetReverse(c, previousPartialRotations);
                }


                return c;
            }).ToArray();
            return new String(charArray);
        }
    }
}
