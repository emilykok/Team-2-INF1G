using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

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
            public string row;
            public string seat;
            public int reservationNumber;
        }

        public List<Tickets> TicketsList = new List<Tickets>();

        [JsonIgnore]
        public string pathCatalog;
        [JsonIgnore]
        public string jsonPathCatalog;

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

            this.pathTickets = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Tickets.json"));
            this.jsonPathTickets = File.ReadAllText(pathTickets);

            this.pathAccounts = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Accounts.json"));
            this.jsonPathAccounts = File.ReadAllText(pathAccounts);

            this.movieDataList = JsonConvert.DeserializeObject<List<MovieData>>(jsonPathCatalog);
            this.TicketsList = JsonConvert.DeserializeObject<List<Tickets>>(jsonPathTickets);
            this.accountDataList = JsonConvert.DeserializeObject<List<AccountData>>(jsonPathAccounts);
        }

        // Method that is used to write data to the JSON file.
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this.TicketsList, Formatting.Indented);
        }

        //// Miscellaneous methods
        // Method that print the items (here from the reservation list)
        public void PrintItem(int start, int stop, List<int> reservationList)
        {
            for (int i = 0; i < TicketsList.Count; i++)
            {
                try
                {
                    Console.WriteLine($"[{i + 1}] {TicketsList[i].filmName} | {TicketsList[i].startTime} {TicketsList[i].weekday}");
                }
                catch
                {
                    break;
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


        //// USER INPUTS FOR CREATION TICKET
        // Method that asks user the amount of people reserving, returns string of the amount (easier to compute)
        public string PersonAmount()
        {
            bool loop = true;
            bool falseInput = false;

            while(loop == true)
            {
                Console.Clear();
                Console.WriteLine("----Reserveren----");
                if (falseInput == true)
                {
                    Console.WriteLine("Ongeldige waarde meegevem, probeer het opnieuw");
                }
                Console.WriteLine("\nVoor hoeveel personen wilt u reserveren?");
                var PersonCount = Console.ReadLine();
                Console.WriteLine($"\nGegeven Waarde: {PersonCount} \nOm toe te passen toets ENTER\nOm opniew te proberen, toets 'r'\nOm terug te gaan, toets 'x'");

                if (PersonCount == "x" || PersonCount == "X")
                {
                    loop = false;
                    return ""; // Returns nothing
                }
                else if (PersonCount == "r" || PersonCount == "R")
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
            return ""; // Returns nothing <-- check for the compiler so that it doesnt nag
        }

        // Method that asks user which hall and date it wants from the schedule, returns string array
        public string[] HallNDate()
        {
            return null; // User must select an hall and date from the schedule...
        }


        //// RESERVATION LIST AND DISPLAY
        // Method that prints the list of reservation of specific user
        public void reservationList(int user)
        {
            string state = " ";
            int start = 0;
            int stop = 0;
            bool loop = true;

            while (loop == true)
            {
                bool executeRun = true;

                // creates a list of all the users reservations, MUST be in the loop, so that it refreshes if a reservation is deleted
                List<int> reservationList = new List<int>();

                for (int i = 0; i < TicketsList.Count; i++)
                {
                    if (TicketsList[i].namePerson == accountDataList[user].Name)
                    {
                        reservationList.Add(TicketsList[i].reservationNumber);
                    }
                }

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
                        Console.Clear();
                        Console.WriteLine("------------------------------");
                        Console.WriteLine("Vul < of > in om te navigeren tussen de bladzijden. \nVoer het corresponderende nummer in om de naar de reservering te gaan\nOm te stoppen toets X.\n");

                        // Print the users
                        PrintItem(start, stop, reservationList);

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
                    Console.Clear();
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
            Console.Clear();
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
            Console.WriteLine("Rij: " + ticket.row); 
            Console.WriteLine("Stoel: " + ticket.seat); 
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

            //Get zaal / datum / tijd for the ticket
            // Insert function here, MUST return string array [amount, hall, weekday, time]

            string ticketAmount = PersonAmount();
            if (ticketAmount == "")
            {
                // NEEDS TO STOP HERE?!!!!
            }


            string[] funcHallArray = { "3", "Zaal 1", "maandag", "14:00" };

            //Get row / seat for the specific hall
            // Insert function here , MUST return string [slice first letter for row, rest convert to int]

            string[] funcRSArray = { "A", "35" };


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
            newTicket.row               = funcRSArray[0];
            newTicket.seat              = funcRSArray[1];
            newTicket.reservationNumber = reservationNumber;

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
        }
        
        // Method that deletes ticket, requires the reservationNumber of the specific ticket
        public void DeleteTicket(int reservationNumber)
        {
            for (int i = 0; i < TicketsList.Count; i++)
            {
                if (reservationNumber == TicketsList[i].reservationNumber)
                {
                    TicketsList.RemoveAt(i);
                }
            }
            System.IO.File.WriteAllText(this.pathTickets, ToJSON());
        }
    }
}