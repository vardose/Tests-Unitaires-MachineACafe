using Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineACafé.Test
{
    internal class SoftwareMachineBuilder
    {
        private IBrewer _brewer = new BrewerSpy();



        public SoftwareMachine Build(IBrewer brewer)
        {
            return new SoftwareMachine(brewer);
        }
    }
}
