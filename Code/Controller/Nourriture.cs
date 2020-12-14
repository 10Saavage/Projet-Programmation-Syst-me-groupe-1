using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe de la nourriture
    public class Nourriture : Objet
    {
        public string nom;
        private string stockage;

        public Nourriture(string nom, string stockage, Entity conteneur, Loader loader) : base(conteneur, loader)
        {
            this.nom = nom;
            this.stockage = stockage;
        }

        public override List<string[]> ReturnInformations()
        {
            List<string[]> infosToReturn = new List<string[]>();

            infosToReturn.Add(new string[] { "ID", id.ToString() });
            infosToReturn.Add(new string[] { "Nom", nom });
            infosToReturn.Add(new string[] { "Stockage", stockage });

            return infosToReturn;
        }
    }
}
