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

        public List<TheaterData> theater150DataList = new List<TheaterData>();
        public List<TheaterData> theater300DataList = new List<TheaterData>();
        public List<TheaterData> theater500DataList = new List<TheaterData>();

        [JsonIgnore]
        public string jsonPath150;
        [JsonIgnore]
        public string path150;

        [JsonIgnore]
        public string jsonPath300;
        [JsonIgnore]
        public string path300;

        [JsonIgnore]
        public string jsonPath500;
        [JsonIgnore]
        public string path500;

        public Theater()
        {
            this.path150 = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Zaal150.json"));
            this.jsonPath150 = File.ReadAllText(path150);

            this.path300 = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Zaal300.json"));
            this.jsonPath300 = File.ReadAllText(path300);

            this.path500 = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Zaal500.json"));
            this.jsonPath500 = File.ReadAllText(path500);

            this.theater150DataList = JsonConvert.DeserializeObject<List<TheaterData>>(jsonPath150);
            this.theater300DataList = JsonConvert.DeserializeObject<List<TheaterData>>(jsonPath300);
            this.theater500DataList = JsonConvert.DeserializeObject<List<TheaterData>>(jsonPath500);
        }

        public string ToJSON(int hall)
        {
            List<TheaterData> theaterDataList = WhichTheaterHall(hall);
            return JsonConvert.SerializeObject(theaterDataList, Formatting.Indented);
        }

        public void DeleteSeat(int index, int hall)
        {
            // delete a seat from the json file //
            List<TheaterData> theaterDataList = WhichTheaterHall(hall);
            theaterDataList.RemoveAt(index);

            string path = WhichPath(theaterDataList.Count);
            System.IO.File.WriteAllText(path, ToJSON(hall));
        }

        public List<TheaterData> WhichTheaterHall(int hall)
        {
            if (hall == 1) return theater150DataList;
            else if (hall == 2) return theater300DataList;
            else return theater500DataList;
        }

        public string WhichPath(int length)
        {
            if (length <= 150) return this.path150;
            else if (length <= 300) return this.path300;
            else return this.path500;
        }

        public void ReserveAvailability(int seatIndex, string film, int day, int hall)
        {
            // reserve a seat for a movie //
            List<TheaterData> theaterDataList = WhichTheaterHall(hall);

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

            DeleteSeat(seatIndex, hall);

            theaterDataList.Insert(seatIndex, TD);

            string path = WhichPath(theaterDataList.Count);
            System.IO.File.WriteAllText(path, ToJSON(hall));
        }

        public void RemoveAvailability(int seatIndex, string film, int day, int hall)
        {
            // remove a seat reservation //
            List<TheaterData> theaterDataList = WhichTheaterHall(hall);

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

            DeleteSeat(seatIndex, hall);

            theaterDataList.Insert(seatIndex, TD);

            string path = WhichPath(theaterDataList.Count);
            System.IO.File.WriteAllText(path, ToJSON(hall));
        }

        public static string IsSeatAvailable(int index, string film, int day, int hall)
        {
            // check if the seat is available for that movie //
            Theater choice = new Theater();
            List<TheaterData> theaterDataList = choice.WhichTheaterHall(hall);
            if (theaterDataList[index].availability[day].Length <= 0) return " ";
            else
            {
                for(int i = 0; i < theaterDataList[index].availability[day].Length; i++)
                {
                    // if the movie is already in the array it's not available //
                    if(theaterDataList[index].availability[day][i] == film)
                    {
                        return "X";
                    }
                }
                return " ";
            }
        }

        public static int ChooseSeat(string film, int day, int hall)
        {
            // function to ask the user which seat he would like to reserve //
            Theater choice = new Theater();
            List<TheaterData> theaterDataList = choice.WhichTheaterHall(hall);
            Console.WriteLine("Welke stoel wilt u reserveren?(zorg dat de letter een Hoofdletter is, Bijvoorbeeld: A3.)");
            string seat = Console.ReadLine();
            for(int i = 0; i < theaterDataList.Count; i++)
            {
                // locate the right seat //
                if(seat == theaterDataList[i].position)
                {
                    // seat isn't available //
                    if(IsSeatAvailable(i, film, day, hall) == "X")
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

        public static void Zaal150(string film, int day, int hall)
        {
            // display all the seats in the 150 hall //
            int index = 0;
            
            Console.WriteLine("     1   2   3   4   5   6   7   8   9   10  11  12\n");
            string zaal150RijA = $"A           [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijB = $"B       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijC = $"C       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijD = $"D   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijE = $"E   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijF = $"F   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijG = $"G   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijH = $"H   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijI = $"I   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijJ = $"J   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijK = $"K   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijL = $"L       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijM = $"M           [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijN = $"N           [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";

            Console.WriteLine($"{zaal150RijA}\n{zaal150RijB}\n{zaal150RijC}\n{zaal150RijD}\n{zaal150RijE}\n{zaal150RijF}\n{zaal150RijG}\n{zaal150RijH}\n{zaal150RijI}\n{zaal150RijJ}\n{zaal150RijK}\n{zaal150RijL}\n{zaal150RijM}\n{zaal150RijN}\n");
        }

        public static void Zaal300(string film, int day, int hall)
        {
            // display all the seats in the 300 hall //
            int index = 0;

            Console.WriteLine("     1   2   3   4   5   6      7   8   9  10  11  12     13  14  15  16  17  18\n");
            string zaal150RijA = $"A       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijB = $"B       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijC = $"C       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijD = $"D       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijE = $"E       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijF = $"F       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijG = $"G   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijH = $"H   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijI = $"I   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijJ = $"J   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijK = $"K   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijL = $"L       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijM = $"M       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijN = $"N       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijO = $"O           [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijP = $"P           [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijQ = $"Q           [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijR = $"R               [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijS = $"S               [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";

            Console.WriteLine($"{zaal150RijA}\n{zaal150RijB}\n{zaal150RijC}\n{zaal150RijD}\n{zaal150RijE}\n{zaal150RijF}\n{zaal150RijG}\n{zaal150RijH}\n{zaal150RijI}\n{zaal150RijJ}\n{zaal150RijK}\n{zaal150RijL}\n{zaal150RijM}\n{zaal150RijN}\n{zaal150RijO}\n{zaal150RijP}\n{zaal150RijQ}\n{zaal150RijR}\n{zaal150RijS}");

        }

        public static void Zaal500(string film, int day, int hall)
        {
            // display all the seats in the 500 hall //
            int index = 0;

            Console.WriteLine("     1   2   3   4   5   6   7   8   9  10  11     12  13  14  15  16  17  18  19     20  21  22  23  24  25  26  27  28  29  30\n");
            string zaal150RijA = $"A                   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijB = $"B               [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijC = $"C               [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijD = $"D               [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijE = $"E               [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijF = $"F           [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]\n";
            string zaal150RijG = $"G       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijH = $"H   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijI = $"I   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijJ = $"J   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijK = $"K   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]\n";
            string zaal150RijL = $"L   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijM = $"M       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijN = $"N           [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijO = $"O           [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijP = $"P               [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijQ = $"Q               [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijR = $"R                       [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] ";
            string zaal150RijS = $"S                               [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";
            string zaal150RijT = $"T                                   [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]    [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}] [{IsSeatAvailable(index++, film, day, hall)}]";

            Console.WriteLine($"{zaal150RijA}\n{zaal150RijB}\n{zaal150RijC}\n{zaal150RijD}\n{zaal150RijE}\n{zaal150RijF}\n{zaal150RijG}\n{zaal150RijH}\n{zaal150RijI}\n{zaal150RijJ}\n{zaal150RijK}\n{zaal150RijL}\n{zaal150RijM}\n{zaal150RijN}\n{zaal150RijO}\n{zaal150RijP}\n{zaal150RijQ}\n{zaal150RijR}\n{zaal150RijS}\n{zaal150RijT}");

        }

        public static void Run(string film, int day, string zaal)
        {
            // function to run the theater class //
            int hall = 0;
            if (zaal == "zaal 1") hall = 1;
            else if (zaal == "zaal 2") hall = 2;
            else if (zaal == "zaal 3") hall = 3;
            Theater choice = new Theater();
            bool retry = true;
            string prompt = "Kies hier welke stoel u wilt reserveren\n--------------------------------------------------------\n";
            while (retry)
            {
                // show the seats and ask which seat the user wants //
                Console.Clear();
                Console.WriteLine(prompt);
                if (hall == 1) Zaal150(film, day, hall);
                else if (hall == 2) Zaal300(film, day, hall);
                else if (hall == 3)Zaal500(film, day, hall);
                int seat = (ChooseSeat(film, day, hall));
                if(seat != -1)
                {
                    // the seat will be reserved and the user is asked if he is sure //
                    choice.ReserveAvailability(seat, film, day, hall);
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
                        choice.RemoveAvailability(seat, film, day, hall);
                    }
                    // the user wants to cancel the seat selection //
                    else if(answer == "x" || answer == "X")
                    {
                        choice.RemoveAvailability(seat, film, day, hall);
                        retry = false;
                    }
                    else
                    {
                        choice.RemoveAvailability(seat, film, day, hall);
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