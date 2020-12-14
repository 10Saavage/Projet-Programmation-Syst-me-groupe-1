using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe comproit (élément de stockage faisant office de transition entre le restaurant et la cuisine)
    public class Comptoir : Salle
    {
        private int[] pointAccesRestaurant;
        private int[] pointAccesCuisine;
        private Carte carte;
        public Stock stockCartes;
        public Stock stockPlats;
        public Stock stockCommandes;

        public Comptoir() { }

        public Comptoir(int width, int height, int[] position, World world, int[] pointAccesRestaurant, int[] pointAccesCuisine) : base(width, height, position, world)
        {
            stockCartes = new Stock(world.GetLoader(), position);
            stockPlats = new Stock(world.GetLoader(), position);
            stockCommandes = new Stock(world.GetLoader(), position);


            if (pointAccesRestaurant[0] < width && pointAccesRestaurant[1] < height)
            {
                this.pointAccesRestaurant = pointAccesRestaurant;
            }
            else
            {
                this.pointAccesRestaurant = new int[] { 0, 0 };
            }

            if (pointAccesCuisine[0] < width && pointAccesCuisine[1] < height)
            {
                this.pointAccesCuisine = pointAccesCuisine;
            }
            else
            {
                this.pointAccesCuisine = new int[] { 0, 0 };
            }
        }

        //Retourne les coordonnées du point d'accès au comptoir du restaurant
        public int[] GetPointAccesRestaurant()
        {
            return pointAccesRestaurant;
        }

        //retourne les coordonnées du point d'accès au comptoir de la cuisine
        public int[] GetPointAccesCuisine()
        {
            return pointAccesCuisine;
        }

        //Charge la carte des menus du restaurant (qui peut changer en fonction des aliments manquants)
        public void LoadCarte(Carte carte)
        {
            this.carte = carte;
        }

        //Retourne la carte des menus
        public Carte GetCarte()
        {
            return carte;
        }

        //retourne un plat en fonction de son nom si celui-ci est présent dans le stock est plats (si la cuisine l'a préparé et déposé dans le comptoir)
        public Plat GetPlat(string plat)
        {
            List<Object> platsEnStock = stockPlats.GetContent();

            foreach (Object objet in platsEnStock)
            {
                if (objet.GetType().ToString() == "Controller.Plat")
                {
                    if (((Plat)objet).nomPlat == plat)
                    {
                        return (Plat)objet;
                    }
                }
            }
            return null;
        }

        //Retourne un bool confirmant si oui ou non le comptoir contiens le plat demandé
        public bool HasPlat(string plat)
        {
            List<Object> platsEnStock = stockPlats.GetContent();

            foreach (Object objet in platsEnStock)
            {
                if (objet.GetType().ToString() == "Controller.Plat")
                {
                    if (((Plat)objet).nomPlat == plat)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Fonction d'ajout de commande au stock des commandes (qui seront récupérées par la cuisine pour les préparer)
        public void AddCommande(List<List<String>> commande)
        {
            stockCommandes.AddContent(commande);
            GeneratePlat();
        }

        //Fonction de conversion de commandes en plats, pour simuler la cuisine le temps que cette dernière soit implémentée
        public void GeneratePlat()
        {
            foreach (List<List<String>> commandeListe in stockCommandes.GetContent())
            {
                foreach (List<String> commande in commandeListe)
                {
                    foreach (String plat in commande)
                    {
                        stockPlats.AddContent(new Plat(plat, 0, new ObjetConteneur(stockPlats, world.GetLoader(), "assiette"), stockPlats, world.GetLoader()));
                    }
                }
            }
        }
    }
}
