**Plan de tests — StockageTest**

- **Fichier source**: [MachineACafé.Test/StockageTest.cs](MachineACafé.Test/StockageTest.cs#L1-L200)

**Résumé**: Ce document décrit, pour chaque test de `StockageTest`, l'objectif, les préconditions, les étapes et les résultats attendus. Il sert de plan de test pour vérification manuelle et reproduction automatique.

**Environnement & doubles**:
- **Commandes**: `CoinCode.FiftyCents`, `ButtonCode.MaintenanceReset`, `ButtonCode.Lungo`
- **Doubles utilisés**: `ChangeMachineFake`, `ChangeMachineSpy`, `BrewerSpy`, `ButtonPanelFake`
- **Builder**: `SoftwareMachineBuilder` pour assembler la machine avec les doubles.

**Cas de test**:

- **CasCaféStockVide**:
  - **Objectif**: Vérifier que si le stock (eau/café) est vide, le café ne peut pas être servi et la monnaie est rendue.
  - **Préconditions**: `brewer.ResultatMakeACoffee = false` (simule stock vide)
  - **Étapes**:
    1. Activer `MaintenanceReset` via `buttonPanel.SimulerButtonPressed` (indique réapprovisionnement récent avec reset activé).
    2. Insérer pièce via `changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents)`.
  - **Résultats attendus**:
    - `brewer.ShouldHaveMadeCoffee()` appelé (tentative de préparation)
    - `changeMachineSpy.ShouldHaveFlushedMoney()` (monnaie rendue)

- **CasStockPleinSansBoutonReset**:
  - **Objectif**: Vérifier que si le reset n'a pas été pressé après rechargement, la machine refuse le service.
  - **Préconditions**: `brewer.ResultatMakeACoffee = false` (reset non activé)
  - **Étapes**: Insérer pièce.
  - **Résultats attendus**: tentative de faire un café puis monnaie rendue (`ShouldHaveMadeCoffee`, `ShouldHaveFlushedMoney`).

- **CasCaféAllongé**:
  - **Objectif**: Vérifier le service d'un café allongé quand il y a assez d'eau.
  - **Préconditions**: comportement par défaut du `BrewerSpy` (suffisamment d'eau)
  - **Étapes**: appuyer `Lungo`, insérer pièce.
  - **Résultats attendus**:
    - `brewer.ShouldHavePulledWater()`
    - `brewer.ShouldHaveMadeCoffee()`
    - `brewer.ShouldHavePouredWater()`
    - `changeMachineSpy.ShouldHaveCollectedMoney()`
    - `buttonPanel.ShouldHaveLungoWarningState(false)` (pas d'alerte)

- **CasCaféAllongéImpossible**:
  - **Objectif**: Vérifier comportement quand il n'y a pas assez d'eau pour un allongé.
  - **Préconditions**: `brewer.ResultatPourWater = false` (pas assez d'eau pour ajouter)
  - **Étapes**: appuyer `Lungo`, insérer pièce.
  - **Résultats attendus**:
    - `brewer.ShouldHavePulledWater()`
    - `brewer.ShouldHaveMadeCoffee()`
    - `brewer.ShouldHavePouredWater()` (échec d'ajout d'eau)
    - `changeMachineSpy.ShouldHaveCollectedMoney()`
    - `buttonPanel.ShouldHaveLungoWarningState(true)` (LED d'alerte allumée)

- **CasStockSemiPleinAvecResetEtCaféNormal**:
  - **Objectif**: Vérifier qu'avec réapprovisionnement partiel et reset activé, un café normal fonctionne.
  - **Préconditions**:
    - `brewer.ResultatMakeACoffee = true` (café normal OK)
    - `brewer.ResultatPourWater = false` (pas assez pour allongé)
    - `buttonPanel.SimulerButtonPressed(MaintenanceReset)` exécuté avant commande
  - **Étapes**: insérer pièce pour café normal.
  - **Résultats attendus**: `brewer.ShouldHaveMadeCoffee()` et `changeMachineSpy.ShouldHaveCollectedMoney()`.

- **CasStockSemiPleinAvecResetEtCaféAllongéImpossible**:
  - **Objectif**: Vérifier qu'avec stock partiel + reset, l'allongé échoue sur l'ajout d'eau mais l'argent est encaissé.
  - **Préconditions**: `brewer.ResultatMakeACoffee = true`, `brewer.ResultatPourWater = false`, reset activé
  - **Étapes**: appuyer `Lungo`, insérer pièce.
  - **Résultats attendus**:
    - `brewer.ShouldHavePulledWater()`
    - `brewer.ShouldHaveMadeCoffee()`
    - `brewer.ShouldHavePouredWater()`
    - `changeMachineSpy.ShouldHaveCollectedMoney()`
    - `buttonPanel.ShouldHaveLungoWarningState(true)`

- **CasStockSemiPleinSansBoutonReset**:
  - **Objectif**: Vérifier qu'avec réapprovisionnement non validé (reset non pressé), la machine refuse le service.
  - **Préconditions**: `brewer.ResultatMakeACoffee = false` (reset non activé)
  - **Étapes**: insérer pièce.
  - **Résultats attendus**: tentative de faire un café puis monnaie rendue (`ShouldHaveMadeCoffee`, `ShouldHaveFlushedMoney`).

**Notes d'exécution**:
- Pour exécuter les tests locaux :

```powershell
dotnet test MachineACafée9.Test\\MachineACafée9.Test.csproj
```

- Pour cibler uniquement `StockageTest` :

```powershell
dotnet test MachineACafée9.Test\\MachineACafée9.Test.csproj --filter FullyQualifiedName~MachineACafée9.Test.StockageTest
```

- Vérifier les implémentations des doubles (`Utilities` et `TestDoubles`) pour comprendre les méthodes d'assertion (`ShouldHave...`).

---

Fichier généré automatiquement à partir de [MachineACafé.Test/StockageTest.cs](MachineACafé.Test/StockageTest.cs#L1-L200).