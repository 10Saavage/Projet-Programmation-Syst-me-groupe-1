using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    //Classe chef de rang, qui accueille les clients, les amène à une table donnée par le Maitre d'hôtel, leur donne le menu et prends les commmandes
    public class ChefDeRang : PersonnelSalle
    {
        private string etat;
        private List<Object[]> clientsToRecieve;
        private int attenteClients;
        private int[] tablePosition;
        private Carte carte;
        private MaitreHotel maitre;
        private Table tableCiblee;
        private List<List<String>> commandes;

        public ChefDeRang(string nom, string prenom, int age, int[] position, Loader loader, MaitreHotel maitreHotel) : base(nom, prenom, age, position, loader)
        {
            etat = "idle";
            clientsToRecieve = new List<Object[]>();
            tablePosition = new int[2];
            carte = null;
            this.maitre = maitreHotel;
            maitre.AddChefDeRang(this);
            commandes = new List<List<String>>();
        }

        //Fonction appelée à chaque tic
        public override void UpdatePersonnage(int delta)
        {
            switch (etat)
            {
                case "idle":

                    if (clientsToRecieve.Count() > 0)
                    {
                        RecieveClients(clientsToRecieve[0]);
                    }
                    else
                    {
                        List<Table> tablesCorrespondent = CheckTablesClientsEtat("menuChoisi");

                        if (tablesCorrespondent.Count() > 0)
                        {
                            foreach (Table table in tablesCorrespondent)
                            {
                                if (table.commisReservee == false)
                                {
                                    Console.WriteLine("CR " + id + " récupère une commande à table " + tablesCorrespondent[0].GetNumTable());
                                    table.commisReservee = true;
                                    etat = "prendCommande";
                                    tableCiblee = table;
                                    MoveTo(tableCiblee.GetPosition());
                                    break;
                                }
                            }
                        }
                    }

                    break;

                case "attendsClientsTable":

                    WaitingClients();
                    break;

                case "apporteCartes":

                    GettingMenus();
                    break;

                case "prendCommande":
                    GettingCommande(tableCiblee);
                    break;

                case "deposeCommande":

                    if (GetPosition() != loader.GetWorld().GetComptoir().GetPointAccesRestaurant())
                    {
                        MoveTo(loader.GetWorld().GetComptoir().GetPointAccesRestaurant());
                    }
                    else
                    {
                        GivingCommandeKitchen();
                    }
                    break;

                case "apporteCouverts":

                    break;
            }
        }

        //Fonction de retour d'informations pour la vue
        public override List<string[]> ReturnInformations()
        {
            List<string[]> infosToReturn = new List<string[]>();

            infosToReturn.Add(new string[] { "ID", id.ToString() });
            infosToReturn.Add(new string[] { "Etat", etat });

            return infosToReturn;
        }

        //Fonction d'attente des clients à la table donnée, le temps qu'ils s'y installent
        private void WaitingClients()
        {
            if (attenteClients > 0)
            {
                if (clientsToRecieve.Count() > 0)
                {
                    Table table = (Table)clientsToRecieve[0][1];
                    bool allClientsPresents = true;

                    foreach (Client client in (List<Client>)clientsToRecieve[0][0])
                    {
                        if (loader.GetWorld().GetTableEntity(GetPosition()).HasEntity(client.id) == false)
                        {
                            allClientsPresents = false;
                        }
                    }

                    if (allClientsPresents == true)
                    {
                        etat = "apporteCartes";
                        Console.WriteLine("CR " + id + " apporte les cartes pour la table " + table.GetNumTable());
                    }
                    else
                    {
                        attenteClients--;
                    }
                }
                else
                {
                    attenteClients = 0;
                }
            }
            else
            {
                Console.WriteLine("CR " + id + " a trop attendus !");
                etat = "idle";
            }
        }

        //Fonction pour aller au comptoir, prendre les menus, retourner à la table ciblée et donner les menus aux clients
        private void GettingMenus()
        {
            if (carte == null)
            {
                if (GetPosition() == ((Table)clientsToRecieve[0][1]).GetPosition())
                {
                    tablePosition = GetPosition();
                    MoveTo(loader.GetWorld().GetComptoir().GetPointAccesRestaurant());
                }
                else
                {
                    carte = loader.GetWorld().GetComptoir().GetCarte();
                    Console.WriteLine("CR " + id + " a récupéré cartes");
                }
            }
            else
            {
                if (GetPosition() == ((Table)clientsToRecieve[0][1]).GetPosition())
                {
                    foreach (Client client in (List<Client>)clientsToRecieve[0][0])
                    {
                        client.RecieveCarte(carte);
                    }

                    Console.WriteLine("CR " + id + " a donné les cartes aux clients de la table " + ((Table)clientsToRecieve[0][1]).GetNumTable());

                    clientsToRecieve.RemoveAt(0);

                    carte = null;

                    etat = "idle";
                }
                else
                {
                    MoveTo(tablePosition);
                }
            }
        }

        //Fonction d'appel du maître d'hôtel, appelée à la création du chef de rang pour que le maître d'hôtel le prenne en compte
        public void InformMaitreHotel(MaitreHotel maitreHotel)
        {
            maitreHotel.AddChefDeRang(this);
        }

        //Fonction appelée par le Maitre d'hôtel pour ajouter au chef de rang des clients à reçevoir
        public void AddClientsToRecieve(List<Client> clients, Table table)
        {
            clientsToRecieve.Add(new Object[] { clients, table });
        }

        //Fonction de réception des clients
        private void RecieveClients(Object[] clientsTable)
        {
            Table table = (Table)clientsTable[1];
            MoveTo(table.GetPosition());
            foreach (Client client in (List<Client>)clientsTable[0])
            {
                client.RecevoirTable(table);
            }
            attenteClients = 5;
            etat = "attendsClientsTable";
            Console.WriteLine("CR " + id + " viens de recevoir des clients pour la table " + table.GetNumTable());
        }

        //Retourne l'état du chef de rang
        public string GetEtat()
        {
            return etat;
        }

        //Récupère la commande des clients à une table donnée
        private void GettingCommande(Table table)
        {
            foreach (Client client in table.GetClients())
            {
                commandes.Add(client.DonnerMenu());
            }

            tableCiblee.commisReservee = false;
            tableCiblee = null;
            etat = "deposeCommande";
        }

        //Donne la commande à la cuisine
        private void GivingCommandeKitchen()
        {
            Console.WriteLine("CR " + id + " a déposé une commande à la cuisine");
            loader.GetWorld().GetComptoir().AddCommande(commandes);
            etat = "idle";
        }

        //Check l'état des clients à une table donnée
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
    }
}
