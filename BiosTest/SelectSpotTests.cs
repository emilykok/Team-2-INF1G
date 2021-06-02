using System;
using System.IO;
using SelectSpot_Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiosTest
{
    [TestClass]
    public class SelectSpotTests
    {
        // DeleteSeat //
        [TestMethod]
        public void DeleteSeat_InputInts_ReturnsVoid()
        {
            // Arrange
            Theater tet = new Theater();
            Theater.TheaterData TD = new Theater.TheaterData();
            TD.position = "TestSeat";
            TD.soort = "Test";
            TD.availability = new string[7][];

            int hall = 1;

            tet.theater150DataList.Add(TD);
            System.IO.File.WriteAllText(tet.path150, tet.ToJSON(hall));
            tet = new Theater();
            int index = tet.theater150DataList.Count - 1;

            // Act
            tet.DeleteSeat(index, hall);

            // Assert
            Assert.AreNotEqual("TestSeat", tet.theater150DataList[tet.theater150DataList.Count - 1].position);
        }

        // WhichTheaterHall //
        [TestMethod]
        public void WhichTheaterHall_InputInt_ReturnsList1()
        {
            // Arrange
            int hall = 1;
            Theater tet = new Theater();
            var expected = tet.theater150DataList;

            // Act
            var result = tet.WhichTheaterHall(hall);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WhichTheaterHall_InputInt_ReturnsList2()
        {
            // Arrange
            int hall = 2;
            Theater tet = new Theater();
            var expected = tet.theater300DataList;

            // Act
            var result = tet.WhichTheaterHall(hall);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WhichTheaterHall_InputInt_ReturnsList3()
        {
            // Arrange
            int hall = 3;
            Theater tet = new Theater();
            var expected = tet.theater500DataList;

            // Act
            var result = tet.WhichTheaterHall(hall);

            // Assert
            Assert.AreEqual(expected, result);
        }

        // WhichPath //
        [TestMethod]
        public void WhichPath_InputInt_ReturnsString1()
        {
            // Arrange
            int length = 150;
            Theater tet = new Theater();
            string expected = tet.path150;

            // Act
            var result = tet.WhichPath(length);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WhichPath_InputInt_ReturnsString2()
        {
            // Arrange
            int length = 300;
            Theater tet = new Theater();
            string expected = tet.path300;

            // Act
            var result = tet.WhichPath(length);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WhichPath_InputInt_ReturnsString3()
        {
            // Arrange
            int length = 500;
            Theater tet = new Theater();
            string expected = tet.path500;

            // Act
            var result = tet.WhichPath(length);

            // Assert
            Assert.AreEqual(expected, result);
        }

        // ReserveAvailability //
        [TestMethod]
        public void ReserveAvailability_MultiInput_ReturnsVoid()
        {
            // Arrange
            int seatIndex = 0;
            string film = "TestFilm";
            int day = 0;
            int hall = 1;

            Theater tet = new Theater();
            var notExpected = tet.theater150DataList[seatIndex].availability[day];

            // Act
            tet.ReserveAvailability(seatIndex, film, day, hall);

            // Assert
            CollectionAssert.AreNotEqual(notExpected, tet.theater150DataList[seatIndex].availability[day]);


            tet.RemoveAvailability(seatIndex, film, day, hall);
        }

        // RemoveAvailability //
        [TestMethod]
        public void RemoveAvailability_MultiInput_ReturnsVoid()
        {
            // Arrange
            int seatIndex = 0;
            string film = "TestFilm";
            int day = 0;
            int hall = 1;

            Theater tet = new Theater();

            tet.ReserveAvailability(seatIndex, film, day, hall);
            var notExpected = tet.theater150DataList[seatIndex].availability[day];

            // Act
            tet.RemoveAvailability(seatIndex, film, day, hall);

            // Assert
            CollectionAssert.AreNotEqual(notExpected, tet.theater150DataList[seatIndex].availability[day]);
        }

        // IsSeatAvailable //
        [TestMethod]
        public void IsSeatAvailable_MultiInput_ReturnsXString()
        {
            // Arrange
            int index = 0;
            string film = "Onward";
            int day = 0;
            int hall = 1;

            Theater tet = new Theater();

            tet.ReserveAvailability(index, film, day, hall);

            // Act
            string result = Theater.IsSeatAvailable(index, film, day, hall);

            // Assert
            Assert.AreEqual("X", result);


            tet.RemoveAvailability(index, film, day, hall);
        }

        [TestMethod]
        public void IsSeatAvailable_MultiInput_ReturnsSpaceString()
        {
            // Arrange
            int index = 0;
            string film = "Onward";
            int day = 0;
            int hall = 1;

            Theater tet = new Theater();

            // Act
            string result = Theater.IsSeatAvailable(index, film, day, hall);

            // Assert
            Assert.AreEqual(" ", result);
        }

        // ChooseSeat //
        [TestMethod]
        public void ChooseSeat_MultiInput_ReturnsTuple_AR()
        {
            // Arrange
            int index = 0;
            string film = "Onward";
            int day = 0;
            int hall = 1;
            Tuple<int, string> expected = Tuple.Create(-1, "");

            Theater tet = new Theater();

            tet.ReserveAvailability(index, film, day, hall);

            // Act
            var input = new StringReader(@"A3
");
            Console.SetIn(input);

            var result = Theater.ChooseSeat(film, day, hall);

            // Assert
            Assert.AreEqual(expected, result);


            tet.RemoveAvailability(index, film, day, hall);
        }

        [TestMethod]
        public void ChooseSeat_MultiInput_ReturnsTuple_IV()
        {
            // Arrange
            int index = 0;
            string film = "Onward";
            int day = 0;
            int hall = 1;
            Tuple<int, string> expected = Tuple.Create(-1, "");

            Theater tet = new Theater();

            tet.ReserveAvailability(index, film, day, hall);

            // Act
            var input = new StringReader(@"dep
");
            Console.SetIn(input);

            var result = Theater.ChooseSeat(film, day, hall);

            // Assert
            Assert.AreEqual(expected, result);


            tet.RemoveAvailability(index, film, day, hall);
        }

        [TestMethod]
        public void ChooseSeat_MultiInput_ReturnsTuple()
        {
            // Arrange
            string film = "Onward";
            int day = 0;
            int hall = 1;
            Tuple<int, string> expected = Tuple.Create(0, "A3");

            Theater tet = new Theater();

            // Act
            var input = new StringReader(@"A3
");
            Console.SetIn(input);

            var result = Theater.ChooseSeat(film, day, hall);

            // Assert
            Assert.AreEqual(expected, result);
        }

        // Zaal150 //

        // Zaal300 //

        // Zaal500 //

        // Run //
    }
}
