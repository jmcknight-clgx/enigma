using Enigma.solvers;
using Xunit;

namespace Enigma.tests.solvers;

public class IoCCalculatorTests
{
    [Fact]
    public void Encryptor_Encrypt_Rotor123BasicTest()
    {
        // Act
        var result = IoCCalculator.CalculateIoC("WemayallowourselvesabriefperiodofrejoicingbutletusnotforgetforamomentthetoilandeffortsthatlieaheadJapanwithallhertreacheryandgreedremainsunsubduedTheinjuryshehasinflictedonGreatBritaintheUnitedStatesandothercountriesandherdetestablecrueltiescallforjusticeandretributionWemustnowdevoteallourstrengthandresourcestothecompletionofourtaskbothathomeandabroadAdvanceBritanniaLonglivethecauseoffreedomGodsavetheKing");
        // Assert
        Assert.Equal(0.06676, result);
    }
}