namespace MachineACafé.Test;

public class UnitTest1
{
    [Fact(DisplayName = "Quand la bonne somme est insérée 2 fois, deux cafés sont servis.")]
    public void Cas2Cafés()
    {
        const ushort prixCaféEnCents = 40;

        // ETANT DONNE une machine a café
        var brewerSpy = new BrewerSpy();
        var machineACafé = new SoftwareMachineBuilder().Build(brewerSpy);

        // QUAND le hardware signale une somme suffisante pour le prix d'un café, deux fois
        machineACafé.Insérer(prixCaféEnCents);
        machineACafé.Insérer(prixCaféEnCents);

        // ALORS le hardware est sollicité pour faire couler deux cafés
        Assert.Equal(2, machineACafé.NombreCafésServis);

        // ET il est demandé au hardware de collecter les fonds
        Assert.Equal(prixCaféEnCents * 2, machineACafé.SommeEncaisséeEnCentimes);
    }

    [Fact(DisplayName = "Quand la bonne somme est insérée, un café est servi.")]
    public void CasNominal()
    {
        const ushort prixCaféEnCents = 40;

        // ETANT DONNE une machine a café
        var brewerSpy = new BrewerSpy();
        var machineACafé = new SoftwareMachineBuilder().Build(brewerSpy);

        // QUAND on insère le bon prix du café
        machineACafé.Insérer(prixCaféEnCents);

        // ALORS MakeACofee est appelé une fois sur le hardware
        Assert.Equal(1, brewerSpy.makeACoffeeAppelé);

        // ET il est demandé au hardware de collecter les fonds
        Assert.Equal(prixCaféEnCents, machineACafé.SommeEncaisséeEnCentimes);
    }

    [Fact(DisplayName = "Quand aucune somme n'est insérée, aucun café n'est servi.")]
    public void CasRien()
    {
        // ETANT DONNE une machine a café
        var brewerSpy = new BrewerSpy();
        var machineACafé = new SoftwareMachineBuilder().Build(brewerSpy);

        // ALORS aucun café n'est servi
        Assert.Equal(0, machineACafé.NombreCafésServis);
    }
}