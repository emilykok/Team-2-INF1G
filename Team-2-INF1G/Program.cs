using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Project
{
    public class Eten
    {
        // declaring struct for json data
        public struct EtenData
        {
            public string naam;
            public string[] inhoud;
            public double[] prijs;
            public string voedingswaarde;
            public string[] allergenen;
            public string[] tags;
            public int clicks;
        }

        // creates a list of type EtenData
        public List<EtenData> etenDataList = new List<EtenData>();

        [JsonIgnore]
        public string jsonPath;
        [JsonIgnore]
        public string path;

        // Constructor
        public Eten()
        {
            this.path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Foods.json"));
            this.jsonPath = File.ReadAllText(path);
            this.etenDataList = JsonConvert.DeserializeObject<List<EtenData>>(jsonPath);
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this.etenDataList, Formatting.Indented);
        }

        // fooddisplay
        public void EtenMenu()
        {
            Console.WriteLine("Eten Menu:\n1. Popcorn zoet - \t\tva 2,99\n2. Popcorn zout - \t\tva 2,49\n3. Popcorn karamel - \t\tva 2,49\n4. M&M's pinda - \t\t3,99\n5. M&M's chocola - \t\t4,49\n6. Chips naturel - \t\tva 2,99\n7. Chips paprika - \t\tva 2,99\n8. Doritos nacho cheese - \t3,99\n9. Haribo goudberen - \t\t3,49\n10. Skittles fruits - \t\t3,99");
            Console.WriteLine("\nTyp het nummer van de item die je wilt bekijken en klik op enter:");

            // leest input command van de console
            string input = Console.ReadLine();
            Console.Clear();

            try // in t geval dat de input te hoog is of niet convertible is tot int
            {
                int num = Convert.ToInt32(input);
                // print naam item
                Console.WriteLine($"{etenDataList[num - 1].naam}\n");

                // print inhoud item
                string inh = "";
                for (int i = 0; i < (etenDataList[num - 1].inhoud).Length; i++)
                {
                    inh += etenDataList[num - 1].inhoud[i] + ", ";
                }

                Console.WriteLine("inhoud: " + inh);
                // print prijs item
                string pri = "";
                for (int i = 0; i < (etenDataList[num - 1].prijs).Length; i++)
                {
                    pri += etenDataList[num - 1].prijs[i] + ", ";
                }
                Console.WriteLine("prijs: " + pri + "\n");

                // print voedingswaarde
                Console.WriteLine($"{etenDataList[num - 1].voedingswaarde}\n");

                // print allergenen
                string al = "";
                for (int i = 0; i < (etenDataList[num - 1].allergenen).Length; i++)
                {
                    al += etenDataList[num - 1].allergenen[i] + ", ";
                }
                Console.WriteLine("allergenen: " + al);

                // input na de display
                bool goodInput = false;
                while (!goodInput)
                {
                    Console.WriteLine("\nTyp 'x' om terug te gaan of '>' om een andere item te selecteren.");
                    input = Console.ReadLine();
                    Console.Clear();
                    if (input == "x" || input == "'x'" || input == "X" || input == "'X'") { goodInput = true; break; }
                    else if (input == ">" || input == "'>'") { EtenMenu(); goodInput = true; break; }
                    else Console.WriteLine("Er is een onjuist command ingevuld, probeer het nog eens.");
                }
            }

            catch (Exception e)
            { // input is niet convertible naar int of input is te hoog nummer
                Console.Clear();
                Console.WriteLine("De input is niet juist, probeer het nogeens\n");
                EtenMenu();
            }
            Console.Clear();

        }

        /* public void UpdateClick(int n)
         {
             int currClick = etenDataList[n - 1].clicks;
             currClick++;
             etenDataList[n - 1].clicks = currClick;

             // write to the JSON file (updates the file)
             System.IO.File.WriteAllText(this.path, ToJSON());
         }*/
    }

    class Program
    {
        static void Main(string[] args)
        {
            Eten eten = new Eten();
            eten.EtenMenu();
        }
    }
}    