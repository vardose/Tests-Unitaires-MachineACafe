using Hardware;

namespace MachineACafé;

public class SoftwareMachine(IBrewer brewer)
{

    public void Insérer(ushort montantEnCents)
    {
        NombreCafésServis ++;
        SommeEncaisséeEnCentimes += 40;

        // Demande au hardware de faire couler un café
        brewer.MakeACoffee();
    }

    public ushort NombreCafésServis { get; private set; }
    public ushort SommeEncaisséeEnCentimes { get; private set; }
}