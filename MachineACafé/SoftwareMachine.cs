using Hardware;

namespace MachineACafé;

public class SoftwareMachine
{
    private readonly IBrewer _brewer;
    private readonly IChangeMachine _changeMachine;

    public SoftwareMachine(IBrewer brewer, IChangeMachine changeMachine)
    {
        _brewer = brewer;
        _changeMachine = changeMachine;
    }

    public void Insérer(Coin pieceInséré)
    {
        ushort montantEnCentimes = 0;
        montantEnCentimes += pieceInséré.ValeurEnCentimes;

        if (montantEnCentimes < 50 || montantEnCentimes > 50)
        {
            _changeMachine.FlushStoredMoney();
            return;
        }

        try
        {
            _brewer.MakeACoffee();
            _changeMachine.CollectStoredMoney();
        }
        catch
        {
            _changeMachine.FlushStoredMoney();
        }
    }
}