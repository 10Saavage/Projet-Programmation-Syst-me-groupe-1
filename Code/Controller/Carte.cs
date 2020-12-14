using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe de la carte, qui contiens tous les menus
    public class Carte : Objet
    {
        private List<List<String>> menus;

        public Carte(Entity conteneur, Loader loader, List<List<String>> menus) : base(conteneur, loader)
        {
            this.menus = menus;
        }

        public override List<string[]> ReturnInformations()
        {
            List<string[]> infosToReturn = new List<string[]>();

            infosToReturn.Add(new string[] { "ID", id.ToString() });

            int i = 0;

            foreach (List<String> menu in menus)
            {
                i++;
                infosToReturn.Add(new string[] { "Menu " + i, menu[0] + " " + menu[1] + " " + menu[2] });
            }

            return infosToReturn;
        }

        public List<List<String>> GetMenus()
        {
            return menus;
        }
    }
}
