namespace MachineACafé;

public class SoftwareMachine
{
    public void InsérerPièce(ushort montantEnCents)
    {
        NombreCafésServis ++;
        SommeEncaisséeEnCentimes += 40;
    }

    public ushort NombreCafésServis { get; private set; }
    public ushort SommeEncaisséeEnCentimes { get; private set; }
}