using Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineACafé
{
    public class Coin
    {
        public CoinCode Code { get; }

        public Coin(CoinCode code)
        {
            Code = code;
        }

        public ushort ValeurEnCentimes => (ushort)Code;
    }
}
