using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
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
}
