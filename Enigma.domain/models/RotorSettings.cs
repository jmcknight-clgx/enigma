using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Enigma.domain.models
{
    public class RotorSettings
    {
        public Rotors Rotor { get; set; }
        public char RingSetting { get; set; }
        public char CurrentValue { get; set; }

        public Rotor SetupRotor()
        {
            using Stream stream = Rotor.GetStream();
            var RotorDto = JsonSerializer.Deserialize<Rotor>(stream);
            RotorDto.CurrentSetting = CurrentValue;
            return RotorDto;
        }
    }
}