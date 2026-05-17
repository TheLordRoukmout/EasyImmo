using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ImmoAppUI.Views;

public partial class AddRealEstatePage : ContentPage
{
    private readonly RealEstateService _realEstateService;
    private readonly ClientService _clientService;
    private readonly TypeEstateService _typeEstateService;

    public AddRealEstatePage()
    {
        InitializeComponent();
        _realEstateService = new RealEstateService();
        _clientService = new ClientService();
        _typeEstateService = new TypeEstateService();
        LoadPickers();
    }

    private void LoadPickers()
    {
        // Charger les types de biens
        var types = _typeEstateService.GetAllTypeEstates();
        TypeEstatePicker.ItemsSource = types;

        // Charger les clients comme propriétaires
        var clients = _clientService.GetAllClients();
        OwnerPicker.ItemsSource = clients;
    }

    private async void OnAddEstateClicked(object sender, EventArgs e)
    {
        try
        {
            var selectedType = TypeEstatePicker.SelectedItem as TypeEstate;
            var selectedOwner = OwnerPicker.SelectedItem as Client;

            if (selectedType == null)
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner un type de bien", "OK");
                return;
            }
            if (selectedOwner == null)
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner un propriétaire", "OK");
                return;
            }

            _realEstateService.AddRealEstate(
                TitleEntry.Text,
                ReferenceEntry.Text,
                AddressEntry.Text,
                CityEntry.Text,
                PostalCodeEntry.Text,
                decimal.Parse(PriceEntry.Text),
                decimal.Parse(SurfaceEntry.Text),
                DescriptionEditor.Text,
                selectedType.IdTypeEstate,
                selectedOwner.IdClient
            );

            await DisplayAlert("Succès", "Bien ajouté avec succès !", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }
}