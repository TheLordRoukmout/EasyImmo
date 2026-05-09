using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;

namespace ImmoApp.TestConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Db Context
            //var context = new ImmoDbContext();

            //Création service Client
            //var clientService = new ClientService(context);


            // Test de la méthodé  GetAllClients()
            //Console.WriteLine("Liste des clients: ");
            //var clients = clientService.GetAllClients();
            //foreach (var client in clients)
            //{
            //    Console.WriteLine($"ID: {client.IdClient} - {client.Firstname} - {client.Lastname}");
            //}

            //Test de la méthode 2 GetClienById
            //Console.WriteLine("Client Par ID");
            //var oneClient = clientService.GetClienById(1);
            //if (oneClient != null)
            //{
            //    Console.WriteLine($"{oneClient.Firstname} - {oneClient.Lastname}");
            //}
            //else
            //{
            //    Console.WriteLine("Client non trouvé");
            //}


            //Test de la méthode 3 Ajouter un client
            //Console.WriteLine("Ajout d'un client");

            //Console.WriteLine("Entrez le Prénom: ");
            //string FirstName = Console.ReadLine();

            //Console.WriteLine("Entrez le Nom: ");
            //string LastName = Console.ReadLine();

            //var newClient = new Client
            //{
            //    Firstname = FirstName,
            //    Lastname = LastName
            //};
            //clientService.AddClient(newClient);
            //Console.WriteLine($"Client {newClient.Firstname} {newClient.Lastname} a été rajouté !");


            //var userService = new UserService();
            //userService.CreateUser("John.doe", "test", "agent", "john@test.com", "0473489285");
            //Console.WriteLine("User crée avec succès");

            //var agentService = new AgentService();
            //Console.WriteLine("Entrez le username: ");
            //string username = Console.ReadLine();

            //Console.WriteLine("Entrez le mot de passe: ");
            //string password = Console.ReadLine();

            //Console.WriteLine("Entrez le role: ");
            //string role = Console.ReadLine();

            //Console.WriteLine("Entrez l'email': ");
            //string email = Console.ReadLine();

            //Console.WriteLine("Entrez le n° de tel: ");
            //string phone = Console.ReadLine();

            //Console.WriteLine("Entrez la date d'engagement: (dd/mm/yy)");
            //string hireDateInput = Console.ReadLine();
            //DateOnly? hireDate = DateOnly.Parse(hireDateInput);

            //agentService.CreateAgent(username, password, role, email, phone, hireDate);
            //Console.WriteLine("Agent créer avec succès");

            //var agenctService = new AgentService();
            //var allAgents = agenctService.GetAllAgents();
            //foreach (var agent in allAgents)
            //{
            //    Console.WriteLine($"ID: {agent.IdAgent} - {agent.IdUserNavigation.Username}");
            //}
        }
    }

}
