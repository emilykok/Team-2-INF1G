using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Eten_Class
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
            Console.Clear();
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
                UpdateClicks(num);

                // input na de display
                bool goodInput = false;
                while (!goodInput)
                {
                    Console.WriteLine("\nTyp 'x' om terug te gaan of 'r' om een andere item te selecteren.");
                    input = Console.ReadLine();
                    Console.Clear();
                    if (input == "x" || input == "'x'" || input == "X" || input == "'X'") { goodInput = true; break; }
                    else if (input == "r" || input == "'r'") { EtenMenu(); goodInput = true; break; }
                    else Console.WriteLine("Er is een onjuist command ingevuld, probeer het nog eens.");
                }
            }

            catch (Exception)
            { // input is niet convertible naar int of input is te hoog nummer
                Console.Clear();
                Console.WriteLine("De input is niet juist, probeer het nogeens\n");
                EtenMenu();
            }
            Console.Clear();

        }

        // Method that deletes entry at certain index
        public void DeleteEten(int index)
        {
            etenDataList.RemoveAt(index);
        }

        // method to view clicks of certain index
        public void ViewClicks(int num)
        {
            Console.Clear();
            Console.WriteLine(etenDataList[num - 1].clicks);
        }

        //method to update clicks
        public void UpdateClicks(int n)
        {
            int index = n - 1;
            // unloading the struct item at given index
            string Naam = etenDataList[index].naam;
            string[] Inhoud = etenDataList[index].inhoud;
            double[] Prijs = etenDataList[index].prijs;
            string Voedingswaarde = etenDataList[index].voedingswaarde;
            string[] Allergenen = etenDataList[index].allergenen;
            string[] Tags = etenDataList[index].tags;
            int Clicks = etenDataList[index].clicks;

            // increment clicks
            Clicks++;

            // Creating the struct item
            EtenData newEtenData = new EtenData();

            newEtenData.naam = Naam;
            newEtenData.inhoud = Inhoud;
            newEtenData.prijs = Prijs;
            newEtenData.voedingswaarde = Voedingswaarde;
            newEtenData.allergenen = Allergenen;
            newEtenData.tags = Tags;
            newEtenData.clicks = Clicks;

            // deletes entry (as struct is immutable)
            DeleteEten(index);

            // add to the list with the added data [indexed]!
            etenDataList.Insert(index, newEtenData);

            // write to the JSON file (updates the file)
            System.IO.File.WriteAllText(this.path, ToJSON());
        }

        //clears all stored clicks
        public void ClearAllClicks()
        {
            for (int i = 0; i < etenDataList.Count; i++)
            {
                ClearClicks(i + 1);
            }
        }
        
        // clears click at certain index
        public void ClearClicks(int n) {
            int index = n - 1;
            // unloading the struct item at given index
            string Naam = etenDataList[index].naam;
            string[] Inhoud = etenDataList[index].inhoud;
            double[] Prijs = etenDataList[index].prijs;
            string Voedingswaarde = etenDataList[index].voedingswaarde;
            string[] Allergenen = etenDataList[index].allergenen;
            string[] Tags = etenDataList[index].tags;
            int Clicks = etenDataList[index].clicks;

            // resets count to zero
            Clicks = 0;

            // Creating the struct item
            EtenData newEtenData = new EtenData();

            newEtenData.naam = Naam;
            newEtenData.inhoud = Inhoud;
            newEtenData.prijs = Prijs;
            newEtenData.voedingswaarde = Voedingswaarde;
            newEtenData.allergenen = Allergenen;
            newEtenData.tags = Tags;
            newEtenData.clicks = Clicks;

            // deletes entry (as struct is immutable)
            DeleteEten(index);

            // add to the list with the added data [indexed]!
            etenDataList.Insert(index, newEtenData);

            // write to the JSON file (updates the file)
            System.IO.File.WriteAllText(this.path, ToJSON());
        }

        // functie die weergeeft welke items de zoekterm bevatten in titel, tags of allergenen
        public string EtenFilter(string toFilter) {
            Console.Clear();
            string s = "";
            toFilter = toFilter.ToLower();
            try
            {   // loopt door alle items in de json
                for (int i = 0; i < etenDataList.Count; i++)
                {   
                    // zoekt in titel van item
                    if (etenDataList[i].naam.ToLower().Contains(toFilter)) s += etenDataList[i].naam + "\n";
                    // zoekt in tags
                    for (int j = 0; j < etenDataList[i].tags.Length; j++)
                    {
                        if (etenDataList[i].tags[j].Contains(toFilter)) s += etenDataList[i].naam + "\n";
                    }
                    // zoekt in allergenen
                    for (int j = 0; j < etenDataList[i].allergenen.Length; j++) {
                        if (etenDataList[i].allergenen[j].Contains(toFilter)) s += etenDataList[i].naam + "\n";
                    }
                }
                return $"Gevonden eten met zoekterm '{toFilter}':\n{s}"; ;
            }

            catch (Exception)
            {
                return "Input is invalid";
            }
            
        }

        // functie die weergeeft welke items de gegeven allergeen niet bevatten
        public string EtenAllergieFilter(string toFilter)
        {
            Console.Clear();
            toFilter = toFilter.ToLower();
            string s = "";
            bool check = true;
            try 
            {   // loopt door alle items in eten json
                for (int i = 0; i < etenDataList.Count; i++) {
                    // loopt door de allergenen in de item
                    for (int j = 0; j < etenDataList[i].allergenen.Length; j++)
                    {   // kijkt of de item allergeen bevat, zo ja gaat check op false en breekt de loop
                        if (etenDataList[i].allergenen[j].Contains(toFilter)) { check = false; break; }
                    }   // als de item het niet bevat wordt hij toegevoegd aan string
                    if (check) s += etenDataList[i].naam + "\n";
                }
                return $"Gevonden eten zonder zoekterm '{toFilter}':\n{s}";
            }
            catch {
                return "Input is invalid";
            }
        }

    }
}


