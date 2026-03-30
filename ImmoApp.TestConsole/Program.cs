using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;

namespace ImmoApp.TestConsole
{
    public class Program
    {
        static void Main(string[] args)
        { 
            //Db Context
            var context = new ImmoDbContext();

            //Création service Client
            var clientService = new ClientService(context);


            // Test de la méthodé  GetAllClients()
            Console.WriteLine("Liste des clients: ");
            var clients = clientService.GetAllClients();
            foreach (var client in clients)
            {
                Console.WriteLine($"ID: {client.IdClient} - {client.Firstname} - {client.Lastname}");
            }

            //Test de la méthode 2 GetClienById
            Console.WriteLine("Client Par ID");
            var oneClient = clientService.GetClienById(1);
            if (oneClient != null)
            {
                Console.WriteLine($"{oneClient.Firstname} - {oneClient.Lastname}");
            }
            else
            {
                Console.WriteLine("Client non trouvé");
            }


            //Test de la méthode 3 Ajouter un client
            Console.WriteLine("Ajout d'un client");

            Console.WriteLine("Entrez le Prénom: ");
            string FirstName = Console.ReadLine();

            Console.WriteLine("Entrez le Nom: ");
            string LastName = Console.ReadLine();

            var newClient = new Client
            {
                Firstname = FirstName,
                Lastname = LastName
            };
            clientService.AddClient(newClient);
            Console.WriteLine($"Client {newClient.Firstname} {newClient.Lastname} a été rajouté !");
        }
    }

}
