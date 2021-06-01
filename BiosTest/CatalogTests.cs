using System;
using System.IO;
using MovieDetail_Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiosTest
{
    [TestClass]
    public class CatalogTests
    {
        // MethodName //
        [TestMethod]
        public void ArrToString_InputArray_ReturnEmptyString() 
        {
            // Arrange
            string[] arr = new string[0];

            // Act
            var result = MovieDetail.ArrToString(arr);

            // Assert
            Assert.AreEqual(result, "");
        }

        // MethodName //
        [TestMethod]
        public void ArrToString_InputArray_ReturnString()
        {
            // Arrange
            string[] arr = new string[] { "Geralt", "Witcher", "Butcher of Blaviken" };

            // Act
            var result = MovieDetail.ArrToString(arr);

            // Assert
            Assert.AreEqual(result, "Geralt, Witcher, Butcher of Blaviken");
        }

        // MethodName //
        [TestMethod]
        public void DisplayMovie_InputInt_ReturnsString()
        {
            // Arrange
            MovieDetail movie = new MovieDetail();
            int index = 1;

            // Act
            var result = MovieDetail.DisplayMovie(index);

            // Assert
            Assert.AreEqual(result, "Titel: The Broken Hearts Gallery\n\n" +
                "Genre: romantiek, komedie\nKijkwijzer: 12, seks, grof taalgebruik\n\n" +
                "Regisseur: Natalie Krinsky\nActeurs: Geraldine Viswanathan, Dacre Montgomery, Utkarsh Ambudkar\n\n" +
                "samenvatting: De twintiger Lucy werkt in een kunstgalerie en is op persoonlijk vlak een grote verzamelaar. Als ze door haar vriendje gedumpt wordt, krijgt ze het idee om het project The Broken Heart Gallery op te zetten, een verzamelplek voor alle objecten die te maken hebben met haar vroegere liefdes. De expositie wordt een succes en krijgt navolging.\n\n" +
                "Rating: 6.2\nSpeeltijd: 108 minuten\n");
        }
    }
}

