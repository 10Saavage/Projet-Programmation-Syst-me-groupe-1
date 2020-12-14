using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe Maitre Hotel, qui va reçevoir les clients
    public class MaitreHotel : PersonnelSalle
    {
        private int randomCountDown;
        private Random rnd;
        private List<List<Client>> clientsAttente;
        private Hall hall;
        private Restaurant restaurant;
        private List<ChefDeRang> chefsDeRang;

        public MaitreHotel(string nom, string prenom, int age, int[] position, Loader loader) : base(nom, prenom, age, position, loader)
        {
            rnd = new Random();
            randomCountDown = 0;
            this.hall = loader.GetWorld().getHall();
            this.restaurant = loader.GetWorld().getRestaurant();
            this.clientsAttente = new List<List<Client>>();
            this.chefsDeRang = new List<ChefDeRang>();
        }

        //Fonction appelée à chaque tic, rendant le personnage dynamique, dans ce cas cela lui permet de générer des client et de les reçevoir
        public override void UpdatePersonnage(int delta)
        {
            //Console.WriteLine(randomCountDown);

            if (randomCountDown <= 0)
            {
                clientsAttente.Add(hall.GenerateClients(loader));
                randomCountDown = rnd.Next(10, 20);
            }
            else
            {
                randomCountDown -= 1;
            }

            if (clientsAttente.Count > 0)
            {
                if (RecieveClients(clientsAttente[0]))
                {
                    clientsAttente.RemoveAt(0);
                }
            }
        }

        //Fonction de retour d'informations pour la vue
        public override List<string[]> ReturnInformations()
        {
            List<string[]> infosToReturn = new List<string[]>();

            infosToReturn.Add(new string[] { "ID", id.ToString() });
            infosToReturn.Add(new string[] { "Clients en attente", clientsAttente.Count().ToString() });

            return infosToReturn;
        }

        //Ajout d'un chef de rang à la liste des chefs de rang
        public void AddChefDeRang(ChefDeRang chefDeRang)
        {
            chefsDeRang.Add(chefDeRang);
        }

        //Le maitre d'hôtel reçois les clients, c'est-à-dire qu'il cherche les tables libres et leur attribue une table qui contiens assez de place
        private bool RecieveClients(List<Client> clients)
        {
            int quantity = clients.Count();
            List<Table> tables = restaurant.GetEmptyTables();
            if (tables.Count() > 0)
            {
                Table designatedTable = tables[0];
                if (designatedTable.Reserver())
                {
                    foreach (Client client in clients)
                    {
                        client.RecevoirMaitre(this);
                        //client.RecevoirTable(designatedTable);
                    }
                    AlertChefDeRang(clients, designatedTable);
                    return true;
                }
            }
            return false;
        }

        //Appel d'un chef de rang pour diriger un groupe de clients vers une table désignée
        private bool AlertChefDeRang(List<Client> clients, Table table)
        {
            foreach (ChefDeRang chef in chefsDeRang)
            {
                if (chef.GetEtat() == "idle")
                {
                    chef.AddClientsToRecieve(clients, table);
                    Console.WriteLine("MH " + id + " alerte CR " + chef.id + " pour reçevoir clients pour table " + table.GetNumTable());
                    return true;
                }
            }
            chefsDeRang[0].AddClientsToRecieve(clients, table);
            Console.WriteLine("MH " + id + " alerte CR " + chefsDeRang[0].id + " pour reçevoir clients pour table " + table.GetNumTable());
            return false;
        }

        //Fonction appelée lorsqu'un client pars, pour détruire l'instance client en question
        public void QuitClient(Client client)
        {
            if (client.table.GetPlacesLibres() == client.table.GetNombrePlaces())
            {
                client.table.Liberer();
            }
        }
    }
    
}
