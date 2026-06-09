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
            // Si le code reçu n'est pas défini dans l'enum CoinCode (ex: 3, 4, 6...)
            if (!Enum.IsDefined(typeof(CoinCode), code))
            {
                throw new ArgumentException($"La pièce avec la valeur {(int)code} n'existe pas.");
            }

            Code = code;
        }

        public ushort ValeurEnCentimes => (ushort)Code;
    }
}
