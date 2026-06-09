using MachineACafé.Test.Utilities;

namespace MachineACafé.Test;

public class SoftwareMachineTest
{
    [Fact]
    public void CasNominal()
    {
        // ETANT DONNE une machine à café
        ChangeMachineSpy changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        SoftwareMachine machine = new SoftwareMachineBuilder().AyantUneChangeMachine(changeMachine).Build();

        // QUAND on insère 40cts
        machine.Insérer(SoftwareMachine.prixDuCafé);

        // ALORS MakeACoffee est appelé une fois sur le hardware
        Assert.Equal(1, machine.NombreCafésServis);

        // ET CollectStoredMoney est appelé une fois sur le hardware
        Assert.Equal(1, changeMachine.CollectStoredMoneyInvocations);
    }

    [Fact]
    public void CasBrewerDéfaillant()
    {
        // ETANT DONNE une machine à café ayant un brewer défaillant
        ChangeMachineSpy changeMachine = new ChangeMachineSpy(new ChangeMachineStub());

        SoftwareMachine machine = new SoftwareMachineBuilder()
            .AyantUnBrewer(new BrewerDummy())
            .AyantUneChangeMachine(changeMachine)
            .Build();

        // QUAND on insère 40cts
        machine.Insérer(SoftwareMachine.prixDuCafé);

        // ALORS FlushStoredMoney est appelé une fois sur le hardware
        Assert.Equal(1, changeMachine.FlushStoredMoneyInvocations);
    }

    [Fact]
    public void TropArgent()
    {
        // ETANT DONNE une machine à café
        ChangeMachineSpy changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        SoftwareMachine machine = new SoftwareMachineBuilder().AyantUneChangeMachine(changeMachine).Build();

        // QUAND on insère plus que le prix d'un café.
        machine.Insérer(SoftwareMachine.prixDuCafé + 1);

        // ALORS MakeACoffee est appelé une fois sur le hardware
        Assert.Equal(1, machine.NombreCafésServis);

        // ET CollectStoredMoney est appelé une fois sur le hardware
        Assert.Equal(1, changeMachine.CollectStoredMoneyInvocations);
    }
    
    [Fact]
    public void PasAssezArgent()
    {
        // ETANT DONNE une machine à café
        ChangeMachineSpy changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        SoftwareMachine machine       = new SoftwareMachineBuilder().AyantUneChangeMachine(changeMachine).Build();

        // QUAND on insère moins que le prix d'un café.
        machine.Insérer(SoftwareMachine.prixDuCafé + 1);
    
        // ALORS DropCashBack est appelé
        // ET FlushStoredMoney est appelé une fois sur le hardware
        Assert.Equal(1, changeMachine.FlushStoredMoneyInvocations);
    }
}