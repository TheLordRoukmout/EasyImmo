using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;
using System.Collections.ObjectModel;

namespace ImmoAppUI.ViewModels
{
    public class RealEstateDetailViewModel : BaseViewModel
    {
        private readonly RealEstateService _realEstateService;
        private readonly EstateImageService _imageService;
        private readonly EstateStatusService _statusService;
        private readonly EventService _eventService;


        // Propriétés du bien
        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string? _reference;
        public string? Reference
        {
            get => _reference;
            set => SetProperty(ref _reference, value);
        }

        private decimal? _price;
        public decimal? Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private decimal? _surface;
        public decimal? Surface
        {
            get => _surface;
            set => SetProperty(ref _surface, value);
        }

        private string? _address;
        public string? Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private string? _city;
        public string? City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private string? _description;
        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private Client? _owner;
        public Client? Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        // Images
        private ObservableCollection<EstateImage> _images = new();
        public ObservableCollection<EstateImage> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        private bool _hasNoImages;
        public bool HasNoImages
        {
            get => _hasNoImages;
            set => SetProperty(ref _hasNoImages, value);
        }

        // Statuts
        private string? _currentStatus;
        public string? CurrentStatus
        {
            get => _currentStatus;
            set => SetProperty(ref _currentStatus, value);
        }

        private ObservableCollection<EstateStatusHistory> _statusHistory = new();
        public ObservableCollection<EstateStatusHistory> StatusHistory
        {
            get => _statusHistory;
            set => SetProperty(ref _statusHistory, value);
        }

        private ObservableCollection<TypeStatusOffer> _allStatuses = new();
        public ObservableCollection<TypeStatusOffer> AllStatuses
        {
            get => _allStatuses;
            set => SetProperty(ref _allStatuses, value);
        }

        public RealEstateDetailViewModel(int idEstate)
        {
            _realEstateService = new RealEstateService();
            _imageService = new EstateImageService();
            _statusService = new EstateStatusService();
            _eventService = new EventService();
            LoadEstate(idEstate);
            LoadImages(idEstate);
            LoadStatus(idEstate);
            LoadEvents(idEstate);
        }

        private void LoadEstate(int idEstate)
        {
            var estate = _realEstateService.GetRealEstateById(idEstate);
            if (estate != null)
            {
                Title = estate.Title;
                Reference = estate.Reference;
                Price = estate.Price;
                Surface = estate.Surface;
                Address = estate.Address;
                City = estate.City;
                Description = estate.Description;
                Owner = estate.IdOwnerNavigation;
            }
        }

        public void LoadImages(int idEstate)
        {
            var images = _imageService.GetImagesByEstate(idEstate);
            Images = new ObservableCollection<EstateImage>(images);
            HasNoImages = Images.Count == 0;
        }

        public void LoadStatus(int idEstate)
        {
            var current = _statusService.GetCurrentStatus(idEstate);
            CurrentStatus = current?.IdStatusOfferNavigation?.Label ?? "Aucun statut";

            var history = _statusService.GetStatusHistory(idEstate);
            StatusHistory = new ObservableCollection<EstateStatusHistory>(history);

            var statuses = _statusService.GetAllStatuses();
            AllStatuses = new ObservableCollection<TypeStatusOffer>(statuses);
        }

        public void ChangeStatus(int idEstate, int idNewStatus)
        {
            _statusService.ChangeStatus(idEstate, idNewStatus);
            LoadStatus(idEstate);
        }

        private ObservableCollection<Event> _events = new();
        public ObservableCollection<Event> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        public void LoadEvents(int idEstate)
        {
            var events = _eventService.GetEventsByEstate(idEstate);
            Events = new ObservableCollection<Event>(events);
        }
    }
}