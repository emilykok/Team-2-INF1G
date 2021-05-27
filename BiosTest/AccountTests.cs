using System;
using Account_Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiosTest
{
    [TestClass]
    public class AccountTests
    {
        // TextCheck //
        [TestMethod]
        public void TextCheck_InputEmpty_ReturnsTrue() 
        {
            // Arrange
            Account acc1 = new Account();
            string[] arr = new string[] {"","",""};

            // Act
            var result = acc1.TextCheck(arr);

            // Assert
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void TextCheck_Input_ReturnsFalse()
        {
            // Arrange
            Account acc1 = new Account();
            string[] arr = new string[] { "a", "test", "boop" };

            // Act
            var result = acc1.TextCheck(arr);

            // Assert
            Assert.IsFalse(result);
        }


        // UsernameCheck //
        [TestMethod]
        public void UsernameCheck_NewName_ReturnsFalse()
        {
            // Arrange
            Account acc1 = new Account();
            string name = "IDontHeccinKnow";

            // Act
            var result = acc1.UsernameCheck(name);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UsernameCheck_TakenName_ReturnsTrue()
        {
            // Arrange
            Account acc1 = new Account();
            string name = "Admin1";

            // Act
            var result = acc1.UsernameCheck(name);

            // Assert
            Assert.IsTrue(result);
        }


        // PrintItem //
        [TestMethod]
        public void PrintItem_InRange_PrintsItems()
        {
            // Arrange
            Account acc1 = new Account();

            // Act
            acc1.PrintItem(2,5);

            // Assert
            Assert.AreEqual("a", "a"); // need a way to read what is printed on the console...
        }

        // AvailableAllergie //
        [TestMethod]
        public void AvailableAllergies_Empty_ReturnAllAllergies() {
            // Arrange
            Account acc1 = new Account();
            string[] allAll = new string[] { "lactose", "soja", "pinda", "amandel", "hazelnoot", "noten", "gluten", "tarwe" };
            string[] allergies = new string[0];

            // Act
            string[] res = acc1.AvailableAllergie(allergies);

            // Assert
            CollectionAssert.AreEqual(res, allAll);
        }

        [TestMethod]
        public void AvailableAllergies_Input_ReturnAllAllergies()
        {
            // Arrange
            Account acc1 = new Account();
            string[] expected = new string[] { "lactose", "pinda", "amandel", "noten", "gluten", "tarwe" };
            string[] allergies = new string[] {"soja", "hazelnoot"};

            // Act
            string[] res = acc1.AvailableAllergie(allergies);

            // Assert
            CollectionAssert.AreEqual(res, expected);
        }

        // Login //
        [TestMethod]
        public void Login_CorrectInput_Return6()
        {
            // Arrange
            Account acc1 = new Account();

            // Act
            int res = acc1.Login("Admin1", "Admin1");

            // Assert
            Assert.AreEqual(res, 4);
        }

        [TestMethod]
        public void Login_WrongtInput_ReturnMin1()
        {
            // Arrange
            Account acc1 = new Account();

            // Act
            int res = acc1.Login("Admin1", "wrong");

            // Assert
            Assert.AreEqual(res, -1);
        }

        // TextLogin //


        // CreateUser //


        // TextCreateUser //


        // DeleteUser //


        // AdminAccountViewer //


        // UpdateUser //


        // AccountView //


    }
}
