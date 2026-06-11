using Hardware;

namespace MachineACafé.Test.Utilities;

internal class SoftwareMachineBuilder
{
    private IBrewer _brewer = new BrewerStub();
    private IChangeMachine _changeMachine = new ChangeMachineStub();
    private IButtonPanel _buttonPanel = new ButtonPanelFake();

    public SoftwareMachine Build()
    {
        return new SoftwareMachine(_brewer, _changeMachine, _buttonPanel);
    }

    public SoftwareMachineBuilder AyantUnBrewer(IBrewer brewer)
    {
        _brewer = brewer;
        return this;
    }

    public SoftwareMachineBuilder AyantUneChangeMachine(IChangeMachine changeMachine)
    {
        _changeMachine = changeMachine;
        return this;
    }

    public SoftwareMachineBuilder AyantUnButtonPanel(IButtonPanel buttonPanel)
    {
        _buttonPanel = buttonPanel;
        return this;
    }
}