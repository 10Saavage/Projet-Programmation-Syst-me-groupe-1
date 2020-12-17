using System;
using BDDLink;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModeleTest
{
    [TestClass]
    public class ModeleTest
    {
        [TestMethod]
        public void Connexion_base_donnees()
        {
            cLAD cLAD = new cLAD();   
            bool connexion = true;

            Assert.AreEqual(cLAD.connexion_basedonnee(), connexion, "la réponse doit etre true");
        }
    }
}
