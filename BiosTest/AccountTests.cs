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
    }
}
