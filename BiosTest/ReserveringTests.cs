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

13
r
0

1
x
");
            Console.SetIn(input);
            string res = resv1.PersonAmount();

            // Assert
            Assert.AreNotEqual("3", res); // checks the value's BE CAREFUL WITH REFERENCES!
        }

        [TestMethod]
        public void HallNDate_CorrectInput()
        {
            // Arrange
            Reservering resv1 = new Reservering();

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"1

");
            Console.SetIn(input);
            string[] res = resv1.HallNDate("The Broken Hearts Gallery");

            string[] expected = { "The Broken Hearts Gallery", "Zaal 3", "Donderdag", "11:45"};
            // Assert
            CollectionAssert.AreEqual(expected, res); // checks the value's BE CAREFUL WITH REFERENCES!
        }
        [TestMethod]
        public void HallNDate_IncorrectInput()
        {
            // Arrange
            Reservering resv1 = new Reservering();

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"what

1
r
3

x
x
");
            Console.SetIn(input);
            string[] res = resv1.HallNDate("The Broken Hearts Gallery");

            string[] expected = { "The Broken Hearts Gallery", "Zaal 3", "Donderdag", "11:45" };
            // Assert
            CollectionAssert.AreNotEqual(expected, res); // checks the value's BE CAREFUL WITH REFERENCES!
        }
        [TestMethod]
        public void HallNDate_NoFilm()
        {
            // Arrange
            Reservering resv1 = new Reservering();

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"1

");
            Console.SetIn(input);
            string[] res = resv1.HallNDate("The Broken Heart Gallery");

            string[] expected = { "The Broken Hearts Gallery", "Zaal 3", "Donderdag", "11:45" };
            // Assert
            CollectionAssert.AreNotEqual(expected, res); // checks the value's BE CAREFUL WITH REFERENCES!
        }

        // CreateTicket //
        [TestMethod]
        public void Create_Ticket_1_person()
        {
            // Arrange
            Reservering resv1 = new Reservering();

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"1

1

A5


");
            Console.SetIn(input);
            resv1.CreateTicket("The Broken Hearts Gallery", 4);

            string expected = "The Broken Hearts Gallery";
            // Assert
            Assert.AreEqual(expected, resv1.TicketsList[1].filmName); // checks the value's BE CAREFUL WITH REFERENCES!
            resv1.DeleteTicket(resv1.TicketsList[1].reservationNumber);
        }

        [TestMethod]
        public void Create_Ticket_2_person()
        {
            // Arrange
            Reservering resv1 = new Reservering();

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"1

2

A5

A6


");
            Console.SetIn(input);
            resv1.CreateTicket("The Broken Hearts Gallery", 4);

            string expected = "The Broken Hearts Gallery";
            // Assert
            Assert.AreEqual(expected, resv1.TicketsList[1].filmName); // checks the value's BE CAREFUL WITH REFERENCES!
            resv1.DeleteTicket(resv1.TicketsList[1].reservationNumber);
        }

        // ReservationNumberIndexer //
        [TestMethod]
        public void ReservationNumberIndexer_Found()
        {
            // Arrange
            Reservering resv1 = new Reservering();

            // Act

            int res = resv1.ReservationNumberIndexer(1);

            // Assert
            Assert.AreEqual(0, res); // checks the value's BE CAREFUL WITH REFERENCES!
        }
        [TestMethod]
        public void ReservationNumberIndexer_notFound()
        {
            // Arrange
            Reservering resv1 = new Reservering();

            // Act

            int res = resv1.ReservationNumberIndexer(2);

            // Assert
            Assert.AreEqual(-1, res); // checks the value's BE CAREFUL WITH REFERENCES!
        }
    }
}
