using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmoApp.DataAccess.Models;

namespace ImmoApp.BLL.Services
{
    public class TypeEstateService
    {
        public List<TypeEstate> GetAllTypeEstates()
        {
            using (var context = new ImmoDbContext())
            {
                return context.TypeEstates.ToList();
            }
        }
    }
}
