using Xunit;

namespace MachineACafé.Test.Utilities.TestDoubles;

public static class ChangeMachineMatchers
{
    // Matcher pour le cas où l'argent est bien encaissé
    public static void ShouldHaveCollectedMoney(this ChangeMachineSpy spy)
    {
        Assert.Equal(1, spy.CollectStoredMoneyInvocations);
        Assert.Equal(0, spy.FlushStoredMoneyInvocations);
    }

    // Matcher pour le cas où l'argent est rendu
    public static void ShouldHaveFlushedMoney(this ChangeMachineSpy spy)
    {
        Assert.Equal(0, spy.CollectStoredMoneyInvocations);
        Assert.Equal(1, spy.FlushStoredMoneyInvocations);
    }

    public static void ShouldBeUntouched(this ChangeMachineSpy spy)
    {
        Assert.True(spy.Untouched);
    }
}
