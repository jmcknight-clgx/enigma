using Enigma.domain;
using Enigma.domain.models;
using Xunit;

namespace Enigma.tests;

public class EncryptorTests
{
    [Fact]
    public void Encryptor_Encrypt_()
    {
        var target = defaultTarget();
        var result = target.Encrypt("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        Assert.Equal("BJELRQZVJWARXSNBXORSTNCFME", result);
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