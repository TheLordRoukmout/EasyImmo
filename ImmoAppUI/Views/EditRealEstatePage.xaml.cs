using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;

namespace ImmoAppUI.Views;

public partial class EditRealEstatePage : ContentPage
{
    private readonly RealEstateService _realEstateService;
    private readonly int _idEstate;

    public EditRealEstatePage(int idEstate)
    {
        InitializeComponent();
        _realEstateService = new RealEstateService();
        _idEstate = idEstate;
        LoadEstate();
    }

    private void LoadEstate()
    {
        var estate = _realEstateService.GetRealEstateById(_idEstate);
        if (estate != null)
        {
            TitleEntry.Text = estate.Title;
            AddressEntry.Text = estate.Address;
            CityEntry.Text = estate.City;
            PostalCodeEntry.Text = estate.PostalCode;
            PriceEntry.Text = estate.Price?.ToString();
            SurfaceEntry.Text = estate.Surface?.ToString();
            DescriptionEditor.Text = estate.Description;
        }
    }

    private async void OnUpdateEstateClicked(object sender, EventArgs e)
    {
        try
        {
            _realEstateService.UpdateRealEstate(
                _idEstate,
                TitleEntry.Text,
                AddressEntry.Text,
                CityEntry.Text,
                PostalCodeEntry.Text,
                decimal.Parse(PriceEntry.Text),
                decimal.Parse(SurfaceEntry.Text),
                DescriptionEditor.Text
            );

            await DisplayAlert("Succès", "Bien modifié avec succès !", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }
}