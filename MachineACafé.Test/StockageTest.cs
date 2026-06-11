
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
        brewer.ShouldHaveMadeCoffee();
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
        brewer.ShouldHaveMadeCoffee();
        changeMachineSpy.ShouldHaveFlushedMoney();
    }

    [Fact]
    public void CasCaféAllongé()
    {
        // ETANT DONNE une machine à café dont le sotck d'eau est plein
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);
        var brewer = new BrewerSpy();
        var buttonPanel = new ButtonPanelFake();

        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .AyantUnButtonPanel(buttonPanel)
            .Build();

        // QUAND on commande un café allongé
        buttonPanel.SimulerButtonPressed(ButtonCode.Lungo);
        changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS le hardware fais couler un café allongé
        brewer.ShouldHavePulledWater();
        brewer.ShouldHaveMadeCoffee();
        brewer.ShouldHavePouredWater();
        changeMachineSpy.ShouldHaveCollectedMoney();
        // ET la LED d'alerte de café allongé impossible reste éteinte
        buttonPanel.ShouldHaveLungoWarningState(false);
    }

    [Fact]
    public void CasCaféAllongéImpossible()
    {
        // ETANT DONNE une machine à café dont le sotck d'eau est insuffisant pour un café allongé
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);
        var brewer = new BrewerSpy();
        var buttonPanel = new ButtonPanelFake();

        brewer.ResultatPourWater = false; // Stock d'eau insuffisant pour un allongé

        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .AyantUnButtonPanel(buttonPanel)
            .Build();

        // QUAND on commande un café allongé
        buttonPanel.SimulerButtonPressed(ButtonCode.Lungo);
        changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS le hardware tente de faire couler un café allongé mais ne peux pas ajouter d'eau
        brewer.ShouldHavePulledWater();
        brewer.ShouldHaveMadeCoffee();
        brewer.ShouldHavePouredWater();
        changeMachineSpy.ShouldHaveCollectedMoney();
        // ET la LED d'alerte s'allume pour signaler qu'il est impossible de faire couler un café allongé
        buttonPanel.ShouldHaveLungoWarningState(true);
    }
}
