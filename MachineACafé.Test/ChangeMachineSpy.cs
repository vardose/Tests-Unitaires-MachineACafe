using Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineACafé.Test
{
    internal class ChangeMachineSpy : IChangeMachine
    {
        private readonly IChangeMachine _behavior;

        public ushort FlushStoredMoneyInvocations { get; private set; }
        public ushort CollectStoredMoneyInvocations { get; private set; }

        public ChangeMachineSpy(IChangeMachine behavior)
        {
            _behavior = behavior;
        }

        public void CollectStoredMoney()
        {
            CollectStoredMoneyInvocations++;
            _behavior.CollectStoredMoney();
        }

        public void FlushStoredMoney()
        {
            FlushStoredMoneyInvocations++;
            _behavior.FlushStoredMoney();
        }

        public void RegisterMoneyInsertedCallback(Action<CoinCode> callback)
        {
            _behavior.RegisterMoneyInsertedCallback(callback);
        }

        public bool DropCashback(CoinCode coinCode)
        {
            return _behavior.DropCashback(coinCode);
        }
    }
}
