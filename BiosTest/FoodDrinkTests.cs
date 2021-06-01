using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Eten_Class;
using Drinken_Class;
using System.IO;

namespace BiosTest
{
    [TestClass]
    public class FoodDrinkTests
    {
        /// FOOD ///

        // EtenMenu //

        //  DeleteEten //

        // ViewClicks //
        //[TestMethod]
        public void ViewClicksF_IntIndex_printClicks()
        {
            // Arrange
            Eten eten = new Eten();
            int num = 8;

            // Act
            var output = new StringWriter();
            Console.SetOut(output);
            eten.ViewClicks(num);
            string res = output.ToString();

            // Assert
            Assert.AreEqual(res, ("Clicks op geselecteerde item is: " + eten.etenDataList[num - 1].clicks));
        }

        // UpdateClicks //
        [TestMethod]
        public void UpdateClicksF_IntIndex_UpdateClicks()
        {
            // Arrange
            int num = 5;
            Eten eten = new Eten();
            int bNum = eten.etenDataList[num - 1].clicks;

            // Act
            eten.UpdateClicks(5);
            int resNum = eten.etenDataList[num - 1].clicks;

            // Assert
            Assert.AreEqual(bNum + 1, resNum);
        }


        // ClearAllClicks //
        [TestMethod]
        public void ClearAllClicksF_IntIndex_ClearClicks()
        {
            // Arrange
            Eten eten = new Eten();
            int num = 8;

            // Act
            eten.ClearAllClicks();
            int resNum = eten.etenDataList[num - 1].clicks;

            // Assert
            Assert.AreEqual(0, resNum);
        }

        // ClearClicks //
        [TestMethod]
        public void ClearClicksF_IntIndex_ClearClicks()
        {
            // Arrange
            int num = 7;
            Eten eten = new Eten();

            // Act
            eten.ClearClicks(num);
            int resNum = eten.etenDataList[num - 1].clicks;

            // Assert
            Assert.AreEqual(0, resNum);
        }

        // EtenFilter //
        [TestMethod]
        public void FilterF_Titel_showRes()
        {
            // Arrange
            Eten eten = new Eten();
            string toSearch = "pop";
            // Act
            string res = eten.EtenFilter(toSearch);
            // Assert
            Assert.AreEqual(res, "Gevonden eten met zoekterm 'pop':\n\nPopcorn zoet\nPopcorn zout\nPopcorn karamel\n");
        }


        // EtenAllergieFilter //

        /// DRINK///

        // DrinkenMenu //

        //  DeleteDrinken //

        // ViewClicks //

        // UpdateClicks //
        [TestMethod]
        public void UpdateClicksD_IntIndex_UpdateClicks()
        {
            // Arrange
            int num = 5;
            Eten eten = new Eten();
            int bNum = eten.etenDataList[num - 1].clicks;

            // Act
            eten.UpdateClicks(5);
            int resNum = eten.etenDataList[num - 1].clicks;

            // Assert
            Assert.AreEqual(bNum + 1, resNum);
        }

        // ClearAllClicks //
        [TestMethod]
        public void ClearAllClicksD_IntIndex_ClearClicks()
        {
            // Arrange
            Drinken drink = new Drinken();
            int num = 8;

            // Act
            drink.ClearAllClicks();
            int resNum = drink.drinkenDataList[num - 1].clicks;

            // Assert
            Assert.AreEqual(0, resNum);
        }

        // ClearClicks //
        [TestMethod]
        public void ClearClicksD_IntIndex_ClearClicks()
        {
            // Arrange
            int num = 7;
            Drinken drink = new Drinken();

            // Act
            drink.ClearClicks(num);
            int resNum = drink.drinkenDataList[num - 1].clicks;

            // Assert
            Assert.AreEqual(0, resNum);
        }

        // DrinkenFilter //
        [TestMethod]
        public void FilterD_Titel_showRes()
        {
            // Arrange
            Drinken drink = new Drinken();
            string toSearch = "wijn";
            // Act
            string res = drink.DrinkenFilter(toSearch);
            // Assert
            Assert.AreEqual(res, "Gevonden drinken met zoekterm 'wijn':\n\nRode wijn\nWitte wijn\n"); // //
        }

        // DrinkenAllergieFilter //
    }
}
