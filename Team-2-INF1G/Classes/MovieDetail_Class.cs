using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Formatting = Newtonsoft.Json.Formatting;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;
using static System.Console;

using Hoofdmenu;

namespace MovieDetail_Class
{
    public class MovieDetail
    {
        // gets the movie info out of the json //
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
            // get path of the json file //
            this.path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Catalog.json"));
            this.jsonPath = File.ReadAllText(path);
            this.movieDataList = JsonConvert.DeserializeObject<List<MovieData>>(jsonPath);
        }
        /*
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this.movieDataList, Formatting.Indented);
        }
        
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
            // function to make a singular string out of a string array //
            string s = "";
            for (int i = 0; i < (before.Length - 1); i++)
            {
                s += before[i] + ", ";
            }
            s += before[(before.Length - 1)];
            return s;
        }

        public static void DisplayMovie(int index)
        {
            // shows all info on the chosen movie //
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
            // shows a list of movies(page of movieTitles) //
            MovieDetail movie = new MovieDetail();
            Console.WriteLine("vul < of > in om te navigeren tussen de bladzijden, druk het corresponderende nummer in om\nde film informatie te zien.");
            Console.WriteLine($"page {page}");
            int x = 0;
            // calculation to get the right index for the json file //
            if (page != 1) x = (10 * (page - 1));
            for (int i = x, count = 0; i < (x + 10); i++, count++)
            {
                Console.WriteLine($"{i + 1}.\t{movie.movieDataList[i].titel}");
            }
        }

        public static string[] GetList(int page)
        {
            // returns a string array of movie titles on that page //
            MovieDetail movie = new MovieDetail();
            int x = 0;
            string[] moviePage = new string[13];
            if (page != 1) x = (10 * (page - 1));
            for (int i = x, count = 0; i < (x + 10); i++, count++)
            {
                moviePage[count] = ($"{i + 1}.\t{movie.movieDataList[i].titel}");
            }
            moviePage[10] = "vorige pagina";
            moviePage[11] = "volgende pagina";
            moviePage[12] = "exit";
            return moviePage;
        }
        public static void Navigation()
        {
            // Navigation of the catalog //
            int page = 1;
            Menu select = new Menu("press", GetList(page));
            select.Run();

            ConsoleKey keyPressed;
            bool retry = true;
            // create a while loop to keep running navigation //
            while (retry)
            {
                // after index 9 the navigation options are placed //
                if(select.SelectedIndex >= 10)
                {
                    // to go back a page //
                    if(select.SelectedIndex == 10)
                    {
                        if (page <= 1)
                        {
                            page = 1;
                            select.Options = GetList(page);
                            select.Run();
                        }
                        else
                        {
                            page--;
                            select.Options = GetList(page);
                            select.Run();
                        }
                    }
                    // to go to the next page //
                    else if(select.SelectedIndex == 11)
                    {
                        if (page >= 5)
                        {
                            page = 5;
                            select.Options = GetList(page);
                            select.Run();
                        }
                        else
                        {
                            page++;
                            select.Options = GetList(page);
                            select.Run();
                        }
                    }
                    // to exit //
                    else if(select.SelectedIndex == 12)
                    {
                        retry = false;
                    }
                }
                // if one of the movies is selected(index 0-9) //
                else
                {
                    // clear the console and print the movie info //
                    Console.Clear();
                    DisplayMovie(((page - 1) * 10) + (select.SelectedIndex + 1));// get the right index for the Json file //
                    ConsoleKeyInfo keyInfo = ReadKey(true);
                    keyPressed = keyInfo.Key;
                    // change the selection menu for select //
                    select.Options = new string[] { "terug naar lijst", "exit" };
                    select.SelectedIndex = 0;
                    select.Run();
                    // to go back to the list of movies //
                    if(select.SelectedIndex == 0)
                    {
                        select.Options = GetList(page);
                        select.Run();
                    }
                    // to exit //
                    else if(select.SelectedIndex == 1)
                    {
                        retry = false;
                    }
                }
            }
        }
    }
}
