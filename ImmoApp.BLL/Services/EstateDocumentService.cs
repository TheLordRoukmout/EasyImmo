using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmoApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ImmoApp.BLL.Services
{
    public class EstateDocumentService
    {
        public List<EstateDocument> GetDocumentsByEstate(int idEstate)
        {
            using (var context = new ImmoDbContext())
            {
                return context.EstateDocuments
                    .Where(d => d.IdEstate == idEstate)
                    .OrderByDescending(d => d.UploadDate)
                    .ToList();
            }
        }

        public List<EstateDocument> GetAllDocuments()
        {
            using (var context = new ImmoDbContext())
            {
                return context.EstateDocuments
                    .Include(d => d.IdEstateNavigation)
                    .OrderByDescending(d => d.UploadDate)
                    .ToList();
            }
        }

        public void AddDocument(int idEstate, string documentName, string documentPath)
        {
            if (string.IsNullOrEmpty(documentName))
                throw new Exception("Le nom du document est obligatoire");

            using (var context = new ImmoDbContext())
            {
                var document = new EstateDocument
                {
                    IdEstate = idEstate,
                    DocumentName = documentName,
                    DocumentPath = documentPath,
                    UploadDate = DateTime.Now
                };
                context.EstateDocuments.Add(document);
                context.SaveChanges();
            }
        }

        public void DeleteDocument(int idDocument)
        {
            using (var context = new ImmoDbContext())
            {
                var document = context.EstateDocuments
                    .FirstOrDefault(d => d.IdDocument == idDocument);
                if (document == null)
                    throw new Exception("Document non trouvé");
                context.EstateDocuments.Remove(document);
                context.SaveChanges();
            }
        }
    }
}