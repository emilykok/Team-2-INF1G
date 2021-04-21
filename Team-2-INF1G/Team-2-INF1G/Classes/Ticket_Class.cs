using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Ticket_Class
{
    public class reservering
    {
        // Is de JSON file van David
        public struct MovieData
        {
            public string titel;
            public double rating;
            public string[] kijkwijzer;
            public string[] genre;
            public string regisseur;
            public int speeltijd;
            public string[] acteurs;
            public string samenvatting;
        }

        public List<MovieData> movieDataList = new List<MovieData>();

        // Noah JSON file
        public struct PrijsKaarten
        {
            public string filmnaam;
            public string naampersoon;
            public string kaartnaam;
            public int prijs;
            public string leeftijd;
            public string tijd;
            public string filmduur;
            public string zaal;
            public string rij;
            public string stoel;
        }

        public List<PrijsKaarten> PrijsKaartenList = new List<PrijsKaarten>();

        [JsonIgnore]
        public string pathCatalog;
        [JsonIgnore]
        public string jsonPathCatalog;

        [JsonIgnore]
        public string pathPrijskaarten;
        [JsonIgnore]
        public string jsonPathPrijskaarten;

        // Constructor
        public reservering()
        {

            this.pathCatalog = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Catalog.json"));
            this.jsonPathCatalog = File.ReadAllText(pathCatalog);

            this.pathPrijskaarten = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Prijskaarten.json"));
            this.jsonPathPrijskaarten = File.ReadAllText(pathPrijskaarten);

            this.movieDataList = JsonConvert.DeserializeObject<List<MovieData>>(jsonPathCatalog);
            this.PrijsKaartenList = JsonConvert.DeserializeObject<List<PrijsKaarten>>(jsonPathPrijskaarten);
        }

        // Method that is used to write data to the JSON file.
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this.PrijsKaartenList, Formatting.Indented);
        }

        public void RunTickets()
        {
            for (int i = 0; i < movieDataList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {movieDataList[i].titel}");
            }

            Console.WriteLine("\nKlik op het nummer van de bijbehoorende film om deze te reserveren.");

            int x = 0;

            bool check = true;
            while (check == true)
            {
                Console.WriteLine("Typ alstublieft een nummer van 1 tot 50");
                string input = Console.ReadLine();
                try
                {
                    x = Convert.ToInt32(input);
                    if (x > -1 && x < 50)
                    {
                        check = false;
                    }
                }
                catch
                {
                    Console.WriteLine("Ongeldige invoer");
                }
            }

            PrijsKaarten newTicket = new PrijsKaarten();
            newTicket.filmnaam = movieDataList[x - 1].titel;
            newTicket.naampersoon = null;
            newTicket.kaartnaam = null;
            newTicket.prijs = -1;
            newTicket.leeftijd = null;
            newTicket.tijd = null;
            newTicket.filmduur = null;
            newTicket.zaal = null;
            newTicket.rij = null;
            newTicket.stoel = null;
            
            // add to the list with the added data
            PrijsKaartenList.Add(newTicket);

            System.IO.File.WriteAllText(this.pathPrijskaarten, ToJSON());
        }
    }
}


/*
        class Program
    {
        static void Main(string[] args)
        {

            Catalogus catalogus = Catalogus.ReadJsonFromFile();
            
            for (int i = 0; i < catalogus.movies.Count; i++)
            {
                Console.WriteLine($"{i+1}. {catalogus.movies[i].titel}");
            }

            Console.WriteLine("\nKlik op het nummer van de bijbehoorende film om deze te reserveren.");

            int x = 0;
            while (!(int.TryParse(Console.ReadLine(), out x) && x > 0 && x <= 50))
            {
                Console.WriteLine("Typ alstublieft een nummer van 1 tot 50");
            };



            Reservering0 reservering0 = new Reservering0();
            //Console.WriteLine(reservering0);

            Prijskaarten prijskaarten = ReadJsonFromFile();
            
            if (x == 1)
            {
                prijskaarten.Kaartjes[0].filmnaam = catalogus.movies[0].titel;
            }
            
            //Testje om te kijken of ik van file naar file kon schrijven.vvvvv
            //prijskaarten.Kaartjes[0].filmduur = Catalogus.ReadJsonFromFile().movies[0].speeltijd;
            WriteJsonToFile(prijskaarten);



        }
        //write naar json
        static void WriteJsonToFile(Prijskaarten prijskaarten)
        {
            String text = JsonConvert.SerializeObject(prijskaarten, Formatting.Indented);
            File.WriteAllText(@"E:\Hogeschool Rotterdam\Project\doggor\Doggor\Prijskaarten.Json", text);
        }
        //read van json
        static Prijskaarten ReadJsonFromFile()
        {
            Prijskaarten prijskaarten = JsonConvert.DeserializeObject<Prijskaarten>(File.ReadAllText(@"E:\Hogeschool Rotterdam\Project\doggor\Doggor\Prijskaarten.Json"));
            return prijskaarten;
        }
        
    }

    //Bij een reservering gaat het nummer altijd 1x omhoog.
    class Reservering0
    {
        private static int reserveringsNummer = 0;
        public readonly int currentReservering;

        private static int getReserveringNummer()
        {
            return reserveringsNummer++;
        }

        public Reservering0()
        {
            this.currentReservering = getReserveringNummer();
        }
    }
    

    class Catalogus
    {
        
        public List<films> movies { get; set; }

        public static Catalogus ReadJsonFromFile()
        {
            Catalogus Catalog = JsonConvert.DeserializeObject<Catalogus>(File.ReadAllText(@"E:\Hogeschool Rotterdam\Project\doggor\Doggor\Catalog.Json"));
            return Catalog;
        }
    }

    class films
    {
        public string titel { get; set; }
        public string speeltijd { get; set; }
    }

    class Prijskaarten
    {
        public List<Kaartje> Kaartjes { get; set; }
    }
    class Kaartje
    {
        public string filmnaam { get; set; }
        public string naampersoon { get; set; }
        public string kaartnaam { get; set; }
        public double prijs { get; set; }
        public string leeftijd { get; set; }
        public string tijd { get; set; }
        public string filmduur { get; set; }
        public string zaal { get; set; }
        public string rij { get; set; }
        public string stoel { get; set; }
    }
}

*/