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

    [Fact]
    public void Encryptor_Encrypt_WithRotor3InitialSettingB()
    {
        var target = new Encryptor(GetSettings(Rotors.III, 'B', 'A'),
            GetSettings(Rotors.II, 'A', 'A'),
            GetSettings(Rotors.I, 'A', 'A'), Reflectors.B);
        var result = target.Encrypt("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        Assert.Equal("DLBINDLTUSCEROVRHSTUAITILN", result);
    }
    
    [Fact]
    public void Encryptor_Encrypt_WithRotor3InitialSettingA_AndRotor3RingSettingB()
    {
        var target = new Encryptor(GetSettings(Rotors.III, 'A', 'B'),
            GetSettings(Rotors.II, 'A', 'A'),
            GetSettings(Rotors.I, 'A', 'A'), Reflectors.B);
        var result = target.Encrypt("ABCDE");
        Assert.Equal("UARJW", result);
    }
    
    [Fact]
    public void Encryptor_Encrypt_WithRotor3InitialSettingC_AndRotor3RingSettingD()
    {
        var target = new Encryptor(GetSettings(Rotors.III, 'C', 'D'),
            GetSettings(Rotors.II, 'A', 'A'),
            GetSettings(Rotors.I, 'A', 'A'), Reflectors.B);
        var result = target.Encrypt("HELLOWORLD");
        Assert.Equal("ZFEBMQKNGR", result);
    }

    private Encryptor defaultTarget() => new Encryptor(GetSettings(Rotors.III, 'A', 'A'),
        GetSettings(Rotors.II, 'A', 'A'),
        GetSettings(Rotors.I, 'A', 'A'), Reflectors.B);

    private RotorSettings GetSettings(Rotors rotors, char initialValue, char ringSetting) => new RotorSettings
    {
        Rotor = rotors,
        CurrentValue = initialValue,
        RingSetting = ringSetting
    };
}