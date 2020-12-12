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

    //Classe Cient
    public class Client: Personnage
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

        public Client(int argent, int attente, string nom, string prenom, int age, int[] position, Loader loader) :base(nom, prenom, age, position, loader)
        {
            this.inventaire.Add(new Divers("argent", this, loader));
            this.attente = attente;
            etat = "idle";
            repasEtape = "null";
        }

        //Fonction appelée à chaque tic pour rendre le personnage dynamique, fait changer son état (mange, attends...)
        public override void UpdatePersonnage(int delta)
        {
            switch(etat)
            {
                case "idle":
                    if(tableDonnee)
                    {
                        bool valid = false;
                        if(MoveTo(table.GetPosition()))
                        {
                            if(Sasseoir())
                            {
                                valid = true;
                            }
                        }

                        if(valid == false)
                        {
                            MoveTo(maitre.GetPosition());
                        }
                    }
                    break;

                case "assis":

                    if(carte != null)
                    {
                        etat = "choisisMenu";
                    }
                    break;

                case "choisisMenu":

                    if(menuChoisi == null)
                    {
                        Random rnd = new Random();
                        menuChoisi = carte.GetMenus()[rnd.Next(0, carte.GetMenus().Count())];
                        attenteCountDown = rnd.Next(1, 3) * attente;
                    }
                    else
                    {
                        if(attenteCountDown > 0)
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

                    if(attenteCountDown > 0)
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

            if(menuChoisi != null)
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
            
            if(tablentity.FindEntityByType("Controller.Table").Count() > 0)
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
            
            if(GetRepasEtape() == "Dessert")
            {
                etat = "part";
            }
            else
            {
                if(GetRepasEtape() == "Entree")
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
            foreach(Client client in table.GetClients())
            {
                if(client.etat != "part")
                {
                    tousClientsFinis = false;
                }
            }

            if(tousClientsFinis)
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

    //Classe abstraite implémentée par tout personnel de la salle du restaurant
    public abstract class PersonnelSalle: Personnage
    {
        public PersonnelSalle(string nom, string prenom, int age, int[] position, Loader loader) : base(nom, prenom, age, position, loader)
        {

        }
    }

    //Classe Maitre Hotel, qui va reçevoir les clients
    public class MaitreHotel: PersonnelSalle
    {
        private int randomCountDown;
        private Random rnd;
        private List<List<Client>> clientsAttente;
        private Hall hall;
        private Restaurant restaurant;
        private List<ChefDeRang> chefsDeRang;

        public MaitreHotel(string nom, string prenom, int age, int[] position, Loader loader) : base(nom, prenom, age, position, loader)
        {
            rnd = new Random();
            randomCountDown = 0;
            this.hall = loader.GetWorld().getHall();
            this.restaurant = loader.GetWorld().getRestaurant();
            this.clientsAttente = new List<List<Client>>();
            this.chefsDeRang = new List<ChefDeRang>();
        }

        //Fonction appelée à chaque tic, rendant le personnage dynamique, dans ce cas cela lui permet de générer des client et de les reçevoir
        public override void UpdatePersonnage(int delta)
        {
            //Console.WriteLine(randomCountDown);

            if (randomCountDown <= 0)
            {
                clientsAttente.Add(hall.GenerateClients(loader));
                randomCountDown = rnd.Next(10, 20);
            }
            else
            {
                randomCountDown -= 1;
            }

            if(clientsAttente.Count > 0)
            {
                if(RecieveClients(clientsAttente[0]))
                {
                    clientsAttente.RemoveAt(0);
                }
            }
        }

        //Fonction de retour d'informations pour la vue
        public override List<string[]> ReturnInformations()
        {
            List<string[]> infosToReturn = new List<string[]>();

            infosToReturn.Add(new string[] { "ID", id.ToString() });
            infosToReturn.Add(new string[] { "Clients en attente", clientsAttente.Count().ToString() });

            return infosToReturn;
        }

        //Ajout d'un chef de rang à la liste des chefs de rang
        public void AddChefDeRang(ChefDeRang chefDeRang)
        {
            chefsDeRang.Add(chefDeRang);
        }

        //Le maitre d'hôtel reçois les clients, c'est-à-dire qu'il cherche les tables libres et leur attribue une table qui contiens assez de place
        private bool RecieveClients(List<Client> clients)
        {
            int quantity = clients.Count();
            List<Table> tables = restaurant.GetEmptyTables();
            if (tables.Count() > 0)
            {
                Table designatedTable = tables[0];
                if(designatedTable.Reserver())
                {
                    foreach(Client client in clients)
                    {
                        client.RecevoirMaitre(this);
                        //client.RecevoirTable(designatedTable);
                    }
                    AlertChefDeRang(clients, designatedTable);
                    return true;
                }
            }
            return false;
        }

        //Appel d'un chef de rang pour diriger un groupe de clients vers une table désignée
        private bool AlertChefDeRang(List<Client> clients, Table table)
        {
            foreach(ChefDeRang chef in chefsDeRang)
            {
                if(chef.GetEtat() == "idle")
                {
                    chef.AddClientsToRecieve(clients, table);
                    Console.WriteLine("MH "+id+" alerte CR "+chef.id+" pour reçevoir clients pour table "+ table.GetNumTable());
                    return true;
                }
            }
            chefsDeRang[0].AddClientsToRecieve(clients, table);
            Console.WriteLine("MH " + id + " alerte CR " + chefsDeRang[0].id + " pour reçevoir clients pour table " + table.GetNumTable());
            return false;
        }

        //Fonction appelée lorsqu'un client pars, pour détruire l'instance client en question
        public void QuitClient(Client client)
        {
            if(client.table.GetPlacesLibres() == client.table.GetNombrePlaces())
            {
                client.table.Liberer();
            }
        }
    }

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
                            foreach(Client client in targetTable.GetClients())
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
                        if(table.serveurReservee == false)
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

                    if(clientsToRecieve.Count() > 0)
                    {
                        RecieveClients(clientsToRecieve[0]);
                    }
                    else
                    {
                        List<Table> tablesCorrespondent = CheckTablesClientsEtat("menuChoisi");

                        if(tablesCorrespondent.Count() > 0)
                        {
                            foreach(Table table in tablesCorrespondent)
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

                    if(GetPosition() != loader.GetWorld().GetComptoir().GetPointAccesRestaurant())
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
            foreach(Client client in (List<Client>)clientsTable[0])
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
            foreach(Client client in table.GetClients())
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
                if(clientsCorrespondent == true)
                {
                    tablesCorrespondent.Add(table);
                }
            }
            return tablesCorrespondent;
        }
    }
}
