using Hardware;

namespace MachineACafé.Test.Utilities;

public class BrewerSpy : IBrewer
{
    private readonly IBrewer _behavior;
    
    public BrewerSpy(IBrewer brewer)
    {
        _behavior = brewer;
    }
    
    public bool MakeACoffee()
    {
        return _behavior.MakeACoffee();
    }

    public bool TryPullWater()
    {
        return _behavior.TryPullWater();
    }

    public bool PourMilk()
    {
        return _behavior.PourMilk();
    }

    public bool PourWater()
    {
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