namespace ImmoAppUI.Views;

public partial class DocumentPage : ContentPage
{
    public DocumentPage()
    {
        InitializeComponent();
    }

    private async void OnAgentsTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new AgentsListPage());
    }

    private async void OnClientsTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new ClientsListPage());
    }

    private async void OnDocumentsTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new AllDocumentsPage());
    }
}