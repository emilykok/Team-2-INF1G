using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using SelectSpot_Class;

using Console_Buffer; 

namespace Reservatie_Class
{
    public class Reservering
    {
        // Account JSON struct
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

        // Schema JSON struct
        public struct MovieSchema
        {
            public string title;
            public string hall;
            public string day;
            public string time;
        }

        public List<MovieSchema> movieSchemaList = new List<MovieSchema>();

        // Movie JSON struct
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

        // Kaartje JSON struct
        public struct Tickets
        {
            public string filmName;
            public string namePerson;
            public int ticketAmount;
            public double price;
            public string weekday;
            public string startTime;
            public int filmDuration;
            public string hall;
            public string[] seatStrings;
            public int reservationNumber;
            public int[] seatIndexes;
        }

        public List<Tickets> TicketsList = new List<Tickets>();

        [JsonIgnore]
        public string pathCatalog;
        [JsonIgnore]
        public string jsonPathCatalog;

        [JsonIgnore]
        public string pathSchema;
        [JsonIgnore]
        public string jsonPathSchema;

        [JsonIgnore]
        public string pathTickets;
        [JsonIgnore]
        public string jsonPathTickets;

        [JsonIgnore]
        public string pathAccounts;
        [JsonIgnore]
        public string jsonPathAccounts;

        // Constructor
        public Reservering()
        {

            this.pathCatalog = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Catalog.json"));
            this.jsonPathCatalog = File.ReadAllText(pathCatalog);

            this.pathSchema = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Movie_Schema.json"));
            this.jsonPathSchema = File.ReadAllText(pathSchema);

            this.pathTickets = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Tickets.json"));
            this.jsonPathTickets = File.ReadAllText(pathTickets);

            this.pathAccounts = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Accounts.json"));
            this.jsonPathAccounts = File.ReadAllText(pathAccounts);

            this.movieDataList = JsonConvert.DeserializeObject<List<MovieData>>(jsonPathCatalog);
            this.movieSchemaList = JsonConvert.DeserializeObject<List<MovieSchema>>(jsonPathSchema);
            this.TicketsList = JsonConvert.DeserializeObject<List<Tickets>>(jsonPathTickets);
            this.accountDataList = JsonConvert.DeserializeObject<List<AccountData>>(jsonPathAccounts);
        }

