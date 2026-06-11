using Hardware;

namespace MachineACafé;

public class SoftwareMachine
{
    private readonly IBrewer _brewer;
    private readonly IChangeMachine _changeMachine;
    private readonly IButtonPanel _buttonPanel;

    private bool _lungoDemandé = false;
    private bool _maintenanceResetDemandé = true; // Par défaut la machine est opérationnelle
    private bool _caféServiAvecSuccès = false;

    public SoftwareMachine(IBrewer brewer, IChangeMachine changeMachine, IButtonPanel buttonPanel)
    {
        _brewer = brewer;
        _changeMachine = changeMachine;
        _buttonPanel = buttonPanel;

        _changeMachine.RegisterMoneyInsertedCallback(coin => Insérer(new Coin((ushort) coin)));

        _buttonPanel.RegisterButtonPressedCallback(bouton => GérerBouton(bouton));
    }

    private void GérerBouton(ButtonCode bouton)
    {
        if (bouton == ButtonCode.Lungo)
        {
            _lungoDemandé = true;
        }

        if (bouton == ButtonCode.MaintenanceReset)
        {
            _maintenanceResetDemandé = true;
        }
    }

    private void Insérer(Coin somme)
    {
        if (somme.ValueInCents < 40 || _maintenanceResetDemandé == false)
        {
            _changeMachine.FlushStoredMoney();
            return;
        }

        try
        {
            if (_lungoDemandé)
            {
                _brewer.TryPullWater();
                _brewer.MakeACoffee();
                bool eauAllongéOk = _brewer.PourWater();

                if (!eauAllongéOk)
                {
                    // Si l'allongé est impossible, on allume la LED de warning
                    _buttonPanel.SetLungoWarningState(true);

                    _changeMachine.FlushStoredMoney();
                    return;
                }

                _caféServiAvecSuccès = true;
            }
            else
            {
                _caféServiAvecSuccès = _brewer.MakeACoffee();
            }

            if (_caféServiAvecSuccès)
            {
                _changeMachine.CollectStoredMoney();
            } else
            {
                _changeMachine.FlushStoredMoney();
            }
        }
        catch
        {
            _changeMachine.FlushStoredMoney();
        }
    }
}