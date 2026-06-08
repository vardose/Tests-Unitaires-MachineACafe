namespace MachineACafe;

public class SoftwareMachine
{
    public void InsererPiece(ushort montantEnCents)
    {
        NombreCafesServis++;
        SommeEncaisseEnCentimes += 40;
    }

    public ushort NombreCafesServis { get; private set; }

    public ushort SommeEncaisseEnCentimes { get; private set; }
}