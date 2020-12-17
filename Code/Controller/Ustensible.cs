using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe d'ustensiles (couverts, casseroles...)
    public class Ustensile : Materiel
    {
        public string ustensileType;
        public Ustensile(Entity conteneur, Loader loader, string ustensileType, bool propre = true) : base(conteneur, loader, propre)
        {
            this.ustensileType = ustensileType;
        }

        
    }
}
