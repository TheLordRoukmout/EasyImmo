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
}