
using Hardware;
using MachineACafé.Test.Utilities;
using MachineACafé.Test.Utilities.TestDoubles;

namespace MachineACafé.Test;

public class StockageTest
{
    [Fact]
    public void CasCaféStockVide()
    {
        // ETANT DONNE une machine à café dont le sotck d'eau ou de café est vide
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);
        var brewer = new BrewerSpy();
        var buttonPanel = new ButtonPanelFake();
        brewer.ResultatMakeACoffee = false; // Stock d'eau vide

        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .AyantUnButtonPanel(buttonPanel)
            .Build();

        // QUAND on commande un café et que le bouton reset a été activé lors du dernier rechargement
        buttonPanel.SimulerButtonPressed(ButtonCode.MaintenanceReset);
        changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS le hardware tente de faire couler un café mais échoue et rend la monnaie
        brewer.ShouldHaveBeenCalledOnce();
        changeMachineSpy.ShouldHaveFlushedMoney();
    }

    [Fact]
    public void CasStockPleinSansBoutonReset()
    {
        // ETANT DONNE une machine à café dont les sotcks ont été réprovisioné mais le bouton reset n'a pas été préssé
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);
        var brewer = new BrewerSpy();
        var buttonPanel = new ButtonPanelFake();
        brewer.ResultatMakeACoffee = false; // Bouton reset non activé

        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .AyantUnButtonPanel(buttonPanel)
            .Build();

        // QUAND on commande un café
        changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS le hardware tente de faire couler un café mais échoue et rend la monnaie
        brewer.ShouldHaveBeenCalledOnce();
        changeMachineSpy.ShouldHaveFlushedMoney();
    }

    /* Cas allongé a voir plus tard
    [Fact]
    public void CasCaféAllongéImpossible()
    {
        // ETANT DONNE une machine à café dont le sotck d'eau est insuffisant pour un café allongé
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);
        var brewer = new BrewerSpy();
        var buttonPanel = new ButtonPanelFake();

        brewer.StockEausuffisant = false; // Stock d'eau vide

        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer) // TODO: Créer un brewer presque vide pour passer le test au vert
            .AyantUnButtonPanel(buttonPanel)
            .Build();

        // QUAND on commande un café allongé
        changeMachine.SimulerBoutonLungo();
        changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS TryPullWater, MakeACoffee puis PourWater sont appelés une fois dans le hardware
        Assert.Equal(1, brewer.TryPullWaterInvocations);
        Assert.Equal(1, brewer.MakeACoffeeInvocations);
        Assert.Equal(1, brewer.PourWaterInvocations);

        // ET le café n'est pas servi
        Assert.True(brewer.TryPullWater());

        // ET le café est servi
        Assert.True(brewer.MakeACoffee());

        // ET le rajout d'eau n'est pas servi
        Assert.False(brewer.PourWater());

        // ET CollectStoredMoney n'est pas appelé
        Assert.Equal(1, changeMachineSpy.CollectStoredMoneyInvocations);

        // ET FlushStoredMoney est appelé une fois sur le hardware
        Assert.Equal(0, changeMachineSpy.FlushStoredMoneyInvocations);
    }
    */
}
