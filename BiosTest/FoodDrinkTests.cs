using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Eten_Class;
using Drinken_Class;

namespace BiosTest
{
    [TestClass]
    public class FoodDrinkTests
    {
        /// FOOD ///

        // EtenMenu //

        //  DeleteEten //

        // ViewClicks //

        // UpdateClicks //
        [TestMethod]
        public void UpdateClicks_IntIndex_UpdateClicks()
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
        public void ClearAllClicks_IntIndex_ClearClicks()
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
        public void ClearClicks_IntIndex_ClearClicks()
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

        // EtenAllergieFilter //



        /// DRINK///

        // DrinkenMenu //

        //  DeleteDrinken //

        // ViewClicks //

        // UpdateClicks //

        // ClearAllClicks //

        // ClearClicks //

        // DrinkenFilter //

        // DrinkenAllergieFilter //
    }
}
