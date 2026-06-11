using Hardware;
using MachineACafé.Test.Utilities;
using MachineACafé.Test.Utilities.TestDoubles;

namespace MachineACafé.Test;

//TODO : Mocks automatisés.

public class SoftwareMachineTest
{
    [Fact]
    public void AucuneAction()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineSpy();
        var brewer = new BrewerSpy();

        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachine)
            .AyantUnBrewer(brewer)
            .Build();

        // ALORS aucune invocation du Brewer ou de la ChangeMachine n'est effectuée
        changeMachine.ShouldBeUntouched();
        brewer.ShouldBeUntouched();
    }

    [Fact]
    public void CasNominal()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);

        var brewer = new BrewerSpy(new BrewerStub());
        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .Build();

        // QUAND on insère une somme supérieure ou égale au prix d'un café
        changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS le hardware fais couler un café et collecte l'argent
        brewer.ShouldHaveMadeCoffee();
        changeMachineSpy.ShouldHaveCollectedMoney();
    }

    [Fact]
    public void CasBrewerDéfaillant()
    {
        // ETANT DONNE une machine à café ayant un brewer défaillant
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);
        var brewer = new BrewerDummy();
        var brewerSpy = new BrewerSpy(brewer);

        _ = new SoftwareMachineBuilder()
            .AyantUnBrewer(brewerSpy)
            .AyantUneChangeMachine(changeMachineSpy)
            .Build();

        // QUAND on insère une somme supérieure ou égale au prix d'un café
        changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS le hardware tente de faire couler un café mais échoue et rend la monnaie
        brewerSpy.ShouldHaveMadeCoffee();
        changeMachineSpy.ShouldHaveFlushedMoney();
    }

    [Fact]
    public void PasAssezArgent()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);

        var brewer = new BrewerSpy();
        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .Build();

        // QUAND on insère moins que le prix d'un café
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);

        // ALORS le hardware ne tente pas faire couler un café et rend la monnaie
        changeMachineSpy.ShouldHaveFlushedMoney();
    }
}