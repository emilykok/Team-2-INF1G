using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Formatting = Newtonsoft.Json.Formatting;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Team_2_INF1G
{
    public class MovieDetail
    {
        // film informatie uit de json file halen(WIP) //
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

        [JsonIgnore]
        public string jsonPath;
        [JsonIgnore]
        public string path;

        public MovieDetail()
        {
            path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Catalog.json"));
            jsonPath = File.ReadAllText(path);
            movieDataList = JsonConvert.DeserializeObject<List<MovieData>>(jsonPath);
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(movieDataList, Formatting.Indented);
        }
        /*
        public static string[] GetList(int page)
        {
            int index;
            if (page == 1) index = 1;
            else index = 1 + (10 * (page - 1));
            string[] titleList;
            //for(int i = 0, x = index; i < 10; i++, x++)
            //{
            //    titleList
            //}
            //return titleList;
        }
        */
        /*
        public void CreateMovie(string Titel, double Rating, string[] Kijkwijzer, string[] Genre, string Regisseur, int Speeltijd, string[] Acteurs, string Samenvatting)
        {
            MovieData newMovieData = new MovieData();
            newMovieData.titel = Titel;
            newMovieData.rating = Rating;
            newMovieData.kijkwijzer = Kijkwijzer;
            newMovieData.genre = Genre;
            newMovieData.regisseur = Regisseur;
            newMovieData.speeltijd = Speeltijd;
            newMovieData.acteurs = Acteurs;
            newMovieData.samenvatting = Samenvatting;

            movieDataList.Add(newMovieData);
            Console.WriteLine(movieDataList);
            System.IO.File.WriteAllText(this.path, ToJSON());
        }
        */

        public static string ArrToString(string[] before)
        {
            // functie om van string array een enkele string te maken //
            string s = "";
            for (int i = 0; i < before.Length - 1; i++)
            {
                s += before[i] + ", ";
            }
            s += before[before.Length - 1];
            return s;
        }

        public static void DisplayMovie(int index)
        {
            // laat alle informatie van de gekozen film zien //
            MovieDetail movie = new MovieDetail();
            Console.WriteLine("vul < in om terug te gaan naar de lijst");
            Console.WriteLine($"Titel: {movie.movieDataList[index - 1].titel}\n");
            Console.WriteLine($"Genre: {ArrToString(movie.movieDataList[index - 1].genre)}\nKijkwijzer: {ArrToString(movie.movieDataList[index - 1].kijkwijzer)}\n");
            Console.WriteLine($"Regisseur: {movie.movieDataList[index - 1].regisseur}\nActeurs: {ArrToString(movie.movieDataList[index - 1].acteurs)}\n");
            Console.WriteLine($"samenvatting: {movie.movieDataList[index - 1].samenvatting}\n");
            Console.WriteLine($"Rating: {movie.movieDataList[index - 1].rating}\nSpeeltijd: {movie.movieDataList[index - 1].speeltijd} minuten");
        }

        public static void MovieList(int page = 1)
        {
            // laat een lijst aan films zien //
            MovieDetail movie = new MovieDetail();
            Console.WriteLine("vul < of > in om te navigeren tussen de bladzijden, druk het corresponderende nummer in om\nde film informatie te zien.");
            Console.WriteLine($"page {page}");
            int x = 0;
            if (page != 1) x = 10 * (page - 1);
            for (int i = x; i < x + 10; i++)
            {
                Console.WriteLine($"{i + 1}.     {movie.movieDataList[i].titel}");
            }
        }

        public static void CodeActivate()
        {
            int page = 1;
            MovieList(page);
            bool retry = true;
            while (retry)
            {
                var navigation = Console.ReadLine();
                if (navigation == "<" && page == 1)
                {
                    Console.Clear();
                    MovieList();
                }
                else if (navigation == "<" || navigation == ">")
                {
                    if (navigation == "<" && page > 1)
                    {
                        Console.Clear();
                        page--;
                        MovieList(page);
                    }
                    else if (navigation == ">" && page < 5)
                    {
                        Console.Clear();
                        page++;
                        MovieList(page);
                    }
                }
                else if (navigation == "x" || navigation == "X")
                {
                    retry = false;
                }
                else
                {
                    int selection = Convert.ToInt32(navigation);
                    Console.Clear();
                    MovieDetail movie = new MovieDetail();
                    DisplayMovie(selection);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MovieDetail.CodeActivate();
        }
    }
}
