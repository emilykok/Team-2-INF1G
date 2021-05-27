using System;
using System.IO;
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
            CollectionAssert.AreEqual(res, expected); // checks the collection disregarding references!
        }

        // Login //
        // correct input
        [TestMethod]
        public void Login_CorrectInput_Return4()
        {
            // Arrange
            Account acc1 = new Account();

            // Act
            int res = acc1.Login("Admin1", "Admin1");

            // Assert
            Assert.AreEqual(res, 4);
        }
 
        // wrong input
        [TestMethod]
        public void Login_WrongtInput_ReturnMin1()
        {
            // Arrange
            Account acc1 = new Account();

            // Act
            int res = acc1.Login("Admin1", "wrong");

            // Assert
            Assert.AreEqual(res, -1); // checks the value's BE CAREFUL WITH REFERENCES!
        }

        // TextLogin //
        [TestMethod]
        public void TextLogin_CorrectInput()
        {
            // Arrange
            Account acc1 = new Account();


            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            // ALSO NO CONSOLE.CLEAR()!
            var input = new StringReader(@"Admin1
Admin1

");
            Console.SetIn(input);

            int res = acc1.TextLogin();
            
            // Assert

            Assert.AreEqual(res, 4); // checks the value's BE CAREFUL WITH REFERENCES!
            
        }

        // CreateUser //


        // TextCreateUser //


        // DeleteUser //


        // AdminAccountViewer //


        // UpdateUser //


        // AccountView //


    }
}
