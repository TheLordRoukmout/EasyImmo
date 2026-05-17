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
    public class DashboardViewModel : BaseViewModel
    {
        private readonly ClientService _clientService;

        private int _totalClients;
        public int TotalClients
        {
            get => _totalClients;
            set => SetProperty(ref _totalClients, value);
        }

        private ObservableCollection<Client> _lastClients = new ObservableCollection<Client>();
        public ObservableCollection<Client> LastClients
        {
            get => _lastClients;
            set => SetProperty(ref _lastClients, value);
        }

        public DashboardViewModel()
        {
            _clientService = new ClientService();
            LoadStats();
        }

        public void LoadStats()
        {
            TotalClients = _clientService.GetAllClients().Count;
            LastClients = new ObservableCollection<Client>(_clientService.GetLastClients());
        }
    }
}