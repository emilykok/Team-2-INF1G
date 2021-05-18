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

        public int CreateTicket(MovieData movieData, AccountData accountData)
        {
            Tickets newTicket = new Tickets();
            newTicket.namePerson = accountData.Name;
            newTicket.filmName = movieData.titel;

            return -1;
        }
        public int ReservationNumber() //Makes reservation number
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

        public int PersonAmount()
        {
            Console.Clear();
            Console.WriteLine("voor hoeveel personen wil je reserveren?");
            var PersonCount = Console.ReadLine();
            if  (PersonCount == "x" || PersonCount == "X")
            {
                return -1;
            }
            else
            {
                try
                {
                    int count = Convert.ToInt32(PersonCount);
                    return count;
                }
                catch
                {
                    Console.WriteLine("dit is geen geldige waarde");
                    return -1;
                }
            }
        }

        //public string[] TimeSelection(string titel)
        //{
        //    int count = 0;
        //    for(int i = 0; i < ...datalist; i++)
        //    {
                // if titel == ...datalist[i].film1 count++
                // if titel == ...datalist[i].film2 count++
                // if titel == ...datalist[i].film3 count++
                // if titel == ...datalist[i].film4 count++
                // if titel == ...datalist[i].film5 count++
                // if titel == ...datalist[i].film6 count++
        //    }
        //}

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

        public bool ReservationConfirm()
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

            string[] funcHallArray = { "3", "Zaal 1", "maandag", "14:00" };

            //Get row / seat for the specific hall
            // Insert function here , MUST return string [slice first letter for row, rest convert to int]

            string[] funcRSArray = { "A", "35" };


            //Makes unique reservation number
            int reservationNumber = ReservationNumber();

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