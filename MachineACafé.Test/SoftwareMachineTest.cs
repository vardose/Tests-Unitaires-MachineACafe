using Hardware;

namespace MachineACafé.Test;

public class SoftwareMachineTest
{
    /* --- Aucune action n'est effectué */
    [Fact]
    public void AucuneAction()
    {
        const ushort prixCaféEnCents = 40;

        // ETANT DONNE une machine a café
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var brewer = new BrewerSpy(new BrewerStub());
        var machineACafé = new SoftwareMachineBuilder()
            .AyantUnBrewer(brewer)
            .AyantUneChangeMachine(changeMachine)
            .Build();

        // ALORS aucune invocation du Brewer ou de la ChangeMachine n'est effectuée
        Assert.Equal(0, brewer.makeACoffeeinvocations);
        Assert.Equal(0, changeMachine.CollectStoredMoneyInvocations);
        Assert.Equal(0, changeMachine.FlushStoredMoneyInvocations);
    }

    /* --- L'utilisteur mets le prix d'un café --- */
    [Fact]
    public void CasNominal()
    {
        const ushort prixCaféEnCents = 40;

        // ETANT DONNE une machine a café
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var brewer = new BrewerSpy(new BrewerStub());
        var machineACafé = new SoftwareMachineBuilder()
            .AyantUnBrewer(brewer)
            .AyantUneChangeMachine(changeMachine)
            .Build();

        // QUAND on insère le bon prix du café
        machineACafé.Insérer(prixCaféEnCents);

        // ALORS MakeACofee est appelé une fois sur le hardware
        Assert.Equal(1, brewer.makeACoffeeinvocations);

        // ET il est demandé au hardware de collecter les fonds
        Assert.Equal(1, changeMachine.CollectStoredMoneyInvocations);
    }

    /* --- Le brewer de la machine est défaillant --- */
    [Fact]
    public void CasBrewerDefaillant()
    {
        const ushort prixCaféEnCents = 40;

        // ETANT DONNE une machine à café avec un brewer défaillant
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var brewer = new BrewerSpy(new BrewerDummy());
        var machineACafé = new SoftwareMachineBuilder()
            .AyantUnBrewer(brewer)
            .AyantUneChangeMachine(changeMachine)
            .Build();

        // QUAND le hardware signale une somme suffisante pour un café
        machineACafé.Insérer(prixCaféEnCents);

        // ALORS le hardware ne peux pas faire couler de café
        Assert.Equal(0, brewer.makeACoffeeinvocations);

        // ET il est demandé au hardware de rembourser les fonds
        Assert.Equal(1, changeMachine.FlushStoredMoneyInvocations);
    }

    /* --- L'utilisateur ne met pas assez d'argent pour un café --- */
    [Fact]
    public void CasMontantInsuffisant()
    {
        const ushort prixCaféEnCents = 40;

        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var brewer = new BrewerSpy(new BrewerStub());
        var machineACafé = new SoftwareMachineBuilder()
            .AyantUnBrewer(brewer)
            .AyantUneChangeMachine(changeMachine)
            .Build();

        // QUAND le hardware signale une somme insuffisante pour un café
        machineACafé.Insérer(prixCaféEnCents - 1);

        // ALORS le hardware ne peux pas faire couler de café
        Assert.Equal(0, brewer.makeACoffeeinvocations);

        // ET il est demandé au hardware de rembourser les fonds
        Assert.Equal(1, changeMachine.FlushStoredMoneyInvocations);
    }

    /* --- L'utilisateur met plus d'argent que ce qu'il faut pour un café --> On rend tout sans café --- */
    [Fact]
    public void CasArgentEnTrop()
    {
        const ushort prixCaféEnCents = 40;

        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var brewer = new BrewerSpy(new BrewerStub());
        var machineACafé = new SoftwareMachineBuilder()
            .AyantUnBrewer(brewer)
            .AyantUneChangeMachine(changeMachine)
            .Build();

        // QUAND le hardware signale une somme plus que suffisante pour un café
        machineACafé.Insérer(prixCaféEnCents + 1);

        // ALORS le hardware ne fait pas couler de café
        Assert.Equal(0, brewer.makeACoffeeinvocations);

        // ET il est demandé au hardware de rembourser les fonds en trop
        Assert.Equal(1, changeMachine.FlushStoredMoneyInvocations);
    }
}