using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

using Console_Buffer;

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

        public static Tuple<List<string[]>, List<string[]>, List<string[]>> GetDaySchedule(string day, List<MovieSchema> movieSchemaList)
        {

            // Zaal 1
            var Zaal_1_List = new List<string[]>();
            for (int i = 0; i < movieSchemaList.Count; i++)
            {
                if (movieSchemaList[i].hall == "Zaal 1" && movieSchemaList[i].day == day)
                {
                    string[] TT = { movieSchemaList[i].title, movieSchemaList[i].time };
                    Zaal_1_List.Add(TT);
                }
            }

            // Zaal 2
            var Zaal_2_List = new List<string[]>();
            for (int i = 0; i < movieSchemaList.Count; i++)
            {
                if (movieSchemaList[i].hall == "Zaal 2" && movieSchemaList[i].day == day)
                {
                    string[] TT = { movieSchemaList[i].title, movieSchemaList[i].time };
                    Zaal_2_List.Add(TT);
                }
            }

            // Zaal 3
            var Zaal_3_List = new List<string[]>();
            for (int i = 0; i < movieSchemaList.Count; i++)
            {
                if (movieSchemaList[i].hall == "Zaal 3" && movieSchemaList[i].day == day)
                {
                    string[] TT = { movieSchemaList[i].title, movieSchemaList[i].time };
                    Zaal_3_List.Add(TT);
                }
            }

            return Tuple.Create(Zaal_1_List, Zaal_2_List, Zaal_3_List);
        }

        public static void PrintDaySchedule(int dag, List<MovieSchema> movieSchemaList)
        {
            // Convert int to string, used in GetDaySchedule
            string day = "";
            if (dag == 0) day = "Maandag";
            else if (dag == 1) day = "Dinsdag";
            else if (dag == 2) day = "Woensdag";
            else if (dag == 3) day = "Donderdag";
            else if (dag == 4) day = "Vrijdag";
            else if (dag == 5) day = "Zaterdag";
            else if (dag == 6) day = "Zondag";


            Tuple<List<string[]>, List<string[]>, List<string[]>> Schedule = GetDaySchedule(day, movieSchemaList);

            Console.WriteLine("Zaal 1");
            foreach (string[] TT in Schedule.Item1)
            {
                Console.WriteLine($"{TT[1]} | {TT[0]}");
            }
            Console.WriteLine("");

            Console.WriteLine("Zaal 2");
            foreach (string[] TT in Schedule.Item2)
            {
                Console.WriteLine($"{TT[1]} | {TT[0]}");
            }
            Console.WriteLine("");

            Console.WriteLine("Zaal 3");
            foreach (string[] TT in Schedule.Item3)
            {
                Console.WriteLine($"{TT[1]} | {TT[0]}");
            }
            Console.WriteLine("");
        }

        public static void ChooseDay()
        {
            Console_Reset.clear();
            Console.WriteLine("Kies hieruit een dag om te bekijken:\n");
            string[] dagen = new string[] { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag" };
            for(int i = 0; i < dagen.Length; i++)
            {
                Console.WriteLine($"{i+1}.\t{dagen[i]}");
            }
            Console.WriteLine("");
        }

        public void Navigation()
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
                    Console_Reset.clear();
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
                        Console_Reset.clear();
                        Console.WriteLine("vul '<' in om terug te gaan\n");
                        PrintDaySchedule(selection - 1, this.movieSchemaList);
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
