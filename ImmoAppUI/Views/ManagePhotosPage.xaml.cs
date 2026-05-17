using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;
using System.Collections.ObjectModel;

namespace ImmoAppUI.Views;

public partial class ManagePhotosPage : ContentPage
{
    private readonly EstateImageService _imageService;
    private readonly int _idEstate;
    private ObservableCollection<EstateImage> _images = new ObservableCollection<EstateImage>();

    public ObservableCollection<EstateImage> Images
    {
        get => _images;
        set
        {
            _images = value;
            OnPropertyChanged();
        }
    }

    public ManagePhotosPage(int idEstate)
    {
        InitializeComponent();
        _imageService = new EstateImageService();
        _idEstate = idEstate;
        BindingContext = this;
        LoadImages();
    }

    private void LoadImages()
    {
        var images = _imageService.GetImagesByEstate(_idEstate);
        Images = new ObservableCollection<EstateImage>(images);
    }

    private async void OnAddPhotoClicked(object sender, EventArgs e)
    {
        try
        {
            // Ouvrir le sélecteur de fichiers
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                // On copie l'image dans le dossier de l'app
                string appFolder = Path.Combine(FileSystem.AppDataDirectory, "EstateImages");
                Directory.CreateDirectory(appFolder);
                string destPath = Path.Combine(appFolder, result.FileName);
                using (var source = await result.OpenReadAsync())
                using (var dest = File.OpenWrite(destPath))
                    await source.CopyToAsync(dest);

                // On sauvegarde le chemin en DB
                _imageService.AddImage(_idEstate, destPath);
                LoadImages();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }

    private async void OnDeleteImageClicked(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        int idImage = (int)button.CommandParameter;
        bool confirm = await DisplayAlert("Confirmation",
            "Supprimer cette photo ?", "Oui", "Non");
        if (confirm)
        {
            _imageService.DeleteImage(idImage);
            LoadImages();
        }
    }

    private void OnSetMainClicked(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        int idImage = (int)button.CommandParameter;
        _imageService.SetMainImage(_idEstate, idImage);
        LoadImages();
    }
}