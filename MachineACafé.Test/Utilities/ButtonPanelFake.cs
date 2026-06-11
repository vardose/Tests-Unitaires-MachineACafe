using Hardware;

namespace MachineACafé.Test.Utilities;

public class ButtonPanelFake : IButtonPanel
{
    private Action<ButtonCode>? _callback;

    // LED correspondant à la possibilité de faire couler un café allongé
    public bool IsLungoWarningLedOn { get; private set; }

    public void RegisterButtonPressedCallback(Action<ButtonCode> callback)
    {
        _callback = callback;
    }

    public void LungoWarningState(bool state)
    {
        IsLungoWarningLedOn = state;
    }

    public void SimulerButtonPressed(ButtonCode bouton)
    {
        _callback?.Invoke(bouton);
    }
}