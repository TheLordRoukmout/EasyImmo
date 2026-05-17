using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImmoApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ImmoApp.BLL.Services
{
    public class EventService
    {
        public List<Event> GetEventsByEstate(int idEstate)
        {
            using (var context = new ImmoDbContext())
            {
                return context.Events
                    .Include(e => e.IdTypeEventNavigation)
                    .Include(e => e.ClientEvents)
                        .ThenInclude(ce => ce.IdClientNavigation)
                    .Where(e => e.IdEstate == idEstate)
                    .OrderByDescending(e => e.DateEvent)
                    .ToList();
            }
        }

        public List<TypeEvent> GetAllTypeEvents()
        {
            using (var context = new ImmoDbContext())
            {
                return context.TypeEvents.ToList();
            }
        }

        public void AddEvent(int idEstate, int idTypeEvent, DateTime dateEvent, string? notes, int idClient, string? role)
        {
            if (dateEvent == default)
                throw new Exception("La date est obligatoire");

            using (var context = new ImmoDbContext())
            {
                var newEvent = new Event
                {
                    IdEstate = idEstate,
                    IdTypeEvent = idTypeEvent,
                    DateEvent = dateEvent,
                    Notes = notes
                };

                context.Events.Add(newEvent);
                context.SaveChanges();

                // Lier le client à l'événement
                var clientEvent = new ClientEvent
                {
                    IdEvent = newEvent.IdEvent,
                    IdClient = idClient,
                    RoleInEvent = role
                };

                context.ClientEvents.Add(clientEvent);
                context.SaveChanges();
            }
        }

        public void DeleteEvent(int idEvent)
        {
            using (var context = new ImmoDbContext())
            {
                // Supprimer d'abord les liens client_event
                var clientEvents = context.ClientEvents
                    .Where(ce => ce.IdEvent == idEvent)
                    .ToList();
                context.ClientEvents.RemoveRange(clientEvents);

                // Supprimer l'événement
                var ev = context.Events.FirstOrDefault(e => e.IdEvent == idEvent);
                if (ev == null)
                    throw new Exception("Événement non trouvé");

                context.Events.Remove(ev);
                context.SaveChanges();
            }
        }
    }
}