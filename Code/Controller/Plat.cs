using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe de Plat, étant une assiette avec des aliments transformée (pour simplifier le code)
    public class Plat : Objet
    {
        private ObjetConteneur platConteneur;
        public int platId;
        public string nomPlat;

        public Plat(string nomPlat, int platId, ObjetConteneur platConteneur, Entity conteneur, Loader loader) : base(conteneur, loader)
        {
            this.nomPlat = nomPlat;
            this.platId = platId;
            this.platConteneur = platConteneur;
        }

        public override List<string[]> ReturnInformations()
        {
            List<string[]> infosToReturn = new List<string[]>();

            infosToReturn.Add(new string[] { "ID", id.ToString() });
            infosToReturn.Add(new string[] { "Plat", nomPlat });

            return infosToReturn;
        }
    }
}
