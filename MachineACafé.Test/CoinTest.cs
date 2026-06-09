using Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineACafé.Test
{
    public class CoinTest
    {
        /* --- Pièces existantes -> Valeurs valide --- */
        [Theory]
        [InlineData(1, CoinCode.OneCent)]
        [InlineData(2, CoinCode.TwoCents)]
        [InlineData(5, CoinCode.FiveCents)]
        [InlineData(10, CoinCode.TenCents)]
        [InlineData(20, CoinCode.TwentyCents)]
        [InlineData(50, CoinCode.FiftyCents)]
        [InlineData(100, CoinCode.OneEuro)]
        [InlineData(200, CoinCode.TwoEuros)]

        public void CoinValeurValide(ushort valeurEntrante, CoinCode codeAttendu)
        {
            var codeMatériel = (CoinCode)valeurEntrante;
            var pièce = new Coin(codeMatériel);

            Assert.Equal(codeAttendu, pièce.Code);
            Assert.Equal(valeurEntrante, pièce.ValeurEnCentimes);
        }

        /* --- Pièces inexistantes -> Valeurs non valide --- */
        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(9)]
        [InlineData(11)]
        [InlineData(19)]
        [InlineData(21)]
        [InlineData(49)]
        [InlineData(51)]
        [InlineData(99)]
        [InlineData(101)]
        [InlineData(199)]
        [InlineData(201)] 
        public void CoinValeurInvalide(ushort valeurInvalide)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var codeInvalide = (CoinCode)valeurInvalide;
                var pièce = new Coin(codeInvalide);
            });
        }
    }
}
