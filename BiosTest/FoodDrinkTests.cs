using System;
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
        [TestMethod]
        public void FoodMenu_validInp_showItem() {
            // Arrange
            Eten eten = new Eten();
            var input = new StringReader(@"4
");
            Console.SetIn(input);

            string[] expected = {"Eten Menu:","---------------------------------------------------", "1. Popcorn zoet - ","va 2,99", "2. Popcorn zout - ", "va 2,49", "3. Popcorn karamel - ", "va 2,49", "4. M&M's pinda - ", "3,99", "5. M&M's chocola - ", "4,49", "6. Chips naturel - ", "va 2,99", "7. Chips paprika - ", "va 2,99", "8. Doritos nacho cheese - ", "3,99", "9. Haribo goudberen - ", "3,49", "10. Skittles fruits - ", "3,99", "11. Terug naar de vorige pagina", "Typ het nummer van de item die je wilt bekijken en klik op enter:", "M&M pinda", "---------------------------------------------------", "inhoud: 250g, ", "prijs: 3.99, ", "Per 100gr:", "Energie: 2154kj(515kcal)", "Vet: 26g", "   Waarvan verzadigd: 11g", "Koolhydraten: 59g", "   Waarvan suikers: 53g", "Voedingsvezel: 0g", "Eiwitten: 9,9g", "Zout: 0.09g", "allergenen: lactose, pinda, soja, amandel, hazelnoot, noten, ", "1. Terug naar het eten & drinken menu" }; // MELISSA VUL HIER JE DE STRING IS   "Hallo\nWereld" => {"Hallo","Wereld"}

            // Act
            var output = new StringWriter();
            Console.SetOut(output);
            eten.EtenMenu();

            string convertString = output.ToString();
            string[] split = convertString.Split(new Char[] { '\t', '\n', '\r' },
                                 StringSplitOptions.RemoveEmptyEntries);

            // Assert
            for (int i = 0; i < split.Length; i++)
            {
                Assert.AreEqual(split[i], expected[i]);
            }
        }

        [TestMethod]
        public void EtenMenu_wrongInp_showItem()
        {
            // Arrange
            Eten eten = new Eten();
            var input = new StringReader(@"a1bc-
");
            Console.SetIn(input);
            string[] expected = { "Eten Menu:", "---------------------------------------------------", "1. Popcorn zoet - ", "va 2,99", "2. Popcorn zout - ", "va 2,49", "3. Popcorn karamel - ", "va 2,49", "4. M&M's pinda - ", "3,99", "5. M&M's chocola - ", "4,49", "6. Chips naturel - ", "va 2,99", "7. Chips paprika - ", "va 2,99", "8. Doritos nacho cheese - ", "3,99", "9. Haribo goudberen - ", "3,49", "10. Skittles fruits - ", "3,99", "11. Terug naar de vorige pagina", "Typ het nummer van de item die je wilt bekijken en klik op enter:", "De input is niet juist, probeer het nogeens" };

            // Act
            var output = new StringWriter();
            Console.SetOut(output);
            eten.EtenMenu();

            string convertString = output.ToString();
            string[] split = convertString.Split(new Char[] { '\t', '\n', '\r' },
                                 StringSplitOptions.RemoveEmptyEntries);

            // Assert
            for (int i = 0; i < split.Length; i++)
            {
                Assert.AreEqual(split[i], expected[i]);
            }
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
        [TestMethod]
        public void DrinkMenu_validInp_showItem()
        {
            // Arrange
            Drinken drinken = new Drinken();
            var input = new StringReader(@"4
");
            Console.SetIn(input);
            string[] expected = { "Drinken Menu:", "---------------------------------------------------", "1. Cola - ", "2,99", "2. Pepsi - ", "3,49", "3. Dr.Pepper - ", "2,99", "4. Fanta Orange - ", "2,99", "5. Spa rood - ", "1,99", "6. Spa blauw - ", "1,99", "7. Appelsap - ", "2,49", "8. Rode wijn - ", "6,49", "9. Witte wijn - ", "6,49", "10. Heineken - ", "3,49", "11. Terug naar de vorige pagina", "Typ het nummer van de item die je wilt bekijken en klik op enter:", "Fanta Orange" , "---------------------------------------------------", "inhoud: 0,5L", "prijs: 3.49", "Per 100ml:", "Energie: 139kj(33kcal)", "Vet: 0g", "   Waarvan verzadigd: 0g", "Koolhydraten: 7,9g", "   Waarvan suikers: 7,6g", "Eiwitten: 0g", "Zout: 0.01g", "allergenen: vegetarisch, ", "1. Terug naar het eten & drinken menu" };

            // Act
            var output = new StringWriter();
            Console.SetOut(output);
            drinken.DrinkenMenu();
            
            string convertString = output.ToString();
            string[] split = convertString.Split(new Char[] { '\t', '\n', '\r' },
                                 StringSplitOptions.RemoveEmptyEntries);
            // Assert
            for (int i = 0; i < split.Length; i++)
            {
                Assert.AreEqual(split[i], expected[i]);
            }
        }

        [TestMethod]
        public void DrinkMenu_wrongInp_showItem()
        {
            // Arrange
            Drinken drinken = new Drinken();
            var input = new StringReader(@"a
");
            Console.SetIn(input);
            string[] expected = { "Drinken Menu:", "---------------------------------------------------", "1. Cola - ", "2,99", "2. Pepsi - ", "3,49", "3. Dr.Pepper - ", "2,99", "4. Fanta Orange - ", "2,99", "5. Spa rood - ", "1,99", "6. Spa blauw - ", "1,99", "7. Appelsap - ", "2,49", "8. Rode wijn - ", "6,49", "9. Witte wijn - ", "6,49", "10. Heineken - ", "3,49", "11. Terug naar de vorige pagina", "Typ het nummer van de item die je wilt bekijken en klik op enter:", "De input is niet juist, probeer het nogeens" };

            // Act
            var output = new StringWriter();
            Console.SetOut(output);
            drinken.DrinkenMenu();

            string convertString = output.ToString();
            string[] split = convertString.Split(new Char[] { '\t', '\n', '\r' },
                                 StringSplitOptions.RemoveEmptyEntries);
            // Assert
            for (int i = 0; i < split.Length; i++)
            {
                Assert.AreEqual(split[i], expected[i]);
            }
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
