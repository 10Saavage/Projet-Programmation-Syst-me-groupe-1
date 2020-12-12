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

    //Classe faisant fonctionner l'ensemble des personnage, elle appelle pour chaque personnage la fonction d'update personnage à une fréquence régulière (1fois par seconde, 10 fois par seconde)
    public class Core
    {
        private List<Personnage> personnages;
        private System.Timers.Timer aTimer;
        private int timeElapsed;
        private Delegate refreshCaller;

        public Core()
        {
            personnages = new List<Personnage>();
            SetTimeElapsed(60);

            SetTimer();
        }

        //Ajout d'un personnage à mettre à jour
        public void AddPersonnage(Personnage personnage)
        {
            personnages.Add(personnage);
        }

        //Suppression d'un personnage à mettre à jour
        public void DelPersonnage(Personnage personnage)
        {
            personnages.Remove(personnage);
        }

        //Définition du temsp écoulé (ne sert à rien dans ce contexte, puisque le Delta n'est pas exploité)
        public void SetTimeElapsed(int timeElapsed)
        {
            this.timeElapsed = timeElapsed;
        }

        //Mise en pause du timer, et donc de l'ensemble de l'application
        public void Pause(bool pause)
        {
            if(pause==true)
            {
                aTimer.Enabled = false;
            }
            else
            {
                aTimer.Enabled = true;
            }
        }

        //Création du timer
        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        //Fonction appelée à chaque tic du timer
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            foreach(Personnage personnage in personnages.ToList())
            {
                personnage.UpdatePersonnage(timeElapsed);
            }

            if(refreshCaller != null)
            {
                refreshCaller.DynamicInvoke();
            }
        }

        //Définition de la vitesse (x1, x10), appelée par la vue lorsqu'on change la vitesse
        public void SetSpeed(bool speedUp)
        {
            if(speedUp == true)
            {
                aTimer.Interval = 100;
            }
            else
            {
                aTimer.Interval = 1000;
            }
        }

        //Référence un délégué donné par la vue pour appeler automatiquement le rafraichissement de la vue à chaque tic
        public void setDeleg(Delegate deleg)
        {
            refreshCaller = deleg;
        }
    }

}
