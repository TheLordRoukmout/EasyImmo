using ImmoAppUI.ViewModels;

namespace ImmoAppUI.Views;

public partial class ClientsListPage : ContentPage
{
    private ClientViewModel _viewModel;

    public ClientsListPage()
    {
        InitializeComponent();
        _viewModel = new ClientViewModel();
        BindingContext = _viewModel;
    }

    private async void OnAddClientClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddClientPage());
    }

    private async void OnDeleteClientClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        int idClient = (int)button.CommandParameter;

        bool confirm = await DisplayAlert("Confirmation",
            "Voulez-vous vraiment supprimer ce client ?", "Oui", "Non");

        if (confirm)
        {
            _viewModel.DeleteClient(idClient);
        }
    }

    private async void OnEditClientClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        int idClient = (int)button.CommandParameter;
        await Navigation.PushAsync(new EditClientPage(idClient));
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            _viewModel.LoadClients();
        else
            _viewModel.SearchClients(e.NewTextValue);
    }
}