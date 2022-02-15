using System;
using System.Collections.Generic;
using Enigma.domain.extensions;

namespace Enigma.domain.models
{
    public class Rotor
    {
        public Dictionary<char, char> Map { get; set; }
        public List<char> Notch { get; set; }
        public List<char> Turnover { get; set; }
        public char CurrentSetting { get; set; }
        public int PartialRotations { get; set; }
        //TODO: RingSettings
        //public char GetNext(char input) => getOffsetCharacter(Map[input]);
        public Rotor()
        {
            PartialRotations = 0;
        }
        public char GetNext(char input, int partialRotations) 
        {
            int value = input.offset() + CurrentSetting.offset() - partialRotations;
            value = value.Normalize();
            return this.Map.ElementAt(value).Value;
        }
        //public char GetReverse(char input) => Map.First(x => x.Value == getNegativeOffsetCharacter(input)).Key;
        public char GetReverse(char input, int partialRotations){
            int value = Map.First(x => x.Value == (input.offset() + partialRotations) % 26 + 65).Key.offset() - CurrentSetting.offset();
            value = value.Normalize();
            return this.Map.ElementAt(value).Key;
        } 

        public bool MoveRotorAndShouldMoveNext()
        {
            bool shouldMoveNextRoror = Turnover.Any(x => x == CurrentSetting);
            CurrentSetting = CurrentSetting.GetNextCharacter();
            PartialRotations = (PartialRotations + 1) % 26;
            return shouldMoveNextRoror;
        }

        private char getOffsetCharacter(char c) => c.GetCharacterWithOffset(this.CurrentSetting.offset());
        private char getNegativeOffsetCharacter(char c) => c.GetCharacterWithOffset(-this.CurrentSetting.offset());

        
    }
}