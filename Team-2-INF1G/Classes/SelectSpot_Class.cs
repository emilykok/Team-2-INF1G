using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Formatting = Newtonsoft.Json.Formatting;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;


namespace SelectSpot_Class
{
    public class Theater
    {
        public struct TheaterData
        {
            public string position;
            public string soort;
            public string[][] availability;
        }

        public List<TheaterData> theaterDataList = new List<TheaterData>();

        [JsonIgnore]
        public string jsonPath;
        [JsonIgnore]
        public string path;

        public Theater()
        {
            // this.path is used in serializing the json data.
            this.path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Zaal150.json"));
            // this.jsonPath is used in deserializing the json data.
            this.jsonPath = File.ReadAllText(path);
            // the constructor loads the json file first, so it can be modified later in the file.
            this.theaterDataList = JsonConvert.DeserializeObject<List<TheaterData>>(jsonPath);
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this.theaterDataList, Formatting.Indented);
        }

        public void DeleteSeat(int index)
        {
            // delete a seat from the json file //
            theaterDataList.RemoveAt(index);

            System.IO.File.WriteAllText(this.path, ToJSON());
        }

        public void ReserveAvailability(int seatIndex, string film, int day)
        {
            // reserve a seat for a movie //
            string Posit    = theaterDataList[seatIndex].position;
            string kind     = theaterDataList[seatIndex].soort;
            string[][] ava    = theaterDataList[seatIndex].availability;

            // add the movietitle to the list of reserved movies for that seat //
            string[] eve = new string[ava[day].Length+1];
            for(int i = 0; i < ava[day].Length; i++)
            {
                eve[i] = ava[day][i];
            }
            eve[eve.Length - 1] = film;
            ava[day] = eve;

            // renew the json item with the added change //
            TheaterData TD = new TheaterData();
            TD.position     = Posit;
            TD.soort        = kind;
            TD.availability = ava;

            DeleteSeat(seatIndex);

            theaterDataList.Insert(seatIndex, TD);

            System.IO.File.WriteAllText(this.path, ToJSON());
        }

        public void RemoveAvailability(int seatIndex, string film, int day)
        {
            // remove a seat reservation //
            string Posit = theaterDataList[seatIndex].position;
            string kind = theaterDataList[seatIndex].soort;
            string[][] ava = theaterDataList[seatIndex].availability;

            // locate the movietitle for which it was reserved and remove it //
            string[] eve = new string[ava.Length - 1];
            for (int i = 0, count = 0; i < ava[day].Length; i++)
            {
                if(film != ava[day][i])
                {
                    eve[count++] = ava[day][i];
                }
            }
            ava[day] = eve;

            // renew the json item with the added change //
            TheaterData TD = new TheaterData();
            TD.position = Posit;
            TD.soort = kind;
            TD.availability = ava;

            DeleteSeat(seatIndex);

            theaterDataList.Insert(seatIndex, TD);

            System.IO.File.WriteAllText(this.path, ToJSON());
        }

        public static string IsSeatAvailable(int index, string film, int day)
        {
            // check if the seat is available for that movie //
            Theater choice = new Theater();
            if (choice.theaterDataList[index].availability[day].Length <= 0) return " ";
            else
            {
                for(int i = 0; i < choice.theaterDataList[index].availability[day].Length; i++)
                {
                    // if the movie is already in the array it's not available //
                    if(choice.theaterDataList[index].availability[day][i] == film)
                    {
                        return "X";
                    }
                }
                return " ";
            }
        }

        public static int ChooseSeat(string film, int day)
        {
            // function to ask the user which seat he would like to reserve //
            Theater choice = new Theater();
            Console.WriteLine("Welke stoel wilt u reserveren?(zorg dat de letter een Hoofdletter is, Bijvoorbeeld: A3.)");
            string seat = Console.ReadLine();
            for(int i = 0; i < choice.theaterDataList.Count; i++)
            {
                // locate the right seat //
                if(seat == choice.theaterDataList[i].position)
                {
                    // seat isn't available //
                    if(IsSeatAvailable(i, film, day) == "X")
                    {
                        Console.WriteLine($"Stoel nummer {seat} is al gereserveerd");
                        return -1;
                    }
                    // seat is available //
                    else
                    {
                        Console.WriteLine($"Stoel nummer {seat} is geselecteerd");
                        return i;
                    }
                }
            }
            // invalid input //
            Console.WriteLine("Dit is geen geldige invoerwaarde");
            return -1;
        }

