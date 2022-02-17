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
        //TODO: RingSettings
        //public char GetNext(char input) => getOffsetCharacter(Map[input]);
        public char GetNext(char input, int rotationalOffest) 
        {
            int value = input.offset() + CurrentSetting.offset() - rotationalOffest;
            value = value.Normalize();
            return this.Map.ElementAt(value).Value;
        }
        //public char GetReverse(char input) => Map.First(x => x.Value == getNegativeOffsetCharacter(input)).Key;
        public char GetReverse(char input, int rotationalOffset){
            int value = Map.First(x => x.Value == (input.offset() + rotationalOffset) % 26 + 65).Key.offset() - CurrentSetting.offset();
            value = value.Normalize();
            return this.Map.ElementAt(value).Key;
        } 

        public bool MoveRotorAndShouldMoveNext()
        {
            bool shouldMoveNextRoror = Turnover.Any(x => x == CurrentSetting);
            CurrentSetting = CurrentSetting.GetNextCharacter();
            return shouldMoveNextRoror;
        }

        private char getOffsetCharacter(char c) => c.GetCharacterWithOffset(this.CurrentSetting.offset());
        private char getNegativeOffsetCharacter(char c) => c.GetCharacterWithOffset(-this.CurrentSetting.offset());

        
    }
}