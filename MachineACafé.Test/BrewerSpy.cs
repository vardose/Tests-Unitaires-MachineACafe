using Hardware;

namespace MachineACafé.Test
{
    internal class BrewerSpy : IBrewer
    {
        public ushort makeACoffeeAppelé { get; set; } = 0;

        public bool MakeACoffee()
        {
            makeACoffeeAppelé++;
            return true;
        }

        public bool PourChocolate()
        {
            return true;
        }

        public bool PourMilk()
        {
            return true;
        }

        public bool PourSugar()
        {
            return true;
        }

        public bool PourWater()
        {
            return true;
        }

        public bool TryPullWater()
        {
            return true;
        }
    }
}
