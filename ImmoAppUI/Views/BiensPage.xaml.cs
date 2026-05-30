using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;
using ImmoAppUI.ViewModels;

namespace ImmoAppUI.Views;

public partial class BiensPage : ContentPage
{
    private RealEstateViewModel _viewModel;
    private readonly TypeEstateService _typeEstateService;
    private readonly TypeStatusOfferService _typeStatusOfferService;

    public BiensPage()
    {
        InitializeComponent();
        _typeEstateService = new TypeEstateService();
        _typeStatusOfferService = new TypeStatusOfferService();
        _viewModel = new RealEstateViewModel();
        BindingContext = _viewModel;
        LoadTypePicker();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadRealEstates();
    }

    private void LoadTypePicker()
    {
        TypePicker.ItemsSource = _typeEstateService.GetAllTypeEstates();
        StatusPicker.ItemsSource = _typeStatusOfferService.GetAllTypeStatusOffers();
    }

    private void OnTypePickerChanged(object sender, EventArgs e)
    {
        ApplyFilters();
    }

    private void OnStatusPickerChanged(object sender, EventArgs e)
    {
        ApplyFilters();
    }

    private void OnFilterClicked(object sender, EventArgs e)
    {
        ApplyFilters();
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        TypePicker.SelectedIndex = -1;
        StatusPicker.SelectedIndex = -1;
        RefEntry.Text = string.Empty;
        PriceEntry.Text = string.Empty;
        CityEntry.Text = string.Empty;
        SurfaceEntry.Text = string.Empty;
        _viewModel.LoadRealEstates();
    }

    private void ApplyFilters()
    {
        var selectedType = TypePicker.SelectedItem as TypeEstate;
        var selectedStatus = StatusPicker.SelectedItem as TypeStatusOffer;
        int? idType = selectedType?.IdTypeEstate;
        int? idStatus = selectedStatus?.IdStatusOffer;
        decimal? maxPrice = decimal.TryParse(PriceEntry.Text, out decimal p) ? p : null;
        decimal? minSurface = decimal.TryParse(SurfaceEntry.Text, out decimal s) ? s : null;

        _viewModel.FilterRealEstates(
            idType,
            RefEntry.Text,
            CityEntry.Text,
            maxPrice,
            minSurface,
            idStatus
        );
    }

    private async void OnAddEstateClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddRealEstatePage());
    }

    private async void OnDetailsClicked(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        int idEstate = (int)button.CommandParameter;
        await Navigation.PushAsync(new RealEstateDetailPage(idEstate));
    }
}