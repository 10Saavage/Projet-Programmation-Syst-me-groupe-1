using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Instancie l'ensemble du world pour que tout soit prêt
namespace Controller
{
    public class Controller
    {
        public World LoadWorld(int[] worldSize, int[] restaurant, int[] comptoir, int[] hall, int nbreTables, int nbreCR, int nbreServ, Delegate deleg)
        {

            World world = new World(20, 20);
            world.GetCore().setDeleg(deleg);

            world.InstantiateRestaurant(restaurant[0], restaurant[1], new int[] { restaurant[2], restaurant[3] });
            world.InstantiateHall(hall[0], hall[1], new int[] { hall[2], hall[3] });
            world.InstantiateComptoir(comptoir[0], comptoir[1], new int[] { comptoir[2], comptoir[3] }, new int[] { comptoir[4], comptoir[5] }, new int[] { comptoir[6], comptoir[7] });

            //Menu de test
            List<List<String>> menu = new List<List<string>>()
            {
                new List<String>{
                    "entree1", "plat1", "dessert1"
                },
                new List<String>{
                    "entree2", "plat2", "dessert2"
                }
            };

            List<int[]> tablesCoords = new List<int[]>();
            int inc = 0;
            int xinc = 0;

            Console.WriteLine(restaurant[0]);

            for (int i = 0; i < nbreTables; i++)
            {
                Console.WriteLine(i);
                if (xinc < restaurant[0])
                {
                    tablesCoords.Add(new int[] { xinc, inc });
                }
                else
                {
                    xinc = 0;
                    inc++;
                    tablesCoords.Add(new int[] { xinc, inc });
                }
                xinc++;
            }

            world.getRestaurant().GenerateTables(tablesCoords);

            world.GetComptoir().LoadCarte(new Carte(world.GetComptoir().stockCartes, world.GetLoader(), menu));


            //world.getRestaurant().GenerateTables(new int[,] { { 1, 1 }, { 2, 2 } });

            MaitreHotel maitre = new MaitreHotel("Eugene", "Baskiez", 60, new int[] { 15, 15 }, world.GetLoader());

            for (int i = 0; i < nbreCR; i++)
            {
                new ChefDeRang("Eugene", "Baskiez", 60, new int[] { 15, 15 }, world.GetLoader(), maitre);
            }

            for (int i = 0; i < nbreServ; i++)
            {
                new Serveur("Robert", "Baskiez", 60, new int[] { 15, 15 }, world.GetLoader());
            }

            //MaitreHotel maitre = new MaitreHotel("Eugene", "Baskiez", 60, new int[] { 15, 15 }, world.GetLoader());
            //ChefDeRang chefDeRang = new ChefDeRang("Robert", "Baskiez", 60, new int[] { 15, 15 }, world.GetLoader(), maitre);
            //ChefDeRang chefDeRang2 = new ChefDeRang("Jean", "Baskiez", 60, new int[] { 15, 15 }, world.GetLoader(), maitre);

            //Serveur serveur = new Serveur("Albert", "Baskiez", 60, new int[] { 15, 15 }, world.GetLoader());
            //Serveur serveur2 = new Serveur("Paul", "Baskiez", 60, new int[] { 15, 15 }, world.GetLoader());

            return world;

        }
    }
}
