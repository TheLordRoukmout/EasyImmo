using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;

namespace ImmoAppUI.Views;

public partial class EditAgentPage : ContentPage
{
    private readonly AgentService _agentService;
    private readonly int _idAgent;

    public EditAgentPage(int idAgent)
    {
        InitializeComponent();
        _agentService = new AgentService();
        _idAgent = idAgent;
        LoadAgent();
    }

    private void LoadAgent()
    {
        var agent = _agentService.GetAllAgents()
            .FirstOrDefault(a => a.IdAgent == _idAgent);

        if (agent != null)
        {
            UsernameEntry.Text = agent.IdUserNavigation.Username;
            EmailEntry.Text = agent.IdUserNavigation.Email;
            PhoneEntry.Text = agent.IdUserNavigation.Phone;
            if (agent.HireDate.HasValue)
                HireDatePicker.Date = agent.HireDate.Value.ToDateTime(TimeOnly.MinValue);
            ActiveSwitch.IsToggled = agent.Active ?? true;
        }
    }

    private async void OnUpdateAgentClicked(object sender, EventArgs e)
    {
        try
        {
            DateOnly? hireDate = DateOnly.FromDateTime(HireDatePicker.Date);

            _agentService.UpdateAgent(
                _idAgent,
                UsernameEntry.Text,
                EmailEntry.Text,
                PhoneEntry.Text,
                hireDate,
                ActiveSwitch.IsToggled
            );

            await DisplayAlert("Succès", "Agent modifié avec succès !", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }
}