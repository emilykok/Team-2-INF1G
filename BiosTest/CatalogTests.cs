using System;
using MovieDetail_Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiosTest
{
    [TestClass]
    class CatalogTests
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

            // Act
            //var result = MovieDetail.ArrToString(arr);

            // Assert
            //Assert.AreEqual(result, "");
        }
    }
}

