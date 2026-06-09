namespace MachineACafé;

public class SoftwareMachine
{
    public const ushort prixCaféEnCents = 40;

    public void Insérer(ushort montantEnCents)
    {
        NombreCafésServis ++;
        SommeEncaisséeEnCentimes += 40;
    }

    public ushort NombreCafésServis { get; private set; }
    public ushort SommeEncaisséeEnCentimes { get; private set; }
}