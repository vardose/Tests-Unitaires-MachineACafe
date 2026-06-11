using Hardware;

namespace MachineACafé.Test.Utilities;

public class ButtonPanelFake : IButtonPanel
{
    private Action<ButtonCode>? _callback;

    public void RegisterButtonPressedCallback(Action<ButtonCode> callback)
    {
        _callback = callback;
    }

    public void SetLungoWarningState(bool state)
    {
    }

    public void SimulerButtonPressed(ButtonCode bouton)
    {
        _callback?.Invoke(bouton);
    }
}