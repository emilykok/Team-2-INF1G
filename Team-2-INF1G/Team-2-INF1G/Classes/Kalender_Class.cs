using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Formatting = Newtonsoft.Json.Formatting;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Kalender_Class
{
    class Kalender
    {
        public struct KalenderData
        {
            public string[] film1;
            public string[] film2;
            public string[] film3;
            public string[] film4;
            public string[] film5;
            public string[] film6;
        }

        public List<KalenderData> kalenderDataList = new List<KalenderData>();

        [JsonIgnore]
        public string jsonPath;
        [JsonIgnore]
        public string path;

        public Kalender()
        {
            this.path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Kalender_Schema.json"));
            this.jsonPath = File.ReadAllText(path);
            this.kalenderDataList = JsonConvert.DeserializeObject<List<KalenderData>>(jsonPath);
        }

        public static Tuple<string[][], string[][], string[][]> GetDaySchedule(int day)
        {
            int index = day + (3 * day);

            Kalender schedule = new Kalender();
            string[][] theater1 = new string[6][]
            { schedule.kalenderDataList[index].film1,
                schedule.kalenderDataList[index].film2,
                schedule.kalenderDataList[index].film3,
                schedule.kalenderDataList[index].film4,
                schedule.kalenderDataList[index].film5,
                schedule.kalenderDataList[index].film6 };
            index++;
            string[][] theater2 = new string[6][]
                { schedule.kalenderDataList[index].film1,
                schedule.kalenderDataList[index].film2,
                schedule.kalenderDataList[index].film3,
                schedule.kalenderDataList[index].film4,
                schedule.kalenderDataList[index].film5,
                schedule.kalenderDataList[index].film6 };
            index++;
            string[][] theater3 = new string[6][]
                { schedule.kalenderDataList[index].film1,
                schedule.kalenderDataList[index].film2,
                schedule.kalenderDataList[index].film3,
                schedule.kalenderDataList[index].film4,
                schedule.kalenderDataList[index].film5,
                schedule.kalenderDataList[index].film6 };

            return Tuple.Create(theater1, theater2, theater3);
        }

        public static void PrintDaySchedule(int dayNum)
        {
            var day = GetDaySchedule(dayNum);
            Console.WriteLine("\t    Zaal1    \t\t\t\t Zaal2    \t\t\t   Zaal3    ");
            for(int i = 0; i < 6; i++)
            {
                Console.WriteLine($"|{day.Item1[i][0]},\t{day.Item1[i][1]}|" +
                    $"\t|{day.Item2[i][0]},\t{day.Item2[i][1]}|" +
                    $"\t|{day.Item3[i][0]},\t{day.Item3[i][1]}|");
            }
            
        }

        public static void ChooseDay()
        {
            string[] dagen = new string[] { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag" };
            for(int i = 0; i < dagen.Length; i++)
            {
                Console.WriteLine($"{i+1}.\t{dagen[i]}");
            }
        }

        public static void Navigation()
        {
            bool retry = true;
            ChooseDay();
            while (retry)
            {
                Console.WriteLine("vul 'x' in om te stoppen met kijken");
                Console.WriteLine("vul het corresponderende nummer in om het schema van die dag te bekijken");
                var navigation = Console.ReadLine();
                if(navigation == "<")
                {
                    Console.Clear();
                    ChooseDay();
                }
                else if(navigation == "x" || navigation == "X")
                {
                    retry = false;
                }
                else
                {
                    try
                    {
                        int selection = Convert.ToInt32(navigation);
                        Console.Clear();
                        Console.WriteLine("vul '<' in om terug te gaan");
                        PrintDaySchedule(selection - 1);
                    }
                    catch
                    {
                        Console.WriteLine("dit is geen geldige invoerwaarde.");
                    }
                }
            }
        }
    }
}
