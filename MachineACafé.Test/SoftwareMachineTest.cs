using Hardware;

namespace MachineACafé.Test;

public class SoftwareMachineTest
{
    public Coin prixCafé = new Coin(CoinCode.FiftyCents);

    /* --- Aucune action n'est effectué */
    [Fact]
    public void AucuneAction()
    {
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
        // ETANT DONNE une machine a café
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var brewer = new BrewerSpy(new BrewerStub());
        var machineACafé = new SoftwareMachineBuilder()
            .AyantUnBrewer(brewer)
            .AyantUneChangeMachine(changeMachine)
            .Build();

        // QUAND on insère le bon prix du café
        machineACafé.Insérer(prixCafé);

        // ALORS MakeACofee est appelé une fois sur le hardware
        Assert.Equal(1, brewer.makeACoffeeinvocations);

        // ET il est demandé au hardware de collecter les fonds
        Assert.Equal(1, changeMachine.CollectStoredMoneyInvocations);
    }

    /* --- Le brewer de la machine est défaillant --- */
    [Fact]
    public void CasBrewerDefaillant()
    {
        // ETANT DONNE une machine à café avec un brewer défaillant
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var brewer = new BrewerSpy(new BrewerDummy());
        var machineACafé = new SoftwareMachineBuilder()
            .AyantUnBrewer(brewer)
            .AyantUneChangeMachine(changeMachine)
            .Build();

        // QUAND le hardware signale une somme suffisante pour un café
        machineACafé.Insérer(prixCafé);

        // ALORS il est demandé au hardware de rembourser les fonds
        Assert.Equal(1, changeMachine.FlushStoredMoneyInvocations);

        // ET il n'est pas demandé au hardware de collecter les fonds
        Assert.Equal(0, changeMachine.CollectStoredMoneyInvocations);
    }

    /* --- L'utilisateur ne met pas assez d'argent pour un café --- */
    [Fact]
    public void CasMontantInsuffisant()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var brewer = new BrewerSpy(new BrewerStub());
        var machineACafé = new SoftwareMachineBuilder()
            .AyantUnBrewer(brewer)
            .AyantUneChangeMachine(changeMachine)
            .Build();

        // QUAND le hardware signale une somme insuffisante pour un café
        prixCafé = new Coin(CoinCode.TwentyCents);
        machineACafé.Insérer(prixCafé);

        // ALORS le hardware ne peux pas faire couler de café
        Assert.Equal(0, brewer.makeACoffeeinvocations);

        // ET il est demandé au hardware de rembourser les fonds
        Assert.Equal(1, changeMachine.FlushStoredMoneyInvocations);
    }

    /* --- L'utilisateur met plus d'argent que ce qu'il faut pour un café --> On rend tout sans café --- */
    [Fact]
    public void CasArgentEnTrop()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var brewer = new BrewerSpy(new BrewerStub());
        var machineACafé = new SoftwareMachineBuilder()
            .AyantUnBrewer(brewer)
            .AyantUneChangeMachine(changeMachine)
            .Build();

        // QUAND le hardware signale une somme plus que suffisante pour un café
        prixCafé = new Coin(CoinCode.OneEuro);
        machineACafé.Insérer(prixCafé);

        // ALORS le hardware ne fait pas couler de café
        Assert.Equal(0, brewer.makeACoffeeinvocations);

        // ET il est demandé au hardware de rembourser les fonds en trop
        Assert.Equal(1, changeMachine.FlushStoredMoneyInvocations);
    }
}