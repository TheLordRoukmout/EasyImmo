using ImmoApp.BLL.Services;

namespace ImmoAppUI.Views
{
    public partial class AddAgentPage : ContentPage
    {
        private readonly AgentService _agentService;

        public AddAgentPage()
        {
            InitializeComponent();
            _agentService = new AgentService();
        }

        private async void OnCreateAgentClicked(object sender, EventArgs e)
        {
            // Récupération des valeurs
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            string email = EmailEntry.Text;
            string phone = PhoneEntry.Text;
            DateOnly hireDate = DateOnly.FromDateTime(HireDatePicker.Date);

            try
            {
                _agentService.CreateAgent(username, password, "agent", email, phone, hireDate);
                await DisplayAlert("Succès", "Agent créé avec succès !", "OK");
                await Navigation.PopAsync(); // retour à la liste
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }
    }
}