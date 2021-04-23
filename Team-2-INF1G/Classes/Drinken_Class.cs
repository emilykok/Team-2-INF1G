using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Drinken_Class 
{
    public class Drinken
    {
        // declaring struct for json data
        public struct DrinkenData
        {
            public string naam;
            public string inhoud;
            public double prijs;
            public string voedingswaarde;
            public string[] allergenen;
            public string[] tags;
            public int clicks;
        }

        // creates a list of type DrinkenData
        public List<DrinkenData> drinkenDataList = new List<DrinkenData>();

        [JsonIgnore]
        public string jsonPath;
        [JsonIgnore]
        public string path;

        // Constructor
        public Drinken()
        {
            this.path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Drinks.json"));
            this.jsonPath = File.ReadAllText(path);
            this.drinkenDataList = JsonConvert.DeserializeObject<List<DrinkenData>>(jsonPath);
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this.drinkenDataList, Formatting.Indented);
        }

        // drinkendisplay
        public void DrinkenMenu()
        {
            Console.WriteLine("Drinken Menu:\n1. Cola - \t\t2,99\n2. Pepsi - \t\t3,49\n3. Dr.Pepper - \t\t2,99\n4. Fanta Orange - \t2,99\n5. Spa rood - \t\t1,99\n6. Spa blauw - \t\t1,99\n7. Appelsap - \t\t2,49\n8. Rode wijn - \t\t6,49\n9. Witte wijn - \t6,49\n10. Heineken - \t\t3,49");
            Console.WriteLine("\nTyp het nummer van de item die je wilt bekijken en klik op enter:");

            // leest input command van de console
            string input = Console.ReadLine();
            Console.Clear();

            try // in t geval dat de input te hoog is of niet convertible is tot int
            {
                int num = Convert.ToInt32(input);
                // print naam item
                Console.WriteLine($"{drinkenDataList[num - 1].naam}\n");

                // print inhoud item
                Console.WriteLine($"inhoud: {drinkenDataList[num - 1].inhoud}");

                // print prijs item
                Console.WriteLine($"prijs: {drinkenDataList[num - 1].prijs}+\n");

                // print voedingswaarde
                Console.WriteLine($"{drinkenDataList[num - 1].voedingswaarde}\n");

                // print allergenen
                string al = "";
                for (int i = 0; i < (drinkenDataList[num - 1].allergenen).Length; i++)
                {
                    al += drinkenDataList[num - 1].allergenen[i] + ", ";
                }
                Console.WriteLine("allergenen: " + al);

                // input na de display
                bool goodInput = false;
                while (!goodInput)
                {
                    Console.WriteLine("\nTyp 'x' om terug te gaan of 'r' om een andere item te selecteren.");
                    input = Console.ReadLine();
                    Console.Clear();
                    if (input == "x" || input == "'x'" || input == "X" || input == "'X'") { goodInput = true; break; }
                    else if (input == "r" || input == "'r'") { DrinkenMenu(); goodInput = true; break; }
                    else Console.WriteLine("Er is een onjuist command ingevuld, probeer het nog eens.");
                }
            }

            catch (Exception)
            { // input is niet convertible naar int of input is te hoog nummer
                Console.Clear();
                Console.WriteLine("De input is niet juist, probeer het nogeens\n");
                DrinkenMenu();
            }
            Console.Clear();

        }

        // Method that deletes entry at certain index
        public void DeleteDrinken(int index)
        {
            drinkenDataList.RemoveAt(index);
        }

        // method to view clicks of certain index
        public void ViewClicks(int num) {
            Console.Clear();
            Console.WriteLine(drinkenDataList[num-1].clicks);
        }

        //method to update clicks
        public void UpdateClicks(int n)
        {
            int index = n - 1;
            // unloading the struct item at given index
            string Naam = drinkenDataList[index].naam;
            string Inhoud = drinkenDataList[index].inhoud;
            double Prijs = drinkenDataList[index].prijs;
            string Voedingswaarde = drinkenDataList[index].voedingswaarde;
            string[] Allergenen = drinkenDataList[index].allergenen;
            string[] Tags = drinkenDataList[index].tags;
            int Clicks = drinkenDataList[index].clicks;

            // increment clicks
            Clicks++;

            // Creating the struct item
            DrinkenData newDrinkenData = new DrinkenData();

            newDrinkenData.naam = Naam;
            newDrinkenData.inhoud = Inhoud;
            newDrinkenData.prijs = Prijs;
            newDrinkenData.voedingswaarde = Voedingswaarde;
            newDrinkenData.allergenen = Allergenen;
            newDrinkenData.tags = Tags;
            newDrinkenData.clicks = Clicks;

            // deletes entry (as struct is immutable)
            DeleteDrinken(index);

            // add to the list with the added data [indexed]!
            drinkenDataList.Insert(index, newDrinkenData);

            // write to the JSON file (updates the file)
            System.IO.File.WriteAllText(this.path, ToJSON());
        }

        //clears all stored clicks
        public void ClearAllClicks()
        {
            for (int i = 0; i < drinkenDataList.Count; i++)
            {
                ClearClicks(i + 1);
            }
        }

        // clears click at certain index
        public void ClearClicks(int n)
        {
            int index = n - 1;
            // unloading the struct item at given index
            string Naam = drinkenDataList[index].naam;
            string Inhoud = drinkenDataList[index].inhoud;
            double Prijs = drinkenDataList[index].prijs;
            string Voedingswaarde = drinkenDataList[index].voedingswaarde;
            string[] Allergenen = drinkenDataList[index].allergenen;
            string[] Tags = drinkenDataList[index].tags;
            int Clicks = drinkenDataList[index].clicks;

            // resets count to zero
            Clicks = 0;

            // Creating the struct item again
            DrinkenData newDrinkenData = new DrinkenData();

            newDrinkenData.naam = Naam;
            newDrinkenData.inhoud = Inhoud;
            newDrinkenData.prijs = Prijs;
            newDrinkenData.voedingswaarde = Voedingswaarde;
            newDrinkenData.allergenen = Allergenen;
            newDrinkenData.tags = Tags;
            newDrinkenData.clicks = Clicks;

            // deletes entry (as struct is immutable)
            DeleteDrinken(index);

            // add to the list with the added data [indexed]!
            drinkenDataList.Insert(index, newDrinkenData);

            // write to the JSON file (updates the file)
            System.IO.File.WriteAllText(this.path, ToJSON());
        }
    }
}
