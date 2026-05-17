using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmoApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ImmoApp.BLL.Services
{
    public class RealEstateService
    {
        public List<RealEstate> GetAllRealEstates()
        {
            using (var context = new ImmoDbContext())
            {
                return context.RealEstates
                    .Include(r => r.IdTypeEstateNavigation)
                    .Include(r => r.IdOwnerNavigation)
                    .Include(r => r.EstateImages)
                    .ToList();
            }
        }

        public List<RealEstate> GetRealEstatesByType(int idTypeEstate)
        {
            using (var context = new ImmoDbContext())
            {
                return context.RealEstates
                    .Where(r => r.IdTypeEstate == idTypeEstate)
                    .Include(r => r.IdTypeEstateNavigation)
                    .Include(r => r.IdOwnerNavigation)
                    .Include(r => r.EstateImages)
                    .ToList();
            }
        }

        public RealEstate? GetRealEstateById(int idEstate)
        {
            using (var context = new ImmoDbContext())
            {
                return context.RealEstates
                    .Include(r => r.IdTypeEstateNavigation)
                    .Include(r => r.IdOwnerNavigation)
                    .FirstOrDefault(r => r.IdEstate == idEstate);
            }
        }

        public void AddRealEstate(string title, string reference, string address, string city, string postalCode, decimal price, decimal surface, string description, int idTypeEstate, int idOwner)
        {
            if (string.IsNullOrEmpty(title))
                throw new Exception("Le titre est obligatoire");
            if (string.IsNullOrEmpty(reference))
                throw new Exception("La référence est obligatoire");
            if (price <= 0)
                throw new Exception("Le prix doit être positif");

            using (var context = new ImmoDbContext())
            {
                bool referenceExists = context.RealEstates.Any(r => r.Reference == reference);
                if (referenceExists)
                    throw new Exception("Cette référence existe déjà");

                var estate = new RealEstate
                {
                    Title = title,
                    Reference = reference,
                    Address = address,
                    City = city,
                    PostalCode = postalCode,
                    Price = price,
                    Surface = surface,
                    Description = description,
                    IdTypeEstate = idTypeEstate,
                    IdOwner = idOwner,
                    CreationDate = DateTime.Now
                };

                context.RealEstates.Add(estate);
                context.SaveChanges();
            }
        }

        public void UpdateRealEstate(int idEstate, string title, string address, string city, string postalCode, decimal price, decimal surface, string description)
        {
            if (string.IsNullOrEmpty(title))
                throw new Exception("Le titre est obligatoire");
            if (price <= 0)
                throw new Exception("Le prix doit être positif");

            using (var context = new ImmoDbContext())
            {
                var estate = context.RealEstates.FirstOrDefault(r => r.IdEstate == idEstate);
                if (estate == null)
                    throw new Exception("Bien non trouvé");

                estate.Title = title;
                estate.Address = address;
                estate.City = city;
                estate.PostalCode = postalCode;
                estate.Price = price;
                estate.Surface = surface;
                estate.Description = description;

                context.SaveChanges();
            }
        }

        public void DeleteRealEstate(int idEstate)
        {
            using (var context = new ImmoDbContext())
            {
                var estate = context.RealEstates.FirstOrDefault(r => r.IdEstate == idEstate);
                if (estate == null)
                    throw new Exception("Bien non trouvé");

                context.RealEstates.Remove(estate);
                context.SaveChanges();
            }
        }

        public List<RealEstate> SearchRealEstates(string query)
        {
            using (var context = new ImmoDbContext())
            {
                return context.RealEstates
                    .Where(r => r.Title.Contains(query) || r.City.Contains(query) || r.Reference.Contains(query))
                    .Include(r => r.IdTypeEstateNavigation)
                    .ToList();
            }
        }

        public List<RealEstate> FilterRealEstates(int? idType, string? reference, string? city, decimal? maxPrice, decimal? minSurface)
        {
            using (var context = new ImmoDbContext())
            {
                var query = context.RealEstates
                    .Include(r => r.IdTypeEstateNavigation)
                    .Include(r => r.IdOwnerNavigation)
                    .Include(r => r.EstateImages)
                    .AsQueryable();

                if (idType.HasValue)
                    query = query.Where(r => r.IdTypeEstate == idType.Value);
                if (!string.IsNullOrEmpty(reference))
                    query = query.Where(r => r.Reference.Contains(reference));
                if (!string.IsNullOrEmpty(city))
                    query = query.Where(r => r.City.Contains(city));
                if (maxPrice.HasValue)
                    query = query.Where(r => r.Price <= maxPrice.Value);
                if (minSurface.HasValue)
                    query = query.Where(r => r.Surface >= minSurface.Value);

                return query.ToList();
            }
        }
    }
}
