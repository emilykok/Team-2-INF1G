using System;
using System.IO;
using MovieDetail_Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiosTest
{
    [TestClass]
    public class CatalogTests
    {
        // ArrToString //
        [TestMethod]
        public void ArrToString_InputStringArray_ReturnEmptyString() 
        {
            // Arrange
            string[] arr = new string[0];

            // Act
            var result = MovieDetail.ArrToString(arr);

            // Assert
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void ArrToString_InputStringArray_ReturnString()
        {
            // Arrange
            string[] arr = new string[] { "Geralt", "Witcher", "Butcher of Blaviken" };

            // Act
            var result = MovieDetail.ArrToString(arr);

            // Assert
            Assert.AreEqual(result, "Geralt, Witcher, Butcher of Blaviken");
        }

        // DisplayMovie //
        [TestMethod]
        public void DisplayMovie_InputInt_ReturnsString()
        {
            // Arrange
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

        // GetList //
        [TestMethod]
        public void GetList_InputInt_ReturnsStringArray()
        {
            // Arrange
            int page = 1;
            string[] expected = new string[] { "1.\tThe Broken Hearts Gallery", "2.\tWords On The Bathroom Walls", "3.\tValley Girl", "4.\tThe Half of It", "5.\tThe Lovebirds", "6.\tHappiest Season", "7.\tTitanic", "8.\tDirty Dancing", "9.\tThe Kissing Booth 2", "10.\tDeadpool", "vorige pagina", "volgende pagina", "filter films", "terug naar hoofmenu" };

            // Act
            var result = MovieDetail.GetList(page);

            // Assert
            CollectionAssert.AreEqual(result, expected);
        }

        // movieFilter //
        [TestMethod]
        public void movieFilter_InputString_ReturnsJaggedStringArray()
        {
            // Arrange
            string tag = "animatie";
            string[] inner = new string[] { "Onward", "vorige pagina", "volgende pagina", "terug naar reguliere catalogus" };
            string[][] expected = new string[][] { inner };

            // Act
            var result = MovieDetail.movieFilter(tag);

            // Assert
            for(int i = 0; i < result.Length; i++)
            {
                CollectionAssert.AreEqual(result[i], expected[i]);
            }
        }


    }
}

