using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmoApp.DataAccess.Models;

namespace ImmoApp.BLL.Services
{
    public class EstateImageService
    {
        public List<EstateImage> GetImagesByEstate(int idEstate)
        {
            using (var context = new ImmoDbContext())
            {
                return context.EstateImages
                    .Where(i => i.IdEstate == idEstate)
                    .ToList();
            }
        }

        public void AddImage(int idEstate, string imagePath, bool isMain = false)
        {
            using (var context = new ImmoDbContext())
            {
                var image = new EstateImage
                {
                    IdEstate = idEstate,
                    ImagePath = imagePath,
                    IsMain = isMain
                };
                context.EstateImages.Add(image);
                context.SaveChanges();
            }
        }

        public void DeleteImage(int idImage)
        {
            using (var context = new ImmoDbContext())
            {
                var image = context.EstateImages.FirstOrDefault(i => i.IdImage == idImage);
                if (image == null)
                    throw new Exception("Image non trouvée");
                context.EstateImages.Remove(image);
                context.SaveChanges();
            }
        }

        public void SetMainImage(int idEstate, int idImage)
        {
            using (var context = new ImmoDbContext())
            {
                // On retire le statut principal de toutes les images
                var images = context.EstateImages.Where(i => i.IdEstate == idEstate).ToList();
                foreach (var img in images)
                    img.IsMain = false;

                // On met la nouvelle image principale
                var mainImage = images.FirstOrDefault(i => i.IdImage == idImage);
                if (mainImage != null)
                    mainImage.IsMain = true;

                context.SaveChanges();
            }
        }
    }
}