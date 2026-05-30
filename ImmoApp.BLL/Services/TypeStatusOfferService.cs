using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImmoApp.DataAccess.Models;

namespace ImmoApp.BLL.Services
{
    public class TypeStatusOfferService
    {
        public List<TypeStatusOffer> GetAllTypeStatusOffers()
        {
            using (var context = new ImmoDbContext())
            {
                return context.TypeStatusOffers.ToList();
            }
        }
    }
}
