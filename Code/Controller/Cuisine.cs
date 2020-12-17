using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe cuisine (la cuisine)
    public class Cuisine : Salle
    {
        //La cuisine n'étant pas implémentée (soucis de temps), la classe n'est pas complète
        public Cuisine() : base() { }

        public Cuisine(int width, int height, int[] position, World world) : base(width, height, position, world)
        {

        }

        public void test()
        {
            Console.WriteLine("erererer");
        }

        public string créationCuisine(int a)
        {
            return "Cuisine";
        }
    }
}
