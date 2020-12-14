using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe serveur, qui apportera les plats aux clients
    public class Serveur : PersonnelSalle
    {
        private string etat;
        private Comptoir comptoir;
        private Table targetTable;
        private bool hasPlats;

        public Serveur(string nom, string prenom, int age, int[] position, Loader loader) : base(nom, prenom, age, position, loader)
        {
            etat = "idle";
            comptoir = loader.GetWorld().GetComptoir();
        }

        //Fonction appelée à chaque tic, définis l'état du serveur et permet de vérifier périodiquement l'état des clients pour prendre des décisions en conséquence
        public override void UpdatePersonnage(int delta)
        {
            switch (etat)
            {
                case "idle":

                    CheckPlatsClients();

                    break;

                case "apportePain":
                    break;

                case "apportePlat":

                    if (hasPlats == false)
                    {
                        if (GetPosition() != comptoir.GetPointAccesRestaurant())
                        {
                            MoveTo(comptoir.GetPointAccesRestaurant());
                        }
                        else
                        {
                            Console.WriteLine("Serveur " + id + " a prit plats pour table " + targetTable.GetNumTable());
                            foreach (Client client in targetTable.GetClients())
                            {
                                inventaire.Add(comptoir.stockPlats.TakeContent(comptoir.stockPlats.FindContent(new Predicate<object>(o => ((Plat)o).nomPlat == client.GetPlatAttente()))));
                            }
                            hasPlats = true;
                        }
                    }
                    else
                    {
                        if (GetPosition() != targetTable.GetPosition())
                        {
                            MoveTo(targetTable.GetPosition());
                        }
                        else
                        {
                            foreach (Client client in targetTable.GetClients())
                            {
                                Plat plat = (Plat)inventaire.Find(o => ((Plat)o).nomPlat == client.GetPlatAttente());
                                inventaire.Remove(plat);
                                client.RecevoirPlat(plat);
                            }
                            targetTable.serveurReservee = false;
                            targetTable = null;
                            hasPlats = false;
                            etat = "idle";
                        }
                    }
                    break;

                case "debarasseRepas":
                    break;
            }
        }

        //Fonction de retour d'informations pour la vue
        public override List<string[]> ReturnInformations()
        {
            List<string[]> infosToReturn = new List<string[]>();

            infosToReturn.Add(new string[] { "ID", id.ToString() });
            infosToReturn.Add(new string[] { "Etat", etat });
            //infosToReturn.Add(new string[] { "Table ciblée", targetTable.GetNumTable().ToString() });
            infosToReturn.Add(new string[] { "A des plats", hasPlats.ToString() });

            return infosToReturn;
        }

        //Check de l'état des clients pour chaque table. Permet de savoir quand des clients sont en train d'attendre un plat
        private List<Table> CheckTablesClientsEtat(string etatToCheck)
        {
            List<Table> tablesRemplies = loader.GetWorld().getRestaurant().GetFilledTables();
            List<Table> tablesCorrespondent = new List<Table>();

            foreach (Table table in tablesRemplies)
            {
                bool clientsCorrespondent = true;

                foreach (Client client in table.GetClients())
                {
                    if (client.GetEtat() != etatToCheck)
                    {
                        clientsCorrespondent = false;
                        break;
                    }
                }
                if (clientsCorrespondent == true)
                {
                    tablesCorrespondent.Add(table);
                }
            }
            return tablesCorrespondent;
        }

        //Check les plats que les clients attendent, afin de les apporter si ils la cuisine les a préparés
        private void CheckPlatsClients()
        {
            List<Table> tablesCorrespondent = CheckTablesClientsEtat("attendsRepas");

            if (tablesCorrespondent.Count() > 0)
            {
                foreach (Table table in tablesCorrespondent)
                {
                    bool platsPrets = true;
                    foreach (Client client in table.GetClients())
                    {
                        if (comptoir.HasPlat(client.GetPlatAttente()) == false)
                        {
                            platsPrets = false;
                        }
                    }

                    if (platsPrets)
                    {
                        if (table.serveurReservee == false)
                        {
                            table.serveurReservee = true;
                            targetTable = table;
                            etat = "apportePlat";
                        }
                        break;
                    }
                }
            }
        }

    }
}
