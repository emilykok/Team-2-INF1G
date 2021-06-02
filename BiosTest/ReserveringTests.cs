using System;
using System.IO;
using Reservatie_Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiosTest
{
    [TestClass]
    public class ReserveringTests
    {
        // PersonAmount //
        [TestMethod]
        public void PersonAmount_CorrectInput()
        {
            // Arrange
            Reservering resv1 = new Reservering();

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"3

");
            Console.SetIn(input);
            string res = resv1.PersonAmount();

            // Assert
            Assert.AreEqual("3", res); // checks the value's BE CAREFUL WITH REFERENCES!
        }
        [TestMethod]
        public void PersonAmount_IncorrectInput()
        {
            // Arrange
            Reservering resv1 = new Reservering();

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"what

r
r
0

x
");
            Console.SetIn(input);
            string res = resv1.PersonAmount();

            // Assert
            Assert.AreNotEqual("3", res); // checks the value's BE CAREFUL WITH REFERENCES!
        }
    }
}
