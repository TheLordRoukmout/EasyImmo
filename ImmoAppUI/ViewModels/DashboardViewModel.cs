using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;
using System.Collections.ObjectModel;

namespace ImmoAppUI.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly ClientService _clientService;
        private readonly EventService _eventService;
        private readonly RealEstateService _realEstateService; // ← une seule fois ici

        private int _totalClients;
        public int TotalClients
        {
            get => _totalClients;
            set => SetProperty(ref _totalClients, value);
        }

        private ObservableCollection<Client> _lastClients = new();
        public ObservableCollection<Client> LastClients
        {
            get => _lastClients;
            set => SetProperty(ref _lastClients, value);
        }

        private ObservableCollection<Event> _mondayEvents = new();
        public ObservableCollection<Event> MondayEvents
        {
            get => _mondayEvents;
            set => SetProperty(ref _mondayEvents, value);
        }

        private ObservableCollection<Event> _tuesdayEvents = new();
        public ObservableCollection<Event> TuesdayEvents
        {
            get => _tuesdayEvents;
            set => SetProperty(ref _tuesdayEvents, value);
        }

        private ObservableCollection<Event> _wednesdayEvents = new();
        public ObservableCollection<Event> WednesdayEvents
        {
            get => _wednesdayEvents;
            set => SetProperty(ref _wednesdayEvents, value);
        }

        private ObservableCollection<Event> _thursdayEvents = new();
        public ObservableCollection<Event> ThursdayEvents
        {
            get => _thursdayEvents;
            set => SetProperty(ref _thursdayEvents, value);
        }

        private int _biensVendus;
        public int BiensVendus
        {
            get => _biensVendus;
            set => SetProperty(ref _biensVendus, value);
        }

        private int _biensLoues;
        public int BiensLoues
        {
            get => _biensLoues;
            set => SetProperty(ref _biensLoues, value);
        }

        private Color _biensVendusColor = Colors.Green;
        public Color BiensVendusColor
        {
            get => _biensVendusColor;
            set => SetProperty(ref _biensVendusColor, value);
        }

        private Color _biensLouesColor = Colors.Green;
        public Color BiensLouesColor
        {
            get => _biensLouesColor;
            set => SetProperty(ref _biensLouesColor, value);
        }

        private ObservableCollection<RealEstate> _lastEstates = new();
        public ObservableCollection<RealEstate> LastEstates
        {
            get => _lastEstates;
            set => SetProperty(ref _lastEstates, value);
        }

        public string MondayLabel => GetDayLabel(0);
        public string TuesdayLabel => GetDayLabel(1);
        public string WednesdayLabel => GetDayLabel(2);
        public string ThursdayLabel => GetDayLabel(3);

        public DashboardViewModel()
        {
            _clientService = new ClientService();
            _eventService = new EventService();
            _realEstateService = new RealEstateService();
            LoadStats();
            LoadWeekEvents();
        }

        public void LoadStats()
        {
            TotalClients = _clientService.GetAllClients().Count;
            LastClients = new ObservableCollection<Client>(_clientService.GetLastClients());
            BiensVendus = _realEstateService.GetCountByStatus(4);
            BiensVendusColor = BiensVendus > 0 ? Colors.Green : Colors.Red;
            BiensLoues = _realEstateService.GetCountByStatus(2);
            BiensLouesColor = BiensLoues > 0 ? Colors.Green : Colors.Red;
            LastEstates = new ObservableCollection<RealEstate>(_realEstateService.GetLastRealEstates());
        }

        public void LoadWeekEvents()
        {
            var events = _eventService.GetEventsForCurrentWeek();
            MondayEvents = new ObservableCollection<Event>(
                events.Where(e => e.DateEvent.Date == DateTime.Today));
            TuesdayEvents = new ObservableCollection<Event>(
                events.Where(e => e.DateEvent.Date == DateTime.Today.AddDays(1)));
            WednesdayEvents = new ObservableCollection<Event>(
                events.Where(e => e.DateEvent.Date == DateTime.Today.AddDays(2)));
            ThursdayEvents = new ObservableCollection<Event>(
                events.Where(e => e.DateEvent.Date == DateTime.Today.AddDays(3)));
        }

        private string GetDayLabel(int daysFromToday)
        {
            var date = DateTime.Today.AddDays(daysFromToday);
            return date.ToString("dddd dd/MM/yyyy", new System.Globalization.CultureInfo("fr-FR"));
        }
    }
}