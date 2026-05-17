using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;

namespace ImmoAppUI.ViewModels
{
    public class ClientViewModel : BaseViewModel
    {
        // Le service pour parler à la BLL
        private readonly ClientService _clientService;

        // Les données à afficher (ObservableCollection notifie l'UI automatiquement)
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set => SetProperty(ref _clients, value); // ← notifie l'UI
        }

        // Le constructeur : on initialise le service et on charge les données
        public ClientViewModel()
        {
            _clientService = new ClientService();
            LoadClients();
        }

        //Les méthodes qui appellent le service
        public void LoadClients()
        {
            var clients = _clientService.GetAllClients();
            Clients = new ObservableCollection<Client>(clients);
        }

        public void AddClient(string Lastname, string Firstname, string? Email, string? Phone, string? Address, string? TypeClient)
        {
            _clientService.AddClient(Lastname, Firstname, Email, Phone, Address, TypeClient);
            LoadClients();
        }

        public void UpdateClient(int idClient, string Lastname, string Firstname, string? Email, string? Phone, string? Address, string? TypeClient)
        {
            _clientService.UpdateClient(idClient, Lastname, Firstname, Email, Phone, Address, TypeClient);
            LoadClients();
        }

        public void DeleteClient(int idClient)
        {
            _clientService.DeleteClient(idClient);
            LoadClients();
        }

        public void SearchClients(string query)
        {
            var clients = _clientService.SearchClients(query);
            Clients = new ObservableCollection<Client>(clients);
        }
    }
}
