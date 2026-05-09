using ImmoApp.BLL.Services;

namespace ImmoAppUI.Views;

public partial class AddClientPage : ContentPage
{
    private readonly ClientService _clientService;

    public AddClientPage()
    {
        InitializeComponent();
        _clientService = new ClientService();
    }

    private async void OnCreateClientClicked(object sender, EventArgs e)
    {
        string lastname = LastnameEntry.Text;
        string firstname = FirstnameEntry.Text;
        string email = EmailEntry.Text;
        string phone = PhoneEntry.Text;
        string address = AddressEntry.Text;
        string typeClient = TypeClientEntry.Text;

        try
        {
            _clientService.AddClient(lastname, firstname, email, phone, address, typeClient);
            await DisplayAlert("Succès", "Client créé avec succès !", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }
}