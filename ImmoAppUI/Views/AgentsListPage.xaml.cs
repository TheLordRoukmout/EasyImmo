using ImmoAppUI.ViewModels;

namespace ImmoAppUI.Views
{
    public partial class AgentsListPage : ContentPage
    {
        private AgentViewModel _viewModel;

        public AgentsListPage()
        {
            InitializeComponent();
            _viewModel = new AgentViewModel();
            BindingContext = _viewModel;
        }

        private async void OnDeleteAgentClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int idAgent = (int)button.CommandParameter;

            bool confirm = await DisplayAlert("Confirmation",
                "Voulez-vous vraiment supprimer cet agent ?", "Oui", "Non");

            if (confirm)
            {
                _viewModel.DeleteAgent(idAgent);
            }
        }

        private async void OnAddAgentClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAgentPage());
        }
    }
}