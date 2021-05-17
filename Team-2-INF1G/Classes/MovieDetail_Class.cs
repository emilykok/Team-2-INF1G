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

        public static string DisplayMovie(int index)
        {
            // shows all info on the chosen movie //
            MovieDetail movie = new MovieDetail();
            string run = "";
            run += ($"Titel: {movie.movieDataList[index - 1].titel}\n\n");
            run += ($"Genre: {ArrToString(movie.movieDataList[index - 1].genre)}\nKijkwijzer: {ArrToString(movie.movieDataList[index - 1].kijkwijzer)}\n\n");
            run += ($"Regisseur: {movie.movieDataList[index - 1].regisseur}\nActeurs: {ArrToString(movie.movieDataList[index - 1].acteurs)}\n\n");
            run += ($"samenvatting: {movie.movieDataList[index - 1].samenvatting}\n\n");
            run += ($"Rating: {movie.movieDataList[index - 1].rating}\nSpeeltijd: {movie.movieDataList[index - 1].speeltijd} minuten\n");

            return run;
        }

        public static string[] GetList(int page)
        {
            // returns a string array of movie titles on that page //
            MovieDetail movie = new MovieDetail();
            int x = 0;
            string[] moviePage = new string[14];
            if (page != 1) x = (10 * (page - 1));
            for (int i = x, count = 0; i < (x + 10); i++, count++)
            {
                moviePage[count] = ($"{i + 1}.\t{movie.movieDataList[i].titel}");
            }
            moviePage[10] = "vorige pagina";
            moviePage[11] = "volgende pagina";
            moviePage[12] = "filter films";
            moviePage[13] = "terug naar hoofmenu";
            return moviePage;
        }
        
        public static string[][] movieFilter(string tag)
        {
            // movielist with filter applied //
            int count = 0;
            MovieDetail movie = new MovieDetail();
            // determine how long the array will be //
            for (int i = 0; i < movie.movieDataList.Count; i++)
            {
                for(int j = 0; j < movie.movieDataList[i].genre.Length; j++)
                {
                    if (tag == movie.movieDataList[i].genre[j]) count++;
                }
            }

            // create one string array for the entire filtered list with movies //
            string[] included = new string[count+1];
            for (int i = 0, arrayIndex = 0; i < movie.movieDataList.Count; i++)
            {
                for (int j = 0; j < movie.movieDataList[i].genre.Length; j++)
                {
                    if (tag == movie.movieDataList[i].genre[j])
                    {
                        included[arrayIndex] = movie.movieDataList[i].titel;
                        arrayIndex++;
                    }
                }
            }

            // divide the list into pages of max. 10 movies a page //
            int movieIndex = 0;
            string[][] divided;
            if (count <= 10) divided = new string[1][];
            else divided = new string[(count / 10 + 1)][];
            for(int i = 0; i < divided.Length; i++)
            {
                if(count >= 10) divided[i] = new string[13];
                else divided[i] = new string[(count % 10)+3];

                for(int j = 0; j < (divided[i].Length-3); j++)
                {
                    divided[i][j] = included[movieIndex++];
                }
                divided[i][(divided[i].Length - 3)] = "vorige pagina";
                divided[i][(divided[i].Length - 2)] = "volgende pagina";
                divided[i][(divided[i].Length - 1)] = "terug naar reguliere catalogus";
                count -= 10;
            }
            return divided;
        }
        
        public static void FilterNavigation()
        {
            // navigation for the entire filter section //
            Console.Clear();
            // genre selection //
            string[] genreOptions = new string[] { "actie", "animatie", "avontuur", "drama", "familie", "fantasy", "horror", "komedie", "misdaad", "muziek", "mysterie", "romantiek", "sci-fi", "thriller" };
            Menu genre = new Menu("gebruik de pijltjes om te navigeren en druk op enter om te selecteren\nKies een genre om te filteren\n",
                                  genreOptions);
            genre.Run();
            string chosenGenre = genreOptions[genre.SelectedIndex];
            Console.Clear();
            // use chosen genre to display a list of movies that fit the filter //
            string[][] filterPages = movieFilter(chosenGenre);
            int currentPage = 0;
            bool retry = true;
            MovieDetail movie = new MovieDetail();
            Menu select = new Menu($"gebruik de pijltjes om te navigeren en druk op enter om te selecteren\ngefilterd genre: {chosenGenre}\n",
                                    filterPages[currentPage],
                                    $"\nPagina: {currentPage + 1} / {filterPages.Length}",
                                    filterPages[currentPage].Length - 3);
            select.Run();
            while (retry)
            {
                // the navigation options at the bottom of the page //
                if (select.SelectedIndex >= (filterPages[currentPage].Length - 3))
                {
                    // to go to the previous page //
                    if (select.SelectedIndex == (filterPages[currentPage].Length - 3))
                    {
                        if (currentPage <= 0)
                        {
                            currentPage = 0;
                            select.finalText = $"\nPagina: {currentPage + 1} / {filterPages.Length}";
                            select.Options = filterPages[currentPage];
                            select.whiteLine = filterPages[currentPage].Length - 3;
                            select.Run();
                        }
                        else
                        {
                            currentPage--;
                            select.SelectedIndex = (filterPages[currentPage].Length - 3);
                            select.finalText = $"\nPagina: {currentPage + 1} / {filterPages.Length}";
                            select.Options = filterPages[currentPage];
                            select.whiteLine = filterPages[currentPage].Length - 3;
                            select.Run();
                        }
                    }
                    // to go to the next page //
                    else if (select.SelectedIndex == (filterPages[currentPage].Length - 2))
                    {
                        if (currentPage >= (filterPages.Length - 1))
                        {
                            currentPage = (filterPages.Length - 1);
                            select.finalText = $"\nPagina: {currentPage + 1} / {filterPages.Length}";
                            select.Options = filterPages[currentPage];
                            select.whiteLine = filterPages[currentPage].Length - 3;
                            select.Run();
                        }
                        else
                        {
                            currentPage++;
                            select.SelectedIndex = (filterPages[currentPage].Length - 2);
                            select.finalText = $"\nPagina: {currentPage + 1} / {filterPages.Length}";
                            select.Options = filterPages[currentPage];
                            select.whiteLine = filterPages[currentPage].Length - 3;
                            select.Run();
                        }
                    }
                    // to return to the regular catalog //
                    else if(select.SelectedIndex == (filterPages[currentPage].Length - 1))
                    {
                        retry = false;
                    }
                }
                // get info about the selected movie //
                else
                {
                    // get the movie info from the json file //
                    string selected = filterPages[currentPage][select.SelectedIndex];
                    string info = "";
                    for(int i = 0; i < movie.movieDataList.Count; i++)
                    {
                        if(movie.movieDataList[i].titel == selected)
                        {
                            info = DisplayMovie(i+1);
                            // info has been found //
                        }

                    }
                    // clear the console and print the movie info //
                    Console.Clear();
                    select.finalText = "";
                    select.Prompt = info;
                    // change the selection menu for select //
                    select.Options = new string[] { "terug naar lijst", "terug naar reguliere catalogus" };
                    select.SelectedIndex = 0;
                    select.whiteLine = 0;
                    select.Run();
                    // to go back to the list of movies //
                    if (select.SelectedIndex == 0)
                    {
                        select.finalText = $"\nPagina: {currentPage + 1} / {filterPages.Length}";
                        select.Options = filterPages[currentPage];
                        select.whiteLine = filterPages[currentPage].Length - 3;
                        select.Prompt = "gebruik de pijltjes om te navigeren en druk op enter om te selecteren\n";
                        select.Run();
                    }
                    // to exit //
                    else if (select.SelectedIndex == 1)
                    {
                        retry = false;
                    }
                }
            }
        }

        public static void Navigation()
        {
            // Navigation of the catalog //
            int page = 1;
            Menu select = new Menu("gebruik de pijltjes om te navigeren en druk op enter om te selecteren\n", GetList(page), $"\nPagina: {page} / 5", 10);
            select.Run();

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
                            select.finalText = $"\nPagina: {page} / 5";
                            select.Options = GetList(page);
                            select.whiteLine = 10;
                            select.Run();
                        }
                        else
                        {
                            page--;
                            select.finalText = $"\nPagina: {page} / 5";
                            select.Options = GetList(page);
                            select.whiteLine = 10;
                            select.Run();
                        }
                    }
                    // to go to the next page //
                    else if(select.SelectedIndex == 11)
                    {
                        if (page >= 5)
                        {
                            page = 5;
                            select.finalText = $"\nPagina: {page} / 5";
                            select.Options = GetList(page);
                            select.whiteLine = 10;
                            select.Run();
                        }
                        else
                        {
                            page++;
                            select.finalText = $"\nPagina: {page} / 5";
                            select.Options = GetList(page);
                            select.whiteLine = 10;
                            select.Run();
                        }
                    }
                    // to filter movies //
                    else if(select.SelectedIndex == 12) // WIP. note to self: create a seperate function for the filter navigation //
                    {
                        FilterNavigation();
                        select.Run();
                    }
                    // to exit //
                    else if(select.SelectedIndex == 13)
                    {
                        retry = false;
                    }
                }
                // if one of the movies is selected(index 0-9) //
                else
                {
                    // clear the console and print the movie info //
                    Console.Clear();
                    select.finalText = "";
                    select.Prompt = DisplayMovie(((page - 1) * 10) + (select.SelectedIndex + 1));// get the right index for the Json file //
                    // change the selection menu for select //
                    select.Options = new string[] { "terug naar lijst", "terug naar hoofdmenu" };
                    select.SelectedIndex = 0;
                    select.Run();
                    // to go back to the list of movies //
                    if(select.SelectedIndex == 0)
                    {
                        select.finalText = $"\nPagina: {page} / 5";
                        select.Prompt = "gebruik de pijltjes om te navigeren en druk op enter om te selecteren\n";
                        select.Options = GetList(page);
                        select.whiteLine = 10;
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
