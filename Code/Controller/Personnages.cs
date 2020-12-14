using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe abstraite qu'implémentent tous les personnages
    public abstract class Personnage : PhysicalEntity
    {
        public string nom;
        public string prenom;
        public int age;
        protected List<Object> inventaire;
        protected Loader loader;

        //Fonction appelée à chaque tic, pour rendre le personnage dynamique
        public abstract void UpdatePersonnage(int delta);

        public Personnage(string nom, string prenom, int age, int[] position, Loader loader):base(loader, position)
        {
            this.loader = loader;
            loader.GetCore().AddPersonnage(this);

            this.nom = nom;
            this.prenom = prenom;
            this.age = age;

            this.inventaire = new List<Object>();
        }

        //Fonction de déplacement
        public bool MoveTo(int[] position)
        {
            if(loader.GetWorld().GetTableEntity(position) != null)
            {
                loader.GetWorld().GetTableEntity(this.position).RemoveEntity(this);
                this.position = position;
                loader.GetWorld().GetTableEntity(this.position).AddEntity(this);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    

    //Classe abstraite implémentée par tout personnel de la salle du restaurant
    public abstract class PersonnelSalle: Personnage
    {
        public PersonnelSalle(string nom, string prenom, int age, int[] position, Loader loader) : base(nom, prenom, age, position, loader)
        {

        }
    }
}
