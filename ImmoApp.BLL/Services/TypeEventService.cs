using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImmoApp.DataAccess.Models;

namespace ImmoApp.BLL.Services
{
    public class TypeEventService
    {
        public List<TypeEvent> GetAllTypeEvents()
        {
            using (var context = new ImmoDbContext())
            {
                return context.TypeEvents.ToList();
            }
        }
    }
}
