using System.Text.Json;
using Enigma.domain.extensions;
using Enigma.domain.models;

namespace Enigma.domain
{
    public class Encryptor
    {
        private List<Rotor> rotors;
        private List<Rotor> reverseRotors;
        private Reflector reflector;
        private Dictionary<Char, Char> plugboard;
        
        // Gear1 is right rotor which rotates first
        public Encryptor(RotorSettings Gear1, RotorSettings Gear2, RotorSettings Gear3, Reflectors reflector, Dictionary<Char, Char> plugboard)
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
            // needs validation map all values to the reverse as well
            this.plugboard = plugboard;
            foreach( char c in this.plugboard.Keys.ToList())
            {
                this.plugboard.Add(this.plugboard[c], c);
            }
        }

        public string Encrypt(string text) 
        {
            var charArray = text.ToCharArray();
            // deal with rotor and reflectors pathway
            charArray = charArray.Select(c => {
                // TODO: plugboard
                //rotor rotors
                bool shouldMoveNextRotor = true;
                foreach( Rotor r in rotors)
                {
                    if (shouldMoveNextRotor) 
                        shouldMoveNextRotor = r.MoveRotorAndShouldMoveNext();
                }
                // input to plugboard
                if (plugboard.ContainsKey(c)) c = plugboard[c];
                // pass through rotors
                foreach (Rotor r in this.rotors)
                {
                    c = r.TraversePinContact(c)
                        .Pipe(i => r.TraverseWiring(i))
                        .Pipe(i => r.TraversePlateContact(i));
                }
                // refect back to rotors
                c = this.reflector.Get(c);
                // pass through the rotors in reverse
                foreach (Rotor r in this.reverseRotors)
                {
                    c = r.ReversePlateContact(c)
                        .Pipe(i => r.ReverseWiring(i))
                        .Pipe(i => r.ReversePinContact(i));
                }
                // output to plugboard
                if (plugboard.ContainsKey(c)) c = plugboard[c];

                return c;
            }).ToArray(); 
            return new String(charArray);
        }
    }
}
