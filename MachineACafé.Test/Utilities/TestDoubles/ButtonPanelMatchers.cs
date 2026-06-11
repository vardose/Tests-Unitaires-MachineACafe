using Xunit;

namespace MachineACafé.Test.Utilities.TestDoubles;

public static class ButtonPanelMatchers
{
    // Matcher pour vérifier si la LED d'alerte a reçu le bon état
    public static void ShouldHaveLungoWarningState(this ButtonPanelFake buttonPanel, bool expectedState)
    {
        Assert.Equal(expectedState, buttonPanel.IsLungoWarningLedOn);
    }
}
