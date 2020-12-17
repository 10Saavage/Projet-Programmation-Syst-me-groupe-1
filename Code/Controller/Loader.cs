using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Controller
{
    //Classe qui est appelée par tout élément instancié, qui donne un ID pour chaque élément et fait office de passerelle
    public class Loader
    {
        private int IDCount;
        private Core core;
        private World world;

        public Loader(Core core, World world)
        {
            IDCount = 0;
            this.core = core;
            this.world = world;
        }

        public int GiveID()
        {
            return IDCount++;
        }

        public Core GetCore()
        {
            return core;
        }

        public World GetWorld()
        {
            return world;
        }
    }
}
