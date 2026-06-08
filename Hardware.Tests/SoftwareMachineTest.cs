using MachineACafe;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hardware.Tests;

[TestClass]
public class SoftwareMachineTest
{

    [TestMethod]
    public void CasNominal()
    {
        const ushort prixCafeEnCents = 40;
        
        // ETANT DONNE
        var machineACafe = new SoftwareMachine();
        
        // QUAND
        machineACafe.InsererPiece(prixCafeEnCents);
        
        // ALORS
        Assert.AreEqual(1, machineACafe.SommeEncaisseEnCentimes);
    }
}