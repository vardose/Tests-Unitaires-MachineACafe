using Xunit;
using Hardware;

namespace MachineACafé.Test.Utilities.TestDoubles;

public static class BrewerMatchers
{
    // Matcher pour le cas où le hardware tente de faire couler un café
    public static void ShouldHaveMadeCoffee(this BrewerSpy spy, ushort count = 1)
    {
        Assert.Equal(count, spy.MakeACoffeeInvocations);
    }

    // Matcher pour le cas où le hardware tente de tirer une dose d'eau
    public static void ShouldHavePulledWater(this BrewerSpy spy, ushort count = 1)
    {
        Assert.Equal(count, spy.TryPullWaterInvocations);
    }

    // Matcher pour le cas où le hardware tente d'ajouter une dose d'eau
    public static void ShouldHavePouredWater(this BrewerSpy spy, ushort count = 1)
    {
        Assert.Equal(count, spy.PourWaterInvocations);
    }

    public static void ShouldBeUntouched(this BrewerSpy spy)
    {
        Assert.True(spy.Untouched);
    }
}
