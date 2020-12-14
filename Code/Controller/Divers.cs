using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe d'objets divers, n'est exploitée que pour l'argent mais peut être implémentée pour n'importe quel autre objet
    public class Divers : Objet
    {
        public string type;
        public int quantity;

        public Divers(string type, Entity conteneur, Loader loader, int quantity = 1) : base(conteneur, loader)
        {
            this.type = type;
            this.quantity = quantity;
        }

        public override List<string[]> ReturnInformations()
        {
            List<string[]> infosToReturn = new List<string[]>();

            infosToReturn.Add(new string[] { "ID", id.ToString() });
            infosToReturn.Add(new string[] { "Type", type });
            infosToReturn.Add(new string[] { "Quantité", quantity.ToString() });

            return infosToReturn;
        }
    }
}
