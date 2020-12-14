using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe abstraite implémentée par tous les objets
    public abstract class Objet: Entity
    {
        private Entity conteneur;

        public Objet(Entity conteneur, Loader loader) : base(loader)
        {
            this.conteneur = conteneur;
        }

        public Entity GetConteneur()
        {
            return conteneur;
        }

        public void SetConteneur(Entity conteneur)
        {
            this.conteneur = conteneur;
        }
    }




    //Classe d'objet conteneur, objet qui va contenir d'autres objets (assiette, qui va contenir des aliments...)
    public class ObjetConteneur : Materiel
    {
        //If it can contain solids or liquids
        public string contenuType;
        public string type;
        private List<Nourriture> contenuNourriture;

        public ObjetConteneur(Entity conteneur, Loader loader, string type, bool propre = true, string contenuType = "Solid") : base(conteneur, loader, propre)
        {
            this.type = type;
            this.contenuType = contenuType;
            contenuNourriture = new List<Nourriture>();
        }

        public void AjouterContenu(Nourriture nourriture)
        {
            contenuNourriture.Add(nourriture);
        }

        public bool RetirerContenu(Nourriture nourriture)
        {
            return contenuNourriture.Remove(nourriture);
        }

        public List<Nourriture> GetContenu()
        {
            return contenuNourriture;
        }
    }
}
