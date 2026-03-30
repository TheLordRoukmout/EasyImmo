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
        private readonly ImmoDbContext _context;

        public ClientService(ImmoDbContext context)
        {
            _context = context;
        }

        public List<Client> GetAllClients()
        {
            return _context.Clients.ToList();
        }

        public Client? GetClienById(int id)
        {
            return _context.Clients.Find(id);
        }

        public void AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }
    }
}
