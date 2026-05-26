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
                    .Include(e => e.IdAgentNavigation)
                        .ThenInclude(a => a.IdUserNavigation)
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

        public void AddEvent(int idEstate, int idTypeEvent, DateTime dateEvent, string? notes, int idClient, string? role, int? idAgent)
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
                    Notes = notes,
                    IdAgent = idAgent
                };

                context.Events.Add(newEvent);
                context.SaveChanges();

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

        public List<Event> GetEventsForCurrentWeek()
        {
            using (var context = new ImmoDbContext())
            {
                var startOfWeek = DateTime.Today;
                var endOfWeek = startOfWeek.AddDays(4);

                return context.Events
                    .Include(e => e.IdTypeEventNavigation)
                    .Include(e => e.IdEstateNavigation)
                    .Where(e => e.DateEvent >= startOfWeek && e.DateEvent <= endOfWeek)
                    .OrderBy(e => e.DateEvent)
                    .ToList();
            }
        }

        public List<Event> GetEventsByMonth(int year, int month)
        {
            using (var context = new ImmoDbContext())
            {
                var startOfMonth = new DateTime(year, month, 1);
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                return context.Events
                    .Include(e => e.IdTypeEventNavigation)
                    .Include(e => e.IdEstateNavigation)
                    .Include(e => e.IdAgentNavigation)
                        .ThenInclude(a => a.IdUserNavigation)
                    .Include(e => e.ClientEvents)
                        .ThenInclude(ce => ce.IdClientNavigation)
                    .Where(e => e.DateEvent >= startOfMonth && e.DateEvent <= endOfMonth)
                    .OrderBy(e => e.DateEvent)
                    .ToList();
            }
        }
    }
}