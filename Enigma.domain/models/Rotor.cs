using Enigma.domain.extensions;

namespace Enigma.domain.models
{
    public class Rotor
    {
        public Dictionary<char, char> Map { get; set; }
        public List<char> Notch { get; set; }
        public List<char> Turnover { get; set; }
        public char CurrentSetting { get; set; }
        public char RingSetting { get; set; }

        public int TraversePinContact(char fromPlugboard)
        {
            return ClampToAlphabet(this.CurrentSetting.offset() + fromPlugboard.offset());
        }

        public int TraverseWiring(int atPinContact)
        {
            int fromPinContact = ClampToAlphabet(atPinContact - this.RingSetting.offset());
            int toPlateContact = ClampToAlphabet(this.Map.ElementAt(fromPinContact).Value.offset());
            int atPlateContact = ClampToAlphabet(toPlateContact + this.RingSetting.offset());
            return atPlateContact;
        }

        public char TraversePlateContact(int atPlateContact)
        {
            return ClampToAlphabet(atPlateContact - this.CurrentSetting.offset())
                .GetLetterFromOffset();
        }
        
        public int ReversePlateContact(char fromReflector)
        {
            return ClampToAlphabet(fromReflector.offset() + this.CurrentSetting.offset());
        }
        
        public int ReverseWiring(int atPlateContact)
        {
            char fromPlateContact = ClampToAlphabet(atPlateContact - this.RingSetting.offset())
                .GetLetterFromOffset();
            int toPinContact = ClampToAlphabet(this.Map.First(x => x.Value == fromPlateContact).Key.offset());
            int atPinContact = ClampToAlphabet(toPinContact + this.RingSetting.offset());
            return atPinContact;
        }
        
        public char ReversePinContact(int atPinContact)
        {
            return ClampToAlphabet(atPinContact - this.CurrentSetting.offset())
                .GetLetterFromOffset();
        }

        [Obsolete]
        public char GetNext(char input, int rotationalOffest) 
        {
            int value = input.offset() + CurrentSetting.offset() - rotationalOffest - this.RingSetting.offset();
            value = value.Normalize();
            return (char)(this.Map.ElementAt(value).Value.offset() + this.RingSetting.offset() %26 + 65);
        }

        [Obsolete]
        public char GetReverse(char input, int rotationalOffset){
            int value = Map.First(x => x.Value == (input.offset() + rotationalOffset - this.RingSetting.offset()) % 26 + 65).Key.offset() - CurrentSetting.offset() + this.RingSetting.offset();
            value = value.Normalize();
            return this.Map.ElementAt(value).Key;
        } 

        public bool MoveRotorAndShouldMoveNext()
        {
            bool shouldMoveNextRoror = Turnover.Any(x => x == CurrentSetting);
            CurrentSetting = CurrentSetting.GetNextCharacter();
            return shouldMoveNextRoror;
        }

        private int ClampToAlphabet(int input)
        {
            return input.Normalize();
        }
        
    }
}