using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

using Account_Class;
using Eten_Class;
using Drinken_Class;
using MovieDetail_Class;
using Ticket_Class;
using Hoofdmenu;

namespace Team_2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // BJORN GEDEELTE //
            //Account acc = new Account();

            // Creates user in JSON
            //acc.TextCreateUser();

            // Login with username and password
            //int print_this = acc.TextLogin();
            //Console.WriteLine(print_this);

            // MELISSA GEDEELTE //
            Eten eten = new Eten();
            //eten.EtenMenu();
            //Drinken drinken = new Drinken();
            //drinken.DrinkenMenu();
            Console.WriteLine(eten.EtenFilter("popcorn"));
            //Console.WriteLine(eten.EtenAllergieFilter("lacto"));

            // DAVID GEDEELTE //
            //MovieDetail.CodeActivate();

            // NOAH GEDEELTE //
            //reservering resv = new reservering();
            //resv.RunTickets();

            // JAMIE GEDEELTE //
            //Applicatie myApllicatie = new Applicatie();
            // myApllicatie.Start();
        }
    }
}
