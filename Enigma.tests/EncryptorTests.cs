using Enigma.domain;
using Enigma.domain.dtos;
using Xunit;

namespace Enigma.tests;

public class EncryptorTests
{
    [Fact]
    public void Test1()
    {
        var target = defaultTarget();
        var result = target.Encrypt("A");
        Assert.Equal("U", result);
    }

    private Encryptor defaultTarget() => new Encryptor(GetSettings(Rotors.III, 'A', 'A'),
            GetSettings(Rotors.II, 'A', 'A'),
            GetSettings(Rotors.I, 'A', 'A'), Reflectors.B);

    private RotorSettings GetSettings(Rotors rotors, char initialValue, char ringSetting) => new RotorSettings {
        Rotor = rotors,
        CurrentValue = initialValue,
        RingSetting = ringSetting
    };

}