using System;
using System.Collections.Generic;

namespace Enigma.domain.dtos
{
    public class Rotor
    {
        public Dictionary<char, char> Map { get; set; }
        public List<char> Notch { get; set; }
        public List<char> Turnover { get; set; }
        public char CurrentSetting { get; set; }

        public char GetNext(char input) => Map[input];
        public char GetReverse(char input) => Map.First(x => x.Value == input).Key;
    }
}