using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;

namespace ImmoAppUI.Views;

public partial class AddEventPage : ContentPage
{
    private readonly EventService _eventService;
    private readonly ClientService _clientService;
    private readonly TypeEventService _typeEventService;
    private readonly int _idEstate;

    public AddEventPage(int idEstate)
    {
        InitializeComponent();
        _eventService = new EventService();
        _clientService = new ClientService();
        _typeEventService = new TypeEventService();
        _idEstate = idEstate;
        LoadPickers();
    }

    private void LoadPickers()
    {
        TypeEventPicker.ItemsSource = _typeEventService.GetAllTypeEvents();
        ClientPicker.ItemsSource = _clientService.GetAllClients();
    }

    private async void OnAddEventClicked(object sender, EventArgs e)
    {
        try
        {
            var selectedType = TypeEventPicker.SelectedItem as TypeEvent;
            var selectedClient = ClientPicker.SelectedItem as Client;

            if (selectedType == null)
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner un type d'événement", "OK");
                return;
            }
            if (selectedClient == null)
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner un client", "OK");
                return;
            }

            _eventService.AddEvent(
                _idEstate,
                selectedType.IdTypeEvent,
                EventDatePicker.Date,
                NotesEditor.Text,
                selectedClient.IdClient,
                RoleEntry.Text
            );

            await DisplayAlert("Succès", "Événement ajouté avec succès !", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }
}