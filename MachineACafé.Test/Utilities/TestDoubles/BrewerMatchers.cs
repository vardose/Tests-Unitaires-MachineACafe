using Xunit;
using Hardware;

namespace MachineACafé.Test.Utilities.TestDoubles;

public static class BrewerMatchers
{
    // Matcher pour le cas où le hardware tente de faire couler un café
    public static void ShouldHaveBeenCalledOnce(this BrewerSpy spy)
    {
        Assert.Equal(1, spy.MakeACoffeeInvocations);
    }

    public static void ShouldBeUntouched(this BrewerSpy spy)
    {
        Assert.True(spy.Untouched);
    }
}
