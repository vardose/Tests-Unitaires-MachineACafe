using Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineACafé.Test
{
    internal class BrewerStub : IBrewer
    {
        public bool MakeACoffee()
        {
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
