using Hardware;

namespace MachineACafé;

public class SoftwareMachine
{
    public const ushort prixDuCafé = 40;
    public SoftwareMachine(IBrewer brewer, IChangeMachine changeMachine)
    {
        changeMachine.FlushStoredMoney();
        changeMachine.CollectStoredMoney();
    }
    
    public void Insérer(ushort montantEnCentimes)
    {
        MontantEncaisséEnCentimes += montantEnCentimes;
    }

    public ushort NombreCafésServis         => 1;
    public ushort MontantEncaisséEnCentimes { get; private set; }
}