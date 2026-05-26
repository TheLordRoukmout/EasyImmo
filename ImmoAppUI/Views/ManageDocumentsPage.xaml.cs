using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;
using System.Collections.ObjectModel;

namespace ImmoAppUI.Views;

public partial class ManageDocumentsPage : ContentPage
{
    private readonly EstateDocumentService _documentService;
    private readonly int _idEstate;
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

    public ManageDocumentsPage(int idEstate)
    {
        InitializeComponent();
        _documentService = new EstateDocumentService();
        _idEstate = idEstate;
        BindingContext = this;
        LoadDocuments();
    }

    private void LoadDocuments()
    {
        var docs = _documentService.GetDocumentsByEstate(_idEstate);
        Documents = new ObservableCollection<EstateDocument>(docs);
    }

    private async void OnAddDocumentClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".pdf", ".doc", ".docx", ".txt", ".xlsx" } }
                })
            });

            if (result != null)
            {
                // Copier le fichier dans le dossier de l'app
                string appFolder = Path.Combine(FileSystem.AppDataDirectory, "EstateDocuments");
                Directory.CreateDirectory(appFolder);
                string destPath = Path.Combine(appFolder, result.FileName);

                using (var source = await result.OpenReadAsync())
                using (var dest = File.OpenWrite(destPath))
                    await source.CopyToAsync(dest);

                _documentService.AddDocument(_idEstate, result.FileName, destPath);
                LoadDocuments();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }

    private async void OnDeleteDocumentClicked(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        int idDocument = (int)button.CommandParameter;

        bool confirm = await DisplayAlert("Confirmation",
            "Supprimer ce document ?", "Oui", "Non");

        if (confirm)
        {
            _documentService.DeleteDocument(idDocument);
            LoadDocuments();
        }
    }

    private void OnOpenDocumentClicked(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        string? path = button.CommandParameter as string;
        if (path != null && File.Exists(path))
            Launcher.OpenAsync(new OpenFileRequest { File = new ReadOnlyFile(path) });
    }
}