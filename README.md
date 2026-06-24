# Tests-Unitaires-MachineACafe

## **Plan de tests** :

- **Fonctionnel > Exactitude**
  - **Niveau**: Acceptation
  - **Temporalité**: Test-First + Defect Testing
  - **Rationnelle**: Valider les comportements attendus côté métier (ex. rendu monnaie, encaissement, LED d'alerte) via les tests d'acceptation automatisés.
  - **Cas couverts**: `CasCaféStockVide`, `CasStockSemiPleinSansBoutonReset`, `CasStockPleinSansBoutonReset`.

- **Fonctionnel > Complétude**
  - **Niveau**: Manuel
  - **Temporalité**: Test-Last
  - **Rationnelle**: Compléter la couverture fonctionnelle par des revues manuelles et exécution ponctuelle pour s'assurer qu'aucune fonctionnalité métier n'a été oubliée. Utiliser mutation testing en complément.
  - **Cas couverts**: revue globale de tous les tests listés ci-dessous.

- **Performance > Temps de réponse**
  - **Niveau**: Intégration
  - **Temporalité**: Test-Last
  - **Rationnelle**: Tests d'intégration si besoin (coût / latence) — difficile à simuler en E2E sans le vrai hardware.

- **Fiabilité > Robustesse**
  - **Niveau**: Intégration
  - **Temporalité**: Test-Last
  - **Rationnelle**: Vérifier tolérance aux pannes et comportements erratiques (monnaie coincée, échec d'ajout d'eau) — penser à monkey testing et scénarios fault-tolerance.

- **Maintenabilité**
  - **Niveau**: Intégration
  - **Temporalité**: Test-Last
  - **Rationnelle**: Analyses manuelles régulières pour garder les tests clairs et évolutifs.

**Aspects non-couverts / Responsabilités**
- Compatibilité: testé localement par le développeur sur sa machine.
- Utilisabilité: hors périmètre (UI non responsable).
- Sécurité: responsabilité du hardware principalement.
- Portabilité: hardware figé — faible priorité.

---

## **Notes d'exécution** :
- Pour exécuter les tests locaux :

```powershell
dotnet test MachineACafée9.Test\\MachineACafée9.Test.csproj
```
---