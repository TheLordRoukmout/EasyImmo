using ImmoApp.BLL.Services;

namespace ImmoAppUI.Views;

public partial class EditClientPage : ContentPage
{
    private readonly ClientService _clientService;
    private readonly int _idClient;

    public EditClientPage(int idClient)
    {
        InitializeComponent();
        _clientService = new ClientService();
        _idClient = idClient;

        // Charger les données du client
        var client = _clientService.GetClientById(idClient);
        if (client != null)
        {
            LastnameEntry.Text = client.Lastname;
            FirstnameEntry.Text = client.Firstname;
            EmailEntry.Text = client.Email;
            PhoneEntry.Text = client.Phone;
            AddressEntry.Text = client.Address;
            TypeClientEntry.Text = client.TypeClient;
        }
    }

    private async void OnUpdateClientClicked(object sender, EventArgs e)
    {
        try
        {
            _clientService.UpdateClient(
                _idClient,
                LastnameEntry.Text,
                FirstnameEntry.Text,
                EmailEntry.Text,
                PhoneEntry.Text,
                AddressEntry.Text,
                TypeClientEntry.Text
            );
            await DisplayAlert("Succès", "Client modifié avec succès !", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }
}