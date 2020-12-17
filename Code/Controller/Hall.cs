using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Le Hall est le hall d'entrée, dans lequel se situe le maître d'hôtel et où les clients vont arriver (génération aléatoire)
    public class Hall : Salle
    {
        public Hall() { }

        public Hall(int width, int height, int[] position, World world) : base(width, height, position, world)
        {

        }

        //Fonction de génération de clients aléatoire, appelée par le maître d'hôtel
        public List<Client> GenerateClients(Loader loader)
        {
            Console.WriteLine("Génération de clients !");
            Random rnd = new Random();
            List<Client> clients = new List<Client>();

            for (int i = 0; i < rnd.Next(1, 4); i++)
            {
                clients.Add(new Client(rnd.Next(30, 100), rnd.Next(1, 4), "Jean", "Albert", rnd.Next(20, 80), this.position, loader));
            }

            return clients;
        }

        public string créationHall(int a)
        {
            return "Hall";
        }
    }
}
