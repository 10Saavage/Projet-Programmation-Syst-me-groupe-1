using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BDDLink
{
    public class CLcad
    {
        private SqlConnection connexion;
        private String command;
        private SqlCommand Sql;
        private SqlDataReader dr;
        private SqlDataAdapter da;
        private String etape;
        private String test = "Nom_Produits_frais";

        private String ddb = @"Data Source=TM-GARRINCHA\SQLEXPRESS;Initial Catalog=resto;User ID=Manoel; Password=Azerty11";
        private ArrayList a1 = new ArrayList();
        private ArrayList a2 = new ArrayList();
        List<String> al = new List<String>();

        public CLcad()
        {
            //Console.WriteLine(ddb);
            connexion = new SqlConnection(ddb);
        }
        // method for update data
        public void update_data(int qt, String qname, String qtable, String qcol, String qprod)
        {
            try
            {
                connexion.Open();
                Console.WriteLine("connexion");
                Console.WriteLine(qtable);
                this.Sql = new SqlCommand("UPDATE " + qtable + " SET " + qcol + " = @a1 WHERE " + qprod + " = @a2", connexion);
                Sql.Parameters.AddWithValue("a1", qt);
                Sql.Parameters.AddWithValue("a2", qname);
                Sql.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("echec");
            }
        }
        // method for read data
        public void read_data(String name, String table, String nbr)
        {
            try
            {
                connexion.Open();
                Console.WriteLine("connexion");
                this.Sql = new SqlCommand("SELECT " + name + " , " + nbr + " FROM " + table, connexion);
                dr = Sql.ExecuteReader();
                while (dr.Read())
                {
                    a1.Add(dr[name].ToString());
                    a1.Add(dr[nbr].ToString());
                }

                for (int i = 0; i < a1.Count; i++)
                {
                    Console.WriteLine(a1[i]);
                }
            }
            catch
            {
                Console.WriteLine("echec");
            }
        }
        //method for read the menu
        public void read_menu()
        {
            try
            {
                connexion.Open();
                Console.WriteLine("connexion");
                this.Sql = new SqlCommand("SELECT Nom_Entree, Nom_Plats, Nom_Desserts FROM menus INNER JOIN entree ON menus.ID_Entree = entree.ID_Entree INNER JOIN plats ON menus.ID_Plats = plats.ID_Plats INNER JOIN desserts ON menus.ID_Desserts = desserts.ID_Desserts", connexion);
                dr = Sql.ExecuteReader();
                dr.Read();
                List<List<String>> menu = new List<List<string>>()
                   {
                new List<String>{
                    dr["Nom_Entree"].ToString(), dr["Nom_Plats"].ToString(), dr["Nom_Desserts"].ToString()
                }};


                // Console.WriteLine(menu[1][3]);
            }
            catch
            {
                Console.WriteLine("echec");
            }
        }
        //method for the step of a receipts
        public void read_step()
        {
            try
            {
                connexion.Open();
                Console.WriteLine("connexion");
                this.Sql = new SqlCommand("SELECT Nom_Entree, Etapes FROM entree INNER JOIN recette ON entree.ID_Recette = recette.ID_Recette", connexion);
                dr = Sql.ExecuteReader();
                dr.Read();
                String etape = dr["Etapes"].ToString();
                String etape1 = dr["Nom_Entree"].ToString();
                Console.WriteLine(etape);
                Console.WriteLine(etape1);
                String[] step = etape.Split('/');
                String test = step[1];
                Console.WriteLine(test);
                String[] step1 = test.Split(';');
                String test1 = step1[0];
                Console.WriteLine(test1);
            }
            catch
            {
                Console.WriteLine("echec");
            }
        }
    }
}
