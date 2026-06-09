using Hardware;

namespace MachineACafé.Test
{
    internal class ChangeMachineStub : IChangeMachine
    {
        public void CollectStoredMoney()
        {
        }

        public void FlushStoredMoney()
        {
        }

        public void RegisterMoneyInsertedCallback(Action<CoinCode> callback)
        {
        }

        public bool DropCashback(CoinCode coinCode)
        {
            return false;
        }
    }
}
