using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
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
            foreach (int[] pos in positions)
            {
                if (pos[0] < width && pos[1] < height)
                {
                    Table table = new Table(world.GetLoader(), new int[] { pos[0] + position[0], pos[1] + position[1] }, tableIdCount, 10);
                    world.GetTableEntity(table.GetPosition()).AddEntity(table);
                    tables.Add(table);
                    tableIdCount++;

                    Console.WriteLine("Table générée à: " + table.GetPosition()[0] + " , " + table.GetPosition()[1]);
                }
            }
        }

        //Retourne toutes les tables présentes
        public List<Table> GetAllTables()
        {
            return tables;
        }

        //Retourne toutes les tables vides présentes
        public List<Table> GetEmptyTables()
        {
            List<Table> emptyTables = new List<Table>();

            foreach (Table table in tables)
            {
                if (table.EstLibre())
                {
                    emptyTables.Add(table);
                }
            }
            return emptyTables;
        }

        //Retourne toutes les tables contenant des clients présentes
        public List<Table> GetFilledTables()
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
        public List<Table> GetReservedTables()
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

        public string TrouverTableRestaurant()
        {
            return "Restaurant";
        }
    } 
}
