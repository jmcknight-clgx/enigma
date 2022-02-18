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
        public char RingSetting { get; set; }

        public char GetNext(char input, int rotationalOffest) 
        {
            int value = input.offset() + CurrentSetting.offset() - rotationalOffest - this.RingSetting.offset();
            value = value.Normalize();
            return (char)(this.Map.ElementAt(value).Value.offset() + this.RingSetting.offset() %26 + 65);
        }

        public char GetReverse(char input, int rotationalOffset){
            int value = Map.First(x => x.Value == (input.offset() + rotationalOffset - this.RingSetting.offset()) % 26 + 65).Key.offset() - CurrentSetting.offset() + this.RingSetting.offset();
            value = value.Normalize();
            return this.Map.ElementAt(value).Key;
            //return (char)(this.Map.ElementAt(value).Key.offset() + this.RingSetting.offset() % 26 + 65);
        } 

        public bool MoveRotorAndShouldMoveNext()
        {
            bool shouldMoveNextRoror = Turnover.Any(x => x == CurrentSetting);
            CurrentSetting = CurrentSetting.GetNextCharacter();
            return shouldMoveNextRoror;
        }
        
    }
}