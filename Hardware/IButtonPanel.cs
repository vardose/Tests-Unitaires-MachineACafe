namespace Hardware;

public interface IButtonPanel
{
    /// <summary>
    /// Enregistre un callback appelé lors de l'appui sur un bouton de la façade avant.
    /// </summary>
    /// <param name="callback">
    /// Callback prenant un unique paramètre contenant l'ID du bouton pressé.
    /// </param>
    void RegisterButtonPressedCallback(Action<ButtonCode> callback);

    /// <summary>
    /// Allume ou éteint la LED informant de l'impossibilité d'avoir un allongé.
    /// </summary>
    /// <param name="state">Le nouvel état de la LED.</param>
    void SetLungoWarningState(bool state);
}