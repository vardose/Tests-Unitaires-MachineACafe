namespace MachineACafé;

public class SoftwareMachine
{
    public void Insérer(ushort montantEnCentimes)
    {
        MontantEncaisséEnCentimes += montantEnCentimes;
    }

    public ushort NombreCafésServis         => 1;
    public ushort MontantEncaisséEnCentimes { get; private set; }
}