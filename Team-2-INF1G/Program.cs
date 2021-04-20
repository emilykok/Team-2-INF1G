using System;

namespace CinaMatrix
{
    public class MovieDetail
    {
        // film informatie uit de json file halen(WIP) //
        public string Titel;
        public double rating;
        public Tuple<int, string[]> kijkwijzer;
        public string[] genre;
        public string regisseur;
        public int speeltijd;
        public string[] acteurs;
        public string samenvatting;

        public MovieDetail()
        {
            this.Titel = "The Broken Hearts Gallery";
            this.rating = 6.2;
            this.kijkwijzer = Tuple.Create(12, new string[] { "seks", "grof taalgebruik" });
            this.genre = new string[] { "romantiek", "komedie" };
            this.regisseur = "Natalie Krinsky";
            this.speeltijd = 108;
            this.acteurs = new string[] { "Geraldine Viswanathan", "Dacre Montgomery", "Utkarsh Ambudkar" };
            this.samenvatting = "De twintiger Lucy werkt in een kunstgalerie en is op persoonlijk vlak een grote verzamelaar.\nAls ze door haar vriendje gedumpt wordt, krijgt ze het idee om het project The Broken Heart Gallery op te zetten,\neen verzamelplek voor alle objecten die te maken hebben met haar vroegere liefdes. De expositie wordt een succes en\nkrijgt navolging.";
        }

        public static string[] GetList(int page)
        {
            int index;
            if (page == 1) index = 1;
            else index = 1 + (10 * (page - 1));
            string[] titleList;
            if (index == 1)
            {
                titleList = new string[10] { "The Broken Hearts Gallery", "Words On The Bathroom Walls", "Valley Girl", "The Half of It", "The Lovebirds", "Happiest Season", "Titanic", "Dirty Dancing", "The Kissing Booth 2", "Deadpool" };
            }
            else
            {
                titleList = new string[10] { "Men in Black: International", "Godzilla: King of the Monsters", "Captain Marvel", "X-Men: Dark Phoenix", "Star Wars: The Rise of Skywalker", "Io", "Avengers: Endgame", "Code 8", "I Am Mother", "Ad Astra" };
            }
            return titleList;
        }
    }

    class Program
    {
        public static string ArrToString(string[] before)
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

        public static void DisplayMovie()
        {
            // laat alle informatie van de gekozen film zien //
            MovieDetail a = new MovieDetail();
            Console.WriteLine($"Titel: {a.Titel}\n");
            Console.WriteLine($"Genre: {ArrToString(a.genre)}\nKijkwijzer: {a.kijkwijzer.Item1}, {ArrToString(a.kijkwijzer.Item2)}\n");
            Console.WriteLine($"Regisseur: {a.regisseur}\nActeurs: {ArrToString(a.acteurs)}\n");
            Console.WriteLine($"samenvatting: {a.samenvatting}\n");
            Console.WriteLine($"Rating: {a.rating}\nSpeeltijd: {a.speeltijd} minuten");
        }

        public static void MovieList(int page)
        {
            // laat een lijst aan films zien //
            string[] titleList = MovieDetail.GetList(page);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{i + 1}.  {titleList[i]}");
            }
        }
        static void Main(string[] args)
        {
            int page = 1;
            MovieList(page);
            while (true)
            {
                var navigation = Console.ReadLine();
                if (navigation == "<" || navigation == ">")
                {
                    if (navigation == "<" && page > 1)
                    {
                        Console.Clear();
                        page--;
                        MovieList(page);
                    }
                    else if (navigation == ">" && page < 2)
                    {
                        Console.Clear();
                        page++;
                        MovieList(page);
                    }
                }
                else
                {
                    // input om proberen te zetten naar int //
                    try
                    {
                        int selection = Convert.ToInt32(navigation);
                        Console.Clear();
                        if (selection == 1) DisplayMovie();
                        else Console.WriteLine("deze film is nog niet beschikbaar");
                    }
                    catch
                    {
                        Console.WriteLine("dit is geen geldige invulwaarde");
                    }
                }
            }
        }
    }
}