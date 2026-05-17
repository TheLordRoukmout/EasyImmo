using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;
using System.Collections.ObjectModel;

namespace ImmoAppUI.ViewModels
{
    public class RealEstateViewModel : BaseViewModel
    {
        private readonly RealEstateService _realEstateService;

        private ObservableCollection<RealEstate> _realEstates = new ObservableCollection<RealEstate>();
        public ObservableCollection<RealEstate> RealEstates
        {
            get => _realEstates;
            set => SetProperty(ref _realEstates, value);
        }

        private int _idTypeEstate;

        public RealEstateViewModel(int idTypeEstate = 0)
        {
            _realEstateService = new RealEstateService();
            _idTypeEstate = idTypeEstate;
            LoadRealEstates();
        }

        public void LoadRealEstates()
        {
            if (_idTypeEstate == 0)
                RealEstates = new ObservableCollection<RealEstate>(_realEstateService.GetAllRealEstates());
            else
                RealEstates = new ObservableCollection<RealEstate>(_realEstateService.GetRealEstatesByType(_idTypeEstate));
        }

        public void DeleteRealEstate(int idEstate)
        {
            _realEstateService.DeleteRealEstate(idEstate);
            LoadRealEstates();
        }

        public void SearchRealEstates(string query)
        {
            RealEstates = new ObservableCollection<RealEstate>(_realEstateService.SearchRealEstates(query));
        }

        public void FilterRealEstates(int? idType, string? reference, string? city, decimal? maxPrice, decimal? minSurface)
        {
            _realEstateService.FilterRealEstates(idType, reference, city, maxPrice, minSurface);
            RealEstates = new ObservableCollection<RealEstate>(
                _realEstateService.FilterRealEstates(idType, reference, city, maxPrice, minSurface));
        }
    }
}