        public static void Zaal150(string film, int day)
        {
            // display all the seats //
            int index = 0;
            
            Console.WriteLine("     1   2   3   4   5   6   7   8   9   10  11  12\n");
            string zaal150RijA = $"A           [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijB = $"B       [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijC = $"C       [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijD = $"D   [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijE = $"E   [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijF = $"F   [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijG = $"G   [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijH = $"H   [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijI = $"I   [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijJ = $"J   [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijK = $"K   [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijL = $"L       [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijM = $"M           [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";
            string zaal150RijN = $"N           [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}] [{IsSeatAvailable(index++, film, day)}]";

            Console.WriteLine($"{zaal150RijA}\n{zaal150RijB}\n{zaal150RijC}\n{zaal150RijD}\n{zaal150RijE}\n{zaal150RijF}\n{zaal150RijG}\n{zaal150RijH}\n{zaal150RijI}\n{zaal150RijJ}\n{zaal150RijK}\n{zaal150RijL}\n{zaal150RijM}\n{zaal150RijN}\n");
        }

        public static void Zaal300()
        {
            string A1 = " "; // moet opgevraagd gaan worden uit json

            Console.WriteLine("     1   2   3   4   5   6      7   8   9  10  11  12     13  14  15  16  17  18\n");
            string zaal150RijA = $"A       [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijB = $"B       [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijC = $"C       [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijD = $"D       [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijE = $"E       [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijF = $"F       [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijG = $"G   [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijH = $"H   [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijI = $"I   [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijJ = $"J   [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijK = $"K   [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijL = $"L       [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijM = $"M       [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijN = $"N       [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijO = $"O           [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijP = $"P           [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijQ = $"Q           [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijR = $"R               [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}]";
            string zaal150RijS = $"S               [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}]";

            Console.WriteLine($"{zaal150RijA}\n{zaal150RijB}\n{zaal150RijC}\n{zaal150RijD}\n{zaal150RijE}\n{zaal150RijF}\n{zaal150RijG}\n{zaal150RijH}\n{zaal150RijI}\n{zaal150RijJ}\n{zaal150RijK}\n{zaal150RijL}\n{zaal150RijM}\n{zaal150RijN}\n{zaal150RijO}\n{zaal150RijP}\n{zaal150RijQ}\n{zaal150RijR}\n{zaal150RijS}");

        }

        public static void Zaal500()
        {
            string A1 = " "; // moet opgevraagd gaan worden uit json

            Console.WriteLine("     1   2   3   4   5   6   7   8   9  10  11     12  13  14  15  16  17  18  19     20  21  22  23  24  25  26  27  28  29  30\n");
            string zaal150RijA = $"A                   [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijB = $"B               [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijC = $"C               [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijD = $"D               [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijE = $"E               [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijF = $"F           [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]\n";
            string zaal150RijG = $"G       [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijH = $"H   [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijI = $"I   [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijJ = $"J   [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijK = $"K   [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]\n";
            string zaal150RijL = $"L   [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijM = $"M       [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijN = $"N           [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijO = $"O           [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijP = $"P               [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijQ = $"Q               [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijR = $"R                       [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] ";
            string zaal150RijS = $"S                               [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}]";
            string zaal150RijT = $"T                                   [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}] [{A1}]    [{A1}] [{A1}] [{A1}]";

            Console.WriteLine($"{zaal150RijA}\n{zaal150RijB}\n{zaal150RijC}\n{zaal150RijD}\n{zaal150RijE}\n{zaal150RijF}\n{zaal150RijG}\n{zaal150RijH}\n{zaal150RijI}\n{zaal150RijJ}\n{zaal150RijK}\n{zaal150RijL}\n{zaal150RijM}\n{zaal150RijN}\n{zaal150RijO}\n{zaal150RijP}\n{zaal150RijQ}\n{zaal150RijR}\n{zaal150RijS}\n{zaal150RijT}");

        }

        public static void Run(string film, int day)
        {
            // function to run the theater class //
            Theater choice = new Theater();
            bool retry = true;
            string prompt = "Kies hier welke stoel u wilt reserveren\n--------------------------------------------------------\n";
            while (retry)
            {
                // show the seats and ask which seat the user wants //
                Console.Clear();
                Console.WriteLine(prompt);
                Zaal150(film, day);
                int seat = (ChooseSeat(film, day));
                if(seat != -1)
                {
                    // the seat will be reserved and the user is asked if he is sure //
                    choice.ReserveAvailability(seat, film, day);
                    Console.WriteLine("weet u het zeker 'ja'. vul anders 'nee' in. vul 'x' in om de reservering te annuleren.");
                    string answer = Console.ReadLine();
                    // the user is sure //
                    if(answer == "ja")
                    {
                        retry = false;
                    }
                    // the user wants to rechoose //
                    else if(answer == "nee")
                    {
                        choice.RemoveAvailability(seat, film, day);
                    }
                    // the user wants to cancel the seat selection //
                    else if(answer == "x" || answer == "X")
                    {
                        choice.RemoveAvailability(seat, film, day);
                        retry = false;
                    }
                }
                // invalid input or reserved seat // 
                else
                {
                    Console.WriteLine("Wilt u het opniew proberen? 'ja/nee'.");
                    string answer = Console.ReadLine();
                    // 'nee' cancels the selection, anything else will repeat the loop //
                    if(answer == "nee")
                    {
                        retry = false;
                    }
                }
                //Zaal300();
                //Zaal500();
            }
                
        }
    }
}