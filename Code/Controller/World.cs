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
