using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe Cient
    public class Client : Personnage
    {
        public int attente;
        public Table table;
        public bool tableDonnee;
        public string etat;
        private MaitreHotel maitre;
        private Carte carte;
        private List<String> menuChoisi;
        private string repasEtape;
        private int attenteCountDown;
        private Plat plat;

        public Client(int argent, int attente, string nom, string prenom, int age, int[] position, Loader loader) : base(nom, prenom, age, position, loader)
        {
            this.inventaire.Add(new Divers("argent", this, loader));
            this.attente = attente;
            etat = "idle";
            repasEtape = "null";
        }

        //Fonction appelée à chaque tic pour rendre le personnage dynamique, fait changer son état (mange, attends...)
        public override void UpdatePersonnage(int delta)
        {
            switch (etat)
            {
                case "idle":
                    if (tableDonnee)
                    {
                        bool valid = false;
                        if (MoveTo(table.GetPosition()))
                        {
                            if (Sasseoir())
                            {
                                valid = true;
                            }
                        }

                        if (valid == false)
                        {
                            MoveTo(maitre.GetPosition());
                        }
                    }
                    break;

                case "assis":

                    if (carte != null)
                    {
                        etat = "choisisMenu";
                    }
                    break;

                case "choisisMenu":

                    if (menuChoisi == null)
                    {
                        Random rnd = new Random();
                        menuChoisi = carte.GetMenus()[rnd.Next(0, carte.GetMenus().Count())];
                        attenteCountDown = rnd.Next(1, 3) * attente;
                    }
                    else
                    {
                        if (attenteCountDown > 0)
                        {
                            attenteCountDown -= 1;
                        }
                        else
                        {
                            Console.WriteLine("Le client " + id + " a choisis le menus " + menuChoisi[0] + " " + menuChoisi[1] + " " + menuChoisi[2]);
                            etat = "menuChoisi";
                        }
                    }

                    break;

                case "menuChoisi":
                    break;

                case "attendsRepas":
                    break;

                case "mange":

                    if (attenteCountDown > 0)
                    {
                        attenteCountDown--;
                    }
                    else
                    {
                        FinisPlat();
                    }

                    break;

                case "part":

                    QuitterTable();

                    break;

                case "paye":

                    Payer();

                    break;
            }
        }

        //Fonction de retour d'informations pour la vue
        public override List<string[]> ReturnInformations()
        {
            List<string[]> infosToReturn = new List<string[]>();

            infosToReturn.Add(new string[] { "ID", id.ToString() });
            infosToReturn.Add(new string[] { "Etat", etat });
            infosToReturn.Add(new string[] { "Modificateur de temps", attente.ToString() });
            infosToReturn.Add(new string[] { "Table donnée", tableDonnee.ToString() });

            if (menuChoisi != null)
            {
                infosToReturn.Add(new string[] { "Menu choisi", menuChoisi[0] + " " + menuChoisi[1] + " " + menuChoisi[2] });
            }
            else
            {
                infosToReturn.Add(new string[] { "Menu choisi", "Aucun" });
            }

            infosToReturn.Add(new string[] { "Etape du repas", repasEtape });
            infosToReturn.Add(new string[] { "Countdown attente", attenteCountDown.ToString() });

            return infosToReturn;
        }

        //Fonction pour reçevoir une table, qui sera appelée par le chef de rang
        public void RecevoirTable(Table table)
        {
            this.table = table;
            tableDonnee = true;
        }

        //Fonction pour reçevoir le maître d'hôtel, vers lequel le client repartiras pour payer
        public void RecevoirMaitre(MaitreHotel maitre)
        {
            this.maitre = maitre;
        }

        //Fonction pour s'asseoir à une table
        public bool Sasseoir()
        {
            TableEntity tablentity = loader.GetWorld().GetTableEntity(position);

            if (tablentity.FindEntityByType("Controller.Table").Count() > 0)
            {
                Table tableCase = (Table)tablentity.FindEntityByType("Controller.Table")[0];

                if (tableCase == table && tableCase.GetPlacesLibres() > 0)
                {
                    table.AjouterClient(this);
                    etat = "assis";

                    Console.WriteLine("Client " + id + " s'asseois à table " + table.GetNumTable());

                    return true;
                }

            }
            return false;
        }

        //Fonction pour reçevoir la crate des menus, appelée par le chef de rang
        public void RecieveCarte(Carte carte)
        {
            this.carte = carte;
        }

        //Fonction pour rendre la carte au chef de rang, change l'état du client en attende du repas
        public void GiveBackCarte()
        {
            carte = null;
        }

        //Renvois l'etat du repas du client (entrée, plat, dessert)
        public string GetRepasEtape()
        {
            return repasEtape;
        }

        //Renvois l'état du client (mange, attends, attends menu...)
        public string GetEtat()
        {
            return etat;
        }

        //Renvois le menu choisis par le client
        public List<String> DonnerMenu()
        {
            etat = "attendsRepas";
            repasEtape = "Entree";
            return menuChoisi;
        }

        //Renvois le plat du menu choisis que le client attends
        public string GetPlatAttente()
        {
            switch (repasEtape)
            {
                case "Entree":
                    return menuChoisi[0];

                case "Plat":
                    return menuChoisi[1];

                case "Dessert":
                    return menuChoisi[2];
            }
            return null;
        }

        //Le client reçois le plat, change son état en "mange"
        public void RecevoirPlat(Plat plat)
        {
            Console.WriteLine("Client " + id + " a reçus " + plat.nomPlat);

            this.plat = plat;
            Random rnd = new Random();
            attenteCountDown = rnd.Next(5, 10) * attente;
            etat = "mange";
        }

        //Le client a finis le plat
        private void FinisPlat()
        {
            plat = null;

            Console.WriteLine("Client " + id + " a finis " + repasEtape);

            if (GetRepasEtape() == "Dessert")
            {
                etat = "part";
            }
            else
            {
                if (GetRepasEtape() == "Entree")
                {
                    repasEtape = "Plat";
                }
                else
                {
                    repasEtape = "Dessert";
                }
                etat = "attendsRepas";
            }
        }

        //Le client quitte la table
        private void QuitterTable()
        {
            bool tousClientsFinis = true;
            foreach (Client client in table.GetClients())
            {
                if (client.etat != "part")
                {
                    tousClientsFinis = false;
                }
            }

            if (tousClientsFinis)
            {
                Console.WriteLine("Client " + id + " s'en va");
                table.RetirerClient(this);
                MoveTo(maitre.GetPosition());
                etat = "paye";
            }
        }

        //Le client paye le maitre d'hôtel
        private void Payer()
        {
            maitre.QuitClient(this);

            loader.GetCore().DelPersonnage(this);
        }
    }
}
