namespace Hardware;

public interface ICupProvider
{
    /// <summary>
    /// Relâche une touillette, sans possibilité de savoir si l'action a été efficace.
    /// </summary>
    void ProvideStirrer();

    /// <summary>
    /// Renvoie l'état du capteur de présence d'une tasse.
    /// </summary>
    /// <returns>
    /// true si une tasse est présente ; sinon false.
    /// </returns>
    bool IsCupPresent();

    /// <summary>
    /// Relâche un gobelet, s'il en reste.
    /// Il est conseillé de vérifier IsCupPresent ensuite.
    /// </summary>
    void ProvideCup();
}