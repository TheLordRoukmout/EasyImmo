using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImmoApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ImmoApp.BLL.Services
{
    public class ClientService
    {
        public List<Client> GetAllClients()
        {
            using (var context = new ImmoDbContext())
            {
                return context.Clients.ToList();
            }
        }

        public Client? GetClientById(int idClient)
        {
            using (var context = new ImmoDbContext())
            {
                return context.Clients.FirstOrDefault(c => c.IdClient == idClient);
            }
        }


        public void AddClient(string Lastname, string Firstname, string? Email, string? Phone, string? Address, string? TypeClient)
        {
            if (string.IsNullOrEmpty(Lastname))
                throw new Exception("Le nom de famille du client est obligatoire.");
            if (string.IsNullOrEmpty(Firstname))
                throw new Exception("Le prénom du client est obligatoire");

            using (var context = new ImmoDbContext())
            {
                bool ClientExists = context.Clients.Any(c => c.Lastname == Lastname && c.Firstname == Firstname && c.Email == Email);
                if (ClientExists)
                    throw new Exception("Ce client existe déjà");

                var client = new Client
                {
                    Lastname = Lastname,
                    Firstname = Firstname,
                    Email = Email,
                    Phone = Phone,
                    Address = Address,
                    TypeClient = TypeClient
                };
                context.Clients.Add(client);
                context.SaveChanges();
            }
        }


        public void DeleteClient(int idClient)
        {
            using (var context = new ImmoDbContext())
            {
                var client = context.Clients.FirstOrDefault(c => c.IdClient == idClient);
                if (client == null)
                    throw new Exception("Client non trouvé");
                context.Clients.Remove(client);
                context.SaveChanges();
            }
        }

        public void UpdateClient(int idClient, string Lastname, string Firstname, string? Email, string? Phone, string? Address, string? TypeClient)
        {
            if (string.IsNullOrEmpty(Lastname) || string.IsNullOrEmpty(Firstname))
                throw new Exception("Le nom et prénom sont obligatoire");

            using (var context = new ImmoDbContext())
            {
                // Trouver le client
                var client = context.Clients.FirstOrDefault(c => c.IdClient == idClient);
                if (client == null)
                    throw new Exception("Client non trouvé");

                // 2. Modifier ses propriétés
                client.Lastname = Lastname;
                client.Firstname = Firstname;
                client.Email = Email;
                client.Phone = Phone;
                client.Address = Address;
                client.TypeClient = TypeClient;

                // 3. Sauvegarder
                context.SaveChanges();
            }
        }

        // Métode pour lsiter les 20n derniers clients dans le dashboard
        public List<Client> GetLastClients()
        {
            using (var context = new ImmoDbContext())
            {
                return context.Clients
                    .OrderByDescending(c => c.IdClient)
                    .Take(20)
                    .ToList();
            }
        }

        //Fonction pour la barre de recherche de client
        public List<Client> SearchClients(string query)
        {
            using (var context = new ImmoDbContext())
            {
                return context.Clients
                    .Where(c => c.Firstname.Contains(query) || c.Lastname.Contains(query))
                    .ToList();
            }
        }
    }
}
