using ImmoAppUI.ViewModels;

namespace ImmoAppUI.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
        BindingContext = new DashboardViewModel();
    }

    private async void OnClientsTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new ClientsListPage());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var vm = BindingContext as DashboardViewModel;
        vm?.LoadStats();
        vm?.LoadWeekEvents();
    }

    private async void OnEstateTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is int idEstate)
            await Navigation.PushAsync(new RealEstateDetailPage(idEstate));
    }
}