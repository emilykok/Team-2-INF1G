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
            public string[] availability;
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

        public static string IsSeatAvailable(int index, string film)
        {
            Theater choice = new Theater();
            if (choice.theaterDataList[index].availability.Length <= 0) return " ";
            else
            {
                for(int i = 0; i < choice.theaterDataList[index].availability.Length; i++)
                {
                    if(choice.theaterDataList[index].availability[i] == film)
                    {
                        return "X";
                    }
                }
                return " ";
            }
        }
        public static void Zaal150(string film)
        {
            int index = 0;
            string A1 = " ";

            Console.WriteLine("     1   2   3   4   5   6   7   8   9   10  11  12\n");
            string zaal150RijA = $"A           [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijB = $"B       [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijC = $"C       [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijD = $"D   [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijE = $"E   [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijF = $"F   [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijG = $"G   [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijH = $"H   [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijI = $"I   [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijJ = $"J   [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijK = $"K   [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijL = $"L       [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijM = $"M           [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";
            string zaal150RijN = $"N           [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}] [{IsSeatAvailable(index++, film)}]";

            Console.WriteLine($"{zaal150RijA}\n{zaal150RijB}\n{zaal150RijC}\n{zaal150RijD}\n{zaal150RijE}\n{zaal150RijF}\n{zaal150RijG}\n{zaal150RijH}\n{zaal150RijI}\n{zaal150RijJ}\n{zaal150RijK}\n{zaal150RijL}\n{zaal150RijM}\n{zaal150RijN}");
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
        public static void Run(string film)
        {
            Theater choice = new Theater();
            Console.Clear();
            Console.WriteLine($"Chosen: {choice.theaterDataList[0].position}");
            Zaal150(film);
            //Zaal300();
            //Zaal500();
            Console.ReadLine();
        }
    }
}