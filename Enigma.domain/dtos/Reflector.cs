using System;
using System.Collections.Generic;

namespace Enigma.domain.dtos
{
    public class Reflector
    {
        public Dictionary<char, char> Map { get; set; }
        public char Get(char input) => Map[input];
    }
}