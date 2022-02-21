using System.Collections.Generic;
using Enigma.domain;
using Enigma.domain.models;
using Xunit;

namespace Enigma.tests;

public class EncryptorTests
{
    [Fact]
    public void Encryptor_Encrypt_Rotor123BasicTest()
    {
        var target = new Encryptor(GetSettings(Rotors.III, 'A', 'A'),
            GetSettings(Rotors.II, 'A', 'A'),
            GetSettings(Rotors.I, 'A', 'A'), Reflectors.B,
            new Dictionary<char, char>());
        var result = target.Encrypt("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        Assert.Equal("BJELRQZVJWARXSNBXORSTNCFME", result);
    }

    [Fact]
    public void Encryptor_Encrypt_PlugboardSettings()
    {
        var target = new Encryptor(GetSettings(Rotors.III, 'A', 'A'),
            GetSettings(Rotors.II, 'A', 'A'),
            GetSettings(Rotors.I, 'A', 'A'), Reflectors.B,
            new Dictionary<char, char>
            {
                {'A', 'B'},
                {'C', 'D'},
                {'E', 'F'},
                {'G', 'H'},
                {'I', 'J'}
            });
        var result = target.Encrypt("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        Assert.Equal("BCIAHNTLJUBRXSNAXORSTNDEMF", result);
    }

    [Fact]
    public void Encryptor_Encrypt_WithRotor3InitialSettingB()
    {
        var target = new Encryptor(GetSettings(Rotors.III, 'B', 'A'),
            GetSettings(Rotors.II, 'A', 'A'),
            GetSettings(Rotors.I, 'A', 'A'), Reflectors.B,
            new Dictionary<char, char>());
        var result = target.Encrypt("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        Assert.Equal("DLBINDLTUSCEROVRHSTUAITILN", result);
    }
    
    [Fact]
    public void Encryptor_Encrypt_WithRotor3InitialSettingA_AndRotor3RingSettingB()
    {
        var target = new Encryptor(GetSettings(Rotors.III, 'A', 'B'),
            GetSettings(Rotors.II, 'A', 'A'),
            GetSettings(Rotors.I, 'A', 'A'), Reflectors.B,
            new Dictionary<char, char>());
        var result = target.Encrypt("ABCDE");
        Assert.Equal("UARJW", result);
    }
    
    [Fact]
    public void Encryptor_Encrypt_WithRotor3InitialSettingC_AndRotor3RingSettingD()
    {
        var target = new Encryptor(GetSettings(Rotors.III, 'C', 'D'),
            GetSettings(Rotors.II, 'A', 'A'),
            GetSettings(Rotors.I, 'A', 'A'), Reflectors.B,
            new Dictionary<char, char>());
        var result = target.Encrypt("HELLOWORLD");
        Assert.Equal("ZFEBMQKNGR", result);
    }

    [Fact]
    public void Encryptor_Encrypt_Rotor345BasicTest()
    {
        var target = new Encryptor(GetSettings(Rotors.V, 'A', 'A'),
            GetSettings(Rotors.IV, 'A', 'A'),
            GetSettings(Rotors.III, 'A', 'A'), Reflectors.B,
            new Dictionary<char, char>());
        var result = target.Encrypt("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        Assert.Equal("HTBTVMVABMJJTMYUHPARRYVHZO", result);
    }

    [Fact]
    public void Encryptor_Encrypt_ReflectorCBasicTest()
    {
        var target = new Encryptor(GetSettings(Rotors.V, 'A', 'A'),
            GetSettings(Rotors.IV, 'A', 'A'),
            GetSettings(Rotors.III, 'A', 'A'), Reflectors.C,
            new Dictionary<char, char>());
        var result = target.Encrypt("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        Assert.Equal("IRUJPJXBPPLTKTKCNBESZEPBRS", result);
    }

    [Fact]
    public void Encryptor_Encrypt_Rotor345RefecltorCWithPlugboardSettings()
    {
        var target = new Encryptor(GetSettings(Rotors.V, 'F', 'G'),
            GetSettings(Rotors.IV, 'H', 'I'),
            GetSettings(Rotors.III, 'J', 'K'), Reflectors.C,
            new Dictionary<char, char>
            {
                {'A', 'B'},
                {'C', 'D'},
                {'E', 'F'},
                {'G', 'H'},
                {'I', 'J'}
            });
        var result = target.Encrypt("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        Assert.Equal("HCXZIWCFGTBPJTQCMGCSGXBKRM", result);
    }

    private RotorSettings GetSettings(Rotors rotors, char initialValue, char ringSetting) => new RotorSettings
    {
        Rotor = rotors,
        CurrentValue = initialValue,
        RingSetting = ringSetting
    };
}