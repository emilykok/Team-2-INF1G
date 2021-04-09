using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Team_2
{

    public class Account
    {
        public string Titel { get; set; }
        public double rating { get; set; }
        public Tuple<int, string[]> kijkwijzer;
        public string[] genre;
        public string regisseur;
        public int speeltijd;
        public string[] acteurs;
        public string samenvatting;


        [JsonIgnore] // Dit ignored de value voor als je naar JSON ombouwt
        public string path;
        [JsonIgnore]
        public string json_path;

        public string ToJSON()
        {
           return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public Account FromJSON(string json_path) // FromJSON returned een Account format, dus de field
        {
            return JsonConvert.DeserializeObject<Account>(json_path); ; 
        }

        public Account()
        {
            // Start van path vinden //
            this.path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Catalog.json"));
            this.json_path = File.ReadAllText(this.path);
            // End van path vinden //
        }
    }
    class Program
    {
        public static string arrToString(string[] before)
        {
            // functie om van string array een enkele string te maken //
            string s = "";
            for (int i = 0; i < (before.Length - 1); i++)
            {
                s += before[i] + ", ";
            }
            s += before[(before.Length - 1)];
            return s;
        }

        public static void displayMovie()
        {
            // laat alle informatie van de gekozen film zien //
            MovieDetail a = new MovieDetail();
            Console.WriteLine($"Titel: {a.Titel}\n");
            Console.WriteLine($"Genre: {arrToString(a.genre)}\nKijkwijzer: {a.kijkwijzer.Item1}, {arrToString(a.kijkwijzer.Item2)}\n");
            Console.WriteLine($"Regisseur: {a.regisseur}\nActeurs: {arrToString(a.acteurs)}\n");
            Console.WriteLine($"samenvatting: {a.samenvatting}\n");
            Console.WriteLine($"Rating: {a.rating}\nSpeeltijd: {a.speeltijd} minuten");
        }

        static void Main(string[] args) 
        {
            displayMovie();
            Console.WriteLine("type a number");
            int el = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"input was: {el}");
        }
    }
}
