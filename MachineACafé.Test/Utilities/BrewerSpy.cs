using Hardware;

namespace MachineACafé.Test.Utilities;

public class BrewerSpy : IBrewer
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

        // Dans le cas où le stock est vide ou que le bouton reset n'a pas été activé, le café ne coule pas
        if (!ResultatMakeACoffee)
        {
            return false;
        }

        return _behavior.MakeACoffee();
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