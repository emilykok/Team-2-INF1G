using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Reservatie_Class
{
    public class Reservering
    {
        // Is de Json van bjorn
        public struct AccountData
        {
            public string Name;
            public string Password;
            
            public int Age;
            public string Gender;
            public string Email;
            public string bankingDetails;
            public string[] Allergies;
            public bool Permission;
        }

        public List<AccountData> accountDataList = new List<AccountData>();


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
            public string prijs;
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

        [JsonIgnore]
        public string pathAccounts;
        [JsonIgnore]
        public string jsonPathAccounts;

        // Constructor
        public Reservering()
        {

            this.pathCatalog = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Catalog.json"));
            this.jsonPathCatalog = File.ReadAllText(pathCatalog);

            this.pathPrijskaarten = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Prijskaarten.json"));
            this.jsonPathPrijskaarten = File.ReadAllText(pathPrijskaarten);

            this.pathAccounts = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Accounts.json"));
            this.jsonPathAccounts = File.ReadAllText(pathAccounts);

            this.movieDataList = JsonConvert.DeserializeObject<List<MovieData>>(jsonPathCatalog);
            this.PrijsKaartenList = JsonConvert.DeserializeObject<List<PrijsKaarten>>(jsonPathPrijskaarten);
            this.accountDataList = JsonConvert.DeserializeObject<List<AccountData>>(jsonPathAccounts);
        }

        // Method that is used to write data to the JSON file.
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this.PrijsKaartenList, Formatting.Indented);
        }

        public void DisplayReservatie(MovieData movieData)
        {
            Console.WriteLine("===============");
            Console.WriteLine("*Reservatie pagina*");
            Console.WriteLine("===============\n");
            Console.WriteLine("Gerserveerde film: " + movieData.titel); //Goed
            Console.WriteLine("Naam: " + accountDataList[0].Name); //Goed

            //if (accountDataList[0].age >= 0 && accountDataList[0].age <= 10) <-- methods die variablen aanpassen NIET in de display method zetten!
            //    PrijsKaartenList[0].kaartnaam = "KinderKaart";


            Console.WriteLine("Kaartje: " + PrijsKaartenList[0].kaartnaam); //Zelf nog niet gedaan
            Console.WriteLine("Prijs: " + PrijsKaartenList[0].prijs); //Zelf nog niet gedaan
            Console.WriteLine("Leeftijd: " + PrijsKaartenList[0].leeftijd); //Zelf nog niet gedaan
            Console.WriteLine("Tijd: " + PrijsKaartenList[0].tijd); //Nog in de maak
            Console.WriteLine("Filmduur: " + movieData.speeltijd); //Goed
            Console.WriteLine("Zaal: " + PrijsKaartenList[0].zaal); //Nog nergens te bekennen
            Console.WriteLine("Rij: " + PrijsKaartenList[0].rij); //Nog nergens te bekennen
            Console.WriteLine("Stoel: " + PrijsKaartenList[0].stoel); //Nog nergens te bekennen
            //Console.WriteLine("Uw authentieke reservatie nummer is: "); <-- Dit werkt nog niet, en geeft ongeldige informatie mee! check hier nog op
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
                    if (x >= 1 && x <= 50)
                    {
                        check = false;
                    }
                }
                catch
                {
                    Console.WriteLine("Ongeldige invoer");
                }

                Console.WriteLine("\nU heeft nu een reservering gemaakt voor de film: " + movieDataList[x - 1].titel + ", klik op R om naar de reservatie te gaan.\nklik op 'X' om terug te gaan");


                bool retry = true;
                while (retry)
                {
                    var navigation = Console.ReadLine();
                    if (navigation == "r" || navigation == "R")
                    {
                        Console.Clear();
                        DisplayReservatie(movieDataList[x - 1]);
                        Console.WriteLine("\nOm terug te gaan, toets 'X'");
                        string whileInput = Console.ReadLine();
                        if (whileInput == "x" || whileInput == "X")
                        {
                            retry = false;
                        }
                    }
                    else
                    {
                        retry = false;
                    }
                }

            }



            PrijsKaarten newTicket = new PrijsKaarten();
            newTicket.filmnaam = movieDataList[x - 1].titel;
            newTicket.naampersoon = null;
            newTicket.kaartnaam = null;
            newTicket.prijs = null;
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