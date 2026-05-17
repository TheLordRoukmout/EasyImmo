using ImmoApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ImmoApp.BLL.Services
{
    public class EstateStatusService
    {
        public List<TypeStatusOffer> GetAllStatuses()
        {
            using (var context = new ImmoDbContext())
            {
                return context.TypeStatusOffers.ToList();
            }
        }

        public EstateStatusHistory? GetCurrentStatus(int idEstate)
        {
            using (var context = new ImmoDbContext())
            {
                return context.EstateStatusHistories
                    .Include(s => s.IdStatusOfferNavigation)
                    .Where(s => s.IdEstate == idEstate && s.DateEnd == null)
                    .OrderByDescending(s => s.DateStart)
                    .FirstOrDefault();
            }
        }

        public List<EstateStatusHistory> GetStatusHistory(int idEstate)
        {
            using (var context = new ImmoDbContext())
            {
                return context.EstateStatusHistories
                    .Include(s => s.IdStatusOfferNavigation)
                    .Where(s => s.IdEstate == idEstate)
                    .OrderByDescending(s => s.DateStart)
                    .ToList();
            }
        }

        public void ChangeStatus(int idEstate, int idNewStatus)
        {
            using (var context = new ImmoDbContext())
            {
                // On ferme le statut actuel
                var currentStatus = context.EstateStatusHistories
                    .Where(s => s.IdEstate == idEstate && s.DateEnd == null)
                    .FirstOrDefault();

                if (currentStatus != null)
                    currentStatus.DateEnd = DateTime.Now;

                // On crée le nouveau statut
                var newStatus = new EstateStatusHistory
                {
                    IdEstate = idEstate,
                    IdStatusOffer = idNewStatus,
                    DateStart = DateTime.Now,
                    DateEnd = null
                };

                context.EstateStatusHistories.Add(newStatus);
                context.SaveChanges();
            }
        }
    }
}