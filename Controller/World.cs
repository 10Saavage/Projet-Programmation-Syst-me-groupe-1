using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Controller
{
    //Le world dans lequel l'ensemble des entités vont évoluer. Il s'agit d'un tableau de dimensions données, chaque case étant une coordonnée et étant un TableEntity
    public class World
    {
        protected int width;
        protected int height;
        protected TableEntity[,] table;

        private Core core;
        private Loader loader;

        private Cuisine cuisine;
        private Restaurant restaurant;
        private Hall hall;
        private Comptoir comptoir;

        public World(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.core = new Core();
            this.loader = new Loader(core, this);

            this.table = new TableEntity[width,height];

            FillTable(table);
        }

        //Retourne le loader
        public Loader GetLoader()
        {
            return loader;
        }

        //Retourne le core
        public Core GetCore()
        {
            return core;
        }

        //Renvois la TableEntity correspondant aux coordonnés données
        public TableEntity GetTableEntity(int[] position)
        {
            if(position[0] < width && position[1] < height)
            {
                return table[position[0], position[1]];
            }
            else
            {
                return null;
            }
            
        }

        //Instancie la cuisine
        public Cuisine InstantiateCuisine(int width, int height, int[] position)
        {
            SetSalle(width, height, position, "Cuisine");
            cuisine = new Cuisine(width, height, position, this);
            return cuisine;
        }

        //Instancie le restaurant
        public Restaurant InstantiateRestaurant(int width, int height, int[] position)
        {
            SetSalle(width, height, position, "Restaurant");
            restaurant = new Restaurant(width, height, position, this);
            return restaurant;
        }

        //Instancie le comptoir
        public Comptoir InstantiateComptoir(int width, int height, int[] position, int[] pointAccesRestaurant, int[] pointAccesCuisine)
        {
            SetSalle(width, height, position, "Comptoir");
            comptoir = new Comptoir(width, height, position, this, pointAccesRestaurant, pointAccesCuisine);
            return comptoir;
        }

        //Instancie le Hall
        public Hall InstantiateHall(int width, int height, int[] position)
        {
            SetSalle(width, height, position, "Hall");
            hall = new Hall(width, height, position, this);
            return hall;
        }

        //Retourne la cuisine
        public Cuisine getCuisine()
        {
            return cuisine;
        }

        //Retourne le restaurant
        public Restaurant getRestaurant()
        {
            return restaurant;
        }

        //Retourne le hall
        public Hall getHall()
        {
            return hall;
        }

        //Retourne le comptoir
        public Comptoir GetComptoir()
        {
            return comptoir;
        }

        //Fonction appelée lorsqu'une des salle est instanciée pour définir le type de salle à ses coordonnées (définis le typesalle des TableEntity)
        private void SetSalle(int width, int height, int[] position, string typeSalle)
        {
            for(int x = position[0]; x < (width + position[0]); x++)
            {
                for (int y = position[1]; y < (height + position[1]); y++)
                {
                    table[x, y].typeSalle = typeSalle;
                }
            }
        }

        //remplis le tableau du world de TableEntity (les instancie)
        private void FillTable(TableEntity[,] table)
        {
            for (int x = 0; x < table.GetLength(0); x++)
            {
                for (int y = 0; y < table.GetLength(1); y++)
                {
                    table[x, y] = new TableEntity();
                }
            }
        }

        //Génère une vue du tableau du world pour une vue (utilisé lors du débug)
        public string[,] GenerateGridView()
        {
            string[,] stringTable = new string[this.table.GetLength(0), this.table.GetLength(1)];

            for (int x = 0; x < this.table.GetLength(0); x++)
            {
                for (int y = 0; y < this.table.GetLength(1); y++)
                {
                    stringTable[x, y] = GetTableEntity(new int[] { x, y }).typeSalle;
                }
            }

            return stringTable;
        }

        //Fonction appelée par la vue qui retourne les informations données d'un élément précisé par son ID
        public List<string[]> GenerateInfosElement(int id)
        {
            return FindElementInWorld(id).ReturnInformations();
        }

        //Retourne un élément à partir de son ID
        public Entity FindElementInWorld(int id)
        {
            for(int i = 0; i < width;i++)
            {
                for (int j = 0; i < height; i++)
                {
                    TableEntity tablentity = GetTableEntity(new int[] { i, j });

                    if(tablentity.HasEntity(id))
                    {
                        return tablentity.GetEntity(id);
                    }
                }
            }
            return null;
        }

        //Renvois l'ensemble des éléments (sous forme de "type + ID") contenus dans la même case qu'un table (pour la vue)
        public List<String[]> GetTableContent(int tableId)
        {
            List<Object> entities = GetTableEntity(restaurant.GetTable(tableId).GetPosition()).GetAllEntities();
            List<String[]> formatEntites = new List<String[]>();

            //Console.WriteLine(restaurant.GetTable(tableId).GetPosition()[0]+" "+ restaurant.GetTable(tableId).GetPosition()[1]);
            foreach(Entity entite in entities)
            {
                //Console.WriteLine(entite.GetType());
                formatEntites.Add(new string[]{ entite.GetType().ToString().Replace("Controller.", "") + " " + entite.id, entite.id.ToString()});
            }

            return formatEntites;
        }

        //Change la vitesse (appelé par la vue)
        public void SetSpeedUp(bool speed)
        {
            core.SetSpeed(speed);
        }

        //Met l'application en pause (appelé par la vue)
        public void SetPause(bool pause)
        {
            core.Pause(pause);
        }
    }

    //Classe abstraite implémentée par toutes les salles (cuisine, restaurant...)
    public abstract class Salle
    {
        public int width;
        public int height;
        public int[] position;
        public World world;

        public Salle()
        {
            this.width = 10;
            this.height = 10;
            this.position = new int[2] { 0, 0 };
        }

        public Salle(int width, int height, int[] position, World world)
        {
            this.width = width;
            this.height = height;

            this.position = position;
            this.world = world;

            Console.WriteLine(position);
        }
    }

    //Classe cuisine (la cuisine)
    public class Cuisine : Salle
    {
        //La cuisine n'étant pas implémentée (soucis de temps), la classe n'est pas complète
        public Cuisine():base() { }

        public Cuisine(int width, int height, int[] position, World world):base(width, height, position, world)
        {

        }

        public void test()
        {
            Console.WriteLine("erererer");
        }
    }

    //Classe restaurant (Salle principale, contenant les tables, et dans laquelle naviguent les chefs de rang et les serveurs)
    public class Restaurant : Salle
    {
        private int tableIdCount;
        private List<Table> tables;

        public Restaurant() { }

        public Restaurant(int width, int height, int[] position, World world) : base(width, height, position, world)
        {
            tableIdCount = 0;
            tables = new List<Table>();
        }

        //Génération des tables à partir d'une liste de coordonnées donnée
        public void GenerateTables(List<int[]> positions)
        {
            foreach(int[] pos in positions)
            {
                if(pos[0] < width && pos[1] < height)
                {
                    Table table = new Table(world.GetLoader(), new int[] { pos[0] + position[0], pos[1] + position[1] }, tableIdCount, 10);
                    world.GetTableEntity(table.GetPosition()).AddEntity(table);
                    tables.Add(table);
                    tableIdCount++;

                    Console.WriteLine("Table générée à: "+ table.GetPosition()[0] + " , "+ table.GetPosition()[1]);
                }
            }
        }

        //Retourne toutes les tables présentes
        public List<Table> GetAllTables()
        {
            return tables;
        }

        //Retourne toutes les tables vides présentes
        public List<Table>GetEmptyTables()
        {
            List<Table> emptyTables = new List<Table>();

            foreach(Table table in tables)
            {
                if(table.EstLibre())
                {
                    emptyTables.Add(table);
                }
            }
            return emptyTables;
        }

        //Retourne toutes les tables contenant des clients présentes
        public List<Table>GetFilledTables()
        {
            List<Table> filledTables = new List<Table>();

            foreach (Table table in tables)
            {
                if (table.GetPlacesLibres() < table.GetNombrePlaces())
                {
                    filledTables.Add(table);
                }
            }
            return filledTables;
        }

        //Retourne toutes les tables réservées
        public List<Table>GetReservedTables()
        {
            List<Table> reservedTables = new List<Table>();

            foreach (Table table in tables)
            {
                if (table.EstLibre() == false)
                {
                    reservedTables.Add(table);
                }
            }
            return reservedTables;
        }

        //Retourne une table à partir de son TableId (ID propre aux tables)
        public Table GetTable(int tableId)
        {
            return tables.Find(t => t.GetNumTable() == tableId);
        }
    }

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

            foreach(Object objet in platsEnStock)
            {
                if(objet.GetType().ToString() == "Controller.Plat")
                {
                    if(((Plat)objet).nomPlat == plat)
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
            foreach(List<List<String>> commandeListe in stockCommandes.GetContent())
            {
                foreach(List<String> commande in commandeListe)
                {
                    foreach(String plat in commande)
                    {
                        stockPlats.AddContent(new Plat(plat, 0, new ObjetConteneur(stockPlats, world.GetLoader(), "assiette"), stockPlats, world.GetLoader()));
                    }
                }
            }
        }
    }

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

            for(int i = 0; i < rnd.Next(1,4); i++)
            {
                clients.Add(new Client(rnd.Next(30, 100), rnd.Next(1, 4), "Jean", "Albert", rnd.Next(20, 80), this.position, loader));
            }

            return clients;
        }
    }

    //Classe TableEntity, classe instanciée dans chaque chaque case tu tableau du world, et qui va contenir les entités présentes dans chaque case
    public class TableEntity
    {
        public string typeSalle;
        private List<Object> entities;

        public TableEntity()
        {
            entities = new List<Object>();
        }

        //Retourne toutes les entités présentes dans cette case (donc cette TableEntity)
        public List<Object> GetAllEntities()
        {
            return entities;
        }

        //Ajoute une entité à la liste des entités présentes dans la case
        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        //Retire une entité de la liste des entités présentes dans la case
        public bool RemoveEntity(Entity entity)
        {
            return entities.Remove(entity);
        }

        //Retourne toutes les entités présents dans la case d'un certain type donné
        public List<Object> FindEntityByType(string type)
        {
            return entities.FindAll(e => ((Entity)e).GetType().ToString() == type);
        }

        //Retourne si oui ou non la case contiens une entité identifiée par son ID
        public bool HasEntity(int entityID)
        {
            int result = entities.FindIndex(e => ((Entity)e).id == entityID);

            if(result > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Retourne une entité identifiée par son ID, si elle est présente dans cette case
        public Entity GetEntity(int id)
        {
            return (Entity)entities.Find(e => ((Entity)e).id == id);
        }
    }

}
