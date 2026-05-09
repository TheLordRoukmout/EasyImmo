using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;

namespace ImmoAppUI.ViewModels
{
    public class AgentViewModel : BaseViewModel
    {
        private readonly AgentService _agentService;

        private ObservableCollection<Agent> _agents = new ObservableCollection<Agent>();
        public ObservableCollection<Agent> Agents
        {
            get => _agents;
            set => SetProperty(ref _agents, value);
        }

        public AgentViewModel()
        {
            _agentService = new AgentService();
            LoadAgents();
        }

        public void LoadAgents()
        {
            var agents = _agentService.GetAllAgents();
            Agents = new ObservableCollection<Agent>(agents);
        }

        public void DeleteAgent(int idAgent)
        {
            _agentService.DeleteAgent(idAgent);
            LoadAgents();
        }
    }
}
