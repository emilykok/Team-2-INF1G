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
        // declaring struct
        public struct EtenData {
            public string naam;
            public string[] inhoud;
            public double[] prijs;
            public string voedingswaarde;
            public string[] allergenen;
            public string[] tags;
        }
        // creates a list of type EtenData
        public List<EtenData> etenDataList = new List<EtenData>();        
        
        [JsonIgnore]
        public string jsonPath;
        [JsonIgnore]
        public string path;

        // Constructor
        public Eten() {
            // Start van path vinden //
            this.path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Foods.json"));
            this.jsonPath = File.ReadAllText(path);
            this.etenDataList = JsonConvert.DeserializeObject<List<EtenData>>(jsonPath);
            // End van path vinden //
        }

        public string ToJSON(){
            return JsonConvert.SerializeObject(this.etenDataList, Formatting.Indented);
        }
        
        // menu //
        public void EtenMenu()
        {
            Console.WriteLine("1. Popcorn zoet - va 2,99\n2. Popcorn zout - va 2,49\n3. Popcorn karamel - va 2,49\n4. M&M's pinda - 3,99\n5. M&M's chocola - 4,49\n6. Chips naturel - va 2,99\n7. Chips paprika - va 2,99\n8. Doritos nacho cheese - 3,99\n9. Haribo goudberen - 3,49\n10. Skittles fruits - 3,99");
            Console.WriteLine("\nTyp het nummer van wat je wilt bekijken en klik op enter:");
            
            string input = Console.ReadLine();
            int num = Convert.ToInt32(input);
            Console.Clear();
            
            Console.WriteLine($"naam: {etenDataList[num-1].naam}\ninhoud: {etenDataList[num-1].inhoud}\n.....");
            Console.WriteLine("Typ 'x' om terug te gaan of '<' om een nieuwe item te selecteren");

            input = Console.ReadLine();
            if (input == "x" || input == "X") { };
            if (input == "<") EtenMenu();
            

            /*  if (num == 1) Console.WriteLine("naam: Popcorn zoet \ninhoud: 85g, 180g, 325g \nprijs: 2.99, 4.99, 6.99 \nvoedingswaarde per 100gr:\n   Energie: 1794kj(424kcal)\n   Vet: 5,2g\n   Waarvan verzadigd: 0,7g\n   Koolhydraten: 89g\n   Waarvan suikers: 55g\n   Voedingsvezel: 3,6g\n   Eiwitten: 3,5g\n   Zout: 0g \nAllergenen: veganistisch, glutenvrij, \ntags: zoet");
            else Console.WriteLine($"Item nummer {num} is nog niet toegevoegd.");*/
        }

        // food info //
        //public void etenInfo(){
        //Console.WriteLine(Foods.json[num-1][naam]);
        //Console.WriteLine(Foods.json[num-1][inhoud]);
        //Console.WriteLine(Foods.json[num-1][prijs]);
        //Console.WriteLine(Foods.json[num-1][voedingswaarde]);
        //Console.WriteLine(Foods.json[num-1][allergenen]);
        //Console.WriteLine(Foods.json[num-1][tags]);
        //Console.WriteLine("Toets x om terug te gaan");
        //if (Console.ReadLine() == 'x') call menu()
        //}
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