using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe abstraite d'entité, implémentée par toutes les autres classes étant des entités dans le world (sauf le world même et les salles)
    public abstract class Entity
    {
        public int id;

        public Entity(Loader loader)
        {
            this.id = loader.GiveID();
        }

        public abstract List<string[]> ReturnInformations();
    }

    //Classe abstraite d'entité physique, c'est-à-dire une entité physiquement présente dans le world (personnage, table, conteneur...). Cette classe exclus donc tous les objets, qui ne sont pas présents physiquement dans le world mais se trouvent toujours dans un conteneur
    public abstract class PhysicalEntity : Entity
    {
        protected int[] position;

        public PhysicalEntity(Loader loader, int[] position) : base(loader)
        {
            this.position = position;
        }

        public int[] GetPosition()
        {
            return position;
        }
    }

    //Classe de conteneur, entité physique ayant un inventaire et pouvant donc contenir d'autres entités
    public abstract class Conteneur : PhysicalEntity
    {
        protected List<Object> contenu;

        public Conteneur(Loader loader, int[] position) : base(loader, position)
        {
            contenu = new List<Object>();
        }

        public void AddContent(Object objet)
        {
            contenu.Add(objet);
        }

        public void DelContent(Object objet)
        {
            contenu.Remove(objet);
        }

        public List<Object> GetContent()
        {
            return contenu;
        }

        public Object FindContent(Predicate<Object> condition)
        {
            return contenu.Find(condition);
        }

        public Object TakeContent(Object objet)
        {
            if(contenu.FindIndex(e => e == objet) > -1)
            {
                DelContent(objet);
                return objet;
            }
            else
            {
                return null;
            }
        }
    }

    //Classe de table. Correspond aux tables présentes dans le world
    public class Table : Conteneur
    {
        private int numTable;
        private int places;
        private List<Client> clientsPresents;
        private bool libre;
        public bool commisReservee;
        public bool serveurReservee;


        public Table(Loader loader, int[] position, int numTable, int places) : base(loader, position)
        {
            this.numTable = numTable;
            libre = true;
            clientsPresents = new List<Client>();
            this.places = places;
        }

        //Retour d'informations pour la vue
        public override List<string[]> ReturnInformations()
        {
            List<string[]> infosToReturn = new List<string[]>();

            infosToReturn.Add(new string[] { "ID", id.ToString() });
            infosToReturn.Add(new string[] { "Numéro table", numTable.ToString() });
            infosToReturn.Add(new string[] { "Places", places.ToString() });
            infosToReturn.Add(new string[] { "Places libres", GetPlacesLibres().ToString() });
            infosToReturn.Add(new string[] { "Places occupées", clientsPresents.Count().ToString() });

            return infosToReturn;
        }

        //Retourne le nombre de places totales
        public int GetNombrePlaces()
        {
            return places;
        }

        //Retourne le nombre de places libres
        public int GetPlacesLibres()
        {
            return places - clientsPresents.Count();
        }

        //Retourne si la table est réservée pour des clients par le Maitre d'hôtel ou non
        public bool EstLibre()
        {
            return libre;
        }

        //Ajoute des clients (lorsqu'ils s'aseoient)
        public void AjouterClient(Client client)
        {
            Console.WriteLine("Un client ajouté à la table " + numTable);
            clientsPresents.Add(client);
        }

        //Retire des clients à la liste des clients assis à cette table
        public void RetirerClient(Client client)
        {
            clientsPresents.Remove(client);
        }

        //Appelée par le maître d'hôtel pour la réserver pour des clients qui vont s'y asseoir (pour éviter que le maître d'hôtel attribue deux groupes de clients à une même table)
        public bool Reserver()
        {
            if (libre == false)
            {
                return false;
            }
            else
            {
                Console.WriteLine("Table numéro " + numTable + " réservée !");
                libre = false;
                return true;
            }
        }

        //Libère la réservation de la table. Elle n'est plus réservée
        public bool Liberer()
        {
            if (libre == true)
            {
                return false;
            }
            else
            {
                libre = true;
                return true;
            }
        }

        //Ranvois le numéro de la table. C'est un ID propre aux tables
        public int GetNumTable()
        {
            return numTable;
        }

        //Retourne tous les clients assis à cette table
        public List<Client> GetClients()
        {
            return clientsPresents;
        }
    }

    //Classe de stock, c'est-à-dire un conteneur qui stockera une grande quantité d'éléments (frigo, réserve...)
    public class Stock: Conteneur
    {
        public Stock(Loader loader, int[] position) : base(loader, position)
        {
            
        }

        public override List<string[]> ReturnInformations()
        {
            List<string[]> infosToReturn = new List<string[]>();

            infosToReturn.Add(new string[] { "ID", id.ToString() });
            infosToReturn.Add(new string[] { "Items", contenu.Count().ToString() });

            return infosToReturn;
        }
    }
}
