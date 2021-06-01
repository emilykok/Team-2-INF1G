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
        // Schema JSON struct
        public struct MovieSchema
        {
            public string title;
            public string hall;
            public string day;
            public string time;
        }

        public List<MovieSchema> movieSchemaList = new List<MovieSchema>();

        [JsonIgnore]
        public string pathSchema;
        [JsonIgnore]
        public string jsonPathSchema;

        public Kalender()
        {
            this.pathSchema = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Movie_Schema.json"));
            this.jsonPathSchema = File.ReadAllText(pathSchema);
            this.movieSchemaList = JsonConvert.DeserializeObject<List<MovieSchema>>(jsonPathSchema);
        }

        public Tuple<string[][], string[][], string[][]> GetDaySchedule(string day)
        {
            // Zaal 1

            for (int i = 0; i < movieSchemaList.Count; i++)
            {
                if (movieSchemaList[i].hall == "Zaal 1" && movieSchemaList[i].day == day)
                {

                }
            }

            for (int i = 0; i < movieSchemaList.Count; i++)
            {
                if (movieSchemaList[i].hall == "Zaal 2" && movieSchemaList[i].day == day)
                {

                }
            }

            for (int i = 0; i < movieSchemaList.Count; i++)
            {
                if (movieSchemaList[i].hall == "Zaal 3" && movieSchemaList[i].day == day)
                {

                }
            }
            return null;
            //return Tuple.Create(theater1, theater2, theater3);
        }

        public static void PrintDaySchedule(string dayNum)
        {
            //var day = GetDaySchedule(dayNum);
            Console.WriteLine("\t    Zaal1    \t\t\t\t Zaal2    \t\t\t   Zaal3    ");
            for(int i = 0; i < 6; i++)
            {
               // Console.WriteLine($"|{day.Item1[i][0]},\t{day.Item1[i][1]}|" +
                //    $"\t|{day.Item2[i][0]},\t{day.Item2[i][1]}|" +
                 //   $"\t|{day.Item3[i][0]},\t{day.Item3[i][1]}|");
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
                        //PrintDaySchedule(selection - 1);
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
