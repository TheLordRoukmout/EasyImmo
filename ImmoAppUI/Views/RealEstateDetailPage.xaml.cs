using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;
using ImmoAppUI.ViewModels;

namespace ImmoAppUI.Views;

public partial class RealEstateDetailPage : ContentPage
{
    private readonly RealEstateService _realEstateService;
    private readonly int _idEstate;
    private RealEstateDetailViewModel? _viewModel;

    public RealEstateDetailPage(int idEstate)
    {
        InitializeComponent();
        _realEstateService = new RealEstateService();
        _idEstate = idEstate;
        try
        {
            _viewModel = new RealEstateDetailViewModel(idEstate);
            BindingContext = _viewModel;
        }
        catch (Exception ex)
        {
            DisplayAlert("Erreur", ex.Message, "OK");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel?.LoadImages(_idEstate);
        _viewModel?.LoadStatus(_idEstate);
        _viewModel?.LoadEvents(_idEstate);
    }

    private async void OnManagePhotosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManagePhotosPage(_idEstate));
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditRealEstatePage(_idEstate));
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmation",
            "Voulez-vous vraiment supprimer ce bien ?", "Oui", "Non");
        if (confirm)
        {
            _realEstateService.DeleteRealEstate(_idEstate);
            await Navigation.PopAsync();
        }
    }

    private void OnChangeStatusClicked(object sender, EventArgs e)
    {
        var selected = StatusPicker.SelectedItem as TypeStatusOffer;
        if (selected == null)
        {
            DisplayAlert("Erreur", "Veuillez sélectionner un statut", "OK");
            return;
        }
        _viewModel?.ChangeStatus(_idEstate, selected.IdStatusOffer);
        DisplayAlert("Succès", "Statut mis à jour !", "OK");
    }

    private async void OnAddEventClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddEventPage(_idEstate));
    }

    private async void OnDeleteEventClicked(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        int idEvent = (int)button.CommandParameter;
        bool confirm = await DisplayAlert("Confirmation",
            "Supprimer cet événement ?", "Oui", "Non");
        if (confirm)
        {
            var eventService = new EventService();
            eventService.DeleteEvent(idEvent);
            _viewModel?.LoadEvents(_idEstate);
        }
    }
}