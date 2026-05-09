using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ImmoApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ImmoApp.BLL.Services
{
    public class AgentService
    {
        public void CreateAgent(string username, string password, string role, string? email, string? phone, DateOnly? HireDate)
        {
            if (HireDate == null)
                throw new Exception("La date d'engagement est obligatoire");



            using (var context = new ImmoDbContext())
            {
                var UserService = new UserService();
                var createdUser = UserService.CreateUser(username, password, role, email, phone);
                var agent = new Agent()
                {
                    IdUser = createdUser.IdUser,
                    HireDate = HireDate,
                    Active = true
                };

                context.Agents.Add(agent);
                context.SaveChanges();
            }
        }

        public List<Agent> GetAllAgents() { 
            using (var context = new ImmoDbContext())
            {
                return context.Agents
                    .Include(a => a.IdUserNavigation)
                    .ToList();
            }
        }

        public void DeleteAgent(int idAgent) {
            using (var context = new ImmoDbContext())
            {
                var agent = context.Agents
                    .Include(a => a.IdUserNavigation)
                    .FirstOrDefault(a => a.IdAgent == idAgent);
                if (agent == null)
                    throw new Exception("Agent non trouvé");
                context.Agents.Remove(agent);
                context.UserProgs.Remove(agent.IdUserNavigation);
                context.SaveChanges();
            }
        }
    }
}
