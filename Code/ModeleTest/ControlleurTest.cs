using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controller;

namespace ModeleTest
{
    /// <summary>
    /// Description résumée pour ControlleurTest
    /// </summary>
    [TestClass]
    public class ControlleurTest
    {
        public ControlleurTest()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active, ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestCore()
        {
            Core core = new Core();
            int valeur = 1;

            Assert.AreEqual(valeur, core.CoreVitesse(1));
        }

        [TestMethod]
        public void ComptoirTest()
        {
            Comptoir comptoir = new Comptoir();
            string assiette = "couverts";

            Assert.AreEqual(comptoir.DonnePlat(1), assiette);
        }

        [TestMethod]
        public void HallCreationTest()
        {
            Hall hall = new Hall();
            string creation = "Hall";

            Assert.AreEqual(hall.créationHall(1), creation);
        }

        [TestMethod]
        public void CuisineCreationTest()
        {
            Cuisine cuisine = new Cuisine();
            string creation = "Cuisine";

            Assert.AreEqual(cuisine.créationCuisine(1), creation);
        }

        [TestMethod]
        public void InstatiationElementWorldTest()
        {
            TableEntity tableEntity = new TableEntity();
            string creation = "TableEntity";

            Assert.AreEqual(tableEntity.InstatiationElementWorld(1), creation);
        }

        [TestMethod]
        public void TrouverTableRestaurant()
        {
            Restaurant restaurant = new Restaurant();
            string creation = "Restaurant";

            Assert.AreEqual(restaurant.TrouverTableRestaurant(), creation);
        }
    }
}
