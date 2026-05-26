using ImmoAppUI.ViewModels;

namespace ImmoAppUI.Views;

public partial class CalendarPage : ContentPage
{
    private CalendarViewModel _viewModel;

    public CalendarPage()
    {
        InitializeComponent();
        _viewModel = new CalendarViewModel();
        BindingContext = _viewModel;
    }

    private void OnPreviousMonthClicked(object sender, EventArgs e)
    {
        _viewModel.GoToPreviousMonth();
    }

    private void OnNextMonthClicked(object sender, EventArgs e)
    {
        _viewModel.GoToNextMonth();
    }

    private async void OnEventTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is int idEstate)
            await Navigation.PushAsync(new RealEstateDetailPage(idEstate));
    }
}