        //// Json methods
        // Method that is used to write data to the JSON file.
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this.TicketsList, Formatting.Indented);
        }


        //// Miscellaneous methods
        // Method that print the items (here from the reservation list)
        public void PrintItemReservation(int start, int stop, List<int> reservationList)
        {
            for (int i = start; i < stop; i++)
            {
                for (int j = 0; j < TicketsList.Count; j++)
                {
                    if (TicketsList[j].reservationNumber == reservationList[i])
                    {
                        try
                        {
                            Console.WriteLine($"[{i + 1}] {TicketsList[j].filmName} | {TicketsList[j].startTime} {TicketsList[j].weekday}");
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
            }
        }

        // Method that print the items (here from the selection list)
        public void PrintItemSelection(string[][] selectionList)
        {
            for (int i = 0; i < selectionList.Length; i++)
            {
                try
                {
                    Console.WriteLine($"[{i + 1}] {selectionList[i][2]} | {selectionList[i][3]} {selectionList[i][1]}");
                }
                catch
                {
                    break;
                }
            }
        }

        // Method that loops through array of integers to seat(s)
        public void DeleteSeatLooper(int[] seats, string film, string day, string hall)
        {
            int zaal = 0;
            if (hall == "Zaal 1") zaal = 1;
            else if (hall == "Zaal 2") zaal = 2;
            else if (hall == "Zaal 3") zaal = 3;

            int dag = 0;
            if (day == "Maandag") dag = 0;
            else if (day == "Dinsdag") dag = 1;
            else if (day == "Woensdag") dag = 2;
            else if (day == "Donderdag") dag = 3;
            else if (day == "Vrijdag") dag = 4;
            else if (day == "Zaterdag") dag = 5;
            else if (day == "Zondag") dag = 6;

            foreach (int seat in seats)
            {
                Theater theater = new Theater();
                if (seat != -1)
                {
                    theater.RemoveAvailability(seat, film, dag, zaal);
                }
            }
        }



        // Method that returns the index of where the reservation number is in the JSON file
        private int ReservationNumberIndexer(int reservationNumber)
        {
            for (int i = 0; i < TicketsList.Count; i++)
            {
                if (reservationNumber == TicketsList[i].reservationNumber)
                {
                    return i;
                }
            }
            return -1;
        }
        
        // Method that creates a number for the reservation
        public int ReservationNumberMaker() //Makes reservation number
        {
            int reservationNumber = 0;
            try
            {
                int number = TicketsList[TicketsList.Count-1].reservationNumber;
                reservationNumber = number + 1;
            }
            catch
            {
                reservationNumber = 1;
            }
            return reservationNumber;
        }

        // Method that prompts the user if the reservation is correct, returns boolean
        private bool ReservationConfirm()
        {
            Console.WriteLine("\nKlik op ENTER op de reservering te plaatsen. Om te annuleren toets 'X'");
            string userInput = Console.ReadLine();
            if (userInput == "x" || userInput == "X")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Method that creates a list of all the reservation for given user
        public List<int> ReservationList(int user)
        {
            List<int> reservationList = new List<int>();

            for (int i = 0; i < TicketsList.Count; i++)
            {
                if (TicketsList[i].namePerson == accountDataList[user].Name)
                {
                    reservationList.Add(TicketsList[i].reservationNumber);
                }
            }
            return reservationList;
        }


        //// USER INPUTS FOR CREATION TICKET
        // Method that asks user the amount of people reserving, returns string of the amount (easier to compute)
        public string PersonAmount()
        {
            bool loop = true;
            bool falseInput = false;

            while(loop == true)
            {
                Console_Reset.clear();
                Console.WriteLine("----Reserveren----");
                if (falseInput == true)
                {
                    Console.WriteLine("Ongeldige waarde meegeven, probeer het opnieuw");
                }
                Console.WriteLine("\nVoor hoeveel personen wilt u reserveren?");
                var PersonCount = Console.ReadLine();
                Console.WriteLine($"\nGegeven Waarde: {PersonCount} \nOm toe te passen toets ENTER\nOm opniew te proberen, toets 'r'\nOm terug te gaan, toets 'x'");
                var confirm = Console.ReadLine();

                if (confirm == "x" || confirm == "X")
                {
                    loop = false;
                    return null; // Returns nothing
                }
                else if (confirm == "r" || confirm == "R")
                {
                    loop = true;
                }
                else if (PersonCount.Length == 0)
                {
                    loop = true;
                    falseInput = true;
                }
                else
                {
                    try
                    {
                        int check = Convert.ToInt32(PersonCount);
                        return PersonCount;
                    }
                    catch
                    {
                        falseInput = true;
                        loop = true;
                    }
                }
            }
            return null; // Returns nothing <-- check for the compiler so that it doesnt nag
        }

        // Method that asks user which hall and date it wants from the schedule, returns string array
        public string[] HallNDate(string title)
        {
            // Determine size of the array
            int count = 0;
            for (int i = 0; i < movieSchemaList.Count; i++)
            {
                if (movieSchemaList[i].title == title)
                {
                    count += 1;
                }
            }

            // Create array of movies that contains the title given in parameter
            string[][] selectionArr = new string[count][];

            count = 0;
            for (int i = 0; i < movieSchemaList.Count; i++)
            {
                if (movieSchemaList[i].title == title)
                {
                    // Creates a movie array 
                    string[] movie = {
                        movieSchemaList[i].title,
                        movieSchemaList[i].hall,
                        movieSchemaList[i].day,
                        movieSchemaList[i].time
                            };

                    // Forwards it to the selection array
                    selectionArr[count] = movie;
                    count += 1;
                }
            }

            // Checks if selectionArr is filled (if the movie is shown)
            if (selectionArr.Length == 0)
            {
                Console_Reset.clear();
                Console.WriteLine("----Reserveren----");
                Console.WriteLine("Op dit moment is deze film niet toonbaar bij ons.");
                Console.WriteLine("\nklik op enter om door te gaan");
                var nothing = Console.ReadLine();
                return null;
            }

            // Lets user select specific movie
            else
            {
                bool loop = true;
                bool falseInput = false;

                while (loop == true)
                {
                    Console_Reset.clear();
                    Console.WriteLine("----Reserveren----");
                    if (falseInput == true)
                    {
                        Console.WriteLine("Ongeldige waarde meegeven, probeer het opnieuw");
                    }
                    Console.WriteLine("\nSelecteer de dag, tijd en zaal.");
                    PrintItemSelection(selectionArr);
                    var userInput = Console.ReadLine();
                    Console.WriteLine($"\nGegeven Waarde: {userInput} \nOm toe te passen toets ENTER\nOm opniew te proberen, toets 'r'\nOm terug te gaan, toets 'x'");
                    var confirm = Console.ReadLine();

                    if (confirm == "x" || confirm == "X")
                    {
                        loop = false;
                        return null; // Returns nothing
                    }
                    else if (confirm == "r" || confirm == "R")
                    {
                        loop = true;
                    }
                    else if (userInput.Length == 0)
                    {
                        loop = true;
                        falseInput = true;
                    }
                    else
                    {
                        try
                        {
                            int check = Convert.ToInt32(userInput);
                            return selectionArr[check - 1];
                        }
                        catch
                        {
                            falseInput = true;
                            loop = true;
                        }
                    }
                }
            }
            return null; // Returns nothing <-- check for the compiler so that it doesnt nag
        }


        //// RESERVATION LIST AND DISPLAY
        // Method that prints the list of reservation of specific user
        public void ReservationUserPrint(int user)
        {
            string state = " ";
            int start = 0;
            int stop = 0;
            bool loop = true;

            while (loop == true)
            {
                bool executeRun = true;

                // creates a list of all the users reservations, MUST be in the loop, so that it refreshes if a reservation is deleted
                List<int> reservationList = ReservationList(user);

                if (reservationList.Count != 0)
                {
                    // runs code when page is at 0, no increase in value
                    if (start == 0 && (state != ">" && state != "<"))
                    {
                        if (start + 40 > reservationList.Count)
                        {
                            if (start == reservationList.Count)
                            {
                                executeRun = false;
                            }
                            stop = reservationList.Count;
                        }
                        else
                        {
                            stop = start + 20;
                        }
                    }

                    // runs code when page is NOT at 0, no increase in value
                    else if (start != 0 && (state != ">" && state != "<"))
                    {
                        if (start + 40 > reservationList.Count)
                        {
                            if (start == reservationList.Count)
                            {
                                executeRun = false;
                            }
                            stop = reservationList.Count;
                        }
                        else
                        {
                            stop = start + 20;
                        }
                    }
                    // runs code with 20 increment, stores value
                    else if (state == ">")
                    {
                        if ((start + 20) >= reservationList.Count)
                        {
                            executeRun = false;
                        }
                        else
                        {
                            start += 20;
                            stop = start + 20;
                        }
                    }

                    // Runs code with 20 decrement, stores value
                    else if (state == "<")
                    {
                        if ((start - 20) < reservationList.Count)
                        {
                            executeRun = false;
                        }
                        else
                        {
                            start -= 20;
                            stop = start + 20;
                        }
                    }

                    // Runs the code
                    if (executeRun == true)
                    {
                        // Header
                        Console_Reset.clear();
                        Console.WriteLine("------------------------------");
                        Console.WriteLine("Vul < of > in om te navigeren tussen de bladzijden. \nVoer het corresponderende nummer in om de naar de reservering te gaan\nOm te stoppen toets X.\n");

                        // Print the users
                        PrintItemReservation(start, stop, reservationList);

                        // current page indicator
                        int pageCounterCurrent = (start / 20) + 1;
                        int pageCounterAll = (accountDataList.Count / 20) + 1;
                        Console.WriteLine($"\nPage {pageCounterCurrent}/{pageCounterAll}");
                    }

                    // Process user input
                    string userInput = Console.ReadLine();
                    try
                    {
                        int convert = Convert.ToInt32(userInput);
                        convert -= 1;

                        bool inspectReservation = true;
                        while (inspectReservation == true)
                        {
                            DisplayReservatie(TicketsList[ReservationNumberIndexer(reservationList[convert])]);
                            Console.WriteLine("\nals u de reservatie wilt verwijderen, typ VERWIJDER. Om terug te gaan, toets 'X'");

                            string innerUserInput = Console.ReadLine();
                            if (innerUserInput == "x" || innerUserInput == "X")
                            {
                                inspectReservation = false;
                            }
                            else if (innerUserInput == "VERWIJDER")
                            {
                                DeleteTicket(reservationList[convert]);
                                inspectReservation = false;
                            }
                        }
                    }
                    catch
                    {
                        if (userInput == ">" || userInput == "<")
                        {
                            state = userInput;
                        }
                        else if (userInput == "x" || userInput == "X")
                        {
                            loop = false;
                            break;
                        }
                        else
                        {
                            state = " ";
                        }
                    }
                }
                else
                {
                    Console_Reset.clear();
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("\nEr zijn geen reserveringen gevonden");
                    Console.WriteLine("\nU kunt gemakkelijk een reservering maken door een film in onze catalogus te selecteren en op reserveren te drukken!");
                    Console.WriteLine("\n\nDruk een toets in om door te gaan");

                    string userInput = Console.ReadLine();
                    loop = false;
                }
            }
        }
    
        // Method that only prints the ticket, has the specific ticket as parameter
        public void DisplayReservatie(Tickets ticket)
        {
            Console_Reset.clear();

            string stoelen = "";

            if (ticket.seatIndexes.Length == 1)
            {
                stoelen = ticket.seatStrings[0];
            }
            else
            {
                foreach (int index in ticket.seatIndexes)
                {
                    stoelen += ticket.seatStrings[index] + " ";
                }
            }
            
            Console.WriteLine("===============");
            Console.WriteLine($"*Reservatie {ticket.filmName}*");
            Console.WriteLine("===============\n");
            Console.WriteLine("Gerserveerde film: " + ticket.filmName); 
            Console.WriteLine("Naam: " + ticket.namePerson); 
            Console.WriteLine("Kaartje(s): " + ticket.ticketAmount); 
            Console.WriteLine("Prijs: " + ticket.price); 
            Console.WriteLine("Dag: " + ticket.weekday); 
            Console.WriteLine("Tijd: " + ticket.startTime); 
            Console.WriteLine("Filmduur: " + ticket.filmDuration); 
            Console.WriteLine("Zaal: " + ticket.hall);
            Console.WriteLine($"Stoel(en): {stoelen}");
            Console.WriteLine("Reservatie nummer: " + ticket.reservationNumber);
        }


        //// CREATE AND DELETE TICKET
        // Method that Creates a ticket, requires the title and user as parameter !! CREATES AN WHILE LOOPS WITH DISPLAY !!
        public void CreateTicket(string title, int user) //Get the film title, index that to get film data. Get user int to index account and get the data
        {
            // Data preparation for creation ticket
            int movieIndex = -1;
            for (int i = 0; i < movieDataList.Count; i++) //Get the film index for corresponding data
            {
                if (title == movieDataList[i].titel)
                {
                    movieIndex = i;
                }
            }

            // Ask the user to select an hall date and time
            string[] HDT = HallNDate(title);
            if (HDT == null)
            {
                return;
            }

            // Ask the amount of people 
            string ticketAmount = PersonAmount();
            if (ticketAmount == null)
            {
                return;
            }

            string[] funcHallArray = { ticketAmount, HDT[1], HDT[3], HDT[2] };
            
            // Loops through amount of people, selects amounts of seats!
            int[] SeatIndexes = new int[Convert.ToInt32(ticketAmount)];
            string[] SeatStrings = new string[Convert.ToInt32(ticketAmount)];

            for (int i = 0; i < Convert.ToInt32(ticketAmount); i++)
            {
                Tuple<int, string> RS = Theater.Run(movieDataList[movieIndex].titel, funcHallArray[2], funcHallArray[1]);

                if (RS == null)
                {
                    this.DeleteSeatLooper(SeatIndexes, movieDataList[movieIndex].titel, funcHallArray[2], funcHallArray[1]);
                    return;
                }
                SeatIndexes[i] = RS.Item1;
                SeatStrings[i] = RS.Item2;
            }
            

            //Makes unique reservation number
            int reservationNumber = ReservationNumberMaker();

            // Convert all data for ticket creation

            Tickets newTicket = new Tickets();
            newTicket.filmName          = movieDataList[movieIndex].titel;
            newTicket.namePerson        = accountDataList[user].Name;
            newTicket.ticketAmount      = Convert.ToInt32(funcHallArray[0]);
            newTicket.price             = 20.00; // <-- HARD CODED!!!
            newTicket.weekday           = funcHallArray[2];
            newTicket.startTime         = funcHallArray[3];
            newTicket.filmDuration      = movieDataList[movieIndex].speeltijd;
            newTicket.hall              = funcHallArray[1];
            newTicket.seatStrings       = SeatStrings;
            newTicket.reservationNumber = reservationNumber;
            newTicket.seatIndexes       = SeatIndexes;

            // Displays selected reservation
            DisplayReservatie(newTicket);
            bool confirm = ReservationConfirm();

            if (confirm == true)
            {
                // add to the list with the added data
                TicketsList.Add(newTicket);

                // write to the JSON file (updates the file)
                System.IO.File.WriteAllText(this.pathTickets, ToJSON());
            }
            else
            {
                this.DeleteSeatLooper(newTicket.seatIndexes, newTicket.filmName, newTicket.weekday, newTicket.hall);
            }
        }
        
        // Method that deletes ticket, requires the reservationNumber of the specific ticket
        public void DeleteTicket(int reservationNumber)
        {
            for (int i = 0; i < TicketsList.Count; i++)
            {
                if (reservationNumber == TicketsList[i].reservationNumber)
                {
                    this.DeleteSeatLooper(TicketsList[i].seatIndexes, TicketsList[i].filmName, TicketsList[i].weekday, TicketsList[i].hall);
                    TicketsList.RemoveAt(i);
                }
            }
            System.IO.File.WriteAllText(this.pathTickets, ToJSON());
        }
    }
}