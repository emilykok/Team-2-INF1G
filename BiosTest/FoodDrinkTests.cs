﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Eten_Class;
using Drinken_Class;
using System.IO;
using Food_Drink_Run;

namespace BiosTest
{
    [TestClass]
    public class FoodDrinkTests
    {
        /// FOOD ///

        // EtenMenu //
        //[TestMethod]
        public void FoodMenu_validInp_showItem() {
            // Arrange
            Eten eten = new Eten();
            var input = new StringReader(@"4
");
            Console.SetIn(input);

            // Act
            var output = new StringWriter();
            Console.SetOut(output);
            eten.EtenMenu();
            string expected = @"Eten Menu:
---------------------------------------------------
1. Popcorn zoet - 		va 2,99
2. Popcorn zout - 		va 2,49
3. Popcorn karamel - 		va 2,49
4. M&M's pinda - 		3,99
5. M&M's chocola - 		4,49
6. Chips naturel - 		va 2,99
7. Chips paprika - 		va 2,99
8. Doritos nacho cheese - 	3,99
9. Haribo goudberen - 		3,49
10. Skittles fruits - 		3,99

11. Terug naar de vorige pagina


Typ het nummer van de item die je wilt bekijken en klik op enter:
M&M pinda
---------------------------------------------------

inhoud: 250g,
prijs: 3.99,

Per 100gr:
Energie: 2154kj(515kcal)
Vet: 26g
   Waarvan verzadigd: 11g
Koolhydraten: 59g
   Waarvan suikers: 53g
Voedingsvezel: 0g
Eiwitten: 9,9g
Zout: 0.09g

allergenen: lactose, pinda, soja, amandel, hazelnoot, noten,


1. Terug naar het eten & drinken menu
";
            // Assert
            Assert.AreEqual(output.ToString(),expected);
        }
        //[TestMethod]
        public void EtenMenu_wrongInp_showItem()
        {
            // Arrange
            Eten eten = new Eten();
            var input = new StringReader(@"80
");
            Console.SetIn(input);

            // Act
            var output = new StringWriter();
            Console.SetOut(output);
            eten.EtenMenu();
            // Assert
            Assert.AreEqual(output.ToString(), "Eten Menu:\n-------------------------------------------------- -\n1.Cola - \t\t2, 99\n2.Pepsi - \t\t3, 49\n3.Dr.Pepper - \t\t2, 99\n4.Fanta Orange - \t2, 99\n5.Spa rood - \t\t1, 99\n6.Spa blauw - \t\t1, 99\n7.Appelsap - \t\t2, 49\n8.Rode wijn - \t\t6, 49\n9.Witte wijn - \t6, 49\n10.Heineken - \t\t3, 49\n\n11.Terug naar de vorige pagina\n\n\nTyp het nummer van de item die je wilt bekijken en klik op enter:");
        }

            //  DeleteEten //
            [TestMethod]
        public void DeleteEten_index_deleteItem(){
            // Arrange
            Eten eten = new Eten();
            bool reach = false;

            // Act
            eten.DeleteEten(9);
            try { 
                var a = eten.etenDataList[9].naam;
                reach = true;
            }
            catch { reach = false; }
            
            
            
            // Assert
            Assert.IsFalse(reach);

        }

        // ViewClicks //
        [TestMethod]
        public void ViewClicksF_IntIndex_printClicks() {
            // Arrange
            Eten eten = new Eten();
            int num = 8;

            // Act
            var output = new StringWriter();
            Console.SetOut(output);
            eten.ViewClicks(num);
            string res = output.ToString();

            // Assert
            Assert.AreEqual(res, @"Clicks op geselecteerde item is: 0
");
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
        [TestMethod]
        public void FilterAlF_Allergie_showRes()
        {
            // Arrange
            Eten eten = new Eten();
            string toSearch = "lactose";
            // Act
            string res = eten.EtenAllergieFilter(toSearch);
            string expected = $"Gevonden eten zonder zoekterm '{toSearch}':\n\nPopcorn zoet\nPopcorn zout\nPopcorn karamel\n";
            // Assert
            Assert.AreEqual(res, expected);
        }


    /// DRINK///

        // DrinkenMenu //
        //[TestMethod]
        public void DrinkMenu_validInp_showItem()
        {
            // Arrange
            Drinken drinken = new Drinken();
            var input = new StringReader(@"4
");
            Console.SetIn(input);

            // Act
            var output = new StringWriter();
            Console.SetOut(output);
            drinken.DrinkenMenu();
            // Assert
            Assert.AreEqual(output.ToString(), $"Eten Menu:\n---------------------------------------------------\n1. Popcorn zoet - \t\tva 2,99\n2. Popcorn zout - \t\tva 2,49\n3. Popcorn karamel - \t\tva 2,49\n4. M&M's pinda - \t\t3,99\n5. M&M's chocola - \t\t4,49\n6. Chips naturel - \t\tva 2,99\n7. Chips paprika - \t\tva 2,99\n8. Doritos nacho cheese - \t3,99\n9. Haribo goudberen - \t\t3,49\n10. Skittles fruits - \t\t3,99\n\n11. Terug naar de vorige pagina\n\n\nTyp het nummer van de item die je wilt bekijken en klik op enter:\nM&M pinda\n---------------------------------------------------\n\ninhoud: 250g, \nprijs: 3.99\n\n{drinken.drinkenDataList[4 - 1].voedingswaarde}\n\nallergenen: lactose, pinda, soja, amandel, hazelnoot, noten, \n\n\n1. Terug naar het eten & drinken menu\n");
        }

        //[TestMethod]
        public void DrinkMenu_wrongInp_showItem()
        {
            // Arrange
            Drinken drinken = new Drinken();
            var input = new StringReader(@"80
");
            Console.SetIn(input);

            // Act
            var output = new StringWriter();
            Console.SetOut(output);
            drinken.DrinkenMenu();
            // Assert
            Assert.AreEqual(output.ToString(), "Drinken Menu:\n-------------------------------------------------- -\n1.Cola - \t\t2, 99\n2.Pepsi - \t\t3, 49\n3.Dr.Pepper - \t\t2, 99\n4.Fanta Orange - \t2, 99\n5.Spa rood - \t\t1, 99\n6.Spa blauw - \t\t1, 99\n7.Appelsap - \t\t2, 49\n8.Rode wijn - \t\t6, 49\n9.Witte wijn - \t6, 49\n10.Heineken - \t\t3, 49\n\n11.Terug naar de vorige pagina\n\n\nTyp het nummer van de item die je wilt bekijken en klik op enter:");
        }
        //  DeleteDrinken //
        [TestMethod]
        public void DeleteDrinken_index_deleteItem()
        {
            // Arrange
            Drinken drinken = new Drinken();
            bool reach = false;

            // Act
            drinken.DeleteDrinken(9);
            try
            {
                var a = drinken.drinkenDataList[9].naam;
                reach = true;
            }
            catch { reach = false; }



            // Assert
            Assert.IsFalse(reach);
        }

        // ViewClicks //
        [TestMethod]
        public void ViewClicksD_IntIndex_printClicks()
        {
            // Arrange
            Drinken drinken = new Drinken();
            int num = 8;

            // Act
            var output = new StringWriter();
            Console.SetOut(output);
            drinken.ViewClicks(num);
            string res = output.ToString();

            // Assert
            Assert.AreEqual(res, (@"Clicks op geselecteerde item is: 0
"));
        }

        // UpdateClicks //
        [TestMethod]
        public void UpdateClicksD_IntIndex_UpdateClicks()
        {
            // Arrange
            int num = 5;
            Drinken drinken = new Drinken();
            int bNum = drinken.drinkenDataList[num - 1].clicks;

            // Act
            drinken.UpdateClicks(5);
            int resNum = drinken.drinkenDataList[num - 1].clicks;

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
        public void FilterD_Titel_showRes() {
            // Arrange
            Drinken drink = new Drinken(); 
            string toSearch = "wijn";
            // Act
            string res = drink.DrinkenFilter(toSearch);
            // Assert
            Assert.AreEqual(res, "Gevonden drinken met zoekterm 'wijn':\n\nRode wijn\nWitte wijn\n");
        }

        // DrinkenAllergieFilter //
        [TestMethod]
        public void FilterAlD_Allergie_showRes()
        {
            // Arrange
            Drinken drinken = new Drinken();
            string toSearch = "vega";
            // Act
            string res = drinken.DrinkenAllergieFilter(toSearch);
            string expected = $"Gevonden drinken zonder zoekterm '{toSearch}':\n\nCola\nPepsi\nDr.Pepper\nFanta Orange\n";
            // Assert
            Assert.AreEqual(res, expected);
        }

        /// MENU ///
    }
}
