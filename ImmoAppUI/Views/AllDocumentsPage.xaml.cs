using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;
using System.Collections.ObjectModel;

namespace ImmoAppUI.Views;

public partial class AllDocumentsPage : ContentPage
{
    private readonly EstateDocumentService _documentService;
    private ObservableCollection<EstateDocument> _documents = new();

    public ObservableCollection<EstateDocument> Documents
    {
        get => _documents;
        set
        {
            _documents = value;
            OnPropertyChanged();
        }
    }

    public AllDocumentsPage()
    {
        InitializeComponent();
        _documentService = new EstateDocumentService();
        BindingContext = this;
        LoadDocuments();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadDocuments();
    }

    private void LoadDocuments()
    {
        var docs = _documentService.GetAllDocuments();
        Documents = new ObservableCollection<EstateDocument>(docs);
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            LoadDocuments();
        else
        {
            var filtered = _documentService.GetAllDocuments()
                .Where(d => d.DocumentName != null &&
                       d.DocumentName.Contains(e.NewTextValue, StringComparison.OrdinalIgnoreCase))
                .ToList();
            Documents = new ObservableCollection<EstateDocument>(filtered);
        }
    }

    private void OnOpenDocumentClicked(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        string? path = button.CommandParameter as string;
        if (path != null && File.Exists(path))
            Launcher.OpenAsync(new OpenFileRequest { File = new ReadOnlyFile(path) });
    }

    private async void OnGoToEstateClicked(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        if (button.CommandParameter is int idEstate)
            await Navigation.PushAsync(new RealEstateDetailPage(idEstate));
    }
}