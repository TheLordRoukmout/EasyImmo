using ImmoAppUI.ViewModels;

namespace ImmoAppUI.Views;

public partial class RealEstatePage : ContentPage
{
    private RealEstateViewModel? _viewModel;
    private readonly int _idTypeEstate;

    public RealEstatePage() : this(0) { }

    public RealEstatePage(int idTypeEstate)
    {
        InitializeComponent();
        _idTypeEstate = idTypeEstate; // ? on stocke juste l'ID
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        DisplayAlert("Debug", $"idTypeEstate = {_idTypeEstate}", "OK");
        _viewModel = new RealEstateViewModel(_idTypeEstate);
        BindingContext = _viewModel;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            _viewModel?.LoadRealEstates();
        else
            _viewModel?.SearchRealEstates(e.NewTextValue);
    }

    private async void OnAddEstateClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddRealEstatePage());
    }

    private async void OnEditEstateClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;
        int idEstate = (int)button.CommandParameter;
        await Navigation.PushAsync(new EditRealEstatePage(idEstate));
    }

    private async void OnDeleteEstateClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;
        int idEstate = (int)button.CommandParameter;
        bool confirm = await DisplayAlert("Confirmation",
            "Voulez-vous vraiment supprimer ce bien ?", "Oui", "Non");
        if (confirm)
            _viewModel?.DeleteRealEstate(idEstate);
    }

    private async void OnDetailsClicked(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        int idEstate = (int)button.CommandParameter;
        await Navigation.PushAsync(new RealEstateDetailPage(idEstate));
    }

    private void OnFilterClicked(object sender, EventArgs e)
    {
        // On branchera les filtres après
    }
}