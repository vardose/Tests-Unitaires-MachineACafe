using Hardware;

namespace MachineACafé.Test.Utilities;

internal class BrewerSpy : IBrewer
{
    private readonly IBrewer _behavior;

    public ushort MakeACoffeeInvocations { get; private set; }
    public ushort TryPullWaterInvocations { get; private set; }
    public ushort PourWaterInvocations { get; private set; }

    public bool ResultatMakeACoffee = true;
    public bool Untouched => MakeACoffeeInvocations == 0;

    public BrewerSpy(): this(new BrewerStub()) {}

    public BrewerSpy(IBrewer behavior)
    {
        _behavior = behavior;
    }

    public bool MakeACoffee()
    {
        MakeACoffeeInvocations++;
        return ResultatMakeACoffee; // Je renvois le stock d'eau pour le cas où un allongé est impossible
    }

    public bool TryPullWater()
    {
        TryPullWaterInvocations++;
        return _behavior.TryPullWater();
    }

    public bool PourMilk()
    {
        return _behavior.PourMilk();
    }

    public bool PourWater()
    {
        PourWaterInvocations++;
        return _behavior.PourWater();
    }

    public bool PourSugar()
    {
        return _behavior.PourSugar();
    }

    public bool PourChocolate()
    {
        return _behavior.PourChocolate();
    }
}