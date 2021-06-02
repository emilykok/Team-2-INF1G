using System;
using System.IO;
using Kalender_Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiosTest
{
    [TestClass]
    public class KalenderTests
    {
        // GetDaySchedule //
        [TestMethod]
        public void Correct_Printed_Movies_Maandag()
        {
            // Arrange
            Kalender kal1 = new Kalender();

            // Act
            var output = new StringWriter();
            Console.SetOut(output);

            Kalender.PrintDaySchedule(0, kal1.movieSchemaList);
            // Assert
            // All expected MUST be written with @
            string expected = @"Zaal 1
9:00 | The Witches
11:15 | The Beast
13:15 | Code 8
15:15 | Jumanji: The Next Level
17:45 | The Half of It
20:00 | A Quiet Place Part 2

Zaal 2
9:15 | Bad Boys for Life
11:45 | Words on Bathroom Walls
14:00 | Avengers: Endgame
17:30 | Initiation
19:30 | Captain Marvel
22:00 | Host

Zaal 3
9:30 | Dolittle
11:30 | Valley Girl
13:15 | Deadpool
16:00 | Onward
18:15 | The Happiest Season

";

            Assert.AreEqual(expected, output.ToString());
        }

        [TestMethod]
        public void Correct_Printed_Movies_Dinsdag()
        {
            // Arrange
            Kalender kal1 = new Kalender();

            // Act
            var output = new StringWriter();
            Console.SetOut(output);

            Kalender.PrintDaySchedule(1, kal1.movieSchemaList);
            // Assert
            // All expected MUST be written with @
            string expected = @"Zaal 1
9:00 | Onward
11:15 | Happiest Season
13:30 | IO
15:30 | Spiderman: Far from Home
18:00 | Deadpool
20:15 | The Invisible Man

Zaal 2
9:15 | Borat Subsequent Movie Film
11:15 | X-Men: Dark Phoenix
13:30 | Star Wars: The Rise of Skywalker
16:15 | Alita: Battle Angel
18:45 | Men in Black: International
21:00 | The Call

Zaal 3
20:30 | Rose: A Love Story
9:30 | Jumanji: The Next Level
11:30 | Titanic
15:15 | The Kissing Booth 2
17:45 | Vanguard
20:00 | Code 8
22:00 | Come True

";

            Assert.AreEqual(expected, output.ToString());
        }

        [TestMethod]
        public void Correct_Printed_Movies_Woensdag()
        {
            // Arrange
            Kalender kal1 = new Kalender();

            // Act
            var output = new StringWriter();
            Console.SetOut(output);

            Kalender.PrintDaySchedule(2, kal1.movieSchemaList);
            // Assert
            // All expected MUST be written with @
            string expected = @"Zaal 1
9:00 | Knives Out
11:30 | Avengers: Endgame
15:00 | X-Men: Dark Phoenix
17:15 | Underwater
19:15 | I Am Mother
21:30 | The Night House

Zaal 2
9:15 | Men in Black: International
11:30 | Spiderman: Far from Home
14:00 | Captain Marvel
16:30 | Force of Nature
18:30 | Bad Boys for Life
21:00 | Host
14:15 | Dirty Dancing

Zaal 3
9:30 | Dora and the Lost City of Gold
11:45 | Alita: Battle Angel
16:15 | Bloodshot
18:30 | Dolittle
20:30 | Possessor

";

            Assert.AreEqual(expected, output.ToString());
        }

        [TestMethod]
        public void Correct_Printed_Movies_Donderdag()
        {
            // Arrange
            Kalender kal1 = new Kalender();

            // Act
            var output = new StringWriter();
            Console.SetOut(output);

            Kalender.PrintDaySchedule(3, kal1.movieSchemaList);
            // Assert
            // All expected MUST be written with @
            string expected = @"Zaal 1
9:00 | IO
11:00 | Honest Thief
13:00 | Men in Black: International
15:15 | Extraction
17:30 | Onward
19:45 | Hunter Hunter

Zaal 2
9:15 | Coming 2 America
11:45 | Survive the Night
13:45 | Godzilla: King of the Monsters
16:30 | Survive the Night
18:30 | Jumanji: The Next Level
21:00 | Initiation

Zaal 3
9:30 | I Am Mother
11:45 | The Broken Hearts Gallery
13:45 | Titanic
17:30 | Honest Thief
19:30 | Dora and the Lost City of Gold
21:45 | A Quiet Place Part 2

";

            Assert.AreEqual(expected, output.ToString());
        }

        [TestMethod]
        public void Correct_Printed_Movies_Vrijdag()
        {
            // Arrange
            Kalender kal1 = new Kalender();

            // Act
            var output = new StringWriter();
            Console.SetOut(output);

            Kalender.PrintDaySchedule(4, kal1.movieSchemaList);
            // Assert
            // All expected MUST be written with @
            string expected = @"Zaal 1
9:00 | Jumanji: The Next Level
11:15 | Godzilla: King of the Monsters
13:45 | Deadpool
16:00 | The Beast
18:00 | Extraction
20:15 | Rose: A Love Story

Zaal 2
9:15 | Ad Astra
11:45 | Dirty Dancing
13:45 | Code 8
15:45 | Gisaengchung(Parasite)
18:15 | Bloodshot
20:30 | The Invisible Man

Zaal 3
9:30 | The Kissing Booth 2
12:00 | Star Wars: The Rise of Skywalker
14:45 | Force of Nature
16:45 | Coming 2 America
19:00 | Underwater
21:00 | The Call

";

            Assert.AreEqual(expected, output.ToString());
        }

        [TestMethod]
        public void Correct_Printed_Movies_Zaterdag()
        {
            // Arrange
            Kalender kal1 = new Kalender();

            // Act
            var output = new StringWriter();
            Console.SetOut(output);

            Kalender.PrintDaySchedule(5, kal1.movieSchemaList);
            // Assert
            // All expected MUST be written with @
            string expected = @"Zaal 1
9:00 | Gisaengchung(Parasite)
11:45 | Bloodshot
13:45 | Happiest Season
16:00 | Knives Out
18:30 | Alita: Battle Angel
21:00 | Come True

Zaal 2
9:15 | Captain Marvel
11:45 | The Half of It
13:45 | The Lovebirds
15:30 | Dora and the Lost City of Gold
17:45 | Spiderman: Far from Home
20:15 | The Night House

Zaal 3
9:30 | Onward
12:00 | Extraction
14:15 | The Half of It
16:15 | Ad Astra
18:45 | Honest Thief
20:45 | Host

";

            Assert.AreEqual(expected, output.ToString());
        }

        [TestMethod]
        public void Correct_Printed_Movies_Zondag()
        {
            // Arrange
            Kalender kal1 = new Kalender();

            // Act
            var output = new StringWriter();
            Console.SetOut(output);

            Kalender.PrintDaySchedule(6, kal1.movieSchemaList);
            // Assert
            // All expected MUST be written with @
            string expected = @"Zaal 1
9:00 | The Lovebirds
10:45 | Underwater
12:45 | Valley Girl
14:45 | Bad Boys for Life
17:15 | Coming 2 America
19:30 | Possessor

Zaal 2
9:15 | The Witches
11:30 | Vanguard
13:45 | Words on the Bathroom Walls
16:00 | Borat Subsequent Movie Film
18:00 | Ad Astra
20:30 | Hunter Hunter

Zaal 3
9:30 | Dolittle
11:30 | Alita: Battle Angel
14:00 | The Broken Hearts gallery
16:15 | I Am Mother
18:30 | X-Men: Dark Phoenix
20:45 | A Quiet Place Part 2

";

            Assert.AreEqual(expected, output.ToString());
        }

        // PrintDaySchedule //

        // ChooseDay //
    }
}
