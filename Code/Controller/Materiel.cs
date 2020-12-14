using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe de matériel (élément étant utilisé et pouvant être propre ou sale, comme les couverts, les assiettes, les nappes...)
    public class Materiel : Objet
    {
        private bool propre;

        public Materiel(Entity conteneur, Loader loader, bool propre = true) : base(conteneur, loader)
        {
            this.propre = propre;
        }

        public override List<string[]> ReturnInformations()
        {
            List<string[]> infosToReturn = new List<string[]>();

            infosToReturn.Add(new string[] { "ID", id.ToString() });
            infosToReturn.Add(new string[] { "Propre", propre.ToString() });

            return infosToReturn;
        }

        public void Salir()
        {
            propre = false;
        }

        public void Nettoyer()
        {
            propre = true;
        }

        public bool GetProprete()
        {
            return propre;
        }
    }
}
